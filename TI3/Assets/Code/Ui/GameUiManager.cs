using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Splines.ExtrusionShapes;
public class GameUiManager : MonoBehaviour
{
    public Material DayTimeFilter;
    public Color Day;
    public Color Night;
    
    [SerializeField] RectTransform Sunicon;
    [SerializeField] RectTransform Moonicon;
    public float UpTopPosY, UpmiddlePosY;
    public float TweenDur;
    public float DNCooldown;
    float DNTimer;
    public bool DayTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.To(() => DayTimeFilter.GetColor("_ColorPallete"), x => DayTimeFilter.SetColor("_ColorPallete", x), Day, TweenDur).SetUpdate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (DNTimer > 0)
        {
            DNTimer -= Time.deltaTime;
        }
        if (DNTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DNTimer = DNCooldown;

                if (DayTime)
                {
                    
                    
                    SunAni();
                    DayTime = false;
                }
                else
                {
                     
                    MoonAni();
                    DayTime = true;
                }
            }
        }
    }
    async void SunAni()
    {
        Sunicon.DOAnchorPosY(UpmiddlePosY, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        Sunicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        Moonicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        DOTween.To(() => DayTimeFilter.GetColor("_ColorPallete"), x => DayTimeFilter.SetColor("_ColorPallete", x), Day, TweenDur).SetUpdate(true);
        await Moonicon.DOAnchorPosY(UpTopPosY, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
    }

    async void MoonAni()
    {
        Moonicon.DOAnchorPosY(UpmiddlePosY, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        Sunicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        Moonicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        DOTween.To(() => DayTimeFilter.GetColor("_ColorPallete"), x => DayTimeFilter.SetColor("_ColorPallete", x), Night, TweenDur).SetUpdate(true);
        await Sunicon.DOAnchorPosY(UpTopPosY, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();
    }
}
