using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class CharacterController2D : MonoBehaviour
{
    private const float _moveSpeed = 5f;
    private const float _rushSpeed = 20f;

    private enum State {
        Normal,
        Rush
    }

    [SerializeField] private LayerMask _dashLayerMask;
    [SerializeField] private float _rushSpeedDropMultiplier = 8f;
    [SerializeField] private float _rushSpeedMinimum = 2f;
    [SerializeField] private float _dashAmount = 7f;

    private Rigidbody2D _rigidbody2D;
    private float _rushCurrentSpeed;
    private Vector3 _moveDrirection;
    private Vector3 _rushDirection;
    private Vector3 _lastMoveDirection;
    private bool _isDashButtonDown = false;
    private State _state;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _state = State.Normal;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Normal:
                float moveX = 0f;
                float moveY = 0f;

                if (Input.GetKey(KeyCode.W)) {
                    moveY = +1f;
                }
                if (Input.GetKey(KeyCode.S)) {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A)) {
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D)) {
                    moveX = +1f;
                }

                _moveDrirection = new Vector3(moveX, moveY).normalized;

                if (moveX != 0 || moveY != 0)
                    _lastMoveDirection = _moveDrirection;

                if (Input.GetKeyDown(KeyCode.F))
                    _isDashButtonDown = true;

                if (Input.GetKeyDown(KeyCode.Space)) {
                    _rushDirection = _lastMoveDirection;
                    _rushCurrentSpeed = _rushSpeed;
                    _state = State.Rush;
                }
                break;

            case State.Rush:
                _rushCurrentSpeed -= _rushCurrentSpeed * _rushSpeedDropMultiplier * Time.deltaTime;
                
                if (_rushCurrentSpeed < _rushSpeedMinimum)
                    _state = State.Normal;
                
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (_state) {
            case State.Normal:
                _rigidbody2D.velocity = _moveDrirection * _moveSpeed;

                if (_isDashButtonDown) {
                    Vector3 dashPosition = transform.position + _lastMoveDirection * _dashAmount;

                    RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _lastMoveDirection, _dashAmount, _dashLayerMask);

                    if (raycastHit2D.collider != null)
                        dashPosition = raycastHit2D.point;

                    _rigidbody2D.MovePosition(dashPosition);
                    _isDashButtonDown = false;
                }
                break;

            case State.Rush:
                _rigidbody2D.velocity = _rushDirection * _rushSpeed;
                break;
        }
    }
}
