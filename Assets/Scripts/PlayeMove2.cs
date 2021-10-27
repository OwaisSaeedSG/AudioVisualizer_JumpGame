using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMove2 : MonoBehaviour
{
    public float jumpShortSpeed = 3f;   
    public float jumpSpeed = 6f;
    bool jump = false;
    bool jumpCancel = false;
    bool grounded = false;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    
    }
    void Update()
    {

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            grounded = false;
        }
        if (Input.GetButtonUp("Jump") && !grounded)     // Player stops pressing the button
            jumpCancel = true;
    }

    void FixedUpdate()
    {
        // Normal jump (full speed)
        if (jump)
        {
            rb.velocity = new Vector3(0, jumpSpeed);
            jump = false;
        }
        // Cancel the jump when the button is no longer pressed
        if (jumpCancel)
        {
            if (rb.velocity.y > jumpShortSpeed)
                rb.velocity = new Vector3(0, jumpShortSpeed);
            jumpCancel = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        jump = false;
        grounded = true;
    }
}
