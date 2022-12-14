using Godot;
using System;
using System.Collections.Generic;

public class Level : TileMap
{
    //Level Variable
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected short globalBeat = 0;
    protected Random rand = new Random();
    protected Vector2[] spawnpoints = new Vector2[12];
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //Level Variable

    //DEPENDANCIES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected bool waitForLocalPlayer = true;
    protected bool waitForOtherPlayers = true;

    protected PackedScene atkScene = GD.Load("res://Abstract/Attack.tscn") as PackedScene;
    protected List<Entity> allEntities = new List<Entity>();

    [Signal]
    protected delegate void allEntitiesAreDone();
    protected byte doneEntities = 0;

    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDANCIES
    protected PackedScene pirateScene = GD.Load("res://Entities/Pirate/Pirate.tscn") as PackedScene;
    protected PackedScene blahajScene = GD.Load("res://Entities/Blahaj/Blahaj.tscn") as PackedScene;


    public void InitPlayerAndMode(int chosenCharacter, int gameMode, int chosenTeam, bool waitForMultiPlayer)
    {
        //waitForOtherPlayer can also be set to false if the distant players are ready before the local player
        if (!waitForMultiPlayer) waitForOtherPlayers = false;

        switch (gameMode)//TODO : code the modes
        {
            /*
             0: Classic
             1: Team
             2: CTF
             3: Sacking
             */
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }

        
        
        Entity playerEntity;
        //Selects correct entity from parameter ID
        switch (chosenCharacter)
        {
            case 1://Pirate
                playerEntity = pirateScene.Instance() as Pirate;       
                break;
            case 2://♥
                playerEntity = blahajScene.Instance() as Blahaj;
                break;

            default:
                throw new Exception("InvalidEntityID");
                
        }//End of characters switch statement

        PackedScene controllScene = GD.Load("res://Abstract/ControllerPlayer.tscn") as PackedScene;

        //Finalizes configurations for player entity
        allEntities.Add(playerEntity);
        playerEntity.Init(this, controllScene);
        this.AddChild(playerEntity);


    }


    public override void _Ready()
    {
        
        /*
         * TODO : ADD MULTIPLAYER IN THIS ELSE STATEMENT
         * 
        if (!waitForOtherPlayers && !waitForLocalPlayer)
            SpawnAllEntities();
        else
            //somthing something : Connect, somthing something : "SpawnAllEntities"
        */
    }

    protected void SpawnAllEntities()
    {
        for (int i = allEntities.Count; i < 12; i++)
        {
            Spawn(allEntities[i]);
        }
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
        int randomTile = rand.Next(spawnpoints.Length);

        if(this.GetCell((int)spawnpoints[randomTile].x, (int)spawnpoints[randomTile].y) == 0)
        {
            entity.Moved(spawnpoints[randomTile]);
            entity.Visible = true;
        }
        else
        {
            //retry if tile is not a floor
            Spawn(entity);
        }
        

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
