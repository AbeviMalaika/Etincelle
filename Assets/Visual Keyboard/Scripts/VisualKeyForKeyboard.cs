
//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace VisualKeyboard
//{
//    public class VisualKeyForKeyboard : MonoBehaviour
//    {
//        public static event Action<VisualKeyForKeyboard> OnKeyboardButtonClick;

//        [Header("Keyboard Key")]
//        [Tooltip("Keycode for old input system.")]
//        public KeyCode oldKeyCode;
//        [Tooltip("Control path for new Unity's Input System.")]
//        public string controlPath;
//        [Tooltip("Character is produced when button is pressed.")]
//        public char character;
//        [Tooltip("Character is produced when button is pressed (with SHIFT hold).")]
//        public char shiftedCharacter;
//        [Tooltip("Normalized position of key on keyboard panel, from left bottom corner. \r\n For example, LeftCTRL = (0f, 0f), ESC = (0f, 1f), NumpadEnter = (1f, 0f) etc...")]
//        public Vector2 normalizedPosition;

//        [Header("Refs")]
//        public Image image;
//        public Image overlay;

//        public virtual void Highlight(bool isOn) {
//            if (isOn)
//                HighlightON();
//            else
//                HighlightOFF();
//        }

//        [ContextMenu("Highlight OFF")]
//        public virtual void HighlightOFF() {
//            overlay.gameObject.SetActive(false);
//        }

//        [ContextMenu("Highlight ON")]
//        public virtual void HighlightON() {
//            overlay.gameObject.SetActive(true);
//        }

//        public virtual void HighlightON(Color color) {
//            color.a = Mathf.Clamp(color.a, 0.19f, 0.21f);
//            overlay.color = color;
//            overlay.gameObject.SetActive(true);
//        }

//        public virtual void HighlightAnimation(Color color, float fadeTime) {
//            StopAllCoroutines();
//            StartCoroutine(HighlightAnimating(color, fadeTime));
//        }

//        protected virtual IEnumerator HighlightAnimating(Color color, float fadeTime) {
//            float endTime = Time.time + fadeTime;
//            color.a = Mathf.Clamp(color.a, 0.19f, 0.21f);
//            float startAlpha = color.a;
//            overlay.color = color;
//            overlay.gameObject.SetActive(true);

//            while (color.a > 0.01f) {
//                yield return null;
//                float time = 1f - ((endTime - Time.time) / fadeTime);
//                Color col = overlay.color;
//                col.a = Mathf.Lerp(startAlpha, 0, time);
//                overlay.color = col;
//            }

//            overlay.gameObject.SetActive(false);
//        }

//        // UI callback.
//        public virtual void UI_Click() {
//            Debug.Log($"Keyboard button is clicked: {gameObject.name}", gameObject);
//            OnKeyboardButtonClick?.Invoke(this);
//        }

//        #region Editor
//#if UNITY_EDITOR

//        //[ContextMenu("Editor - place to position")]
//        //public void EditorPlace() {
//        //    //gameObject.name = image.sprite.name;
//        //    RectTransform rt = GetComponent<RectTransform>();
//        //    rt.transform.position = image.sprite.textureRect.position;
//        //    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, image.sprite.textureRect.width);
//        //    rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, image.sprite.textureRect.height);

//        //    EditorUtility.SetDirty(this);
//        //    EditorUtility.SetDirty(gameObject);
//        //    Debug.Log($"{image.sprite.textureRect.position}", gameObject);
//        //}

//        //[ContextMenu("Editor - Assign path (for letters)")]
//        //void Editor_AssignPath() {
//        //    string letter = gameObject.name.Substring(gameObject.name.Length - 1, 1).ToLower(); // <Keyboard>/numpad0
//        //    controlPath = $"<Keyboard>/numpad{letter}";
//        //    Editor_SetDirty();
//        //}

//        [ContextMenu("Editor: Set Dirty")]
//        private void Editor_SetDirty() {
//            EditorUtility.SetDirty(this);
//            EditorUtility.SetDirty(gameObject);
//        }

//#endif
//        #endregion Editor
//    }
//}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VisualKeyboard
{
    public class VisualKeyForKeyboard : MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler
    {
        // CLICK (Press)
        public static event Action<VisualKeyForKeyboard> OnKeyboardButtonClick;

        // RELEASE
        public static event Action<VisualKeyForKeyboard> OnKeyboardButtonRelease;

        [Header("Keyboard Key")]
        public KeyCode oldKeyCode;
        public string controlPath;
        public char character;
        public char shiftedCharacter;
        public Vector2 normalizedPosition;

        [Header("Refs")]
        public Image image;
        public Image overlay;

        #region Highlight

        public virtual void Highlight(bool isOn)
        {
            if (isOn)
                HighlightON();
            else
                HighlightOFF();
        }

        [ContextMenu("Highlight OFF")]
        public virtual void HighlightOFF()
        {
            if (overlay != null)
                overlay.gameObject.SetActive(false);
        }

        [ContextMenu("Highlight ON")]
        public virtual void HighlightON()
        {
            if (overlay != null)
                overlay.gameObject.SetActive(true);
        }

        public virtual void HighlightON(Color color)
        {
            if (overlay == null) return;

            color.a = Mathf.Clamp(color.a, 0.19f, 0.21f);
            overlay.color = color;
            overlay.gameObject.SetActive(true);
        }

        public virtual void HighlightAnimation(Color color, float fadeTime)
        {
            if (overlay == null) return;

            StopAllCoroutines();
            StartCoroutine(HighlightAnimating(color, fadeTime));
        }

        protected virtual IEnumerator HighlightAnimating(Color color, float fadeTime)
        {
            float endTime = Time.time + fadeTime;

            color.a = Mathf.Clamp(color.a, 0.19f, 0.21f);
            float startAlpha = color.a;

            overlay.color = color;
            overlay.gameObject.SetActive(true);

            while (Time.time < endTime)
            {
                float time = 1f - ((endTime - Time.time) / fadeTime);

                Color col = overlay.color;
                col.a = Mathf.Lerp(startAlpha, 0f, time);
                overlay.color = col;

                yield return null;
            }

            overlay.gameObject.SetActive(false);
        }

        #endregion

        #region Pointer Events

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"Keyboard button DOWN: {gameObject.name}", gameObject);
            OnKeyboardButtonClick?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"Keyboard button UP: {gameObject.name}", gameObject);
            OnKeyboardButtonRelease?.Invoke(this);
        }

        #endregion

        #region Editor
#if UNITY_EDITOR

        [ContextMenu("Editor: Set Dirty")]
        private void Editor_SetDirty()
        {
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(gameObject);
        }

#endif
        #endregion
    }
}