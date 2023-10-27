using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart_btn : MonoBehaviour
{
    public GameObject Gamemanager;
    public GameObject Score;
    public GameObject Best;
    public void Start_MainGame_btn()
    {
        SceneManager.LoadScene("MainScene");
        Gamemanager.SetActive(true);
        Score.SetActive(true);
        Best.SetActive(true);
        Destroy(gameObject);
    }
}
