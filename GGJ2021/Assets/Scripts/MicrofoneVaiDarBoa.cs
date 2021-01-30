using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrofoneVaiDarBoa : MonoBehaviour {


    private AudioSource audioSource;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();

        foreach (string device in Microphone.devices) {
            Debug.Log(device);
        }
        string padrao = Microphone.devices[0].ToString();

        audioSource.Stop();

        audioSource.clip = Microphone.Start(padrao, false, 10, AudioSettings.outputSampleRate);
        audioSource.loop = true;
        
        
        Debug.Log(padrao);
        Debug.Log(Microphone.IsRecording(padrao).ToString());

        if (Microphone.IsRecording(padrao)) {
            //while (!(Microphone.GetPosition(padrao) > 0)) {
            //}

            Debug.Log("Gravacao iniciada");
            audioSource.Play();

        }else {
            Debug.Log("Algo deu errado");
        }

    }

    // Update is called once per frame
    void Update(){
        // Debug.Log(audioSource.GetSpectrumData());

        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        /*
        for (int i = 1; i < spectrum.Length - 1; i++) {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }*/


        for (int i = 0; i < 10; i++) {
            if (spectrum[i] * 100000 > 60) {
                Debug.Log("OPA");
            }
        }

        

    }
}
