using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = 0.15f;
    [SerializeField] private float _jumpHeight = 12.0f;
    [SerializeField] private GameObject _ledgeChecker;
    private bool _jumping;
    private bool _climbingUp = false;
    private Ledge _currentLedge;

    private CharacterController _controller;
    private Vector3 _velocity;

    private Animator _animator;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("null component _controller::Player");
        }

        _animator = GetComponentInChildren<Animator>();

        if (_animator == null)
        {
            Debug.LogError("null component _animator::Player");
        }
    }

    void Update()
    {
        CalculateMovement();
        ClimbingUpLedge();

    }

    private void ClimbingUpLedge()
    {
        if (_climbingUp == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetTrigger("ClimbUp");
                _climbingUp = false;
            }
        }
    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            if (_jumping == true)
            {
                _jumping = false;
                _animator.SetBool("Jumping", false);
            }

            float _direction = Input.GetAxisRaw("Horizontal");
            _jumping = false;
            _velocity = new Vector3(_direction, 0, 0) * _speed;

            if (_direction != 0)
            {

                Vector3 facing = transform.localEulerAngles;

                if (_velocity.x > 0)
                {
                    facing.y = 0;
                }
                else
                {
                    facing.y = 180;
                }

                transform.localEulerAngles = facing;
            }

            _animator.SetFloat("Speed", Mathf.Abs(_direction));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y += _jumpHeight;
                _jumping = true;
                _animator.SetBool("Jumping", true);
            }
        }
        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void LedgeGrabbed(GameObject handPos, Ledge currentLedge)
    {
        _climbingUp = true;
        _animator.SetBool("LedgeGrab", true);
        _animator.SetFloat("Speed", 0);
        _animator.SetBool("Jumping", false);
        _controller.enabled = false;
        transform.position = handPos.transform.position;
        _currentLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        transform.position = _currentLedge.GetStandPos();
        _animator.SetBool("LedgeGrab", false);
        _controller.enabled = true;
    }
}
