using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceIntroQuestCompleter : MonoBehaviour {

	public QuestObjectives questList;
	private PlayerControl player;
	private float wait;
	public GameObject enemyToSpawn;
	private bool spawnedEnemyYet = false;
	public AudioSource questChime;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		player.GetComponentInParent<PlayerInput>().enabled = false;
		player.GetComponentInParent<PlayerControl>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//first quest - player does nothing
		if (questList.GetCurrentQuest() == 0) {
			//play welcome sound file
			//move on to next quest after sound file finishes
			wait += Time.deltaTime;
			if (wait > 10f) {
				questList.NextQuest();
				wait = 0;
			}
			//player.MoveForward();
		}
		//second quest - player speeds up
		else if (questList.GetCurrentQuest() == 1) {
			if (Input.GetKey(KeyCode.W)) {
				player.currentSpeed = player.maxSpeed;
				player.MaxTurbines(player.highTurbines);
				questList.NextQuest();
				questChime.Play();
			}
			else
				player.currentSpeed = player.defaultSpeed;
			player.MoveForward();
		}
		//third quest - player slows down
		else if (questList.GetCurrentQuest() == 2) {
			if (Input.GetKey(KeyCode.S)) {
				player.currentSpeed = player.minSpeed;
				player.MaxTurbines(player.lowTurbines);
				questList.NextQuest();
				questChime.Play();
			}
			else
				player.currentSpeed = player.defaultSpeed;
			player.MoveForward();
		}
		//fourth quest - player does a barrel roll
		else if (questList.GetCurrentQuest() == 3) {
			if (Input.GetKey(KeyCode.A)) {
				transform.Rotate(0, 0, Time.deltaTime * player.rollSpeed);
				questList.NextQuest();
				questChime.Play();
			}
			else if (Input.GetKey(KeyCode.D)) {
				transform.Rotate(0, 0, -Time.deltaTime * player.rollSpeed);
				questList.NextQuest();
				questChime.Play();
			}
			player.MoveForward();
		}
		//fifth quest - player steers with mouse
		else if (questList.GetCurrentQuest() == 4) {
			player.GetComponentInParent<PlayerControl>().enabled = true;
			wait += Time.deltaTime;
			if (wait > 5f) {
				questList.NextQuest();
				wait = 0;
				questChime.Play();
			}
		}
		//sixth quest - player kills ship
		else if (questList.GetCurrentQuest() == 5) {
			
			//spawn enemy in front of player
			if (!spawnedEnemyYet) {
				GameObject enemy = Instantiate(enemyToSpawn, player.transform.position, Quaternion.Inverse(player.transform.rotation)) as GameObject;
				enemy.transform.position = player.transform.position + (Vector3.forward * 300);
				Debug.Log(enemy.GetComponent<EnemyMovement>().target);
				questList.QuestObjective[questList.GetCurrentQuest()] = enemy.transform;
				spawnedEnemyYet = true;
			}
			player.GetComponentInParent<PlayerInput>().enabled = true;
			if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
				questChime.Play();
				questList.NextQuest();
			}
		}
		//seventh quest - player moves on to next level
		else if (questList.GetCurrentQuest() == 6) {

			if ((player.transform.position - questList.QuestObjective[questList.GetCurrentQuest()].position).magnitude < 25) {
				questList.QuestObjective[questList.GetCurrentQuest()].GetComponent<LevelEnd>().enabled = true;
				wait = 0;
			}
			wait++;
			if (wait > 8f) {
				Scene level = SceneManager.GetActiveScene();
				SceneManager.LoadScene(level.buildIndex + 1);
			}
		}
	}
}
