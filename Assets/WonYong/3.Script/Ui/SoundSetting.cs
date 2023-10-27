using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    public GameObject SoundUI;
    private bool isSound = false;
    private void Awake()
    {
        SoundUI.SetActive(false);
    }
    public void onClick_SoundUI_Btn()
    {
        SoundUI.SetActive(!isSound);
        isSound = !isSound;
    }
}
