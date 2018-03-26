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
			Debug.Log("loaded space quests");
		}
		else if (level.buildIndex == 3) {

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

			//move ui marker accordingly
			if (QuestObjective[currentQuest].GetComponent<Renderer>().isVisible) {
				Vector3 temp = cam.WorldToScreenPoint(QuestObjective[currentQuest].position);
				QuestMarkerUI.transform.position = new Vector3(temp.x, temp.y + 20f, 0f);
			}
			else {
				QuestMarkerUI.transform.position = new Vector3(10000f, 10000f, 10000f);
			}
		}
		else {
			//deactivate ui marker for current quest
			if (QuestMarkerUI.gameObject.activeSelf)
				QuestMarkerUI.gameObject.SetActive(false);
		}
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

	public void NextQuest() {
		currentQuest++;
	}

	public int GetCurrentQuest() {
		return currentQuest;
	}
}
