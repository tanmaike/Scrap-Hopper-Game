using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour {
   
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(RunSparkleAnimation());
    }

    private IEnumerator RunSparkleAnimation() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            animator.SetTrigger("randomSparkle");
        }
    }
}
