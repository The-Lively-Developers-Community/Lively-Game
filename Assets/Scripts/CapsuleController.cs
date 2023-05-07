using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapsuleController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float jumpHeight = 5.0f;
    public TextMeshProUGUI timerText;

    private float timer = 0.0f;
    private Transform capsuleTransform;
    private Rigidbody rb;
    private bool isGrounded = true;
    private Transform cameraTransform;
    private Vector3 cameraForward;
    private Vector3 movement;

    void Start()
    {
        capsuleTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {

        // Use cameraForward instead of cameraTransform.forward
        Vector3 jumpDirection = cameraForward + Vector3.up;

        // Sprint using Ctrl+W
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            movement *= sprintSpeed / moveSpeed;
        }

        // Jump using the spacebar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && timer <= 0.0f)
        {
            rb.AddForce(jumpDirection * Mathf.Sqrt(jumpHeight * -2.0f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            capsuleTransform.position = new Vector3(0, 1.1f, -10);
        }

        // Apply movement to rigidbody
        movement.y = rb.velocity.y;
        rb.velocity = movement;

        if (isGrounded)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                timer = 0.0f;
            }
            timerText.text = "Time until stamina fills up: " + timer.ToString("F1");
        }
        else
        {
            if (timer < 0.0f)
            {
                timer = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        // Flatten the camera's forward direction onto the XZ plane
        cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;

        // Move the capsule using WASD keys
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraTransform.forward).normalized;

        movement = cameraForward * verticalMovement + cameraRight * horizontalMovement;
        movement = Vector3.ClampMagnitude(movement, 1.0f) * moveSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            timer = 0.5f;
            timerText.text = "Time until stamina fills up: " + timer.ToString("F1");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision is happening with a ground object
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Get the contact points between the capsule and the ground
            ContactPoint[] contacts = collision.contacts;

            // Check if any of the contact points have a normal vector pointing upwards (i.e., from the bottom of the capsule)
            bool isBottomCollision = false;
            foreach (ContactPoint contact in contacts)
            {
                if (contact.normal.y > 0.05f)
                {
                    isBottomCollision = true;
                    break;
                }
            }

            // If the collision is happening from the bottom, set the timer to 2.0
            if (isBottomCollision)
            {
                timer = 2.0f;
            }
        }
    }
}