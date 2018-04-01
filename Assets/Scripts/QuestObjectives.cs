using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestObjectives : MonoBehaviour {

	/***************************************
	 * ALL OF THESE ARRAYS ARE DESIGNED TO BE
	 * PARALLEL. DO NOT ITERATE THROUGH THEM
	 * SEPARATELY EVER.
	 ***************************************/
	private string[] QuestHeaderList = new string[7];			//quest titles
	private string[] QuestObjectiveList = new string[7];		//quest text
	public Transform[] QuestObjective = new Transform[7];		//quest marker (if applicable)
	private int currentQuest = 0;								//currently active quest
	private int totalQuests;
	private int lastFrameQuest = -1;							//compare and update quest text on completion
	public Text QuestHeaderText;								//quest header text box
	public Text QuestObjectiveText;								//quest objective text box
	public RawImage QuestMarkerUI;
	public Camera cam;

	// Use this for initialization
	void Start () {
		Scene level = SceneManager.GetActiveScene();
		if (level.buildIndex == 1) {
			//no quests planned for horse race
		}
		else if (level.buildIndex == 2) {
			LoadSpaceIntroQuests();
		}
		else if (level.buildIndex == 3) {
			LoadSpaceTwoQuests();
			Debug.Log("loaded space two");
		}
		else if (level.buildIndex == 4) {
			//no quests planned for boss fight
		}

		QuestHeaderText.text = QuestHeaderList[currentQuest];
		QuestObjectiveText.text = QuestObjectiveList[currentQuest];
		lastFrameQuest = currentQuest;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(currentQuest);

		//update quest ui text
		if (lastFrameQuest != currentQuest) {
			QuestHeaderText.text = QuestHeaderList[currentQuest];
			QuestObjectiveText.text = QuestObjectiveList[currentQuest];
			lastFrameQuest = currentQuest;
		}

		//quest ui marker
		if (QuestObjective[currentQuest] != null) {

			//activate marker object if inactive
			if (!QuestMarkerUI.gameObject.activeSelf)
				QuestMarkerUI.gameObject.SetActive(true);

			
			Vector3 uiSpacePos = cam.WorldToScreenPoint(QuestObjective[currentQuest].position);

			Vector3 vectCamToTarget = (QuestMarkerUI.transform.forward - Camera.main.transform.forward).normalized;
			
			/*
			float dotProd = Vector2.Dot(vectCamToTarget, Camera.main.transform.position.normalized);
			Debug.Log("dot product: " + dotProd);

			Transform enemyTemp = QuestMarkerUI.transform;
			enemyTemp.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
			enemyTemp.SetPositionAndRotation(new Vector3(QuestMarkerUI.transform.position.x,
											QuestMarkerUI.transform.position.y,
											QuestMarkerUI.transform.position.z),
											Quaternion.identity);
			Transform cameraTemp = Camera.main.transform;
			cameraTemp.SetPositionAndRotation(new Vector3(Camera.main.transform.position.x,
											 Camera.main.transform.position.y,
											 Camera.main.transform.position.z),
											 Quaternion.identity);
			Vector3 vectCamToTargetNoRotation = enemyTemp.localPosition - cameraTemp.localPosition;
			Debug.Log(vectCamToTarget + " -> " + vectCamToTargetNoRotation);
			*/

			//move ui marker accordingly
			if (QuestObjective[currentQuest].GetComponent<Renderer>().isVisible) {
				QuestMarkerUI.transform.position = new Vector3(uiSpacePos.x, uiSpacePos.y + 20f, 0f);
				//Debug.Log("on screen " + QuestMarkerUI.transform.position);
			}
			else {
				Debug.Log("cam to target" + vectCamToTarget);
				//	Vector3 vectPlayerToTarget = player.position - QuestMarkerUI.transform.position;
				//	Vector2 flat = Vector3.Project(vectPlayerToTarget, player.forward);
				//constrain to screen dimensions less 25 px for marker size
				int screenSizeX = Camera.main.scaledPixelWidth;
				int screenSizeY = Camera.main.scaledPixelHeight;

				//get current marker size (scales with screen size so we need lossyscale)
				int markerSizeX = (int)((float)QuestMarkerUI.gameObject.GetComponent<RectTransform>().rect.width 
									* QuestMarkerUI.transform.lossyScale.x);
				int markerSizeY = (int)((float)QuestMarkerUI.gameObject.GetComponent<RectTransform>().rect.height 
									* QuestMarkerUI.transform.lossyScale.y);

				//min and max screen x values
				float minX = markerSizeX;
				float maxX = screenSizeX - markerSizeX;

				//min and max screen y values
				float minY = markerSizeY;
				float maxY = screenSizeY - markerSizeY;

				//clamp ui space position to screen dimensions accounting for marker size
				//uiSpacePos.x = Mathf.Clamp(uiSpacePos.x, minX, maxX);
				//uiSpacePos.y = Mathf.Clamp(uiSpacePos.y, minY, maxY);
				//Debug.Log("screen space " + screenSizeX + "x by " + screenSizeY + "y");
				Debug.Log("marker space " + minX + "-" + maxX + "x " + minY + "-" + maxY + "y");
				Debug.Log("enemy uiSpacePos " + uiSpacePos);
				
				Debug.Log("uiSpPos x = " + uiSpacePos.x + " gets clamped to " + Mathf.Clamp(uiSpacePos.x, minX, maxX));
				Debug.Log("uiSpPos y = " + uiSpacePos.y + " gets clamped to " + Mathf.Clamp(uiSpacePos.y, minY, maxY));
				Vector2 onScreenSpace = new Vector2(Mathf.Clamp(uiSpacePos.x, minX, maxX),
													Mathf.Clamp(uiSpacePos.y, minY, maxY));
				//Debug.Log("does uispace clamp? " + uiSpacePos); MUST BE DIRECTLY ASSIGNED TO GET CLAMPED
				Debug.Log("enemy screenSpace preswap " + onScreenSpace);

				if ((onScreenSpace.x > minX && onScreenSpace.x < maxX) ||  //X is neither min nor max
					 (onScreenSpace.y > minY && onScreenSpace.y < maxY)) { //Y is neither min nor max
					//bool xx = (onScreenSpace.x > minX && onScreenSpace.x < maxX);
					//bool yy = (onScreenSpace.y != minY && onScreenSpace.y != maxY);
					//Debug.Log("x edge? " + xx);
					//Debug.Log("y edge? " + yy);
					//Debug.Log("y is " + onScreenSpace.y.ToString("0.00") + ", y min is " + 
					//		  minY.ToString("0.00") + ", y max is " + maxY.ToString("0.00"));
					Debug.Log("not touching an edge! snap called on coord " + onScreenSpace);
					onScreenSpace = new Vector2((maxX - onScreenSpace.x) + minX, (maxY - onScreenSpace.y) + minY);
					Debug.Log("enemy screenSpace postswap " + onScreenSpace);
					onScreenSpace = SnapToClosestEdge(onScreenSpace, minX, maxX, minY, maxY);
				}

				QuestMarkerUI.transform.position = new Vector3(onScreenSpace.x, onScreenSpace.y, 0f);
				//Debug.Log("off screen " + QuestMarkerUI.transform.position);

				//else { //dot prod < 0 (behind me)

				//}
				//QuestMarkerUI.transform.position = new Vector3(10000f, 10000f, 10000f);
			}
		}
		else {
			//deactivate ui marker for current quest
			if (QuestMarkerUI.gameObject.activeSelf)
				QuestMarkerUI.gameObject.SetActive(false);
		}
	}

	private Vector2 SnapToClosestEdge(Vector2 onScreenSpace, float minX, float maxX, float minY, float maxY) {
		float xLerp = Mathf.InverseLerp(minX, maxX, onScreenSpace.x);
		float yLerp = Mathf.InverseLerp(minY, maxY, onScreenSpace.y);
		Debug.Log("x lerp: " + xLerp + "y lerp: " + yLerp);
		//need 4 special cases: split the screen into quadrants
		//(0,0) is the bottom left of the screen
		if (xLerp < 0.5f && yLerp < 0.5f) { //quadrant one bottom left
			Debug.Log("x and y bottom left");
			if (xLerp < yLerp) { //if x is closer to the edge than y
				onScreenSpace.x = minX;
				Debug.Log("x floored");
			}
			else {
				onScreenSpace.y = minY;
				Debug.Log("y floored");
			}
		}
		else if (xLerp < 0.5f && yLerp > 0.5f) { //quadrant two top left
			Debug.Log("x and y top left");
			if (xLerp < (1 - yLerp)) { //if x is closer to the edge than y
				onScreenSpace.x = minX;
				Debug.Log("x floored");
			}
			else {
				onScreenSpace.y = maxY;
				Debug.Log("y ceilinged");
			}
		}
		else if (xLerp > 0.5f && yLerp > 0.5f) { //quadrant three top right
			Debug.Log("x and y top right");
			if ((1 - xLerp) < (1 - yLerp)) { //if x is closer to the edge than y
				onScreenSpace.x = maxX;
				Debug.Log("x ceilinged");
			}
			else {
				onScreenSpace.y = maxY;
				Debug.Log("y ceilinged");
			}
		}
		else if (xLerp > 0.5f && yLerp < 0.5f) { //quadrant four bottom right
			Debug.Log("x and y bottom right");
			if ((1 - xLerp) < yLerp) { //if x is closer to the edge than y
				onScreenSpace.x = maxX;
				Debug.Log("x ceilinged");
			}
			else {
				onScreenSpace.y = minY;
				Debug.Log("y floored");
			}
		}
		return onScreenSpace;
	}

	private void LoadSpaceIntroQuests() {

		//first quest - player does nothing
		int i = 0;
		QuestHeaderList[i] = "How2Fly";
		QuestObjectiveList[i] = "Stay a while and listen.";

		//second quest - player speeds up
		i++;
		QuestHeaderList[i] = "Gotta Go Fast";
		QuestObjectiveList[i] = "Press W to go fast.";

		//third quest - player slows down
		i++;
		QuestHeaderList[i] = "Goldilocks Does Not Approve";
		QuestObjectiveList[i] = "Press S to go slow.";

		//fourth quest - player does a barrel roll
		i++;
		QuestHeaderList[i] = "Obvious Reference";
		QuestObjectiveList[i] = "Press A or D to do a barrel roll.";

		//fifth quest - player steers with mouse
		i++;
		QuestHeaderList[i] = "Jiggle and Wiggle";
		QuestObjectiveList[i] = "Just move your mouse around. You can press the previous buttons at the same time too!";

		//sixth quest - player kills ship
		i++;
		QuestHeaderList[i] = "OH NO";
		QuestObjectiveList[i] = "Left click to FIRE! DESTROY DESTROY KILLKILL MURDER SLAUGHTER THEM ALL";

		//seventh quest - player moves on to next level
		i++;
		QuestHeaderList[i] = "move on";
		QuestObjectiveList[i] = "move on";

		totalQuests = i + 1;
	}

	private void LoadSpaceTwoQuests() {

		//first quest - go to marker
		int i = 0;
		QuestHeaderList[i] = "How2Fly";
		QuestObjectiveList[i] = "Stay a while and listen.";

		//second quest - kill the enemies
		i++;
		QuestHeaderList[i] = "How2Fly";
		QuestObjectiveList[i] = "Stay a while and listen.";

		//third quest - back to the marker
		i++;
		QuestHeaderList[i] = "How2Fly";
		QuestObjectiveList[i] = "Stay a while and listen.";
	}
	public void NextQuest() {
		currentQuest++;
	}

	public int GetCurrentQuest() {
		return currentQuest;
	}
}
