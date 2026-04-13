using System;
using UnityEngine;

public class ObstaclesMove : MonoBehaviour
{
    private float speed; 
    private PlayerController playerController;
    [SerializeField] private float yPos;
    [SerializeField] private bool isRotating;

    void Start()
    {
        speed = 15f;    
        transform.position = new Vector3(transform.position.x,yPos,transform.position.z);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            // if (isRotating)
            // {
            //     transform.Rotate(0,5,0);
            // }
            CheckOutOfBounds();
        }
    }

    void CheckOutOfBounds()
    {
        if(transform.position.x < -13)
        {
            Destroy(gameObject);
        }        
    }
}
