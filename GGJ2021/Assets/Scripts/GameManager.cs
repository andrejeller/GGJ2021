using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager: MonoBehaviour {


    public static GameManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private float tempo_de_jogo = 0; // Quando aperta o play

    // -55 -> +55

    public GameObject ui_hora_das_palmas;
    public Slider sld_caminho;
    public Transform osPais;

    private bool palmas_liberadas = false;

    float vaiAte = 0.0f;
    private float intervalo_das_palmas = 0.0f;
    private float bloqueia_as_palmas = 0.0f;

    private float valor_intervalo_das_palmas = 1.2f;
    private float valor_bloqueia_as_palmas = 0.8f;

    private void Start() {
        // IniciarJogo();
    }
    public void IniciarJogo() {
        tempo_de_jogo = Time.time;
        ui_hora_das_palmas.SetActive(false);
        intervalo_das_palmas = valor_intervalo_das_palmas;
        bloqueia_as_palmas = 0.0f;
        comecou = true;
        // Camera.main.transform.position = Vector3.zero;
    }

    bool comecou = false;
    bool chegou_no_fim = false;
    void Update() {

        if (!comecou) return;

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
                    bloqueia_as_palmas = valor_bloqueia_as_palmas;
                    palmas_liberadas = true;
                }
            } else {
                // Palmas liberadas
                if (bloqueia_as_palmas >= 0) {
                    bloqueia_as_palmas -= Time.deltaTime;

                    if (Input.GetKeyDown(KeyCode.Space)) {
                        bloqueia_as_palmas = 0.0f;
                        intervalo_das_palmas = valor_intervalo_das_palmas;
                        palmas_liberadas = false;
                        vaiAte = cam.position.x + 5.0f;
                        cam.DOMoveX(vaiAte, intervalo_das_palmas);
                        Debug.Log("Deu Tempo");
                    }

                } else {
                    bloqueia_as_palmas = 0.0f;
                    intervalo_das_palmas = valor_intervalo_das_palmas;
                    palmas_liberadas = false;
                    if (cam.position.x >= -50.0f) {
                        vaiAte = cam.position.x - 3.0f;
                        cam.DOMoveX(vaiAte, intervalo_das_palmas);
                    }
                    Debug.Log("NÃO Deu Tempo");
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
            intervalo_das_palmas = valor_intervalo_das_palmas;
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

    
}
