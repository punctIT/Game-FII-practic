using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Transform door; // Transformul ușii
    public float slideDistance = 2f; // Cât de mult se va glisa ușa pe axa X
    public float slideSpeed = 2f; // Viteza de glisare
    public float autoCloseDelay = 5f; // Timpul după care ușa se va închide automat (în secunde)
    public float minScaleX = 0.1f; // Scala minimă pe X (pentru micșorare)

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Vector3 closedScale;
    private Vector3 openScale;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = door.position;
        openPosition = closedPosition + new Vector3(0, 0, -slideDistance); // Glisare pe axa X

        closedScale = door.localScale;
        openScale = new Vector3(minScaleX, closedScale.y, closedScale.z); // Doar X se micșorează
    }

    public void LiftDoor()
    {
        StopAllCoroutines();
        if (!isOpen)
        {
            StartCoroutine(SlideAndScaleDoor(openPosition, openScale));
            StartCoroutine(AutoCloseDoor());
        }
        else
        {
            StartCoroutine(SlideAndScaleDoor(closedPosition, closedScale));
            StopCoroutine(AutoCloseDoor()); // Nu mai e necesar, dar păstrăm pentru claritate
        }
        isOpen = !isOpen;
    }

    IEnumerator SlideAndScaleDoor(Vector3 targetPosition, Vector3 targetScale)
    {
        while (Vector3.Distance(door.position, targetPosition) > 0.01f || Vector3.Distance(door.localScale, targetScale) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, targetPosition, slideSpeed * Time.deltaTime);
            door.localScale = Vector3.MoveTowards(door.localScale, targetScale, slideSpeed * Time.deltaTime);
            yield return null;
        }

        door.position = targetPosition;
        door.localScale = targetScale;
    }

    IEnumerator AutoCloseDoor()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        if (isOpen)
        {
            StartCoroutine(SlideAndScaleDoor(closedPosition, closedScale));
            isOpen = false;
        }
    }
}
