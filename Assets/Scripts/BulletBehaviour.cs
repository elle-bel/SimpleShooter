using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private bool canDestroy = true;
    private void Awake() {
        Destroy(gameObject, 5);
    }

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // need to decide whether to make persons triggers or not
    // because of walls? and other persons -> do we want them to collide or no?

    void OnTriggerEnter(Collider other) 
    {
       // if (other.gameObject.CompareTag("Person") && canDestroy)
        //{
        //    Destroy(other.transform.parent.gameObject);
        //}   
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Wall")){
            canDestroy = false;
        }

        if (other.gameObject.CompareTag("Person") && canDestroy)
        {
            // if a trigger than use parent...?
            Destroy(other.transform.gameObject);
        }   
    }
}
