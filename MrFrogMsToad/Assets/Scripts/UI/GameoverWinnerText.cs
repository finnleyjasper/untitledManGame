/* 
* filename: GameoverWinnerText.cs
* author: Finnley
* description: updates the game over text on the ui to show reason for game over and the winner/loser
*
*
* created: 20 May 2024
* last modified:  28 May 2024
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameoverWinnerText : MonoBehaviour
{
    private Player _winner;
    private Player _loser;
    private string _reasonForGameOver;
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = _reasonForGameOver + "\n\n" + _winner.name +" has won! :D\n " + _loser.name + " has lost! :(";
    }

    public void UpdateText(Player winner, Player loser, string reasonForGameOver)
    {
        _reasonForGameOver = reasonForGameOver;
        _winner = winner;
        _loser = loser;
    }

}