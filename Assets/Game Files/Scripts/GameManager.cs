
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Zombies in the scene")]
    public List<EnemyAi> ZombiesInTheScene;

    [Header("Zomabies Characters")]
    public List<EnemyAi> ZombiePrefabs;

    [Header("Spawners")]
    public GameObject[] Spawners;



    [Header("Game Properties")]
    public TextMeshProUGUI WavesText;
    public Player player;
    public AudioSource WaveClearedSFX;
    public float MaxNumbersOfZombies;
    public float CurrentNumberOfZombies;
    public float ExtraZombiesPerWave;
    public float SpawnPauseDuration;
    public float ExtraEnemyHealthAmount;
    public float ExtraEnemyDamageAmount;
    public int CurrentWave;
    public bool WaveCleared;

    private void Start()
    {
        //Declaring a new list in the scene
        ZombiesInTheScene = new List<EnemyAi>();

        //Edit the value of the text
        WavesText.text = "Wave 1 ";
    }

    private void Update()
    {
        //Checking if the number of the spawned zombies equal to the maximum numbers of zombies
        if (CurrentNumberOfZombies == MaxNumbersOfZombies)
        {
            //Disable the spawners in the scene
            SpawnerState(false);

            //Checking if the player killed all the zombies in the scene
            if (ZombiesInTheScene.Count == 0)
            {
                //Setting the wave cleared flag to true
                WaveCleared = true;
                //Lauching a new wave
                LaunchNewWave();
            }
        }


    }

    void LaunchNewWave()
    {
        //Checking if the wave finished
        if (WaveCleared)
        {
            //Increaase the number of the current wave
            CurrentWave++;

            //Reseting the number of the current wave
            CurrentNumberOfZombies = 0;

            //Editing the text
            WavesText.text = "Wave " + CurrentWave;

            //Increase the value of the max zombies per wave
            MaxNumbersOfZombies += ExtraZombiesPerWave;

            //Setting the flag tp false to insure that it only rin once
            WaveCleared = false;

            //Calling the PauseSpawners coroutine
            StartCoroutine(PauseSpawners());

            //Edit zommbie properties
            EditZombieProperties();

            //Play Audio Source
            WaveClearedSFX.Play();


        }
    }


    void EditZombieProperties()
    {
        foreach (EnemyAi ai in ZombiePrefabs)
        {
            ai.Health += ExtraEnemyHealthAmount;
            ai.Damage += ExtraEnemyDamageAmount;
        }
    }

    public void ResetZombieProperties()
    {
        foreach (EnemyAi ai in ZombiePrefabs)
        {
            ai.Health = 5;
            ai.Damage = 1.2f;
        }
    }

    IEnumerator PauseSpawners()
    {
        //Pausing the spawners for X amount of seconds
        yield return new WaitForSeconds(SpawnPauseDuration);

        //Enabling the spawners in the scene
        SpawnerState(true);
    }


    void SpawnerState(bool enable)
    {
        //Looping through the spawners in the scene
        //Disabling or Enabling the spawners
        for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].GetComponent<Spawner>().CanSpawn = enable;
        }
    }
    public void AddEnemy(EnemyAi enemy)
    {
        //Adding the zombie prefab to the list
        ZombiesInTheScene.Add(enemy);
        //Incrementing the number of Current Zombies
        CurrentNumberOfZombies++;

    }

    public void RemoveEnemy(EnemyAi enemy)
    {
        //Removing the zombie from the list
        ZombiesInTheScene.Remove(enemy);
    }
}
