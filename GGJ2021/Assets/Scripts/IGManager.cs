using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Welcoming,
    MainMenu,       //State when the game is loading
    Menu,           //State when the loading is OK, and we are waiting the player press a button
    Starting,       //State when the player press "PlayGame" and we are waiting the "Vanish" animation from the menu
    Playing,        //State where the game happens
    FirstMove,
    PlayAgain,
    Pause,          //State when we were in the game but the player press "PAUSE"
    GameOver,       //State when the player LOSE or WIN the game
    Info,           //State where we put some info about the game, like dev name's
    HowToPlay,      //State where we teach the Player what he need to do so he can play the game
    promo,          // ---------------- WHAT ABOUT THESE??
    wait,           //Bonus State if we need to do something else, that is required to Wait something
    none
}

public abstract class IGManager: MonoBehaviour {

    public static IGManager instance;
    protected State _gameIs = State.Welcoming;
    protected State _gameWas = State.Welcoming;
    public State GameIs { get { return _gameIs; } private set { } }
    //public static GameObject gObject { get { return instance.gameObject; } private set { } }

    #region STATE MACHINE
    public static void StateMachine() {

        switch (instance.GameIs) {
            case State.Welcoming:
                break;
            case State.MainMenu:
                // FUNC
                break;
            case State.Menu:
                instance.StartCoroutine(instance.MenuCoroutine());
                break;
            case State.Starting:
                // Fecha a tela de menu e aí então o jogo começa
                instance.StartCoroutine(instance.StartingCoroutine());
                break;
            case State.Playing:
                instance.StartCoroutine(instance.PlayingCoroutine());
                break;
            case State.FirstMove:
                // FUNC
                break;
            case State.Pause:
                // FUNC
                break;
            case State.GameOver:
                instance.StartCoroutine(instance.GameOverCoroutine());
                break;
            case State.Info:
                break;
            case State.HowToPlay:
                // FUNC
                break;
            case State.promo:
                break;
            case State.wait:
                // FUNC
                Debug.Log("Waiting Somthing");
                break;
            case State.none:
                // FUNC
                break;
            default:
                break;
        }

    }
    #endregion

    #region FUNCOES GERAIS
    public void ChangeGameState(State nextState) {
        _gameWas = _gameIs;
        _gameIs = nextState;

        if (_gameWas != State.Info && _gameWas != State.Pause && _gameWas != State.HowToPlay)
            StateMachine();

        //BottomMenu.ChangeTo(nextState);
        //Debug.Log("Next State - " + nextState);
    }
    protected IEnumerator ResetScene(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        //SceneLoader.ReloadScene();
    }
    #endregion

    #region FUNCOES ABSTRATAS
    // protected abstract void HandleScreen(State WhatState);
    // protected abstract void StartTheGame(); // Já criei outra - IniciaroOJogo
    public abstract void PlayAgain();
    #endregion

    #region CORROTINAS ABSTRATAS
  
    protected abstract IEnumerator MenuCoroutine();
    protected abstract IEnumerator StartingCoroutine();
    protected abstract IEnumerator PlayingCoroutine();

   
   
    protected abstract IEnumerator GameOverCoroutine();
    #endregion


   

}
