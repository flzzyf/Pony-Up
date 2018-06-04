using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    #region Singleton
    public static GameManager instance;
	private void Awake()
	{
        if (instance == null)
            instance = this;
        else
            Destroy(this);
	}
#endregion

    public Text scoreText;
    string scoreTextText;

    public Transform twilight;
    Vector2 twilightOriginPos;

    public GameObject startButton;
    [HideInInspector]
    public bool gaming;
    float score;

	void Start () 
	{
        scoreTextText = scoreText.text;
        twilightOriginPos = twilight.position;

        Init();

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
        scoreText.text = scoreTextText + "0";
    }

    public void GameStart()
    {
        gaming = true;

        startButton.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        gaming = false;
    }

    public void GameReset()
    {
        twilight.position = twilightOriginPos;
        Init();
        startButton.SetActive(true);

    }

    public void GameRestart()
    {
        GameReset();

        GameStart();
    }
}
