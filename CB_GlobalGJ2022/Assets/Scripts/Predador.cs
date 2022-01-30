using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predador : Animal
{
    public GameObject sPredador;
    public float stunTime;
    void Hunt(GameObject presa)
    {
        this.hunger = 0;
        anim.SetTrigger("comer");
        presa.GetComponent<Animal>().Die();
        speed = 0;
        chaseSpeed = 0;

    }
    public void Matar()
    {
        emoter.SetTrigger("none");
        speed = tSpeed;
        chaseSpeed = tChaseSpeed;
        Breed(sPredador);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Presa")
        {
            if (child) return;
            Hunt(collision.gameObject);
        }
        if (collision.transform.tag == "Terreno")
        {
            flipper = flipper * -1;
        }
        if (collision.transform.tag == "Predador")
        {
            RandomizeDirection();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Boom")
        {
            Debug.Log("uga");
            StartCoroutine(Stun());
        }
    }
    IEnumerator Stun()
    {
        emoter.SetTrigger("medo");
        print("ui");
        speed = 0;
        chaseSpeed = 0;
        yield return new WaitForSeconds(stunTime);
        speed = tSpeed;
        chaseSpeed = tChaseSpeed;
        emoter.SetTrigger("none");
    }

    private void Update()
    {
        if (!isDead)
        {
            Eat();
            if (child)
            {
                if (Timer.getTime() - age > 10f)
                {
                    Grow();
                }
            }
        }
        
    }
}
