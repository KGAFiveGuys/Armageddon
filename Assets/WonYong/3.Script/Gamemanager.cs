using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public PlayerPrefs playerPrefs;
    [Header("���� ������ ����� ����")]
    [SerializeField] private Text Score;
    [SerializeField] private Text Best_Score;

    [Header("�׾����� ����� ����")]
    [SerializeField] private Text Best;
    [SerializeField] private Text Your;

    [Header("������ ����� UI������Ʈ")]
    [SerializeField] private GameObject DeadUI;
    //todo : ĳ���� �״°� �Ϸ�Ǹ� , DeadUI Ȱ��ȭ ���ٰ� 


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
