using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionCanvas : MonoBehaviour
{
    [SerializeField] private GameObject victoryContainer;
    [SerializeField] private GameObject loseContainer;
    [SerializeField] private string nextLevelName;
    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        playerController.OnWin += OnWin;
        playerController.OnLose += OnLose;
    }

    private void OnDisable()
    {
        playerController.OnWin -= OnWin;
        playerController.OnLose -= OnLose;
    }

    public void OnWin()
    {
        victoryContainer.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void OnLose()
    {
        loseContainer.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(nextLevelName);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }


}
