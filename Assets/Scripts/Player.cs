using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject fireball;
    public Transform[] muzzles;
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = -1 * Input.GetAxis("Horizontal");
        float verticalInput = -1 * Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

        if(Input.GetKeyDown("space"))
        {
            foreach(var muzzle in muzzles )
            {
                var fire = Instantiate(fireball);
                fire.transform.position = muzzle.position;
            }
        }
    }
}
