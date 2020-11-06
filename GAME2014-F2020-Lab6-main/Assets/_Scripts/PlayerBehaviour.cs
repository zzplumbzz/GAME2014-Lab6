using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;
    public float jumpForce;
    public float maximumVelocity;
    public bool isGrounded;
    public Transform SpawnPoint;
    public bool isJumping;

    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                //move right
                m_rigidbody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = false;
                m_animator.SetInteger("AnimState", 1);
            }

            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                //move left
                m_rigidbody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                m_spriteRenderer.flipX = true;
                m_animator.SetInteger("AnimState", 1);
            }
            else if(!isJumping)
            {
                m_animator.SetInteger("AnimState", 0);
            }
            if (joystick.Vertical > joystickVerticalSensitivity)
            {
                //jump
                m_rigidbody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime);
                m_animator.SetInteger("AnimState", 2);
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }
            
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //respawn
        if(other.gameObject.CompareTag("DeathPlane"))
        transform.position = SpawnPoint.position;
    }
}
