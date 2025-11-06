using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Referências")]
    public Image buttonImage;
    public TMP_Text buttonText;

    [Header("Cores")]
    public Color normalButtonColor = Color.black;
    public Color normalTextColor = Color.yellow;
    public Color hoverButtonColor = Color.yellow;
    public Color hoverTextColor = Color.black;
    public Color clickButtonColor = Color.gray;
    public Color clickTextColor = Color.black;

    [Header("Escala")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    public float scaleSpeed = 10f;

    private bool isHovered = false;

    private void Start()
    {
        buttonImage.color = normalButtonColor;
        buttonText.color = normalTextColor;
        transform.localScale = normalScale;
    }

    private void Update()
    {
        Vector3 targetScale = isHovered ? hoverScale : normalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        buttonImage.color = hoverButtonColor;
        buttonText.color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        buttonImage.color = normalButtonColor;
        buttonText.color = normalTextColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.color = clickButtonColor;
        buttonText.color = clickTextColor;
    }
}