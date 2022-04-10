using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer rend;
    private Animator anim;
    private Vector3 velocity;
    //public float speed = 3.0f;
    public float jumpSpeed;
    public float moveSpeed;
    //public Transform ground;
    private bool grounded;
    private bool shouldJump;
    private bool canJump;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = true;
    float jumpTime;
    bool jumpCancelled;
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;
    public float distanceToCheck=0.5f;
    public GameController gameController;
    private bool gameOver; 
    private bool isFall;

    // Start is called before the first frame update
    void Start(){
        velocity = new Vector3(0f, 0f, 0f);
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        gameOver = false;
        isFall = false;
        //shouldJump = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
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

        //  if (Physics2D.Raycast(transform.position, Vector2.down, distanceToCheck))
        // {
        //     isGrounded = true;
        // }
        // else
        // {
        //     isGrounded = false;
        // }

        if (gameController.healthPoint <= 0)
        {
            animator.SetBool("die", true);
            gameOver = true;
            gameController.gameLost();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameController.pause();
        }

        if (!gameOver&& !isFall)
        {
            //if the player fall down to the abyess
            if (transform.position.y < -6)
            {

                gameController.loseHP(20);
            }


            //animation 
            if (Input.GetAxis("Horizontal") != 0)
            {

                if (Input.GetKey("d"))
                {
                    animator.SetBool("move", true);
                    velocity = new Vector3(1, 0, 0);
                    spriteRenderer.flipX = false;
                    //rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
                }
                else if (Input.GetKey("a"))
                {
                    animator.SetBool("move", true);
                    spriteRenderer.flipX = true;

                    velocity = new Vector3(-1, 0, 0);
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

        
            if (Input.GetKeyDown(KeyCode.Space)&&isGrounded) 
            {
                float jumpForce = Mathf.Sqrt(jumpSpeed * -2 * (Physics2D.gravity.y * rb.gravityScale));
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                canJump = true;
                jumpCancelled = false;
                jumpTime = 0;
            
            }
            if (canJump)
            {
                jumpTime += Time.deltaTime;
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    jumpCancelled = true;
                }
                if (jumpTime > buttonTime)
                {
                    canJump = false;
                }
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
       
            transform.Translate(velocity * Time.deltaTime * moveSpeed);
        }


        void FixedUpdate(){
            if (gameController.healthPoint <= 0){
                animator.SetBool("die", true);
                gameOver = true;
            }
            // jump
            if (gameOver == false){
                if (jumpCancelled && canJump && rb.velocity.y>0){
                    print("jump");
        
                    //rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                    rb.AddForce(Vector2.down * cancelRate);
                    //canJump = false;
            
                }
            }
        }
 
    }


    private void OnCollisionEnter2D(Collision2D other)

    {   // if no hp
        if (gameController.currentHP() <= 0)
        {
           // setBack();
            transform.rotation = Quaternion.identity;
            animator.SetBool("die", true);
            gameOver = true;

        }

        // if run into trap
        if (other.collider.gameObject.CompareTag("Trap"))
        {
            //setBack();
            transform.rotation = Quaternion.identity;
            animator.SetTrigger("fall");
            gameController.loseHP(1);
            isFall = true;

            ////this starts a coroutine... a non-blocking function
            StartCoroutine(PlayerCanFireAgain());
        }

        // if run into hidden button
        if(other.collider.gameObject.CompareTag("Button")){
            other.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Tilemap_hidden").GetComponent<TilemapRenderer>().enabled = false;
        }

        if(other.collider.gameObject.CompareTag("Heart")){
            if(gameController.currentHP() < gameController.getMaxHP()){
                gameController.setCurrentHP();
                other.collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                other.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }else if(gameController.currentHP() == gameController.getMaxHP()){
                other.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        Collider2D collider = other.collider;
        Vector3 contactPoint = other.contacts[0].point;
        Vector3 center = collider.bounds.center;

        //bool right = contactPoint.x > center.x;
        bool bottom = contactPoint.y < center.y;
        //print("center " + center.y);
        //print(contactPoint.y);

        foreach(ContactPoint2D hitPos in other.contacts)
        {
            if (hitPos.normal.y > 0)
            {
                Debug.Log("hit bottom");
                animator.SetBool("jump", false);
                isGrounded = true;
                print("GROUNDED");
                canJump = true;
            }
            else if (hitPos.normal.y < 0)
            {
                //Debug.Log("hit top");
            }
            else
            {
                Debug.Log("hit middle");
            }
        }

       
    }

    private void OnCollisionExit2D(Collision2D collision){      
        print("collision exit");
            isGrounded = false;
        animator.SetBool("jump", true);
    }

    private void setBack(){
        Vector3 velocityTemp = new Vector3(-1 *10 * 10*Time.deltaTime, 1 * 10* Time.deltaTime*3, 0);
        transform.Translate(velocityTemp * Time.deltaTime * 30);
    }

    IEnumerator PlayerCanFireAgain()
    {
        //this will pause the execution of this method for 3 seconds without blocking
        yield return new WaitForSecondsRealtime(1);
        isFall = false;
    }
}
