using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrollTrigger : MonoBehaviour
{
    public CameraScroll cameraScrollScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraScrollScript.StartScrolling();
            Destroy(gameObject); 
        }
    }
}
