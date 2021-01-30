using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JanelaInfo : MonoBehaviour {

    private bool podeFechar = false;
    private bool podeAbrir = true;


    private void Start() {
        transform.localScale = Vector2.zero;
        //gameObject.SetActive(false);
        podeFechar = false;
        podeAbrir = true;
    }

    public void AbrirJanela() {
        if (podeAbrir) {
            podeAbrir = false;
            StartCoroutine(Abrir());
        }
    }

    public void FecharJanela() {
        if (podeFechar) {
            podeFechar = false;
            StartCoroutine(Fechar());
        }
    }

    private IEnumerator Abrir() {

        //gameObject.SetActive(true);
        transform.localScale = Vector2.zero;
        yield return null;

        transform.DOScale(1.2f, 0.3f).OnComplete(() => {
            transform.DOScale(1.0f, 0.2f);
        });
        yield return new WaitForSeconds(0.5f);
        podeFechar = true;
    }

    private IEnumerator Fechar() {
        yield return null;

        transform.DOScale(1.2f, 0.3f).OnComplete(() => {
            transform.DOScale(0.0f, 0.2f);
        });
        yield return new WaitForSeconds(0.5f);
        //gameObject.SetActive(true);
        podeAbrir = true;
    }

}
