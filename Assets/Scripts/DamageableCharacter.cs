using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable {


    Animator animator;
    Rigidbody2D rb;
    Collider2D physicsCollider;
    bool isAlive = true;
    public bool disableSimulation = false;

    public float Health {
        set {
            if (value < _health) {
                animator.SetTrigger("hit");
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


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCollider = GetComponent<Collider2D>();
        animator.SetBool("isAlive", isAlive);
    }

    public void OnHit(float damage, Vector2 knockback) {
        Health -= damage;

        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage) {
        Health -= damage;
    }

    public void MakeUntargetable() {
        rb.simulated = false;
    }

    public void OnObjectDestroy() {
        Destroy(gameObject);
    }


}
