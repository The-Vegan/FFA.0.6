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

        spawnpoints[4] = new Vector2(18, 13);
        spawnpoints[5] = new Vector2(13, 17);

        spawnpoints[6] = new Vector2(22, 18);
        spawnpoints[7] = new Vector2(17, 22);

        spawnpoints[8] = new Vector2(6, 23);
        spawnpoints[9] = new Vector2(29, 23);

        spawnpoints[10] = new Vector2(12, 29);
        spawnpoints[11] = new Vector2(23, 29);

        

        //DEBUG (REMOVE LATER)
        //______________________________________
        SpawnAllEntities();
        //______________________________________
        //DEBUG (REMOVE LATER)

        base._Ready();//When debug is over, this will spawn all entities 
        // -/!\ NEEDS TO BE CALLED AFTER SpawnAllEntities
    }

}
