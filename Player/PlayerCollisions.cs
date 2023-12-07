using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [Header("Level Restart Configs")]
    [SerializeField] private float levelRestartDelay = 2f;
    //[SerializeField] private bool restartDebugOnOff = false;
    private const string coin = "Coin"; 
    private PlayerMovement playerMovementScript;
   
    [SerializeField] public static bool isCrashed = false;

    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != coin)
        {
            FindObjectOfType<ObjectPool>().shouldSpawn = false;
            StartCoroutine(PlayerCrashedEvent());
        }
    }

    private IEnumerator PlayerCrashedEvent()
    {
        isCrashed = true;
        FindObjectOfType<AudioManager>().PlaySound("PlayerCrash");
        SceneManagement sceneManagerScript = FindObjectOfType<SceneManagement>();
        GetComponent<Collider2D>().enabled = false;

        sceneManagerScript.PauseButton.SetActive(false);
        playerMovementScript.enabled = false;
        playerMovementScript.ApplyCrashKnockback(); // This still works despite player movement being disabled, as disabling the script only prevents monobehaviour methods being called, not custom methods. 
        FindObjectOfType<BackgroundScroller>().enabled = false;
        FindObjectOfType<GameManager>().ElementMovementSpeed = 0;
       

        

        ///////////////////////////////////////////////////
        yield return new WaitForSeconds(levelRestartDelay);
        //////////////////////////////////////////////////
        
        
        sceneManagerScript.ActivateGameOverUI();
        
    }


}
