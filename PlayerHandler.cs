using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//should have these to function properly
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class PlayerHandler : MonoBehaviour {

	//parameters for the rpg element
	private int playerLevel = 1;
	private int experienceRequired = 10;
	private int currentExperience = 0;
	private static float maxHealth = 100f;
	private float currentHealth = maxHealth;
	private float attackPower = 10f;
	private float defensePower = 10f;
	private float luck = 10f;
	private float accuracy = 10f;


	//should not be effected outside of status effects
	private float speed = 10f;
	private float jumpPower = 10f;

	//maxHealth (0, 7), attackPower (1, 8), defensePower (2, 9), speed (3, 10), jumpPower (4, 11), luck (5, 12), accuracy (6, 13) (+/-)
	//first Parameter is the percent change, second is the duration
	private float[,] status = new float[14,2];

	//11 items with a max of 100 in inventory
	//0 - Potion (30% heal)
	//1 - Panacea (clear negative status) 
	//2 - Temporary MaxHP Boost (15s)
	//3 - Temporary Attack Boost (15s)
	//4 - Temporary Defense Boost (15s)
	//5 - Temporary Speed Boost (15s)
	//6 - Temporary Jump Boost (15s)
	//7 - Temporary Luck Boost (15s)
	//8 - Temporary Accuracy Boost (15s)
	//9 - Armor Level (scales percentage-wise, level 1 = 1% boost, Level 2 = 2%, etc.)
	//10 - Broom Level (scales percentage-wise, level 1 = 1% boost, Level 2 = 2%, etc.)
	private int[,] inventory = new int[11,100];


	//________________________________________________________________________________________________________________________________________
	//set and get methods for each parameter

	int getPlayerLevel(){ return playerLevel; }
	void setPlayerLevel(int level, bool withIncrement = true){
		if (withIncrement) {
			reset();
			for (int i = 1; i < level; i++) {
				setCurrentExperience( getExperienceRequired() );
				checkLevelUp();
			}
		}
		else {
			playerLevel = level;
		}
	}

	int getExperienceRequired(){ return experienceRequired; }
	void setExperienceRequired(int exp){ experienceRequired = exp; }

	int getCurrentExperience(){ return currentExperience; }
	void setCurrentExperience(int exp){ currentExperience = exp; }

	float getMaxHealth(){ return maxHealth; }
	void setMaxHealth(float HP){ maxHealth = HP; }

	float getCurrentHealth(){ return currentHealth; }
	void setCurrentHealth(float HP){ currentHealth = HP; }

	float getAttackPower(){ return attackPower; }
	void setAttackPower(float atk){ attackPower = atk; }

	float getDefensePower(){ return defensePower; }
	void setDefensePower(float def){ defensePower = def; }

	float getLuck(){ return luck; }
	void setLuck(float lck){ luck = lck; }

	float getAccuracy(){ return accuracy; }
	void setAccuracy(float acc){ accuracy = acc; }

	float getSpeed(){ return speed; }
	void setSpeed(float spd){ speed = spd; }

	float getJumpPower(){ return jumpPower; }
	void setJumpPower(float jump){ jumpPower = jump; }

	float[,] getStatus(){ return status; }


	int[,] getInventory(){ return inventory; }


	//________________________________________________________________________________________________________________________________________


	//
	void reset(){
		playerLevel = 1;
		experienceRequired = 10;
		currentExperience = 0;
		maxHealth = 100f;
		currentHealth = maxHealth;
		attackPower = 10f;
		defensePower = 10f;
		luck = 10f;
		accuracy = 10f;
	}

	//method to use an item, there should be some cooldown method attached to the button handler
	void UseItem(int itemIndex){
		if (inventory[itemIndex] == 0){
			//play error sound
		}

		if (itemIndex == 0){
			//use health potion
			inventory[itemIndex] -= 1;
			currentHealth *= 1.3f;
			//keeps health below max health
			if (currentHealth > maxHealth){
				currentHealth = maxHealth;
			}
		}

		if (itemIndex == 1){
			//use Panacea
			for (int i = 7; i < 12; i++){
				status [i, 0] = 0;
				status[i, 1] = 0;
			}
		}

		if (itemIndex > 1 && itemIndex < 9){
			//use on of the temporary boosts
			inventory[itemIndex] -= 1;
			status [itemIndex - 2, 0] = 20;
			status[itemIndex - 2, 1] = 15;
		}

		//armor and broom level are not included since they are passive items
	}

	//method to calculate a output damage within a range
	float calculateDamage(){
		//a number from 90% to 100% of the base attack power is taken and then has the status and passive items applied to calculate the damage
		return Random.Range(attackPower * .9, attackPower * 1.1) * (1 + inventory [10, 0] * .01) * (1 + status [1, 0] - status [8, 0]);
	}

	//method that takes the input damage and readjusts it value based on defense
	void takeDamage(float damage, int enemyAttackPower){
		//readjust the damage based on the percentage difference of the enemy's attack power and the player effecctive defense
		damage = damage * (1 - (defensePower * (1 + status[2, 0] - status[9, 0]) - enemyAttackPower) / defensePower);
		currentHealth -= damage;
		//sets HP to zero if you take too much damage
		if (currentHealth < 0) {
			currentHealth = 0;
		}
	}

	//decides whether or not the attack will hit or miss, based on your luck and the enemy's accuracy
	bool willTakeHit(int enemyAccuracy){
		if (luck > enemyAccuracy * 1.5) {
			return false;
		}
		if (luck * 1.5 < enemyAccuracy) {
			return true;
		}
		return Random.Range (0, 1) < .7;
	}

	//method that will update the status effects on the player 
	//should be processed by a timer handler to be executed every second
	void statusUpdate(){
		for (int i = 0; i < status.GetLength(0); i++) {
			if (status [i, 1] == 0) {
				status [i, 0] = 0;
			} 
			else {
				status[i, 1] -= 1;
			}
		}
	}

	//put in update function to constatnly check if you can level up
	//randomly increments parameters
	void checkLevelUp(){
		if (currentExperience >= experienceRequired) {
			//carries over experience
			//can level up multiple times
			currentExperience = currentExperience - experienceRequired;
			playerLevel++;
			//increments experience needed for the next level
			experienceRequired = (int)(10 * Mathf.Pow(playerLevel, 2.25f));
			maxHealth += Random.Range (10, 20);
			//refresh health
			currentHealth = maxHealth;
			//increments over parameters randomly
			attackPower += Random.Range (3, 7);
			defensePower += Random.Range (3, 7);
			luck += Random.Range (3, 7);
			accuracy += Random.Range (3, 7);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
