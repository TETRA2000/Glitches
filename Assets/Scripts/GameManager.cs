using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariable.scores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemyKilled()
    {
        GlobalVariable.scores += 10;
    }
}
