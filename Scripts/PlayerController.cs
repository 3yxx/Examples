using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  
    public bool gameOver;
    public float gravityModifier;

    public AudioClip deathSound;
    public AudioClip jumpSound;
    public ParticleSystem deathParticle;
    public ParticleSystem runParticle;
    [SerializeField] GameObject gameOverCanvas;

    private float jumpForce = 670;
    private bool isOnGround;
    private Rigidbody playerRigid;
    private Animator animator;
    private AudioSource playerSounds;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerSounds = GetComponent<AudioSource>();


        isOnGround = true;
        gameOver = false;
        
        //After restarting multiplue again
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRigid.AddForce(Vector3.up  * jumpForce,ForceMode.Impulse);
            playerSounds.PlayOneShot(jumpSound,1.0f);
            isOnGround = false;

            animator.SetTrigger("Jump_trig");
            runParticle.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            runParticle.Play();
        }
        else if(collision.gameObject.tag == "Obstacles")
        {
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int",1);

            playerSounds.PlayOneShot(deathSound,1.2f);

            deathParticle.Play();
            runParticle.gameObject.SetActive(false);

            gameOverCanvas.SetActive(true);
            gameOver = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Prototype 3");
    }
    
}
