using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    public static DestroySound instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private AudioClip[] Clips;
    [SerializeField] private AudioSource[] AudioSources;


    public void PlayDestroySound(int index) // 0 Default 1 Special
    {
        for (int i = 0; i < AudioSources.Length; i++)
        {
            if (!AudioSources[i].isPlaying)
            {
                AudioSources[i].clip = Clips[index];
                AudioSources[i].Play();
                break;
            }
        }
    }


}
