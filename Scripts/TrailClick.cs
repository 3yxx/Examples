using UnityEngine;

public class TrailClick : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Spawner spawner;
    private Vector3 mousePos;
    private BoxCollider boxCollider;
    private TrailRenderer trail;
    private bool swiping;

    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        
        trail.enabled = false;
        boxCollider.enabled = false;
    }

    void Update()
    {
        if (!spawner.isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Good")
        {
            spawner.AddScore(collision.gameObject.GetComponent<Target>().score);
        }
        else if (collision.gameObject.tag == "Bad")
        {
            spawner.MinusHealth();
        }

        Destroy(collision.gameObject);
    }


    private void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
        transform.position = mousePos;
    }

     private void UpdateComponents()
    {
        trail.enabled = swiping;
        boxCollider.enabled = swiping;
    }
}
