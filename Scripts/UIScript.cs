using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
