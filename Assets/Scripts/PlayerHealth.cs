using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth: MonoBehaviour {
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.5f);     // The colour the damageImage is set to, to flash.


	//Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	Destructible destructible;                              // Reference to the player's movement.
	//PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.


	void Awake() {
		// Setting up the references.
		//anim = GetComponent<Animator>();
		playerAudio = GetComponent<AudioSource>();
		//playerMovement = GetComponent<PlayerMovement>();
		//playerShooting = GetComponentInChildren<PlayerShooting>();
		destructible = GetComponent<Destructible>();
		// Set the initial health of the player.
		currentHealth = startingHealth;
		
	}


	void Update() {
		//// If the player has just been damaged...
		if (damaged) {
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else {
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		//// Reset the damaged flag.
		damaged = false;
	}


	public void TakeDamage() {
		// Set the damaged flag so the screen will flash.
		damaged = true;
		damageImage.color = flashColour;
		//// Reduce the current health by the damage amount.
		//destructible.hitPoints -= amount;

		//// Set the health bar's value to the current health.
		healthSlider.value = destructible.hitPoints;

		//// Play the hurt sound effect.
		//playerAudio.Play();
	}


	void Death() {
		// Set the death flag so this function won't be called again.
		if (!isDead) isDead = true;

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		playerAudio.clip = deathClip;
		playerAudio.Play();
	}
}