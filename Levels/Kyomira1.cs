using Godot;
using System;

public class Kyomira1 : Level
{
    
    public override void _Ready()
    {
        spawnpoints[0] = new Vector2(12, 6);
        spawnpoints[1] = new Vector2(23, 6);

        spawnpoints[2] = new Vector2(6, 12);
        spawnpoints[3] = new Vector2(29, 12);

        spawnpoints[4] = new Vector2(13, 17);
        spawnpoints[5] = new Vector2(22, 18);

        spawnpoints[6] = new Vector2(6, 23);
        spawnpoints[7] = new Vector2(29, 23);

        spawnpoints[8] = new Vector2(12, 29);
        spawnpoints[9] = new Vector2(23, 29);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
