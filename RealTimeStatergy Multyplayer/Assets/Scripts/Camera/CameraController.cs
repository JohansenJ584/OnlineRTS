using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private Transform playerCameraTransform = null;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 10f;
    [SerializeField] private Vector2 screenXlimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;

    private Vector2 previosInput;
    private Controls controls;
    public override void OnStartAuthority()
    {
        playerCameraTransform.gameObject.SetActive(true);
        playerCameraTransform.transform.position += new Vector3(0f, 0f, -10f);
        controls = new Controls();

        controls.Player.MoveCamera.performed += SetPreviosInput;
        controls.Player.MoveCamera.canceled += SetPreviosInput;
        controls.Enable();
    }
    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority || !Application.isFocused)
        {
            return;
        }
        UpdateCameraPosition();
    }
    private void UpdateCameraPosition()
    {
        Vector3 pos = playerCameraTransform.position;
        if(previosInput == Vector2.zero)
        {
            Vector3 cursorMovment = Vector3.zero;
            Vector2 cursorPosition = Mouse.current.position.ReadValue();
            if(cursorPosition.y >= Screen.height - screenBorderThickness)
            {
                cursorMovment.z += 1;
            }
            else if(cursorPosition.y <= screenBorderThickness)
            {
                cursorMovment.z -= 1;
            }
            if (cursorPosition.x >= Screen.width - screenBorderThickness)
            {
                cursorMovment.x += 1;
            }
            else if (cursorPosition.x <= screenBorderThickness)
            {
                cursorMovment.x -= 1;
            }
            pos += cursorMovment.normalized * speed * Time.deltaTime;
        }
        else
        {
            pos += new Vector3(previosInput.x, 0f, previosInput.y) * speed * Time.deltaTime;
        }
        pos.x = Mathf.Clamp(pos.x, screenXlimits.x - 10f, screenXlimits.y + 10f);
        pos.z = Mathf.Clamp(pos.z, screenZLimits.x - 10f, screenZLimits.y + 10f);

        playerCameraTransform.position = pos;
    }

    private void SetPreviosInput(InputAction.CallbackContext ctx)
    {
        previosInput = ctx.ReadValue<Vector2>();
    }
}
