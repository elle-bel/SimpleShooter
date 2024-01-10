using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    public float personSpeed = 2;

    private float distanceTravelled = 0;
    private float maxDistance = 2.0f;
    private float travelled;
    
    float getMovementAmount(Vector3 initPosition, Vector3 newPosition){
        return (float)Math.Sqrt(Math.Pow(newPosition.x - initPosition.x, 2) + Math.Pow(newPosition.z - initPosition.z, 2));
    }

    void moveForward(){
        var initPos = transform.position;
        transform.Translate(Vector3.forward * Time.deltaTime * personSpeed);
        travelled = getMovementAmount(initPos, transform.position);
        distanceTravelled += travelled;
    }

    // need to smooth out rotations
    void Update()
    {
        moveForward();
        if (travelled == 0){
            transform.Rotate(new Vector3(0, 180,0));
        }
        if (distanceTravelled > maxDistance){
            distanceTravelled = 0;
            transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0,180),0));
        } 
    }
}
