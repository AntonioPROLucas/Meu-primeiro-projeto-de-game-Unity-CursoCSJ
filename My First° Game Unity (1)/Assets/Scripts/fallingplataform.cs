using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingplataform : MonoBehaviour
{
    public float fallingTime = 1.5f;
    public int breaktime = 8;
    public Animator anim;

    public TargetJoint2D joint;

    [SerializeField] private Rigidbody2D rb;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall(){
        yield return new WaitForSeconds(fallingTime);
        joint.enabled = false;
        anim.SetInteger("transition", 1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject,breaktime);
    }
}
