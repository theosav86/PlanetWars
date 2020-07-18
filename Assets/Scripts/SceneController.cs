﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    #region Variables
    private int greenScore;
    private int redScore;

    //Sphere radius inside which spaceships (cubes) spawn.
    private readonly int warZoneRadius = 50;

    private enum GameState {IDLE, RUNNING, GAME_OVER};

    private GameState currentState;

    //Slots for each cube Prefab
    public CubeController greenCubePrefab;
    public CubeController redCubePrefab;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager(GameState.IDLE);

        greenScore = 0;
        redScore = 0;

        ScoreBroker.CallUpdateGreenCubeScore(greenScore);
        ScoreBroker.CallUpdateRedCubeScore(redScore);

        //Scene controller Subscribes to Cube Death as an Observer.
        ScoreBroker.GreenCubeKilled += ScoreBroker_GreenCubeKilled;
        ScoreBroker.RedCubeKilled += ScoreBroker_RedCubeKilled; 
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnGreenCube()
    {
        //If this is the first cube in the game, change the state of the game to RUNNING
        if (currentState == GameState.IDLE)
        {
            GameStateManager(GameState.RUNNING);
        }

        //SELECT RANDOM SPAWN POINT INSIDE A SPAWN SPHERE
        Vector3 randomSpawnPoint = Random.insideUnitSphere * warZoneRadius;

        //Spawn a GREEN Cube Prefab
        Instantiate(greenCubePrefab, randomSpawnPoint, Quaternion.identity);

        //increase GREEN SCORE as a value
        greenScore ++;

        //EVENT UPDATE GREENSCORE to update the score on the HUD
        ScoreBroker.CallUpdateGreenCubeScore(greenScore);
    }

    //Method that fires when the RED SPAWN BUTTON is pressed.
    public void SpawnRedCube()
    {
        //If this is the first cube in the game, change the state of the game to RUNNING
        if (currentState == GameState.IDLE)
        {
            GameStateManager(GameState.RUNNING);
        }

        //SELECT RANDOM SPAWN POINT INSIDE A SPAWN SPHERE (warZoneRadius)
        Vector3 randomSpawnPoint = Random.insideUnitSphere * warZoneRadius;

        //Spawn a RED Cube Prefab
        Instantiate(redCubePrefab, randomSpawnPoint, Quaternion.identity);

        //Increase RED SCORE as a value
        redScore++;

        //EVENT UPDATE REDSCORE to update the score on the HUD
        ScoreBroker.CallUpdateRedCubeScore(redScore);
    }

    #region Scene Controller Subscribed to Cube Death events handling
    private void ScoreBroker_GreenCubeKilled()
    {
        //When a Green Cube Dies reduce the greenScore value by 1 and call the event to update the HUD as well
        greenScore -= 1;
        ScoreBroker.CallUpdateGreenCubeScore(greenScore);

        //if there are no more green cubes in the game then the RED team is winning the fight
        if (greenScore <= 0 && currentState == GameState.RUNNING)
        {
            ScoreBroker.CallTeamIsWinning("RED");
        }
    }
    private void ScoreBroker_RedCubeKilled()
    {
        redScore -= 1;
        ScoreBroker.CallUpdateRedCubeScore(redScore);

        //if there are no more RED cubes in the game then the GREEN team is winning the fight
        if (redScore <= 0 && currentState == GameState.RUNNING)
        {
            ScoreBroker.CallTeamIsWinning("GREEN");
        }
    }
    #endregion

    //Method that takes a new game state as an argument and changes the current state to the new state
    private void GameStateManager(GameState state)
    {
        currentState = state;
    }

    private void WinConditions(string winner)
    {
        //ScoreBroker.CallTeamIsWinning(winner);
    }
}
