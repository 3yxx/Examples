using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Rigidbody enemyRb;
    private Vector3 lookDirection;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(player.GetComponent<PlayerController>().isAlive)
        {
            lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
       

        if(transform.position.y < -10 )
        {
            Destroy(gameObject);
        }
    }

    
}
