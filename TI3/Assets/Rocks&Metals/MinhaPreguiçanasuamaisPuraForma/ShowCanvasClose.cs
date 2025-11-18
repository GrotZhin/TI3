using UnityEngine;
using DG.Tweening;

public class ShowCanvasClose : MonoBehaviour
{
    public CanvasGroup G;
    public float TweenDur;
    public string playerTag = "Player";
    public float activationDistance = 2f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        player = playerObj.transform;
        G.alpha = 0f;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= activationDistance)
        {
            G.DOFade(1f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        }
        else
        {
            G.DOFade(0f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        }
    }
}
