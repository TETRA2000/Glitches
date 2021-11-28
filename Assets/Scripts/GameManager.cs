using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject scoreGameObject;
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreGameObject.GetComponent<Text>();
        scoreText.text = "SCORES: 9999";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
