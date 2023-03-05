using UnityEngine;

namespace Lab5
{
    public class RodaNoProprioEixo : MonoBehaviour
    {
        [SerializeField] private Vector3 rotation;
        [SerializeField] private float rotationSpeed;

        void Update()
        {
            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
        }
    }
}
