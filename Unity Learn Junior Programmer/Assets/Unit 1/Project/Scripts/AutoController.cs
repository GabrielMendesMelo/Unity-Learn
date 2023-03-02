using UnityEngine;

namespace Prototype1
{
    public class AutoController : MonoBehaviour
    {
        [SerializeField] private float velocidadeMin;
        [SerializeField] private float velocidadeMax;
        private float velocidade = 0;

        private void Start()
        {
            velocidade = Random.Range(velocidadeMin, velocidadeMax);
        }

        void Update()
        {
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
        }
    }
}
