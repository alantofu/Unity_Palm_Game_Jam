using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class CameraController : MonoBehaviour
{
    // https://www.youtube.com/watch?v=rnqF6S7PfFA&t=734s

    public Transform cameraTransform;
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed; // change camera movement speed
    public float movementTime; // change camera smoothness movement
    public Vector3 newPosition;
    public Vector3 zoomAmount;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        // need to google how Vector3.Lerp works
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

}
