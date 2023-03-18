using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject BulletPos;
    public float triggerlengthX;
    public float triggerlengthY;
    public float FiringTime;
    private bool CanFire;
    private bool CanFire2;
    private float distance;
    private float distanceY;
    public float StartDelay;
    void Start()
    {
        Bullet1.transform.position = BulletPos.transform.position;
        Bullet2.transform.position = BulletPos.transform.position;
        CanFire = true;
        CanFire2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Player.transform.position.x - this.transform.position.x;
        distanceY = Player.transform.position.y - this.transform.position.y;
        if(distance < 0f)
        {
            distance = distance * -1;
        }
        if(distanceY < 0f)
        {
            distanceY = distanceY * -1;
        }

        if(distance > -1 * triggerlengthX & distance < triggerlengthX & distanceY > -1 * triggerlengthY & distanceY < triggerlengthY)
        {   
            if(CanFire == true)
            {
                Bullet1.transform.position = BulletPos.transform.position;
                Bullet2.transform.position = BulletPos.transform.position;
                CanFire2 = true;
                CanFire = false;
                Invoke("Bullet1Fire", StartDelay);
            }
        }
        else
        {
            CanFire = true;
            CanFire2 = false;
        }
    }

    void Bullet1Fire()
    {
        if(CanFire2 == true)
        {
            
            if (distance > -1 * triggerlengthX && distance < triggerlengthX && distanceY > -1 * triggerlengthY && distanceY < triggerlengthY)
            {
                Bullet1.GetComponent<BulletFollow>().trigger3 = false;
                Bullet1.GetComponent<BulletFollow>().trigger2 = false;
                Bullet1.GetComponent<BulletFollow>().trigger = false;
                Bullet1.GetComponent<SpriteRenderer>().enabled = true;
                Bullet1.GetComponent<Collider2D>().enabled = true;
                Bullet1.GetComponent<BulletFollow>().enabled = true;
                Invoke("Bullet2Fire", FiringTime);
            }
        }
    }

    void Bullet2Fire()
    {
        distance = Player.transform.position.x - this.transform.position.x;
        if (CanFire2 == true & distance > -1 * triggerlengthX && distance < triggerlengthX & distanceY > -1 * triggerlengthY && distanceY < triggerlengthY)
        {
            Bullet2.GetComponent<BulletFollow>().trigger3 = false;
            Bullet2.GetComponent<BulletFollow>().trigger2 = false;
            Bullet2.GetComponent<BulletFollow>().trigger = false;
            Bullet2.GetComponent<BulletFollow>().trigger = false;
            Bullet2.GetComponent<SpriteRenderer>().enabled = true;
            Bullet2.GetComponent<Collider2D>().enabled = true;
            Bullet2.GetComponent<BulletFollow>().enabled = true;
            Invoke("BulletReload", FiringTime * 1.5f);
        }
    }

    void BulletReload()
    {
        Bullet1.GetComponent<SpriteRenderer>().enabled = false;
        Bullet1.GetComponent<Collider2D>().enabled = false;
        Bullet1.GetComponent<BulletFollow>().enabled = false;
        Bullet1.transform.position = BulletPos.transform.position;
        Bullet2.GetComponent<SpriteRenderer>().enabled = false;
        Bullet2.GetComponent<Collider2D>().enabled = false;
        Bullet2.GetComponent<BulletFollow>().enabled = false;
        Bullet2.transform.position = BulletPos.transform.position;
        Bullet1Fire();
    }
}
