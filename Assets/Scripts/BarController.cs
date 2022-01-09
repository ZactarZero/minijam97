using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    public List<GameObject> clientPrefabs = new List<GameObject>();
    public List<ChairController> chairs = new List<ChairController>();
    public Transform spawnPoint;

    private float lastSpawnTime = 0f;
    private float spawnTimeCooldown = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastSpawnTime + spawnTimeCooldown){
            GameObject clientPrefab = clientPrefabs[Random.Range(0, clientPrefabs.Count)];
            GameObject client = Instantiate(clientPrefab, spawnPoint.position, Quaternion.identity);
            ClientController clientController = client.GetComponent<ClientController>();
            List<ChairController> availableChairs = new List<ChairController>();
            foreach (ChairController chair in chairs)
            {
                if (chair.isTaken == false){
                    availableChairs.Add(chair);
                }
            }
            if (availableChairs.Count > 0){
                StartCoroutine(clientController.GoSit(availableChairs[Random.Range(0, availableChairs.Count)]));
                lastSpawnTime = Time.time;
                spawnTimeCooldown = Random.Range(20f, 40f);
            }
        }
    }
}
