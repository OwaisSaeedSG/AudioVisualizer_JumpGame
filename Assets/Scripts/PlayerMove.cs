using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float jumpForce;
    public float gravityMod;
    public bool onGround = true;
    public bool jumpKey = false;
    public bool isJumping = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            onGround = false;
            jumpKey = true;
            isJumping = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpKey = false;
        }
    }

    private void FixedUpdate()
    {
        if(isJumping)
        {
            if (!jumpKey && Vector3.Dot(rb.velocity, Vector3.up) > 0)
            {
                rb.AddForce(Vector3.down * jumpForce);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
        onGround = true;
    }
}
