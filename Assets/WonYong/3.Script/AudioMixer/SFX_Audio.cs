using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Audio : MonoBehaviour
{
    public static SFX_Audio instance = null;
    public AudioSource audioSource;
    //----audioclip //
    public AudioClip Jump;
    public AudioClip BoomFalling;
    public AudioClip BoomDestory;
    public AudioClip BoomDestory_special;
    public AudioClip Die;

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
    public void Play_BoomDestory_Sound()
    {
        audioSource.PlayOneShot(BoomDestory);
    }

    

    public void Play_BoomDestory_special_Sound()
    {
        audioSource.PlayOneShot(BoomDestory_special);
    }

    public  void Play_Boon_Sound()
    {
        audioSource.PlayOneShot(BoomFalling, 3f);
    }




    public void Play_Die_Sound()
    {
        audioSource.PlayOneShot(Die);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SFX_Audio.instance.Play_Jump_Sound();
        }


    }
}
