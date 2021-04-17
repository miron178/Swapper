using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{


    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    void Start()
    {
        Play("BackgroundMusic");

        Scene currentScene = SceneManager.GetActiveScene();
        string SceneName = currentScene.name;

        if (SceneName == "EndScreen")
        {
            StopPlaying("SlimeWalk");
            StopPlaying("BigWalk");
            StopPlaying("ArmourWalk");
            StopPlaying("SmallWalk");
            StopPlaying("BigChicken");
        }
        else if (SceneName == "MainMenu")
        {
            StopPlaying("SlimeWalk");
            StopPlaying("BigWalk");
            StopPlaying("ArmourWalk");
            StopPlaying("SmallWalk");
            StopPlaying("BigChicken");
        }

    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

   
    //code to make the sound play
    //FindObjectOfType<AudioManager>().Play("NameOfAudio");
}
