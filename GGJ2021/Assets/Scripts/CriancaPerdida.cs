using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CriancaPerdida : MonoBehaviour {


    public GameObject[] bones = new GameObject[5];
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void MudarBone() {
        int rand = Random.Range(0, 5);
        for (int i = 0; i < bones.Length; i++) {
            bones[i].SetActive(false);
        }
        bones[rand].SetActive(true);
    }

    public void IrParaOsPais() {
        transform.parent = null;
        anim.SetBool("levanta", true);
        transform.DOMove(new Vector3(54.5f, -1.5f, -2.0f), 1.3f);
        transform.DORotate(new Vector3(0, 200, 0), 1.3f, RotateMode.FastBeyond360);
    }

}
