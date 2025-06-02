using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlataformaBase : MonoBehaviour
{
    public bool plataformYellow;
    public bool plataformRed;
    public bool plataformBlue;
    public bool plataformGreen;

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    private bool isFading = false;
    public int platformPoints = 0;
    private bool playerTouched = false;

    void Start()
    {
        int randomColor = Random.Range(0, 4);

        plataformYellow = false;
        plataformRed = false;
        plataformBlue = false;
        plataformGreen = false;

        switch (randomColor)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (!playerTouched)
            {
                ScoreManager.instance.AddScore(platformPoints);
                playerTouched = true;
            }
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
}