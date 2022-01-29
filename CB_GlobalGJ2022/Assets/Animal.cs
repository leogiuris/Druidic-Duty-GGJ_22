using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //fome
    public int hunger;
    public int maxHunger;
    private float hungerTimer;
    public float hungerTime;

    public int age;
    //componentes
    public GameController gC;
    private Rigidbody2D rBody;

    //movimentação
    public bool roaming = true;
    public float minMoveTime;
    public float maxMoveTime;
    public float moveTime;
    public float moveTimer;
    public Vector2 direction;
    public float speed;
    public float flipper = 1;



    // Start is called before the first frame update
    void Start()
    {
        gC = GameObject.Find("gameController").GetComponent<GameController>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        moveTimer = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);        
        direction = Random.insideUnitCircle;

        hungerTimer = hungerTime;

    }

    void Move()
    {
        if(roaming)
        {
            rBody.velocity = direction.normalized * speed * flipper;
            if(Time.time - moveTimer > moveTime)
            {
                Debug.Log("mudei dir");
                direction = Random.insideUnitCircle;
                moveTimer = Time.time;
                moveTime = Random.Range(minMoveTime, maxMoveTime);
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
        if(gameObject.GetComponent<Presa>())
        {
            gC.herbQtd--;
        }
        if (gameObject.GetComponent<Predador>())
        {
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
