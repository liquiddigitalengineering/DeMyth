using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, savingGameobject, gameOverMenu;
    [SerializeField] private PersistantSaving saving;
    [SerializeField] private string sceneName;

    private void OnEnable()
    {
        PlayerHealth.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= GameOver;
    }
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        OpenResumeMenu();
    }

    private void OpenResumeMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenMainMenu() => SceneManager.LoadScene(sceneName);

    public void QuitGame() => StartCoroutine(QuittingCoroutine());

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    #region Quit
    private IEnumerator QuittingCoroutine()
    {
        savingGameobject.SetActive(true);
        saving.SaveData();
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }
    #endregion
}
