using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{

    public float jumpSpeed = 6f;
    public float fallSpeed = 6f;
    public float lowfallSpeed = 5f;
    bool grounded = false;

    private Rigidbody rb;

    // Using custom gravity
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;


    // Angle for the AI
    public float angleUp= 45f;


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

        //------------------- For the AI -------------------//
        var directionUp = (-transform.up - transform.right).normalized;
        //var directionDown = Quaternion.AngleAxis(angle, transform.right) * transform.forward;

        Ray rayUp = new Ray(transform.position, directionUp);
        Ray rayMid = new Ray(transform.position, transform.right);
        Ray rayDown = new Ray(transform.position, transform.forward);

        //RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(rayMid, 1.5f))
        {
            Debug.DrawRay(rayMid.origin, rayMid.direction * 1.5f, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(rayMid.origin, rayMid.direction * 1.5f, Color.white);
            Debug.Log("Did Not Hit");
        }


        // Up
        if (Physics.Raycast(rayUp, 1))
        {
            Debug.DrawRay(rayUp.origin, rayUp.direction * 1, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(rayUp.origin, rayUp.direction * 1, Color.white);
            Debug.Log("Did Not Hit");
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
}
