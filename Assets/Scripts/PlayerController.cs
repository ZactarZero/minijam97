using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform handSpot;
    public GameObject objectInHand = null;

    private Vector3 cameraCenter = new Vector3(0.5f, 0.5f, 0);
    
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(cameraCenter);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)){
            if (Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("Interactable"))){
                if (objectInHand == null){
                    if (hit.collider.CompareTag("Cup")){
                        hit.collider.transform.SetParent(handSpot);
                        hit.collider.transform.localPosition = Vector3.zero;
                        hit.collider.transform.localRotation = Quaternion.identity;
                        objectInHand = hit.collider.gameObject;
                        hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                    } else if (hit.collider.CompareTag("Trail")){
                        hit.collider.transform.SetParent(handSpot);
                        hit.collider.transform.localPosition = Vector3.zero;
                        hit.collider.transform.localRotation = Quaternion.identity;
                        objectInHand = hit.collider.gameObject;
                        hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                    }
                } else {
                    if (hit.collider.CompareTag("Table")){
                        objectInHand.transform.SetParent(null);
                        objectInHand.transform.position = hit.point;
                        objectInHand.transform.rotation = Quaternion.identity;
                        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
                        objectInHand = null;
                    } else if (hit.collider.CompareTag("Trail") && objectInHand.CompareTag("Cup")){
                        objectInHand.transform.SetParent(hit.collider.transform);
                        objectInHand.transform.position = hit.point;
                        objectInHand.transform.rotation = Quaternion.identity;
                        objectInHand.GetComponent<Rigidbody>().isKinematic = false;
                        objectInHand = null;
                    }
                }
            }
        }
    }
}
