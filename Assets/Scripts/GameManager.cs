using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

namespace KTC.Game
{
    public class GameManager : MonoBehaviour
    {
        private const float SPAWN_INTERVAL = 2f;
        private const float SCORE_RATE = 10f;
        private const float SPAWN_X = 10f;
        private const float SPAWN_Y = -2.5f;
        private const float SPAWN_Z = 0f;

        [SerializeField] private GameObject _obstaclePrefab = null;
        [SerializeField] private TextMeshProUGUI _scoreText = null;
        [SerializeField] private TextMeshProUGUI _gameOverText = null;

        private float _timer = 0f;
        private float _score = 0f;
        private bool _isGameOver = false;

        private float _elapsedTime = 0f;

        public float ElapsedTime
        {
            get { return _elapsedTime; }
        }

        private void Start()
        {
            Time.timeScale = 1f;

            if (_gameOverText != null)
            {
                _gameOverText.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_isGameOver)
            {
                if (Keyboard.current != null &&
                    Keyboard.current.rKey.wasPressedThisFrame)
                {
                    RestartGame();
                }

                return;
            }

            _score += Time.deltaTime * SCORE_RATE;

            if (_scoreText != null)
            {
                _scoreText.text = string.Format(ConstGame.SCORE_TEXT,Mathf.FloorToInt(_score));
            }

            _timer += Time.deltaTime;

            if (_timer >= SPAWN_INTERVAL)
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
                new Vector3(SPAWN_X, SPAWN_Y, SPAWN_Z),
                Quaternion.identity);
        }

        public void GameOver()
        {
            _isGameOver = true;
            Time.timeScale = 0f;

            if (_gameOverText != null)
            {
                _gameOverText.text = ConstGame.GAME_OVER_TEXT + "\n" + ConstGame.RESTART_TEXT;
                _gameOverText.gameObject.SetActive(true);
            }
        }

        private void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}