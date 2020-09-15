using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.Cinemachine
{
    public class TouchVisualizer : MonoBehaviour
    {
        public Image left;
        public Image right;

        private void OnEnable()
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);

            left.raycastTarget = false;
            right.raycastTarget = false;

            Application.targetFrameRate = 60;
        }

        void Update()
        {
            int touchCount = Input.touchCount;
            bool isLeft = false;
            bool isRight = false;
            
            for (int i = 0; i < touchCount; i++)
            {
                if (Input.GetTouch(i).position.x < Screen.width * 0.5f)
                {
                    isLeft = true;
                    // left.gameObject.SetActive(true);
                }
                else
                {
                    isRight = true;
                    // right.gameObject.SetActive(true);
                }
            }

            if (isLeft && !left.gameObject.activeSelf)
            {
                left.gameObject.SetActive(true);
            }
            else if (!isLeft && left.gameObject.activeSelf)
            {
                left.gameObject.SetActive(false);
            }
            
            if (isRight && !right.gameObject.activeSelf)
            {
                right.gameObject.SetActive(true);
            }
            else if (!isRight && right.gameObject.activeSelf)
            {
                right.gameObject.SetActive(false);
            }
        }
    }
}
