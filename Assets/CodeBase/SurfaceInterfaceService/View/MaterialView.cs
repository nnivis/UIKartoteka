using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.SurfaceInterfaceService.View
{
    public class MaterialView : MonoBehaviour
    {
        public string IdMaterial => IDMaterial;
        public event Action<MaterialView> OnSelectButtonClick;
        
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected Image _stateBackGround;
        [SerializeField] protected Button selectButton;
        protected string IDMaterial;
        protected string NameMaterial;

        protected virtual void OnEnable() => selectButton.onClick.AddListener(OnSelectButtonClicked);
        public  void Initialize(string idMaterial, string nameMaterial)
        {
            IDMaterial = idMaterial;
            NameMaterial = nameMaterial;

            UpdateText();
        }

        private void UpdateText() => nameText.text = NameMaterial;
        public void ActiveDefaultState() => SetImageColor(new Color32(51, 51, 51, 100));
        public void ActiveSelectedState() => SetImageColor(new Color32(37, 68, 166, 255));
        protected void SetImageColor(Color32 color)
        {
            if (_stateBackGround == null) return;
            _stateBackGround.color = color;
        }
        protected void OnSelectButtonClicked() => OnSelectButtonClick?.Invoke(this);
        protected virtual void OnDestroy() => selectButton.onClick.RemoveListener(OnSelectButtonClicked);
    }
}
