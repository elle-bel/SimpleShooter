using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAction pressed;
    public Rigidbody bullet;
    public Camera playerCamera;
    public float playerSpeed = 1;
    public float bulletSpeed = 1;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private void Awake() 
    {
        pressed.Enable();
        pressed.performed += context => {OnPressed(context);};
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue){
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        //Debug.Log("movement vector: " + movementVector + "x value: " + movementVector.x);
        movementX = movementVector.x; 
        movementY = movementVector.y;   
    }

    private void FixedUpdate() {
        // will have to change per camera direction
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        movement = CameraDirection(movement) * Time.deltaTime * playerSpeed;
        rb.MovePosition(transform.position + movement);
    }

    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = playerCamera.transform.forward;
        var cameraRight = playerCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        return cameraForward * movementDirection.z + cameraRight * movementDirection.x; 
   
    }

    void OnPressed(InputAction.CallbackContext context)
    {
        Rigidbody p = Instantiate(bullet, transform.TransformPoint(0,0.5f,1), Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up));
        Physics.IgnoreCollision(p.GetComponentInChildren<Collider>(), GetComponent<Collider>());
        //Debug.Log("force vector? " + transform.forward);
        p.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
