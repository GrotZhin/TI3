using UnityEngine;
using DG.Tweening;

public class CarrotAni : MonoBehaviour
{
    public float TweenDur;


    void Start()
    {
        Vector3 targetPosition = transform.localPosition + new Vector3(0, 0.4f, 0);
        transform.DOLocalMoveY(targetPosition.y, TweenDur).SetLoops(-1, LoopType.Yoyo);
    }

}
