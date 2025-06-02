using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZonaDeMorte : MonoBehaviour
{
    public Transform player;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    private float fixedY;
    public ScreenTransition transition; 


    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector2(player.position.x, fixedY);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (virtualCamera != null)
            {
                virtualCamera.Follow = null;
            }

            StartCoroutine(HandleTransition());
        }
    }

    private IEnumerator HandleTransition()
    {
        yield return StartCoroutine(transition.StartTransition());

        StartCoroutine(ReloadSceneAfterDelay(.5f));
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
