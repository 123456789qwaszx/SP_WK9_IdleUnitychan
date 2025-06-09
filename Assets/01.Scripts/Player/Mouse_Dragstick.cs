using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse_Dragstick : MonoBehaviour
{
    [SerializeField]
    private GameObject _mousecursor;
    [SerializeField]
    private GameObject _mouseBackground;

    Vector2 _LClickPos;
    private float _radius;


    void Start()
    {
        if (Input.GetMouseButtonDown(0)) _LClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _radius = _mouseBackground.GetComponent<RectTransform>().sizeDelta.y / 3;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetMouseDown();
        }
        if (Input.GetMouseButton(0))
        {
            GetMouseDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetMouseUp();
        }
    }


    public void GetMouseDown()
    {
        _mouseBackground.transform.position = Input.mousePosition;
        _mousecursor.transform.position = Input.mousePosition;
        _LClickPos = (Vector2)Input.mousePosition;
    }


    public void GetMouseDrag()
    {
        Vector2 clickDir = (Vector2)Input.mousePosition - _LClickPos;

        float moveDist = Mathf.Min(clickDir.magnitude, _radius);
        Vector2 moveDir = clickDir.normalized;

        Vector2 newPosition = _LClickPos + moveDir * moveDist;
        _mousecursor.transform.position = newPosition;

        GameManager.Instance.MouseDir = moveDir;
    }


    public void GetMouseUp()
    {
        _mousecursor.transform.position = _LClickPos;
        GameManager.Instance.MouseDir = Vector2.zero;
    }
}
