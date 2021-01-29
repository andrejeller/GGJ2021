using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S00_SplashMenu: MonoBehaviour{

    public GameObject logo, painelInfo;
    public GameObject[] buttons = new GameObject[3];

    public void Appear() {
        StartCoroutine(ShowAll());
    }
    public void Vanish() {
        StartCoroutine(HideAll());
    }


    private IEnumerator ShowAll() {

        // -- Hide all
        logo.transform.localScale = Vector2.zero;
        painelInfo.transform.localScale = Vector2.zero;
        buttons[0].transform.localScale = Vector2.zero;
        buttons[1].transform.localScale = Vector2.zero;
        buttons[2].transform.localScale = Vector2.zero;
        yield return null;

        // -- Show the cards and grass
        for (int i = 0; i < 3; i++) {
            ShowUpSomeButton(i);
            yield return new WaitForSeconds(0.2f); //0.2
        }

        //Mover o texto de versao para cima
        painelInfo.transform.DOScale(1.0f, 0.5f);
        ///versionText.transform.DOMoveY(20.0f, 1.0f);
        yield return new WaitForSeconds(0.2f);

        // -- Show the logos
        Sequence logo1_Show = DOTween.Sequence();
        logo1_Show.Append(logo.transform.DOScale(1.2f, 0.5f))
                              .Append(logo.transform.DOScale(0.9f, 0.4f))
                              .Append(logo.transform.DOScale(1.0f, 0.5f));

    }

    private IEnumerator HideAll() {
        // -- Hide Logos
        logo.transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            logo.transform.DOScale(0.0f, 0.4f);
        });
        yield return new WaitForSeconds(0.2f);

        // -- Hide cards and grass
        painelInfo.transform.DOScale(0.0f, 0.5f);

        yield return new WaitForSeconds(0.3f);
        //versionText.transform.DOMoveY(-60.0f, 0.7f);

        for (int i = 0; i < 3; i++) {
            HideSomeButton(i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        Hide();
    }

    private void ShowUpSomeButton(int cardID) {
        Sequence character_Show = DOTween.Sequence();
        character_Show.Append(buttons[cardID].transform.DOScale(1.2f, 0.5f))
                      .Append(buttons[cardID].transform.DOScale(0.9f, 0.4f))
                      .Append(buttons[cardID].transform.DOScale(1.0f, 0.5f));

    }

    private void HideSomeButton(int ID) {
        buttons[ID].transform.DOScale(1.2f, 0.5f).OnComplete(() => {
            buttons[ID].transform.DOScale(0.0f, 0.4f);
        });
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    public void Hide() {
        logo.transform.localScale = Vector2.zero;
        painelInfo.transform.localScale = Vector2.zero;
        buttons[0].transform.localScale = Vector2.zero;
        buttons[1].transform.localScale = Vector2.zero;
        buttons[2].transform.localScale = Vector2.zero;
        gameObject.SetActive(false);
    }


}
