using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Started,
    Paused,
    Resume,
    Won,
    Lost
}

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int rows = 10;
    public int columns = 10;
    public GameObject gridPrefab;

    public GameState gameState;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private float cellSize = 1.0f; // Size of each grid cell


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SwitchGameState(GameState.Started);
    }

    public void SwitchGameState(GameState newState)
    {
        //if (gameState == newState)
        //    return;

        gameState = newState;

        switch (gameState)
        {
            case GameState.Started:
                GameStarted();
                break;
            case GameState.Paused:
                GamePaused();
                break;
            case GameState.Resume:
                GameResume();
                break;
            case GameState.Won:
                GameWon();
                break;
            case GameState.Lost:
                GameLost();
                break;
            default:
                break;
        }
    }

    private void GameLost()
    {

    }

    private void GameWon()
    {
  
    }

    private void GameResume()
    {
      
    }

    private void GamePaused()
    {

    }

    private void GameStarted()
    {
        Vector3 gridCenter = new Vector3(columns * offset * cellSize, rows * offset * cellSize, 0);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * cellSize, row * cellSize, 0) - gridCenter;
                Instantiate(gridPrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
