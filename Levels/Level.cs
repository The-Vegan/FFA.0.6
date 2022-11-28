using Godot;
using System;
using System.Collections.Generic;

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
    protected List<Entity> allEntities = new List<Entity>();

    [Signal]
    protected delegate void allEntitiesAreDone();
    protected byte doneEntities = 0;

    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDANCIES

    public override void _Ready()
    {
        //DEBUG (REMOVE LATER)
        //______________________________________
        spawnpoints[0] = new Vector2(0, 0);
        spawnpoints[1] = new Vector2(0, 0);

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
        PackedScene blahajScene = GD.Load("res://Entities/Blahaj/Blahaj.tscn") as PackedScene;
        
        Entity pirate = blahajScene.Instance() as Entity;
        pirate.Init(this, controllScene);
        this.AddChild(pirate);
        Spawn(pirate);
        allEntities.Add(pirate);

        //STATIC ENTITY INSTANCING

        //______________________________________
        //DEBUG (REMOVE LATER)
    }

    //ENTITY RELATED METHODS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    public void MoveEntity(Entity entity,Vector2 newTile)
    {
        //Checks if tile is walkable
        if (this.GetCell((int)(newTile.x),(int)(newTile.y)) == 0)
        {
            entity.Moved(newTile);
        }
        else
        {
            entity.Moved(entity.pos);
        }
    }

    public void Spawn(Entity entity)
    {
        entity.Moved(spawnpoints[rand.Next(10)]);
        entity.Visible = true;
        

    }

    protected void EntityDone()//Signal method : is triggered when an entity has finished it's BeatUpdate coroutine
    {
        doneEntities++;
        if (doneEntities >= allEntities.Count) EmitSignal("allEntitiesAreDone");
    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //ENTITY RELATED METHODS

    //ATTACK RELATED METHODS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    public void CreateAtk(Entity source, List<List<Dictionary<String, short>>> atkData, String path, byte[] collumns, bool flipable)
    {
        Attack atkInstance = atkScene.Instance() as Attack;
        atkInstance.InitAtk(source, atkData, this, path, collumns, flipable);
        this.AddChild(atkInstance);
    }


    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //ATTACK RELATED METHODS
    public async void TimerUpdate()
    {
        GD.Print("[Level] - - - - - - - - - - - - - - - - - - - - - - - -");
        globalBeat++;
        
        GetTree().CallGroup("Entities", "BeatUpdate");
        GD.Print("[Level] Before allEntitiesAreDone");
        await ToSignal(this, "allEntitiesAreDone");
        GD.Print("[Level] After allEntitiesAreDone");
        GetTree().CallGroup("Attacks", "BeatAtkUpdate");
    }

}
