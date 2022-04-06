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

    public Collider2D collider;
    private bool shouldJump;
    private bool canJump;
    private Rigidbody2D rb;
    private float offset;
    private float distToGround;


    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canJump = false;
        shouldJump = false;
        //offset = (transform.position.y-ground.transform.position.y) + 1;
        distToGround = collider.bounds.extents.y;
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

        //generate velocity to move horizontally
        velocity = new Vector3(Input.GetAxis("Horizontal") * 1f, 0f, 0f);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if ( Input.GetKeyDown("space"))
        {
            print("canjump");
            
            canJump = true;
            shouldJump = true;
        }

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
        if (velocity != Vector3.zero)
        {
            rb.AddForce(velocity * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        // jump
        if (canJump)
        {
            print("jump");
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            canJump = false;
        }
    }


    bool IsGrounded() {
        print("isGround "+ Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1)));
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }
}
