using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public PlayerPrefs playerPrefs;
    [Header("게임 진행중 띄워줄 점수")]
    [SerializeField] private Text Score;
    [SerializeField] private Text Best_Score;

    [Header("죽었을때 띄워줄 점수")]
    [SerializeField] private Text Best;
    [SerializeField] private Text Your;

    [Header("죽은후 띄워줄 UI오브젝트")]
    [SerializeField] private GameObject DeadUI;
    //todo : 캐릭터 죽는거 완료되면 , DeadUI 활성화 해줄것 


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
        Score.text = $"Score: {Mathf.FloorToInt(score).ToString()}";

        if (score > bestScore)
        {
            bestScore = (int)score;
            PlayerPrefs.SetInt(Key, bestScore);
            PlayerPrefs.Save();
        }

        Best.text = bestScore.ToString();
        Your.text = Mathf.FloorToInt(score).ToString();

    }
    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Dead_UI_On()
    {
        DeadUI.SetActive(true);
    }
}
