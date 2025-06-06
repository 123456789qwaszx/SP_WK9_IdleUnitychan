using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    CharacterController controller;

    public float _moveSpeed;
    public float _jumpPower;
    public float _rotationSensitivety = 1300f;


    public bool _isGrounded;
    public bool _readyToJump;
    public bool CanAct = true;

    
    public float _gravity;
    public float _verticalSpeed;

    const float JumpAbortSpeed = 1f;

    readonly int _HashAirborneVerticalSpeed = Animator.StringToHash("VerticalSpeed");
    readonly int _HashMoveSpeed = Animator.StringToHash("Speed");
    readonly int _HashMoveDirection = Animator.StringToHash("Direction");
    readonly int _HashISGrounded = Animator.StringToHash("Grounded");



    void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!CanAct)
            return;

        Move();
        Jump();

        
        if (!_isGrounded)
            anim.SetFloat(_HashAirborneVerticalSpeed, _verticalSpeed/_jumpPower);

    }


    public void Move()
    {
        Vector3 dir = GameManager.Instance.MouseDir;
        Vector3 moveDir = new Vector3(dir.x, 0, dir.y).normalized;

        _moveSpeed = Mathf.Abs(dir.y) + Mathf.Abs(dir.x) + Mathf.Abs(dir.z);
        anim.speed = 1.5f;


        if (dir.y > 0.1)
        {
            controller.Move(moveDir * Time.deltaTime * _moveSpeed * 2.3f);

            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotationSensitivety);

            anim.SetFloat(_HashMoveSpeed, dir.y);
            anim.SetFloat(_HashMoveDirection, dir.x);
        }
        else if (dir.y < -0.1)
        {
            controller.Move(moveDir * Time.deltaTime * _moveSpeed * 2.3f);

            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotationSensitivety);

            anim.SetFloat(_HashMoveSpeed, Mathf.Abs(dir.y));
            anim.SetFloat(_HashMoveDirection, -dir.x);
        }
        else
        {
            controller.Move(moveDir * Time.deltaTime * Mathf.Lerp(_moveSpeed, 0.01f, 120.0f * Time.deltaTime));

            anim.SetFloat(_HashMoveSpeed, Mathf.Lerp(Mathf.Abs(dir.y), 0.01f, 3.0f * Time.deltaTime));
        }

        if (moveDir == Vector3.zero)
        {
            anim.SetFloat(_HashMoveSpeed, Mathf.Lerp(Mathf.Abs(dir.y), 0, 3.0f * Time.deltaTime));
        }

    }


    public void Jump()
    {
        // 혹시 2단점프나, 공중에서 점프아이템 먹는 기능을 위해 p_ReadyToJump체크도 추가함.
        // 실제 체크는 IsGround로만 해도됨.
        if (!GameManager.Instance.JumpInput && controller.isGrounded)
            _readyToJump = true;

        if (_isGrounded)
        {
            _verticalSpeed = -_gravity;

            if (GameManager.Instance.JumpInput && _readyToJump)
            {
                _verticalSpeed = _jumpPower;
                _isGrounded = false;
                _readyToJump = false;
                anim.SetBool(_HashISGrounded, false);
            }
        }

        else
        {
            if (!GameManager.Instance.JumpInput && _verticalSpeed > 0.0f)
            {
                _verticalSpeed -= JumpAbortSpeed * Time.deltaTime;

            }
            else if (Mathf.Approximately(_verticalSpeed, 0f))
            {
                _verticalSpeed = 0f;
            }

            _verticalSpeed -= _gravity * Time.deltaTime;


            if (_verticalSpeed < -_jumpPower)
            {
                _verticalSpeed = -_jumpPower;
            }
        }
            controller.Move(_verticalSpeed * Vector3.up * Time.deltaTime);
        //다시 점프 뛸 준비

        if (controller.isGrounded)
        {
            _isGrounded = true;
            anim.SetBool(_HashISGrounded, true);
        }
    }
}
