using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Vector2 spawnPointRangeAbs;

    public float minSpawnInterval;
    public float maxSpawnInterval;

    private float nextSpawnInteval;
    private float lastSpawnTime;

    public GameObject[] itemPrefabs;

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
            SpawnItem();

            lastSpawnTime = Time.timeSinceLevelLoad;
            nextSpawnInteval = GetNextSpawnInterval();
        }
    }

    private float GetNextSpawnInterval()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void SpawnItem()
    {
        var x = Random.Range(-1 * spawnPointRangeAbs.x, spawnPointRangeAbs.x);
        var y = Random.Range(-1 * spawnPointRangeAbs.y, spawnPointRangeAbs.y);

        var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        var item = Instantiate(prefab);
        item.transform.position = new Vector3(x, y, 0);
    }
}
