using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{
    public Transform beer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)){
            Fill();
        }
    }

    public void Fill(){
        if (beer.localScale.z < 600f){
            beer.gameObject.SetActive(true);
            beer.localScale = beer.localScale + Vector3.forward * 50f;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("BeerDrop")){
            Fill();
            Destroy(other.gameObject);
        }
    }
    
}
