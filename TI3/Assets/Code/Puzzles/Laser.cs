using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    LineRenderer lr;
    [Header("Laser Settings")]
    [SerializeField] float maxDistance = 300f;
    [SerializeField] int maxReflections = 5;
    [SerializeField] bool canReflectOnAnything = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Cast(transform.position, transform.forward);
    }

    void Cast(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, position);
        for (int i = 0; i < maxReflections; i++)
        {
            Ray ray = new(position, direction);
            if (Physics.Raycast(ray, out RaycastHit rayHit, maxDistance, 1, QueryTriggerInteraction.Ignore))
            {
                position = rayHit.point;
                direction = Vector3.Reflect(direction, rayHit.normal);
                lr.SetPosition(i + 1, rayHit.point);
                if (!canReflectOnAnything && !rayHit.collider.CompareTag("Mirror"))
                {
                    if (rayHit.collider.CompareTag("goal"))
                    {
                        Debug.Log("vc venceulinda");
                        break;
                        
                    }
                    lr.positionCount = i + 2;
                    break;
                }

            }
            else
            {
                lr.positionCount = i + 2;
                lr.SetPosition(i + 1, position + direction * 300);
                break;
            }
        }

    }
}
