using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlock : MonoBehaviour
{
     GM gm;
     [SerializeField] GameObject rock1;
    [SerializeField] GameObject rock2;
    [SerializeField] GameObject rock3;
    [SerializeField] List<GameObject> unlock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
           gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
         if (SceneManager.GetActiveScene().name == "Prototype")
        {
            if (gm.puzzle1)
            {
                unlock.Add(rock1);
                rock1 = null;
            }

            if (gm.puzzle2)
            {
                unlock.Add(rock2);
                rock2 = null;
            }
            if (gm.puzzle3)
            {
                unlock.Add(rock3);
                rock3 = null;
            }
            foreach (var obj in unlock)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
