using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private string _LoadSceneName = "Level1";

    public void LoadScene()
    {
        SceneManager.LoadScene(_LoadSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
