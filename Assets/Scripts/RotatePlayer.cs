using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] private InputAction axis;

    public float RotationSpeed = 1;

    private Vector2 rotation;

    private void Awake() 
    {
        axis.Enable();
        axis.performed += context => {OnRotate(context);};
    }

    void OnRotate(InputAction.CallbackContext context)
    {
        rotation = axis.ReadValue<Vector2>();
        //Debug.Log("in on rotate " + rotation + " x value " + rotation.x);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation *= Time.deltaTime * RotationSpeed;
        transform.Rotate(Vector3.up, rotation.x);
        //transform.Rotate(Vector3.right, rotation.y);
    }
}
