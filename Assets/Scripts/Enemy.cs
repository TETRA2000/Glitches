using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float minMoveSpeed;
    public float maxMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.Normalize(player.transform.position - transform.position);
        transform.position += Random.Range(minMoveSpeed, maxMoveSpeed) * direction * Time.deltaTime;
    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}
