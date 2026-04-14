using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Spawner spawner;
    public float spawnRate;
    public int health;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        spawner.StartGame(spawnRate,health);
    }
    
}
