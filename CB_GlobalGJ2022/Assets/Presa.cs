using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : Animal
{

    bool isFleeing;

    void EatVeg(GameObject planta)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Arvore")
        {
            EatVeg(collision.gameObject);
        }
        if (collision.transform.tag == "Terreno")
        {
            flipper = flipper * -1;
        }
    }

}
