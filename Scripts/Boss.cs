using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject bossProjectile;
    private GameObject player;
    public GameObject[] enemies;
    
    private bool isSpawnedEnemies = false;
    private bool isShooted = true;
    private float timeForNewWave = 8f;
    private float timeForShoot = 12f;
    private float projectileSpeed = 10f;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(ShootTimer(timeForShoot));    
    }
    
    void Update()
    {
        if (!isSpawnedEnemies)
        {
            Spawn();
        }

        if (!isShooted)
        {
            Shoot();
        }

        if(transform.position.y <= -2)
        {
            Destroy(gameObject);
        }

        // if(isShooted && isSpawnedEnemies)
        // {
        //     CreatePushingPads();
        //     CreateWalls();
        //     Debug.Log("YAAAYYY");
        // }
    }

    void Spawn()
    {  
        transform.LookAt(player.transform);
        bool isSpawnedRightSide = false;
        float z = 1;

        for(int i = 1; i < 3; i++)
        {
            
            int typeOfPrefabs = Random.Range(0,enemies.Length);

            GameObject newEnemy = Instantiate(enemies[typeOfPrefabs]);
            newEnemy.transform.SetParent(transform);

            if (isSpawnedRightSide)
            {
                newEnemy.transform.localPosition = new Vector3(-1,0,z);
                Debug.Log(newEnemy.transform.localPosition);
            }
            else
            {
                newEnemy.transform.localPosition = new Vector3(1,0,z);
                isSpawnedRightSide = true;
            }

            newEnemy.transform.localRotation = Quaternion.Euler(0,20,0);
            gameObject.transform.DetachChildren();

        }

        isSpawnedEnemies = true;
        StartCoroutine(WaveTimer(timeForNewWave));
    }


    void Shoot()
    {   
        
        transform.LookAt(player.transform);
        GameObject projectile =  Instantiate(bossProjectile);
        
        projectile.transform.SetParent(transform);
        projectile.transform.localPosition = new Vector3(0,0,1.2F);
        gameObject.transform.DetachChildren();

        isShooted = true;
        StartCoroutine(ShootTimer(timeForShoot));

    }

    void CreatePushingPads()
    {
        
    }

    void CreateWalls()
    {
        
    }


    IEnumerator WaveTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isSpawnedEnemies = false;
    }
    IEnumerator ShootTimer(float time)
    {
        yield return new WaitForSeconds(time);
        isShooted = false;
    }
}
