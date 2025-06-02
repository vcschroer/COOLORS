using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class StarFinal : MonoBehaviour
{
    public int proximaFase = 2;
    public string cenaMenuFases = "Fases";
    public ScreenTransition screenTransitionScript;

    private bool jaAtivado = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (jaAtivado) return;

        if (other.CompareTag("Player"))
        {
            jaAtivado = true;
            StartCoroutine(FinalizarFase());
        }
    }

    private IEnumerator FinalizarFase()
    {
        int faseAtual = PlayerPrefs.GetInt("faseAtual", 1);
        if (proximaFase > faseAtual)
        {
            PlayerPrefs.SetInt("faseAtual", proximaFase);
        }

        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(screenTransitionScript.StartTransition());

        SceneManager.LoadScene(cenaMenuFases);
    }

    private IEnumerator FadeOut()
    {
        float duration = 0.2f;
        float elapsed = 0f;
        Color corOriginal = spriteRenderer.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            spriteRenderer.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 0f);
    }
}
