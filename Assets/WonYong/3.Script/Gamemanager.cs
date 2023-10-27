using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public PlayerPrefs playerPrefs;

    [SerializeField] private Text Score;
    [SerializeField] private Text Best_Score;

    private string Key = "Best_Score";
    private float score = 0;
    private int bestScore = 0;



    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt(Key, bestScore);
        Best_Score.text = $"Best : {bestScore}";
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        score += Time.deltaTime;
        Score.text = $"Score: {score:F0}";

        if (score > bestScore)
        {
            bestScore = (int)score;
            PlayerPrefs.SetInt(Key, bestScore);
            PlayerPrefs.Save();
        }

    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
