using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float score;
    private int enemiesAlive = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UIManager.Instance.UpdateScore(0);
    }

    public void EnemySpawned()
    {
        enemiesAlive++;
    }

    public void EnemyKilled(Enemy enemy)
    {
        UpdateScore(enemy.GetPower());
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void UpdateScore(float value)
    {
        score += value;
        UIManager.Instance.UpdateScore(score);
    }

}