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

            if (distance >interactRadius)
                Rock.SetActive(false);

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
        InspectUiManager manage = FindObjectOfType<InspectUiManager>();

        if (manage != null)
        {
            
            manage.SetTexts(rockMetalText, informativeHeader, informativeBody);
            manage.OpenMenu();
                    
          
            Debug.LogWarning($"[InspectObject] Nenhum objeto com a tag '{playerTag}' foi encontrado na cena!");
       
            manage.OnInspectClosed += ReleaseInspect;

            inspectActive = true;
            justOpened = true;

            cachedPlayerMovement = player.GetComponent<PlayerMove>();
            if (cachedPlayerMovement != null)
                cachedPlayerMovement.enabled = false;
        }
        if(InspectUiManager.inspactive == false)
        {
            Rock.SetActive(false);
        }
        else
        {
            Rock.SetActive(true);
        }
    }

    void CloseMenu()
    {
        InspectUiManager manage = FindObjectOfType<InspectUiManager>();

        if (manage != null)
        {
            manage.Back();
            manage.OnInspectClosed -= ReleaseInspect;

            inspectActive = false;

            cachedPlayerMovement = player.GetComponent<PlayerMove>();
            if (cachedPlayerMovement != null)
                cachedPlayerMovement.enabled = true;
        }
    }

    public void ReleaseInspect()
    {
        inspectActive = false;
        
        if (cachedPlayerMovement != null)
            cachedPlayerMovement.enabled = true;

        cachedPlayerMovement = null;

        InspectUiManager manage = FindObjectOfType<InspectUiManager>();
        if (manage != null)
            manage.OnInspectClosed -= ReleaseInspect;
            
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}