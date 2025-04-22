using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenu : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioSource audioSource;

    public void LoadScene()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
            StartCoroutine(LoadSceneAfterSound());
        }
        else
        {
            Debug.LogWarning("AudioSource sau AudioClip nu este setat!");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    public void exitGame(){
        Application.Quit();
    }
    private IEnumerator LoadSceneAfterSound()
    {
        yield return new WaitForSeconds(buttonClickSound.length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
