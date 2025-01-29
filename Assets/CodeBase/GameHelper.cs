using CodeBase.SurfaceInterfaceService;
using CodeBase.SurfaceInterfaceService.Data;
using UnityEngine;

namespace CodeBase
{
    public class GameHelper : MonoBehaviour
    {
        [SerializeField] private MaterialContent materialContent;
        [SerializeField] private SurfaceHandler surfaceHandler;

        private void Awake() => surfaceHandler.Initialize(materialContent, materialContent.DefaultMaterial);
        private void Start() => StartWork();
        private void StartWork() => surfaceHandler.StartWork(false);
    }
}