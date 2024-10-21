using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour {
    [SerializeField] private GameObject block, Player, Ball, leftSide, rightSide;
    private int initalCount = 35;
    private List<MyObjects> blockList = new List<MyObjects>();
    private BlockPlacer placer;
    private Player player;
    private Ball mainBall;
    private FollowPlayer left, right;
    private float xOffset = 1.88f;
    // Start is called before the first frame update
    public void removeBlock(Block block) {
        if (blockList.Contains(block)) {
            blockList.Remove(block);  // Remove the block from the list
        }
    }

    private void initalizeList() {
        foreach (var block in blockList) {
                Destroy(block.gameObject);
        }   
        blockList.Clear();

        for (int i = 0; i < initalCount; i++) {

            Block blockInstance = Instantiate(block).GetComponent<Block>();
            blockInstance.setGameWorld(this);
            blockList.Add(blockInstance);
        }
        
        placer.placeBlocks(blockList);
    }

    public void resetGame() {
        initalizeList();
    }

    void Start() {
        player = Instantiate(Player).GetComponent<Player>();
        mainBall = Instantiate(Ball).GetComponent<Ball>();
        mainBall.setPlayer(player);
        mainBall.setWorld(this);
        left = Instantiate(leftSide).GetComponent<FollowPlayer>();
        right = Instantiate(rightSide).GetComponent<FollowPlayer>();
        left.setOffsetPlayer(-xOffset, player);
        right.setOffsetPlayer(xOffset, player);
        placer = gameObject.AddComponent<BlockPlacer>();

        initalizeList();
    }

    

    // Update is called once per frame
    void Update()
    {
    }
}
