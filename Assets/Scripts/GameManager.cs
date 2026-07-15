using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private UpgradeManager upgradeManager;

    private float score;
    private int enemiesAlive = 0;
    public bool isUpgrading { get; private set; } // ??
    public float floorPower { get; private set; } // ??
    public bool floorCleared = false; // ??

    // buffs
    public void UpdateSpeedMultiplier(float value) => speedMultiplier += value;
    public float speedMultiplier { get; private set; }


    public void UpdateDamageMultiplier(float value) => damageMultiplier += value;
    public float damageMultiplier { get; private set; }


    public void UpdateBulletLifespanMultiplier(float value) => bulletLifespanMultiplier += value;
    public float bulletLifespanMultiplier { get; private set; }


    public void UpdateBulletSpeedMultiplier(float value) => bulletSpeedMultiplier += value;
    public float bulletSpeedMultiplier { get; private set; }


    public void UpdateAttackCooldownMultiplier(float value) => attackCooldownMultiplier += value;
    public float attackCooldownMultiplier { get; private set; }


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
        floorPower = 1;
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

    public void InitiateNewScene()
    {
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