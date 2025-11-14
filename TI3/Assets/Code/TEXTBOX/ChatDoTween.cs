using UnityEngine;
using DG.Tweening;

public class ChatDoTween : MonoBehaviour
{
    public Transform Chat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Chat.DOScaleX(1,0.3f).SetEase(Ease.InFlash).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
