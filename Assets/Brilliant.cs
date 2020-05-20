using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brilliant : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    private float randVal;
    [SerializeField] public GameObject rootGm;
    private float SearchRandVal()
    {
        var radnRg = Random.Range(-1, 1);

        return radnRg;
    }

    private void OnEnable()
    {
        transform.position = startPos;
        randVal = SearchRandVal();
        
        while(randVal==0)
            randVal = SearchRandVal();
    }

    private void BrilliantRotate() => transform.Rotate(0, 0, randVal*30 * Time.deltaTime * 15);
   
    private void BrilliantUp()
    {       
        transform.position = Vector3.MoveTowards(transform.position, Vector3.up, Time.deltaTime * 5);

        if (Vector3.Distance(transform.position, Vector3.up) < 0.01f)
            rootGm.SetActive(false);
        
     
    }

    private void Update()
    {
        BrilliantRotate();
        BrilliantUp();
    }

}
