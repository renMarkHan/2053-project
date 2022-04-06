using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer rend;
    private Animator anim;
    private Vector3 velocity;
    public float speed = 3.0f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate location of screen borders
        // this will make more sense after we discuss vectors and 3D
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        //get the width of the object
        float width = rend.bounds.size.x;
        float height = rend.bounds.size.y;

        transform.rotation = Quaternion.identity;

        //animation 
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }
        if (Input.GetKeyDown("q"))
        {
            animator.SetTrigger("attack");
        }
            //animation got mirrored when player goes left
        if (Input.GetAxis("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //generate velocity to move horizontally
        velocity = new Vector3(Input.GetAxis("Horizontal") * 1f, 0f, 0f);

        //make sure the obect is inside the borders... if edge is hit reverse direction
        if ((transform.position.x <= leftBorder + width / 2.0) && velocity.x < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.x >= rightBorder - width / 2.0) && velocity.x > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        // if(velocity.x>0){
        //     anim.Play("move");
        // }
        transform.Translate(velocity * Time.deltaTime * speed);
    }

    void fixedUpdate()
    {
        // anim.Play("stand");
    }
}
