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
    public Slider popularityDisplay;
    public GameObject gameOverScreen;
    public float popularity = 1f;

    private float lastSpawnTime = 0f;
    private float spawnTimeCooldown = 10f;
    private bool gameIsStillGoing = true;


    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time >= lastSpawnTime + spawnTimeCooldown && gameIsStillGoing){
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
                spawnTimeCooldown = Random.Range(5f, 20f);
            }
        }
    }

    public void ChangePopularity(float amount){
        popularity += amount;
        popularity = popularity > 1f ? 1f : popularity;
        popularityDisplay.value = popularity;

        if (popularity <= 0f) {
            Time.timeScale = 0f;
            gameIsStillGoing = false;
            gameOverScreen.SetActive(true);
        }
    }

    public void Pay(float amount){
        money += amount;
        moneyDisplay.text = $"$ {money}";
    }
}
