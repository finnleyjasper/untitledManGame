using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = player.name + "\n" + "Score: " + player.Score.ToString() + "\nLives: " + player.Lives.ToString();
    }
}
