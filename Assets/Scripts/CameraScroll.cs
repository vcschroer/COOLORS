using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed;        
    private bool shouldScroll = false;
    public Transform player;

    void Update()
    {
        if (shouldScroll)
        {

            transform.position = new Vector3
            (
            transform.position.x + scrollSpeed * Time.deltaTime,
            player.position.y,
            transform.position.z
            );      
        }
    }
    
    public void StartScrolling()
    {
        shouldScroll = true;
    }
        
    
}
