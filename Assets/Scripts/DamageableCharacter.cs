using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable {

    public GameObject healthText;
    public bool isInvicibilityEnabled = false;
    public float invincibilityTime = 0.25f;
    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;
    private float invincibleTimeEnlapsed = 0;
    public bool disableSimulation = false;

    public float Health {
        set {
            if (value < _health) {
                animator.SetTrigger("hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            _health = value;

            if (_health <= 0) {
                animator.SetBool("isAlive", false);
                Targetable = false;
            }
        }
        get {
            return _health;
        }
    }

    public bool Targetable {
        set {

            _targetable = value;

            if (disableSimulation) {
                rb.simulated = value;
            }

            physicsCollider.enabled = value;

        }
        get {
            return _targetable;
        }
    }

    public float _health = 3;
    public bool _targetable = true;
    public bool _invincible = false;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCollider = GetComponent<Collider2D>();
        animator.SetBool("isAlive", isAlive);
    }

    private void FixedUpdate() {
        if (Invincible) {

            invincibleTimeEnlapsed += Time.deltaTime;

            if (invincibleTimeEnlapsed > invincibilityTime) {
                Invincible = false;
            }
        }
    }

    public void OnHit(float damage, Vector2 knockback) {

        if (!Invincible) {

            Health -= damage;

            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (isInvicibilityEnabled) {
                Invincible = true;
            }
        }

    }

    public void OnHit(float damage) {
        if (!Invincible) {

            Health -= damage;

            if (isInvicibilityEnabled) {
                Invincible = true;
            }

        }
    }

    public void MakeUntargetable() {
        rb.simulated = false;
    }

    public void OnObjectDestroy() {
        Destroy(gameObject);
    }

    public bool Invincible {
        get {
            return _invincible;
        }
        set {
            _invincible = value;

            if (_invincible == true) {
                invincibleTimeEnlapsed = 0f;
            }

        }
    }
}

