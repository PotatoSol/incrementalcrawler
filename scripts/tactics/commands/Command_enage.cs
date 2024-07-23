using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Command_engage : Command {
  Crawler user;
  Crawler target = null;
  public Command_engage(Crawler inputUser, Crawler inputTarget){ //for use if there's a target we want to engage
    user = inputUser;
    target = inputTarget;
  }
  public Command_engage(Crawler inputUser){ //for use if there is no specific target, and we want to move towards exit
    user = inputUser;
  }

  float minRange = 0f;
  float maxRange = 3f;

  public void SetClosestEnemy(){
    //Finds the closest enemy
    //If there is no enemy (within sight, NOT RANGE), then move towards the closest exit 
  }

  public bool EnemyInRange(){
    var targetables = ((SceneTree)Engine.GetMainLoop()).GetNodesInGroup("crawlers").Where(x => x is Crawler).ToArray();
    foreach(Crawler enemy in targetables){
      var distance = user.GlobalPosition.DistanceTo(enemy.GlobalPosition);
      if(distance > minRange && distance < maxRange){
        if(target == null){
          target = enemy;
        } else if (distance < user.GlobalPosition.DistanceTo(target.GlobalPosition)){
          target = enemy;
        }
      }
    }

    return false;
  }
  public void SetMinRange(float inputMinRange){
    minRange = inputMinRange;
  }

  public void SetMaxRange(float inputMaxRange){
    maxRange = inputMaxRange;
  }

  public override Boolean IsValid() {
    return true;
  }
  public override void Execute(){
    if(target == null){
      //move to exit
    } else {
      Vector3 userPos = user.GlobalPosition;
      Vector3 targetPos = target.GlobalPosition;
      if(userPos.DistanceTo(targetPos) > maxRange){
        user.MoveToward(targetPos.X, targetPos.Y);
      }
    }
  }
}