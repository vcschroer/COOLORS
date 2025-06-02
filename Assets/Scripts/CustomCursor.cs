using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D customCursor; // Arraste sua imagem de cursor aqui no Inspector
    public Vector2 hotSpot = new Vector2(0, 0); // Ponto de "clique" do cursor

    void Start()
    {
        // Define o cursor personalizado
        Cursor.SetCursor(customCursor, hotSpot, CursorMode.Auto);
    }

    void OnDisable()
    {
        // Restaura o cursor padrão quando o objeto é desativado
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
