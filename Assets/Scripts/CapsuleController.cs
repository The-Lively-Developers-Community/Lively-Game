using UnityEngine;
using TMPro;

public class CapsuleController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 500f;
    public float timerMax = 2f;
    public TextMeshProUGUI timerText;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = timerMax;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0f, jumpForce, 0f));
            isGrounded = false;
        }

        if (timer < timerMax)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                timer = timerMax;
            }
        }

        timerText.text = "Time until stamina fills up: " + Mathf.Round(timer * 100f) / 100f;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Get the contact points between the capsule and the ground
            ContactPoint[] contacts = collision.contacts;

            // Check if any of the contact points have a normal vector pointing upwards (i.e., from the bottom of the capsule)
            bool isBottomCollision = false;
            foreach (ContactPoint contact in contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isBottomCollision = true;
                    break;
                }
            }

            // If the collision is happening from the bottom, set the timer to 0
            if (isBottomCollision)
            {
                timer = 0f;
                isGrounded = true;
            } // 
        }
    }
}