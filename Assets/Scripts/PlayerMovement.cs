using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    public Rigidbody2D playerRb;
    public float moveSpeed = 5f;
    public float input;
    public float jumpForce = 5f;
    private BoxCollider2D boxCollider2d;
    public Animator animator;

    public SpriteRenderer spriteRenderer;
    
    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
    // Update is called once per frame

    private void Awake(){
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(input));

        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }

        if(isGrounded() && Input.GetKeyDown(KeyCode.UpArrow))
        {   
            animator.SetBool("isJumping", true);
            playerRb.velocity = Vector2.up * jumpForce;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2(input * moveSpeed, playerRb.velocity.y);

    }

    bool isGrounded(){
        RaycastHit2D raycastHit2d =  Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit2d.collider != null;
    }   

}
