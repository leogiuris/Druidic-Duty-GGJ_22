using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //fome
    public int hunger;
    public int chaseHunger;
    public int maxHunger;
    private float hungerTimer;
    public float hungerTime;

    public int age;

    //caçar
    public bool chase;
    public GameObject alvo;

    //componentes
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public bool souPresa = false;
    [HideInInspector]
    public GameController gC;
    [HideInInspector]
    private Rigidbody2D rBody;

    //movimentação
    public GameObject danger;
    public bool flee = false;
    public float safeRange;

    public bool roaming = true;
    public float minMoveTime;
    public float maxMoveTime;
    public float moveTime;
    public float moveTimer;
    public Vector2 direction;
    public float speed;
    public float chaseSpeed;
    [HideInInspector]
    public float tSpeed;
    [HideInInspector]
    public float tChaseSpeed;
    [HideInInspector]
    public float flipper = 1;

    //emotes
    public Animator emoter;



    // Start is called before the first frame update
    void Start()
    {
        tChaseSpeed = chaseSpeed;
        tSpeed = speed;
        hunger = 0;
        gC = GameObject.Find("gameController").GetComponent<GameController>();
        anim = gameObject.GetComponent<Animator>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        moveTimer = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);        
        direction = Random.insideUnitCircle;

        hungerTimer = hungerTime;
        //identificar se sou prese aou pred
        if (gameObject.GetComponent<Presa>())
        {
            souPresa = true;
        }
        if (gameObject.GetComponent<Predador>())
        {
            souPresa = false;
        }


    }

    void Move()
    {
        if (!flee)
        {
            if (roaming)
            {
                rBody.velocity = direction.normalized * speed * flipper;
                if (Time.time - moveTimer > moveTime)
                {
                    RandomizeDirection();
                }
            }
            if (chase)
            {
                if (alvo == null)
                {
                    if (!souPresa)
                    {
                        GetClosestAlvo(gC.herbivoros);
                    }
                    else
                    {
                        GetClosestAlvo(gC.plantas);
                    }

                }
                else
                {
                    direction = alvo.transform.position - transform.position;
                    rBody.velocity = direction.normalized * chaseSpeed;
                }
            }
        }
        else
        {
            if (danger != null)
            {
                emoter.SetTrigger("medo");
                direction = transform.position - danger.transform.position;
                rBody.velocity = direction.normalized * chaseSpeed;
                if(Vector3.Distance(transform.position, danger.transform.position) > safeRange)
                {
                    emoter.SetTrigger("none");
                    danger = null;
                    flee = false;
                }
            }
            else
            {
                flee = false;
            }
        }
        transform.localScale = new Vector3(-Mathf.Sign(rBody.velocity.x), 1, 1);
    }
    public void RandomizeDirection()
    {
        direction = Random.insideUnitCircle;
        moveTimer = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);
    }
    public void GetClosestAlvo(List<GameObject> alvos)
    {
        float minDist = Mathf.Infinity;
        foreach(GameObject t in alvos)
        {
            float dist = Vector3.Distance(transform.position, t.transform.position);
            if(dist<minDist)
            {
                minDist = dist;
                alvo = t;
            }
        }
    }

    public void Eat()
    {
        if(Time.time - hungerTimer > hungerTime)
        {
            hunger++;
            hungerTimer = Time.time;
        }
        if(hunger > chaseHunger)
        {            
            chase = true;
            roaming = false;
            if (!souPresa)
            {

                emoter.SetTrigger("hunt");
            }
            else
            {
                emoter.SetTrigger("fome");
            }
        }
        else
        {
            alvo = null;
            chase = false;
            roaming = true;
        }
        if(hunger == maxHunger)
        {
            Die();
        }
    }
    public void Breed()
    {

    }

    public void Die()
    {
        if(souPresa)
        {
            gC.herbivoros.Remove(this.gameObject);
            gC.herbQtd--;
        }
        else
        {
            gC.predadores.Remove(this.gameObject);
            gC.predQtd--;
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Eat();
    }
    void FixedUpdate()
    {
        Move();
    }
}
