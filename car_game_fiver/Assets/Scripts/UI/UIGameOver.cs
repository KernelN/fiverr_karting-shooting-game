using UnityEngine;
//using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] GameObject victoryText;
    [SerializeField] GameObject defeatText;

    void Start()
    {
        if (GameManager.Get().playerWon)
        {
            victoryText.SetActive(true);
        }
        else
        {
            defeatText.SetActive(true);
        }
    }
}
