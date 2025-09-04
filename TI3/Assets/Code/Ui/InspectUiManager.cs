using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
public class InspectUiManager : MonoBehaviour
{
    public GameObject inspCanvas;
    
    public CanvasGroup gp;
    bool inspactive = false;
    public Transform DoSize;
    public float tweendur = 0.8f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inspCanvas.SetActive(false);
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inspactive)
            {
                gp.DOFade(1, 0.2f).SetEase(Ease.OutFlash);
                inspCanvas.SetActive(true);
                await ScaleAni();
                inspactive = true;
            }
            else if (inspactive)
            {
                 await DoSize.DOScale(0.3f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
                 gp.DOFade(0, 0.4f).SetEase(Ease.OutFlash).SetUpdate(true);
                inspCanvas.SetActive(false);
                await ScaleAni();
                inspactive = false;
            }
        }

        


    }

    public async void Back()
    {
        await DoSize.DOScale(0.3f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
                 gp.DOFade(0, 0.4f).SetEase(Ease.OutFlash).SetUpdate(true);
        inspCanvas.SetActive(false);
                await ScaleAni();
                inspactive = false;
    }
    
    
    public async Task ScaleAni()
    {
        if (!inspactive)
        {
            await DoSize.DOScale(1.2f, tweendur).SetEase(Ease.OutFlash).SetUpdate(true).AsyncWaitForCompletion();
        }
        else if (inspactive)
        {
            await DoSize.DOScale(1f, 0.2f).SetEase(Ease.InFlash).SetUpdate(true).AsyncWaitForCompletion();
        }
    }
}
