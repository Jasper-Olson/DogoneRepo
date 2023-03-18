using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRefresh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if(player.tag == "Player")
        {
            if(player.GetComponent<Health>().CurrentHealth < player.GetComponent<Health>().StartingHealth)
            {
                player.GetComponent<Health>().CurrentHealth = player.GetComponent<Health>().StartingHealth;
            }
        }
    }
}
