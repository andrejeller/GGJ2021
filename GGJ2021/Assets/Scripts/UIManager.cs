using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject painel_Menu;
    public GameObject painel_GamePlay;


    public void BotaoPlay() {
        painel_Menu.SetActive(false);
        painel_GamePlay.SetActive(false);
        GameManager.instance.IniciarJogo();
    }

}
