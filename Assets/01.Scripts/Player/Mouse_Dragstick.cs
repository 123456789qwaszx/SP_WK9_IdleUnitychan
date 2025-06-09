using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Layer
{
    Ground = 6,
    Monster = 7,
    Wall = 8,
    Item = 9
}

public class Mouse_Dragstick : MonoBehaviour
{
    Vector2 _LClickPos;

    [SerializeField]
    private GameObject _mousecursor;

    [SerializeField]
    private GameObject _mouseBackground;

    private float _radius;


    public float checkRate = 0.1f;
    public float lastCheckTime;

    Texture2D _attackIcon;
    Texture2D _HandIcon;
    Texture2D _LootIcon;
    public bool IsHandCursor;

    void Start()
    {
        checkRate = 0.05f;

        _attackIcon = Resources.Load<Texture2D>("Cursor/Cursor_Attack");
        _LootIcon = Resources.Load<Texture2D>("Cursor/Cursor_Loot");
        _HandIcon = Resources.Load<Texture2D>("Cursor/Cursor_Hand");

        if (Input.GetMouseButtonDown(0)) _LClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _radius = _mouseBackground.GetComponent<RectTransform>().sizeDelta.y / 3;


    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            UpdateMouseCursor();
        }

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

        if (Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.JumpInput = true;

        }
        if (Input.GetMouseButtonUp(1))
        {
            GameManager.Instance.JumpInput = false;
        }
    }



    int _mask = (1 << (int)Layer.Ground) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Item );

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Layer.Monster)
            {
                Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                IsHandCursor = false;
            }
            else if (hit.collider.gameObject.layer == (int)Layer.Item)
            {
                Cursor.SetCursor(_LootIcon, new Vector2(_LootIcon.width / 3, 0), CursorMode.Auto);
                IsHandCursor = false;
            }
            else
            {
                Cursor.SetCursor(_HandIcon, Vector2.zero, CursorMode.Auto);
            }
        }

        if (!Physics.Raycast(ray, out hit, 100.0f, _mask) && (IsHandCursor == false))
        {
            Cursor.SetCursor(_HandIcon, Vector2.zero, CursorMode.Auto);
            IsHandCursor = true;
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
