using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Tooltip("The speed at which barriers and coins move towards the player")]
    [SerializeField] private float elementMovementSpeed = 10f;
    public float ElementMovementSpeed { get { return elementMovementSpeed; } set { elementMovementSpeed = value; } }
    
    [Header("Element Score Values")]
    [SerializeField] private int barrierScoreValue = 10;
    [SerializeField] private int coinScoreValue = 100;

    [Header("Scoring Reference Cache")]
    [SerializeField] private int playerScore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;

    private int startScore = 0;

    public int PlayerScore { get { return playerScore; } }

    // Start is called before the first frame update
    void Awake()
    {
        ResetScore();
        UpdatePlayerScore();
    }

    private void ResetScore()
    {
        playerScore = startScore;
        scoreText.text = playerScore.ToString();
    }

    public void BarrierPassScoreIncrease()
    {
        playerScore += barrierScoreValue;
        UpdatePlayerScore();
    }

    public void CoinScoreIncrease()
    {
        playerScore += coinScoreValue;
        UpdatePlayerScore();
    }

    private void UpdatePlayerScore()
    {
        scoreText.text = playerScore.ToString();
        endScoreText.text = "Your Score:\n" + playerScore.ToString();
        Debug.Log("Method called");
    }
}
