using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public float checkRate = 0.1f;
    public float lastCheckTime;

    int _mask = (1 << (int)Layer.Ground) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Item);

    Texture2D _attackIcon;
    Texture2D _HandIcon;
    Texture2D _LootIcon;
    public bool IsHandCursor;


    void Start()
    {
        checkRate = 0.1f;

        _attackIcon = Resources.Load<Texture2D>("Cursor/Cursor_Attack");
        _LootIcon = Resources.Load<Texture2D>("Cursor/Cursor_Loot");
        _HandIcon = Resources.Load<Texture2D>("Cursor/Cursor_Hand");
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            UpdateMouseCursor();
        }
    }
    
    
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
}
