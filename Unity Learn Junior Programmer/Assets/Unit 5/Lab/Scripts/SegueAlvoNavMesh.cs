using UnityEngine;
using UnityEngine.AI;

namespace Lab5
{
    public class SegueAlvoNavMesh : MonoBehaviour
    {
        [SerializeField] private Transform alvo;
        [SerializeField] private float velocidade;

        private Transform Alvo { get; set; }
        private float Velocidade { get; set; }

        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            Configurar(alvo, velocidade);
        }

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = Velocidade;
        }

        private void FixedUpdate()
        {
            if (Alvo != null)
            {
                navMeshAgent.destination = Alvo.position;
            }
        }

        public void Configurar(Transform alvo = null, float? velocidade = null)
        {
            Alvo = alvo ?? this.alvo;
            Velocidade = velocidade ?? this.velocidade;
        }
    }
}
