using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour {
    public Vector2 velocity;
    public float gravityFactor;
    public float desiredx;
    public bool grounded;

    // Start is called before the first frame update
    void Start() {
        
    }

    public virtual void CollideHorizontal(Collider2D other) {
        
    }

    public virtual void CollideVertical(Collider2D other) {
        
    }

    public void Movement(Vector2 move, bool Horizontal) {
        if (move.magnitude < 0.00001f) return;
        grounded = false;
        RaycastHit2D[] results = new RaycastHit2D[16];
        int cnt = GetComponent<Rigidbody2D>().Cast(move, results, move.magnitude + 0.02f);
        bool collision = false;
        for (int i = 0; i < cnt; ++i) {
            if (Mathf.Abs(results[i].normal.x) > 0.3f && Horizontal) {
                collision = true;
                CollideHorizontal(results[i].collider);
            }
            if (Mathf.Abs(results[i].normal.y) > 0.3f && !Horizontal) {
                if (results[i].normal.y > 0.3f) grounded = true;
                CollideVertical(results[i].collider);
                collision = true;
            }
        }
        if (collision) return;
        transform.position += (Vector3)move;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 acceleration = 9.81f * Vector2.down * gravityFactor;
        velocity += acceleration * Time.fixedDeltaTime;
        if (Mathf.Abs(desiredx) < 0.1f) {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, 10f * Time.fixedDeltaTime);
        }
        else {
             velocity.x = desiredx;
        }
        Vector2 move = velocity * Time.fixedDeltaTime;
        Movement(new Vector2(move.x, 0), true);
        Movement(new Vector2(0, move.y), false);
    }
}
