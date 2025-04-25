using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class FirstMenu : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioSource audioSource;
    public InventoryData inventory;
    public GameObject menu;
    public GameObject SaveMenu;
    public GameObject SettingsMenu;
    private void LoadScene(string sceneToLoad)
    {
         PlaySound();
        StartCoroutine(LoadSceneAfterSound(sceneToLoad));
        SceneManager.LoadScene(sceneToLoad);
        
    }
    private IEnumerator LoadSceneAfterSound(string sceneToLoad)
    {
        yield return new WaitForSeconds(buttonClickSound.length);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void NewGame(){
        
        inventory.scene="Act1";
        inventory.elevatorKey=false;
        inventory.haveFlashLight=false;
        inventory.playerPosititon=new Vector3(-91,-13,58);
        LoadScene("Act1");
    }

    public void exitGame(){
         PlaySound();
        Application.Quit();
    }
   
    public void continueBtn(){
        PlaySound();
        SaveMenu.SetActive(true);
        menu.SetActive(false);
    }
    public void back(){
         PlaySound();
        SaveMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void Setting(){
        PlaySound();
        menu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
     void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
