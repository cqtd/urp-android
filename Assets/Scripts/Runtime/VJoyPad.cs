using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.Cinemachine
{
    public class VJoyPad : MonoBehaviour, 
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        enum JoystickType
        {
            Fixed,
            Floating,
        }

        [SerializeField] Canvas canvas = default;

        [SerializeField]
        JoystickType joystickType = JoystickType.Fixed;

        [SerializeField] Image imageBg = default;

        [SerializeField] Image imageJoystick = default;

        [SerializeField] Text text = default;

        private Vector3 _inputVector;
        public Vector3 InputVector
        {
            get
            {
                return this._inputVector;
            }
        }

        public static VJoyPad instance;

        void Awake()
        {
            instance = this;
        }

        void MoveJoystickToCurrentTouchPosition()
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.transform as RectTransform, Input.mousePosition, this.canvas.worldCamera, out pos);
            this.imageBg.rectTransform.position = this.canvas.transform.TransformPoint(pos);
        }


        public void OnPointerDown(PointerEventData e)
        {
            if (this.joystickType == JoystickType.Floating)
            {
                MoveJoystickToCurrentTouchPosition();
            }

            OnDrag(e);
        }


        public void OnDrag(PointerEventData e)
        {
            if (e.pressPosition.x > Screen.width * 0.5f) return;
            
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                this.imageBg.rectTransform,
                e.position,
                e.pressEventCamera,
                out pos))
            {

                // if (pos.x > Screen.width * 0.5f) return;

                pos.x = (pos.x / this.imageBg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / this.imageBg.rectTransform.sizeDelta.y);

                this._inputVector = new Vector3(pos.x * 2, 0, pos.y * 2);
                this._inputVector = (this._inputVector.magnitude > 1.0f) ? this._inputVector.normalized : this._inputVector;

                Vector3 joystickPosition = new Vector3(
                    this._inputVector.x * (this.imageBg.rectTransform.sizeDelta.x * .4f),
                    this._inputVector.z * (this.imageBg.rectTransform.sizeDelta.y * .4f));
                this.imageJoystick.rectTransform.anchoredPosition = joystickPosition;
                
                
            }
        }

        public void OnPointerUp(PointerEventData e)
        {
            this._inputVector = Vector3.zero;
            this.imageJoystick.rectTransform.anchoredPosition = Vector3.zero;
        }

        void Update()
        {
            text.text = $"{InputVector.x:N2}, {InputVector.y:N2}, {InputVector.z:N2}";
        }
    }
}
