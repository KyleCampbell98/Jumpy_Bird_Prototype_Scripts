using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierDestroyer : MonoBehaviour
{
    [Header("The number of barriers that have been reset since the last coin was spawned. Helps determine when the next coin will be randomly spawned.")]
    [SerializeField] private int barrierResetCount;
    SceneManagement sceneManagerScript;
    CoinObjectPool coinObjectPoolScript;

    private void Awake()
    {
        coinObjectPoolScript = FindObjectOfType<CoinObjectPool>();
        sceneManagerScript = FindObjectOfType<SceneManagement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.SetActive(false);
            IncreaseBarrierCount();
            SpawnCoin();
            collision.gameObject.GetComponent<Barrier_Mover>().ResetBarrierPosition();
            

        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void IncreaseBarrierCount()
    {
        barrierResetCount++;
    }

    public void SpawnCoin()
    {
        if(barrierResetCount == coinObjectPoolScript.TargetSpawnNumber && sceneManagerScript.ShouldSpawnElements)
        {
            coinObjectPoolScript.ActivateCoinInPool();
            barrierResetCount = 0;
        }
    }

}
