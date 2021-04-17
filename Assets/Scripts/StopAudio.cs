using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().StopPlaying("SlimeWalk");
        FindObjectOfType<AudioManager>().StopPlaying("BigWalk");
        FindObjectOfType<AudioManager>().StopPlaying("ArmourWalk");
        FindObjectOfType<AudioManager>().StopPlaying("SmallWalk");
        FindObjectOfType<AudioManager>().StopPlaying("BigChicken");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
