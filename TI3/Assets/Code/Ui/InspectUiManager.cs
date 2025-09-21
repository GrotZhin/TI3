using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using Sfx;
public class InspectUiManager : MonoBehaviour
{
    public GameObject inspCanvas;
    public GameObject MainUi;
    public CanvasGroup gp;
    bool inspactive = false;
    public Transform DoSize;
    public float tweendur = 0.8f;

    [Header("Relativos aos coletaveis")]

    public GameObject rockMetalTextUI;
    public GameObject informativeHeaderUI;
    public GameObject informativeBodyUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inspCanvas.SetActive(false);
    }

    // Update is called once per frame
    async void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     OpenMenu();
        // }
    }
    public async void OpenMenu()
    {
        if (!inspactive)
        {
            MainUi.SetActive(false);
            gp.DOFade(1, 0.2f).SetEase(Ease.OutFlash);
            inspCanvas.SetActive(true);
            await ScaleAni();
            inspactive = true;
        }
        else if (inspactive)
        {
            
            await DoSize.DOScale(0.3f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
            MainUi.SetActive(true);
            gp.DOFade(0, 0.4f).SetEase(Ease.OutFlash).SetUpdate(true);
            inspCanvas.SetActive(false);
            await ScaleAni();
            inspactive = false;
        }
    }

    // necessario pra mudar o texto
    public void SetTexts(string rock, string header, string body)
    {
        rockMetalTextUI.GetComponent<TMP_Text>().text = rock;
        informativeHeaderUI.GetComponent<TMP_Text>().text = header;
        informativeBodyUI.GetComponent<TMP_Text>().text = body;
    }


    public async void Back()
    {

        await DoSize.DOScale(0.3f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
        gp.DOFade(0, 0.4f).SetEase(Ease.OutFlash).SetUpdate(true);
        inspCanvas.SetActive(false);
        await ScaleAni();
        MainUi.SetActive(true);
        inspactive = false;

        //necessario pra travar e nao ativar outro trigger do coletavel
        GetColectable.ReleaseInspect();
    }
    
    
    public async Task ScaleAni()
    {
        if (!inspactive)
        {
            soundManager.PlaySound(SoundType.AmethystPopUp);
            await DoSize.DOScale(1.2f, tweendur).SetEase(Ease.OutCubic).SetEase(Ease.OutFlash).SetUpdate(true).AsyncWaitForCompletion();
        }
        else if (inspactive)
        {
            await DoSize.DOScale(0.3f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
        }
    }
}
