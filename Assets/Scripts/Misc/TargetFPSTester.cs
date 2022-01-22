using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetFPSTester : MonoBehaviour {

    // public Slider fpsSlider;
    // public Text fpsText;
    [Range(4, 240)]
    public int value;
    void Awake() {
        
    }
    
    void Update() {
        Application.targetFrameRate = value;

        // Application.targetFrameRate = (int) fpsSlider.value;
        // fpsText.text = fpsSlider.value.ToString();
    }
}
