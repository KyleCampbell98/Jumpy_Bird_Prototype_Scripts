using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    Transform rayCastParent;
    float barrierDistance;
    CoinObjectPool coinObjectPoolScript;

    private void Awake()
    {
        rayCastParent = gameObject.transform.Find("TestBarrierRaycast");
        coinObjectPoolScript = FindObjectOfType<CoinObjectPool>();
    }

    void Update()
    {
        if (barrierDistance != 0) { return; }
        CheckDistance();
    }

    private void CheckDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayCastParent.position, Vector2.right, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject.transform.parent.CompareTag("Obstacle"))
        {
            Debug.Log("Checking...");
            barrierDistance = hit.distance;
            coinObjectPoolScript.DistanceBetweenBarrierObstacles = barrierDistance;
            Debug.Log("Raycast Script: " + Environment.NewLine + "Distance of " + hit.collider.gameObject.transform.parent.name + " is: " + barrierDistance);
        }
    }

    private void OnDisable()
    {
        Destroy(this.GetComponent<TEST>());
    }
}
