using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer rend;
    private Animator anim;
    private Vector3 velocity;
    public float speed = 3.0f;
    public float jumpSpeed;
    public float moveSpeed = 3.25f;
    //public Transform ground;
    private bool grounded;

    private bool shouldJump;
    private bool canJump;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = true;
    


    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        //shouldJump = false;

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
        float moveHorizontal = Input.GetAxis("Horizontal");

        //velocity = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.identity;

        //animation 
        if (Input.GetAxis("Horizontal") != 0)
        {
            
            if (Input.GetKeyDown("d"))
            {
                animator.SetBool("move", true);
               
                velocity = new Vector3(1 * moveSpeed * Time.deltaTime, 0, 0);
                spriteRenderer.flipX = false;
                //rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            }
            else if (Input.GetKeyDown("a"))
            {
                spriteRenderer.flipX = true;
                
                animator.SetBool("move", true);
                
           
                velocity = new Vector3(-1*moveSpeed*Time.deltaTime, 0, 0);
                //velocity = new Vector3(-1f, 0f, 0f);
                //rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
                //spriteRenderer.flipX = true;
            }


        }
        else
        {
            velocity = new Vector3(0f, 0f, 0f);
            animator.SetBool("move", false);
        }
        if (Input.GetKeyDown("q"))
        {

            animator.SetTrigger("attack");
        }

        //generate velocity to move horizontally
        //velocity = new Vector3(Input.GetAxis("Horizontal") * 1f, 0f, 0f);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //if ( Input.GetKeyDown("space")&&isGrounded)
        //{
        //    print("canjump");

        //    canJump = true;
        //    shouldJump = true;
        //}

        //make sure the obect is inside the borders... if edge is hit reverse direction
        if ((transform.position.x <= leftBorder + width / 2.0) && velocity.x < 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
        if ((transform.position.x >= rightBorder - width / 2.0) && velocity.x > 0f)
        {
            velocity = new Vector3(0f, 0f, 0f);
        }
       
        transform.Translate(velocity * Time.deltaTime * speed);
    }


    void FixedUpdate(){
        //anim.Play("stand");
        
        //Vector3 velocity = new Vector3(moveHorizontal, 0.0f, 0f);
        
       
            //rb.AddForce(velocity , ForceMode2D.Impulse);
            
        //animation got mirrored when player goes left
        //    if (Input.GetAxis("Horizontal") < 0)
        //{
        //    velocity = new Vector3(-1f, 0f, 0f);
        //    rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
        //    spriteRenderer.flipX = true;
        //}
        //else
        //{
        //    spriteRenderer.flipX = false;
        //}


        // jump
        if (canJump&&Input.GetKeyDown("space") && isGrounded)
        {
            print("jump");
   
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            canJump = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D collider = other.collider;
        Vector3 contactPoint = other.contacts[0].point;
        Vector3 center = collider.bounds.center;

        //bool right = contactPoint.x > center.x;
        bool bottom = contactPoint.y < center.y;
        print("center " + center.y);
        print(contactPoint.y);

        //if (bottom)
        //{
            animator.SetBool("jump", false);
            isGrounded = true;
            Debug.Log("test");
            print("GROUNDED");
            canJump = true;
            
        //}
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        print("collision exit");
            isGrounded = false;
        animator.SetBool("jump", true);

    }

}
