using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dzao : MonoBehaviour
{
    public float speed = 2f;
    public float turnTime = 2f; // Tempo após o qual o inimigo vai se virar
    private bool isFacingRight = false;

    void Start()
    {
        // Inicia a coroutine de virar após um tempo
        StartCoroutine(TurnAfterDelay());
    }

    void Update()
    {
        MoveEnemy();
    }

   void MoveEnemy()
    {
        Vector2 movement = isFacingRight ? Vector2.right : Vector2.left;
        transform.Translate(movement * speed * Time.deltaTime);

        // Ajusta a escala do sprite para virar na direção correta
        if ((isFacingRight && transform.localScale.x > 0) || (!isFacingRight && transform.localScale.x < 0))
        {
            // Inverte a escala X
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator TurnAfterDelay()
    {
        yield return new WaitForSeconds(turnTime);

        // Inverte a direção
        isFacingRight = !isFacingRight;

        // Reinicia a coroutine para o próximo virar
        StartCoroutine(TurnAfterDelay());
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<Player>().Hit();
        }
   }
}
