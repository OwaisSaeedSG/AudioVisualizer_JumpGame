using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AIMove3 : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float fallSpeed = 2.5f;
    [SerializeField] private float lowfallSpeed = 2f;
    bool grounded = false;
    bool jump = false;

    private Rigidbody rb;

    // Using custom gravity
    private float gravityScale = 2.5f;
    private static float globalGravity = -9.81f;

    // ------------- AI 3 ------------- //


    // Get edge of the object we are standing on
    Vector3 max;

    // Get restart level canvasd
    public GameObject restartCanvas;

    // Number of jumps
    public int Njumps;

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

        if (jump && grounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
            grounded = false;
        }

        if (rb.transform.position.x + 1 > max.x)
        {
            jump = true;
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
        max = collision.collider.bounds.max;
        Njumps++;
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
       // restartCanvas.SetActive(true);
    }
}
