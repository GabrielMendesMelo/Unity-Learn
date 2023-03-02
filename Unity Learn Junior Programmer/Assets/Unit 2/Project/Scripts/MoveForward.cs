using UnityEngine;

namespace Prototype2
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private float velocidadeMin = 20;
        [SerializeField] private float velocidadeMax = 20;
        [SerializeField] private float maxZCima = 30;
        [SerializeField] private float maxZBaixo = -5;
        private float velocidade;

        private void Awake()
        {
            velocidade = Random.Range(velocidadeMin, velocidadeMax);
        }

        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * velocidade);
            if (transform.position.z > maxZCima || transform.position.z < maxZBaixo)
            {
                Destroy(gameObject);
            }
        }
    }
}
