using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public AudioClip buttonClickSound; // sunetul de buton
    private AudioSource audioSource;
    private bool isPaused = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        PlaySound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Pause()
    {
        PlaySound();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        PlaySound();
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        PlaySound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
