using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predador : Animal
{

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
        }
        if (collision.transform.tag == "Terreno")
        {
            flipper = flipper * -1;
        }
    }
}
