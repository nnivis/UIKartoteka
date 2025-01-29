using UnityEngine;

namespace CodeBase.SurfaceInterfaceService.Data
{
    [CreateAssetMenu(fileName = "Material", menuName = "Surface/Material")]
    public class MaterialData : ScriptableObject
    {
        [SerializeField] private string materialId;
        [SerializeField] private string materialName;

        public string MaterialId => materialId;
        public string MaterialName => materialName;
    }
}
