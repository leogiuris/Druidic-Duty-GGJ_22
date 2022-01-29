using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    public int hunger;
    public int age;
    //componentes
    private Rigidbody2D rBody;

    //movimentação
    public bool roaming = true;
    public float minMoveTime;
    public float maxMoveTime;
    public float moveTime;
    public float moveTimer;
    public Vector2 direction;
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
        moveTimer = Time.time;
        moveTime = Random.Range(minMoveTime, maxMoveTime);        
        direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    void Move()
    {
        if(roaming)
        {
            rBody.velocity = direction.normalized * speed;
            if(Time.time - moveTimer > moveTime)
            {
                Debug.Log("mudei dir");
                direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                moveTimer = Time.time;
                moveTime = Random.Range(minMoveTime, maxMoveTime);
            }
        }
    }

    public void Eat()
    {

    }
    public void Breed()
    {

    }

    public void Die()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        Move();
    }
}
