using UnityEngine;

public class BoxCheck : MonoBehaviour
{
    public Animator animator;
  
    
    MeshRenderer renderer;

    Material materialPlaca;
    [SerializeField] GameObject identifier;
    GM gm;
    BoxCount boxCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
        boxCount = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCount>();
        renderer = GetComponent<MeshRenderer>();
        materialPlaca = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
      

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {

            MeshRenderer meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
           
            if (meshRenderer != null)
            {
                Material materialAtual = meshRenderer.material;
                if (materialAtual.name.Equals(materialPlaca.name))
                {
                    identifier.SetActive(false); 
                   
                    boxCount.counterBox++;
                    
                }
            }
            
         
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Push"))
        {

            MeshRenderer meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Material materialAtual = meshRenderer.material;
                if (materialAtual.name.Equals(materialPlaca.name))
                {
                    identifier.SetActive(true);
                    boxCount.counterBox--;
                    
                }
            }
            
         
        }
    }
}
