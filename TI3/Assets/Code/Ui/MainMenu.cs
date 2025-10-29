using Sfx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    float sounds = 1.0f;
    float music = 1.0f;
    
     
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] GameObject optionMenu;
    public GameObject controls;

    // just copied the pause ani var's
    public GameObject PMenu;
    
    public GameObject OMenu;
    public CanvasGroup PGroup;
    public CanvasGroup OGroup;
    public float BgTopPosY, BgmiddlePosY;
    public float UpTopPosY, UpmiddlePosY;
    public float TweenDur;
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
    void Start()
    {
        sounds = PlayerPrefs.GetFloat("Sounds", 1.0f);
        music = PlayerPrefs.GetFloat("Music", 1.0f);
        controls.SetActive(false);
        soundSlider.value = sounds;
        musicSlider.value = music;
    }
    public void PlayGame(string sceneName)
    {
        // Load the specified game scene
        soundManager.PlaySound(SoundType.Menu);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void OpenMenu()
    {
        optionMenu.SetActive(true);
        soundManager.PlaySound(SoundType.Menu);
        
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
    public void SavePreferences()
    {
        // Save player preferences
        PlayerPrefs.SetFloat("Sounds", sounds);
        PlayerPrefs.SetFloat("Music", music);
    }
    public void SetSoundVolume()
    {
        sounds = soundSlider.value;
        // Update sound volume in the game
        AudioListener.volume = sounds;
    }
    public void SetMusicVolume()
    {
        music = musicSlider.value;
        // Update music volume in the game
        AudioListener.volume = music;
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
