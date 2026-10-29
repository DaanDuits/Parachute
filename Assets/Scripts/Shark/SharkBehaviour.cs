using UnityEngine;
using FoGD.Shark.States;
using FoGD.Shark.States.Bank;

namespace FoGD.Shark
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
    public class SharkBehaviour : MonoBehaviour
    {
        [Tooltip("The range between x and y the shark can be on on the x axis")][SerializeField] private Vector2 xPosRange = new Vector2(-8f, 8f);
        [Tooltip("The range between x and y the shark can be on on the y axis")][SerializeField] private Vector2 yPosRange = new Vector2(-4f, 2f);
        [Tooltip("The amount of time in seconds it takes the shark to go from a to b")][SerializeField] private float swimSpeed = 5f;
        [Tooltip("The amount of time in seconds it takes the shark to jump")][SerializeField] private float jumpSpeed = 3f;
        [Range(0f, 1f)][SerializeField] private float aggroToJump = 0.1f;

        private float _aggression;

        private SharkAggressionStateBank _bank;
        private SpriteRenderer _renderer;
        private CircleCollider2D _collider;

        private int _direction = 1;

        public Vector2 XPosRange
        { get => xPosRange; }
        public Vector2 YPosRange
        { get => yPosRange; }
        public float SwimSpeed
        { get => swimSpeed; }
        public float JumpSpeed
        { get => jumpSpeed; }
        public float AggroToJump
        { get => aggroToJump; }
        public SharkAggressionState CurrentState
        { get; set; }
        public int Direction
        { get => _direction; }
        public Vector2 Position
        { 
            get => transform.position; 
            set => transform.position = value; 
        }
        public Quaternion Rotation
        { set => transform.rotation = value; }
        public float Aggression
        {
            get => _aggression;
            set => _aggression = value;
        }
        public bool IsCollider
        { get => _collider.enabled; }

        private void Awake()
        {
            // intitialize values
            _bank = new SharkAggressionStateBank(this);

            CurrentState = _bank.PassiveState();
            CurrentState.EnterState();

            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();
            EnableCollider(false);
        }

        private void Start()
        {
            GameManager.Instance.OnWin.AddListener(() => { Destroy(gameObject); });
            GameManager.Instance.OnLose.AddListener(() => { Destroy(gameObject); });
        }

        private void Update()
        {
            CurrentState.UpdateState();
        }

        /// <summary>
        /// Flip the sprite and swimming direction
        /// </summary>
        public void Flip()
        {
            _direction *= -1;
            _renderer.flipX = _direction == -1;
            _collider.offset = new Vector2(_direction, 0f);
        }

        public void EnableCollider(bool enabled)
        {
            _collider.enabled = enabled;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameManager.Instance.RemoveHealth(1);
        }
    }
}
