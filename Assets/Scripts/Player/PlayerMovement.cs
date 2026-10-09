using UnityEngine;
using FoGD.Input;
using UnityEngine.InputSystem;

namespace FoGD.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        [Header("")]
        [SerializeField] private float speed = 3f;
        [Tooltip("Range of the x axis between value's x and y")][SerializeField] private Vector2 xPosRange = new Vector2(-10f, 10f);
        [Tooltip("The static position on the y axis")][SerializeField] private float yPos = -2.5f;

        private InputMap _inputMap;

        private Rigidbody2D _rigidBody;

        private BoxCollider2D _collider;
        private Bounds _bounds;

        private Vector2 _appliedMovement;
        private float _movementInput;

        private void Awake()
        {
            // Initialize values
            _inputMap = new InputMap();

            _rigidBody = GetComponent<Rigidbody2D>();

            _collider = GetComponent<BoxCollider2D>();
            _bounds = _collider.bounds;
        }

        private void OnEnable()
        {
            // Enable input
            _inputMap.Player.Enable();
            _inputMap.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            // Disable input
            _inputMap.Player.Disable();
            _inputMap.Player.RemoveCallbacks(this);
        }

        private void FixedUpdate()
        {
            // Check if the player is in the bounds
            if ((_bounds.min.x + transform.position.x <= xPosRange.x) || 
                (_bounds.max.x + transform.position.x >= xPosRange.y))
                _rigidBody.linearVelocityX = 0f;

            // Keep player within the bounds
            float clampX = Mathf.Clamp(transform.position.x, xPosRange.x + _bounds.max.x, xPosRange.y + _bounds.min.x);
            _rigidBody.position = new Vector2(clampX, yPos);

            // Calculate new velocity
            _appliedMovement.x = _movementInput * (speed * Time.deltaTime);
            _rigidBody.linearVelocity += _appliedMovement;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<float>();
        }
    }
}
