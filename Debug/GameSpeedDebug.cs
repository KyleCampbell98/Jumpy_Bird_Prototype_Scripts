using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedDebug : MonoBehaviour
{
    [Tooltip("Speed of the game is adjustable for debugging purposes")]
    [Range(0.1f, 2f)][SerializeField] private float gameSpeed = 1f;
    

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
