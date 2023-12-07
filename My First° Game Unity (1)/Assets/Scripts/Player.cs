using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;
    public float jumpForce;
    public int life;
    public int bananas;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sprite;
    public float minYForGameOver = -6;

    [Header("UI")]
    public TextMeshProUGUI bananasText;
    public TextMeshProUGUI lifeText;
    public GameObject gameOver;

    private Vector2 direction;
    public bool isGrounded;
    private bool recovery;

    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = life.ToString();
        gameOver.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = life.ToString();
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Jump();
        PlayAnim();
        Death();
        if (transform.position.y < minYForGameOver)
        {
            // Adicione aqui a lógica para o "game over", como reiniciar o jogo ou exibir um menu de game over.
            
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //é usado para fisica
    void FixedUpdate()
    {
        Movement();
    }

    //andar
    void Movement()
    {
        rig.velocity = new Vector2(direction.x * speed, rig.velocity.y);
    }

    //pular
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            anim.SetInteger("transition", 2);
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    //morrer
    void Death()
    {
        if (life <= 0)
        {
            anim.SetInteger("transition", 3);
            Invoke("endgame", 0.5f);
        }
    }

    void endgame(){
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    void falling(){
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }


    //restart

   public void RestartGame()
    {
        // Carregue a cena atual novamente
        SceneManager.LoadScene("SampleScene");
        
        // Destrua o jogador atual
        Destroy(gameObject);
    }

    //animações
    void PlayAnim()
    {
        if (direction.x > 0)
        {
            if (isGrounded == true)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = Vector3.zero;
        }
        if (direction.x < 0)
        {
            if (isGrounded == true)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (direction.x == 0)
            if (isGrounded == true)
            {
                anim.SetInteger("transition", 0);
            }
    }
    public void Hit()
    {
        if (recovery == false)
        {
            StartCoroutine(Flick());
        }
    }
    IEnumerator Flick()
    {
        int i = 0;
        life--;
        recovery = true;
        if(life > 0){
            for (i = 0; i <= 3; i++)
            {
                sprite.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.2f);
                sprite.color = new Color(1, 1, 1, 1);
            }
        }
        recovery = false;
    }

    public void respawn(){
        DontDestroyOnLoad(gameObject); 
    }

    public void IncreaseScore()
    {
        bananas++;
        bananasText.text = bananas.ToString();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }
}
