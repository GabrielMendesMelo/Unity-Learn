using UnityEngine;

namespace Lab4
{
    public class MovePorControle : MonoBehaviour
    {
        [SerializeField] private float velocidade;

        private float Velocidade { get; set; }

        private void Awake()
        {
            Configurar(velocidade);
        }

        void Update()
        {
            Mover(Velocidade);
        }


        public void Configurar(float? velocidade = null)
        {
            Velocidade = velocidade ?? this.velocidade;
        }

        private void Mover(float velocidade)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput != 0 || horizontalInput != 0)
            {
                Vector3 direcao = new Vector3(horizontalInput, 0, verticalInput);
                float clampedMagnitude = Mathf.Clamp01(direcao.magnitude);

                transform.Translate(direcao.normalized * clampedMagnitude * Time.deltaTime * velocidade);
            }
        }
    }
}
