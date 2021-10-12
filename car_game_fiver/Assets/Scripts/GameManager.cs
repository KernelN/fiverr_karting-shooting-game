using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public bool playerWon;
    public int score;
    string racingSceneName;

    //Unity Actions
    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    //Methods
    public void EndGame()
    {
        racingSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(3);
    }

    //Actions
    void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "racingSceneName")
        {
            score = 0;
        }
    }
}
