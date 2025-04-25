using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private SettingsData settingsData;
    public Slider SenXSlider;
    public Slider SenYSlider;

    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioSource audioSource;

    public GameObject MainManu;
    public GameObject SettingMenu;
    public GameObject Constolspanel;
    public void back(){
        PlaySound();
        MainManu.SetActive(true);
        SettingMenu.SetActive(false);
        
    }

    void Start()
    {
        settingsData.Load(1);
        SenXSlider.value = settingsData.sensitivityX;
        SenYSlider.value = settingsData.sensitivityY;
        SenXSlider.onValueChanged.AddListener(SenXChanged);
        SenYSlider.onValueChanged.AddListener(SenYChanged);
    }
    void SenXChanged(float newValue)
    {
        settingsData.sensitivityX=newValue;
        settingsData.Save(1);
    }
     void SenYChanged(float newValue)
    {
       settingsData.sensitivityY=newValue;
       settingsData.Save(1);
    }

    void Update()
    {
        
    }
    public void Controls(){
        Constolspanel.SetActive(true);

    }
    void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
