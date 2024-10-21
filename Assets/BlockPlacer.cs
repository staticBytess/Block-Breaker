using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour {
    // Start is called before the first frame update
    private int rows = 7, cols = 5;
    public void placeBlocks(List<MyObjects> blocks) {
        int blockIndex = 0;
        float xDiff = 0, yDiff = 0;
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (blockIndex < blocks.Count) { // Ensure index is valid
                    blocks[blockIndex].gameObject.transform.position = new Vector3(-6 + xDiff, 5 + yDiff, 0);
                    Debug.Log("placed block: " + blockIndex);
                    xDiff += 3;
                    blockIndex++; // Move to the next block
                } else {
                    Debug.LogError("Block index out of range!");
                }
            }
            xDiff = 0;
            yDiff -= 0.7f;
        }
    }

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
