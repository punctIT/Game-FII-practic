using UnityEngine;
using TMPro;

public class TextFlickerWithSound : MonoBehaviour
{
    public float minFlickerTime = 1.0f;  // timp minim între flicker-uri
    public float maxFlickerTime = 3.0f;  // timp maxim între flicker-uri

    public AudioClip flickerSound;
    private AudioSource audioSource;
    private TextMeshProUGUI tmpText;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();

        // Adaugă un AudioSource dacă nu există
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(Flicker());
    }

    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            // Așteaptă un timp random
            float waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(waitTime);

            // Dezactivează textul
            tmpText.enabled = false;

            // Redă sunetul
            if (flickerSound != null)
            {
                audioSource.PlayOneShot(flickerSound);
            }

            // Stă fără text un pic
            yield return new WaitForSeconds(0.15f);  // durata cât dispare textul

            // Activează la loc textul
            tmpText.enabled = true;
        }
    }
}
