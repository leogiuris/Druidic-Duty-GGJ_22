using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //movimento
    private Rigidbody2D rBody;
    private float dir;
    private float yDir;
    private float xDir;
    public float speed;
    private Vector2 norm;

    //interação
    public GameController gC;
    private bool plantar;

    public bool emArvore = false;
    public int maxSementes;
    public int sementes;
    public GameObject planta;
    public float coolDown;
    public float plantTimer;

    //mouse
    public float castDistance;

    private MouseController mouse;
    //animação
    private Animator anim;

    //paredes
    public int maxParedes;
    public int paredes;
    public GameObject parede;



    void Start()
    {
        plantTimer = Time.time;
        paredes = maxParedes;
        anim = gameObject.GetComponent<Animator>();
        gC = GameObject.Find("gameController").GetComponent<GameController>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        mouse = GetComponent<MouseController>();
        dir = 1f;
    }



    // Update is called once per frame
    void Update()
    {
        InputManager();
        if (paredes > maxParedes) paredes = maxParedes;
        if(Time.time - plantTimer > coolDown)
        {
            sementes++;
            plantTimer = Time.time;
        }
    }


    void FixedUpdate()
    {
        Movimento();
    }

    private void InputManager()
    {
        if (!gC.isPlaying) return;

        yDir = Input.GetAxisRaw("Vertical");
        xDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Plantar(transform.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Plantar(mouse.getMousePos());
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("boom");
        }
        if (Input.GetMouseButton(1))
        {
            CriarParede(mouse.getMousePos());
        }

    }

    private void Movimento()
    {
        norm = new Vector2(xDir, yDir);
        rBody.velocity = norm.normalized * speed;
        if(xDir != 0)
        {
            dir = xDir;
        }
        transform.localScale = new Vector3(dir, 1, 1);
        if(rBody.velocity != Vector2.zero)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }




    private void Plantar(Vector3 t)
    {
        t.z = 0;


        if (sementes > 0 && !mouse.GetComponent<MouseController>().emArvore && gC.ChecaSeTaDentro(t))
        {
            sementes--;
            GameObject novaPlanta = Instantiate(planta, t, transform.rotation);
            gC.plantas.Add(novaPlanta);
            
        }
        else
        {
            Debug.Log("não pode plantar");
        }
    }
    private void CriarParede(Vector3 t)
    {
        t.z = 0;
        if (paredes > 0 && !mouse.GetComponent<MouseController>().emArvore && gC.ChecaSeTaDentro(t))
        {
            GameObject novaParede = Instantiate(parede, t, transform.rotation);
            Parede paredeScript = novaParede.GetComponent<Parede>();
            paredeScript.dono = this;
            paredes--;
        }
        else
        {
            Debug.Log("não pode parede");
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == ("arvore"))
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
