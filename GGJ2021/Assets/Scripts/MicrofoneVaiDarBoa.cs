using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrofoneVaiDarBoa : MonoBehaviour {

    private AudioClip clipRecord;

    void Start() {

        foreach (string device in Microphone.devices) {
            Debug.Log(device);
        }
        string padrao = Microphone.devices[0].ToString();

        clipRecord = Microphone.Start(padrao, true, 999, AudioSettings.outputSampleRate);

        Debug.Log(padrao);
        Debug.Log(Microphone.IsRecording(padrao).ToString());

        if (Microphone.IsRecording(padrao)) {
            //while (!(Microphone.GetPosition(padrao) > 0)) {
            //}

            Debug.Log("Gravacao iniciada");
            //audioSource.Play();

        } else {
            Debug.Log("Algo deu errado");
        }

    }

    void Update() {

        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
        clipRecord.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        float levelMax = 0;
        for (int i = 0; i < dec; i++) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // use it like:
        // Debug.Log(Mathf.Sqrt(levelMax) * 100);
        if (Mathf.Sqrt(levelMax) * 100 > 1.1f) {
            Debug.Log("Opa!");
            GameManager.local_instance.ComandoAvancarComMicrofone();
        }
    }

    

}

/*
 
 
 */