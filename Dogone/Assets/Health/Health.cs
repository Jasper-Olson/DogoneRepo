
using UnityEngine;

public class Health : MonoBehaviour
{
    public float StartingHealth;
    public float CurrentHealth;
    public Animator anim;
    public bool death;
    public ButtonScript Button;
    private bool isDead;
    public Animator animator;

    private void Awake()
    {
        CurrentHealth = StartingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage2(float Damage)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {

        }

        else
        {
            CurrentHealth = CurrentHealth - Damage;
            
            if(Damage > 0)
            {
                if (CurrentHealth > 0)
                {
                    anim.Play("Hurt");
                }
            

                else
                {
                    if (!death && !isDead)
                    {
                        isDead = true;
                        Button.gameOver();
                        anim.Play("Death");
                        GetComponent<HeroKnight>().enabled = false;
                        death = true;
                    }
                }
            }
        }
    }

    public void TakeDamage(float Damage)
    {

        CurrentHealth = CurrentHealth - Damage;
        if(Damage > 0)
        {
            if (CurrentHealth > 0)
            {
                anim.Play("Hurt");
            }
            else
            {
                if (!death && !isDead)
                {
                    isDead = true;
                    Button.gameOver();
                    anim.Play("Death");
                    GetComponent<HeroKnight>().enabled = false;
                    death = true;
                }
            }
        }
    }
}