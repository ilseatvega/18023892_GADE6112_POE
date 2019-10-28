using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMelee : MonoBehaviour
{
    //for the spawning of units
    public GameObject meleePrefab;
    //spawn delay
    public float spawnDelay;

    //spawn random units
    public void Spawn()
    {
        Vector3 spawnPos = transform.position;
        Instantiate(meleePrefab, spawnPos, Quaternion.identity);
        //setting the delay back to 0
        spawnDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay += Time.deltaTime;
        if (spawnDelay >= 8)
        {
            Spawn();
        }
    }
}
