using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public GameObject player;
    public GameObject playerpos;
    public Animator animator;
    public float speed;
    private float distance;
    [SerializeField] private float Damage;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public float attackRate = 1f;
    private float nextAttack = 0f;
    public float maxdistance;
    public float mindistance;

    void Start()
    {
        animator.SetBool("PlayerClose", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerpos.transform.position.x <= maxdistance && playerpos.transform.position.x >= mindistance)
        {

            distance = Vector2.Distance(this.transform.position, playerpos.transform.position);
            if(playerpos.transform.position.x - this.transform.position.x > 0)
            {
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Up"))
                {
               
                }
                else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump To Fall"))
                {
              
                }

                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }

            else if(playerpos.transform.position.x - this.transform.position.x < 0)
            {
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Up"))
                {
                
                }
                else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump To Fall"))
                {
                
                }

                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }


            if(distance < 5)
            {
                animator.SetBool("PlayerClose", true);
            }
            else if(distance >= 5)
            {
                animator.SetBool("PlayerClose", false);
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Start-up"))
            {
                animator.SetBool("Invincible", true);
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Land"))
            {
                animator.SetBool("Invincible", false);
                if(Time.time >= nextAttack)
                {
                    attack();
                    nextAttack = Time.time + 1f * attackRate;
                }
            }
        
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Up"))
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }
        
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump To Fall"))
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            }

            if(animator.GetBool("IsDead"))
            {
                this.enabled = false;
            }

            else
            {
                return;
            }

        }

    }

    void attack()
    {
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach(Collider2D playerEnemy in hitplayer)
        {
            playerEnemy.GetComponent<Health>().TakeDamage2(Damage);
            return;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        if (attackPoint == null)
        return;
    }
}