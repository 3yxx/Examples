using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    private float horizontalInput;
  
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal1");
        transform.Rotate(Vector3.up,horizontalInput * Time.deltaTime * rotationSpeed);        
    }
}
