using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//win or lose retry
public class GameController : MonoBehaviour
{
    //Overlay Screens
    public GameObject winScreen, loseScreen;

    //method to start lose
    public void Lose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    //method for win
    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    //retry
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Lobby");
    }
}
