﻿using UnityEngine;

public class InspectRocks : MonoBehaviour
{
    [Header("Textos do Item")]
    [TextArea] public string rockMetalText;
    [TextArea] public string informativeHeader;
    [TextArea] public string informativeBody;

    [Header("Configurações")]
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;
    public string playerTag = "Player";
    public GameObject Rock;

    private bool inspectActive = false;
    private Transform player;
    private static PlayerMove cachedPlayerMovement;

    private bool justOpened = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning($"[InspectObject] Nenhum objeto com a tag '{playerTag}' foi encontrado na cena!");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

       
        if (!inspectActive)
        {
            if (distance <= interactRadius && Input.GetKeyDown(interactKey))
            {
                
                OpenInspectMenu();
                return;
            }

             if (distance > interactRadius)
            {
                Rock.SetActive(false);
            }
            //     Debug.Log("ta flikando aq");  

            return;
        }

       
        if (inspectActive)
        {
            if (Input.GetKeyDown(interactKey))
            {
                CloseMenu();
                return;
            }
        }
    }
    void OpenInspectMenu()
    {
        InspectUiManager manage = FindFirstObjectByType<InspectUiManager>();

        if (manage == null)
        {
            Debug.LogError("[InspectObject] Nenhum InspectUiManager encontrado na cena!");
            return;
        }

        manage.SetTexts(rockMetalText, informativeHeader, informativeBody);
        manage.OpenMenu();

        manage.OnInspectClosed += ReleaseInspect;

        inspectActive = true;

        cachedPlayerMovement = player.GetComponent<PlayerMove>();
        if (cachedPlayerMovement != null)
            cachedPlayerMovement.enabled = false;

        Rock.SetActive(true);
    }
    void CloseMenu()
    {
        InspectUiManager manage = FindFirstObjectByType<InspectUiManager>();

        if (manage != null)
        {
            manage.Back();
            manage.OnInspectClosed -= ReleaseInspect;
        }

        inspectActive = false;

        if (cachedPlayerMovement != null)
            cachedPlayerMovement.enabled = true;
    }

    public void ReleaseInspect()
    {
        inspectActive = false;
        
        if (cachedPlayerMovement != null)
            cachedPlayerMovement.enabled = true;

        cachedPlayerMovement = null;

        InspectUiManager manage = FindFirstObjectByType<InspectUiManager>();
        if (manage != null)
            manage.OnInspectClosed -= ReleaseInspect;
            
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}