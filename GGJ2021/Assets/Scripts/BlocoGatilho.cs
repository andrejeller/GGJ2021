using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoGatilho : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("bloco_batida")) {
            //GameManager.instance.BlocoEntrou();
        }

    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("bloco_batida")) {
           // GameManager.instance.BlocoSaiu();
        }
    }

}
