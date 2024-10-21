using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Ball : MyObjects {
    public float speed, maxSpeed = 10, minSpeed = 7, sidesX = 1.12f, defaultY = -2.4f;
    private float wallX = 9, topWallY = 6.1f;
    private Rigidbody rb;
    private Player player;
    private GameWorld gw;
    private int floor = -5;
    private bool hasHitBlock = false;


    public void setPlayer(Player setMe) {
        this.player = setMe;
    }

    public void setWorld(GameWorld world) {
        this.gw = world;
    }



    private bool outOfBounds() {
        float myX = this.transform.position.x;
        float myY = this.transform.position.y;
        return myX < -wallX || myX > wallX || myY > topWallY;
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
        if(Math.Abs(offset) > sidesX) {
            if(speed < maxSpeed) {
                speed+=2;
            }

            //Sends back left or right depending on the side hit
            if (offset < 0) {
                rb.velocity = new Vector3(-Math.Abs(newVelocity.x)-1, newVelocity.y, newVelocity.z);
            }
            else {
                rb.velocity = new Vector3(Math.Abs(newVelocity.x+1), newVelocity.y, newVelocity.z);

            }

        }
        else if (speed > minSpeed && Math.Abs(offset) < 1) {
            speed--;
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if (!hasHitBlock) {
            Vector3 normal = collision.transform.up; // Use the normal of the surface
            Vector3 incomingVector = rb.velocity; // Get the current velocity of the ball

            // Reflect the incoming vector based on the normal of the surface
            Vector3 reflectedVector = Vector3.Reflect(incomingVector, normal);

            // Set the new velocity of the ball
            rb.velocity = reflectedVector.normalized * speed;
        }
        if (collision.CompareTag("Block") && !hasHitBlock) {
            Block block = collision.GetComponent<Block>();
            gw.removeBlock(block); // Remove the block from the GameWorld
            Destroy(block.gameObject); // Destroy the block
            hasHitBlock = true;
        }


        if (collision.gameObject == player.gameObject) {
            //add horizontal movement based on distance from center
            addHorizontalMovement();
        }
    }

    private void resetBall() {
        this.transform.position = new Vector3(0, -2.4f, 0);
        rb.velocity = new Vector3(0, 1, 0) * speed; 
    }

    private void followPlayer() {
        this.transform.position = new Vector3(player.transform.position.x, defaultY, 0);
    }

    private void resetGame() {
        gw.resetGame();
        resetBall();
        player.transform.position = new Vector3(0, -3f, 0);
    }

    void Update() {
        hasHitBlock = false;
        if (gw.isNewGame()) {
        followPlayer();
        }
        else if(this.transform.position.y < floor) {
            resetGame();
            gw.setNewGame();
        }
        else if (outOfBounds()) {
            resetBall();
            gw.setNewGame();
        }

        Debug.Log("new game: " + gw.isNewGame());
    }
}
