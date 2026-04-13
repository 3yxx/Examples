using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] obstacles;
    private Vector3 spawnPos;

    private float startDelay = 2; 
    private float repeatDelay = 2f;
    private PlayerController playerController;

    void Start()
    {
        InvokeRepeating("Spawn",startDelay,repeatDelay);
        spawnPos = transform.position;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    
    void Spawn()
    {
        if (!playerController.gameOver)
        {
            int numberOfObstacle = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[numberOfObstacle], spawnPos, obstacles[numberOfObstacle].transform.rotation);
        }
    }
}
