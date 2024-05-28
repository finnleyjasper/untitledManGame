/* 
* filename: LoadingText.cs
* author: Finnley
* description: animates the loading "...." beneath the "loading" screen
*
* created: 20 May 2024
* last modified:  20 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string _newText = "";
    private float _lastUpdatedTime;
    private float _delay = 0.8f;

    private void Update()
    {
        if (Time.time > _lastUpdatedTime + _delay)
        {
            if (_newText == ".")
            {
                _newText = ". .";
            }
            else if (_newText == ". .")
            {
                _newText = ". . .";
            }
            else if (_newText == ". . .")
            {
                _newText = ". . . .";
            }
            else if (_newText == ". . . .")
            {
                _newText = "";
            }
            else
            {
                _newText = ".";
            }

            _lastUpdatedTime = Time.time;
        }
        
        text.text = _newText;
    }
}