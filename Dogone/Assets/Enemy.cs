using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public Animator animator;
public GameObject ExtraHeart;
public bool HeartGiven;
public int maxHealth = 50;
public bool LeaveCoins;
public GameObject Coin1, Coin2, Coin3;
int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ExtraHeart.GetComponent<FollowPlayer>().enabled = false;
        ExtraHeart.GetComponent<Collider2D>().enabled = false;
        ExtraHeart.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        if(animator.GetBool("Invincible"))
        {

        }

        else
        {
        currentHealth -= damage;
        }

        if(currentHealth <= 0)
        {
            Debug.Log("Enemy died");
            animator.SetBool("IsDead", true);
            Invoke("Die", 2);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void Die()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if(HeartGiven == true)
        {
            ExtraHeart.GetComponent<SpriteRenderer>().enabled = true;
            ExtraHeart.GetComponent<Collider2D>().enabled = true;
            ExtraHeart.GetComponent<FollowPlayer>().enabled = true;
            ExtraHeart.transform.position = this.transform.position;
        }
        if(LeaveCoins == true)
        {
            Coin1.GetComponent<SpriteRenderer>().enabled = true;
            Coin2.GetComponent<SpriteRenderer>().enabled = true;
            Coin3.GetComponent<SpriteRenderer>().enabled = true;
            Coin1.GetComponent<Collider2D>().enabled = true;
            Coin2.GetComponent<Collider2D>().enabled = true;
            Coin3.GetComponent<Collider2D>().enabled = true;
        }
        this.enabled = false;
    }

}