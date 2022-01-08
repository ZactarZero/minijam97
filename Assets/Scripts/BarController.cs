using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public GameObject clientPrefab1;
    public GameObject clientPrefab2;
    public GameObject clientPrefab3;
    public List<GameObject> clientPrefabs = new List<GameObject>();

    void Start()
    {
        clientPrefabs.Add(clientPrefab1);
        clientPrefabs.Add(clientPrefab2);
        clientPrefabs.Add(clientPrefab3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            
        }
    }
}
