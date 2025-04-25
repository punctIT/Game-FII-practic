using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ElevatorTransport : MonoBehaviour
{
    public InventoryData inventoryData;
    public Transform player;
    public void load7thFloor(){
         inventoryData.playerPosititon=player.position;
         SceneManager.LoadScene("Act3");
    }
}
