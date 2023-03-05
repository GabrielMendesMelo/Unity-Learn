using UnityEngine;

namespace Lab4
{
    public class MoveEmUmaDirecao : MonoBehaviour
    {
        [SerializeField] private Transform alvo;
        [SerializeField] private Vector3 direcao;
        [SerializeField] private float velocidade;

        private Transform Alvo { get; set; }
        private Vector3 Direcao { get; set; }
        private float Velocidade { get; set; }

        private void Awake()
        {
            Configurar(alvo, direcao, velocidade);
        }

        private void Update()
        {
            Mover(Direcao, Velocidade);
        }

        public void Configurar(Transform alvo = null, Vector3? direcao = null, float? velocidade = null)
        {
            if (alvo != null && direcao != null && direcao != Vector3.zero)
            {
                Debug.LogWarning("Alvo tem prioridade sobre Direcao. O valor de Direcao não será usado.");
            }
            Alvo = alvo ?? this.alvo;
            if (Alvo != null)
            {
                Direcao = alvo.position - transform.position;
            }
            else
            {
                Direcao = direcao ?? this.direcao;
            }
            Velocidade = velocidade ?? this.velocidade;

            ApontarParaDirecao();
        }

        private void ApontarParaDirecao()
        {
            Direcao.Normalize();
            float angulo = Mathf.Atan2(Direcao.x, Direcao.z) * Mathf.Rad2Deg;

            Quaternion rotacaoY = Quaternion.Euler(0f, angulo, 0f);
            Quaternion rotacaoX = Quaternion.Euler(transform.eulerAngles.x, 0f, 0f);

            transform.rotation = rotacaoY * rotacaoX;
        }

        private void Mover(Vector3 direcao, float velocidade)
        {
            Vector3 alvo = transform.position + direcao * velocidade;
            transform.position = Vector3.MoveTowards(transform.position, alvo, Velocidade * Time.deltaTime);
        }
    }
}
