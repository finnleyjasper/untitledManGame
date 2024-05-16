/* 
* filename: GameUIManager.cs
* author: Finnley
* description: manages and updates variables for the ui to print during gameplay; player score/lives
*
* reference(s) - https://levelup.gitconnected.com/ease-of-building-ui-elements-in-unity-4f501c7e7c5e
*
* created: 16 May 2024
* last modified:  16 May 2024
*/

/* Notes:
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;

    private void Start()
    {
       
    }

    public void UpdateScoreUI(Player player)
    {
        _scoreText.text = player.gameObject.name + "'s score: " + player.Score.ToString();
    }

    public void UpdateLivesUI(Player player)
    {
        _livesText.text = player.gameObject.name + " lives remaining: " + player.Lives.ToString();
    }
}
