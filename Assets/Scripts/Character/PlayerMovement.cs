using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isSprinting = false;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {        
        if(isSprinting){
            moveSpeed = 10.0f;
        }
        else{
            moveSpeed = 5.0f;
        }
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        //animator.SetBool("isSprinting", false);
        animator.SetBool("isWalking", true);


        if(context.canceled){
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        //animator.SetBool("isSprinting", true);
        //animator.SetBool("isWalking", true);
        isSprinting = true;

        if(context.canceled){
            isSprinting = false;
        }
    }
}
