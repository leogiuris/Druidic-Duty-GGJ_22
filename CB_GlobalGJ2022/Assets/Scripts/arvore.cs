using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arvore : MonoBehaviour
{
    public PlayerController player;
    public MouseController mouse;
    private bool mousehere = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        mouse = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseController>();
    }

    private void OnDestroy()
    {
        if (mousehere)
        {
            mouse.emArvore = false;
        }
    }

    private void OnMouseOver()
    {
        mouse.emArvore = true;
        mousehere = true;
    }
    private void OnMouseExit()
    {
        mouse.emArvore = false;
        mousehere = false;
    }

}
