using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    private float _timer;
    private float _score;
    private bool _isGameOver;

    private void Start()
    {
        Time.timeScale = 1f;
        _gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isGameOver)
        {
            if (Keyboard.current != null &&
                Keyboard.current.rKey.wasPressedThisFrame)
            {
                RestartGame();
            }

            return;
        }

        _score += Time.deltaTime * 10f;
        _scoreText.text = "SCORE : " + Mathf.FloorToInt(_score);

        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnObstacle();
            _timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        if (_obstaclePrefab == null)
        {
            return;
        }

        Instantiate(
            _obstaclePrefab,
            new Vector3(10f, -2.5f, 0f),
            Quaternion.identity);
    }

    public void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f;
        _gameOverText.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}