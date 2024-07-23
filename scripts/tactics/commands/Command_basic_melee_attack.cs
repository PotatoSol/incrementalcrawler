using Godot;
using System;
using System.Collections.Generic;

public partial class Command_basic_melee_attack : Command
{
	Crawler user;
	Crawler target;

	float maxRange = 3.0f; 
	float minRange = 0.0f;
	
	public void SetUser(Crawler inputUser){
		user = inputUser;
	}
	
	public void SetTarget(Crawler inputTarget){
		target = inputTarget;
	}

	public void SetMaxRange(float newMaxRange){
		maxRange = newMaxRange;
	}

	public void SetMinRange(float newMinRange){
		minRange = newMinRange;
	}
	
	public override Boolean IsValid() { 
		Vector3 userPos = user.GlobalPosition;
		Vector3 targetPos = target.GlobalPosition;
		
		float distance = userPos.DistanceTo(targetPos);
		if(distance <= maxRange && distance >= minRange){
			return true;
		} else return false;
		
	}

	public override void Execute(){
		//do a basic attack
	}
}
