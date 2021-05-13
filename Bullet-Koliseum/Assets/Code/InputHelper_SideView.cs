using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHelper_SideView : InputHelper
{
    public Transform theGunPosition;
    public bool getGun = false;
    public bool SpawnGun = false;
    Collider2D i;

    public int Speed;

    public SpriteRenderer weapon;


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

    public Transform Spawner;
    public float timeOfSpawn;

    Rigidbody2D rb2D;
    BoxCollider2D box;
    float yOffset = 0.1f;
    Vector2 origin;
    Vector2 size;
    bool isGrounded;
    int jumpCount = 1;

    float horizontalMove;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        i = GetComponent<Collider2D>();
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

        if (SpawnGun == true)
        {
            timeOfSpawn += Time.deltaTime;

            if (timeOfSpawn >= 5f)
            {
                Spawner.gameObject.SetActive(true);
                SpawnGun = false;
                timeOfSpawn = 0;
            }
        }
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
        
        if (getGun == true)
        {
            i.gameObject.SetActive(false);
            theGunPosition.gameObject.SetActive(true);
            getGun = false;
            SpawnGun = true;
        }
    }

    protected override void Drop(InputAction.CallbackContext value)
    {
        if (getGun == false)
        {
            theGunPosition.gameObject.SetActive(false);  
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        i = collision;

        if(collision.gameObject.CompareTag("Gun"))
        {
            getGun = true;
        }
    }

    void WeaponFlip()
    {
        weapon.flipX = spriteRenderer.flipX;
    }


}