using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PersonagensSentados: MonoBehaviour {

    private Animator anim;
    private int hashLevanta = Animator.StringToHash("levanta");

    private bool levantei = false;
    public Vector3 posVirado;
    public Vector3 rotacaoVirado;

    void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (levantei) return;

        if (Camera.main.transform.position.x + 2.0f >= transform.position.x) {
            Debug.Log("Levantei");
            levantei = true;
            HoraDeLevantar();
        }
    }

    public void HoraDeLevantar() {
        transform.DOLocalMove(posVirado, 0.9f);
        transform.DOLocalRotate(rotacaoVirado, 0.9f, RotateMode.Fast);
        anim.SetBool(hashLevanta, true);
    }

}
