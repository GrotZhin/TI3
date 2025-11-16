using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;
public class NightDayChanges : MonoBehaviour
{
    public List<GameObject> DayProps;
    public List<GameObject> NightProps;
    
   

    // Update is called once per frame
    void Update()
    {
        if (GM.DayTime)
        {

            foreach (var obj in DayProps) obj.SetActive(false);
            foreach (var obj in NightProps) obj.SetActive(true);
            
        }
        else if (!GM.DayTime)
        {
            
            foreach (var obj in DayProps) obj.SetActive(true);
            foreach (var obj in NightProps) obj.SetActive(false);
            
        }
        
    }
}
