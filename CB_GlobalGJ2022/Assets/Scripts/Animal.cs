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

    protected float age;
    protected bool child = true;
    public bool isDead = false;
    //caçar
    public bool chase;
    public GameObject alvo;

    //componentes
    private CircleCollider2D myCol;
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
        child = true;
        age = moveTimer = hungerTimer = Timer.getTime();
        tChaseSpeed = chaseSpeed;
        tSpeed = speed;
        hunger = 0;
        myCol = gameObject.GetComponent<CircleCollider2D>();
        gC = GameObject.Find("gameController").GetComponent<GameController>();
        anim = gameObject.GetComponent<Animator>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        //moveTimer = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);        
        direction = Random.insideUnitCircle;
        transform.localScale = new Vector3(Mathf.Sign(direction.x) * 0.5f, 0.5f);


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

    protected void Grow()
    {
        child = false;
        transform.localScale = new Vector3(1, 1);
    }

    void Move()
    {

        float scale = 1f ;
        if (child)
        {
            scale = 0.6f;
        }

        if (!flee)
        {
            if (roaming)
            {
                rBody.velocity = direction.normalized * speed * flipper;
                if (Timer.getTime() - moveTimer > moveTime)
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
                    if (alvo == null)
                    {
                        roaming = true;
                        chase = false;
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

        transform.localScale = new Vector3(Mathf.Sign(rBody.velocity.x)* scale, 1 * scale);

    }

    public void RandomizeDirection()
    {
        direction = Random.insideUnitCircle;
        moveTimer = Timer.getTime();
        moveTime = Random.Range(minMoveTime, maxMoveTime);

    }



    public void GetClosestAlvo(List<GameObject> alvos)
    {
        float minDist = 30f;
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

        if(Timer.getTime() - hungerTimer > hungerTime)
        {
            hunger++;
            hungerTimer = Timer.getTime();
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
        if(hunger == maxHunger && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void Breed(GameObject animal)
    {
        
        gC.SpawnaAnimal(animal, transform.position);
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
        speed = 0;
        chaseSpeed = 0;
        myCol.enabled = false;
        anim.SetTrigger("dead");
    }
    public void Poof()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
         //Eat();
        
    }
    void FixedUpdate()
    {
        if(!isDead)
            Move();
    }
}
