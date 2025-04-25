using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    private bool isOpen = false;
    public Transform drawer; // Sertarul propriu-zis
    public float slideDistance = 0.5f; // Cât se trage în față
    public float slideSpeed = 1f; // Unități pe secundă
    public Vector3 slideDirection = Vector3.forward; // Direcția glisării
    public bool isLocked = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    [Header("Sunet (opțional)")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    void Start()
    {
        closedPosition = drawer.localPosition;
        openPosition = closedPosition + slideDirection.normalized * slideDistance;
    }

    public void OpenDrawer()
    {
        if (isLocked) return;

        StopAllCoroutines(); // Întrerupe glisarea anterioară, dacă există
        StartCoroutine(SlideDrawer(isOpen ? closedPosition : openPosition));

        // Redă sunetul corespunzător dacă există
        if (audioSource != null)
        {
            AudioClip clipToPlay = isOpen ? closeSound : openSound;
            if (clipToPlay != null)
            {
                audioSource.PlayOneShot(clipToPlay);
            }
        }

        isOpen = !isOpen;
    }

    IEnumerator SlideDrawer(Vector3 targetPosition)
    {
        while (Vector3.Distance(drawer.localPosition, targetPosition) > 0.001f)
        {
            drawer.localPosition = Vector3.MoveTowards(drawer.localPosition, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
        drawer.localPosition = targetPosition; // Fixează poziția exact
    }
}
