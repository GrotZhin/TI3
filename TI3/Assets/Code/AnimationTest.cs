using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;
    public Animator animator1;
    OpenGate openGate;
    MeshRenderer renderer;
    Material materialPlaca;
    Push counter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = GameObject.FindGameObjectWithTag("Player").GetComponent<Push>();
        openGate = GameObject.FindGameObjectWithTag("Player").GetComponent<OpenGate>();
        renderer = GetComponent<MeshRenderer>();
        materialPlaca = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.i == 4 && openGate.open == true )
            animator1.SetTrigger("abrir");

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
                    counter.i++;
                    Debug.Log("i: " + counter.i);
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
                    counter.i--;
                    Debug.Log("i: " + counter.i);
                }
            }
            
         
        }
    }
}
