using System.Collections.Generic;
using UnityEngine;

namespace Prototype2
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float velocidade;
        [SerializeField] private float maxX = 16;
        [SerializeField] private float maxZ = 5;
        [SerializeField] private float minZ = -2;
        [SerializeField] private GameObject[] comidas;
        [SerializeField] private List<GameObject> vidas;
        [SerializeField] private TMPro.TextMeshProUGUI pontosTxt;

        private int pontos = 0;

        void Update()
        {
            Mover();
            ManterJogadorDentroDeLimites();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AtirarComida();
            }
        }

        private void Mover()
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            if (verticalInput != 0 || horizontalInput != 0)
            {
                Vector3 direcao = new Vector3(horizontalInput, transform.position.y, verticalInput);
                float clampedMagnitude = Mathf.Clamp01(direcao.magnitude);

                transform.Translate(direcao.normalized * clampedMagnitude * Time.deltaTime * velocidade);
            }
        }

        private void ManterJogadorDentroDeLimites()
        {
            Vector3 p = transform.position;

            if (p.z > maxZ)
            {
                transform.position = new Vector3(p.x, p.y, maxZ);
            }
            else if (p.z < minZ)
            {
                transform.position = new Vector3(p.x, p.y, minZ);
            }

            if (p.x > maxX)
            {
                transform.position = new Vector3(maxX, p.y, p.z);
            }
            else if (p.x < -maxX)
            {
                transform.position = new Vector3(-maxX, p.y, p.z);
            }
        }

        private void AtirarComida()
        {
            GameObject comida = comidas[Random.Range(0, comidas.Length)];
            Instantiate(comida, transform.position, comida.transform.rotation);
        }

        public void AcrescentarPonto()
        {
            pontos++;
            pontosTxt.text = $"Pontos: {pontos}";
        }

        public void AcertarJogador()
        {
            if (vidas.Count > 0)
            {
                GameObject vida = vidas[vidas.Count - 1];
                vidas.Remove(vida);
                Destroy(vida);
            }
            else
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            Destroy(gameObject);
        }
    }
}
