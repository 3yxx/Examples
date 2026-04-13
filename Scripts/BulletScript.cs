using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float forceBullet;

    public GameObject enemy;
    public ParticleSystem hittedParticle;
    public AudioClip hitClip;
    private AudioSource audioSource;
    private Vector3 directionToEnemy;
    private Rigidbody bulletRb;

    BulletScript(GameObject enemy)
    {
        this.enemy = enemy;
    }

    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();   
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(enemy != null || transform.position.y >= -1)
        {    
            directionToEnemy = (enemy.transform.position - transform.position);

            transform.LookAt(directionToEnemy);
            transform.Rotate(90,0,0,Space.Self);

            bulletRb.AddForce(directionToEnemy * speed);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Vector3 awayFromBullet = (enemy.transform.position - transform.position);
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            enemyRb.AddForce(awayFromBullet * forceBullet, ForceMode.Impulse);
            hittedParticle.Play();
            audioSource.PlayOneShot(hitClip);

            StartCoroutine(WaitUntilParticle());
        }        
    }

    IEnumerator WaitUntilParticle()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    
}
