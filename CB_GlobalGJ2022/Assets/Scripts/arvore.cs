using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arvore : MonoBehaviour
{
    public PlayerController player;
    public MouseController mouse;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        mouse = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseController>();
    }

    private void OnMouseOver()
    {
        mouse.emArvore = true;
    }

}
