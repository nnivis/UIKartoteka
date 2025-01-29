using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.SurfaceInterfaceService.View
{
    public class SwitchToggle : MonoBehaviour 
    {
        public bool IsToggled => _isToggled;
        public event Action<bool> OnToggleChange;
        
        [SerializeField] RectTransform uiHandleRectTransform;
        [SerializeField] Color backgroundActiveColor;
        [SerializeField] Color handleActiveColor;

        private Image _backgroundImage;
        private Image _handleImage;

        private Color _backgroundDefaultColor;
        private Color _handleDefaultColor;

        private Toggle _toggle;
        private Vector2 _handlePosition;
        private bool _isToggled;


        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            _handlePosition = uiHandleRectTransform.anchoredPosition;

            _backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
            _handleImage = uiHandleRectTransform.GetComponent<Image>();

            _backgroundDefaultColor = _backgroundImage.color;
            _handleDefaultColor = _handleImage.color;

            _toggle.onValueChanged.AddListener(OnSwitch);

            if (_toggle.isOn)
                OnSwitch(true);
        }

        private void OnSwitch(bool on)
        {
            _isToggled = on; 

            uiHandleRectTransform.DOAnchorPos(on ? _handlePosition * -1 : _handlePosition, .4f).SetEase(Ease.InOutBack);
            _backgroundImage.DOColor(on ? backgroundActiveColor : _backgroundDefaultColor, .6f);
            _handleImage.DOColor(on ? handleActiveColor : _handleDefaultColor, .4f);
            
            OnToggleChange?.Invoke(_isToggled);
        }

        void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}