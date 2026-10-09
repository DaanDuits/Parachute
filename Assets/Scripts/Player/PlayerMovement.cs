using UnityEngine;
using FoGD.Input;
using UnityEngine.InputSystem;

namespace FoGD.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        [SerializeField] private float speed = 3;
        [SerializeField] private float minX  = -10, maxX = 10;
        [SerializeField] private float yPos = -2.5f;

        private InputMap _inputMap;

        private Rigidbody2D _rigidBody;

        private BoxCollider2D _collider;
        private Bounds _bounds;

        private Vector2 _appliedMovement;
        private float _movementInput;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _bounds = _collider.bounds;
        }

        private void OnEnable()
        {
            _inputMap = new InputMap();
            _inputMap.Player.Enable();
            _inputMap.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            _inputMap.Player.Disable();
            _inputMap.Player.RemoveCallbacks(this);
        }

        private void Update()
        {
            if ((_bounds.min.x + transform.position.x <= minX && _movementInput <= 0.0f) || (_bounds.max.x + transform.position.x >= maxX && _movementInput >= 0.0f))
                _rigidBody.linearVelocityX = 0;

            _appliedMovement.x = _movementInput * (speed * Time.deltaTime);
            _rigidBody.linearVelocity += _appliedMovement;

            float clampX = Mathf.Clamp(transform.position.x, _bounds.min.x + transform.position.x, _bounds.max.x + transform.position.x);

            _rigidBody.position = new Vector2(clampX, yPos);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<float>();
        }
    }
}
