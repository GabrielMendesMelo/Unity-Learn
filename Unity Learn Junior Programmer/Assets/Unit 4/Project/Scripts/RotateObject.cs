using UnityEngine;

namespace Prototype4
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private Vector3 rotation;
        [SerializeField] private float rotationSpeed;

        void Update()
        {
            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        }
    }
}
