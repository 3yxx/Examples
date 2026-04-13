using System.Collections;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
   
    void Update()
    {
        transform.Rotate(Vector3.up);
        StartCoroutine(PowerTimeEnd());
    }

    IEnumerator PowerTimeEnd()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
