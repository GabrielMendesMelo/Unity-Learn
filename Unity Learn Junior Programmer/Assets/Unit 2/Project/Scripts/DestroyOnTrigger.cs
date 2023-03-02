using UnityEngine;

namespace Prototype2
{
    public class DestroyOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();

            if (other.tag == "Player")
            {
                playerController.AcertarJogador();
            }
            else
            {
                Destroy(other.gameObject);

                AnimalController animalController = gameObject.GetComponent<AnimalController>();
                animalController.PerderVida();
                if (animalController.Vidas <= 0)
                {
                    playerController.AcrescentarPonto();
                    Destroy(gameObject);
                }
            }
        }
    }
}
