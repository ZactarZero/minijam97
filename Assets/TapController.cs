using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    public GameObject gota;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pinga());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pinga(){
        while (true) {
            yield return new WaitForSeconds(0.1f);
            Instantiate(gota, transform.position, Quaternion.identity);
        }
    }
}
