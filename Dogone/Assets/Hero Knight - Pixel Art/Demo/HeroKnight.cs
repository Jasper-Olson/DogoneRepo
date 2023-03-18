using UnityEngine;
using System.Collections;
using System;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    public Transform attackPoint;
    public Animator            m_animator;
    public float attackRange = 0.51f;
    public float attackRate = 1f;
    public LayerMask enemyLayers;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private float               m_delayToIdle = 0.0f;
    public CoinManager CM;
    public int attackDamage = 50;
    float nextAttack = 0f;
    public float rollRate = 1f;
    Vector3 spawnPosition;
    float nextRoll = 0f;
    public Animator EnemyAnimator;
    public CapsuleCollider2D Collider;
    public float collectRate;

    // Use this for initialization
   
    
    
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that checks roll duration


        // Disable rolling if timer extends duration

        //Check if character just landed on the ground


        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        if(!m_grounded && m_body2d.velocity == new Vector2(0f,0f))
        {
            inputX = 0f;
        }

        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

       
      
       

        //Attacking
        if(Input.GetMouseButtonDown(0))
        {

            if(Time.time >= nextAttack)
            {
                Attacking();
                nextAttack = Time.time + 1f / attackRate;
            }
            //new stuff here
            
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
        {   
            m_animator.SetBool("IdleBlock", false);
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && m_grounded && m_body2d.velocity.x > 0f)
        {
            if(Time.time >= nextRoll)    
            {
                m_animator.SetTrigger("Roll");
                m_rolling = true;
                Collider.size = new Vector2(0.7869025f, 1.317964f / 2.5f);
                Collider.offset = new Vector2(-0.01399994f, 0.433f);
                m_body2d.velocity = new Vector2(m_rollForce * m_facingDirection, m_body2d.velocity.y);
                nextRoll = Time.time + 1 / rollRate;
            }
        }
            
        else if (Input.GetKeyDown("left shift") && m_grounded && m_body2d.velocity.x < 0f)
        {
            if(Time.time >= nextRoll)    
            {
                m_animator.SetTrigger("Roll");
                m_rolling = true;
                Collider.size = new Vector2(0.7869025f, 1.317964f / 2.5f);
                Collider.offset = new Vector2(-0.01399994f, 0.433f);
                m_body2d.velocity = new Vector2(m_rollForce * m_facingDirection, m_body2d.velocity.y);
                nextRoll = Time.time + 1 / rollRate;
            }
        }

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if(m_delayToIdle < 0)
            {
                m_animator.SetInteger("AnimState", 0);
            }
        }


        if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            m_rolling = true;
        }

        if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            m_rolling = false;
            Collider.size = new Vector2(0.7869025f, 1.317964f);
            Collider.offset = new Vector2(-0.01399994f, 0.6980022f);
        }

    }

    void Attacking()
    {
        if(EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Start-up"))
        {
            m_animator.SetTrigger("Attack1");
        }
        
        else if(EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Up"))
        {
            m_animator.SetTrigger("Attack1");
        }

        else if(EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump To Fall"))
        {
            m_animator.SetTrigger("Attack1");
        }

        else
        {

            m_animator.SetTrigger("Attack1");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        
    }
    private void OnAnimatorMove()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            if(Time.time >= collectRate)
            {
                Destroy(other.gameObject);
                CM.CoinCount++;
                collectRate = Time.time + 0.1f;
            }
        }

        else if (other.gameObject.CompareTag("Coin2"))
        {
            if(Time.time >= collectRate)
            {
                if(Time.time >= collectRate)
                {
                    Destroy(other.gameObject);
                    CM.CoinCount2++;
                    collectRate = Time.time + 0.1f;
                }
            }
        }

        else if (other.gameObject.CompareTag("Coin3"))
        {
            if(Time.time >= collectRate)
            {
                if(Time.time >= collectRate)
                {
                    Destroy(other.gameObject);
                    CM.CoinCount3++;
                    collectRate = Time.time + 0.1f;
                }
            }
        }

        else if (other.gameObject.CompareTag("Hearts"))
        {
            if(Time.time >= collectRate)
            {
                Destroy(other.gameObject);
                GetComponent<Health>().CurrentHealth = GetComponent<Health>().CurrentHealth + other.GetComponent<FollowPlayer>().BonusHealth;
                collectRate = Time.time + 0.1f;
            }
        }


    }
}



