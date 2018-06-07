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

    public GameObject twilight;
    Vector2 twilightOriginPos;

    public GameObject startButton;
    [HideInInspector]
    public bool gaming;
    float score;

    LevelManager levelManager;
    [HideInInspector]
    public CaneController caneController;

	void Start () 
	{
        levelManager = GetComponent<LevelManager>();
        caneController = GetComponent<CaneController>();

        scoreTextText = scoreText.text;
        twilightOriginPos = twilight.transform.position;

        Init();

	}

	void Update () 
	{
        if(gaming)
        {
            score += Time.deltaTime;
            scoreText.text = scoreTextText + (score * 10).ToString("F0");
        }

        if (!Application.isFocused)
        {
            //手机上退出到后台
            Application.Quit();
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

        levelManager.StartLevel();
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        gaming = false;

        StartCoroutine(GameOverAnim());

    }

    IEnumerator GameOverAnim()
    {
        yield return new WaitForSeconds(1f);
        GameReset();

    }

    public void GameReset()
    {
        twilight.transform.position = twilightOriginPos;
        twilight.GetComponent<TwilightSparkle>().Rebirth();

        Init();
        startButton.SetActive(true);

        levelManager.ClearLevel();

    }

    public void GameRestart()
    {
        GameReset();

        GameStart();
    }
}
