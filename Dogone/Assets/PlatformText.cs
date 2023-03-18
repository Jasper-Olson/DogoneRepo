using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformText : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Platform;
    public TextMesh text;
    private bool trigger;

    void Start()
    {
        trigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Platform.GetComponent<RandomMove>().PlayerPresent == true & trigger == true)
        {
            Invoke("WriteText1", 10);
            trigger = false;
        }
    }

    void WriteText1()
    {
        text.text = "You'll get there \n someday";
        Invoke("WriteText2", 10);
    }

    void WriteText2()
    {
        text.text = "Current ETA: \n August";
        Invoke("WriteText3", 10);
    }

    void WriteText3()
    {
        text.text = "Did I mention \n it moves randomly";
        Invoke("WriteText4", 7);
    }

    void WriteText4()
    {
        text.text = "Anyway";
        Invoke("WriteText5", 3);
    }

    void WriteText5()
    {
        text.text = "Best of luck";
        Invoke("WriteText6", 15);
    }

    void WriteText6()
    {
        text.text = " ";
    }
}
