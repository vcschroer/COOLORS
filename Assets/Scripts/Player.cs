using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform posicaoDosPes;
    public LayerMask plataformsLayer;
    public SpriteRenderer sr;
    public Animator playerani;
    public bool onGround;
    public bool Jumping;
    public float speed;
    private float direction;
    public float jumpForce;
    public bool isYellow;
    public bool isRed;
    public bool isBlue;
    public bool isGreen;
    public int jumpsCount = 2;
    public BoxCollider2D colliderDoCorpo;
    public GameObject colliderPes;
    public CinemachineVirtualCamera virtualCamera;
    bool die;
    public ParticleSystem dust;
    public Color flashColor;
    public float flashDuration = 0.1f;
    private Color originalColor = Color.white;
    public float squashStretchDuration = 0.1f;
    private Vector3 originalScale;
    private bool wasOnGround;




    void Start()
    {
        originalScale = transform.localScale;

        Time.timeScale = 1.2f; 

        die = false;
    }

    void Update()
    {

        playerani.SetBool("Jumping", Jumping);

        onGround = Physics2D.OverlapCircle(posicaoDosPes.position, 0.1f, plataformsLayer);

        if (!die)
        {
            PlayerMove();

            if (Input.GetKeyDown(KeyCode.Space) && jumpsCount > 0)
            {

                Jump();
            }

            HandleColorChange();

        }

        


    }

    private void FixedUpdate()
    {
        bool wasJumping = Jumping;

        onGround = Physics2D.OverlapCircle(posicaoDosPes.position, 0.1f, plataformsLayer);

        if (rb.velocity.y > 0 && !onGround)
        {
            Jumping = true;
        }
        else if (onGround)
        {
            jumpsCount = 1;
            Jumping = false;
        }

        if (!wasOnGround && onGround)
        {
            StartCoroutine(DoSquashEffect());
        }

        wasOnGround = onGround;
    }

    private IEnumerator DoStretchEffect()
    {
        transform.localScale = new Vector3(originalScale.x * 0.8f, originalScale.y * 1.2f, originalScale.z);
        yield return new WaitForSeconds(squashStretchDuration);
        transform.localScale = originalScale;
    }

    private IEnumerator DoSquashEffect()
    {
        transform.localScale = new Vector3(originalScale.x * 1.2f, originalScale.y * 0.8f, originalScale.z);
        yield return new WaitForSeconds(squashStretchDuration);
        transform.localScale = originalScale;
    }

    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            direction = -1f; 
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            direction = 1f; 
        }
        else
        {
            direction = 0f; 
        }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        UpdateSpriteDirection();

        if (direction != 0)
        {
            playerani.SetFloat("Speed", Mathf.Abs(direction * speed)); 
        }
        else
        {
            playerani.SetFloat("Speed", 0); 
        }
    }

    private void Jump()
    {
        createDust();
        jumpsCount--;
        rb.velocity = Vector2.up * jumpForce;
        StartCoroutine(DoStretchEffect());
    }

    private void UpdateSpriteDirection()
    {

        if (direction < 0)
        {
            sr.flipX = true;  
        }
        else if (direction > 0)
        {
            sr.flipX = false; 
        }
    }

    private void HandleColorChange()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetColor(new Color(1f, 0.45f, 0.45f));
            isRed = true;
            isYellow = false;
            isBlue = false;
            isGreen = false;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetColor(new Color(0.45f, 0.72f, 1f));
            isRed = false;
            isYellow = false;
            isBlue = true;
            isGreen = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetColor(new Color(0.48f, 1f, 0.45f));
            isRed = false;
            isYellow = false;
            isBlue = false;
            isGreen = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetColor(new Color(1f, 0.93f, 0.45f));
            isRed = false;
            isYellow = true;
            isBlue = false;
            isGreen = false;
        }
    }

    private void SetColor(Color color)
    {
        sr.color = color;
    }
    public IEnumerator morte()
    {
        die = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 9f, ForceMode2D.Impulse);
        colliderDoCorpo.isTrigger = true;
        colliderPes.SetActive(false);

        if (virtualCamera != null)
        {
            virtualCamera.Follow = null;
        }


        yield return new WaitForSeconds(0.8f);

        GameObject transitionObj = GameObject.Find("TransitionManager");

        if (transitionObj != null)
        {
            ScreenTransition transition = transitionObj.GetComponent<ScreenTransition>();

            if (transition != null)
            {
                yield return StartCoroutine(transition.StartTransition()); 
            }
            else
            {
                Debug.LogError("O objeto TransitionManager foi encontrado, mas n�o tem o script ScreenTransition!");
            }
        }
        else
        {
            Debug.LogError("TransitionManager n�o encontrado na cena!");
        }

        Invoke("restartGame", .5f);
    }


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void createDust()
    {
        dust.Play(); 
    }

    private IEnumerator FlashSprite()
    {
        float timer = 0;

        while (timer < 0.3f)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sr.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
            timer += flashDuration * 2;
        }
    }

}


