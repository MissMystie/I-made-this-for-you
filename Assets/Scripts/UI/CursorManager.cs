using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Mystie.Core
{
    public class CursorManager : MonoBehaviour
    {
        private InputManager input;

        [field: SerializeField] public RectTransform cursor { get; private set; }
        [SerializeField] private Animator anim;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            input = GameManager.Instance.inputManager;
            UnityEngine.Cursor.visible = false;
        }

        protected void OnEnable()
        {
            input.actions.UI.Point.performed += OnPoint;
            SetPosition(input.point);
            SetVisible(true);
        }

        protected void OnDisable()
        {
            input.actions.UI.Point.performed -= OnPoint;
            SetVisible(false);
        }

        public void SetVisible(bool visible = true)
        {
            cursor.gameObject.SetActive(visible);
        }

        public void OnPoint(CallbackContext ctx)
        {
            SetPosition(ctx.ReadValue<Vector2>());
        }

        public void SetPosition(Vector2 pos)
        {
            cursor.transform.position = pos;
        }
    }
}
