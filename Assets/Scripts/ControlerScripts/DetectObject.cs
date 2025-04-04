using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    [HideInInspector]
    public KeyCode interactKey = KeyCode.E; // Tasta pentru interactiune
    public float maxDistance = 5f; // Distanta maxima de detectie
    
    void Update()
    {
        if (Input.GetKeyDown(interactKey)) 
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                Debug.Log("Te uiti la: " + hit.collider.gameObject.name);
                if(hit.collider.gameObject.name=="Cube"){
                    Destroy(hit.collider.gameObject);
                }
                hit.collider.GetComponent<Door>()?.OpenDoor();
                if(hit.collider.gameObject.name=="Cube (1)"){
                    Rigidbody rb=hit.collider.gameObject.GetComponent<Rigidbody>();
                    rb.AddForce(Vector3.up*5,ForceMode.Impulse);
                }
            }
            else
            {
                Debug.Log("Nu te uiti la niciun obiect.");
            }
        }
    }
}
