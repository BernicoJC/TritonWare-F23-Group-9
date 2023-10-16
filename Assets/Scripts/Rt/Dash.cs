using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    public string suffix;

    [SerializeField]
    public Sprite newSprite;

    [SerializeField]
    public Sprite oldSprite;

    [SerializeField]
    private float DashForce = 1500f;

    private SpriteRenderer spriteR;
    private bool isDashing;
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dash" + suffix) && !isDashing)
        {
            StartCoroutine(DashAnimation());
        }
    }

    private IEnumerator DashAnimation()
    {
        isDashing = true;
        float dir = transform.eulerAngles.y < 90 ? 1 : -1;

        changeSprite(newSprite);
        m_Rigidbody2D.AddForce(new Vector2(dir * DashForce, 0f));
        
        yield return new WaitForSeconds(0.2f);
        changeSprite(oldSprite);
        
        isDashing = false;
    }

    private void changeSprite(Sprite toSprite)
    {
        spriteR.sprite = toSprite;
    }
}
