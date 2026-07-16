using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private UpgradeManager upgradeManager;

    private float score;
    private int enemiesAlive = 0;
    public bool isUpgrading { get; private set; } // ??
    public int floorPower { get; private set; } // ??
    public bool floorCleared = false; // ??

    // buffs
    public float speedMultiplier { get; private set; }
    public void UpdateSpeedMultiplier(float value) => speedMultiplier += value;

    public float damageMultiplier { get; private set; }
    public void UpdateDamageMultiplier(float value) => damageMultiplier += value;

    public float bulletLifespanMultiplier { get; private set; }
    public void UpdateBulletLifespanMultiplier(float value) => bulletLifespanMultiplier += value;

    public float bulletSpeedMultiplier { get; private set; }
    public void UpdateBulletSpeedMultiplier(float value) => bulletSpeedMultiplier += value;

    public float attackCooldownMultiplier { get; private set; }
    public void UpdateAttackCooldownMultiplier(float value) => attackCooldownMultiplier += value;

    public float doubleScoreChance { get; private set; }
    public void UpdateDoubleScoreChance(float value) => doubleScoreChance += value;



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
        floorPower = 1;
        score = 0;
        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateFloor(floorPower);
    }

    public void EnemySpawned()
    {
        enemiesAlive++;
    }

    public void EnemyKilled(Enemy enemy)
    {
        // roll for double score
        int scoreMultiplier = 1;

        if (doubleScoreChance <= 0)
        {
            scoreMultiplier = 1;
        }
        else if (doubleScoreChance >= 1)
        {
            scoreMultiplier = 2;
        }
        else if (Random.value < doubleScoreChance)
        {
            scoreMultiplier = 2;
        }

        UpdateScore(enemy.GetPower() * scoreMultiplier);

        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            floorCleared = true;
            UIManager.Instance.ShowFloorEndMessage();
        }
    }

    public void InitiateUpgradeChoice()
    {
        if (isUpgrading) return;

        UIManager.Instance.HideFloorEndMessage();
        isUpgrading = true;
        upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        upgradeManager.StartUpgrading();
    }

    public void InitiateNewFloor()
    {
        floorPower += 1;
        UIManager.Instance.UpdateFloor(floorPower);

        isUpgrading = false;
        floorCleared = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(float value)
    {
        score += value;
        UIManager.Instance.UpdateScore(score);
    }

}