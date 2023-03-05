using UnityEngine;

namespace Lab4
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
