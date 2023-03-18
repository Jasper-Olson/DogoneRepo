using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_change_damage : MonoBehaviour
{
    // Start is called before the first frame update
    public float ColorChangeRate;
    private Color Color2;

    void Start()
    {
        //start with red
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Color2 = GetComponent<SpriteRenderer>().color;
        if(Color2 == Color.green)
        {
            Invoke("Red", ColorChangeRate);
        }

        else if(Color2 == Color.red)
        {
            Invoke("Green", ColorChangeRate);
        }
    }

    void Red()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void Green()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<Collider2D>().enabled = false;
    }   
}