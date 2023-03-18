using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockedArea : MonoBehaviour
{
    public GameObject Trigger;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Collider2D>())
        {
            GetComponent<Collider2D>().enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Trigger.GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if(GetComponent<Collider2D>())
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
