using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameoverWinnerText : MonoBehaviour
{
    private Player _winner;
    private Player _loser;
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = _winner.name +" has won! :D\n " + _loser.name + " has lost! :(";
    }

    public void UpdateWinner(Player winner, Player loser)
    {
        _winner = winner;
        _loser = loser;
    }

}