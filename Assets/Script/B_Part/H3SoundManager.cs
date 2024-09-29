using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Demo_B用サウンドマネージャー
/// </summary>
public class H3SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] soundData;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(string name)
    {
        if(name == "idle_dia")
        {
            audioSource.clip = soundData[0];
            audioSource.Play();
            audioSource.loop = false;
        }
        
        if(name == "loop1_dia")
        {
            audioSource.clip = soundData[1];
            audioSource.Play();
            audioSource.loop = false;
        }

        if (name == "loop2_dia")
        {
            audioSource.clip = soundData[2];
            audioSource.Play();
            audioSource.loop = false;
        }

        if (name == "shoot_dia")
        {
            audioSource.clip = soundData[3];
            audioSource.Play();
            audioSource.loop = false;
        }

        if (name == "loop1_vo")
        {
            audioSource.clip = soundData[4];
            audioSource.Play();
            audioSource.loop = true;
        }

        if (name == "loop2_vo")
        {
            audioSource.clip = soundData[5];
            audioSource.Play();
            audioSource.loop = true;
        }

        if (name == "idle_vo")
        {
            audioSource.clip = soundData[6];
            audioSource.Play();
            audioSource.loop = true;
        }


    }

    public void Stop()
    {
        audioSource.Stop();
    }

}
