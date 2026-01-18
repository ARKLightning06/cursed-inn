using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int health;
    public string enemyName;
    private Rigidbody2D enemy;

    public float knockbackSpeed;

    public Player player;

    private void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
    }

    //when the collision between a weapon and the enemy is detected
    private void OnTriggerEnter2D(Collider2D WeaponCollider)
    {

        if (player.isAttacking == true)
        {
            //set weapon to game object that collided with the enemy 
            GameObject weapon = WeaponCollider.gameObject;

            //stand in to check
            Debug.Log(enemyName + " Hit by " + weapon.name);

            //apply knockback
            ApplyKnockback(weapon.transform);

        }
    }

    //boolean to determine whether the enemy is in the middle of being knocked back so it can't be knocked back again
    private bool isKnockedBack = false;

    //apply a knockback on the enemy
    public void ApplyKnockback(Transform WeaponPos)
    {
        if (isKnockedBack) return;

        // 1. Calculate the direction from the source to the current object
        Vector2 knockbackDirection = (transform.position - WeaponPos.position).normalized;

        // 2. Apply the force using Rigidbody2D.AddForce

        enemy.linearVelocity = knockbackDirection * knockbackSpeed;
        StartCoroutine(KnockbackRoutine());
    }

    // Coroutine to manage the knockback state and duration
    IEnumerator KnockbackRoutine()
    {
        isKnockedBack = true;
        // prevent enemy from getting hit again
        yield return new WaitForSeconds(0.3f);
        isKnockedBack = false;

        // Optional: reduce velocity to stop faster if needed
        enemy.linearVelocity = Vector2.zero;
    }
}
