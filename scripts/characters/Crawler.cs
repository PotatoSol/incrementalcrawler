using Godot;
using System;
using System.Collections.Generic;

public partial class Crawler : CharacterBody3D{
	
	public List<Tactic> tacticQueue = new List<Tactic>(); //list of all the tactics
	public int current_state = 0; 
	/*
	Find a better way to do this:
	1 = idle;
	*/
	
	public String name = "John Doe";
	
	//first stat is primary, second is secondary - i.e. strength is prmiary, char
	/*
	STRength determines max phys hit and health, CHArisma determins phys acc and health
	WISdom determines spellcast speed and acc and mana // INTelligence determines max mag hit and mana
	DEX determines num of phys hits and attack/movement speed //LUcK determines phys crit chance and other luck based rolls
	Health (HP) = STR * CHA // Mana (MP) = WIS * INT
	ARMor determins defense against PHYsical attacks // RESistance determines defense against MAGical attacks
	*/
	public double strength = 10.0, charisma = 10.0, wisdom = 10.0, intelligence = 10.0, dexterity = 10.0, luck = 10.0, health = 100.0, mana = 100.0, armor = 10.0, resistance = 10.0; 
	public float JumpVelocity = 10.0f, Speed = 10.0f;
	//recalc later
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle() * 2f; // Get the gravity from the project settings to be synced with RigidBody nodes.

	//Adds a command to the tactic queue
	public virtual void AddTactic(Tactic inputTactic){
		tacticQueue.Add(inputTactic);
	}

	public virtual float GetDistance(CharacterBody3D target){
		float returnDistance = GlobalPosition.DistanceTo(target.GlobalPosition);
		return returnDistance;
	}
	
	//Moves towards a Coordinate
	public void MoveToward(float x, float y){
		Vector3 velocity = Velocity;
		velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
	}
	
	public override void _Ready(){
		Tactic engageTactic = new Tactic();
		Command_engage engageCommand = new Command_engage(this);
		Condition_closest_target engageCondition = new Condition_closest_target(this);
		engageTactic.SetCommand(engageCommand);
		engageTactic.SetCondition(engageCondition);
	}

	public override void _Process (double delta){
		foreach(Tactic tact in tacticQueue){
			if(tact.IsValid()){
				tact.DoCommand();
				GD.Print("help?");
				break;
			}
		}
	}
	
	//handles physics processes and inputs - delete inputs when no longer necessary
	/*
	public override void _PhysicsProcess(double delta){
		Vector3 velocity = Velocity;
			// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()){
			velocity.Y = JumpVelocity;
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Transform3D transform = Transform;
		GD.Print(transform.Basis);
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero){
			float angle = Mathf.Atan2(direction.X, direction.Z);
			Quaternion target = new Quaternion(Vector3.Up, angle);
			Quaternion interp = transform.Basis.GetRotationQuaternion().Slerp(target, .3f);
			transform.Basis = new Basis(interp);
			
			//transform = transform.LookingAt(new Vector3(direction.X, -direction.Y, direction.Z), Vector3.Up);

			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		} else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}
			// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y -= gravity * (float)delta;
		}
		Transform = transform;
		Velocity = velocity;
		MoveAndSlide();
	}
	*/
}
