using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    //Entire gameobjcts
    public GameObject Lobby;

    //Each individual screen - canvas objects
    public GameObject StartScreen, MainScreen, CreditScreen, InstructionScreen, CharacterScreen;
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject);
        if (other.gameObject.GetComponent<Entity>())
        {
            print("yes");
            Lobby.SetActive(true);
            print(Lobby.activeSelf);
        }
    }

    //events for each buttons
    public void Play()
    {
        //Lobby.SetActive(false);
        MainScreen.SetActive(true);
    }

    public void Instructions()
    {
        //Lobby.SetActive(false);
        InstructionScreen.SetActive(true);
    }

    public void Credits()
    {
        //Lobby.SetActive(false);
        CreditScreen.SetActive(true);
    }
    public void Characters()
    {
        //Lobby.SetActive(false);
        CharacterScreen.SetActive(true);
    }

    public void BackToStart()
    {
        //Lobby.SetActive(false);
        CharacterScreen.SetActive(false);
        CreditScreen.SetActive(false);
        InstructionScreen.SetActive(false);
        MainScreen.SetActive(false);
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("StageOne");
    }
}
