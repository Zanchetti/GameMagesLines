using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public ScoreManager scoreManager;

    // Update is called once per frame
    private void Start() {
        gameIsPaused = false;
    }
    void Update()
    {
        if (scoreManager.isCounting == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioListener.pause = false;
        scoreManager.isCounting = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        AudioListener.pause = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        AudioListener.pause = false;
        SceneManager.LoadScene("Jogo");
    }
}
