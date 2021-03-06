﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// UIのViewへの参照
    /// </summary>
    [SerializeField]
    private ScoreViewer scoreViewer = null;

    [SerializeField]
    private ScoreViewer highScoreViewer = null;

    [SerializeField]
    private RemainPlayerViewer remainPlayaerViewer = null;

    [SerializeField]
    private GameOverViewer gameOverText = null;

    public void SetScore(int score)
    {
        scoreViewer.SetScore(score);
    }

    public void SetHighScore(int score)
    {
        highScoreViewer.SetScore(score);
    }

    public void SetHeart(int heart)
    {
        remainPlayaerViewer.SetRemain(heart);
    }

    public void AppearGameOver()
    {
        gameOverText.ShowGameOver();
    }
}
