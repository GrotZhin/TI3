using UnityEngine;
using DG.Tweening;
using TMPro;
using Sfx;
using System;
public class InspectUiManager : MonoBehaviour
{

    public GameObject inspCanvas;
    public GameObject MainUi;
    public CanvasGroup gp;
    bool inspactive = false;
    public Transform DoSize;
    public float tweendur = 0.8f;

    public event Action OnInspectClosed;

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
    
    public async void OpenMenu()
    {
        if (!inspactive)
        {
            MainUi.SetActive(false);
            ScaleAni();
            gp.DOFade(1, 0.2f).SetEase(Ease.OutFlash);
            inspCanvas.SetActive(true);
            
            inspactive = true;
        }
        else if (inspactive)
        {
            
            await DoSize.DOScale(0.3f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
            MainUi.SetActive(true);
            gp.DOFade(0, 0.4f).SetEase(Ease.OutFlash).SetUpdate(true);
            inspCanvas.SetActive(false);
            ScaleAni();
            inspactive = false;

            OnInspectClosed?.Invoke();
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
        ScaleAni();
        MainUi.SetActive(true);
        inspactive = false;

        InspectRocks.ReleaseInspect();
        OnInspectClosed?.Invoke();
    }


    public async void ScaleAni()
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