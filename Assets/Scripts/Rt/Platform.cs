using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : OwnedObject
{
    // Start is called before the first frame update
    private BoxCollider2D bc;
    [SerializeField]

    string suffix;
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {


        if (Input.GetButtonDown("AxisY" + suffix) && Input.GetAxisRaw("AxisY" + suffix) < 0)
        {
                //print("GotInTag");
            bc.enabled = false;
            StartCoroutine(EnableCollider());



        }
    }
    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.2f);
        bc.enabled = true;
    }
}
