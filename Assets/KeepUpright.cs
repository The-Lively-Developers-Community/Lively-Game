using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUpright : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate the ground normal and rotate towards it
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            // Calculate the rotation only around the Y axis
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = 0.0f; eulerRotation.z = 0.0f;
            targetRotation = Quaternion.Euler(eulerRotation);

            // Apply the rotation to the rigidbody
            rb.MoveRotation(targetRotation * transform.rotation);
        }
    }

}
