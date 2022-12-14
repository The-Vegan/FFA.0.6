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
    protected bool firstInitiation = true;
    protected byte playerToWaitFor = 0;

    protected PackedScene atkScene = GD.Load("res://Abstract/Attack.tscn") as PackedScene;
    protected List<Entity> allEntities = new List<Entity>();

    [Signal]
    protected delegate void allEntitiesAreDone();
    protected byte doneEntities = 0;

    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDANCIES
    protected PackedScene pirateScene = GD.Load("res://Entities/Pirate/Pirate.tscn") as PackedScene;
    protected PackedScene blahajScene = GD.Load("res://Entities/Blahaj/Blahaj.tscn") as PackedScene;


    public void InitPlayerAndMode(int chosenCharacter,int gameMode,int chosenTeam)
    {
        //gameMode contains 
        if (firstInitiation)
        {
            firstInitiation = false;
            int mode = (gameMode - (gameMode % 10)) / 10; //devide by 10 and floors the result

            switch (mode)
            {
                /*
                 0: Classic
                 1: Team
                 2: CTF
                 */
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

            playerToWaitFor =(byte) ((gameMode % 10) + 1);
            
        }//Only enters when initialized for the forst time

        String controllerScenePath;

        if((chosenTeam & 0b1_00) == 0)
        {
            controllerScenePath = "res://Abstract/ControllerPlayer.tscn";
        }
        else
        {
            //^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^\\
            //MULTIPLAYER CONTROLLER NOT YET CODED : HERE USING SINGLEPLAYER CONTROLLER
            controllerScenePath = "res://Abstract/ControllerPlayer.tscn";
            //v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^v^V\\
            //controllerScenePath = "res://Abstract/ControllerMultiplayer.tscn";
        }
        PackedScene controllScene = GD.Load(controllerScenePath) as PackedScene;
        switch (chosenCharacter)
        {
            case 1://Pirate
                //Instancing pirate
                Pirate pirate = pirateScene.Instance() as Pirate;
                //Add pirate to Entity list
                allEntities.Add(pirate);
                //Gives pirate his controller
                pirate.Init(this, controllScene);
                //Adds the pirate to the scene tree
                this.AddChild(pirate);

                //Reduces Player counter (when 0, fills room with CPUs)
                playerToWaitFor--;
                break;
            case 2://♥
                Blahaj blahaj = pirateScene.Instance() as Blahaj;
                allEntities.Add(blahaj);
                blahaj.Init(this, controllScene);
                this.AddChild(blahaj);
                playerToWaitFor--;
                break;

            default:
                break;
        }//End of characters switch statement
        
        if(playerToWaitFor <= 0)
        {
            controllerScenePath = "res://Abstract/ControllerCpu.tscn";
            while (allEntities.Count < 10)
            {
                switch (rand.Next(2)+1)
                {
                    case 1:
                        Pirate pirate = pirateScene.Instance() as Pirate;
                        allEntities.Add(pirate);
                        pirate.Init(this, controllScene);
                        this.AddChild(pirate);
                        break;
                    case 2:
                        Blahaj blahaj = pirateScene.Instance() as Blahaj;
                        allEntities.Add(blahaj);
                        blahaj.Init(this, controllScene);
                        this.AddChild(blahaj);
                        break;

                    default:
                        break;
                }
            }
        }



    }


    public override void _Ready()
    {
        //DEBUG (REMOVE LATER)
        //______________________________________
        spawnpoints[0] = new Vector2(0, 0);
        spawnpoints[1] = new Vector2(1, 0);

        spawnpoints[2] = new Vector2(15, 0);
        spawnpoints[3] = new Vector2(14, 0);

        spawnpoints[4] = new Vector2(0, 8);
        spawnpoints[5] = new Vector2(1, 8);

        spawnpoints[6] = new Vector2(15, 8);
        spawnpoints[7] = new Vector2(14, 8);

        spawnpoints[8] = new Vector2(7, 4);
        spawnpoints[9] = new Vector2(7, 5);

        //______________________________________
        //DEBUG (REMOVE LATER)

        //SpawningEntities
        for(byte i = 0; i < allEntities.Count; i++)
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
        int randomTile = rand.Next(10);

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
