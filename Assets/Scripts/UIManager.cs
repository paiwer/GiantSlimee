using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private string _mainMenuSceneName = "MainMenu";

    private PlayerInfo _playerInfoScript;
    private WinPlate _winPlateScript;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;

        _playerInfoScript = FindObjectOfType<PlayerInfo>();
        _winPlateScript = FindObjectOfType<WinPlate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        GameOverUI();
        WinUI();
    }

    private void GameOverUI()
    {
        if(_playerInfoScript.IsDead)
        {
            SetMenu(_gameOverUI, true, true, 0, CursorLockMode.None);
        }
    }

    private void WinUI()
    {
        if (_winPlateScript.IsWin)
        {
            SetMenu(_winUI, true, true, 0, CursorLockMode.None);
        }
    }

    private void Pause()
    {
        if(!_gameOverUI.activeSelf && !_winUI.activeSelf) //didn't active
        {
            if (_isPaused)
            {
                SetMenu(_pauseUI, false, false, 1, CursorLockMode.Locked);
            }
            else
            {
                SetMenu(_pauseUI, true, true, 0, CursorLockMode.None);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume()
    {
        SetMenu(_pauseUI, false, false, 1, CursorLockMode.Locked);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    private void SetMenu(GameObject ui, bool active, bool pause, float timeScale, CursorLockMode cursorLockMode)
    {
        ui.SetActive(active);
        _isPaused = pause;
        Time.timeScale = timeScale;
        Cursor.lockState = cursorLockMode;
    }
}
