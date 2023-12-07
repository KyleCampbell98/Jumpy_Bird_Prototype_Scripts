using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    [Header("UI Scene Cache")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject startGameCanvas;
    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject resumeGameCanvas;
    [SerializeField] private TextMeshProUGUI pauseScoreText;

    private GameObject pauseButton;
    private int currentSceneIndex;

    private bool shouldSpawnElements;
    private bool isPaused;
    private ObjectPool objectPoolScript;
    private PlayerMovement playerMovementScript;
    private GameManager gameManagerScript;

    // Getter Properties
    public bool ShouldSpawnElements { get { return shouldSpawnElements; } }
    public bool StartGameCanvas { get { return startGameCanvas.activeSelf; } }

    public GameObject PauseButton {  get { return pauseButton; } }

    private void Awake()
    {
        LocateReferences();
        shouldSpawnElements = false;
        isPaused = false;
    }

    private void LocateReferences()
    {
        objectPoolScript = FindObjectOfType<ObjectPool>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        gameManagerScript = FindObjectOfType<GameManager>();
        scoreCanvas = GameObject.FindGameObjectWithTag("ScoreCanvas");
        pauseButton = scoreCanvas.transform.Find("Pause_Button").gameObject;
    }

    private void Start()
    {
        playerMovementScript.enabled = false;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        scoreCanvas.SetActive(false);
    }

    private void Update()
    {
        PauseGame(); //DEBUG ONLY
    }

    public void ActivateGameOverUI()
    {
        if (gameOverCanvas == null) { Debug.LogError("gameOver Canvas is Missing"!); return; }

        Debug.Log("Pausing");
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        
    }

    public void StartGameClickFunction()
    { 
        StartCoroutine(StartGameOnClick());
    }

    IEnumerator StartGameOnClick()
    {
        SetCanvasGameStartStatus();
        playerMovementScript.enabled = true;
        shouldSpawnElements = true;
        instructionsCanvas.SetActive(true);

        yield return new WaitUntil(() => Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0));

        instructionsCanvas.SetActive(false);
        StartCoroutine(objectPoolScript.SpawnEnemy());
        playerMovementScript.isDemoMode = false;
        shouldSpawnElements = true;
        playerMovementScript.StopAllCoroutines();
        playerMovementScript.DemoUpThrust();
    }

    private void SetCanvasGameStartStatus()
    {
        startGameCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currentSceneIndex);
        PlayerCollisions.isCrashed = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGameOnClick()
    {
        if (!isPaused && !PlayerCollisions.isCrashed)
        {
            isPaused = true;
            Time.timeScale = 0;
            scoreCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(true);
            pauseScoreText.text = "Current Score: \n " + gameManagerScript.PlayerScore;
        }
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused && !PlayerCollisions.isCrashed)
        {
            isPaused = true;
            Time.timeScale = 0;
            scoreCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(true);
            pauseScoreText.text = "Current Score: \n " + gameManagerScript.PlayerScore;
            
        }
    }

    public void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        StartCoroutine(ResumeGameCountdown());

    }

    IEnumerator ResumeGameCountdown()
    {
        resumeGameCanvas.SetActive(true);
        TextMeshProUGUI resumeCountdownText = resumeGameCanvas.GetComponentInChildren<TextMeshProUGUI>();
        resumeCountdownText.text = "3...";
        yield return new WaitForSecondsRealtime(1);
        resumeCountdownText.text = "2...";
        yield return new WaitForSecondsRealtime(1);
        resumeCountdownText.text = "1...";
        yield return new WaitForSecondsRealtime(1);
        resumeCountdownText.text = "Go!";
        yield return new WaitForSecondsRealtime(1.5f);
        resumeGameCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
