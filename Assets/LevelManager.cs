using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{



public static LevelManager Instance { get; private set; }

private List<string> levelsList = new List<string> { "Scene1", "Scene2" };
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void loadFirstLevel(){
        SceneManager.LoadScene("Scene1");
    }

    public void loadNextLevel(int level) {
        if(level - 1 < levelsList.Count)
        SceneManager.LoadScene(levelsList[level-1]); // Replace with the name of your next scene
        else {
            level = 1;
        }
    }

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
