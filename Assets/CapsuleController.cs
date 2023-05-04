using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float jumpHeight = 5.0f;

    private Transform capsuleTransform;
    private Rigidbody rb;
    private bool isGrounded = true;
    private Transform cameraTransform;
    private Vector3 cameraForward;

    void Start()
    {
        capsuleTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        // Flatten the camera's forward direction onto the XZ plane
        cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
    }
    void FixedUpdate()
    {
        // Calculate movement direction in the XZ plane
        Vector3 moveDir = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        moveDir *= Input.GetAxis("Vertical") + Input.GetAxis("Horizontal");
        moveDir = Vector3.ClampMagnitude(moveDir, 1f);

        // Move the capsule in the XZ plane
        Vector3 moveAmount = moveDir * moveSpeed * Time.deltaTime;
        moveAmount.y = rb.velocity.y;
        rb.velocity = moveAmount;

        // Sprint using Ctrl+W
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W))
        {
            rb.velocity = moveAmount * sprintSpeed / moveSpeed;
        }

        // Jump using the spacebar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector3 jumpDirection = Vector3.up;
            rb.AddForce(jumpDirection * Mathf.Sqrt(jumpHeight * -2.0f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Set isGrounded to true when the capsule touches the ground
        if (collision.gameObject.CompareTag("Ground"))
        {//                                o <[it works, dont touch it!!]
            isGrounded = true;//          /|\
        }//                               / \
    }
}
