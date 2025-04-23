using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class SaveSlotUI : MonoBehaviour
{
    public InventoryData inventoryData;
    public String Act;
    public Transform player;
    public Button[] saveButtons;
    public TMP_Text[] buttonLabels;

    public AudioClip buttonClickSound; // sunetul de buton
    private AudioSource audioSource;

    public GameObject gb;
    public void save1btn(){
        PlaySound();
        inventoryData.playerPosititon=player.position;
        inventoryData.scene=Act;
        inventoryData.Save(1);
        UpdateButtonLabels();
        Resume();
    }
    public void load1btn(){
        PlaySound();
        if(inventoryData.SaveSlotExists(1)){
            inventoryData.Load(1);
            SceneManager.LoadScene(inventoryData.scene);
        }  else {
            FirstMenu mn = FindObjectOfType<FirstMenu>();
            if (mn != null) {
                mn.back();
            }
        }
       

    }
    public void save2btn(){
        PlaySound();
        inventoryData.playerPosititon=player.position;
        inventoryData.scene=Act;
        inventoryData.Save(2);
        UpdateButtonLabels();
          Resume();
    }
    public void load2btn(){
        PlaySound();
        if(inventoryData.SaveSlotExists(2)){
            inventoryData.Load(2);
            SceneManager.LoadScene(inventoryData.scene);
        }  else {
            FirstMenu mn = FindObjectOfType<FirstMenu>();
            if (mn != null) {
                mn.back();
            }
        }
    }

    public void save3btn(){
        PlaySound();
        inventoryData.playerPosititon=player.position;
        inventoryData.scene=Act;
        inventoryData.Save(3);
         UpdateButtonLabels();
         Resume();
    }
    public void load3btn(){
        PlaySound();
       if(inventoryData.SaveSlotExists(3)){
            inventoryData.Load(3);
            SceneManager.LoadScene(inventoryData.scene);
        }
        else {
            FirstMenu mn = FindObjectOfType<FirstMenu>();
            if (mn != null) {
                mn.back();
            }
        }
    }
    private void Start()
    {
        UpdateButtonLabels();
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateButtonLabels()
    {
        for (int i = 0; i < saveButtons.Length; i++)
        {
            int slot = i + 1;
            if (inventoryData.SaveSlotExists(slot))
            {
                buttonLabels[i].text = $"Slot {slot} - Salvat";
            }
            else
            {
                buttonLabels[i].text = $"Slot {slot} - Liber";
            }
        }
    }
     void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
     public void Resume()
    {
        
        gb.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
