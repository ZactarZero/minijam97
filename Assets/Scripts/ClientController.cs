using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    private CupController watashiNoCupo;
    private WaitForSeconds beerNoTabeteCooldown = new WaitForSeconds(4f);
    private Vector3 spawnPoint;
    private ChairController chairUnderButt = null;
    private SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position;
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        transform.rotation = chairUnderButt.transform.rotation;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cup") && other.transform.parent == null){
            watashiNoCupo = other.GetComponent<CupController>();
            if (watashiNoCupo.hasBeer) {
                StartCoroutine(ConsumeBeer());
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

                chairUnderButt = null;
                Destroy(gameObject);
            }
            yield return beerNoTabeteCooldown;
        }
    }
}
