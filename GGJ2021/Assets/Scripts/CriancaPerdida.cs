using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriancaPerdida : MonoBehaviour {


    public GameObject[] bones = new GameObject[5];

    public void MudarBone() {
        int rand = Random.Range(0, 5);
        for (int i = 0; i < bones.Length; i++) {
            bones[i].SetActive(false);
        }
        bones[rand].SetActive(true);
    }

}
