using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private GameManager playerScore;
    [SerializeField] private AudioManager audioManagerScript;

    private void Awake()
    {
        playerScore = FindObjectOfType<GameManager>();
        audioManagerScript = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScore.BarrierPassScoreIncrease();
            audioManagerScript.PlaySound("BarrierPass");
        }
    }
}
