using UnityEngine;

namespace Lab4
{
    public class SegueAlvo : MonoBehaviour
    {
        [SerializeField] private Transform alvo;
        [SerializeField] private float velocidade;
        [SerializeField] private Vector3 alvoDistanciaASerMantida;
        [SerializeField] private Vector3 posicaoASerMantida;

        private Transform Alvo { get; set; }
        private float Velocidade { get; set; }
        private Vector3 AlvoDistanciaASerMantida { get; set; }
        private Vector3 PosicaoASerMantida { get; set; }


        private void Awake()
        {
            Configurar(alvo, velocidade, alvoDistanciaASerMantida, posicaoASerMantida);
        }

        private void LateUpdate()
        {
            SeguirAlvo();
        }

        public void Configurar(Transform alvo = null, float? velocidade = null, Vector3? alvoDistanciaASerMantida = null, Vector3? posicaoASerMantida = null, float? atraso = null)
        {
            Alvo = alvo ?? this.alvo;
            Velocidade = velocidade ?? this.velocidade;
            AlvoDistanciaASerMantida = alvoDistanciaASerMantida ?? this.alvoDistanciaASerMantida;
            PosicaoASerMantida = posicaoASerMantida ?? this.posicaoASerMantida;
        }

        private void SeguirAlvo()
        {
            if (Alvo != null)
            {
                Vector3 alvoPosicao = Alvo.position + PosicaoASerMantida;
                float distancia = Vector3.Distance(transform.position, alvoPosicao);
                if (!EstaProximoOSuficiente(distancia, AlvoDistanciaASerMantida))
                {
                    Mover(alvoPosicao, Velocidade, Alvo);
                }
            }
        }

        private void Mover(Vector3 alvoPosicao, float velocidade, Transform alvo)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvoPosicao, velocidade * Time.deltaTime);
            transform.LookAt(alvo);
        }

        private bool EstaProximoOSuficiente(float distancia, Vector3 alvoDistanciaASerMantida)
        {
            return distancia < alvoDistanciaASerMantida.magnitude;
        }
    }
}
