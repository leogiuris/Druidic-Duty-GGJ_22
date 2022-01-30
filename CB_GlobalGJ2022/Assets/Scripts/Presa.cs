using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : Animal
{
    public GameObject sPresa;
    private GameObject minhaPlanta;
    bool isFleeing;

    public void EatVeg(GameObject planta)
    {
        anim.SetTrigger("comer");
        hunger = 0;
        minhaPlanta = planta;
        speed = 0;
        chaseSpeed = 0;
;        
    }
    public void MataPlanta()
    {
        emoter.SetTrigger("none");
        speed = tSpeed;
        chaseSpeed = tChaseSpeed;
        Breed(sPresa);
        
        gC.plantas.Remove(minhaPlanta);
        Destroy(minhaPlanta);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Terreno")
        {
            flipper = flipper * -1;
        }
        if (collision.transform.tag == "Presa")
        {
            RandomizeDirection();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Presa")
        {
            RandomizeDirection();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "arvore")
        {
            
        }
        if (collision.transform.tag == "Danger")
        {
            flee = true;
            danger = collision.gameObject;
        }
        if (collision.transform.tag == "Boom")
        {
            flee = true;
            danger = collision.gameObject;
        }
    }
}
