using UnityEngine;
using UnityEngine.UI;

namespace Prototype5
{
    public class DifficultButton : MonoBehaviour
    {
        [SerializeField] private int difficulty;
        private Button button;
        private GameManager gameManager;

        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(SetDifficulty);
            gameManager = FindObjectOfType<GameManager>();
        }

        private void SetDifficulty()
        {
            gameManager.StartGame(difficulty);
        }
    }
}
