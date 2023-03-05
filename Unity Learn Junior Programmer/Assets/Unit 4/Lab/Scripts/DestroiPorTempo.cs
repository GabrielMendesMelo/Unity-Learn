using System.Collections;
using UnityEngine;

namespace Lab4
{
    public class DestroiPorTempo : MonoBehaviour
    {
        [SerializeField] private float tempo;

        private float Tempo { get; set; }

        private void Awake()
        {
            Configurar(tempo);
        }

        void Start()
        {
            StartCoroutine("DestruirPorTempo");
        }

        public void Configurar(float? tempo = null)
        {
            Tempo = tempo ?? this.tempo;
        }

        private IEnumerator DestruirPorTempo()
        {
            yield return new WaitForSeconds(Tempo);
            Destroy(gameObject);
        }
    }
}
