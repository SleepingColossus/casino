using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Light light;
    public Text gameOverText;
    public AudioController audioController;

    private bool _gameOver;

    private void Update()
    {
        if (_gameOver && light.intensity > 0)
        {
            light.intensity -= 0.01f;
            gameOverText.enabled = true;
        }
    }

    public void EndGame()
    {
        if (!_gameOver)
        {
            _gameOver = true;
            audioController.PlayGameOverSound();
        }
    }
}
