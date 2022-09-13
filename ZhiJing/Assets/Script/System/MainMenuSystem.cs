using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    public PlayerState playerstate;
    public void NewGame()
    {
        playerstate.StartNewGame();
        SceneManager.LoadScene("Chapter" + playerstate.CurChapter);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Chapter" + playerstate.CurChapter);
    }

}
