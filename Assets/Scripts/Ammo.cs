using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float speed = 10f;
    private int enemyLayer;

    public GameObject ExplosionPrefav;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == enemyLayer)
        {
            var explosion = Instantiate(ExplosionPrefav);
            Destroy(explosion, 2.0f);

            collision.gameObject.GetComponent<Enemy>().Damage();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(transform.position + -1 * transform.up * speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
