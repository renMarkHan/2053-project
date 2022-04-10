using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  
    private Animator animator;
    
    public float speed = 2.0f;
    private Vector3 velocity;

    private Rigidbody2D rb;
    private int nextupdate=1;
    private SpriteRenderer spriteRenderer;
    private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        velocity = new Vector3(1f, 0f, 0f);
        //rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=Quaternion.identity;

        if (canAttack)
        {
            canAttack = false;
            animator.Play("blackEnemy_attack");
            StartCoroutine(PlayerCanFireAgain());
        }

        int change = Random.Range(0, 100);

        //move left
        if(change>=50){
            if(Time.time>nextupdate){
                velocity = new Vector3(-1f, 0f, 0f);
                nextupdate=Mathf.FloorToInt(Time.time)+3;
                spriteRenderer.flipX = false;
            }
        }
        //move right
        else if (change<50){
            //go right

            if(Time.time>nextupdate){
                velocity = new Vector3(1f, 0f, 0f);
                nextupdate=Mathf.FloorToInt(Time.time)+3;
                spriteRenderer.flipX = true;
            }
        }
        transform.Translate(velocity * Time.deltaTime * speed);
        //animator.Play("walk");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print("collider enemy");
        if (collision.contacts[0].point == Vector2.left)
        {
            Debug.Log("hit middle");
            velocity = new Vector3(-1 * velocity.x, 0f, 0f);
            if (velocity.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (velocity.x > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
            foreach (ContactPoint2D hitPos in collision.contacts)
        {
            if (hitPos.normal.y > 0)
            {
                //Debug.Log("hit bottom");
            }
            else if (hitPos.normal.y < 0)
            {
                //Debug.Log("hit top");
            }
            //else
            //{
            //    Debug.Log("hit middle");
            //    velocity = new Vector3(-1 * velocity.x, 0f, 0f);
            //    if (velocity.x < 0)
            //    {
            //        spriteRenderer.flipX = false;
            //    }
            //    else if (velocity.x > 0)
            //    {
            //        spriteRenderer.flipX = true;
            //    }
            //}
        }
       
    }

    IEnumerator PlayerCanFireAgain()
    {
        //this will pause the execution of this method for 3 seconds without blocking
        yield return new WaitForSecondsRealtime(3);
        canAttack = true;
    }
}
