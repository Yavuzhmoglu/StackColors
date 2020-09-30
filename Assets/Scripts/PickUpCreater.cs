using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCreater : MonoBehaviour
{
    public GameObject[] Zone;
    public GameObject[] PickUps;


    void Start()
    {
        PickUpCreate();



    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PickUpCreate()
    {
        
        for (int i = 0; i < Zone.Length; i++)
        {int rndPick = Random.Range(0, 11);
           Instantiate(PickUps[rndPick],Zone[i].transform.position, Zone[i].transform.rotation);
        }

        
    }
}
