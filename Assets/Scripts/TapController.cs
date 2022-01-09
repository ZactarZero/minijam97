using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject beerDrop;
    public bool isOpen = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    IEnumerator Pinga(){
        while (true) {
            yield return new WaitForSeconds(0.15f);
            Instantiate(beerDrop, spawnPoint.position, Quaternion.identity);
        }
    }

    public void Interact(){
        if (isOpen){
            StopAllCoroutines();
            isOpen = false;
            animator.SetTrigger("Close");
        } else {
            StartCoroutine(Pinga());
            isOpen = true;
            animator.SetTrigger("Open");
        }
    }
}
