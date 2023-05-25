using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontal;
    public float speed = 5;
    public float jumpForce;
    public bool onGround;

    public bool isAttacking;

    private Animator animator;
    private Rigidbody2D rigidbody;
    public ParticleSystem particleSystem;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsJump", !onGround);
        if (!isAttacking)
        {
            Move();
            Jump();
        }
        
        Attack();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, 0, 0);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsRun", true);
            speed = 10;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsRun", false);
            speed = 5;
            
        }
        
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true )
        {
            onGround = false;
            animator.SetTrigger("Jump");
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.S) && onGround == true)
        {
            animator.SetTrigger("Attack");
        }
    }

    void PlayTrail()
    {
        particleSystem.Play();
        isAttacking = true;

    }

    void StopTrail()
    {
        particleSystem.Stop();
        isAttacking = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        
    }
}
