using UnityEngine;

namespace Prototype4
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float velocidadeShiftMult = 2;
        private float VelocidadeComShift { get { return Input.GetKey(KeyCode.LeftShift) ? velocidadeShiftMult : 1; } }

        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * VelocidadeComShift * Time.deltaTime);
        }
    }
}
