using UnityEngine;
using UnityEditor;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() // Runs slightly before start - allows playing of Sounds in Start method.
    {
        if (instance == null)
        {
            instance = this;
        }// Ensures that there is an instance of he Audio Manager instanciated.
        else
        {
            Destroy(gameObject);
            return;
        }// If there already an instance the new one is Destroyed.

        DontDestroyOnLoad(gameObject); // This stops the audio manager being destroyed between scenes.

        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            // Parses the sounds in the array and assigns their volume, pitch and loop variables to ones that 
            // Can be interacted with in the inspector.
        }
    }

    void Start() // What to play at the start of the game. - Do this in MenuScreenController.
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the corresponding sound's name in sounds array.
        if (s == null)
        {
            EditorUtility.DisplayDialog("Sound Load Error" ,
                "Sound: " + name  + " was not loaded correctly! "
                + "\n Check name spellings.", "OK");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the corresponding sound's name in sounds array.
        if (s == null)
        {
            EditorUtility.DisplayDialog("Sound Stop Error",
                "Sound: " + name + " was not stopped correctly! "
                + "\n Check name spellings.", "OK");
            return;
        }
        s.source.Stop();
    }
}
