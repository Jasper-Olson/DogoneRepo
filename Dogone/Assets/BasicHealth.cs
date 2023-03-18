using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealth : MonoBehaviour
{    
    //health
    public int Maxhealth = 5;
    public int CurrentHealth;
    public GameObject CannoWhite;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = Maxhealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CannoWhite.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("TurnOff", 0.1f);
        if(CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void TurnOff()
    {
        CannoWhite.GetComponent<SpriteRenderer>().enabled = false;
    }
}
