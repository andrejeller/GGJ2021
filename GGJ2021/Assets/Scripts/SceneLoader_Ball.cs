using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader_Ball: MonoBehaviour {

    //public bool rotate = true;

    void Update() {
        //if (!rotate) return;
        transform.Rotate(0, 0, -150 * Time.deltaTime);
    }
}
