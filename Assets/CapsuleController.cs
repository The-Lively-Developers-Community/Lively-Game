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

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            movement *= sprintSpeed / moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            capsuleTransform.position = new Vector3(0, 1.1f, -10);
        }

        // Apply movement to rigidbody
        movement.y = rb.velocity.y;
        rb.velocity = movement;
    }

    void Update()
    {
        if (isGrounded)
        {
            timer -= Time.deltaTime;
            if (timer < 0.0f)
            {
                timer = 0.0f;
            }
            timerText.text = "Time until stamina fills up: " + timer.ToString("F1");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            timer = 2.0f;
            timerText.text = "Time until stamina fills up: " + timer.ToString("F1");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
