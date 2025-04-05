using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Transform ligthswitch; // Asignează manual obiectul care se va activa/dezactiva
    bool isOn = false; // Starea inițială a luminii
    public void SetLight()
    {
        if (isOn)
        {
            isOn=false;
            ligthswitch.gameObject.SetActive(false);
        }
        else
        {
            isOn=true;
            ligthswitch.gameObject.SetActive(true);
        }
    }
}
