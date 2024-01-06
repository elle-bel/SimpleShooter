using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PersonSpawners : MonoBehaviour
{
    public GameObject person;
    public GameObject gameFloor;

    // civillianRate representing a percentage of how many enemies spawned are civillians
    [Range(0,50)]
    public float civillianRate = 30;

    [Range(0,10)]
    public float spawnRatePer3Second = 2;

    private Bounds bounds;

    struct Coordinates{
        public float x;
        public float z;

        public Coordinates(float X, float Z) { x = X; z = Z;}
    }

    // requires that the center is at 0
    private float boundsX;
    private float boundsXNeg;
    private float boundsZ;
    private float boundsZNeg;
    private float spawnY = 0.25f;
    private bool collided;
    private int counter = 0;
    private Coordinates spawnPoint;
    private bool quitting;
    private float spawnerY;

    // Start is called before the first frame update
    void Start()
    {
        Bounds bounds = gameFloor.GetComponentInChildren<Renderer>().bounds;
        foreach(Renderer render in gameFloor.transform.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(render.bounds);
        }
        Assert.AreEqual(bounds.center, new Vector3(0,0,0));
        boundsX = bounds.extents.x;
        boundsXNeg = boundsX * -1;
        boundsZ = bounds.extents.z;
        boundsZNeg = boundsZ * -1;

        spawnerY = transform.position.y;

        StartCoroutine(BeginSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BeginSpawning(){
        while(!quitting){
            StartCoroutine(GetSpawnPosition());
            yield return new WaitForSeconds(3);
            for (int i = 0; i < spawnRatePer3Second; i++){
                SpawnPerson();
            }
        }
    }

    private void OnApplicationQuit() {
        quitting = true;
    }

    public void SpawnPerson(){
        if (Random.Range(1,100) < civillianRate+1){
            // spawn civillian           
            var newPerson = Instantiate(person, new Vector3(spawnPoint.x, spawnY, spawnPoint.z), Quaternion.identity);
            newPerson.GetComponent<RenderPerson>().IsEnemy = false;
        } else {
            // spawn enemy
            var newPerson = Instantiate(person, new Vector3(spawnPoint.x, spawnY, spawnPoint.z), Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("in ontrigger enter");
        collided = true;
        counter++;
        if (counter == 3){
            Debug.Log("couldnt find a valid position in 3 tries. ending");
            collided = false;
        }
    }

    IEnumerator GetSpawnPosition(){
        float newX;
        float newZ;
        do {
            newX = Random.Range(boundsXNeg, boundsX);
            newZ = Random.Range(boundsZNeg, boundsZ);
            Debug.Log("vals are: " + newX + " " + newZ);

            transform.position = new Vector3(newX, spawnerY, newZ);
            yield return new WaitForSeconds(0.5f);
            // moving it so ontriggerenter can fire
            transform.position = new Vector3(newX, spawnerY+3, newZ);
        } while (collided);
        Debug.Log("found coordinates valid" + newX + " and " + newZ);
        collided = false;
        spawnPoint = new Coordinates(newX, newZ);
    }
}