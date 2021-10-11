using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public bool playerWon;
    public int score;

    public void EndGame()
    {
        SceneManager.LoadScene(3);
    }
}
