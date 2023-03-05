using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Prototype5
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> targets;
        [SerializeField] private float spawnRate = 1f;
        [SerializeField] private TextMeshProUGUI scoreTxt;
        [SerializeField] private GameObject gameOverTxt;
        [SerializeField] private GameObject titleScreen;
        [SerializeField] private TextMeshProUGUI livesTxt;
        [SerializeField] private int livesInicial = 3;
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private Slider musicSlider;

        private bool gameOver;
        public bool IsGameOver { get => gameOver; }
        private int score;
        private int lives;

        private AudioSource audioSource;

        private bool isPaused = false;

        private void Start()
        {
            gameOver = true;
            audioSource = GetComponent<AudioSource>();
            musicSlider.onValueChanged.AddListener(delegate { MudarVolume(); });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
            {
                Pausar();
            }
        }

        private void Pausar()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            pauseMenu.SetActive(isPaused);
        }

        public void StartGame(int difficulty)
        {
            gameOver = false;

            StartCoroutine("SpawnTarget");
            score = 0;
            UpdateScore(0);
            lives = livesInicial;
            UpdateLives(0);
            titleScreen.SetActive(false);
            spawnRate /= difficulty;
        }

        private void MudarVolume()
        {
            audioSource.volume = musicSlider.value;
        }

        private IEnumerator SpawnTarget()
        {
            while (!gameOver)
            {
                yield return new WaitForSeconds(spawnRate);
                int index = Random.Range(0, targets.Count);
                Instantiate(targets[index]);
            }
        }

        public void UpdateScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreTxt.text = $"Score: {score}";
        }

        public void UpdateLives(int livesToRemove)
        {
            lives -= livesToRemove;
            livesTxt.text = $"Lives: {lives}";
            if (lives <= 0)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            gameOver = true;
            gameOverTxt.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
