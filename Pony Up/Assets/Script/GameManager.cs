using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;
	private void Awake()
	{
        if (instance == null)
            instance = this;
        else
            Destroy(this);
	}

	public Text scoreText;
    string scoreTextText;

    public Transform twilight;

    bool gaming;
    float score;

	void Start () 
	{
        scoreTextText = scoreText.text;

        Init();

        //测试
        GameStart();
	}

	void Update () 
	{
        if(gaming)
        {
            score += Time.deltaTime;
            scoreText.text = scoreTextText + (score * 10).ToString("F0");
        }
	}

    void Init()
    {
        score = 0;
    }

    void GameStart()
    {
        gaming = true;
    }
}
