using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 

public class ScreenTransition : MonoBehaviour
{
    public int columns;
    public int rows;
    public float squareSize;
    public float delayBetweenDiagonals;
    public GameObject squarePrefab;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            StartCoroutine(StartOpeningTransition());
        }
    }
    //transição de entrada
    public IEnumerator StartOpeningTransition()
    {
        //ele vai criar o gridcontainer q o proprio nome ja diz qq faz
        GameObject gridContainer = CreateGridContainer();
        // vai criar os quadrados em cada posição do grid
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject square = Instantiate(squarePrefab, gridContainer.transform);
                //vai ajustar o tamanho dos quadrados
                RectTransform rect = square.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(squareSize, squareSize);
                //vai bota eles no lugar certo
                rect.anchoredPosition = new Vector2(
                    (x - columns / 2) * squareSize,
                    (y - rows / 2) * squareSize
                );
                //aqui é pra eles ta no tamanho certo e ta ativo
                square.transform.localScale = Vector3.one;
                square.SetActive(true);
            }
        }
        //vai indo em diagonal ali no grid
        for (int sum = columns + rows - 2; sum >= 0; sum--)
        {
            for (int x = columns - 1; x >= 0; x--)
            {
                int y = sum - x;
                if (y >= 0 && y < rows)
                {
                    //aqui ele vai chama pra cada quadradinho que sumir bota de diminuir, no caso da pra botar fade e outros efeitos
                    int index = x * rows + y;
                    GameObject square = gridContainer.transform.GetChild(index).gameObject;
                    StartCoroutine(ShrinkAndDisable(square));
                }
            }
            //aqui é pra esperar o delay obviamente
            yield return new WaitForSeconds(delayBetweenDiagonals);
        }
        
        yield return new WaitForSeconds(1f);
        Destroy(gridContainer);
    }

    //essa tem a mesma logica que a de entrada so que é pra saida
    public IEnumerator StartTransition()
    {
        GameObject gridContainer = CreateGridContainer();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject square = Instantiate(squarePrefab, gridContainer.transform);
                RectTransform rect = square.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(squareSize, squareSize);
                rect.anchoredPosition = new Vector2(
                    (x - columns / 2) * squareSize,
                    (y - rows / 2) * squareSize
                );
                square.transform.localScale = Vector3.zero;
                square.SetActive(true);
            }
        }

        for (int sum = 0; sum <= columns + rows - 2; sum++)
        {
            for (int x = 0; x < columns; x++)
            {
                int y = sum - x;
                if (y >= 0 && y < rows)
                {
                    int index = x * rows + y;
                    GameObject square = gridContainer.transform.GetChild(index).gameObject;
                    StartCoroutine(GrowAndEnable(square));
                }
            }
            yield return new WaitForSeconds(delayBetweenDiagonals);
        }

        yield return new WaitForSeconds(0.12f);
        
    }
    //cria o grid
    private GameObject CreateGridContainer()
    {
        //faz ele como filhote
        GameObject gridContainer = new GameObject("GridContainer");
        gridContainer.transform.SetParent(transform);
        //bota recttransform pra ele funcionar como ui e ter o tamanho da tela
        RectTransform gridRect = gridContainer.AddComponent<RectTransform>();
        gridRect.sizeDelta = new Vector2(Screen.width, Screen.height);
        //centraliza pra fica no mei
        gridRect.anchorMin = Vector2.zero;
        gridRect.anchorMax = Vector2.one;
        gridRect.pivot = new Vector2(0.5f, 0.5f);
        gridRect.anchoredPosition = Vector2.zero;
        return gridContainer;
    }
    //animacao de fica grande
    private IEnumerator GrowAndEnable(GameObject square)
    {
        //vai iniciar nanico e fica grande
        float duration = 0.3f;
        float t = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        //usa lerp pra faze ele crescer 
        while (t < duration)
        {
            t += Time.deltaTime;
            square.transform.localScale = Vector3.Lerp(startScale, targetScale, t / duration);
            yield return null;
        }
        //pra ter ctz q ele vai ta no tamanho certo
        square.transform.localScale = targetScale;
    }
    //mesma logica mas pra fica pequenino
    private IEnumerator ShrinkAndDisable(GameObject square)
    {
        float duration = 0.3f;
        float t = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 targetScale = Vector3.zero;

        while (t < duration)
        {
            t += Time.deltaTime;
            square.transform.localScale = Vector3.Lerp(startScale, targetScale, t / duration);
            yield return null;
        }

        square.SetActive(false);
    }
}
