using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public Transform door; // Asignează manual obiectul care se rotește
    public float rotationSpeed = 90f; // Grade pe secundă
    public bool isLocked = false; // Ușa este blocată sau nu

    [Tooltip("1 = spre exterior, -1 = spre interior")]
    public int openDirection = 1; // 1 sau -1, definește direcția de deschidere

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, 90 * openDirection, 0);
    }

    public void OpenDoor()
    {
        if (isLocked) return; 
        StopAllCoroutines(); // Evită bug-uri dacă apeși rapid
        StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
        isOpen = !isOpen; // Comută starea ușii
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(door.rotation, targetRotation) > 0.1f)
        {
            door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        door.rotation = targetRotation; // Fixează poziția finală exactă
    }
}
