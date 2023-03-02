using UnityEngine;

namespace Lab3
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float velocidadeMovimento;
        [SerializeField] private float velocidadeRotacao;
        private Vector3 posicaoInicial;

        private void Start()
        {
            posicaoInicial = transform.position;
        }

        void Update()
        {
            Mover();
            Girar();
        }

        private void Mover()
        {
            float verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * verticalInput * velocidadeMovimento * Time.deltaTime);
        }

        private void Girar()
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            transform.Rotate(Vector3.up * horizontalInput * velocidadeRotacao * Time.deltaTime);
        }
        private void OnCollisionStay(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Chao"))
            {
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GanharJogo();
        }

        private void GanharJogo()
        {
            transform.position = posicaoInicial;
            transform.rotation = Quaternion.identity;
        }
    }
}
