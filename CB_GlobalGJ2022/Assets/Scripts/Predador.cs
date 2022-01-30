using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predador : Animal
{
    public GameObject sPredador;
    public float stunTime;
    void Hunt(GameObject presa)
    {
        this.hunger--;
        anim.SetTrigger("comer");
        presa.GetComponent<Animal>().Die();
        speed = 0;
        chaseSpeed = 0;

    }
    public void Matar()
    {
        speed = tSpeed;
        chaseSpeed = tChaseSpeed;
        gC.SpawnaAnimal(sPredador, transform.position);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Presa")
        {
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Predador")
        {
            RandomizeDirection();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Danger")
        {
            if(collision.gameObject.GetComponentInParent<Transform>().tag != "Predador")
            {
                StartCoroutine(Stun());
            }
            
        }
    }
    IEnumerator Stun()
    {
        print("ui");
        speed = 0;
        chaseSpeed = 0;
        yield return new WaitForSeconds(stunTime);
        speed = tSpeed;
        chaseSpeed = tChaseSpeed;
    }
    
}
