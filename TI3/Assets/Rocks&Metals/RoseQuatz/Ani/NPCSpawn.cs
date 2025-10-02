using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject NPC;
    public GameObject Rock;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameUiManager.DayTime)
        {
            NPC.SetActive(true);
            Rock.SetActive(false);
        }
        else if (!GameUiManager.DayTime)
        {
            NPC.SetActive(false);
            Rock.SetActive(true);
        }
        
    }
}
