using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presa : Animal
{
    public GameObject sPresa;
    bool isFleeing;

    void EatVeg(GameObject planta)
    {
        hunger--;
        Destroy(planta);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Terreno")
        {
            flipper = flipper * -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "arvore")
        {
            EatVeg(collision.gameObject);
            gC.SpawnaAnimal(sPresa, transform.position);
        }
    }

    }
