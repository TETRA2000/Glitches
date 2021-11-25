using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMasterVolume : MonoBehaviour
{
    public float masterVolume;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = masterVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
