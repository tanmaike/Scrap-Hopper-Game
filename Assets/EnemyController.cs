using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsBase {
    private Rigidbody2D rb;

    Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GetComponent<Collider2D>().isTrigger = true;
        
        if (Random.Range(0, 2) == 0) desiredx = Random.Range(-5, -1);
        else desiredx = Random.Range(1, 5);
    }
    
    void FixedUpdate() {
        rb.velocity = new Vector2(desiredx, 0);
        animator.SetFloat("xVelocity", rb.velocity.x);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Lethal")) {
            desiredx = -desiredx;
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Platforms")) {
            return;
        }
    }
}
