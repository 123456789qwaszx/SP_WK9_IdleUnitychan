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


    Texture2D _attackIcon;
    Texture2D _HandIcon;
    Texture2D _LootIcon;

    void Start()
    {
        _attackIcon = Resources.Load<Texture2D>("Cursor/Cursor_Attack");
        _LootIcon = Resources.Load<Texture2D>("Cursor/Cursor_Loot");
        _HandIcon = Resources.Load<Texture2D>("Cursor/Cursor_Hand");

        if (Input.GetMouseButtonDown(0)) _LClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _radius = _mouseBackground.GetComponent<RectTransform>().sizeDelta.y / 3;


    }

    void Update()
    {
        UpdateMouseCursor();

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
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == (int)Layer.Monster)
            {
                Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
            }
            else if (hit.collider.gameObject.layer == (int)Layer.Item)
            {
                Cursor.SetCursor(_LootIcon, new Vector2(_LootIcon.width / 3, 0), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(_HandIcon, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    // Action에서 Input.GetKey(AnyKey) 이것의 정확한 작용을 모르니 에러가 자꾸 나는데...
    // 아무튼 필요한 개념자체가 까다로운 건 아니야. 그냥 지하철역 외우듯이 알기만 하면 되는건데...

    //1. GetMouseDown
    //2. GetMouseDrag
    //3. GetMouseUp
    // 이렇게 세가지가 필요해.

    // 저렇게 세가지 동작이 일어났을 때,
    // 또 마우스 l,m,r 별로 다르게 설정해줘야 해.

    // 이건 너무너무너무 귀찮은데?

    // Vector2 _LClickPos;, Vector2 _RClickPos;, Vector2 _MClickPos;
    // 이거 세 개 중에서, 좌클릭만 살리자.

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
