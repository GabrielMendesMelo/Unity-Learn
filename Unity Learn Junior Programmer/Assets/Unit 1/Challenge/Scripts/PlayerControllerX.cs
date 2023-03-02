using UnityEngine;

namespace Challenge1
{
    public class PlayerControllerX : MonoBehaviour
    {
        public float speed;
        public float rotationSpeed;
        public float verticalInput;

        public GameObject propeller;
        public float propellerSpeed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // get the user's vertical input
            verticalInput = Input.GetAxis("Vertical");

            // move the plane forward at a constant rate
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // tilt the plane up/down based on up/down arrow keys
            transform.Rotate(Vector3.right * rotationSpeed * verticalInput * Time.deltaTime);
        }

        private void Update()
        {
            propeller.transform.Rotate(Vector3.forward, Time.deltaTime * propellerSpeed);
        }
    }
}
