using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Config Parameters")]
    private float upthrustStrength = 16.4f;
    private Vector2 playerCrashKnockback = new Vector2(-2, 3);
    

    [Header("Demo Mode Configs")]
    private float demoUpthrust = 15;
    public bool isDemoMode;
    private float birdStartYPos;
 
    //Component References
    private Rigidbody2D playerRigidbody;

    private void Awake()
    {
        isDemoMode = true;
        LocateComponentReferences();
    }

    private void LocateComponentReferences()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        birdStartYPos = transform.position.y;
        StartCoroutine(DemoModeMovement());
    }

    private IEnumerator DemoModeMovement()
    {
        DemoUpThrust();
        yield return new WaitUntil(() => transform.position.y <= birdStartYPos);
        StartCoroutine(DemoModeMovement());
    }

    public void DemoUpThrust()
    {
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(Vector2.up * demoUpthrust, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDemoMode)
        {
            StopCoroutine(DemoModeMovement());
            ApplyUpthrust();
        }
    }

    private void ApplyUpthrust()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            playerRigidbody.velocity = Vector2.zero; // Everytime jump is pressed, the current velocity is cancelled out in favor of the new velocity.
            playerRigidbody.AddForce(Vector2.up * upthrustStrength, ForceMode2D.Impulse);
        }
    }

    public void ApplyCrashKnockback()
    {   
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(playerCrashKnockback, ForceMode2D.Impulse);
    }

    

   
}
