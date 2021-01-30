using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S00_SplashGameOver: MonoBehaviour
{

    [Header("Painel Falas")]
    public GameObject painelFalas;
    public GameObject balao1, balao2;

    [Header("Painel Final Final")]
    public GameObject painelFinalFinal;
    public GameObject[] outros = new GameObject[4];

    [Header("Outro")]
    public GameObject travaPlay;

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        gameObject.SetActive(false);
        painelFalas.SetActive(false);
        painelFinalFinal.SetActive(false);
    }

    public void Appear_Falas() {
        StartCoroutine(ShowAll_Falas());
    }
    public void Vanish_Falas() {
        StartCoroutine(HideAll_Falas());
    }

    /// FINAL FINAL
    public void Appear_Final() {
        StartCoroutine(ShowAll_Final());
    }
    public void Vanish_Final() {
        StartCoroutine(HideAll_Final());
    }


    private IEnumerator ShowAll_Final() {

        painelFinalFinal.SetActive(true);
        travaPlay.SetActive(true);
        painelFinalFinal.transform.localScale = Vector2.one;
        outros[0].transform.localScale = Vector2.zero;
        outros[1].transform.localScale = Vector2.zero;
        outros[2].transform.localScale = Vector2.zero;
        outros[3].transform.localScale = Vector2.zero;
        yield return null;

        // -- Show the cards and grass
        for (int i = 0; i < 4; i++) {
            Sequence character_Show = DOTween.Sequence();
            character_Show.Append(outros[i].transform.DOScale(1.2f, 0.5f))
                          .Append(outros[i].transform.DOScale(0.9f, 0.4f))
                          .Append(outros[i].transform.DOScale(1.0f, 0.5f));
            yield return new WaitForSeconds(0.2f); //0.2
        }

        yield return new WaitForSeconds(2.2f);
        travaPlay.SetActive(false);

    }

    private IEnumerator HideAll_Final() {
        
        outros[0].transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            outros[0].transform.DOScale(0.0f, 0.4f);
        });
        yield return new WaitForSeconds(0.1f);

        outros[1].transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            outros[1].transform.DOScale(0.0f, 0.4f);
        });
        yield return new WaitForSeconds(0.1f);

        outros[2].transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            outros[2].transform.DOScale(0.0f, 0.4f);
        });
        yield return new WaitForSeconds(0.1f);

        outros[3].transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            outros[3].transform.DOScale(0.0f, 0.4f);
        });

        yield return new WaitForSeconds(0.5f);
        painelFinalFinal.SetActive(false);
    }


    /// BALOES DE FALA
    private IEnumerator ShowAll_Falas() {

        // -- Hide all
        painelFalas.gameObject.SetActive(true);
        painelFalas.transform.localScale = Vector2.one;
        balao1.transform.localScale = Vector2.zero;
        balao2.transform.localScale = Vector2.zero;
        yield return null;

        balao1.transform.DOScaleY(1.0f, 0.5f);
        balao1.transform.DOScaleX(-1.0f, 0.5f);
        balao2.transform.DOScale(1.0f, 0.5f);
        yield return new WaitForSeconds(0.5f);

    }

    private IEnumerator HideAll_Falas() {
        // -- Hide Logos
        balao1.transform.DOScaleY(1.2f, 0.4f).OnComplete(() => {
            balao1.transform.DOScaleY(0.0f, 0.4f);
        });
        balao1.transform.DOScaleX(-1.2f, 0.5f).OnComplete(() => {
            balao1.transform.DOScaleX(0.0f, 0.4f);
        });
        yield return new WaitForSeconds(0.2f);
        balao2.transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            balao2.transform.DOScale(0.0f, 0.4f);
        });

        yield return new WaitForSeconds(0.9f);
        painelFalas.SetActive(false);
    }





}
