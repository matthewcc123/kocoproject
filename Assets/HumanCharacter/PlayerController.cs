using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator anim;
    public float playerYSize;
    public float moveSpeed;
    public float jumpSpeed;

    private float moveX;
    private bool moveJump;
    [SerializeField] private bool grounded;

    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveJump = Input.GetKey(KeyCode.Space);

        //Animation
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Move", moveX != 0f);
        anim.SetFloat("JumpVelocity", Mathf.Sign(rb.velocity.y));

        //Scale Direction
        if (moveX != 0)
            transform.localScale = new Vector2(Mathf.Sign(moveX), 1f);

    }

    private void FixedUpdate()
    {
        //Check Ground
        grounded = (Physics2D.Raycast(transform.position, Vector2.down, playerYSize, LayerMask.GetMask("Ground")));
        Debug.DrawLine(transform.position, (Vector2)transform.position + (Vector2.down * playerYSize), Color.red);

        //Movement
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        //Jump
        if (grounded && moveJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

    }

}
