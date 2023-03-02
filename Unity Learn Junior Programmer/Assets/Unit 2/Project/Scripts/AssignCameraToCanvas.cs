using UnityEngine;

namespace Prototype2
{
    public class AssignCameraToCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        void Start()
        {
            canvas.worldCamera = FindObjectOfType<Camera>();
        }
    }
}
