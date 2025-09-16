using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
    private float startPosX, startPosY, height;
    public GameObject cam;
    public float parallaxEffect;

    void Start() {
        startPosY = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate() {
        float distance = (cam.transform.position.y - startPosY) * parallaxEffect;
        float movement = cam.transform.position.y * (1 - parallaxEffect);

        transform.position = new Vector3(startPosX, startPosY + distance, transform.position.z);

        if(movement > startPosY + height) startPosY += height;
        else if (movement < startPosY - height) startPosY -= height;

    }
}