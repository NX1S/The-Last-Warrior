using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Properties")]
    public List<GameObject> ZombiePrefabs;
    public Transform[] SpawnPositions;
    int index;
    [SerializeField] GameManager Manager;

    [Header("Spawn Rate")]
    [SerializeField] float SpawnRate = 6f;

    [Header("Flags")]
    public bool CanSpawn = true;
    private bool isSpawning = false;

    void Start()
    {
        //Finding the game manager hame object in the scene
        Manager = FindObjectOfType<GameManager>();
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
    }

    void Spawn()
    {
        //Intializing a new integer 
        //This new integer will be used
        //To choose a random zombie from the array
        int i = Random.Range(0, ZombiePrefabs.Count);

        //Choose a random position
        index = Random.Range(0, SpawnPositions.Length);

        //Intializing a new gameobject called new zombies
        //And assigning it to the intialized one
        GameObject newZombie = Instantiate(ZombiePrefabs[i], SpawnPositions[index].position, Quaternion.identity);

        //Storing the EnemyAi component form the instantiated zombie
        //Into a variable called ai
        EnemyAi ai = newZombie.GetComponent<EnemyAi>();

        //Adding the zombie to the list
        //Manager.AddEnemy(ai);
    }
}
