using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restartgame : MonoBehaviour
{
    [SerializeField] string nextSceneName;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
