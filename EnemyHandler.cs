using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//should have these to function properly
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class EnemyHandler : MonoBehaviour {

	private Animator anim;
	private AnimatorStateInfo currentState;
	private CapsuleCollider col;
	private Rigidbody rb;
	private Vector3 velocity;

	//parameters for the rpg element
	private static float maxHealth = 100f;
	private float currentHealth = maxHealth;
	private float attackPower = 10f;
	private float defensePower = 10f;
	private float luck = 10f;
	private float accuracy = 10f;


	//should not be effected outside of status effects
	private float speed = 10f;
	private float jumpPower = 10f;

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

	void reset(){
		maxHealth = 100f;
		currentHealth = maxHealth;
		attackPower = 10f;
		defensePower = 10f;
		luck = 10f;
		accuracy = 10f;
	}

	float calculateDamage(){
		//a number from 90% to 100% of the base attack power is taken and then has the status and passive items applied to calculate the damage
		return Random.Range(attackPower * .9f, attackPower * 1.1f);
	}

	//method that takes the input damage and readjusts it value based on defense
	void takeDamage(float damage, int enemyAttackPower){
		//readjust the damage based on the percentage difference of the enemy's attack power and the player effecctive defense
		damage = damage * (1 - (defensePower - enemyAttackPower) / defensePower);
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

	void Start () {
		anim = GetComponent<Animator>();
		col = GetComponent<CapsuleCollider>();
		rb = GetComponent<Rigidbody>();
	}

}
