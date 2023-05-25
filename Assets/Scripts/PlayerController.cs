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
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsJump", !onGround);
        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal * speed * Time.deltaTime, 0, 0);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10;
            animator.SetBool("IsRun", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
            animator.SetBool("IsRun", false);
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
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
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
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

    void Trail()
    {
        particleSystem.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        
    }
}
