using UnityEngine;
using UnityEngine.AI;

namespace Lab5
{
    public class InimigoControle : MonoBehaviour
    {
        [SerializeField] private LayerMask jogadorLayer;
        [SerializeField] private LayerMask projetilLayerMask;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        void Start()
        {
            SegueAlvoNavMesh segueAlvoNavMesh = GetComponent<SegueAlvoNavMesh>();
            JogadorControle jogador = FindObjectOfType<JogadorControle>();
            segueAlvoNavMesh.Configurar(alvo: jogador.gameObject.transform);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_navMeshAgent.velocity.z < -0.5f)
            {
                _animator.SetFloat("Speed_f", 1);
            }
            else
            {
                _animator.SetFloat("Speed_f", 0);
            }
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
