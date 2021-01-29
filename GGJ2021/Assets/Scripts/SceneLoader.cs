using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader: MonoBehaviour
{

    private static SceneLoader _instance;
    public static SceneLoader instance { get { return _instance; } private set { } }


    public Image panel, ball;
    public SceneLoader_Ball ball_sc;
    //public GameObject ball;


    void Awake() {
        if (_instance == null) {
            _instance = this;
            //Debug.Log("Instancia de SceneLoader criada");
            Debug.Log("[SYS] SceneLoader created");
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
            //Debug.Log("Instancia de SceneLoader já existe");
            Debug.Log("[SYS] SceneLoader instance already exists");

        }
    }

    public void InitializeOn() {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1);
        ball.color = new Color(ball.color.r, ball.color.g, ball.color.b, 1);
    }
    public void InitializeOff() {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
        ball.color = new Color(ball.color.r, ball.color.g, ball.color.b, 0);
    }


    public void Show() {
        StartCoroutine(ShowCorroutine());
    }
    public void Vanish() {
        StartCoroutine(VanishCorroutine());
    }
    public static void ReloadScene() {
        int atual = SceneManager.GetActiveScene().buildIndex;
        instance.StartCoroutine(instance.LoadSceneCorroutine(atual));
    }

    public static void LoadScene(int sceneIndex) {
        instance.StartCoroutine(instance.LoadSceneCorroutine(sceneIndex));
    }


    private IEnumerator VanishCorroutine() {
        yield return null;
        float fadeTime = 0.9f;

        //ball_sc.rotate = true;

        ball.DOFade(0, fadeTime);
        panel.DOFade(0, fadeTime / 2);

    }
    private IEnumerator ShowCorroutine() {
        yield return null;
        float fadeTime = 0.9f;


        //ball_sc.rotate = true;

        ball.DOFade(1, fadeTime / 2);
        panel.DOFade(1, fadeTime);

    }

    private IEnumerator LoadSceneCorroutine(int sceneIndex) {
        yield return null;
        Show();

        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = true;

        //yield return new WaitUntil(() => operation.isDone);

        //operation.allowSceneActivation = true;
        operation.completed += (asyncOperation) => {
            //
            //operation.allowSceneActivation = true;
            new WaitForSeconds(0.5f);
            Vanish();
        };

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }





    public void QuitGame() {
        Debug.Log("Jogo Encerrado");
        Application.Quit();
    }

    //public void LoadStandBy() {
    //    SceneManager.LoadScene("STANDBY");
    //}

    //public void LoadMainMenu() {
    //    SceneManager.LoadScene(1);
    //}

    //public void LoadChoseThingsMenu() {
    //    SceneManager.LoadScene(2);
    //}

    //public void LoadGame() {
    //    LOAD_SCENE.sceneToLoad = 3;
    //    SceneManager.LoadScene(5);
    //}

    //public void LoadCredts() {
    //    Debug.Log("Indo aos Creditos --- Sem funcao abaixo");
    //    //SceneManager.LoadScene(2);
    //}



}
