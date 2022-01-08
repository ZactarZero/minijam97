using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform trailSpot;

    private Vector3 cameraCenter = new Vector3(0.5f, 0.5f, 0);
    private Vector3 trailRotation = new Vector3(90f, 0f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(cameraCenter);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)){
            if (Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("Interactable"))){
                Debug.Log(hit.collider.name);
                hit.collider.transform.SetParent(trailSpot);
                hit.collider.transform.localPosition = Vector3.zero;
                hit.collider.transform.localRotation = Quaternion.Euler(trailRotation);
            }
        }
    }
}
