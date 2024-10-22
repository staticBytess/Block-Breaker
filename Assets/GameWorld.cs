using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour {
    [SerializeField] private GameObject block, Player, Ball, leftSide, rightSide;
    private int initalCount = 35, totalBounces = 0;
    private bool newAttempt, gameOver;
    private List<MyObjects> blockList = new List<MyObjects>();
    private BlockPlacer placer;
    private Player player;
    private Ball mainBall;
    private FollowPlayer left, right;
    private float xOffset = 1.12f;
    // Start is called before the first frame update
    public void removeBlock(Block block) {
        if (blockList.Contains(block)) {
            blockList.Remove(block);  // Remove the block from the list
        }

         if (blockList.Count < 1){
            showResults();
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

    public void setLevel(){
        gameOver = false;
        newAttempt = true;
        mainBall.setBall();
        TextManager.Instance.clearScreen();
        TextManager.Instance.enableSpaceText();
        initalizeList();
        player.transform.position = new Vector3(0, -3f, 0);
        totalBounces = 0;
    }

     public void startLevel(){
        TextManager.Instance.clearScreen();
        mainBall.startBall();
        newAttempt = false;
    }

    public bool isNewAttempt() {
        return newAttempt;
    }

    private void Awake() {
        newAttempt = true;
        
    }

    public void showResults(){
        if (blockList.Count < 1){
            TextManager.Instance.gameWon();
        }
        TextManager.Instance.showBounces(totalBounces);
        mainBall.freezeBall();
        gameOver = true;
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

    public void incrementBounce(){
        totalBounces++;
    }

    

    void Update() {
        if (newAttempt && Input.GetKeyDown("space")) {
            startLevel();
        }
        else if (gameOver && Input.GetKeyDown("space")) {
            setLevel();
        }
    }
}
