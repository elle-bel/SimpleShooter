using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    public bool goesForward = true;
    public float personSpeed = 2;

    private bool forward = true;
    private float distanceTravelled = 0;
    private float maxDistance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // need to change this so it always moves "forward" in "direction its facing"
    void Update()
    {
        if (forward){
            if (goesForward) {
                transform.Translate(Vector3.right * Time.deltaTime * personSpeed);
                distanceTravelled += (Vector3.right * Time.deltaTime * personSpeed).x;
            } else {
                transform.Translate(Vector3.left * Time.deltaTime * personSpeed);
                distanceTravelled += (Vector3.left * Time.deltaTime * personSpeed).x * -1;
            }
            
            if (distanceTravelled > maxDistance){
                forward = false;
                distanceTravelled = 0;
            }
        } else {
            if (goesForward) {
                transform.Translate(Vector3.left * Time.deltaTime * personSpeed);
                distanceTravelled += (Vector3.left * Time.deltaTime * personSpeed).x * -1;
            } else {
                transform.Translate(Vector3.right * Time.deltaTime * personSpeed);
                distanceTravelled += (Vector3.right * Time.deltaTime * personSpeed).x;
            }
            if (distanceTravelled > maxDistance){
                forward = true;
                distanceTravelled = 0;
            }
        }
    }
}
