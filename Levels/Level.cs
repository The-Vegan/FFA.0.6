using Godot;
using System;

public class Level : TileMap
{
    //Level Variable
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected short globalBeat = 0;
    protected Random rand = new Random();
    protected Vector2[] spawnpoints = new Vector2[10];
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //Level Variable

    //DEPENDANCIES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected PackedScene atkScene = GD.Load("res://Abstract/Attack.tscn") as PackedScene;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDANCIES

    public override void _Ready()
    {
        //DEBUG (REMOVE LATER)
        //______________________________________
        spawnpoints[0] = Vector2.Zero;
        spawnpoints[1] = Vector2.Zero;

        spawnpoints[2] = new Vector2(15, 0);
        spawnpoints[3] = new Vector2(15, 0);

        spawnpoints[4] = new Vector2(0, 8);
        spawnpoints[5] = new Vector2(0, 8);

        spawnpoints[6] = new Vector2(15, 8);
        spawnpoints[7] = new Vector2(15, 8);

        spawnpoints[8] = new Vector2(7, 4);
        spawnpoints[9] = new Vector2(7, 4);


        //STATIC ENTITY INSTANCING
        PackedScene controllScene = GD.Load("res://Abstract/ControllerPlayer.tscn") as PackedScene;
        PackedScene pirateScene = GD.Load("res://Entities/Pirate/Pirate.tscn") as PackedScene;
        Pirate pirate = pirateScene.Instance() as Pirate;
        pirate.Init(this, controllScene);
        this.AddChild(pirate);
        Spawn(pirate);

        //STATIC ENTITY INSTANCING

        //______________________________________
        //DEBUG (REMOVE LATER)



    }

    public void MoveEntity(Entity entity,Vector2 newTile)
    {
        //Checks if tile is walkable
        if (this.GetCell((int)(newTile.x),(int)(newTile.y)) == 0)
        {
            entity.Moved(newTile);
        }


    }

    public void Spawn(Entity entity)
    {
        entity.Moved(spawnpoints[rand.Next(10)]);
        entity.Visible = true;
        

    }
    
    public void TimerUpdate()
    {
        globalBeat++;
        
        GetTree().CallGroup("Entities", "BeatUpdate");
    }

}
