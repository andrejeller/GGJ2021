using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager: IGManager
{


    public static GameManager local_instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private float tempo_de_jogo = 0; // Quando aperta o play

    [Header("Paineis")]
    public S00_SplashMenu _sc_Menu;
    public S00_SplashGamePlay _sc_GamePlay;
    public GameObject _sc_GameOver;


    [Space]
    public GameObject ui_hora_das_palmas;
    public Slider sld_caminho;
    public Transform osPais;
    public CriancaPerdida crianca;

    private bool palmas_liberadas = false;

    // -55 -> +55
    float vaiAte = 0.0f;
    private float intervalo_das_palmas = 0.0f;
    private float bloqueia_as_palmas = 0.0f;

    // private float valor_intervalo_das_palmas = 0.35f; // 0.35f
    // private float valor_bloqueia_as_palmas = 0.2f; // 0.2f

    // 01 -- PRIMEIRO ESTADO
    private IEnumerator Start() {
        _sc_Menu.Hide();
        _sc_GamePlay.Hide();

        SceneLoader.instance.InitializeOn();
        // crianca.MudarBone();

        yield return new WaitForSeconds(0.6f);
        SceneLoader.instance.Vanish();

        yield return new WaitForSeconds(0.9f);
        ChangeGameState(State.Menu);

        //StateMachine();
    }

    // 02 -- VAMOS COMEÇAR ABRINDO O MENU
    protected override IEnumerator MenuCoroutine() {
        yield return null;
        Debug.Log("Mostrando Menu");
        _sc_Menu.Show();
        _sc_Menu.Appear();
    }

    // 03 -- APERTEI O PLAY -- StartTheGame
    public void IniciarJogo() {
        if (_gameIs == State.Menu) {
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


    // bool comecou = false;
    bool chegou_no_fim = false;
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
                        bloqueia_as_palmas = 0.0f;
                        intervalo_das_palmas = ValorIntervaloDasPalmas();
                        palmas_liberadas = false;
                        vaiAte = cam.position.x + 2.0f;
                        cam.DOMoveX(vaiAte, intervalo_das_palmas);
                        Debug.Log("Deu Tempo");
                        _sc_GamePlay.FlashAcertou();
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
                AnimacaoFim();
            }

        }






        //float final = Mathf.Lerp(cam.transform.position.x, vaiAte, 0.5f);
        //cam.position = new Vector3(final, cam.position.y, cam.position.z);



    }


    public void BotaoDeTeste() {
        if (Camera.main.transform.position.x < 50.0f && palmas_liberadas && bloqueia_as_palmas >= 0) {
            bloqueia_as_palmas = 0.0f;
            intervalo_das_palmas = ValorIntervaloDasPalmas();
            palmas_liberadas = false;
            vaiAte = Camera.main.transform.position.x + 5.0f;
            Camera.main.transform.DOMoveX(vaiAte, intervalo_das_palmas);
            Debug.Log("Deu Tempo");
        }
    }

    private void AnimacaoFim() {

        Camera.main.transform.DOMoveX(53.0f, 1.0f);
        osPais.DOMove(new Vector3(54.0f, -1.6f, -1.7f), 1.0f);
        // ativar pulos de alegria
        //precisa entregar o filho tambem
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








    // -- AS OUTRAS ESTAO AQUI
    protected override void PlayAgain() {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator GameOverCoroutine() {
        throw new System.NotImplementedException();
    }



    // -- PARA DEPOIS

    protected override IEnumerator WelcomeCoroutine() {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator PromoCoroutine() {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator InfoCoroutine() {
        throw new System.NotImplementedException();
    }

    /*protected override void HandleScreen(State WhatState) {
        // SERVE APENAS ATIVAR E DESATIVAR OS 'GAMEOBJECTS' 
        // NÃO DEVE CHAMAR NENHUMA OUTRA FUNÇÃO
        switch (WhatState) {
            case State.Menu:
                _sc_Menu.Show();
                _sc_GamePlay.gameObject.SetActive(false);
                _sc_GameOver.gameObject.SetActive(false);
                break;
            case State.Starting:
                break;
            case State.Playing:
                _sc_GamePlay.gameObject.SetActive(true);

                _sc_Menu.Hide();
                _sc_GameOver.gameObject.SetActive(false); // Game Over é o Play Again
                break;
            
            case State.GameOver:
                _sc_GameOver.gameObject.SetActive(true);

                _sc_Menu.Hide();
                _sc_GamePlay.gameObject.SetActive(false);
                break;
            case State.Info:
                break;
            case State.HowToPlay:
                //_sc_Pause.gameObject.SetActive(false);
                //_sc_IndoParaOMenu.gameObject.SetActive(false);
                break;
            case State.promo:
                break;
            case State.wait:
                break;
            case State.none:
                break;
            default:
                Debug.LogWarning("Algo Errado :/");
                break;
        }
    }*/


}
