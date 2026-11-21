
using UnityEngine;

namespace KTC.Game
{
    public class Obstacle : MonoBehaviour
    {
        private const float MOVE_SPEED = 5f;
        private const float ACCELERATION = 0.15f;
        private const float MAX_SPEED = 12f;
        private const float DESTROY_POSITION_X = -15f;

        private GameManager _gameManager = null;

        private void Start()
        {
            _gameManager = FindFirstObjectByType<GameManager>();
        }

        private void Update()
        {
            float currentSpeed = MOVE_SPEED;

            if (_gameManager != null)
            {
                currentSpeed += _gameManager.ElapsedTime * ACCELERATION;
            }

            currentSpeed = Mathf.Min(currentSpeed, MAX_SPEED);

            transform.position += Vector3.left * currentSpeed * Time.deltaTime;

            if (transform.position.x < DESTROY_POSITION_X)
            {
                Destroy(gameObject);
            }
        }
    }
}
