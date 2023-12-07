using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObjectPool : MonoBehaviour
{
    [Header("Pool Parameters")]
    [SerializeField] private int coinPoolSize;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject[] coinPool;
    [SerializeField] private Transform coinParent;
    
    
    [Header("Random Coint Spawn Parameters")]
    [SerializeField] private int lowerCoinSpawnBound;
    [SerializeField] private int upperCoinSpawnBound;
    [Tooltip("Uses upper and lower bound to identify a random target for barrier resets for random coin spawning.")]
    [SerializeField] private int targetSpawnNumber;

    [Header("Coin Height Spawn Parameters")]
    private float coinYPos;
    [SerializeField] private float lowerYCoinSpawnLimit = -1.5f;
    [SerializeField] private float upperYCoinSpawnLimit = 3.2f;

    [SerializeField] private float distanceBetweenBarrierObstacles;
    [SerializeField] private Transform mostRecentBarrier;

    // Setter Methods
    public Transform MostRecentBarrier { set { mostRecentBarrier = value; } }
    public float DistanceBetweenBarrierObstacles { set { distanceBetweenBarrierObstacles = value; } }

    // Getter Methods
    public Transform CoinParent { get { return coinParent; } }
    public int TargetSpawnNumber { get { return targetSpawnNumber; } }

    private void Awake()
    {
        coinParent = GameObject.Find("CoinParent").transform;
        coinPool = new GameObject[coinPoolSize];
        PopulatePool();
    }

    private void PopulatePool()
    {
        for (int i = 0; i < coinPool.Length; i++)
        {
            coinPool[i] = Instantiate(coinPrefab, transform.position, Quaternion.identity, coinParent);
            coinPool[i].GetComponent<CoinMover>().BarrierDistance = distanceBetweenBarrierObstacles;
            coinPool[i].SetActive(false);
        }
    }

    private void Start()
    {
        SetCoinSpawnBarrierTarget();
    }

    private void SetCoinSpawnBarrierTarget()
    {
        targetSpawnNumber = 0;
        targetSpawnNumber = Random.Range(lowerCoinSpawnBound, upperCoinSpawnBound);
        Debug.Log("Barrier Reset Target is: " + targetSpawnNumber.ToString());
    }

    private void SetCoinYPos()
    {
        coinYPos = 0;
        coinYPos = Random.Range(lowerYCoinSpawnLimit, upperYCoinSpawnLimit);
        Debug.Log("Coin Y Pos = " + coinYPos);
    }

    public void ActivateCoinInPool()
    {
        for (int i = 0; i < coinPool.Length; i++)
        {
            if (coinPool[i].activeInHierarchy == false)
            {
                SetCoinYPos();
                coinPool[i].SetActive(true);
                coinPool[i].transform.position = new Vector2(mostRecentBarrier.position.x + (Mathf.RoundToInt(distanceBetweenBarrierObstacles / 2)), coinYPos);
             
                SetCoinSpawnBarrierTarget(); // Resets the spawn target for the next coin in the pool.
                return;
            }
        }
    }


}
