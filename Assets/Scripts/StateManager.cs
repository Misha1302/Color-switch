using UnityEngine.SceneManagement;

public sealed class StateManager
{
    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}