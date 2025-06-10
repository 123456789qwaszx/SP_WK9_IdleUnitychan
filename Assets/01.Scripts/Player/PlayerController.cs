using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 또 플레이어 애니메이션 Direction 값의 -1, +1이 현재 dir.y로만 조절되고 있는데,
// dir.x조건도 추가할것.
public class PlayerController : MonoBehaviour
{
    Animator anim;
    CharacterController controller;

    public bool _isGrounded;
    public bool _readyToJump;

    float _curDirx;

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

    void FixedUpdate()
    {
        if (!_isGrounded)
            anim.SetFloat(_HashAirborneVerticalSpeed, _verticalSpeed / CharacterManager.Instance.Player.stat.JumpPower);
    }


    public void Move()
    {
        Vector3 dir = GameManager.Instance.MouseDir;
        Vector3 moveDir = new Vector3(dir.x, 0, dir.y).normalized;
        anim.speed = 1.5f;


        if (dir.y > 0.2)
        {
            controller.Move(moveDir * Time.deltaTime * CharacterManager.Instance.Player.stat.MoveSpeed * 2.3f);

            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * CharacterManager.Instance.Player.stat.RotationSensitivity);

            anim.SetFloat(_HashMoveSpeed, dir.y + Mathf.Abs(dir.x));
            _curDirx = Mathf.Lerp(_curDirx, 1, Time.deltaTime * 0.5f);
            anim.SetFloat(_HashMoveDirection, _curDirx);
        }
        else if (dir.y < -0.2)
        {
            controller.Move(moveDir * Time.deltaTime * CharacterManager.Instance.Player.stat.MoveSpeed * 2.3f);

            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * CharacterManager.Instance.Player.stat.RotationSensitivity);

            anim.SetFloat(_HashMoveSpeed, Mathf.Abs(dir.y) + Mathf.Abs(dir.x));
            _curDirx = Mathf.Lerp(_curDirx, -1, Time.deltaTime * 0.5f);
            anim.SetFloat(_HashMoveDirection, _curDirx);
        }
        else if (dir.y >= -0.2 && dir.y <= 0.2 && dir.y != 0)
        {
            controller.Move(moveDir * Time.deltaTime * CharacterManager.Instance.Player.stat.MoveSpeed * 1.7f);

            Quaternion lookRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * CharacterManager.Instance.Player.stat.RotationSensitivity);

            anim.SetFloat(_HashMoveSpeed, Mathf.Abs(dir.y) + Mathf.Abs(dir.x));
            _curDirx = Mathf.Lerp(_curDirx, 0, Time.deltaTime * 0.5f);
            anim.SetFloat(_HashMoveDirection, _curDirx);

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
                _verticalSpeed = CharacterManager.Instance.Player.stat.JumpPower;
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


            if (_verticalSpeed < -CharacterManager.Instance.Player.stat.JumpPower)
            {
                _verticalSpeed = -CharacterManager.Instance.Player.stat.JumpPower;
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

//
// 1. 처음 좌우 이동이 안먹던건, dir y의 예외처리가 이상해서, else로 빠졌기 때문.
// 2. 그걸 수정했더니, 위를 볼땐 dir/ 아래를 볼땐 -dir을 하는 과정에서 고개가 확확 튀어버림.
// 3. 이걸 수정하기 위해선,
//   1) 우선 animation Blending을 해야하고
//   2) 스크립트에서도 Lerp를 사용해야 하는데
// 이번기회에 Lerp의 조건을 다시 깨달음.
// (float a,float b,float c)라고 적혀있지만
// a는 무조건 a= Lerp(a, ...)으로 해야 실시간으로 반영됨. 만약 a=Lerp(0.2, ...)로 하면 a= 0.2로 한것과 똑같음.

//   3)마찬가지로 조건도 좌우 Dir이 확 뒤집힐때는
// 최대한 0에 가깝도록 해야 자연스러움.
// _curDirx = Mathf.Lerp(_curDirx, 0, Time.deltaTime * 0.5f);
// 이렇게.

//  4)추가로 아직 안한것인데,
// if (dir.X ...) 조건으로 정확한 애니메이션 방향을 잡아줘야 조금 더 정확하고 예쁜 애니메이션을 만들 수 있음.