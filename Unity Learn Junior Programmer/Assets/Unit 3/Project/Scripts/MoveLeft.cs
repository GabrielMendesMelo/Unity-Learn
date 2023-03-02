using UnityEngine;

namespace Prototype3
{
    public class MoveLeft : MonoBehaviour
    {
        [SerializeField] private float velocidadeMult = 2;
        [SerializeField] private float limiteXParaDestruir = -5;
        private PlayerController playerController;

        private void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            if (!playerController.EstaGameOver)
            {
                Mover();
                DestruirForaDoLimite();
            }
        }

        private void Mover()
        {
            transform.Translate(Vector3.left * (FaseInfo.Velocidade * velocidadeMult) * Time.deltaTime);
        }

        private void DestruirForaDoLimite()
        {
            if (transform.position.x < limiteXParaDestruir)
            {
                Destroy(gameObject);
            }
        }
    }
}
