using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private static SoundManager instance;
    public List<AudioSource> list;

    public static SoundManager Instance()
    {
        if (!instance)
            instance = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        return instance;
    }

    public void PlayMusic(int num)
    {
        foreach (AudioSource audioSource in list)
            audioSource.Stop();
        list[num].Play();
    }
}
