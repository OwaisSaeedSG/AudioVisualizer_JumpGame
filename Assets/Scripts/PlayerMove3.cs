using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove3 : MonoBehaviour
{

    public float jumpSpeed = 6f;
    public float fallSpeed = 6f;
    public float lowfallSpeed = 5f;
    bool grounded = false;

    private Rigidbody rb;

    // Using custom gravity
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void Update()
    {

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            grounded = false;
        }

    }

    void FixedUpdate()
    {

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        if (rb.velocity.y < 0)
        {
            rb.AddForce(gravity * fallSpeed, ForceMode.Acceleration);
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.AddForce(gravity * lowfallSpeed, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(gravity, ForceMode.Acceleration);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Obstacle")
        {
            Debug.Log("Obstacle Hit");
        }
    }
}
