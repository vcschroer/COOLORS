using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScrollRectSlider : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float transitionTime = 0.3f;

    private int currentPage = 0;
    private int totalPages = 2;
    private float[] pagePositions;

    private void Start()
    {
        pagePositions = new float[totalPages];
        for (int i = 0; i < totalPages; i++)
        {
            pagePositions[i] = (float)i / (totalPages - 1);
        }
        NextPage();
    }

    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            StopAllCoroutines();
            StartCoroutine(SmoothScrollTo(pagePositions[currentPage]));
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            StopAllCoroutines();
            StartCoroutine(SmoothScrollTo(pagePositions[currentPage]));
        }
    }

    private System.Collections.IEnumerator SmoothScrollTo(float target)
    {
        float elapsedTime = 0f;
        float start = scrollRect.horizontalNormalizedPosition;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(start, target, elapsedTime / transitionTime);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = target;
    }
}
