using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour {
    public float swordDamage = 1f;
    public float knockbackForce = 100f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(0.124f, -0.101f, 0);
    public Vector3 faceLeft = new Vector3(-0.124f, -0.101f, 0);


    private void Start() {
        if (swordCollider == null) {
            Debug.LogWarning("Sword Collider is not assigned");
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {

        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null) {
            Vector3 parentPosition = transform.parent.position;

            Vector2 direction = (collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(swordDamage, knockback);
        }

    }

    void IsFacingRight(bool isFacingRight) {
        if (isFacingRight) {
            gameObject.transform.localPosition = faceRight;
        } else {
            gameObject.transform.localPosition = faceLeft;
        }
    }


}
