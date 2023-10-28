using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart_btn : MonoBehaviour
{
    public void Start_MainGame_btn()
    {
        GameObject[] gameObjectsInMainScene = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in gameObjectsInMainScene)
        {
            Destroy(obj);
        }

        SceneLoader.Instance.UnloadScenes();
        SceneLoader.Instance.Start_Game();
    }

}
