using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMover : MonoBehaviour
{
    private Vector3 coinStartingPos; 
    private float coinYPos;
    private float coinXPos;
    private float barrierDistance;
    public float BarrierDistance { set { barrierDistance = value; } }

    // Script References
    private GameManager gameManagerScript;
    private CoinObjectPool coinObjectPoolScript;
    private AudioManager audioManagerScript;

    private void Awake()
    {
        LocateScriptReferences();
        ResetCoinPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManagerScript.CoinScoreIncrease();
            audioManagerScript.PlaySound("CoinCollected");
            gameObject.SetActive(false);
        }
    }

    private void LocateScriptReferences()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        coinObjectPoolScript = FindObjectOfType<CoinObjectPool>();
        audioManagerScript = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        CoinMovement();
    }

    private void CoinMovement()
    {
        transform.position += Vector3.left * gameManagerScript.ElementMovementSpeed * Time.deltaTime;
    }

    private void OnDisable()
    {
        ResetCoinPosition();
    }

    private void ResetCoinPosition()
    {
        transform.position = coinObjectPoolScript.CoinParent.position;
    }
}
