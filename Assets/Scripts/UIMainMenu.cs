using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private string _levelSceneName = "Level1";

    public void PlayLevel1()
    {
        SceneManager.LoadScene(_levelSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
