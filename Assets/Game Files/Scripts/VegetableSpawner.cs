
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VegetableSpawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    public List<GameObject> VegetablePrefabs;
    public Transform[] SpawnPositions;
    [SerializeField] int index;
    

    [Header("Spawn Rate")]
    [SerializeField] float SpawnRate = 6f;

    [Header("Flags")]
    public bool CanSpawn = true;
    private bool isSpawning = false;

    void Start()
    {
        
    }

    void Update()
    {
        //Checking if we can spawn 
        if (CanSpawn && !isSpawning)
        {
            InvokeRepeating("Spawn", 1f, SpawnRate);
            isSpawning = true;
        }

        //Checking if we cannot spawn
        if (!CanSpawn && isSpawning)
        {
            CancelInvoke("Spawn");
            isSpawning = false;
        }

        if (index == SpawnPositions.Length)
        {
            CancelInvoke("Spawn");
        }
    }

    void Spawn()
    {
        //Intializing a new integer 
        //This new integer will be used
        //To choose a random vegetable from the array
        int i = Random.Range(0, VegetablePrefabs.Count);



        //Intializing a new gameobject called new Vegetable
        //And assigning it to the intialized one
        GameObject newVegetable = Instantiate(VegetablePrefabs[i], SpawnPositions[index].position, Quaternion.identity);
        //Choose a random position
        index++;

    }
}
