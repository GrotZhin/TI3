
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class SceneChange : MonoBehaviour
{
    [SerializeField] RectTransform trans;
    [SerializeField] float tweendur;
    public CanvasGroup fade;
    [SerializeField] string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade.DOFade(0, 0.1f).SetEase(Ease.OutFlash).SetUpdate(true);
    }
    // Update is called once per frame
    async void OnTriggerEnter(Collider other)
    {
        fade.DOFade(1, 1).SetEase(Ease.OutFlash).SetUpdate(true);
        await trans.DOAnchorPosX(-2562, tweendur).SetUpdate(true).AsyncWaitForCompletion();
        
        if (other.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(sceneName);

    }
   
}
