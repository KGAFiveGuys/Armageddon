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

    private PlayerController playerController;
	private void Start()
	{
        playerController = FindObjectOfType<PlayerController>();
    }

	private void Update()
    {
		if (!playerController.isDie)
		{
            score += Time.deltaTime;
            Score.text = $"Score: {Mathf.FloorToInt(score),3}";
        }
		else
		{
            Best.text = $"{bestScore,3}";
            Your.text = $"{Mathf.FloorToInt(score),3}";
        }
    }

    public void ReStartGame()
    {
		if (score > bestScore)
		{
			bestScore = (int)score;
			PlayerPrefs.SetInt(Key, bestScore);
			PlayerPrefs.Save();
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Dead_UI_On()
    {
        DeadUI.SetActive(true);
    }
}
