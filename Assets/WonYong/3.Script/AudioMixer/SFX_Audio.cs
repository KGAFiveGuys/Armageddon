using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Audio : MonoBehaviour
{
    public static SFX_Audio instance = null;
    public AudioSource audioSource;
    //----audioclip //
    public AudioClip Jump;
    public AudioClip ¿î¼®;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void Play_Jump_Sound()
    {
        audioSource.PlayOneShot(Jump);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SFX_Audio.instance.Play_Jump_Sound();
        }


    }
}
