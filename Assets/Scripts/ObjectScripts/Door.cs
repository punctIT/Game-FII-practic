using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public Transform door;
    public float rotationSpeed = 90f;
    public bool isLocked = false;

    [Tooltip("1 = spre exterior, -1 = spre interior")]
    public int openDirection = 1;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    [Header("Sunete")]
    public AudioClip doorSound;
    private AudioSource audioSource;

    void Start()
    {
        closedRotation = door.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, 90 * openDirection, 0);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OpenDoor()
    {
        if (isLocked) return;

        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? closedRotation : openRotation));
        isOpen = !isOpen;

        if (doorSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(doorSound);
        }
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        while (Quaternion.Angle(door.rotation, targetRotation) > 0.1f)
        {
            door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        door.rotation = targetRotation;

        if (col != null)
            col.enabled = true;
    }
}
