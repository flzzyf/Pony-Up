using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Text scoreText;
    string scoreTextText;

    public GameObject twilight;
    Vector2 twilightOriginPos;

    [HideInInspector]
    public bool gaming;
    float score;

    public GameObject startingPanel;
    public GameObject groundObject;

    void Start()
    {
        scoreTextText = scoreText.text;
        twilightOriginPos = twilight.transform.position;

        Init();
    }

    void Update()
    {
        if (gaming)
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

        startingPanel.SetActive(false);

        LevelManager.Instance().StartLevel();

        CloudManager.Instance().StartGenerate();
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
        startingPanel.SetActive(true);

        LevelManager.Instance().ClearLevel();

        groundObject.GetComponent<WorldObject>().Reset();

    }

    public void GameRestart()
    {
        GameReset();

        GameStart();
    }
}
