using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject barrier;
    [SerializeField] private Transform barrierParent;
    CoinObjectPool coinObjectPool;

    [Header("Object Pool Configs")]
    [SerializeField] private int poolSize;
    [SerializeField] private float barrierSpawnDelay;
    [SerializeField] private GameObject[] barriersPool;
    public bool shouldSpawn;
    

    public Transform BarrierParent { get { return barrierParent; } }

    private void Awake()
    {
        barrierParent = GameObject.Find("BarrierParent").transform; // Used for hierachy management
        barriersPool = new GameObject[poolSize];
        coinObjectPool = FindObjectOfType<CoinObjectPool>();
        PopulatePool();
        
        shouldSpawn = true;
    }

    private void PopulatePool()
    {
        for(int i = 0; i < barriersPool.Length; i++)
        {
            barriersPool[i] = Instantiate(barrier, transform.position, Quaternion.identity, barrierParent);
            barriersPool[i].SetActive(false);
            barriersPool[i].name = "barrier number: " + i.ToString();
        }

       barriersPool[0].AddComponent<TEST>();

    }

    public IEnumerator SpawnEnemy()
    {
        while (shouldSpawn) 
        {
            if(!shouldSpawn) { yield break; }
            for (int i = 0; i < barriersPool.Length; i++)
            {
                if (barriersPool[i].activeInHierarchy == false)
                {
                    barriersPool[i].SetActive(true); // activates the barrier in the array at the position of i;
                    
                    coinObjectPool.MostRecentBarrier = barriersPool[i].transform;

                    int noOfActiveBarriers = 0;
                    foreach (GameObject barrier in barriersPool)
                    {

                        if (barrier.activeInHierarchy) { noOfActiveBarriers++; }
                        
                    }
                    if(noOfActiveBarriers == barriersPool.Length)
                    {
                        Debug.Log("Need to extend object pool for current spawn rate!");
                        yield break;
                    }

                    yield return new WaitForSeconds(barrierSpawnDelay);

                }
            }
        } 
    }

    
}
