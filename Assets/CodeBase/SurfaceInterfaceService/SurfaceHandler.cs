using System.Collections.Generic;
using CodeBase.SurfaceInterfaceService.Data;
using UnityEngine;


namespace CodeBase.SurfaceInterfaceService
{
    public class SurfaceHandler : MonoBehaviour
    {
        [SerializeField] private SurfacePanel surfacePanel;
        private MaterialContent _materialContent;
        private HashSet<string> _defaultMaterial;

        public void Initialize(MaterialContent materialContent, HashSet<string> defaultMaterial)
        {
            _materialContent = materialContent;
            _defaultMaterial = defaultMaterial;
            
            surfacePanel.Initialize();
            surfacePanel.OnApplyButtonClick += ApplyСhanges;
            surfacePanel.OnRemoveMaterial += RemoveMaterial;
            surfacePanel.OnToggleChange += UpdateStatePanel;
        }

        public void StartWork(bool isSurfaceNew) => 
            surfacePanel.StartWork(_materialContent.Materials, _defaultMaterial, isSurfaceNew);

        private void UpdateStatePanel(bool _isToggled) => StartWork(_isToggled);

        private void RemoveMaterial(string materialId) => 
            _materialContent.RemoveMaterial(materialId);

        private void ApplyСhanges(bool isSurfaceNew, string idMaterial)
        {
            Debug.Log($"CallBack : {isSurfaceNew}, {idMaterial}");
        }
    }
}
