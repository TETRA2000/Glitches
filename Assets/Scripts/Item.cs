using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    private int playerLayer, ammoLayer;
    private UnityEvent glitchEvent;

    public enum GlitchMode { Normal, DisableGuns }
    public GlitchMode glitchMode;

    private float spawnTime;

    private SpriteRenderer spriteRenderer;

    private bool acitivated = false;
    private float activationWaitTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: refactor
        glitchEvent = GameObject.Find("Player").GetComponent<Player>().glitchEvent;

        playerLayer = LayerMask.NameToLayer("Player");
        ammoLayer = LayerMask.NameToLayer("Ammo");

        spriteRenderer = GetComponent<SpriteRenderer>();

        spawnTime = Time.time;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!acitivated && Time.time - spawnTime > activationWaitTime)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            acitivated = true;
        }    
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - spawnTime < activationWaitTime) return;


        if(collision.gameObject.layer == playerLayer ||
            collision.gameObject.layer == ammoLayer)
        {
            Destroy(gameObject);
            ActivateGlitch();
        }
    }

    private void ActivateGlitch()
    {
        switch(glitchMode)
        {
            case GlitchMode.Normal:
                glitchEvent.Invoke();
                break;
            case GlitchMode.DisableGuns:
                break;
        }
    }
}
