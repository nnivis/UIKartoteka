using System.Collections.Generic;
using CodeBase.SurfaceInterfaceService.Data;
using UnityEngine;

namespace CodeBase.SurfaceInterfaceService.View
{
    [CreateAssetMenu(fileName = "MaterialViewFactory", menuName = "Surface/MaterialViewFactory")]
    public class MaterialViewFactory : ScriptableObject
    {
        [SerializeField] private MaterialView materialView;
        [SerializeField] private MaterialButtonView materialButtonView;

        public MaterialView GetMaterialView(MaterialData materialData, Transform parent)
        {
            MaterialView instance = Instantiate(materialView, parent);
            instance.Initialize(materialData.MaterialId, materialData.MaterialName);
            
            return instance;
        }

        public MaterialButtonView GetMaterialButtonView(MaterialData materialData, Transform parent)
        {
            MaterialButtonView instance = Instantiate(materialButtonView, parent);
            instance.Initialize(materialData.MaterialId, materialData.MaterialName);

            return instance;
        }
    }
}