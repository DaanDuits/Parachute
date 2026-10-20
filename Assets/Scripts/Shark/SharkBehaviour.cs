using UnityEngine;
using FoGD.Shark.States;
using FoGD.Shark.States.Bank;

namespace FoGD.Shark
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SharkBehaviour : MonoBehaviour
    {
        [Tooltip("The range between x and y the shark can be on on the x axis")][SerializeField] private Vector2 xPosRange = new Vector2(-8f, 8f);
        [Tooltip("The amount of time in seconds it takes the shark to go from a to b")][SerializeField] private float swimSpeed = 5f;
        [SerializeField] private float startX, endX;
        [SerializeField] private float startHeight, endHeight;
        [SerializeField] private float jumpTime;

        private SharkAggressionStateBank _bank;
        private SpriteRenderer _renderer;

        int _direction = 1;

        float _timer = 0f;

        public Vector2 XPosRange
        { get => xPosRange; }
        public float SwimSpeed
        { get => swimSpeed; }
        public SharkAggressionState CurrentState
        { get; set; }
        public int Direction
        { get => _direction; }
        public Vector2 Position
        { set => transform.position = value; }

        private void Awake()
        {
            _bank = new SharkAggressionStateBank(this);

            CurrentState = _bank.PassiveState();
            CurrentState.EnterState();

            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            CurrentState.UpdateState();

            //float x = Mathf.Lerp(startX, endX, _timer);

            //float y = 4f * endHeight * _timer * (1f - _timer) + startHeight;

            //transform.position = new Vector2(x, y);

            //_timer += Time.deltaTime / jumpTime;
            //_timer = _timer % 1f;
        }

        public void Flip()
        {
            _direction *= -1;
            _renderer.flipX = _direction == -1;
        }
    }
}
