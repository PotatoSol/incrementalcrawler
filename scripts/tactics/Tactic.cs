using Godot;
using System;
using System.Collections.Generic;

public class Tactic
{
	Condition Cond;
	Command Comm;
	
	
	//Debug
	public String SayMoo(){
		return "Moo";
	}
	
	public void SetCommand(Command inputCommand){
		Comm = inputCommand;
		
	}
	
	public void SetCondition(Condition inputCondition){
		Cond = inputCondition;
	}
	
	public bool IsValid(){
		return Cond.IsValid();
	}
	
	public void DoCommand(){
		Comm.Execute();
	}
	
}
