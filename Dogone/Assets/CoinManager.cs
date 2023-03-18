using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public float CoinCount;
    public float CoinCount2;
    public float CoinCount3;
    public Text CoinText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CoinText.GetComponent<CoinColorChange>().blue == true)
        {
            CoinText.text = CoinCount3.ToString();
        }

        else if(CoinText.GetComponent<CoinColorChange>().purple == true)
        {
            CoinText.text = CoinCount2.ToString();
        }

        else
        {
            CoinText.text = CoinCount.ToString();
        }
    }
}
