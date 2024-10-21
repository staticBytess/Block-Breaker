using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Ball : MyObjects {
    public float speed;
    private float leftWallX = -9, rightWallX = 9, topWallY = 6.1f;
    private Rigidbody rb;
    private Player player;
    private GameWorld gw;
    private int floor = -5;
    

    public void setPlayer(Player setMe) {
        this.player = setMe;
    }

    public void setWorld(GameWorld world) {
        this.gw = world;
    }

    private bool outOfBounds() {
        float myX = this.transform.position.x;
        float myY = this.transform.position.y;
        return myX < leftWallX || myX > rightWallX || myY > topWallY;
    }


    void Start() {
        speed = 7f;
        rb = GetComponent<Rigidbody>();
        resetBall(); // Initialize the ball's velocity
    }

    public void addHorizontalMovement() {
        float offset = this.transform.position.x - player.getXPos();
        Vector3 newVelocity = rb.velocity;
        newVelocity.x += offset;

        rb.velocity = newVelocity;
        if(Math.Abs(offset) > 1.25) {
            if(speed < 10) {
                speed++;
            }

            //Sends back left or right depending on the side hit
            if (offset < 0) {
                rb.velocity = new Vector3(-Math.Abs(newVelocity.x), newVelocity.y, newVelocity.z);
            }
            else {
                rb.velocity = new Vector3(Math.Abs(newVelocity.x), newVelocity.y, newVelocity.z);

            }

        }
        else if (speed > 7 && Math.Abs(offset) < 1) {
            speed--;
        }
    }

    private void OnTriggerEnter(Collider collision) { 
            // Reflect the ball regardless of what it collides with
            Vector3 normal = collision.transform.up; // Use the normal of the surface
            Vector3 incomingVector = rb.velocity; // Get the current velocity of the ball

            // Reflect the incoming vector based on the normal of the surface
            Vector3 reflectedVector = Vector3.Reflect(incomingVector, normal);

            // Set the new velocity of the ball
            rb.velocity = reflectedVector.normalized * speed;

        if (collision.gameObject == player.gameObject) {
            //add horizontal movement based on distance from center
            addHorizontalMovement();

        }
    }

    private void resetBall() {
        this.transform.position = new Vector3(0, -2.4f, 0);
        rb.velocity = new Vector3(0, 1, 0) * speed; 
    }

    private void resetGame() {
        gw.resetGame();
        resetBall();
        player.transform.position = new Vector3(0, -3f, 0);
       
    }


    void Update() {
        if(this.transform.position.y < floor) {
            resetGame();
        }

        if (outOfBounds()) {
            resetBall();
        }
    }
}
