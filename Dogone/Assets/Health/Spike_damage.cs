
using UnityEngine;

public class Spike_damage : MonoBehaviour
{
    private float canHit = 0f;
    [SerializeField] private float Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Time.time >= canHit)
            {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().TakeDamage(Damage);
                canHit = Time.time + 1f;
            }
        }
    }
}
