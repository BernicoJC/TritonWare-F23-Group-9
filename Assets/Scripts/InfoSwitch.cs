using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSwitch : MonoBehaviour
{
    [SerializeField]
    public GameObject leftInfo;
    public GameObject rightInfo;
    public GameObject controlInfo;
    //public GameObject startButton;




    public static int switchinfo = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changetheInfo()
    {
        print(switchinfo);

        if(switchinfo % 2 == 1)
        {
            leftInfo.SetActive(true);
            rightInfo.SetActive(true);
            controlInfo.SetActive(false);
            //startButton.SetActive(true);
        }
        else if(switchinfo % 2 == 0)
        {
            leftInfo.SetActive(false);
            rightInfo.SetActive(false);
            controlInfo.SetActive(true);
            //startButton.SetActive(false);
        }
        switchinfo++;
        print(switchinfo);
    }
}
