using UnityEngine;
using FoGD.Shark.States;
using FoGD.Shark.States.Bank;

namespace FoGD.Shark
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
    public class SharkBehaviour : MonoBehaviour
    {
        [Header("Movement")]
        [Tooltip("The range between x and y the shark can be on on the x axis")]
        [SerializeField] private Vector2 xPosRange = new Vector2(-8f, 8f);
        [Tooltip("The range between x and y the shark can be on on the y axis")]
        [SerializeField] private Vector2 yPosRange = new Vector2(-4f, 2f);
        [Tooltip("The amount of time in seconds it takes the shark to go from a to b")]
        [SerializeField] private float swimSpeed = 5f;
        [Tooltip("The amount of time in seconds it takes the shark to jump")]
        [SerializeField] private float jumpSpeed = 3f;
        [Header("Hunger")]
        [SerializeField] private float jumpCooldown = 4f;
        [SerializeField] private float maxHunger = 50f;
        [SerializeField] private float hungerDepleteRate = 2f;
        [Header("Gameplay")]
        [SerializeField] private int damage = 1;
        [SerializeField] private int scorePenalty = 100;

        private float _hunger;

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
        public float JumpCooldown
        { get => jumpCooldown; }
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
        public float MaxHunger
        { get => maxHunger; }
        public float Hunger
        {
            get => _hunger;
            set => _hunger = value;
        }
        public bool IsCollider
        { get => _collider.enabled; }
        public bool IsStunned
        { get; set; }
        public float StunTime
        { get; set; }

        private void Awake()
        {
            // intitialize values
            _bank = new SharkAggressionStateBank(this);

            CurrentState = _bank.PassiveState();
            CurrentState.EnterState();

            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CircleCollider2D>();
            EnableCollider(false);

            _hunger = maxHunger;
        }

        private void Start()
        {
            GameManager.Instance.OnWin.AddListener(() => { Destroy(gameObject); });
            GameManager.Instance.OnLose.AddListener(() => { Destroy(gameObject); });
            GameManager.Instance.ObstacleManager.OnObstacleMissed.AddListener(target => {
                // If the target is shark food add the value

                ISharkFood sharkFood = target as ISharkFood;
                if (sharkFood != null)
                    _hunger += ((ISharkFood)target).Value;

                ISharkStunner sharkStunner = target as ISharkStunner;
                if (sharkStunner != null)
                {
                    IsStunned = true;
                    StunTime = ((ISharkStunner)target).Value;
                }
            });
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

        public void DepleteHunger()
        {
            _hunger -= hungerDepleteRate * Time.deltaTime;
            _hunger = Mathf.Clamp(_hunger, 0f, maxHunger);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameManager.Instance.RemoveHealth(damage);
            GameManager.Instance.RemoveScore(scorePenalty);
        }
    }
}
