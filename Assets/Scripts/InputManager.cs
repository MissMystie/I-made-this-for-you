using System;
using System.Collections.Generic;
using Mystie.Core;
using Mystie.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Mystie
{
    public class InputManager
    {
        public event Action onCancel;
        public event Action onEscape;
        public event Action<Vector2> onNavigate;
        public event Action<int> onZoom;
        public event Action onZoomShift;
        public event Action<Vector2> onClick;
        public event Action<Vector2> onClickSecondary;
        public event Action<Vector2> onPointerMove;
        public event Action onShowMore;

        public Controls actions;
        public Vector2 point { get; private set; }
        public Vector2 navigate { get; private set; }

        public const string KEYBOARD_CTRL = "Keyboard";
        public const string GAMEPAD_CTRL = "Gamepad";

        public InputManager()
        {
            actions = new Controls();

            SubInputs();
        }

        public void SubInputs()
        {
            //actions.UI.Escape.started += OnEscape;
            actions.UI.Cancel.started += OnCancel;
            actions.UI.Click.started += OnClick;
            actions.UI.RightClick.performed += OnClickSecondary;
            actions.UI.Point.performed += OnPoint;
            actions.UI.Navigate.performed += OnNavigate;
            //actions.UI.MouseWheel.performed += OnZoom;
            actions.UI.MiddleClick.started += OnZoomShift;

            actions.UI.Enable();
            actions.Player.Enable();
        }

        public void UnsubInputs()
        {
            actions.UI.Disable();
            actions.Player.Disable();

            //actions.UI.Escape.started -= OnEscape;
            actions.UI.Cancel.started -= OnCancel;
            actions.UI.Click.started -= OnClick;
            actions.UI.RightClick.performed -= OnClickSecondary;
            actions.UI.Point.performed -= OnPoint;
            actions.UI.Navigate.performed -= OnNavigate;
            //actions.UI.MouseWheel.performed -= OnZoom;
            actions.UI.MiddleClick.started -= OnZoomShift;
        }

        /*~InputManager()
        {
            
        }*/

        public void OnCancel(CallbackContext ctx)
        {
            onCancel?.Invoke();
        }

        public void OnEscape(CallbackContext ctx)
        {
            onEscape?.Invoke();
        }

        public void OnNavigate(CallbackContext ctx)
        {
            navigate = ctx.ReadValue<Vector2>();
            onNavigate?.Invoke(navigate);
        }

        public void OnPoint(CallbackContext ctx)
        {
            //if (inputs.currentControlScheme == KEYBOARD_CTRL) { }
            point = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            onPointerMove?.Invoke(point);
        }

        public void OnClick(CallbackContext ctx)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            //if (IsPointerOverUIElement()) return;
            onClick?.Invoke(point);
        }

        public void OnClickSecondary(CallbackContext ctx)
        {
            //Debug.Log("Secondary click");
            onClickSecondary?.Invoke(point);
        }

        private void OnZoom(CallbackContext ctx)
        {
            onZoom?.Invoke(Mathf.RoundToInt(ctx.ReadValue<Vector2>().y));
        }

        private void OnZoomShift(CallbackContext ctx) => onZoomShift?.Invoke();

        private void OnShowMore(CallbackContext ctx) => onShowMore?.Invoke();

        private bool IsPointerOverUIElement()
        {
            // Check if EventSystem exists
            if (EventSystem.current == null)
            {
                Debug.LogError("No EventSystem found in scene!");
                return false;
            }

            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Mouse.current.position.ReadValue();

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            Debug.Log($"Raycast found {results.Count} UI elements");

            // Log what we found
            for (int i = 0; i < results.Count; i++)
            {
                Debug.Log($"Hit {i}: {results[i].gameObject.name} (Layer: {results[i].gameObject.layer})");
            }

            return results.Count > 0;
        }
    }
}
