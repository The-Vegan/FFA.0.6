using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Entity : AnimatedSprite
{


    //DEPENDENCIES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected PackedScene controllerScene;
    protected GenericController controller;
    protected Level map;
    protected Tween tween;
    protected AnimationPlayer animPlayer;

    [Signal]
    delegate void entityIsDone();
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDENCIES

    //MOVEMENT RELATED
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected short packet;
    public Vector2 pos = new Vector2(-1,-1);
    public Vector2 prevPos;

    protected byte stun = 0, cooldown = 3;

    [Signal]
    delegate void movedByLevel(bool true_tho);
    protected bool reallyMoved = false;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //MOVEMENT RELATED

    //ATTACK VARIABLES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected String atkFolder;
    protected byte[] animPerBeat;
    protected bool flippableAnim = false;

    protected List<List<Dictionary<String, short>>> DOWNATK = new List<List<Dictionary<String, short>>>();
    protected List<List<Dictionary<String, short>>> LEFTATK = new List<List<Dictionary<String, short>>>();
    protected List<List<Dictionary<String, short>>> RIGHTATK = new List<List<Dictionary<String, short>>>();
    protected List<List<Dictionary<String, short>>> UPATK = new List<List<Dictionary<String, short>>>();
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //ATTACK VARIABLES

    //ANIMATION
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    bool damaged = false;
    protected String direction = "Down";
    protected String action;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //ANIMATION


    public void Init(Level level,PackedScene c)
    {
        
        animPlayer = (AnimationPlayer)this.GetNode("AnimationPlayer");
        map = level;
        controllerScene = c;

        this.Connect("movedByLevel", this, "SetRealyMoved");
        this.Connect("entityIsDone", map, "EntityDone");
    }
    async public override void _Ready()
    {
        tween = (Tween)this.GetNode("Tween");
        await ToSignal(GetTree(), "idle_frame");
        controller = controllerScene.Instance() as GenericController;
        this.AddChild(controller);
    }


    //GESTION DES INPUTS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    public void SetPacket(short p)
    {
        this.packet |= p;
    }

    protected virtual short PacketParser(short packetToParse)
    {
        //Rest
        if (packetToParse >= 512) return 512;

        short parsedPacket = 0;
        bool item = false;
        if (packetToParse >= 256) item = true;

        if      ((packetToParse & 0b0000_0001) != 0) parsedPacket = 1;
        else if ((packetToParse & 0b0000_0010) != 0) parsedPacket = 2;
        else if ((packetToParse & 0b0000_0100) != 0) parsedPacket = 4;
        else if ((packetToParse & 0b0000_1000) != 0) parsedPacket = 8;
        else if ((packetToParse & 0b0001_0000) != 0) parsedPacket = 16;
        else if ((packetToParse & 0b0010_0000) != 0) parsedPacket = 32;
        else if ((packetToParse & 0b0100_0000) != 0) parsedPacket = 64;
        else if ((packetToParse & 0b1000_0000) != 0) parsedPacket = 128;

        if (item)
        {
            if (parsedPacket >= 16) parsedPacket >>= 4;//offsets attack into move
            parsedPacket |= 256;//enable item flag
        }

        return parsedPacket;
    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //GESTION DES INPUTS

    //GESTION DES MOUVEMENTS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected void AskMovement()
    {
        packet = PacketParser(packet);

        //if none of movement bits are set to true
        if ((packet & 0b1111) == 0) return;

        if      ((packet & 0b0001) != 0) map.MoveEntity(this, pos + Vector2.Down);
        else if ((packet & 0b0010) != 0) map.MoveEntity(this, pos + Vector2.Left);
        else if ((packet & 0b0100) != 0) map.MoveEntity(this, pos + Vector2.Right);
        else if ((packet & 0b1000) != 0) map.MoveEntity(this, pos + Vector2.Up);
    }

    protected virtual void AskAtk()
    {
        if ((packet & 0b1111_0000) == 0) return;
        action = "Atk";
        if      ((packet & 0b0001_0000) != 0) map.CreateAtk(this, DOWNATK, atkFolder + "DownAtk", animPerBeat, flippableAnim);
        else if ((packet & 0b0010_0000) != 0) map.CreateAtk(this, LEFTATK, atkFolder + "LeftAtk", animPerBeat, flippableAnim);
        else if ((packet & 0b0100_0000) != 0) map.CreateAtk(this, RIGHTATK, atkFolder + "RightAtk", animPerBeat, flippableAnim);
        else if ((packet & 0b1000_0000) != 0) map.CreateAtk(this, UPATK, atkFolder + "UpAtk", animPerBeat, flippableAnim);
    }

    public void Moved(Vector2 newTile)
    {
        if (pos == newTile) 
            EmitSignal("movedByLevel", false);
        else 
            EmitSignal("movedByLevel", true);


        map.SetCell((int)pos.x, (int)pos.y, 0);
        
        pos = newTile;
        map.SetCell((int)pos.x, (int)pos.y, 3);
        action = "Idle";

        tween.InterpolateProperty(this, "position",                          //Property to interpolate
            this.Position, new Vector2((pos.x * 64) + 32, (pos.y * 64) + 16),//initVal,FinalVal
            0.33f,                                                           //Duration
            Tween.TransitionType.Sine, Tween.EaseType.Out);                  //Tween says Trans rights
        tween.Start();
        animPlayer.Play("Move");
        

    }
    protected void SetRealyMoved(bool didItTho)
    {
        reallyMoved = didItTho;
    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //GESTION DES MOUVEMENTS

    //GESTION DES ANIMATIONS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    private async void MidBeatAnimManager()
    {
        await ToSignal(this, "animation_finished");
        if (damaged) 
        {
            this.Play("Damaged");
            damaged = false;
        }
        else
        {
            switch (action)
            {
                case "Idle":
                case "Cooldown":
                    action = "Wait";
                    break;
                case "Atk":
                    action = "Cooldown";
                    break;
                    
            }



            this.Play(action + direction);
        }

    }

    private void OnBeatAnimManager()
    {




    }

    private void DirectionSetter()
    {
        if      ((packet & 0b0001) != 0) direction = "Down";
        else if ((packet & 0b0010) != 0) direction = "Left";
        else if ((packet & 0b0100) != 0) direction = "Right";
        else if ((packet & 0b1000) != 0) direction = "Up";
        else if ((packet & 0b0001_0000) != 0) direction = "Down";
        else if ((packet & 0b0010_0000) != 0) direction = "Left";
        else if ((packet & 0b0100_0000) != 0) direction = "Right";
        else if ((packet & 0b1000_0000) != 0) direction = "Up";
    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //GESTION DES ANIMATIONS



    protected void BeatUpdate()
    {
        if (stun != 0) { cooldown = 0; stun--; return; }
        if (cooldown != 0)
        {
            cooldown--;
            return;
        }

        DirectionSetter();
        
        if(packet == 0)
        {
            this.Play("FailedInput");
            action = "Idle";
            MidBeatAnimManager();
            return;
        }
        //If the player moves, AskMovement returns true and skips AskAtk
        //This structure is meant to simplify the override for Pirate
        AskMovement();
        AskAtk();
        

        this.Play(action + direction);
        MidBeatAnimManager();

        //VALUES RESETS
        //>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<
        packet = 0;
        //>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<
        //VALUES RESETS
    }//End of BeatUpdate

}
