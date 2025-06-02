using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonHover : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    public float rotationAmount = 5f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        originalRotation = rectTransform.rotation;
    }

    public void OnPointerEnter()
    {
        rectTransform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit()
    {
        rectTransform.localScale = originalScale;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * 5f) * rotationAmount;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}