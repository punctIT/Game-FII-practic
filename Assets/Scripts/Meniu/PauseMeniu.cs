using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public GameObject settingUi;

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
         settingUi.SetActive(false);
        Time.timeScale = 1f;
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
        isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void SettingUI(){
        pauseMenuUI.SetActive(false);
        settingUi.SetActive(true);
    }
    void Pause()
    {
        PlaySound();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
       
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        PlaySound();
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        PlaySound();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Act0");
    }

    void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
