using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowAndDestroy : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float destroyDistance = 1.5f;
    public float speed = 3.5f;
    public string playerTag = "Player";
    public AudioClip followSound; // Sunetul care va fi redat
    public GameObject uiObject; // UI-ul care va fi activat temporar

    private Transform player;
    private NavMeshAgent agent;
    private AudioSource audioSource;
    private bool hasPlayedSound = false; // Flag pentru a verifica daca sunetul a fost redat

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        audioSource = GetComponent<AudioSource>(); // Obține componenta AudioSource

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Adaugă un AudioSource dacă nu există
        }

        if (uiObject != null)
        {
            uiObject.SetActive(false); // Asigură-te că UI-ul e inactiv la început
        }
    }

    void Update()
    {
        if (player == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag(playerTag))
                {
                    // Verifică dacă avem linie de vedere către player
                    RaycastHit rayHit;
                    if (Physics.Raycast(transform.position, (hit.transform.position - transform.position).normalized, out rayHit, detectionRadius))
                    {
                        if (rayHit.transform == hit.transform)
                        {
                            player = hit.transform;

                            // Redă sunetul doar o dată
                            if (followSound != null && !hasPlayedSound)
                            {
                                audioSource.PlayOneShot(followSound);
                                hasPlayedSound = true; // Setează flag-ul pentru a evita redarea ulterioară
                            }
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            agent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.position) <= destroyDistance)
            {
                // Activează UI-ul înainte de a distruge obiectul
                if (uiObject != null)
                {
                    StartCoroutine(ActivateUIAndDestroy());
                }
                else
                {
                    Destroy(gameObject); // Distruge imediat dacă UI-ul nu este setat
                }
            }
        }
    }

    private IEnumerator ActivateUIAndDestroy()
    {
        uiObject.SetActive(true); // Activează UI-ul
        yield return new WaitForSeconds(1f); // Așteaptă 1 secundă
        uiObject.SetActive(false); // Dezactivează UI-ul
        Destroy(gameObject); // Distruge obiectul
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
