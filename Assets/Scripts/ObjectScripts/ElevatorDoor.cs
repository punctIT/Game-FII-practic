using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Transform door; // Transformul ușii care se va glisa
    public float slideDistance = 2f; // Cât de mult se va glisa ușa pe axa X
    public float slideSpeed = 2f; // Viteza de glisare
    public float autoCloseDelay = 5f; // Timpul după care ușa se va închide automat (în secunde)

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false; // Urmărește starea ușii

    void Start()
    {
        closedPosition = door.position;
        openPosition = closedPosition + new Vector3(0, 0, slideDistance); // Glisare pe axa Z
    }

    public void LiftDoor()
    {
        // Comută starea ușii și începe glisarea
        StopAllCoroutines(); // Oprește orice altă corutină activă
        if (!isOpen)
        {
            StartCoroutine(SlideDoorCoroutine(openPosition));
            StartCoroutine(AutoCloseDoor()); // Începe timerul pentru închiderea automată
        }
        else
        {
            StartCoroutine(SlideDoorCoroutine(closedPosition));
            StopCoroutine(AutoCloseDoor()); // Oprește timerul dacă ușa se închide manual
        }
        isOpen = !isOpen; // Comută starea ușii
    }

    IEnumerator SlideDoorCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(door.position, targetPosition) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
        door.position = targetPosition; // Asigură că poziția finală este exactă
    }

    IEnumerator AutoCloseDoor()
    {
        // Așteaptă un timp specificat înainte de a închide ușa
        yield return new WaitForSeconds(autoCloseDelay);
        if (isOpen) // Verifică dacă ușa este deschisă și o închide
        {
            StartCoroutine(SlideDoorCoroutine(closedPosition));
            isOpen = false;
        }
    }
}
