using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [Header("Configurações")]
    public string playerTag = "Player"; // tag usada pra identificar o player
    public float lookRadius = 5f;       // raio de detecção
    public float rotationSpeed = 5f;    // suavidade da rotação

    private Transform player;
    private bool isLookingAtPlayer = false;

    // Procura o player pela tag
    void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning($"[NPCLookAtPlayer] Nenhum objeto com a tag '{playerTag}' foi encontrado na cena!");
        }
    }


    // Calcula a distância até o player
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= lookRadius)
        {
            isLookingAtPlayer = true;
            LookAt();
        }
        else
        {
            isLookingAtPlayer = false;
        }
    }

    void LookAt()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
