using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    bool IsMoving {
        set {
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    bool IsRunning {
        set {
            isRunning = value;
        }
    }

    public float moveSpeed = 150f;
    public float maxSpeed = 2f;
    public float idleFriction = 0.9f;
    private bool isMoving = false;
    private bool isRunning = false;
    private bool canMove = true;
    Vector2 moveInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {

        if (canMove == true && moveInput != Vector2.zero) {

            if (isRunning == false) {

                rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);

            } else if (isRunning == true) {
                float currentMaxSpeed = isRunning ? maxSpeed * 1.5f : maxSpeed;
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), currentMaxSpeed);

            }

            if (moveInput.x > 0) {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            } else if (moveInput.x < 0) {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }

            IsMoving = true;

        } else {
            //rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);

            IsMoving = false;
        }


    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnRun(InputValue value) {

        isRunning = value.isPressed;
        IsRunning = isRunning;

    }

    void OnFire() {
        animator.SetTrigger("swordAttack");
    }

    void LockMovement() {
        canMove = false;
    }
    void UnlockMovement() {
        canMove = true;
    }

}
