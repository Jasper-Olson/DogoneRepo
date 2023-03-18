using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public GameObject Player;
    public float Speed;
    public float TravelDistance;
    public float MinimumX;
    public float MaximumX;
    public float MinimumY;
    public float MaximumY;
    private Vector2 target;
    private Vector2 startpoint;
    public bool PlayerPresent;
    private float Timer;
    public float distance;
    public float distance2;

    void Start()
    {
        startpoint = transform.position;
        PlayerPresent = false;
        target = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        distance2 = Vector2.Distance(transform.position, target);
        distance = Player.transform.position.y - transform.position.y;
        if(distance < 0.24 & distance > 0.2f)
        {
            PlayerPresent = true;
        }
        else
        {
            PlayerPresent = false;
        }

        if(PlayerPresent == true & transform.position.x < MaximumX & transform.position.x > MinimumX & transform.position.y < MaximumY & transform.position.y > MinimumY)
        {
            if(distance2 < 0.1f)
            {
                target = new Vector2(transform.position.x + Random.Range(-1 * TravelDistance, TravelDistance), transform.position.y + Random.Range(-1 * TravelDistance, TravelDistance));
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
                Player.transform.position = Vector2.MoveTowards(Player.transform.position, new Vector2(target.x, target.y + 0.1324596f), Speed * Time.deltaTime);
            }
        }
        else if(PlayerPresent == true)
        {
            target = startpoint;
            transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, new Vector2(target.x, target.y + 0.1324596f), Speed * Time.deltaTime);
        }
    }
}
