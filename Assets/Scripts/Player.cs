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

    public GameObject explosionPrefab;

    public UnityEvent glitchEvent;

    private int enemyLayer;

    private bool died = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(!died)
        {
            float horizontalInput = -1 * Input.GetAxis("Horizontal");
            float verticalInput = -1 * Input.GetAxis("Vertical");

            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

            if (Input.GetKeyDown("space"))
            {
                audioSource.Play();

                foreach (var muzzle in muzzles)
                {
                    var fire = Instantiate(fireball);
                    fire.transform.position = muzzle.position;
                }
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(!died) ResetPlayerPosition();
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
            died = true;

            var explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;

            gameObject.SetActive(false);
            Destroy(explosion, 0.5f);
            Invoke("MoveToGameOverScene", 0.5f);
        }
    }

    private void MoveToGameOverScene()
    {
        SceneManager.LoadScene("Assets/Scenes/GameOverScene.unity");
    }

}
