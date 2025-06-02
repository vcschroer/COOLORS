using UnityEngine;
using UnityEngine.UI;

public class FundoController : MonoBehaviour
{
    public RawImage fundo;
    public float x;
    public float y;

    public Player player;
    private bool playerIsDead = false;

    public bool colorChange; 

    private Color[] cores = new Color[4]
    {
        new Color(1f, 0.45f, 0.45f),   
        new Color(0.45f, 0.72f, 1f),  
        new Color(0.48f, 1f, 0.45f),   
        new Color(1f, 0.93f, 0.45f)   
    };

    private int corAtualIndex = 0;
    private float tempoTransicao = 2f; 
    private float t = 0f;

    private void Start()
    {
        if (player != null)
        {
            fundo.color = player.sr.color;
        }
    }

    void Update()
    {
        fundo.uvRect = new Rect(fundo.uvRect.position + new Vector2(x, y) * Time.deltaTime, fundo.uvRect.size);

        if (colorChange)
        {
            TrocarCorSuavemente();
        }
        else if (player != null && !playerIsDead)
        {
            fundo.color = player.sr.color;
        }
    }

    void TrocarCorSuavemente()
    {
        Color corAtual = cores[corAtualIndex];
        Color proximaCor = cores[(corAtualIndex + 1) % cores.Length];

        t += Time.deltaTime / tempoTransicao;
        fundo.color = Color.Lerp(corAtual, proximaCor, t);

        if (t >= 1f)
        {
            corAtualIndex = (corAtualIndex + 1) % cores.Length;
            t = 0f;
        }
    }

    public void OnPlayerDeath()
    {
        playerIsDead = true;
    }
}
