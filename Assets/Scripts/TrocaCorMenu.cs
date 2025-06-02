using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrocaCorMenu : MonoBehaviour
{
    public TextMeshProUGUI titulo;
    public float transitionTime;

    private Color[] colors = new Color[]
    {
        new Color(1f, 0.45f, 0.45f), 
        new Color(0.45f, 0.72f, 1f),
        new Color(0.48f, 1f, 0.45f),
        new Color(1f, 0.93f, 0.45f)  
    };

    private int currentColorIndex = 0;

    void Start()
    {
        if (titulo == null)
        {
            titulo = GetComponent<TextMeshProUGUI>();
        }

        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        while (true)
        {
            Color startColor = titulo.color;
            Color targetColor = colors[(currentColorIndex + 1) % colors.Length];

            float elapsedTime = 0f;
            while (elapsedTime < transitionTime)
            {
                titulo.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            titulo.color = targetColor;
            currentColorIndex = (currentColorIndex + 1) % colors.Length;

            yield return new WaitForSeconds(1f); 
        }
    }
}

