using System;
using Unity.Mathematics;
using UnityEngine;

public class SpawnvfxDestroy : MonoBehaviour
{
    public float time = 0;
    
        void Update()
        {
        Destroy(this.gameObject, time);
        }
    
}
