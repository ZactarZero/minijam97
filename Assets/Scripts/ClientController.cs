using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    public BarController barManager;

    private CupController watashiNoCupo;
    private WaitForSeconds beerNoTabeteCooldown = new WaitForSeconds(4f);
    private Vector3 spawnPoint;
    private ChairController chairUnderButt = null;
    private SphereCollider col;
    private int timesConsumed = 0;
    private bool cupWasDirty = false;
    private AudioSource audioSource;

    private float timeToWait = 30f;
    private float arriveTime = 0f;
    private bool goingHome = false;

    void Start()
    {
        spawnPoint = transform.position;
        col = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        if (arriveTime != 0f && Time.time > arriveTime + timeToWait && watashiNoCupo == null && !goingHome) {
            goingHome = true;
            StartCoroutine(GoHome());
        }
    }

    IEnumerator GoHome() {
        chairUnderButt.isTaken = false;
        transform.LookAt(spawnPoint);
        Vector3 startingPosition = transform.position;

        float lastWalkingTime = Time.time;
        float timeWalking = 0f;
        while (timeWalking <= 2f){
            transform.position = Vector3.Lerp(startingPosition, spawnPoint, timeWalking/2f);
            timeWalking += Time.time - lastWalkingTime;
            lastWalkingTime = Time.time;
            yield return null;
        }
        chairUnderButt = null;
        barManager.ChangePopularity(-0.1f);
        Destroy(gameObject);
    }

    public IEnumerator GoSit(ChairController chair){
        chairUnderButt = chair;
        chairUnderButt.isTaken = true;
        transform.LookAt(chairUnderButt.transform.position);
        Vector3 startingPosition = transform.position;

        float lastWalkingTime = Time.time;
        float timeWalking = 0f;
        while (timeWalking <= 2f){
            transform.position = Vector3.Lerp(startingPosition, chairUnderButt.transform.position, timeWalking/2f);
            timeWalking += Time.time - lastWalkingTime;
            lastWalkingTime = Time.time;
            yield return null;
        }
        col.enabled = true;
        arriveTime = Time.time;
        transform.rotation = chairUnderButt.transform.rotation;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cup") && other.transform.parent == null){
            watashiNoCupo = other.GetComponent<CupController>();
            if (watashiNoCupo.hasBeer) {
                StartCoroutine(ConsumeBeer());
                cupWasDirty = watashiNoCupo.isDirty;
            } else {
                watashiNoCupo = null;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Cup") && other.transform.parent == null){
            watashiNoCupo = null;
            StopAllCoroutines();
        }
    }

    IEnumerator ConsumeBeer(){
        while (true) {
            bool doneConsuming = watashiNoCupo.Consume();
            timesConsumed += 1;
            if (doneConsuming) {
                col.enabled = false;
                chairUnderButt.isTaken = false;
                transform.LookAt(spawnPoint);
                Vector3 startingPosition = transform.position;

                float lastWalkingTime = Time.time;
                float timeWalking = 0f;
                while (timeWalking <= 2f){
                    transform.position = Vector3.Lerp(startingPosition, spawnPoint, timeWalking/2f);
                    timeWalking += Time.time - lastWalkingTime;
                    lastWalkingTime = Time.time;
                    yield return null;
                }
                barManager.Pay(cupWasDirty ? timesConsumed * 0.20f : timesConsumed * 1f);
                barManager.ChangePopularity(cupWasDirty ? -0.05f : 0.5f);
                chairUnderButt = null;
                Destroy(gameObject);
            }
            yield return beerNoTabeteCooldown;
        }
    }
}
