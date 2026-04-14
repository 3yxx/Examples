using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Target : MonoBehaviour
{
    public int score;
    private float minForce = 14f;
    private float maxForce = 18f;
    private float maxTorque = 1.5f;
    private float xRange = 4f;
    private float ySpawnPos = -6f;

    public ParticleSystem destroyParticle;
    private Rigidbody objectRb;
    private Vector3 torqueVector;
    private Spawner spawner;

    private void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        torqueVector = GenerateTorque();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

        ThrowObject();
    }

    private void Update()
    {
        if (transform.position.y < -8)
        {
            if(tag == "Good")
            {
                spawner.MinusHealth();
            }

            Destroy(gameObject);
        }
        
    }

    // private void OnMouseDown()
    // {
    //     if(tag == "Good")
    //     {
    //         spawner.AddScore(score);
    //     }
    //     else if (tag == "Bad")
    //     {
    //        spawner.MinusHealth();
    //     }

    //     ParticleSystem particle = Instantiate(destroyParticle,transform.position,transform.rotation);
        
    //     Destroy(gameObject);
    //     Destroy(particle,1f);

    // }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);    
    }

    private void OnDestroy()
    {
        ParticleSystem particle = Instantiate(destroyParticle,transform.position,transform.rotation);
        Destroy(particle,1f);
    }

    private void ThrowObject()
    {
        objectRb.AddForce(Vector3.up * GenerateForce(), ForceMode.Impulse);
        objectRb.AddTorque(torqueVector,ForceMode.Impulse);
        transform.position = RandomizePosition();
    }

    private float GenerateForce()
    {
        return Random.Range(minForce,maxForce);
    }

    private Vector3 GenerateTorque()
    {
        return new Vector3(Random.Range(-maxTorque,maxTorque),Random.Range(-maxTorque,maxTorque),Random.Range(-maxTorque,maxTorque));
    }

    private Vector3 RandomizePosition()
    {
        return new Vector3(Random.Range(-xRange,xRange),ySpawnPos);
    }
}
