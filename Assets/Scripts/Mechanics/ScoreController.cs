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

    //score ui
    ScoreUI scoreui;
    public bool stage0, stage1, stage2, stage3;
    //buff spawners when flag reaches milestones
    public EnemySpawner[] spawners;
    public FriendlySpawner[] friends;

    private void Awake()
    {
        scoreui = FindObjectOfType<ScoreUI>();
        stage0 = true;
    }
    public int Score;
    public void AddScore(int entities, Entity entity)
    {
        //if the score is for players
        if (entity.GetType() == typeof(PlayerController))
        {
            Score += entities;
            scoreui.UpdateScore(Score);
        }
        //else if the score is for enemies
        else if (entity.GetType() == typeof(EnemyController))
        {
            //if score is 0 or lesser js revert to 0
            Score -= entities;
            if (Score < 0) Score = 0;
            else scoreui.UpdateScore(Score);
        }
        //if the players reached the end and won
        if (Score >= 300)
        {
            GameController gc = FindObjectOfType<GameController>();
            gc.Win();
        }
        //if the players reached the next milestone
        if (Score >= 80 && !stage1)
        {
            stage1 = true; stage0 = false;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.cdrangemin = 7.5f;
                spawner.cdrangemax = 10f;
            }
            foreach (FriendlySpawner spawner in friends)
            {
                spawner.spawnCDRangeMin = 10f;
                spawner.spawnCDRangeMax = 12.5f;
            }
        }
        else if (Score >= 160 && !stage2)
        {
            stage2 = true; stage1 = false;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.cdrangemin = 6.5f;
                spawner.cdrangemax = 8f;
            }
            foreach (FriendlySpawner spawner in friends)
            {
                spawner.spawnCDRangeMin = 8f;
                spawner.spawnCDRangeMax = 10f;
            }
        }
        else if (Score >= 240 && !stage3)
        {
            stage3 = true; stage2 = false;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.cdrangemin = 5f;
                spawner.cdrangemax = 6f;
            }
            foreach (FriendlySpawner spawner in friends)
            {
                spawner.spawnCDRangeMin = 8f;
                spawner.spawnCDRangeMax = 10f;
            }
        }

        //if the players reached the previous milestone
        else if (Score < 240 && stage3)
        {
            stage3 = false; stage2 = true;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.cdrangemin = 6.5f;
                spawner.cdrangemax = 8f;
            }
            foreach (FriendlySpawner spawner in friends)
            {
                spawner.spawnCDRangeMin = 6;
                spawner.spawnCDRangeMax = 8f;
            }
        }
        else if (Score < 160 && stage2)
        {
            stage2 = false; stage1 = true;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.cdrangemin = 7.5f;
                spawner.cdrangemax = 10f;
            }
            foreach (FriendlySpawner spawner in friends)
            {
                spawner.spawnCDRangeMin = 10f;
                spawner.spawnCDRangeMax = 12.5f;
            }
        }
        else if (Score < 80 && stage1)
        {
            stage1 = false; stage0 = true;
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.cdrangemin = 10f;
                spawner.cdrangemax = 12.5f;
            }
            foreach (FriendlySpawner spawner in friends)
            {
                spawner.spawnCDRangeMin = 12.5f;
                spawner.spawnCDRangeMax = 15f;
            }
        }
    }
}
