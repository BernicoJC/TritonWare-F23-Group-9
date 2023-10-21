using UnityEngine;

public class InfoSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject leftInfo;
    public GameObject rightInfo;
    public GameObject controlInfo;
    public static int switchinfo = 1;
    public AudioSource buttonNoise;

    public void changetheInfo()
    {
        if(switchinfo % 2 == 1)
        {
            leftInfo.SetActive(true);
            rightInfo.SetActive(true);
            controlInfo.SetActive(false);
            buttonNoise.Play();
        }
        else if(switchinfo % 2 == 0)
        {
            leftInfo.SetActive(false);
            rightInfo.SetActive(false);
            controlInfo.SetActive(true);
            buttonNoise.Play();
        }
        switchinfo++;
    }
}
