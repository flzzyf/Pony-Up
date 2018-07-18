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

    public float globalGravity = 0.15f;

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
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }

    void Init()
    {
        score = 0;
        scoreText.text = scoreTextText + "0";

        LevelGenerator.Instance().firstLevel = true;
    }

    public void GameStart()
    {
        gaming = true;

        Init();

        startingPanel.SetActive(false);

        LevelManager.Instance().StartLevel();

        CloudManager.Instance().StartGenerateCloud();

        SoundManager.Instance().Play("BGM");
    }

    public void GameOver()
    {
        gaming = false;

        StartCoroutine(GameOverAnim());
        CloudManager.Instance().StopGenerateCloud();

        SoundManager.Instance().StopPlay("BGM", 1f);
        SoundManager.Instance().Play("GameOver");
        SoundManager.Instance().Play("Hit");

    }

    IEnumerator GameOverAnim()
    {
        yield return new WaitForSeconds(1.6f);
        GameReset();

    }

    public void GameReset()
    {
        twilight.transform.position = twilightOriginPos;
        twilight.GetComponent<TwilightSparkle>().Rebirth();

        startingPanel.SetActive(true);

        LevelManager.Instance().ClearLevel();

        groundObject.GetComponent<WorldObject>().Reset();

        CloudManager.Instance().ClearCloud();
    }

    public void GameRestart()
    {
        GameReset();

        GameStart();
    }

    public void IEnumeratorTrigger(IEnumerator _ie)
    {
        StartCoroutine(_ie);
    }
}
