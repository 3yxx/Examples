using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public List<GameObject> targets;

    public bool isGameOver = false;
    private bool isGameActive = false;
    private int score;
    private int health;
    private float spawnRate = 1.5f;



    public void StartGame(float spawnRate, int health)
    {
        this.health = health;
        this.spawnRate /= spawnRate;
        isGameActive = true;
        healthText.text =$"Score: {health}";

        gameObject.SetActive(true);
        StartCoroutine(SpawnCooldown());
    }

    private void Update()
    {
        if(health == 0)
        {
            ShowGameOver();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            isGameActive = false;

            pauseUI.SetActive(true);
        }

        if(!isGameActive && Input.anyKeyDown)
        {
            isGameActive = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void MinusHealth()
    {
        healthText.text = $"Score: {--health}";
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        isGameOver = true;
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnCooldown()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);   
        }
    }

}
