using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private TbGame tbGame;

    [SerializeField]
    private RtGame rtGame;

    [SerializeField]
    private Camera mainCamera;

    [Header("Overlays")]
    [SerializeField]
    private Canvas overlayCanvas;

    [SerializeField]
    private GameObject pauseOverlay;

    [SerializeField]
    private GameObject winOverlay;

    [SerializeField]
    private PlayerObjectList winOverlays;

    [SerializeField]
    private GameObject[] countdownObjects;

    [Header("Options")]
    [SerializeField]
    private bool isStartingWithRt;

    [field: SerializeField]
    public int TbRounds { get; private set; } = 7;

    [Header("Visuals")]
    [SerializeField]
    private float slideDuration = 1f;

    private int tbRound => tbTurn / (int)Player.Count;
    private int tbTurn;

    private Vector3 startPosition;
    private GameObject pausedGame;
    private GameObject currentGame;
    private bool isPaused;
    private float pauseTime = float.MinValue;

    private Player? winner;
    private float winTime = float.MinValue;

    private void Awake()
    {
        tbGame.OnEndTurn += onTbEndTurn;
        rtGame.OnRoundEnd += onRtRoundChange;

        tbGame.OnWin += onWin;
        rtGame.OnWin += onWin;
    }

    private void Start()
    {
        startPosition = overlayCanvas.transform.localPosition;

        currentGame = isStartingWithRt ? rtGame.gameObject : tbGame.gameObject;
        pausedGame = isStartingWithRt ? tbGame.gameObject : rtGame.gameObject;

        mainCamera.transform.position = currentGame.transform.position + Vector3.Scale(mainCamera.transform.position, Vector3.forward);

        pauseOverlay.SetActive(true);
        winOverlay.SetActive(false);
        foreach (var w in winOverlays)
            w.SetActive(false);

        isPaused = false;
        winner = null;

        disableHierarchy(pausedGame);
        StartCoroutine(goAnimation());
    }

    private void Update()
    {
        if (winner != null)
            DoWin();
        else if (isPaused)
            DoTransition();
    }

    private void DoWin()
    {
        var start = Vector3.Scale(startPosition, new Vector3(1, 1, 1));
        var middle = Vector3.Scale(startPosition, new Vector3(1, 0, 1));

        float elapsed = (Time.time - winTime) / slideDuration;
        float clamped = Mathf.Clamp01(elapsed);
        float fraction = 1 - Mathf.Pow(1 - clamped, 4);
        overlayCanvas.transform.localPosition = Vector3.Lerp(start, middle, fraction);

    }

    private void DoTransition()
    {
        var start = Vector3.Scale(startPosition, new Vector3(1, 1, 1));
        var middle = Vector3.Scale(startPosition, new Vector3(1, 0, 1));
        var end = Vector3.Scale(startPosition, new Vector3(1, -1, 1));

        float elapsed = (Time.time - pauseTime) / slideDuration;
        if (elapsed < 1)
        {
            float clamped = Mathf.Clamp01(elapsed);
            float fraction = 1 - Mathf.Pow(1 - clamped, 4);
            overlayCanvas.transform.localPosition = Vector3.Lerp(start, middle, fraction);
            return;
        }

        elapsed -= slideDuration;
        {
            var from = pausedGame.transform.position;
            var to = currentGame.transform.position;

            float clamped = Mathf.Clamp01(elapsed);
            float fraction = clamped < 0.5f ? Mathf.Pow(2 * clamped, 3) / 2 : 1 - Mathf.Pow(-2 * clamped + 2, 3) / 2;
            var pos = Vector3.Lerp(from, to, fraction);
            pos.z = mainCamera.transform.position.z;
            mainCamera.transform.position = pos;
        }

        elapsed -= slideDuration;
        {
            var fraction = Mathf.Pow(Mathf.Clamp01(elapsed), 4);
            overlayCanvas.transform.localPosition = Vector3.Lerp(middle, end, fraction);
        }

        elapsed -= slideDuration;
        if (elapsed > 0)
        {
            isPaused = false;
            enableHierarchy(currentGame);
        }
    }

    private IEnumerator goAnimation()
    {
        disableHierarchy(currentGame);

        foreach (var o in countdownObjects)
            o.SetActive(false);
        
        for (int i = 0; i < countdownObjects.Length; i++)
        {
            countdownObjects[i].SetActive(true);

            if (i > 0)
                countdownObjects[i - 1].SetActive(false);

            yield return new WaitForSeconds(1);
        }

        countdownObjects.Last().SetActive(false);
        enableHierarchy(currentGame);
    }

    private void enableHierarchy(GameObject gameObject)
    {
        foreach (var component in gameObject.GetComponentsInChildren<MonoBehaviour>())
        {
            if (component == null)
                continue;

            component.enabled = true;
        }

        foreach (var rb in gameObject.GetComponentsInChildren<Rigidbody2D>())
            rb.simulated = true;
    }

    private void disableHierarchy(GameObject gameObject)
    {
        foreach (var component in gameObject.GetComponentsInChildren<MonoBehaviour>())
        {
            if (component is null or INeverDisabled or ILayoutElement)
                continue;

            component.enabled = false;
        }

        foreach (var rb in gameObject.GetComponentsInChildren<Rigidbody2D>())
        {
            if (rb.isKinematic)
                continue;

            rb.simulated = false;
        }
    }

    private void OnDestroy()
    {
        tbGame.OnEndTurn -= onTbEndTurn;
        rtGame.OnRoundEnd -= onRtRoundChange;
    }

    private void onTbEndTurn()
    {
        tbTurn++;
        if (tbRound < TbRounds)
            return;

        tbTurn = 0;

        isPaused = true;
        pauseTime = Time.time;
        pausedGame = tbGame.gameObject;
        currentGame = rtGame.gameObject;

        disableHierarchy(pausedGame);
    }

    private void onRtRoundChange()
    {
        isPaused = true;
        pauseTime = Time.time;
        pausedGame = rtGame.gameObject;
        currentGame = tbGame.gameObject;

        disableHierarchy(pausedGame);
    }

    private void onWin(Player winner)
    {
        this.winner = winner;

        disableHierarchy(pausedGame);
        disableHierarchy(currentGame);

        pauseOverlay.SetActive(false);
        winOverlays[winner].SetActive(true);
        winOverlay.SetActive(true);
        winTime = Time.time;
    }
}
