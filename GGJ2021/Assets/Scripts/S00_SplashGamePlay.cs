using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S00_SplashGamePlay: MonoBehaviour {

    public GameObject palmas, sliderCaminho;
    public GameObject fundoTutorial, popUpTutorial, setas;
    public GameObject acertou, errou, ads;

    public void Appear() {
        StartCoroutine(ShowAll());
    }
    public void Vanish() {
        StartCoroutine(HideAll());
    }


    private IEnumerator ShowAll() {

        fundoTutorial.SetActive(true);
        popUpTutorial.SetActive(true);
        setas.SetActive(true);
        ads.SetActive(true);

        // -- Hide all
        palmas.transform.localScale = Vector2.zero;
        sliderCaminho.transform.localScale = Vector2.zero;
        fundoTutorial.transform.localScale = Vector2.zero;
        popUpTutorial.transform.localScale = Vector2.zero;
        setas.transform.localScale = Vector2.zero;
        ads.transform.localScale = Vector2.zero;
        yield return null;

        //Mover o texto de versao para cima
        palmas.transform.DOScale(1.0f, 0.5f);
        sliderCaminho.transform.DOScale(1.0f, 0.7f);
        fundoTutorial.transform.DOScale(1.0f, 0.7f);
        popUpTutorial.transform.DOScale(1.0f, 0.7f);
        setas.transform.DOScale(1.0f, 0.7f);
        ads.transform.DOScale(1.0f, 0.7f);
        ///versionText.transform.DOMoveY(20.0f, 1.0f);
        yield return new WaitForSeconds(0.2f);



    }

    private IEnumerator HideAll() {
        // -- Hide Logos
        palmas.transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            palmas.transform.DOScale(0.0f, 0.4f);
        });
        yield return new WaitForSeconds(0.2f);

        // -- Hide cards and grass
        sliderCaminho.transform.DOScale(0.0f, 0.5f);
        ads.transform.DOScale(0.0f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        fundoTutorial.SetActive(false);
        popUpTutorial.SetActive(false);
        setas.SetActive(false);
        ads.SetActive(false);

        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        palmas.transform.localScale = Vector2.zero;
        sliderCaminho.transform.localScale = Vector2.zero;
        fundoTutorial.SetActive(false);
        popUpTutorial.SetActive(false);
        setas.SetActive(false);
        //buttons[0].transform.localScale = Vector2.zero;
        //buttons[1].transform.localScale = Vector2.zero;
        //buttons[2].transform.localScale = Vector2.zero;
        gameObject.SetActive(false);
    }




    public void FlashAcertou() {
        StartCoroutine(FlashAcertouCorroutine());
    }
    public void FlashErrou() {
        StartCoroutine(FlashErrouCorroutine());
    }
    private IEnumerator FlashAcertouCorroutine() {
        float time = 0.05f;
        yield return new WaitForSeconds(time);
        acertou.SetActive(true);
        yield return new WaitForSeconds(time);
        acertou.SetActive(false);

        yield return new WaitForSeconds(time);
        acertou.SetActive(true);
        yield return new WaitForSeconds(time);
        acertou.SetActive(false);

        yield return new WaitForSeconds(time);
        acertou.SetActive(true);
        yield return new WaitForSeconds(time);
        acertou.SetActive(false);


        yield return null;
    }

    
    private IEnumerator FlashErrouCorroutine() {
        float time = 0.05f;
        yield return new WaitForSeconds(time);
        errou.SetActive(true);
        yield return new WaitForSeconds(time);
        errou.SetActive(false);

        yield return new WaitForSeconds(time);
        errou.SetActive(true);
        yield return new WaitForSeconds(time);
        errou.SetActive(false);

        yield return new WaitForSeconds(time);
        errou.SetActive(true);
        yield return new WaitForSeconds(time);
        errou.SetActive(false);

        yield return null;
    }

}
