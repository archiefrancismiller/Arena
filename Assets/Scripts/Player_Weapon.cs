// P L A Y E R _ W E A P O N
// Handles weapon behavior
// Attach to the Holder, which should be a child of the Main Camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
	// Create the weapon array
	public GameObject[] weapons;

	// The currently selected weapon
	private int equippedWeapon;

	void Start ()
	{

	}

	void Update()
	{
		// On Swap input, call SwapWeapon
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwapWeapon(1);
			Debug.Log ("Pistol");
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwapWeapon(2);
			Debug.Log ("Machine Pistol");
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwapWeapon(3);
			Debug.Log ("Carbine");
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			SwapWeapon(4);
			Debug.Log ("Battle Rifle");
		}
	}
		
	public void SwapWeapon(int num)
	{
		equippedWeapon = num;
	}

	public void InstantiateWeapon()
	{
		// Destroy currently equipped weapon
		// Instantiate newly selected weapon
	}

	// Pistol
	void Pistol() {
	
	}

	// Machine pistol
	void MachinePistol(){
	
	}

	// Carbine
	void Carbine() {
	
	}

	// Battle rifle
	void BattleRifle() {
	
	}
}
