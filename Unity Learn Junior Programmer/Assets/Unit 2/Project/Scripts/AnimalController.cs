using UnityEngine;
using UnityEngine.UI;

namespace Prototype2
{
    public class AnimalController : MonoBehaviour
    {
        [SerializeField] private int vidas = 3;
        [SerializeField] private Slider vidaSlider;

        private int _vidas;
        public int Vidas
        {
            get
            {
                return _vidas;
            }
            set
            {
                _vidas = value;
            }
        }

        private void Start()
        {
            _vidas = vidas;
            vidaSlider.maxValue = vidas;
            vidaSlider.value = vidas;
        }

        public void PerderVida()
        {
            Vidas--;
            vidaSlider.value = Vidas;
        }
    }
}
