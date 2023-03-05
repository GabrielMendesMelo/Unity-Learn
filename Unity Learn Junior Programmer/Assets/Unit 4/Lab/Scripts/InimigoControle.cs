using UnityEngine;

namespace Lab4
{
    public class InimigoControle : MonoBehaviour
    {
        [SerializeField] private LayerMask jogadorLayer;
        [SerializeField] private LayerMask projetilLayerMask;

        void Start()
        {
            SegueAlvoNavMesh segueAlvoNavMesh = GetComponent<SegueAlvoNavMesh>();
            JogadorControle jogador = FindObjectOfType<JogadorControle>();
            segueAlvoNavMesh.Configurar(alvo: jogador.gameObject.transform);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((jogadorLayer.value & (1 << collision.gameObject.layer)) > 0)
            {
                Destroy(collision.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((projetilLayerMask.value & (1 << other.gameObject.layer)) > 0)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
