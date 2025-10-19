
using Unity.VisualScripting;
using UnityEngine;

public class SimonSays : MonoBehaviour
{


    [SerializeField] GameObject[] rocks;
    [SerializeField] GameObject[] sequence;  
    int position = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSequence(rocks, 2);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void CreateSequence(GameObject[] rocks, int limit)
    {

       sequence = new GameObject[limit ];

        for (int i = 0; i < sequence.Length; i++)
        {
            sequence[i] = rocks[Random.Range(0, rocks.Length)];
        }
    }
    public void RockObject(GameObject rock)
    {
        CheckSequence(rock, sequence);
    }

    void CheckSequence(GameObject rock, GameObject[] sequence )
    {

        if (rock == sequence[position])
        {
            position += 1;

        }
        else
        {
            position = 0;
        }
        
            
        if (position == sequence.Length) Debug.Log("ganhou");
    }
}
