using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private List<string> sceneList = new List<string>();

    private static SceneLoader _instance = null;
    public static SceneLoader Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (_instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        foreach (var scene in sceneList)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
