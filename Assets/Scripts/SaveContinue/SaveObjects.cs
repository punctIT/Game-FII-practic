using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.SceneManagement;

public class SaveObjects : MonoBehaviour
{
    public GameObject saveMenuUI;

    public void Pause()
    {
       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        saveMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
    }

}
