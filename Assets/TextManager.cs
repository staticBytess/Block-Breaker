using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    public static TextManager Instance { get; private set; }

    public TMP_Text spaceText, levelWonScreen, results;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        enableSpaceText();
    }

    public void enableSpaceText(){
        spaceText.gameObject.SetActive(true);
    }

    public void showBounces(int count){
        results.text = "Total Bounces: " + count 
                        + System.Environment.NewLine 
                        + System.Environment.NewLine
                        + "Press space to restart";
        results.gameObject.SetActive(true);
    }

    public void gameWon(){
        levelWonScreen.gameObject.SetActive(true);
    }

    public void clearScreen(){
        results.gameObject.SetActive(false);
        levelWonScreen.gameObject.SetActive(false);
        spaceText.gameObject.SetActive(false);
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
