using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Ball : MyObjects {
    public float speed, maxSpeed = 30, minSpeed = 10, sidesX = 1.12f, defaultY = -2.4f;
    private float wallX = 9, topWallY = 6.1f;
    private Rigidbody rb;
    private Player player;
    private GameWorld gw;
    private int floor = -5;
    private bool hasHitBlock, follow, ballReset;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }


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
        speed = 8f;
        rb = GetComponent<Rigidbody>();
        setBall(); // Initialize the ball's velocity
    }

    public void addHorizontalMovement() {
        float offset = this.transform.position.x - player.getXPos();
        Vector3 newVelocity = rb.velocity;

        rb.velocity = newVelocity;
        float xVelocity = newVelocity.x;
        float speed = rb.velocity.magnitude;

        if(Math.Abs(offset) < sidesX/4 && (int)speed > minSpeed){
            speed--;
        }
        else if ((int)speed < maxSpeed){
            speed++;
        }
        if (offset < 0) {
            xVelocity += -Math.Abs(offset * 1.25f) - 1;
        } else {
            xVelocity += Math.Abs(offset * 1.25f) + 1;
        }

       
        newVelocity = new Vector3(xVelocity, newVelocity.y, 0);
        newVelocity = newVelocity.normalized * speed;
        rb.velocity = new Vector3(newVelocity.x, newVelocity.y, rb.velocity.z);
        transform.position += new Vector3(xVelocity * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider collision) {
        if (!hasHitBlock) {
           Vector3 normal = collision.transform.up; // Use the normal of the surface
            Vector3 incomingVector = rb.velocity; // Get the current velocity of the ball

            // Store the current speed (magnitude of the velocity)
            float currentSpeed = rb.velocity.magnitude;

            // Reflect the incoming vector based on the normal of the surface
            Vector3 reflectedVector = Vector3.Reflect(incomingVector, normal);

            // Set the new velocity of the ball with the same speed
            rb.velocity = reflectedVector.normalized * currentSpeed;
            gw.incrementBounce();
        }
        if (collision.CompareTag("Block") && !hasHitBlock) {
            Block block = collision.GetComponent<Block>();
            gw.removeBlock(block); // Remove the block from the GameWorld
            Destroy(block.gameObject); // Destroy the block
            hasHitBlock = true;
        }


        if (collision.gameObject == player.gameObject) {
            //add horizontal movement based on distance from center
            gw.incrementBounce();
            addHorizontalMovement();
        }
    }

    public void setBall() {
        follow = true;
        this.transform.position = new Vector3(0, -2.4f, 0);
        rb.velocity = new Vector3(0, 1, 0) * speed; 
    }

    public void freezeBall(){
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void startBall(){
        follow = false;
    }

    private void followPlayer() {
        this.transform.position = new Vector3(player.transform.position.x, defaultY, 0);
    }

    void Update() {
        hasHitBlock = false;
        if (ballReset && Input.GetKeyDown("space")) {
            ballReset = false;
        }
        else if (follow) {
        followPlayer();
        }
        else if(this.transform.position.y < floor) {
            gw.showResults();
        }
        else if (outOfBounds()) {
            setBall();
            follow = true;
            ballReset = true;
        }
    }
}
