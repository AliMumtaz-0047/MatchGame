using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Game_Controller controller;
    public Input_Controller input;
    public enum GameState
    {
        InProgress,
        Win,
        GameEnded
    }

    public GameState currentState = GameState.InProgress;
    private int totalCards;
    private int matchedPairs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        controller.Initialize_Game();
        totalCards=controller.cards.Count;
        input.enabled=false;
        Invoke("enable_Input", 4);
    }
    public void enable_Input()
    {
       input.enabled=true;
    }
    // Update is called once per frame
    void Update()
    {
        // Check if all pairs are matched
        if (input.currentMatches == totalCards / 2 && currentState!=GameState.GameEnded)
        {
            // Update game state to "You win!"
            currentState = GameState.Win;
            Debug.Log("you win");
            
        }
        
    }
}
