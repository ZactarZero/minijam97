using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GoSit(ChairController chair){
        chair.isTaken = true;
        transform.LookAt(chair.transform.position);
        Vector3 startingPosition = transform.position;

        float lastWalkingTime = Time.time;
        float timeWalking = 0f;
        while (timeWalking <= 2f){
            transform.position = Vector3.Lerp(startingPosition, chair.transform.position, timeWalking/2f);
            timeWalking += Time.time - lastWalkingTime;
            lastWalkingTime = Time.time;
            yield return null;
        }
        
        transform.rotation = chair.transform.rotation;
    }
}
