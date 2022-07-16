using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gameObj;
    public Animator animator;

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded = false;
    bool jump = false;
    bool crouch = false;
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        GroundCheck();
        if (horizontalMove != 0)
        {
            animator.SetBool("IsMoving", true);
        }

        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        else if (isGrounded == true && jump == false)
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }
    private void FixedUpdate()
    {
        print(crouch);
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void GroundCheck()
    {
        //Check if the GroundCheckObject is colliiding with other
        //2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }
}
