using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager: IGManager {


    public static GameManager local_instance;
    private void Awake() {
        if (instance == null && local_instance == null) {
            instance = this;
            local_instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private float tempo_de_jogo = 0; // Quando aperta o play

    [Header("Paineis")]
    public S00_SplashMenu _sc_Menu;
    public S00_SplashGamePlay _sc_GamePlay;
    public S00_SplashGameOver _sc_GameOver;
    public JanelaInfo lembreDoFone;

    [Space]
    public GameObject ui_hora_das_palmas;
    public Slider sld_caminho;
    public Transform osPais;
    public CriancaPerdida crianca;

    private bool palmas_liberadas = false;
    public bool botoes_liberados = false;

    // -55 -> +55
    private float vaiAte = 0.0f;
    private float intervalo_das_palmas = 0.0f;
    private float bloqueia_as_palmas = 0.0f;
    private bool chegou_no_fim = false;

    // private float valor_intervalo_das_palmas = 0.35f; // 0.35f
    // private float valor_bloqueia_as_palmas = 0.2f; // 0.2f


    // 01 -- PRIMEIRO ESTADO
    private IEnumerator Start() {
        yield return null;

        botoes_liberados = false;

        _sc_Menu.Hide();
        _sc_GamePlay.Hide();
        _sc_GameOver.Hide();

        // camera começa no -53

        SceneLoader.instance.InitializeOn();
        crianca.MudarBone();

        yield return new WaitForSeconds(0.6f);
        SceneLoader.instance.Vanish();

        yield return new WaitForSeconds(0.9f);
        //ChangeGameState(State.Menu);
        _sc_Menu.Show();
        lembreDoFone.AbrirJanela();

        //StateMachine();
    }

    public void FecharLembreteFone() {
        lembreDoFone.FecharJanela();
        ChangeGameState(State.Menu);
    }


    // 02 -- VAMOS COMEÇAR ABRINDO O MENU
    protected override IEnumerator MenuCoroutine() {
        yield return null;
        Debug.Log("Mostrando Menu");
        _sc_Menu.Appear();
        
        yield return new WaitForSeconds(2.0f);
        botoes_liberados = true;
    }

    // 03 -- APERTEI O PLAY -- StartTheGame
    public void IniciarJogo() {
        if (_gameIs == State.Menu && botoes_liberados) {
            botoes_liberados = false;
            ChangeGameState(State.Starting);
        }
        // Camera.main.transform.position = Vector3.zero;
    }

    // 04 -- Fechar Menu, Alguem pode andar, alguma animacao diferente
    protected override IEnumerator StartingCoroutine() {
        yield return null;

        _sc_Menu.Vanish();
        yield return new WaitForSeconds(2.2f);

        Debug.Log("Comecando o Jogo");
        _sc_GamePlay.Show();
        _sc_GamePlay.Appear();

        yield return new WaitForSeconds(1.2f);

        _sc_GamePlay.fundoTutorial.SetActive(true);
        _sc_GamePlay.popUpTutorial.SetActive(true);

        bool aparecer = false;
        for (int i = 0; i < 8; i++) {
            aparecer = !aparecer;
            _sc_GamePlay.setas.SetActive(aparecer);
            yield return new WaitForSeconds(0.3f);
        }

        _sc_GamePlay.popUpTutorial.SetActive(false);
        _sc_GamePlay.fundoTutorial.SetActive(false);

        yield return new WaitForSeconds(0.7f);
        ChangeGameState(State.Playing);
    }

    protected override IEnumerator PlayingCoroutine() {
        yield return null;

        Debug.Log("Hey");
        tempo_de_jogo = Time.time;
        ui_hora_das_palmas.SetActive(false);
        intervalo_das_palmas = ValorIntervaloDasPalmas();
        bloqueia_as_palmas = 0.0f;
        // comecou = true;
    }


    protected override IEnumerator GameOverCoroutine() {
        yield return null;

        botoes_liberados = false;
        Camera.main.transform.DOMoveX(53.0f, 1.0f);
        osPais.DOMove(new Vector3(54.0f, -1.6f, -1.7f), 1.0f);
        crianca.IrParaOsPais();
        // ativar pulos de alegria
        
        _sc_GamePlay.Vanish();
        yield return new WaitForSeconds(0.7f);

        _sc_GameOver.Show();
        _sc_GameOver.Appear_Falas();
        yield return new WaitForSeconds(4.7f);

        _sc_GameOver.Vanish_Falas();
        yield return new WaitForSeconds(0.3f);

        _sc_GameOver.Appear_Final();
        // começa a recarregar a fase
        SceneLoader.ReloadScene();
        yield return new WaitForSeconds(3.0f);

        botoes_liberados = true;
    }

    // comecar jogo de novo
    public override void PlayAgain() {
        if (botoes_liberados) {
            botoes_liberados = false;
            SceneLoader.Show();
        }
    }


    void Update() {
        // ate -40 mais devagar
        if (_gameIs != State.Playing) return;
        // if (!comecou) return;

        Transform cam = Camera.main.transform;

        // Chegou no 50, tudo para , hora do do tween


        if (cam.position.x < 50.0f) {
            ui_hora_das_palmas.SetActive(palmas_liberadas);
            sld_caminho.value = cam.position.x;


            if (!palmas_liberadas) {
                if (intervalo_das_palmas >= 0) {
                    intervalo_das_palmas -= Time.deltaTime;
                } else {
                    intervalo_das_palmas = 0.0f;
                    bloqueia_as_palmas = ValorBloqueiaAsPalmas();
                    palmas_liberadas = true;
                }
            } else {
                // Palmas liberadas
                if (bloqueia_as_palmas >= 0) {
                    bloqueia_as_palmas -= Time.deltaTime;

                    if (Input.GetKeyDown(KeyCode.Space)) {
                        MovimentaComPalmas();
                    }

                } else {
                    bloqueia_as_palmas = 0.0f;
                    intervalo_das_palmas = ValorIntervaloDasPalmas();
                    palmas_liberadas = false;
                    if (cam.position.x >= -50.0f) {
                        vaiAte = cam.position.x - 1.5f;
                        cam.DOMoveX(vaiAte, intervalo_das_palmas);
                        _sc_GamePlay.FlashErrou();
                    }
                    Debug.Log("NÃO Deu Tempo");
                    _sc_GamePlay.FlashErrou();
                }

            }
        } else {
            // pequena animacao do final
            if (!chegou_no_fim) {
                chegou_no_fim = true;
                ChangeGameState(State.GameOver);
            }

        }

    }


    public void ComandoAvancarComMicrofone() {
        MovimentaComPalmas();
    }
    public void BotaoDeTeste() {
        MovimentaComPalmas();
    }
    private void MovimentaComPalmas() {

        if (palmas_liberadas && Camera.main.transform.position.x < 50.0f && bloqueia_as_palmas >= 0 ) {
            bloqueia_as_palmas = 0.0f;
            intervalo_das_palmas = ValorIntervaloDasPalmas();
            palmas_liberadas = false;
            vaiAte = Camera.main.transform.position.x + 2.0f;
            Camera.main.transform.DOMoveX(vaiAte, intervalo_das_palmas);
            Debug.Log("Deu Tempo");
            _sc_GamePlay.FlashAcertou();
        }
        
    }

    

    private float ValorIntervaloDasPalmas() {
        // -50 -> + 50
        if (Camera.main.transform.position.x < -40.0f) {
            return 1.4f;
        } else if (Camera.main.transform.position.x >= -40.0f && Camera.main.transform.position.x < -25.0f) {
            return 0.7f;
        } else {
            return 0.35f;
        }
    }

    private float ValorBloqueiaAsPalmas() {
        // -50 -> + 50
        if (Camera.main.transform.position.x < -40.0f) {
            return 0.6f;
        } else if (Camera.main.transform.position.x >= -45.0f && Camera.main.transform.position.x < 25.0f) {
            return 0.4f;
        } else {
            return 0.2f;
        }
    }


   

}
