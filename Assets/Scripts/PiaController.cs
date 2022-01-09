using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiaController : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject waterDrop;
    public GameObject water;
    public bool isOpen = false;
    public List<GameObject> cupsInside = new List<GameObject>();
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator Pinga(){
        while (true) {
            yield return new WaitForSeconds(0.15f);
            Instantiate(waterDrop, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cup") && other.transform.parent == null && !cupsInside.Contains(other.gameObject)){
            cupsInside.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Cup")){
            cupsInside.Remove(other.gameObject);
        }
    }

    public void Interact(){
        if (isOpen){
            StopAllCoroutines();
            isOpen = false;
            water.SetActive(false);
        } else {
            StartCoroutine(Pinga());
            isOpen = true;
            water.SetActive(true);
            foreach (GameObject cup in cupsInside)
            {
                cup.GetComponent<CupController>().Clean();
            }
        }
    }
}
