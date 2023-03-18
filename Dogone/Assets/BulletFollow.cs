using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Cannon;
    public GameObject BaseBullet;
    public Animator PlayerAnim;
    public float MoveSpeed;
    public float damage;
    public int EnemyDamage;
    public string AnimationName;
    public string AnimationName2;
    Vector3 direction;
    public bool trigger;
    public bool trigger2;
    public bool trigger3;
    private float timer = 0f;
    private float timer2 = 0f;
    public float random;
    void Start()
    {
        trigger = false;
        trigger2 = false;
        trigger3 = false;
        GetComponent<AudioSource>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   

        if(trigger == false)
        {
            random = Random.Range(0f, 1f);

            random = BaseBullet.GetComponent<BulletFollow>().random;

            if(random <= 0.5f)
            {
                GetComponent<SpriteRenderer>().color = new Color (0, 255, 243, 255);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color (255, 0, 0, 255);
            }

            Invoke("VectorCalc", 0.1f);
            trigger = true;
        }

        if(trigger2 == true)
        {
            transform.position += direction * Time.deltaTime * MoveSpeed;
        }

        if(trigger3 == true)
        {
            trigger2 = false;
            transform.position -= direction * Time.deltaTime * MoveSpeed;
            GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            Invoke("EndSound", 0.1f);
        }
    }
    void VectorCalc()
    {
        var Ydistance = Player.transform.position.y - transform.position.y;
        var Xdistance = Player.transform.position.x - transform.position.x;
        var Zdistance = Player.transform.position.z - transform.position.z;
        direction = (new Vector3 (Player.transform.position.x + Xdistance * 10, Player.transform.position.y + Ydistance * 10, Player.transform.position.z + Zdistance * 10) - this.transform.position).normalized;
        trigger2 = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Shooter")
        {
            if(Time.time >= timer2)
            {
                collision.GetComponent<BasicHealth>().TakeDamage(EnemyDamage);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                timer2 = Time.time + 0.05f;
            }
        }

        if(collision.tag == "Player")
        {
            if(random <= 0.49f)
            {
                if(Player.transform.position.x - Cannon.transform.position.x < 0f & collision.GetComponent<SpriteRenderer>().flipX == false && PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
                {
                    trigger = true;
                    trigger2 = false;
                    trigger3 = true;
                    GetComponent<Collider2D>().enabled = true;
                }

                else if(Player.transform.position.x - Cannon.transform.position.x > 0f & collision.GetComponent<SpriteRenderer>().flipX == true && PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName))
                {
                    trigger = true;
                    trigger2 = false;
                    trigger3 = true;
                    GetComponent<Collider2D>().enabled = true;
                }

                else if(Time.time >= timer)
                {
                    collision.GetComponent<Health>().TakeDamage(damage);
                    this.GetComponent<SpriteRenderer>().enabled = false;
                    this.GetComponent<Collider2D>().enabled = false;
                    timer = Time.time + 0.05f;
                }
            }

            else
            {
                if(Player.transform.position.x - Cannon.transform.position.x < 0f & collision.GetComponent<SpriteRenderer>().flipX == false && PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName2))
                {
                    trigger = true;
                    trigger2 = false;
                    trigger3 = true;
                    GetComponent<Collider2D>().enabled = true;
                }

                else if(Player.transform.position.x - Cannon.transform.position.x > 0f & collision.GetComponent<SpriteRenderer>().flipX == true && PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName2))
                {
                    trigger = true;
                    trigger2 = false;
                    trigger3 = true;
                    GetComponent<Collider2D>().enabled = true;
                }

                else if(Time.time >= timer)
                {
                    collision.GetComponent<Health>().TakeDamage(damage);
                    this.GetComponent<SpriteRenderer>().enabled = false;
                    this.GetComponent<Collider2D>().enabled = false;
                    timer = Time.time + 0.05f;
                }
            }

        }
    }

    void EndSound()
    {
        GetComponent<AudioSource>().enabled = false;
    }

}
