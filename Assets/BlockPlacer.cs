using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour {
    // Start is called before the first frame update
    private int rows, cols;
    [SerializeField] Block rowRemover, colRemover;
    private Block deleter;
    int blockIndex = 0;
    private float rowIncrement = -0.7f, colIncrement = 3f;
    float xDiff = 0, yDiff = 0;
    
    public void addRowRemover(Block remover){
        rowRemover = remover;
    }
    public void addColRemover(Block remover){
        colRemover = remover;
    }
    public void placeBlocks(List<MyObjects> blocks, int count) {
        rows = count / 5; 
        cols = 5;
        xDiff = 0;
        yDiff = 0;
        
        if (count < cols){
            cols = count;
            rows = 1;
        }
        
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (blockIndex < blocks.Count) { 
                    blocks[blockIndex].gameObject.transform.position = new Vector3(-6 + xDiff, 5 + yDiff, 0);
                    xDiff += colIncrement;
                    blockIndex++; // Move to the next block
                } else {
                    Debug.Log("Block index out of range!");
                }
            }
            xDiff = 0;
            yDiff += rowIncrement;
        }
    }

    public void removeCol(int deleteCol){
        deleter = Instantiate(colRemover).GetComponent<Block>();
        deleter.transform.position = new Vector3(deleter.transform.position.x + (deleteCol-1)*colIncrement, deleter.transform.position.y, deleter.transform.position.z);
    }

    public void removeRow(int deleteRow){
        deleter = Instantiate(rowRemover).GetComponent<Block>();
        deleter.transform.position = new Vector3(deleter.transform.position.x, deleter.transform.position.y + (deleteRow-1)*rowIncrement, deleter.transform.position.z);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
