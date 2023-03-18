using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedChase : MonoBehaviour
{
    public GameObject player;
    public GameObject playerpos;
    public Animator animator;
    public float StartTrigger;
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
    public float StartDelay;

    void Start()
    {
        animator.SetBool("PlayerClose", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x <= maxdistance && player.transform.position.x >= mindistance)
        {
            Invoke("Begin", StartDelay);
        }
        else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Up - Animation"))
            {
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump To Fall - Animation"))
            {
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Down - Animation"))
            {
            }
            else
            {
                animator.SetBool("PlayerClose", false);
                player.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    void attack()
    {
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach(Collider2D playerEnemy in hitplayer)
        {
            playerEnemy.GetComponent<Health>().TakeDamage(Damage);
            return;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        if (attackPoint == null)
        return;
    }

    void Begin()
    {

        distance = Vector2.Distance(this.transform.position, playerpos.transform.position);
        if(playerpos.transform.position.x - this.transform.position.x > 0)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Up - Animation"))
            {
           
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump To Fall - Animation"))
            {
          
            }

            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        else if(playerpos.transform.position.x - this.transform.position.x < 0)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Up - Animation"))
            {
            
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump To Fall - Animation"))
            {
            
            }

            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }


        if(player.transform.position.x - transform.position.x < StartTrigger)
        {
            animator.SetBool("PlayerClose", true);
        }
        else if(player.transform.position.x - transform.position.x >= StartTrigger)
        {
            animator.SetBool("PlayerClose", false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Start-up - Animation"))
        {
            animator.SetBool("Invincible", true);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Land - Animation"))
        {
            animator.SetBool("Invincible", false);
            if(Time.time >= nextAttack)
            {
                attack();
                nextAttack = Time.time + 1f * attackRate;
            }
        }
        
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump Up - Animation"))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerpos.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            Collider2D[] Moveplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach(Collider2D playerEnemy in Moveplayer)
            {
                playerEnemy.transform.position = Vector2.MoveTowards(playerEnemy.transform.position, new Vector2(playerpos.transform.position.x, playerEnemy.transform.position.y), speed * Time.deltaTime);
                playerEnemy.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Red Jump To Fall - Animation"))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(playerpos.transform.position.x, this.transform.position.y), speed * Time.deltaTime);
            Collider2D[] Moveplayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            foreach(Collider2D playerEnemy in Moveplayer)
            {
                playerEnemy.transform.position = Vector2.MoveTowards(playerEnemy.transform.position, new Vector2(playerpos.transform.position.x, playerEnemy.transform.position.y), speed * Time.deltaTime);
            }
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