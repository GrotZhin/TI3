using System;
using Unity.Mathematics;
using UnityEngine;

public class Spawnvfx : MonoBehaviour
{
    public GameObject Spawn;
    public GameObject Master;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDisable()
        {
        Instantiate(Spawn, Master.transform.position, quaternion.identity);
    }
}
