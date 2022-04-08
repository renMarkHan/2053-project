using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  
    private Animator anim;
    
    public float speed = 2.0f;
    private Vector3 velocity;

    private Rigidbody2D rb;
    private int nextupdate=1;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        velocity = new Vector3(0f, 0f, 0f);
        //rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=Quaternion.identity;
        int change = Random.Range(0, 100);

        //move left
        if(change>=50){
            if(Time.time>nextupdate){
            velocity = new Vector3(-1f, 0f, 0f);
            nextupdate=Mathf.FloorToInt(Time.time)+2;
            }
        }
        //move right
        else if (change<50){
            if(Time.time>nextupdate){
                velocity = new Vector3(1f, 0f, 0f);
              nextupdate=Mathf.FloorToInt(Time.time)+2;
            }
        }
        transform.Translate(velocity * Time.deltaTime * speed);
        anim.Play("walk");
    }
}
