using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act2 : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject elevatorKey;
    public GameObject elevatorlock;

    public GameObject Ghost;
    void Start()
    {
        if(inventoryData.elevatorKey==true){
            Destroy(elevatorKey);
            Destroy(elevatorlock);
           
        }
    }
    void Update()
    {
        if(inventoryData.elevatorKey==true){
            Destroy(elevatorKey);
            Destroy(elevatorlock);
          
        }
    }


}
