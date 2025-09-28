using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Splines.ExtrusionShapes;
public class GameUiManager : MonoBehaviour
{
    public GameObject PMenu;
    public CanvasGroup PGroup;
    public Material DayTimeFilter;
    public Color Day;
    public Color Night;
    
    [SerializeField] RectTransform Sunicon;
    [SerializeField] RectTransform Moonicon;
    public float BgTopPosY, BgmiddlePosY;
    public float UpTopPosY, UpmiddlePosY;
    public float TweenDur;
    public float DNCooldown;
    float DNTimer;
    public bool DayTime;
    //pause menu ani
    [SerializeField] RectTransform Bg;
    [SerializeField] RectTransform B1;
    [SerializeField] RectTransform B2;
    [SerializeField] RectTransform B3;
    public float BtTopPosx=-555;
    
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PMenu.SetActive(true);
            
            PauseMenuani();
            Time.timeScale = 0;
            
        }
    }
    public void PauseMenuani()
    {
        PGroup.DOFade(1, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        Bg.DOAnchorPosX(BgmiddlePosY, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        B1.DOAnchorPosX(48.14868f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        B2.DOAnchorPosX(439.995f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        B3.DOAnchorPosX(518.995f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        
    }
    public async void Back()
    {
        Time.timeScale = 1;
        Bg.DOAnchorPosX(BgTopPosY, TweenDur).SetEase(Ease.InFlash).SetUpdate(true);
        B1.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true);
        B2.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        B3.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        await PGroup.DOFade(0, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true).AsyncWaitForCompletion();
        
        
        
        PMenu.SetActive(false);
        
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
