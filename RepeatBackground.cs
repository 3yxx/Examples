using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private float speed = 15f;
    private float repeatWidth;
    private PlayerController playerController;
    private Vector3 startPos;



    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x/2;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);

            if(transform.position.x < startPos.x - repeatWidth)
            {
                transform.position = startPos;
            }
        }
    }
}
