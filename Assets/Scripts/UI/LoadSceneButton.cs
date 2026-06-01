using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
