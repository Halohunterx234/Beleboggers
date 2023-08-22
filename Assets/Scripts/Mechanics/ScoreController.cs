using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // In charge of the following:
    // Updating the scores of the teams
    // Updating the UI bars of the teams score accordingly

    //1 entity needs 10 mins to completely reach all the way
    //1 entity -> 1 score every 2 seconds
    //Full bar = 300 score
    //Every +1 entity will add the same amount of score
    //Every 50 score reached by the players, will lead to a brief stage:
    //where capturing is not allowed by the players and a load of enemies will come

    public int Score;
    public void AddScore(int entities, Entity entity)
    {
        //if the score is for players
        if (entity.GetType() == typeof(PlayerController))
        {
            Score += entities;
        }
        //else if the score is for enemies
        else if (entity.GetType() == typeof(EnemyController))
        {
            Score -= entities;
        }
    }
}