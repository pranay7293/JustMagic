using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIService : MonoBehaviour
{
    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("JustMagic");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
