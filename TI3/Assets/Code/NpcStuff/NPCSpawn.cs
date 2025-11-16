using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

public class NPCSpawn : MonoBehaviour
{

    public List<GameObject> NPC;
    public List<GameObject> Rock;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.DayTime)
        {

            foreach (var obj in NPC) obj.SetActive(true);
            foreach (var obj in Rock) obj.SetActive(false);
            
        }
        else if (!GM.DayTime)
        {
            
            foreach (var obj in NPC) obj.SetActive(false);
            foreach (var obj in Rock) obj.SetActive(true);
            
        }
        
    }
}
