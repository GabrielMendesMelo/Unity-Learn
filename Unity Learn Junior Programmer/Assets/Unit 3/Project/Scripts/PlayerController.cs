using UnityEngine;

namespace Prototype3
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float puloForca = 25;
        [SerializeField] private float gravidade = 6;
        private Rigidbody rb;
        private Animator animator;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private ParticleSystem dirtParticle;
        [SerializeField] private AudioClip[] jumpSounds;
        [SerializeField] private AudioClip[] crashSounds;
        private AudioSource audioSource;
        private int nPulos = 0;

        private float velocidadeFaseBackup;
        [SerializeField] private float dashMult = 3f;

        [SerializeField] private TMPro.TextMeshProUGUI txtPontos;
        private float pontos;

        private bool btnPuloPressionado { get => Input.GetKeyDown(KeyCode.Space); }
        private bool btnDashPressionado { get => Input.GetKey(KeyCode.LeftShift); }
        private bool btnDashSoltou { get => Input.GetKeyUp(KeyCode.LeftShift); }
        public bool EstaGameOver { get; private set; }

        [SerializeField] private GameObject spawnMager;
        [SerializeField] private float velocidadeAnimacaoInicial = 5;
        [SerializeField] private float posXInicial = 2.5f;

        private void Awake()
        {
            FaseInfo.Comecou = false;
        }

        private void Start()
        {
            EstaGameOver = false;
            rb = GetComponent<Rigidbody>();
            Physics.gravity *= gravidade;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            velocidadeFaseBackup = FaseInfo.Velocidade;

            pontos = 0;
        }

        void Update()
        {
            if (FaseInfo.Comecou)
            {
                if (!EstaGameOver)
                {
                    pontos += Time.deltaTime * FaseInfo.Velocidade;
                    txtPontos.text = $"Pontos: {System.Math.Round(pontos, 2)}";

                    if (btnPuloPressionado && !EstaGameOver && nPulos < 2)
                    {
                        Pular();
                    }
                    if (btnDashPressionado)
                    {
                        FaseInfo.Velocidade = velocidadeFaseBackup * dashMult;
                    }
                    if (btnDashSoltou)
                    {
                        FaseInfo.Velocidade = velocidadeFaseBackup;
                    }
                }
            }
            else
            {
                MoverAteOInicio();
            }
        }

        private void MoverAteOInicio()
        {
            if (transform.position.x < posXInicial)
            {
                transform.position += Vector3.right * velocidadeAnimacaoInicial * Time.deltaTime;
            }
            else
            {
                Instantiate(spawnMager, Vector3.zero, Quaternion.identity);
                FaseInfo.Comecou = true;
            }
        }

        private void Pular()
        {
            nPulos++;
            audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
            dirtParticle.Stop();
            if (nPulos == 1)
            {
                rb.AddForce(Vector3.up * puloForca, ForceMode.Impulse);
                animator.SetTrigger("Jump_trig");
            }
            else if (nPulos == 2)
            {
                rb.AddForce(Vector3.up * puloForca / 2, ForceMode.Impulse);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Chao"))
            {
                nPulos = 0;
                dirtParticle.Play();
            }
            else if (collision.gameObject.CompareTag("Inimigo"))
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            audioSource.PlayOneShot(crashSounds[Random.Range(0, crashSounds.Length)]);
            dirtParticle.Stop();
            particle.Play();
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            EstaGameOver = true;
        }
    }
}
