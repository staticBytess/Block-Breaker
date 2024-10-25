using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameWorld : MonoBehaviour {
    [SerializeField] private GameObject block, Player, Ball, leftSide, rightSide, rightWall, leftWall, topWall, backDrop, mainLight;
    [SerializeField] private Block rowRemover, colRemover;
    private int totalBounces = 0, level = 1, maxLevel = 2;
    private bool newAttempt, gameOver;
    private List<MyObjects> blockList = new List<MyObjects>();
    private List<int> blocksPerLevel = new List<int>{1, 25};
    private BlockPlacer placer;
    private Player player;
    private Ball mainBall;
    private FollowPlayer left, right;
    private float xOffset = 1.12f;

    void Start() {
        newAttempt = true;
        DontDestroyOnLoad(gameObject);
        
        player = Instantiate(Player).GetComponent<Player>();
        mainBall = Instantiate(Ball).GetComponent<Ball>();
        mainBall.setPlayer(player);
        mainBall.setWorld(this);
        left = Instantiate(leftSide).GetComponent<FollowPlayer>();
        right = Instantiate(rightSide).GetComponent<FollowPlayer>();
        left.setOffsetPlayer(-xOffset, player);
        right.setOffsetPlayer(xOffset, player);
        placer = gameObject.AddComponent<BlockPlacer>();
        placer.addColRemover(colRemover);
        placer.addRowRemover(rowRemover);


        DontDestroyOnLoad(player);
        DontDestroyOnLoad(mainBall);
        DontDestroyOnLoad(left);
        DontDestroyOnLoad(right);
        DontDestroyOnLoad(placer);
        DontDestroyOnLoad(rightWall);
        DontDestroyOnLoad(leftWall);
        DontDestroyOnLoad(topWall);
        DontDestroyOnLoad(backDrop);
        DontDestroyOnLoad(mainLight);

        initalizeList(); 
        //might be setLevel() instead ??
    }

    private void initalizeList() {
        foreach (var block in blockList) {
                Destroy(block.gameObject);
        }   
        blockList.Clear();

        int levelIndex = level - 1;

        if( levelIndex < blocksPerLevel.Count){
            for (int i = 0; i < blocksPerLevel[level-1]; i++) {
                Block blockInstance = Instantiate(block).GetComponent<Block>();
                blockInstance.setGameWorld(this);
                blockList.Add(blockInstance);
            }
            placer.placeBlocks(blockList, blocksPerLevel[levelIndex]);
        }
       customizeLevel();
    }

    private void customizeLevel(){
        if(level==1){
            /*placer.removeRow(2);
            placer.removeRow(4);*/
        }
        else if(level==2){
            placer.removeCol(3);
        }
    }
    // Start is called before the first frame update
    public void removeBlock(Block block) {
        if (blockList.Contains(block)) {
            blockList.Remove(block);  // Remove the block from the list
        }

        if (blockList.Count < 1){
            showResults();
        }
    }

    public void resetGame() {
        initalizeList();
    }

    public void setLevel(){
        if(level>maxLevel){
            level = 1;
        }
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

    public void showResults(){
        if (blockList.Count < 1){
            TextManager.Instance.gameWon(level);
            level++;
            //LevelManager.Instance.loadNextLevel(level);
        }
        TextManager.Instance.showBounces(totalBounces);
        mainBall.freezeBall();
        gameOver = true;
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
