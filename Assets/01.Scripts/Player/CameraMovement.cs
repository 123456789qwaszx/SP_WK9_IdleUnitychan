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
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, 2.0f * Time.deltaTime);
    }
}
