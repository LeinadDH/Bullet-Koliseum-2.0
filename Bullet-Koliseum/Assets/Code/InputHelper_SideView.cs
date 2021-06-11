using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHelper_SideView : InputHelper
{
    public Transform leftHand;

    public Transform RiflePosition;
    public Transform MiniPosition;

    public bool getRifle = false;
    public bool getMiniGun = false;
    Collider2D i;

    public int Speed;

    public SpriteRenderer RifleFlip;
    public SpriteRenderer MiniFlip;


    [Header("Move")]
    public float walkSpeed = 1;
    public bool useLinearDrag = true;
    
    [Header("Jump")]
    public float jumpForce = 1;
    public int multiJump = 1;

    [Header("Animation")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Collider2D collision;

    Rigidbody2D rb2D;
    BoxCollider2D box;
    float yOffset = 0.1f;
    Vector2 origin;
    Vector2 size;
    bool isGrounded;
    int jumpCount = 1;

    float horizontalMove;

    public GameObject menu;
    
    public Action<InputAction.CallbackContext> onReload;
    public Action<InputAction.CallbackContext> onShoot;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        //i = GetComponent<Collider2D>();
        origin = new Vector2(0, box.bounds.extents.y * -2.5f) + box.offset;
        size = new Vector2(box.bounds.size.x, 0.01f);
    }

    private void Update()
    {
        #region Move
        rb2D.velocity = new Vector2(
            Mathf.Clamp(rb2D.velocity.x * (useLinearDrag ? 1 : 0) + (horizontalMove * walkSpeed),
                -walkSpeed,
                walkSpeed),
            rb2D.velocity.y);
        #endregion

        #region Ground Test
        isGrounded = Physics2D.BoxCast(transform.TransformPoint(origin), size, 0, Vector2.down, yOffset).collider != null;
        Debug.DrawLine(
            transform.TransformPoint(new Vector3(size.x * 0.5f, origin.y - yOffset)),
            transform.TransformPoint(new Vector3(size.x * -0.5f, origin.y - yOffset)),
            isGrounded ? Color.green : Color.red);
        #endregion

        #region Animation        
        spriteRenderer.flipX = (rb2D.velocity.x == 0) ? spriteRenderer.flipX : (rb2D.velocity.x < 0) ? true : false;
        animator.SetFloat("Speed", rb2D.velocity.sqrMagnitude);
        animator.SetFloat("Vertical", rb2D.velocity.y);
        animator.SetFloat("Horizontal", rb2D.velocity.x);
        animator.SetBool("isGrounded", isGrounded);
        #endregion

        WeaponFlip();

        if (rb2D.velocity.x >= 0)
        {
            Speed = 1;
        }
        else if (rb2D.velocity.x < 0)
        {
            Speed = -1;
        }

        Vector3 leftHandPos = this.transform.InverseTransformPoint(leftHand.position);
        if (spriteRenderer.flipX)
            leftHandPos.x *= -1;
        RiflePosition.localPosition = leftHandPos;
        MiniPosition.localPosition = leftHandPos;
    }

    protected override void Action(InputAction.CallbackContext value)
    {
        
    }

    protected override void Jump(InputAction.CallbackContext value)
    {
        if (value.ReadValueAsButton())
        {
            if (isGrounded)
                jumpCount = 1;

            if (jumpCount <= multiJump)
            {
                jumpCount++;
                rb2D.AddForce(Vector2.up * Mathf.Abs(Physics2D.gravity.y) * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    protected override void Move(InputAction.CallbackContext value)
    {
        horizontalMove = value.ReadValue<Vector2>().x;
    }

    protected override void PickUp(InputAction.CallbackContext value)
    {
        i.gameObject.SetActive(false); 

        if (getRifle == true)
        {
            RiflePosition.gameObject.SetActive(true);
            MiniPosition.gameObject.SetActive(false);
            getRifle = false;
        }
        if (getMiniGun == true)
        {     
            MiniPosition.gameObject.SetActive(true);
            RiflePosition.gameObject.SetActive(false);
            getMiniGun = false;
        }
    }

    protected override void Drop(InputAction.CallbackContext value)
    {
        if (i == null)
            return;

        i.gameObject.SetActive(true);

        if (getRifle == false)
        {
            RiflePosition.gameObject.SetActive(false);  
        }
        if (getMiniGun == false)
        {
            MiniPosition.gameObject.SetActive(false);
        }
    }

    protected override void Reload(InputAction.CallbackContext value)
    {
        onReload?.Invoke(value);
    }

    protected override void Shoot(InputAction.CallbackContext value)
    {
        onShoot?.Invoke(value);
    }

    protected override void Menu(InputAction.CallbackContext value)
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        i = collision;

        if(collision.gameObject.CompareTag("rifleDeAsalto"))
        {
            getRifle = true;
            getMiniGun = false;
        }

        if (collision.gameObject.CompareTag("MiniGun"))
        {
            getMiniGun = true;
            getRifle = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (i!=null && i.Equals(collision))
            i = null;
    }

    void WeaponFlip()
    {
        RifleFlip.flipX = spriteRenderer.flipX;

        MiniFlip.flipX = spriteRenderer.flipX;
    }
}
