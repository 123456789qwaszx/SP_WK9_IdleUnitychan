using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIOpenClose : MonoBehaviour
{
    public GameObject inven;
    bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OCInven();
        }
    }

    public void OCInven()
    {

        if (inven != null)
        {
            isActive = !isActive;
            inven.SetActive(!inven.activeSelf);

            if (isActive) // 카메라 제어만.
            {
                DisableGameCamLook();
            }
            else
            {
                EnableGameCamLook();
            }
        }
    }

    
    public void EnableGameCamLook() // 카메라 움직임 활성화 + 커서 숨김
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    public void DisableGameCamLook() // 카메라 움직임 비활성화 + 커서 보임
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
