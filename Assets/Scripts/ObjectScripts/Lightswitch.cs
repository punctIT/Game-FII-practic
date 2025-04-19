using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Obiectul de activat/dezactivat")]
    public GameObject obiectDeControlat;
    public GameObject[] neons = new GameObject[2];

    [Header("Sunet la comutare")]
    public AudioClip sunetComutare;           // Clipul audio
    private AudioSource audioSource;          // Componenta care va reda sunetul

    private bool estePornit = false;

    void Start()
    {
        // Asigură-te că avem un AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (obiectDeControlat != null)
        {
            obiectDeControlat.SetActive(estePornit);
        }
    }

    public void ToggleSwitch()
    {
        estePornit = !estePornit;

        if (obiectDeControlat != null)
        {
            obiectDeControlat.SetActive(estePornit);
            neons[0].SetActive(estePornit);
            neons[1].SetActive(!estePornit);
        }

        // Redă sunetul la comutare
        if (sunetComutare != null && audioSource != null)
        {
            audioSource.PlayOneShot(sunetComutare);
        }
    }
}
