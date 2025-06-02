using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BOTOESMENU : MonoBehaviour
{
    public ScreenTransition screenTransitionScript; 

    void Start()
    {
    }
    public void Jogar()
    {
        
        StartCoroutine(PlayTransitionAndLoadScene());
    }

    IEnumerator PlayTransitionAndLoadScene()
    {
        
        yield return StartCoroutine(screenTransitionScript.StartTransition());

        
        SceneManager.LoadScene("Fases");
    }

    public void sair()
    {
        StartCoroutine(PlayTransitionAndQuit());

    }

    IEnumerator PlayTransitionAndQuit()
    {
        yield return StartCoroutine(screenTransitionScript.StartTransition());

        Application.Quit();

    }

    public void InfiniteButton()
    {
        StartCoroutine(InfinitePlay());
    }

    IEnumerator InfinitePlay()
    {
        yield return StartCoroutine(screenTransitionScript.StartTransition());
        SceneManager.LoadScene("Jogo Infinito");
    }


}
