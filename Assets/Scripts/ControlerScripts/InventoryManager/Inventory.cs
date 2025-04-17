using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public InventoryData playerInventory;
    public Transform FlashLight;
    public KeyCode FlashKey;
    private bool flashOnOFF=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
         if (Input.GetKeyDown(FlashKey)&&playerInventory.haveFlashLight){
            flashOnOFF=!flashOnOFF;
            Debug.Log(playerInventory.elevatorKey);
         }
         FlashLightObject();
    }
    void FlashLightObject(){
        FlashLight.gameObject.SetActive(flashOnOFF);   
    }
}
