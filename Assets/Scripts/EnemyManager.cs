using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyGameObject;
    public float minSpawnInterval;
    public float maxSpawnInterval;

    private float nextSpawnInteval;
    private float lastSpawnTime;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.timeSinceLevelLoad;
        nextSpawnInteval = GetNextSpawnInterval();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - lastSpawnTime > nextSpawnInteval)
        {
            var enemy = Instantiate(enemyGameObject);
            enemy.GetComponent<Enemy>().player = player;

            lastSpawnTime = Time.timeSinceLevelLoad;
            nextSpawnInteval = GetNextSpawnInterval();
        }
    }

    private float GetNextSpawnInterval()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
