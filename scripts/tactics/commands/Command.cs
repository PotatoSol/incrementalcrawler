using Godot;
using System;
using System.Collections.Generic;

public abstract class Command
{
	public abstract void Execute(); //executes the command

	public abstract Boolean IsValid(); //returns true if the command is valid, false otherwise
}
