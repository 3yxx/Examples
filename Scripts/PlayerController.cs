using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float speed;
    public int powerUp;
    public bool isAlive;
    private float verticalInput;
    private float horizontalInput;
    private bool isPowerUped;
    private bool isJumped;
    private string typeOfPowerUp;
    private int timeOfPowerup;

    public GameObject gameOverCanvas;
    public GameObject[] Indicators;
    private GameObject powerUpIndicator;
    public ParticleSystem PowerUpParticle;
    public ParticleSystem GroundedParticle;
    
    public AudioClip powerUpUsed;
    public AudioClip powerUpSound;
    private AudioSource sfxSource;

    public GameObject bulletPrefab;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    delegate void PowerUps();
    private PowerUps powerUpDelegate;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        sfxSource = GetComponent<AudioSource>();

        powerUpIndicator = new GameObject();
        focalPoint = GameObject.Find("Focal point");
        typeOfPowerUp = "None";

        isAlive = true;
        isJumped = false;
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        powerUpIndicator.transform.position = transform.position + new Vector3(0,-0.5F,0);
        

        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);     
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed);     

        if(transform.position.y < -10)
        {
            gameOverCanvas.SetActive(true);
            isAlive = false;
        }

        if (isPowerUped && typeOfPowerUp != "Power")
        {
            powerUpDelegate.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        powerUpIndicator.SetActive(false);
        
        SelectPowerUp(other.tag);

        Destroy(other.gameObject);
        sfxSource.PlayOneShot(powerUpSound,1f);

        isPowerUped = true;
        powerUpIndicator.transform.position = transform.position + new Vector3(0,-0.5F,0);

        StartCoroutine(PowerUpCountDown());
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy"  && typeOfPowerUp == "Power")
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log($"Collided with {collision.gameObject.name} with powerup set to {isPowerUped} with increased power on {powerUp}");
            
            PowerUpParticle.Play();
            enemyRb.AddForce(awayFromPlayer * powerUp,ForceMode.Impulse);
            sfxSource.PlayOneShot(powerUpUsed,0.3f);
        }
        
        if(collision.gameObject.tag == "Ground" && isJumped)
        {
            isJumped = false;
            GroundedParticle.Play();
            sfxSource.PlayOneShot(powerUpUsed);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies)
            {
                Vector3 awayFromPlayer = (enemy.transform.position - transform.position);
                enemy.GetComponent<Rigidbody>().AddForce(awayFromPlayer * powerUp * 2,ForceMode.Impulse);
            }
            
        }
    }

    void SelectPowerUp(string tag)
    {
        if(tag == "Powerup")
        {
            Indicators[0].SetActive(true);
            powerUpIndicator = Indicators[0];
            typeOfPowerUp = "Power";
            timeOfPowerup = 7;
            
        }
        else if(tag == "ShootPowerUp")
        {
            Indicators[1].SetActive(true);
            powerUpIndicator = Indicators[1];
            typeOfPowerUp = "Shoot";
            timeOfPowerup = 5;
            
            powerUpDelegate += Shoot;
        }
        else if(tag =="JumpPowerUp")
        {
            Indicators[2].SetActive(true);
            powerUpIndicator = Indicators[2];
            timeOfPowerup = 6;
            typeOfPowerUp = "Jump";

            powerUpDelegate += SuperJump;
        }
    }

    void SuperJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(speed * Vector3.up * 1.25f, ForceMode.Impulse);
            PowerUpParticle.Play();
            isJumped = true;
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach(GameObject enemy in enemies)
            {
                Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
                bulletPrefab.GetComponent<BulletScript>().enemy = enemy;
                if(enemy.transform.position.y <= 1)
                {
                    Instantiate(bulletPrefab, transform.position + transform.forward, bulletPrefab.transform.rotation);
                }
            }
        }
    }

    
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(timeOfPowerup);

        powerUpIndicator.SetActive(false);
        isPowerUped = false;
        typeOfPowerUp = "None";
        powerUpDelegate = null;
    }
}
