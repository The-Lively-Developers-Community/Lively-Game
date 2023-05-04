using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;

    public float sensitivity = 1f; // set the sensitivity for the camera movement

    private float mouseX = 0f; // stores the horizontal mouse movement
    private float mouseY = 0f; // stores the vertical mouse movement

    private float clampAngle = 80f; // set the maximum angle the camera can rotate vertically

    void Start()
    {
        initalOffset = transform.position - targetObject.position;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the mouse to the center of the screen

        mouseX += Input.GetAxis("Mouse X") * sensitivity; // update the horizontal mouse movement
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity; // update the vertical mouse movement
        mouseY = Mathf.Clamp(mouseY, -clampAngle, clampAngle); // clamp the vertical rotation angle to a certain range

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f); // create a new rotation quaternion from the mouse movement
        Vector3 position = rotation * new Vector3(0f, 0f, -initalOffset.magnitude) + targetObject.position; // calculate the new camera position based on the rotation and offset

        transform.rotation = rotation; // set the camera rotation to the new rotation
        transform.position = position; // set the camera position to the new position
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
        {
            Cursor.lockState = CursorLockMode.None; // unlock the mouse if the application loses focus
        }
    }
}
