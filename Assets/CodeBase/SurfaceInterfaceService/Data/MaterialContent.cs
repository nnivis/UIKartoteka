using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.SurfaceInterfaceService.Data
{
    [CreateAssetMenu(fileName = "MaterialContent", menuName = "Surface/MaterialContent")]
    public class MaterialContent : ScriptableObject
    {
        public IEnumerable<MaterialData> Materials => materials;
        public HashSet<string> DefaultMaterial => _defaultMaterial;
        
        [SerializeField] private List<MaterialData> materials;

        private readonly HashSet<string> _defaultMaterial = new HashSet<string>()
        {
            "CEILING",
            "FLOOR",
            "WALL"
        };

        public void AddMaterial(MaterialData newMaterial)
        {
            if (newMaterial != null && !materials.Any(m => m.MaterialId == newMaterial.MaterialId))
            {
                materials.Add(newMaterial);
            }
            
            PrintMaterialList();
        }

        public void RemoveMaterial(string materialId)
        {
            var materialToRemove = materials.FirstOrDefault(m => m.MaterialId == materialId);
            if (materialToRemove != null)
            {
                materials.Remove(materialToRemove);
            }

            PrintMaterialList();
        }
        
        public void PrintMaterialList()
        {
            foreach (var material in materials)
            {
                Debug.Log($"Material ID: {material.MaterialId}, Name: {material.MaterialName}");
            }
        }

    }
}