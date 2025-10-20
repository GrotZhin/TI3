using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{
    GM gm;
    [SerializeField] GameObject winPanel;
    
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          winPanel.SetActive(false);
          gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.counterBox >= 4)
        {
            Win();
        }
    }
    void Win()
    {
        gm.puzzle2 = true;
        winPanel.SetActive(true);

    }
}
