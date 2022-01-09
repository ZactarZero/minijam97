using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{
    public Transform beer;
    public bool hasBeer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(){
        if (beer.localScale.z < 600f){
            beer.gameObject.SetActive(true);
            beer.localScale = beer.localScale + Vector3.forward * 50f;
            hasBeer = true;
        }
    }

    public bool Consume(){
        beer.localScale = beer.localScale - Vector3.forward * 50f;
        if (beer.localScale.z <= 0f) {
            beer.gameObject.SetActive(false);
            hasBeer = false;
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("BeerDrop")){
            Fill();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        
    }
    
}
