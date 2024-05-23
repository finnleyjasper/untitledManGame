using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    public GameObject introScreen;

    public void PlayGame()
    {
        ShowIntroScreen();
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // a message for the console so you know this method works even in the editor

        Application.Quit();
    }

    public void ShowIntroScreen()
    {
        introScreen.SetActive(true);
        StartCoroutine(PauseBeforeStartGame(20));
    }

    IEnumerator PauseBeforeStartGame(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(nextSceneName);
    }
}
