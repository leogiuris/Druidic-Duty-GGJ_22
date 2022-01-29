using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movimento
    private Rigidbody2D rBody;
    private float yDir ;
    private float xDir ;
    public float speed;
    private Vector2 norm;

    //interação
    public GameController gC;
    private bool plantar;

    public bool emArvore = false;
    public int sementes;
    public GameObject planta;

    //mouse
    public float castDistance;

    void Start()
    {
        gC = GameObject.Find("gameController").GetComponent<GameController>();
        rBody = gameObject.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
    }
    void FixedUpdate()
    {
        Movimento();
    }
    private void InputManager()
    {
        yDir = Input.GetAxisRaw("Vertical");
        xDir = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Plantar();
        }

    }
    private void Movimento()
    {
        norm = new Vector2(xDir, yDir);
        rBody.velocity = norm.normalized  * speed;
    }
    private void Plantar()
    {
        if(sementes > 0 && ! emArvore)
        {
            GameObject novaPlanta = Instantiate(planta, transform.position, transform.rotation);
            gC.plantas.Add(novaPlanta);
            sementes--;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag ==("arvore"))
        {
            emArvore = true;
        }
        else
        {
            emArvore = false;
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ("arvore"))
        {
            emArvore = false;
        }
    }
}
