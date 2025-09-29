using JetBrains.Annotations;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float xvel, yvel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        xvel = 3;

        rb.linearVelocity = new Vector3(xvel, yvel, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        xvel = -xvel;
    }
}
