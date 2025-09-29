using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{

    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public bool isGrounded;
    public bool isRight;
    public Animator anim;
    float xvel, yvel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        xvel = -2.5f;
    }
 
    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1.75f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayer);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, Vector3.down * rayLength, hitColor);

        return hitSomething;

    }


    // Update is called once per frame
    void Update()

    {

        yvel = rb.linearVelocity.y;
         
        

        if (xvel < 0)
        {
            if (ExtendedRayCollisionCheck(-0.5f, 0) == false)
            {
                xvel = -xvel;
                isRight = false;
            }
        }

        if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(0.5f, 0) == false)
            {
                xvel = -xvel;
                isRight = true;
            }
        }

        if (xvel > 0)
        {
            anim.SetBool("isWalking", true);
        }

        if (xvel > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

    }


}
