using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Vector3 _offset;

    Vector3 lastdir = Vector3.zero;

    void Start()
    {
        _offset = transform.position - _target.position;
    }

    void LateUpdate()
    {
        // if (lastdir == Vector3.zero)
        // {
            // Vector3 dir = GameManager.Instance.MouseDir;
            // Vector3 moveDir = new Vector3(dir.x, 0, dir.y);

            transform.position =_target.position + _offset;

            //lastdir = dir;
        // }
        // else
        // {
        //     Vector3 lastdir = GameManager.Instance.MouseDir;
        //     Vector3 moveDir = new Vector3(lastdir.x, 0, lastdir.y);

        //     transform.position = -moveDir + _target.position + _offset;
        // }
    }
}
