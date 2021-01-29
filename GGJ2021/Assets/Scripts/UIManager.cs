using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject painel_Menu;
    public GameObject painel_GamePlay;

    private void Start() {
        painel_Menu.SetActive(true);
        painel_GamePlay.SetActive(false);
    }
    public void BotaoPlay() {
        painel_Menu.SetActive(false);
        painel_GamePlay.SetActive(true);
        GameManager.instance.IniciarJogo();
    }

}
