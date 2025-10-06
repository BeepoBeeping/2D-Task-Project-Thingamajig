using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public bool isGrounded;
    public bool isRight;
    public Animator anim;
    public LayerMask groundLayer;
    public int health;
    public int lives;
    HelperScript helper;

    void Start()
    {
        lives = 3;
        health = 3;
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        helper = gameObject.AddComponent<HelperScript>();
    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 2.25f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayer);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
           
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;

    }

    // Update is called once per frame
    void Update()
    {

        float xvel, yvel;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            xvel = -6;
            helper.DoFlipObject(true);
            ExtendedRayCollisionCheck(0.5f, 0);
            anim.SetBool("isRunning", true);
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            xvel = 6;
            helper.DoFlipObject(false);
            ExtendedRayCollisionCheck(-0.5f, 0);
            anim.SetBool("isRunning", true);
        }

        //do ground check

        if (ExtendedRayCollisionCheck(-0.5f, 0) == true)
        {
            isGrounded = true;
            if (yvel < 0)
            {
                anim.SetBool("isJumping", false);
            }
        }
        else
        {
            isGrounded = false;
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            yvel = 8.25f;

            print("Jump");
            anim.SetBool("isJumping", true);
            anim.SetBool("isRunning", false);  
        }

        if (xvel == 0)
        {
            anim.SetBool("isRunning", false);
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        if(lives == 0)
        {
            SceneManager.LoadScene("LVL_1");
            lives = 3;
        }

        if(health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            lives = lives - 1;

        }

      

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
            {
                health = health - 1;
            }

        if (other.gameObject.tag == "Lava")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        
        if (other.gameObject.tag == "Finish")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
    }

   


    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
        print("Grounded");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        anim.SetBool("isJumping", false);
    }
    */



    /*
    float xpos = transform.position.x;
    float ypos = transform.position.y;

    if (Input.GetKeyDown("w") == true)
    {
        ypos += 0.1f;
        transform.position = new Vector3(xpos, ypos, 0);
        print(ypos);
    }    
    if (Input.GetKeyDown("d") == true)
    {
        xpos += 0.1f;           
        print(xpos);
    }
    if (Input.GetKeyDown("s") == true)
    {
        ypos -= 0.1f;            
        print(ypos);
    }
    if (Input.GetKeyDown("a") == true)
    {
        xpos -= 0.1f;        
        print(xpos);
    }

    if (ypos > 5f)
    {
        ypos = 5f;
    }
    if (ypos < -5f)
    {
        ypos = -5f;
    }
    if (xpos > 6f)
    {
        xpos = 6f;
    }
    if (xpos < -6f)
    {
        xpos = -6f;
    }

    transform.position = new Vector2(xpos, ypos);
    */
}

