using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class GameUiManager : MonoBehaviour
{
    public GameObject PMenu;

    public GameObject OMenu;
    public CanvasGroup PGroup;
    public CanvasGroup OGroup;
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

    public float BlockerCooldown;
    float BlockerTimer;
    [SerializeField] RectTransform Blocker;
    public static bool DayTime;
    //pause menu ani
    [SerializeField] RectTransform Bg;
    [SerializeField] RectTransform B1;
    [SerializeField] RectTransform B2;
    [SerializeField] RectTransform B3;
    //Options Menu Ani
    [SerializeField] RectTransform BgOptions;
    [SerializeField] RectTransform BC;
    [SerializeField] RectTransform BV;

    public float BtTopPosx = -555;
    public static GameUiManager gameUiManager;

  public GameObject controls;
    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sfx;
    [SerializeField] Slider music;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    
        Color startColor = Shader.GetGlobalColor("_ColorPallete"); DOTween.To(() => startColor, x => Shader.SetGlobalColor("_ColorPallete", x), Day, 0.5f).SetEase(Ease.InOutSine).SetUpdate(true);
        
        if (GM.firtsStart) SunAni();
     
        else
        {
            if (!GM.DayTime) SunAni();
            
            if (GM.DayTime) MoonAni();
        }
        GM.firtsStart = false;
        if(sfx!=null){
            float value = PlayerPrefs.GetFloat("Sounds",1);
            float temp = Mathf.Log10(value) * 20;
            if (value == 0) temp = -60;
            audioMixer.SetFloat("sfxVolume", temp);
            sfx.value = value;
            sfx.onValueChanged.AddListener(SetSfx);
        }
        if(music!=null){
            float value = PlayerPrefs.GetFloat("Music",1);
            float temp = Mathf.Log10(value) * 20;
            if (value == 0) temp = -60;
            audioMixer.SetFloat("musicVolume", temp);
            music.value = value;
            music.onValueChanged.AddListener(SetMusic);
        }
        if(AnalyticsController.Self != null) AnalyticsController.Self.StartAnly("DayNightToggle", 0);
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
                if(AnalyticsController.Self != null) AnalyticsController.Self.UpdateAnlyValue("DayNightToggle");
                DNTimer = DNCooldown;

                if (GM.DayTime)
                {
                    SunAni();
                    GM.DayTime = false;
                }
                else
                {
                    MoonAni();
                    GM.DayTime = true;
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
    public void BlockerDo()
    {
        Blocker.DOAnchorPosX(410.2825f, 0.1f).SetUpdate(true)
        .SetAutoKill(false)
            .OnComplete(async () =>
            {
                await Blocker.DOScale(2, 0.8f).SetUpdate(true).AsyncWaitForCompletion();
                Blocker.DOAnchorPosX(99999, 0.1f).SetUpdate(true);
            });
    }
    public void PauseMenuani()
    {
        BlockerDo();

        PGroup.DOFade(1, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        Bg.DOAnchorPosX(BgmiddlePosY, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        B1.DOAnchorPosX(48.14868f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        B2.DOAnchorPosX(439.995f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        B3.DOAnchorPosX(518.995f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);

    }
    public void Back()
    {
        Debug.Log("comecou");
        Time.timeScale = 1;
        GetColectable.ReleaseInspect();
        Debug.Log("acabou");
    }
    public async void BackAni()
    {
        BlockerDo();
        Bg.DOAnchorPosX(BgTopPosY, TweenDur).SetEase(Ease.InFlash).SetUpdate(true);
        B1.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true);
        B2.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        B3.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        await PGroup.DOFade(0, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true).AsyncWaitForCompletion();

        PMenu.SetActive(false);

    }
    public void OptionsMenuani()
    {
        BlockerDo();
        OMenu.SetActive(true);
        OGroup.DOFade(1, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        BgOptions.DOAnchorPosX(400, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);

        BC.DOAnchorPosX(439.995f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);
        BV.DOAnchorPosX(518.995f, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true);

    }

    public async void Back2Menu()
    {
        BlockerDo();
        PMenu.SetActive(true);
        BgOptions.DOAnchorPosX(BgTopPosY, TweenDur).SetEase(Ease.InFlash).SetUpdate(true);
        BC.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        BV.DOAnchorPosX(BtTopPosx, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        await OGroup.DOFade(0, TweenDur).SetEase(Ease.OutFlash).SetUpdate(true).AsyncWaitForCompletion();



        OMenu.SetActive(false);
        if(music!=null){
            PlayerPrefs.SetFloat("Music",music.value);
        }
        if(sfx!=null){
            PlayerPrefs.SetFloat("Sounds",sfx.value);
        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
        
    }
    async void SunAni()
    {
        Sunicon.DOAnchorPosY(UpmiddlePosY, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        Sunicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        Moonicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        Color startColor = Shader.GetGlobalColor("_ColorPallete"); DOTween.To(() => startColor, x => Shader.SetGlobalColor("_ColorPallete", x), Day, 0.5f).SetEase(Ease.InOutSine).SetUpdate(true);
        await Moonicon.DOAnchorPosY(UpTopPosY, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();

    }

    async void MoonAni()
    {
        Moonicon.DOAnchorPosY(UpmiddlePosY, TweenDur).SetEase(Ease.InOutFlash).SetUpdate(true);
        Sunicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        Moonicon.DORotate(new Vector2(0, 360), TweenDur, RotateMode.FastBeyond360);
        Color startColor = Shader.GetGlobalColor("_ColorPallete"); DOTween.To(() => startColor, x => Shader.SetGlobalColor("_ColorPallete", x), Night, 0.5f).SetEase(Ease.InOutSine).SetUpdate(true);
        await Sunicon.DOAnchorPosY(UpTopPosY, TweenDur).SetEase(Ease.InOutCubic).SetUpdate(true).AsyncWaitForCompletion();

    }
    public void SetSfx(float value){
        float temp = Mathf.Log10(value) * 20;
        if (value == 0) temp = -60;
        audioMixer.SetFloat("sfxVolume", temp);
    }
    public void SetMusic(float value){
        float temp = Mathf.Log10(value) * 20;
        if (value == 0) temp = -60;
        audioMixer.SetFloat("musicVolume", temp);
    }
 public void Controls()
    {
        controls.SetActive(true);
        Back2Menu();
    }
    public void conback()
    {
        controls.SetActive(false);
        OptionsMenuani();
    }
}
