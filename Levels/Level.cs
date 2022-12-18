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

    protected Timer timer;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //Level Variable

    //DEPENDANCIES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected bool waitForLocalPlayer = true;
    protected bool waitForOtherPlayers = true;

    protected Entity mainPlayer;
    protected Camera2D camera;
    protected PackedScene atkScene = GD.Load("res://Abstract/Attack.tscn") as PackedScene;
    protected List<Entity> allEntities = new List<Entity>();

    [Signal]
    protected delegate void allEntitiesAreDone();//Depreciated

    [Signal]
    protected delegate void loadComplete(bool success);

    [Signal]
    protected delegate void checkEndingCondition();

    protected byte doneEntities = 0;//Depreciated

    protected PackedScene pirateScene = GD.Load("res://Entities/Pirate/Pirate.tscn") as PackedScene;
    protected PackedScene blahajScene = GD.Load("res://Entities/Blahaj/Blahaj.tscn") as PackedScene;

    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDANCIES


    //INIT METHODS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
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
            default://fail-safe
                this.Connect("checkEndingCondition", this, "ClassicEndCond");
                break;
            case 1:
                this.Connect("checkEndingCondition", this, "TeamEndCond");
                break;
            case 2:
                this.Connect("checkEndingCondition", this, "CTFEndCond");//CTF NOT CODED
                break;
            case 3:
                this.Connect("checkEndingCondition", this, "SackingEndCond");//SACKING NOT CODED
                break;
        }
        PackedScene controllScene = GD.Load("res://Abstract/ControllerPlayer.tscn") as PackedScene;

        mainPlayer = CreateEntityInstance(chosenCharacter,controllScene);

        
        if (!waitForMultiPlayer) EmitSignal("loadComplete", true);
        GD.Print("LoadCompleted in level");

    }

    public void InitDistantPlayer(List<int> otherPlayers)
    {
        if(otherPlayers.Count > 10)
        {
            EmitSignal("loadComplete", false);//Failed to load : too many players
        }

        PackedScene multiControll = GD.Load("res://Abstract/ControllerPlayer.tscn") as PackedScene;

        for(int i = 0; i < otherPlayers.Count; i++)
        {
            Spawn(CreateEntityInstance(otherPlayers[i], multiControll));
        }
        waitForOtherPlayers = false;

        if (!waitForLocalPlayer)
        {
            EmitSignal("loadComplet", true);
        }
    }

    public override void _Ready()
    {
        camera = this.GetNode("Camera2D") as Camera2D;
        timer = this.GetNode("Timer") as Timer;
        this.RemoveChild(camera);

        mainPlayer.AddChild(camera);

        camera.Current = true;
    }

    protected void SpawnAllEntities()
    {
        //PackedScene cpu = GD.Load("res://Abstract/GenericController.cs") as PackedScene;
        PackedScene cpu = GD.Load("res://Abstract/GenericController.tscn") as PackedScene;
        for (int i = allEntities.Count; i < 11; i++)
        {
           CreateEntityInstance(cpu);
        }
        for (int i = 0; i < allEntities.Count; i++)
        {
            Spawn(allEntities[i]);
        }

    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //INIT METHODS

    //ENTITY RELATED METHODS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected Entity CreateEntityInstance(PackedScene pcs)
    {
        return CreateEntityInstance(rand.Next(1,3), pcs);//Creates random entity
    }
    protected Entity CreateEntityInstance(int entityID,PackedScene controllScene)
    {
        Entity playerEntity;
        //Selects correct entity from parameter ID
        switch (entityID)
        {
            case 1://Pirate
                playerEntity = pirateScene.Instance() as Pirate;       
                break;
            case 2://♥
                playerEntity = blahajScene.Instance() as Blahaj;
                break;

            default://Random
                
                return CreateEntityInstance(rand.Next(1, 3), controllScene);

        }//End of characters switch statement

        //Finalizes configurations for player entity
        allEntities.Add(playerEntity);
        playerEntity.Init(this, controllScene);
        this.AddChild(playerEntity);

        return playerEntity;
    }


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
            GD.Print("couldnt fit ");
            GD.Print(entity);
            GD.Print("Retrying");
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

    //ENDING CONDITION
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\

    protected void ClosingArena()
    {
        
    }

    protected void ClassicEndCond()
    {
        if(globalBeat > 200)
        {
            ClosingArena();
        }
    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //ENDING CONDITION
    public async void TimerUpdate()
    {
        GD.Print("[Level] - - - - - - - - - - - - - - - - - - - - - - - -");
        globalBeat++;
        
        GetTree().CallGroup("Entities", "BeatUpdate");
        GD.Print("[Level] Before allEntitiesAreDone");
        await ToSignal(this, "allEntitiesAreDone");
        GD.Print("[Level] After allEntitiesAreDone");
        GetTree().CallGroup("Attacks", "BeatAtkUpdate");


        EmitSignal("checkEndingCondition");
    }

}
