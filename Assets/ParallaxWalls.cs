using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxWalls : MonoBehaviour {
    private Vector3 startPos;
    private Transform camTransform;
    private float height;
    public float parallaxEffect;

    void Start() {
        camTransform = Camera.main.transform;
        startPos = transform.position;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void LateUpdate() {
        float distance = (camTransform.position.y - startPos.y) * parallaxEffect;
        float movement = camTransform.position.y * (1 - parallaxEffect);

        transform.position = new Vector3(startPos.x, startPos.y + distance, transform.position.z);

        if(movement > startPos.y + height) startPos.y += height;
        else if (movement < startPos.y - height) startPos.y -= height;
    }
}