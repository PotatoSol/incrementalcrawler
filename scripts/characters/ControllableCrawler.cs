using Godot;
using System;
using System.Collections.Generic;

public partial class ControllableCrawler : Crawler
{
	string state = "idle";
	private Vector3 _rotateY = new Vector3(0, 0, 0);
	public override void _Ready(){
		base._Ready();
		Condition_closest_target aCondition = new Condition_closest_target(this); //testing, delete this
		Command_basic_melee_attack aCommand = new Command_basic_melee_attack(); //testing, delete this
		Tactic initialTactic = new Tactic();
		initialTactic.SetCondition(aCondition);
		initialTactic.SetCommand(aCommand);
		
		tacticQueue.Add(initialTactic);
		AddToGroup("crawlers");
	}
	public override void _PhysicsProcess(double delta){
		Vector3 velocity = Velocity;
			// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()){
			velocity.Y = JumpVelocity;
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Transform3D transform = Transform; 
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
}
