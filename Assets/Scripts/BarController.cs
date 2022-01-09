using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public List<GameObject> clientPrefabs = new List<GameObject>();
    public List<ChairController> chairs = new List<ChairController>();
    public Transform spawnPoint;
    public float money = 0f;
    public Text moneyDisplay;

    private float lastSpawnTime = 0f;
    private float spawnTimeCooldown = 10f;


    void Start()
    {
        
    }

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
                clientController.barManager = this;
                StartCoroutine(clientController.GoSit(availableChairs[Random.Range(0, availableChairs.Count)]));
                lastSpawnTime = Time.time;
                spawnTimeCooldown = Random.Range(20f, 40f);
            }
        }
    }

    public void Pay(float amount){
        money += amount;
        moneyDisplay.text = $"$ {money}";
    }
}
