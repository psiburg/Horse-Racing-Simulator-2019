using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIController : MonoBehaviour {

	public bool UIEnabled;
	//private RectTransform[] allChildren;
	private bool alreadyEnabled;
	private bool alreadyDisabled;
	private CanvasGroup UIGroup;
	private AudioSource UIPowerup;

	// Use this for initialization
	void Start () {
		//allChildren = GetComponentsInChildren<RectTransform>();
		UIGroup = GetComponent<CanvasGroup>();
		UIPowerup = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(UIEnabled);
		if (UIEnabled && !alreadyEnabled) {
			EnableUI();
			alreadyDisabled = false;
			alreadyEnabled = true;
		}
		else if (!UIEnabled && !alreadyDisabled) {
			DisableUI();
			alreadyDisabled = true;
			alreadyEnabled = false;
		}

		if (UIGroup.alpha < 1f && UIEnabled) {
			UIGroup.alpha += Time.deltaTime / UIPowerup.clip.length;
		}

	}

	void EnableUI() {
		//enable space particles on ui
		UIPowerup.Play();
		ParticleRenderer spaceDust = Camera.main.GetComponentInChildren<ParticleRenderer>(true);
		if (spaceDust != null) {
			spaceDust.enabled = true;
		}
	} 

	void DisableUI() {
		UIGroup.alpha = 0;
		//disable space particles on ui
		ParticleRenderer spaceDust = Camera.main.GetComponentInChildren<ParticleRenderer>(true);
		if (spaceDust != null) {
			spaceDust.enabled = false;
		}
	}
}
