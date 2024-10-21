using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MyObjects {
    public float speed;
    private float border = 6;
    // Start is called before the first frame update
    void Start() {
        speed = 20f;
    }

    public float getXPos() {
        return this.transform.position.x;
    }



    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveX, 0, 0);
        transform.Translate(move * speed * Time.deltaTime);

        //ensures player cannot go out of bounds
        if (this.transform.position.x > border) {
            transform.position = new Vector3(border, -3, 0);
        } else if(this.transform.position.x < -border) {
            transform.position = new Vector3(-border, -3, 0);
        }

    }
}
