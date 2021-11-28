using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float minMoveSpeed;
    public float maxMoveSpeed;

    public GameObject gameManager;
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.Normalize(player.transform.position - transform.position);
        transform.position += Random.Range(minMoveSpeed, maxMoveSpeed) * direction * Time.deltaTime;
    }

    public void Damage()
    {
        gameManagerScript.OnEnemyKilled();

        Destroy(gameObject);
    }

    public void OnGlitch()
    {
        if (player != null)
        {
            var pos = transform.position;

            int count = 0;
            do
            {
                pos.x = Random.Range(pos.x - 3f, pos.x + 3f);
                pos.y = Random.Range(pos.y - 3f, pos.y + 3f);

                count++;
            } while (!(count > 10 || Vector3.Distance(pos, player.transform.position) > 4f));

            transform.position = pos;
        }
    }
}
