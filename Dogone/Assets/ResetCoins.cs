using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetCoins : MonoBehaviour
{
    public GameObject CoinManager;
    private float CoinCount;
    private float CoinCount2;
    public bool checkpoint1;
    public bool checkpoint2;
    public GameObject CoinText;
    public bool blue, purple;

    // Start is called before the first frame update
    void Start()
    {
        CoinCount = CoinManager.GetComponent<CoinManager>().CoinCount;
        CoinCount2 = CoinManager.GetComponent<CoinManager>().CoinCount2;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if(player.gameObject.CompareTag("Player"))
        {   
            if(CoinCount >= 10f)
            {
                checkpoint1 = true;
            }
            if(CoinCount2 >= 10f)
            {
                checkpoint2 = true;
            }
            if(purple == true)
            {
                CoinText.GetComponent<CoinColorChange>().purple = true;
            }
            if(blue == true)
            {
                CoinText.GetComponent<CoinColorChange>().blue = true;
            }
            CoinManager.GetComponent<CoinManager>().CoinCount = 0f;
        }
    }
}
