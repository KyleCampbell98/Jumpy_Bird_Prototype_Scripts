using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Barrier_Mover : MonoBehaviour
{
    private float barrierYPos;
    private float barrierXPos;
    public float BarrierXPos { set { return; } }

    [SerializeField] private float upperYBarrierLimit = 3.2f;
    [SerializeField] private float lowerYBarrierLimit = -0.8f;

    // script references
    GameManager gameManagerScript;
    ObjectPool objectPoolScript;

    // Update is called once per frame

    private void Awake()
    {
        LocateScriptReferences();
    }

    private void LocateScriptReferences()
    {
        objectPoolScript = FindObjectOfType<ObjectPool>();
        gameManagerScript = FindObjectOfType<GameManager>();
       
    }

    private void OnEnable()
    {
        ResetBarrierPosition();
    }

    private void Update()
    {
        BarrierMovement();
    }

    private void OnDisable()
    {
        ResetBarrierPosition();
    }

    public void ResetBarrierPosition()
    {
        barrierYPos = UnityEngine.Random.Range(lowerYBarrierLimit, upperYBarrierLimit);
        transform.position = new Vector3(objectPoolScript.BarrierParent.position.x, barrierYPos, objectPoolScript.BarrierParent.position.z);
    }

    private void BarrierMovement()
    {
       transform.position += Vector3.left * gameManagerScript.ElementMovementSpeed * Time.deltaTime;
    }

   
}
