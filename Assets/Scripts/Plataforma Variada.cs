using System.Collections;
using UnityEngine;

public class PlataformaVariada : MonoBehaviour
{
    public bool plataformYellow;
    public bool plataformRed;
    public bool plataformBlue;
    public bool plataformGreen;
    public float timerPlataform;

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    private bool isFading = false;
    private bool playerTouched = false;  
    private Coroutine mudarCorCoroutine;
    public int platformPoints = 0;

    private void Start()
    {
        plataformYellow = false;
        plataformRed = false;
        plataformBlue = false;
        plataformGreen = false;

        EscolherCorInicial();
        mudarCorCoroutine = StartCoroutine(MudarCorPeriodicamente());
    }

    private void EscolherCorInicial()
    {
        int randomColor = Random.Range(0, 4);
        DefinirCor(randomColor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!playerTouched && collision.collider.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(platformPoints);
            playerTouched = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                if (!CoresIguais(player))
                {
                    StartCoroutine(player.morte());
                }
                else if (!isFading)
                {
                    StartCoroutine(FadeOutAndDestroy());
                }
            }

            if (mudarCorCoroutine != null)
            {
                StopCoroutine(mudarCorCoroutine);
            }
        }
    }

    private bool CoresIguais(Player player)
    {
        return (plataformRed && player.isRed) ||
               (plataformBlue && player.isBlue) ||
               (plataformGreen && player.isGreen) ||
               (plataformYellow && player.isYellow);
    }

    IEnumerator FadeOutAndDestroy()
    {
        isFading = true;
        yield return new WaitForSeconds(1f);
        float fadeDuration = 0.5f;
        float fadeStep = 0.05f;
        float timeElapsed = 0f;

        Color originalColor = spriteRenderer.color;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timeElapsed += fadeStep;
            yield return new WaitForSeconds(fadeStep);
        }

        Destroy(gameObject);
    }

    IEnumerator MudarCorPeriodicamente()
    {
        while (!playerTouched)
        {
            yield return new WaitForSeconds(timerPlataform);
            int randomColor = Random.Range(0, 4);
            DefinirCor(randomColor);
        }
    }

    private void DefinirCor(int colorIndex)
    {
        plataformRed = false;
        plataformBlue = false;
        plataformGreen = false;
        plataformYellow = false;

        switch (colorIndex)
        {
            case 0:
                plataformRed = true;
                spriteRenderer.color = new Color(1f, 0.45f, 0.45f);
                break;
            case 1:
                plataformBlue = true;
                spriteRenderer.color = new Color(0.45f, 0.72f, 1f);
                break;
            case 2:
                plataformGreen = true;
                spriteRenderer.color = new Color(0.48f, 1f, 0.45f);
                break;
            case 3:
                plataformYellow = true;
                spriteRenderer.color = new Color(1f, 0.93f, 0.45f);
                break;
        }
    }
}
