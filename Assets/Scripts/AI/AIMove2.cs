using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMove2 : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float fallSpeed = 2.5f;
    public float lowfallSpeed = 2f;
    bool grounded = false;
    public bool jump = false;

    private Rigidbody rb;

    // Using custom gravity
    public float gravityScale = 2.5f;
    public static float globalGravity = -9.81f;

    // Get restart level canvasd
    public Canvas restartCanvas;
    private bool playerDeadBool;

    // To make sure AI is on or not
    public bool autoPilot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        playerDeadBool = false;
        autoPilot = false;
    }
    void Update()
    {
        if (autoPilot == false)
        { 
            if (Input.GetButtonDown("Jump") && grounded)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                grounded = false;
            }
        }
        else
        {
            if (jump && grounded)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                grounded = false;
            }
        }
        
    }

    void FixedUpdate()
    {

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        if (rb.velocity.y < 0)
        {
            rb.AddForce(gravity * fallSpeed, ForceMode.Acceleration);
            jump = false;
        }
        else if (rb.velocity.y > 0 && !jump)
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

    public void TopTrigger(Collider t)
    {
        jump = true;

        Debug.Log("Top True");
    }
    public void MidTrigger(Collider m)
    {
        jump = true;
        Debug.Log("Mid True");
    }
    public void BottomTrigger(Collider b)
    {
        jump = true;
        Debug.Log("Bot True");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Pole")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnBecameInvisible()
    {
        if(!playerDeadBool)
        { 
            restartCanvas.gameObject.SetActive(true);
            playerDeadBool = false;
        }
    }

    public void TurnAutoPilotOnOff()
    {
        if (autoPilot)
            autoPilot = false;
        else
            autoPilot = true;
    }

}
