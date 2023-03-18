using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public float movespeed;
    public float Gravity;
    public CircleCollider2D Collider;
    private float timer;
    private Vector3 direction;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.x - transform.position.x > -7f & Player.transform.position.x - transform.position.x < 7f)
        {
            if(Player.transform.position.x - transform.position.x > 0f)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + Time.time * movespeed * -10f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(movespeed / 2, Gravity * -1);
            }
            else if(Player.transform.position.x - transform.position.x < 0f)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + Time.time * movespeed * 10f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(movespeed / -2, Gravity * -1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

        }
        else
        {
            if(collision.GetComponent<Collider2D>().isTrigger == false)
            {
                Gravity = 0f;
            }
        }
    }
}
