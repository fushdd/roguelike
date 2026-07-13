using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    private List<Transform> spawnPointList;

    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float floorPower;

    private void Start()
    {
        spawnPointList = new();

        // collect spawn points
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPointList.Add(transform.GetChild(i));
        }

        // spawn enemies at random spawn points
        while (floorPower > 0)
        {
            int randomEnemyIndex = Random.Range(0, enemies.Count);
            Enemy enemy = enemies[randomEnemyIndex].GetComponent<Enemy>();
            float enemyPower = enemy.GetPower();
            // if chosen enemy is too strong, remove from the list and try again
            if (enemyPower > floorPower)
            {
                enemies.RemoveAt(randomEnemyIndex);
                continue;
            }

            // choose random spawn point
            int randomSpawnPointIndex = Random.Range(0, spawnPointList.Count);

            Instantiate(enemy, spawnPointList[randomSpawnPointIndex].position, Quaternion.identity);
            floorPower -= enemyPower;
            spawnPointList.RemoveAt(randomSpawnPointIndex);
            // increment enemy count
            GameManager.Instance.EnemySpawned();
        }

    }


    private void Update()
    {

    }

}
