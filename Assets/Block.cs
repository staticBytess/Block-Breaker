using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MyObjects {
    private GameWorld gw;

    public void setGameWorld(GameWorld world) {
        gw = world;  // Set the reference to GameWorld
    }

    private void OnTriggerEnter(Collider collision) {
    if (collision.CompareTag("Block") || collision.CompareTag("Remover") ) {
        if(!this.CompareTag("Remover")){
            gw.removeBlock(this); // Remove the block from the GameWorld
        }
        
        Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Awake() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
