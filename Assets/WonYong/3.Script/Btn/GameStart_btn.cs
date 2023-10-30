using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart_btn : MonoBehaviour
{
    private Gamemanager gamemanager;

    private void Awake()
    {
        gamemanager = FindObjectOfType<Gamemanager>();
    }

    public void Start_MainGame_btn()
    {
        GameObject[] gameObjectsInMainScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in gameObjectsInMainScene)
        {
            Destroy(obj);
        }

        gamemanager.SaveBestScores();
        gamemanager.LoadBestScores();

        SceneLoader.Instance.UnloadScenes();
        SceneLoader.Instance.Start_Game();
    }

}
