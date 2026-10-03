using UnityEngine;
using FoGD.Input;
using UnityEngine.InputSystem;

namespace FoGD.Player
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        [SerializeField] private float speed = 3;
        [SerializeField] private float minX  = -10, maxX = 10;

        private InputMap _inputMap;

        private BoxCollider2D _collider;
        private Bounds _bounds;

        private Vector3 _appliedMovement;
        private float _movementInput;

        private void Awake()
        {
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
            if ((_bounds.min.x + transform.position.x <= minX && _movementInput < 0.0f) || (_bounds.max.x + transform.position.x >= maxX && _movementInput > 0.0f))
                return;

            _appliedMovement.x = _movementInput * (speed * Time.deltaTime);
            transform.position += _appliedMovement;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<float>();
        }
    }
}
