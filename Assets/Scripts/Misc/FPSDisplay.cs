using UnityEngine;

public class FPSDisplay : MonoBehaviour {
	float deltaTime = 0.0f;
	
	bool average = true;

	const int averageCount = 200;
	float[] lastFps;
	int lastFpsPointer = 0;

	float averageFps;

	GUIStyle style;
	Rect rect;

	void Awake() {
		style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = Screen.height * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		rect = new Rect(0, 0, Screen.width, Screen.height * 2 / 100);
	
		if (average) {
			averageFps = Time.deltaTime;
			lastFps = new float[averageCount];
			for (int i = 0; i < averageCount; ++i)
				lastFps[i] = averageFps;
		}
	}
	void Update() {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		if (average) {
			lastFps[lastFpsPointer] = Time.deltaTime;

			float accum = 0;
			for (int i = 0; i < averageCount; ++i) {
				accum += lastFps[i];
			}
			averageFps = accum / averageCount;
			lastFpsPointer = ++lastFpsPointer % averageCount;
		}
	}

	void OnGUI() {
		int w = Screen.width, h = Screen.height;

		style.fontSize = h * 2 / 100;
		rect.width = w;
		rect.height = h * 2 / 100;
		
		float msec = deltaTime * 1000.0f;
		float fps = Time.timeScale == 0f ? 0f : 1.0f / deltaTime;
		string text = "";
		if (average) {
			text = string.Format("{0:0.0} ms ({1:0.} fps) ({2:0.} avg)", msec, fps, 1.0f / averageFps);
		} else {
			text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		}
		GUI.Label(rect, text, style);
	}
}