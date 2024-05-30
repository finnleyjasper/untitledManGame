/* 
* filename: MainMenu.cs
* author: Finnley
* description: a script to manage the outcome of each button. both options have a delay to ensure the animation plays
* 
* created: 26 May 2024
* last modified:  26 May 2024
*/

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
        Invoke("ShowIntroScreen", 0.5f);
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // a message for the console so you know this method works even in the editor

        Application.Quit();
    }

    public void ShowIntroScreen()
    {
        introScreen.SetActive(true);
        StartCoroutine(PauseBeforeStartGame(15));
    }

    IEnumerator PauseBeforeStartGame(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(nextSceneName);
    }
}
