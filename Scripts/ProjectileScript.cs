using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public float forceBullet;

    public ParticleSystem hittedParticle;
    public AudioClip hitClip;
    private GameObject player;
    private AudioSource audioSource;
    private Rigidbody projectileRb;
    private Vector3 directionOfPlayer;


    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();   
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        
        directionOfPlayer = (player.transform.position - transform.position);

    }

    void Update()
    {
        projectileRb.AddForce(directionOfPlayer * speed, ForceMode.Impulse);

        if(transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 awayFromBullet = (player.transform.position - transform.position);
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            playerRb.AddForce(awayFromBullet * forceBullet, ForceMode.Impulse);
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
