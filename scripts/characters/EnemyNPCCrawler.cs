using Godot;
using System;

public partial class EnemyNPCCrawler: Crawler
{
	
	public override void _Ready(){
		base._Ready();
		
		AddToGroup("enemies");
	}

	public override string ToString(){
		return this.Name;
	}

}
