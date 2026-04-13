using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static public int numberOfWave;
    private PlayerController player;
    private int countOfEnemies;
    
    private bool isPlayerHadRest = false;
    private bool isBossWave = false;
    private bool isPowerUpSpawned = false;

    private float repeatSpawn = 5f;
    private float delaySpawn = 5f;
    private float spawnPosX;
    private float spawnPosZ;
    private float spawnRange = 9f;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private GameObject[] powerUpPrefab;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        numberOfWave = 1;
        StartNewWave(numberOfWave);
        
    }

    void Update()
    {
        countOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(countOfEnemies == 0 && player.isAlive)
        {
            numberOfWave ++;
            StartNewWave(numberOfWave);
        }
        
        if (isBossWave && !isPowerUpSpawned)
        {
            int typeOfPrefabs = UnityEngine.Random.Range(0,powerUpPrefab.Length);
            Spawn(powerUpPrefab[typeOfPrefabs]);
            isPowerUpSpawned =  true;

            StartCoroutine(PowerUpTimer());
        }
        
        
        
    }

    void BossWave()
    {
        int typeOfPrefabs = UnityEngine.Random.Range(0,powerUpPrefab.Length);
            
        Spawn(powerUpPrefab[typeOfPrefabs]);
        Spawn(enemyPrefab[2]);

        isBossWave = true;
        isPowerUpSpawned =  true;
        
        StartCoroutine(PowerUpTimer());

    }

    void StartNewWave(int numberOfWave)
    {   
        Debug.Log(numberOfWave % 3);
        if(numberOfWave % 3 == 0)
        {
            BossWave();
        }
        else
        {
            isBossWave = false;
            int typeOfPrefabs = UnityEngine.Random.Range(0,powerUpPrefab.Length);
            Spawn(powerUpPrefab[typeOfPrefabs]);

            for(int i = 0; i < numberOfWave; i++)
            {
                typeOfPrefabs = UnityEngine.Random.Range(0,enemyPrefab.Length-1);
                Spawn(enemyPrefab[typeOfPrefabs]);
            }
        }
       
    }

    void Spawn(GameObject gameObject)
    {
        Instantiate(gameObject,GenerateSpawnPoint(),gameObject.transform.rotation);  
    }

    

    Vector3 GenerateSpawnPoint()
    {
        spawnPosX = 0;
        spawnPosZ = 0;
        
        while(spawnPosX == 0 && spawnPosZ == 0)
        {
            spawnPosX = UnityEngine.Random.Range(-spawnRange,spawnRange);
            spawnPosZ = UnityEngine.Random.Range(-spawnRange,spawnRange);            
        }

        return new Vector3(spawnPosX,0,spawnPosZ);
    }

    IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(5f);
        
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(7f);
        isPowerUpSpawned = false;
    }

    
}
