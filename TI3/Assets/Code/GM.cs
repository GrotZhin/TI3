using UnityEngine;

public class GM : MonoBehaviour
{
    public int counterBox = 0;
    public bool puzzle1 = false, puzzle2 = false;
    public static GM instance;
    public static bool DayTime;
    public static bool firtsStart = true;
    public static bool interacting = false;
    void Awake()
    {
        
        Debug.Log("boll: " + firtsStart);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
