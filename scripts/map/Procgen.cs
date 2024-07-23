using Godot;
using System;


public partial class procgen : Node
{
  int max_x = 16;
  int max_y = 16;

  public void Gen(){
    var rand = new Random();
    int start_x = (rand.Next(2) + 1) & max_x;
    int start_y = (rand.Next(2) + 1) * max_y;
    int[] start_coords = new int[]{start_x, start_y};

  }

}
