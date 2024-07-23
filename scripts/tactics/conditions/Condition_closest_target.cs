using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

//depreciated

//finds the closest target to the user, can set parameters
public class Condition_closest_target : Condition
{	
	private float minRange, maxRange;
	private CharacterBody3D user, closestTarget;
	private float closestDistance = -1;
	
	public Condition_closest_target(CharacterBody3D inputUser){ //defaults to something within 10m.
		user = inputUser;
		minRange = 0.0f;
		maxRange = 10.0f;
	}
	
	public Condition_closest_target(CharacterBody3D inputUser, float inputMin, float inputMax){
		user = inputUser;
		closestTarget = user;
		minRange = inputMin;
		maxRange = inputMax;
	}
	
	/* Should be unneeded, can probably be deleted
	public void SetUser(CharacterBody3D inputUser){
		user = inputUser;
	}
	*/
	
	//Returns distance between targets
	public float GetDistanceBetween(CharacterBody3D inputUser, CharacterBody3D target){
		float output = (float) Math.Sqrt(Math.Pow(inputUser.GlobalPosition[0] - target.GlobalPosition[0], 2) + Math.Pow(inputUser.GlobalPosition[2] - target.GlobalPosition[2], 2));
		return output;
	}
	
	public void Calculate(){
		var targetables = ((SceneTree)Engine.GetMainLoop()).GetNodesInGroup("crawlers").Where(x => x is Crawler).ToArray();
		foreach(Crawler aCrawler in targetables){
			float dist = GetDistanceBetween(user, aCrawler);
			if(user == aCrawler){
				continue;
			}
			if(dist >= minRange && dist <= maxRange){
				if(dist < closestDistance || closestDistance == -1 || closestTarget == user){				
					closestDistance = dist;
					closestTarget = aCrawler;
				}
			}	
		}
		if(GetDistanceBetween(user, closestTarget) > maxRange || GetDistanceBetween(user, closestTarget) < minRange){
			closestTarget = user;
			closestDistance = -1;
		}
	}
	
	public float GetClosestDistnace(){
		return closestDistance;
	}
	
	public override bool IsValid(){	
		Calculate();
		if(closestTarget != user && closestTarget != null){
			return true;
		} else return false;
	}
}
