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
    [SerializeField] private Text Best_Score1;
    
    [Header("죽었을때 띄워줄 점수")]
    [SerializeField] private Text Best1;
    [SerializeField] private Text Best2;
    [SerializeField] private Text Best3;
    [SerializeField] private Text Your;

    [Header("죽은후 띄워줄 UI오브젝트")]
    [SerializeField] private GameObject DeadUI;
    //todo : 캐릭터 죽는거 완료되면 , DeadUI 활성화 해줄것 

    private string Key1 = "Best_Score1";
    private string Key2 = "Best_Score2";
    private string Key3 = "Best_Score3";
    private float score = 0;
    private int bestScore1 = 0;
    private int bestScore2 = 0;
    private int bestScore3 = 0;

    private PlayerController playerController;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    
	private void Start()
    {
        LoadBestScores();
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            gameObject.SetActive(true);
        }
    }

    public void LoadBestScores()
    {
        bestScore1 = PlayerPrefs.GetInt(Key1, bestScore1);
        bestScore2 = PlayerPrefs.GetInt(Key2, bestScore2);
        bestScore3 = PlayerPrefs.GetInt(Key3, bestScore3);
        Best_Score1.text = $"Best1 : {bestScore1}";
    }

    private void Update()
    {
		if (!playerController.isDie)
		{
            score += Time.deltaTime;
            Score.text = $"Score: {Mathf.FloorToInt(score),3}";

            if (score > bestScore1)
                Best1.text = $"{score,3}";
        }
		else
		{
            Best1.text = $"{bestScore1,3}";
            Best2.text = $"{bestScore2,3}";
            Best3.text = $"{bestScore3,3}";
            Your.text = $"{Mathf.FloorToInt(score),3}";
        }
    }

    private void ReStartGame()
    {
        SaveBestScores();
        LoadBestScores();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveBestScores()
    {
        var scoreList = new List<int>();
        scoreList.Add(bestScore1);
        scoreList.Add(bestScore2);
        scoreList.Add(bestScore3);

        scoreList.Sort((a,b) => b.CompareTo(a));

        if (score > scoreList[0])
            scoreList[0] = (int)score;
        else if (score > scoreList[1])
            scoreList[1] = (int)score;
        else if (score > scoreList[2])
            scoreList[2] = (int)score;

        PlayerPrefs.SetInt(Key1, scoreList[0]);
        PlayerPrefs.SetInt(Key2, scoreList[1]);
        PlayerPrefs.SetInt(Key3, scoreList[2]);
        PlayerPrefs.Save();
    }

    public void Dead_UI_On()
    {
        DeadUI.SetActive(true);
    }
}
