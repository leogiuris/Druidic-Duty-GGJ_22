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
        presa.GetComponent<Animal>().Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Presa")
        {
            Hunt(collision.gameObject);
            gC.SpawnaAnimal(sPredador, transform.position);
        }
        if (collision.transform.tag == "Terreno")
        {
            flipper = flipper * -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Danger")
        {
            StartCoroutine(Stun());
        }
    }
    IEnumerator Stun()
    {
        speed = 0;
        chaseSpeed = 0;
        yield return new WaitForSeconds(stunTime);
        speed = tSpeed;
        chaseSpeed = tChaseSpeed;
    }
    
}
