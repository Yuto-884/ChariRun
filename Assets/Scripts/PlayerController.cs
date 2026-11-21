using UnityEngine;
using UnityEngine.InputSystem;

namespace KTC.Game
{
    public class PlayerController : MonoBehaviour
    {
        private const float JUMP_POWER = 15f;
        private const string GROUND_OBJECT_NAME = "Ground";
        private const string OBSTACLE_TAG = "Obstacle";

        private Rigidbody2D _rigidbody2D = null;
        private bool _isGrounded = false;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Keyboard.current != null &&
                Keyboard.current.spaceKey.wasPressedThisFrame &&
                _isGrounded)
            {
                _rigidbody2D.linearVelocity = new Vector2(0f, JUMP_POWER);
                _isGrounded = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == GROUND_OBJECT_NAME)
            {
                _isGrounded = true;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.name == GROUND_OBJECT_NAME)
            {
                _isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.name == GROUND_OBJECT_NAME)
            {
                _isGrounded = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(OBSTACLE_TAG))
            {
                KTC.Game.GameManager gameManager = FindFirstObjectByType<KTC.Game.GameManager>();

                if (gameManager != null)
                {
                    gameManager.GameOver();
                }
            }
        }
    }
}