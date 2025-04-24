using UnityEngine;
using System.Collections;

public class FlashlightZoom : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public Light flashlight;
    public float rangeStep = 1f;
    public float angleStep = 1f;
    public float minRange = 10f;  // Modificat pentru o valoare mai intuitivă
    public float maxRange = 40f;  // Modificat pentru a nu fi prea mare
    public float minAngle = 15f;  // Modificat pentru a oferi un unghi mai larg
    public float maxAngle = 50f;  // Modificat pentru a fi mai natural

    [Header("Flicker Settings")]
    public float minCheckInterval = 4f;
    public float maxCheckInterval = 8f;
    public float minFlickerDuration = 0.1f;
    public float maxFlickerDuration = 0.4f;
    [Range(0f, 1f)] public float flickerChance = 0.05f; // 5%
    public AudioClip flickerSound;

    private float nextCheckTime;
    private AudioSource audioSource;

    void Start()
    {
        ScheduleNextCheck();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        HandleZoom();

        if (Time.time >= nextCheckTime)
        {
            if (Random.value <= flickerChance)
                StartCoroutine(FlickerEffect());

            ScheduleNextCheck();
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            flashlight.range = Mathf.Clamp(flashlight.range - rangeStep, minRange, maxRange);
            flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle + angleStep, minAngle, maxAngle);
        }
        else if (scroll < 0f)
        {
            flashlight.range = Mathf.Clamp(flashlight.range + rangeStep, minRange, maxRange);
            flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle - angleStep, minAngle, maxAngle);
        }
    }

    void ScheduleNextCheck()
    {
        nextCheckTime = Time.time + Random.Range(minCheckInterval, maxCheckInterval);
    }

    IEnumerator FlickerEffect()
    {
        flashlight.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
        flashlight.enabled = true;

        if (flickerSound != null)
            audioSource.PlayOneShot(flickerSound);
    }
}
