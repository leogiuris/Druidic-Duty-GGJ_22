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

    private MouseController mouse;
    //animação
    private Animator anim;



    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        gC = GameObject.Find("gameController").GetComponent<GameController>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        mouse = GetComponent<MouseController>();

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
            Plantar(transform.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Plantar(mouse.getMousePos());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("boom");
        }

    }

    private void Movimento()
    {
        norm = new Vector2(xDir, yDir);
        rBody.velocity = norm.normalized  * speed;
    }




    private void Plantar(Vector3 t)
    {
        t.z = 0;


        if(sementes > 0 && !mouse.GetComponent<MouseController>().emArvore && gC.ChecaSeTaDentro(t))
        {
            GameObject novaPlanta = Instantiate(planta, t, transform.rotation);
            gC.plantas.Add(novaPlanta);
            sementes--;
        }
        else
        {
            Debug.Log("não pode plantar");
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == ("arvore"))
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
