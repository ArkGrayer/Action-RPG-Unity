using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour {

    public float damage = 1;
    public float knockbackForce = 20f;
    public float moveSpeed = 500f;

    public DetectionZone detectionZone;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        // detectionZone = GetComponent<DetectionZone>();
    }

    void FixedUpdate() {

        if (detectionZone != null && detectionZone.detectedObjs.Count > 0) {

            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D col) {
        Collider2D collider = col.collider;

        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null) {

            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageable.OnHit(damage, knockback);

        }

    }


}
