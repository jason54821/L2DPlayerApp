using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2DSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] soundData;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySequence1()
    {
        audioSource.clip = soundData[0];
        audioSource.Play();
    }

    public void PlaySequence2()
    {
        audioSource.clip = soundData[1];
        audioSource.Play();
    }

    public void PlaySequence3()
    {
        audioSource.clip = soundData[2];
        audioSource.Play();
    }

    public void PlaySequence4()
    {
        audioSource.clip = soundData[3];
        audioSource.Play();
    }

    public void PlaySequence5()
    {
        audioSource.clip = soundData[4];
        audioSource.Play();
    }

}
