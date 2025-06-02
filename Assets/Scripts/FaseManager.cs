using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class FaseManager : MonoBehaviour
{
    public Button fase1Button, fase2Button, fase3Button;
    public Image cadeadoFase2, cadeadoFase3;
    public TextMeshProUGUI textoFase2, textoFase3;

    private int faseAtual = 1;
    public ScreenTransition screenTransitionScript;

    void Start()
    {
        faseAtual = PlayerPrefs.GetInt("faseAtual", 1);

        AtualizarFases();

        fase1Button.onClick.AddListener(() => StartCoroutine(CarregarFase("Fase 1")));
        fase2Button.onClick.AddListener(() => StartCoroutine(CarregarFase("Fase 2")));
        fase3Button.onClick.AddListener(() => StartCoroutine(CarregarFase("Fase 3")));
    }

    void AtualizarFases()
    {
        fase1Button.interactable = true;

        bool fase2Liberada = faseAtual >= 2;
        fase2Button.interactable = fase2Liberada;
        cadeadoFase2.gameObject.SetActive(!fase2Liberada);
        textoFase2.gameObject.SetActive(fase2Liberada);

        bool fase3Liberada = faseAtual >= 3;
        fase3Button.interactable = fase3Liberada;
        cadeadoFase3.gameObject.SetActive(!fase3Liberada);
        textoFase3.gameObject.SetActive(fase3Liberada);
    }

    IEnumerator CarregarFase(string nomeFase)
    {
        yield return StartCoroutine(screenTransitionScript.StartTransition());
        SceneManager.LoadScene(nomeFase);
    }
}
