using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundJump;
    private AudioSource audioSourceJump;
    [SerializeField]
    private AudioMixerGroup audioMixerGroup;
    public float speed = 10;          // скорость перемещения
    [SerializeField]
    private float speedMoveLadder = 5;  //скорость перемещения по леснице
    public float jumpForce = 10;      // скорость прыжка
    [SerializeField]
    private bool isGround = false;    // флаг земля
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    private Rigidbody2D rb2d;
    public float horizontal;         //ось горизонт 
    public float vertical;            //ось вертикаль
    public bool Jumped = false;        // флаг прыжок
    private bool facingRight = true;   // куда повернут
    [SerializeField]
    private bool isLadder = false;     // флаг лесницы
    [SerializeField]
    private bool isjump = false;       // флаг прыжок
    public bool isStop = true;
    [SerializeField]
    private bool isInventory;         // флаг предмета инвентаря
    public bool isAction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();   // кеш тела

        audioSourceJump = gameObject.AddComponent<AudioSource>();
        audioSourceJump.outputAudioMixerGroup = audioMixerGroup;
        audioSourceJump.playOnAwake = false;
        audioSourceJump.volume = 0.5f;
        audioSourceJump.clip = soundJump;

    }

    // Update is called once per frame
    void Update()
    {
        if (isStop)
        {
            //UpdateAxis();
            UpdateFlip();
            UpdateMoveLadder();
            Jump();
            Action();
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
            Jumped = false;
            horizontal = 0;
            vertical = 0;
            isAction = false;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isStop)
        {
            Move();
        }

    }

    /// <summary>
    /// опрос кнопок
    /// </summary>
    void UpdateAxis()
    {
        //Jumped = Input.GetButtonDown("Jump");        // прыжок
        // horizontal = Input.GetAxis("Horizontal");   // A   D
        // vertical = Input.GetAxis("Vertical");        // W   S

    }


    /// <summary>
    /// Метод для действия
    /// </summary>
    void Action()
    {
        if (isInventory)
        {

            isAction = false;

        }
    }


    /// <summary>
    /// Проверка на движение в противополжную сторону
    /// </summary>
    void UpdateFlip()
    {
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();
    }


    /// <summary>
    /// Переворот противоположную сторону
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    /// <summary>
    /// Движение вправо влево
    /// </summary>
    private void Move()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
        //horizontal = 0;
    }

    /// <summary>
    /// Прыжок
    /// </summary>
    private void Jump()
    {
        if (Jumped) // если нажата пробел
        {
            if (isGround) // если на земле 
            {
                rb2d.velocity = (Vector2.up * jumpForce); // прыгай 
                audioSourceJump.Play();
            }
        }
        Jumped = false;
    }

    /// <summary>
    /// Проверка входа на лесницу
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        //if (other.tag == "Ladder")          //(other.gameObject.CompareTag("Ladder"))   // если таг лесница
        //{
        //    isLadder = true;     // это лесница
        //}

        if (other.gameObject.CompareTag("Inventory"))
        {
            isInventory = true;
        }

    }

    /// <summary>
    /// выход с лесницы
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        //if (other.tag == "Ladder")
        //{
        //    isLadder = false;
        //}

        if (other.gameObject.CompareTag("Inventory"))
        {
            isInventory = false;
        }


    }

    /// <summary>
    /// Движение по лесницы
    /// </summary>
    void UpdateMoveLadder()   // Пока так дальше  будем  переделывать
    {
        if (isLadder) // если на леснице
        {
            rb2d.gravityScale = 0;

            if (vertical != 0) // если нажаты   W или  S
            {
                rb2d.velocity = new Vector2(0, speedMoveLadder * vertical);   // движение по лестнице
            }
        }
        else
        {
            rb2d.gravityScale = 1;
        }

        vertical = 0;
    }
}
