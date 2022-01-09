using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{
    public Transform beer;
    public bool hasBeer = false;
    public bool isDirty = false;
    public Material cleanMat;
    public Material dirtyMat;
    private Renderer mat;
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Glass")){
                mat = child.GetComponent<Renderer>();
            }
        }
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
            isDirty = true;
            mat.material = dirtyMat;
            return true;
        }
        return false;
    }

    public void Clean(){
        beer.localScale = beer.localScale - Vector3.forward * beer.localScale.z;
        beer.gameObject.SetActive(false);
        hasBeer = false;
        isDirty = false;
        mat.material = cleanMat;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("BeerDrop")){
            Fill();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        
    }

    private void OnTriggerEnter(Collider other) {
        if ((other.CompareTag("Table") || other.CompareTag("Trail") || other.CompareTag("Ground")) && (transform.parent == null || transform.parent.CompareTag("Trail"))){
            hasBeer = false;
            beer.localScale = beer.localScale - Vector3.forward * beer.localScale.z;
            beer.gameObject.SetActive(false);
            if (Random.Range(0, 100f) < 50f){
                sound.Stop();
                sound.Play();
                transform.position = Vector3.zero;
                Destroy(gameObject, 3f);
            }
        }
    }
    
}
