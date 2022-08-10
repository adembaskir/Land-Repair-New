using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public  Text scoreText;

    public void Start()
    {
        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            score = PlayerPrefs.GetInt("CoinAmount", score);
        }
    }
    private void Update()
    {
        scoreText.text = score.ToString();
    }
}
