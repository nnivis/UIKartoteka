using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.SurfaceInterfaceService.Data;
using CodeBase.SurfaceInterfaceService.View;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.SurfaceInterfaceService
{
    public class SurfacePanel : MonoBehaviour
    {
        public event Action<bool> OnToggleChange;
        public event Action<bool, string> OnApplyButtonClick;
        public event Action<string> OnRemoveMaterial;

        [SerializeField] private MaterialViewFactory materialViewFactory;
        [SerializeField] private SwitchToggle switchToggle;
        [SerializeField] private Button applyButton;
        [SerializeField] private RectTransform parent;

        private readonly List<MaterialView> _materialViews = new List<MaterialView>();
        private MaterialView _selectMaterialView;


        private string _lastSelectedMaterialId;
        private bool _wasMaterialButtonSelected;

        public void Initialize()
        {
            applyButton.onClick.AddListener(OnApplyButtonClicked);
            switchToggle.OnToggleChange += UpdateStatePanel;
        }


        public void StartWork(IEnumerable<MaterialData> materialsData, HashSet<string> defaultMaterials,
            bool isSurfaceNew)
        {
            Clear();

            foreach (var materialData in materialsData)
            {
                MaterialView materialView = defaultMaterials.Contains(materialData.MaterialId)
                    ? materialViewFactory.GetMaterialView(materialData, parent)
                    : materialViewFactory.GetMaterialButtonView(materialData, parent);

                if (materialView is MaterialButtonView materialButtonView)
                {
                    materialButtonView.OnRemoveMaterial += RemoveMaterial;

                    if (isSurfaceNew)
                        materialButtonView.DeActiveSelectedState();
                    else
                        materialButtonView.ActiveSelectedForButtonState();
                }
                else
                {
                    materialView.ActiveDefaultState();
                }

                materialView.OnSelectButtonClick += UpdateSelectMaterial;
                _materialViews.Add(materialView);
            }

            MaterialView candidate = null;

            if (!_wasMaterialButtonSelected && !string.IsNullOrEmpty(_lastSelectedMaterialId))
                candidate = _materialViews.FirstOrDefault(m => m.IdMaterial == _lastSelectedMaterialId);


            if (candidate == null)
                candidate = _materialViews.FirstOrDefault();


            _wasMaterialButtonSelected = false;

            if (candidate != null)
                UpdateSelectMaterial(candidate);
        }


        private void Clear()
        {
            foreach (MaterialView materialView in _materialViews)
            {
                Destroy(materialView.gameObject);
            }

            _materialViews.Clear();
        }


        private void UpdateStatePanel(bool isToggled)
        {
            OnToggleChange?.Invoke(isToggled);

            if (_selectMaterialView is MaterialButtonView materialButtonView)
            {
                materialButtonView.DeActiveSelectedState();
                _selectMaterialView = null;
                _lastSelectedMaterialId = null;

                var candidate = _materialViews.FirstOrDefault();
                if (candidate != null)
                    UpdateSelectMaterial(candidate);
            }
            else
            {
                _lastSelectedMaterialId = _selectMaterialView?.IdMaterial;
            }
        }

        private void OnApplyButtonClicked()
        {
            string materialId = _selectMaterialView != null && _selectMaterialView.IdMaterial != null
                ? _selectMaterialView.IdMaterial
                : "UNKNOWN";

            OnApplyButtonClick?.Invoke(switchToggle.IsToggled, materialId);
        }


        private void RemoveMaterial(MaterialView materialView)
        {
            ValidateSelection(materialView);
            _materialViews.Remove(materialView);
            Destroy(materialView.gameObject);

            OnRemoveMaterial?.Invoke(materialView.IdMaterial);
        }


        private void ValidateSelection(MaterialView materialView)
        {
            if (_selectMaterialView != null && _selectMaterialView.IdMaterial == materialView.IdMaterial)
                _selectMaterialView = null;
        }


        private void UpdateSelectMaterial(MaterialView newSelected)
        {
            if (_selectMaterialView != null)
                _selectMaterialView.ActiveDefaultState();

            _selectMaterialView = newSelected;
            _selectMaterialView.ActiveSelectedState();
            _lastSelectedMaterialId = _selectMaterialView.IdMaterial;
        }

        private void OnDestroy()
        {
            applyButton.onClick.RemoveListener(OnApplyButtonClicked);
        }
    }
}