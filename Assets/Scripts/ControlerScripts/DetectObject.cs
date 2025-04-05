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
  void Update() {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance)) {
      GameObject obj = hit.collider.gameObject;

      if (obj.CompareTag("Interactble")) {
        myText.text =
            "Apasa " + interactKey + " pentru a interactiona cu: " + obj.name;

        if (Input.GetKeyDown(interactKey)) {
          Debug.Log("Te uiti la: " + obj.name);

          if (obj.name == "Cube")
            Destroy(obj);

          obj.GetComponent<Door>()?.OpenDoor();
          obj.GetComponent<LightSwitch>()?.SetLight();
          obj.GetComponent<ElevatorDoor>()?.LiftDoor();

          if (obj.name == "Cube (1)") {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
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
