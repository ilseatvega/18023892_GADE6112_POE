using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRanged : MonoBehaviour
{
    //for the spawning of units
    public GameObject rangedPrefab;
    //spawn delay
    public float spawnDelay;
    Random rnd = new Random();

    //spawn random units
    public void Spawn()
    {
        Vector3 spawnPos = transform.position;
        Instantiate(rangedPrefab, spawnPos, Quaternion.identity);
        //setting the delay back to 0
        spawnDelay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay += Time.deltaTime;
        if (spawnDelay >= 7)
        {
            Spawn();
        }
    }
}
