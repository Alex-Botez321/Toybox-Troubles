
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private RectTransform cursorTransform;
    [SerializeField] private float cursorSpeed = 100;

    private bool previousMouseState;
    private Mouse virtualMouse;

    [SerializeField]private Camera someCamera;


    private void OnEnable()
    {
        //creates a virtual mouse component, we'll have a destructor so we can only see
        //it once the game starts but oncewe stop playin in editor it dissapears
        if(virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        //pairs the user to the playerInput componenet on the cursor with the virtual mouse 
        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if(cursorTransform != null)
        {
            Vector2 cursorPosition = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, cursorPosition);
        }

        InputSystem.onAfterUpdate += UpdateMotion;

    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if (virtualMouse != null || Gamepad.current == null)
        {
            return;
        }

        //reading value on the gamepad
        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        stickValue = stickValue * cursorSpeed * Time.deltaTime;

        Vector2 currentPostion = virtualMouse.position.ReadValue();

        //stick value acts as delta, and we change current position of the cursor
        Vector2 newPosition = currentPostion + stickValue;
        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, stickValue);


        //checking if the button is pressed,
        //otherwise if its already pressed we cant change it
        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (previousMouseState != aButtonIsPressed)
        {
            //output MouseState variable within the scope of this if statement
            //it copies the state of the virtual mouse
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;

        if(canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            someCamera = null;
        }
        else
        {
            someCamera = Camera.main;
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, someCamera, out anchoredPosition);

        cursorTransform.anchoredPosition = anchoredPosition;
    }
}
