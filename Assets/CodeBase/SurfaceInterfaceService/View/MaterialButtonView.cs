using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.SurfaceInterfaceService.View
{
    public class MaterialButtonView : MaterialView
    {
        public event Action<MaterialView> OnRemoveMaterial;

        [SerializeField] private Button removeButton;

        protected override void OnEnable()
        {
            base.OnEnable();
            removeButton.onClick.AddListener(OnRemoveButtonClicked);
        }

        private void OnRemoveButtonClicked() => OnRemoveMaterial?.Invoke(this);

        public void DeActiveSelectedState()
        {
            ActiveDefaultState();
            SetTextTransparency(20);
            selectButton.gameObject.SetActive(false);
            removeButton.gameObject.SetActive(false);
            
        }

        public void ActiveSelectedForButtonState()
        {
            SetTextTransparency(255);
            selectButton.gameObject.SetActive(true);
            removeButton.gameObject.SetActive(true);
        }
        private void SetTextTransparency(byte alpha)
        {
            Color32 color = nameText.color;
            color.a = alpha; 
            nameText.color = color;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            removeButton.onClick.RemoveListener(OnRemoveButtonClicked);
        }
    }
}