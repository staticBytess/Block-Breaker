using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MyObjects {
    private GameWorld gw;

    public void setGameWorld(GameWorld world) {
        gw = world;  // Set the reference to GameWorld
    }

    private void OnTriggerEnter(Collider other) {

        if (gw != null) {
            gw.removeBlock(this);
        }
       Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Awake() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
