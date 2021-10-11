using UnityEngine;
using TMPro; //used for score only

public class UIGameOver : MonoBehaviour
{
    [SerializeField] GameObject victoryText;
    [SerializeField] GameObject defeatText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool gameUsesScore = true;
    GameManager gameManager;

    void Start()
    {
        //Game Manager
        gameManager = GameManager.Get();

        //Victory / Defeat
        SetVictoryDefeat();

        if (!gameUsesScore) return;
        //Score
        SetScore();
    }

    void SetVictoryDefeat()
    {
        if (gameManager.playerWon)
        {
            victoryText.SetActive(true);
        }
        else
        {
            defeatText.SetActive(true);
        }
    }
    void SetScore()
    {
        scoreText.text = "Score: " + gameManager.score.ToString("000");
    }
}
