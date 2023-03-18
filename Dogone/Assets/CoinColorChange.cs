using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinColorChange : MonoBehaviour
{
    public Text Cointext;
    public bool yellow;
    public bool purple;
    public bool blue;
    public Color CurrentColor;
    // Start is called before the first frame update
    void Start()
    {
        CurrentColor = Cointext.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(blue == true)
        {
            Cointext.color = Color.blue;
        }
        else if(purple == true)
        {
            Cointext.color = Color.magenta;
        }
        else
        {
            Cointext.color = CurrentColor;
        }
    }
}
