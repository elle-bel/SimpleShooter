using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    public event EventHandler PersonDestroyed;
    public float personSpeed = 2;
    public float rotationSpeed = 2;

    private float distanceTravelled = 0;
    private float maxDistance = 2.0f;
    private float travelled;

    private bool rotating;
    private float rotationGoal;
    private float amountRotated;
    
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
        if (rotating){
            transform.Rotate(new Vector3(0,rotationGoal,0) * Time.deltaTime * rotationSpeed);
            amountRotated += (new Vector3(0,rotationGoal,0) * Time.deltaTime * rotationSpeed).y;
            if (amountRotated >= rotationGoal){
                amountRotated = 0;
                rotating = false;
            }
        }
        else {
            moveForward();
            if (travelled == 0){
                rotationGoal = 180.0f;
                rotating = true;
            }
            if (distanceTravelled > maxDistance){
                distanceTravelled = 0;
                rotationGoal = UnityEngine.Random.Range(0,180);
                rotating = true;
            } 
        }
    }

    private void OnDestroy() {
        Debug.Log("in ondestroy");
        PersonDestroyed?.Invoke(this, null);
    }
}
