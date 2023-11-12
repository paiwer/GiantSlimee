using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfoScript;
    [SerializeField] private WinPlate _winPlateScript;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private string _mainMenuSceneName = "MainMenu";

    public bool _isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerInfoScript = FindObjectOfType<PlayerInfo>();
        _winPlateScript = FindObjectOfType<WinPlate>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //Debug.Log(Time.timeScale);

        //GameOverUI();
        WinUI();
    }

    private void GameOverUI()
    {
        if(_playerInfoScript.IsDead)
        {
            SetGameOverMenu(true, 0);
        }
    }

    private void WinUI()
    {
        if (_winPlateScript.IsWin)
        {
            SetWinMenu(true, 0);
        }
    }

    private void Pause()
    {
        if(!_gameOverUI.activeSelf) //didn't active
        {
            if(!_winUI.activeSelf)
            {
                _isPaused = !_isPaused;

                if (_isPaused)
                {
                    SetPauseMenu(true, 0);
                }
                else
                {
                    SetPauseMenu(false, 1);
                }
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        SetPauseMenu(false, 1);
        _isPaused = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    private void SetPauseMenu(bool active, float timeScale)
    {
        _pauseUI.SetActive(active);
        Time.timeScale = timeScale;
    }

    private void SetWinMenu(bool active, float timeScale)
    {
        _winUI.SetActive(active);
        Time.timeScale = timeScale;
    }

    private void SetGameOverMenu(bool active, float timeScale)
    {
        _gameOverUI.SetActive(active);
        Time.timeScale = timeScale;
    }
}
