using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    private int playerLayer, ammoLayer;
    private UnityEvent glitchEvent;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: refactor
        glitchEvent = GameObject.Find("Player").GetComponent<Player>().glitchEvent;

        playerLayer = LayerMask.NameToLayer("Player");
        ammoLayer = LayerMask.NameToLayer("Ammo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerLayer ||
            collision.gameObject.layer == ammoLayer)
        {
            Destroy(gameObject);
            glitchEvent.Invoke();
        }
    }
}
