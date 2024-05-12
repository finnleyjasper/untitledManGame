using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string nextSceneName;

    public void PlayGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // a message for the console so you know this method works even in the editor

        Application.Quit();
    }
}
