using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetectObject : MonoBehaviour {
  [HideInInspector]
  public KeyCode interactKey = KeyCode.E;  // Tasta pentru interactiune
  public float maxDistance = 5f;           // Distanta maxima de detectie
  public TMP_Text myText;

  public InventoryData playerInventory;

  void Update() {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance)) {
      GameObject obj = hit.collider.gameObject;

      if (obj.CompareTag("Interactble")) {
        myText.text = obj.name;
        
        if (Input.GetKeyDown(interactKey)) {
        

          if (obj.name == "Cube")
            Destroy(obj);

          obj.GetComponent<Door>()?.OpenDoor();
          obj.GetComponent<ElevatorDoor>()?.LiftDoor();
          obj.GetComponent<Drawer>()?.OpenDrawer();

          if (obj.name == "ElevatorKey") {
            playerInventory.elevatorKey=true;
            Destroy(obj);
          }

          if (obj.name == "Flashlight"){
              Destroy(obj);
              playerInventory.haveFlashLight=true;
           }
           if (obj.name == "BasketBall"){
              Rigidbody rb = obj.GetComponent<Rigidbody>();
              if (rb != null)
              {
                  // Direcție combinată: puțin în sus și înainte
                  Vector3 forceDirection = (-transform.forward + Vector3.up).normalized * 100f;
                  rb.AddForce(forceDirection);
              }
           }
            
        }
      } else {
        myText.text = "";
      }
    } else {
      myText.text = "";
    }
  }
}
