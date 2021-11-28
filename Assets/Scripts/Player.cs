using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject fireball;
    public Transform[] muzzles;
    public float moveSpeed = 10f;

    public Vector2 spawnPointRangeAbs;

    private AudioSource audioSource;

    public UnityEvent glitchEvent;

    private int enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = -1 * Input.GetAxis("Horizontal");
        float verticalInput = -1 * Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

        if(Input.GetKeyDown("space"))
        {
            audioSource.Play();

            foreach(var muzzle in muzzles )
            {
                var fire = Instantiate(fireball);
                fire.transform.position = muzzle.position;
            }
        }
    }

    private void OnBecameInvisible()
    {
        ResetPlayerPosition();
    }

    private void ResetPlayerPosition()
    {
        var x = Random.Range(-1 * spawnPointRangeAbs.x, spawnPointRangeAbs.x);
        var y = Random.Range(-1 * spawnPointRangeAbs.y, spawnPointRangeAbs.y);

        transform.position = new Vector3(x, y, 0);

        glitchEvent.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            SceneManager.LoadScene("Assets/Scenes/GameOverScene.unity");
        }
    }

}
