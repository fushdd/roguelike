using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    private List<Transform> spawnPointList;
    public float enemyCount;
    public GameObject enemy;

    private void Awake()
    {
        spawnPointList = new();
        
        // collect spawn points
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPointList.Add(transform.GetChild(i));
        }

        // spawn enemies at random spawn points
        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(0, spawnPointList.Count);
            Instantiate(enemy, spawnPointList[randomIndex].position, Quaternion.identity);
            spawnPointList.RemoveAt(randomIndex);
        }

    }

}
