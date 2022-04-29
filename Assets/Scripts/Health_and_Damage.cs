using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_and_Damage : MonoBehaviour
{
    public int health = 100;
    public bool invencible = false;
    public float invencibleTime = 1.5f;
    public float stopTime = 0.5f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Hurt(int amount)
    {
        if (!invencible && health > 0)
        {
            health -= amount;
            anim.Play("Get Damage");
            StartCoroutine(Invencibility());
            StartCoroutine(StopSpeed());

            if (health == 0)
            {
                
            }
        }
        
    }


    public void Die()
    {
        Debug.Log("Game Over");
        
    }
    IEnumerator Invencibility()
    {
        invencible = true;
        yield return new WaitForSeconds(invencibleTime);
        invencible = false;
    }

    IEnumerator StopSpeed()
    {
        var actualSpeed = GetComponent<Player_Movement>().movementSpeed;
        GetComponent<Player_Movement>().movementSpeed = 0;
        yield return new WaitForSeconds(stopTime);
        GetComponent<Player_Movement>().movementSpeed = actualSpeed;

    }
}


