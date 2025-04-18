using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Obiectul de activat/dezactivat")]
    public GameObject obiectDeControlat;
    public GameObject[] neons = new GameObject[2];

    [Header("Sunet la comutare")]
    public AudioClip clickSound;
    private AudioSource audioSource;

    private bool estePornit = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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

        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
