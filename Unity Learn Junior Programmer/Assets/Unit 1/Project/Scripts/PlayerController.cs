using System;
using UnityEngine;

namespace Prototype1
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float velocidadeMax;
        [SerializeField] private float aceleracaoMult;
        [SerializeField] private float desaceleracaoMult;
        [SerializeField] private float velocidadeDeRotacaoRef;

        [SerializeField] private GameObject cameraPrimeiraPessoa;
        [SerializeField] private GameObject cameraTerceiraPessoa;

        [SerializeField] private int playerId;

        private float velocidade = 0;
        private float velocidadeRot;

        private void Awake()
        {
            if (playerId < 1 || playerId > 2)
            {
                throw new ArgumentException("playerId só pode ser 1 ou 2");
            }
        }

        void Update()
        {
            if ((Input.GetKeyDown(KeyCode.V) && playerId == 1) || (Input.GetKeyDown(KeyCode.Delete) && playerId == 2))
            {
                if (cameraPrimeiraPessoa.activeInHierarchy)
                {
                    cameraTerceiraPessoa.SetActive(true);
                    cameraPrimeiraPessoa.SetActive(false);
                }
                else
                {
                    cameraTerceiraPessoa.SetActive(false);
                    cameraPrimeiraPessoa.SetActive(true);
                }
            }

            float verticalInput = Input.GetAxis($"Vertical{playerId}");
            if (verticalInput > 0)
            {
                if (velocidade < velocidadeMax)
                {
                    velocidade += Time.deltaTime * aceleracaoMult * verticalInput;
                }
                else
                {
                    velocidade = velocidadeMax;
                }
            }
            else if (verticalInput < 0)
            {
                if (velocidade > -velocidadeMax)
                {
                    velocidade += Time.deltaTime * aceleracaoMult * verticalInput;
                }
                else
                {
                    velocidade = -velocidadeMax;
                }
            }
            else
            {
                if (velocidade > 0)
                {
                    velocidade -= Time.deltaTime * desaceleracaoMult;
                }
                else if (velocidade < 0)
                {
                    velocidade += Time.deltaTime * desaceleracaoMult;
                }
                else
                {
                    velocidade = 0;
                }
            }
            velocidadeRot = velocidadeDeRotacaoRef * velocidade;

            float horizontalInput = Input.GetAxis($"Horizontal{playerId}");

            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
            transform.Rotate(Vector3.up, velocidadeRot * Time.deltaTime * horizontalInput);
        }
    }
}
