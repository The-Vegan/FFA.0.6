using Godot;
using System;
using System.Collections.Generic;

public class Entity : AnimatedSprite
{


    //DEPENDENCIES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected PackedScene controllerScene;
    protected GenericController controller;
    protected Level map;
    protected Tween tween;
    protected AnimationPlayer animPlayer;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDENCIES

    //MOVEMENT RELATED
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected short packet;
    public Vector2 pos;
    public Vector2 prevPos;

    protected byte stun = 0, cooldown = 3;

    
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
    protected bool AskMovement()
    {
        packet = PacketParser(packet);

        //if none of movement bits are set to true
        if ((packet & 0b1111) == 0) return false;

        if ((packet & 0b0001) != 0) map.MoveEntity(this, pos + Vector2.Down);
        else if ((packet & 0b0010) != 0) map.MoveEntity(this, pos + Vector2.Left);
        else if ((packet & 0b0100) != 0) map.MoveEntity(this, pos + Vector2.Right);
        else if ((packet & 0b1000) != 0) map.MoveEntity(this, pos + Vector2.Up);

        return true;
    }

    protected virtual void AskAtk(bool hasAlreadyMoved)
    {
        //Only the AskMovement method may be used as parameter
        if (hasAlreadyMoved) return;

        action ="Atk";
        if      ((packet & 0b0001_0000) != 0) map.CreateAtk(this, DOWNATK,atkFolder + "DownAtk",animPerBeat,flippableAnim);
        else if ((packet & 0b0010_0000) != 0) map.CreateAtk(this, LEFTATK, atkFolder + "LeftAtk", animPerBeat, flippableAnim);
        else if ((packet & 0b0100_0000) != 0) map.CreateAtk(this, RIGHTATK, atkFolder + "RightAtk", animPerBeat, flippableAnim);
        else if ((packet & 0b1000_0000) != 0) map.CreateAtk(this, UPATK, atkFolder + "UpAtk", animPerBeat, flippableAnim);

    }

    public void Moved(Vector2 newTile)
    {
        map.SetCell((int)pos.x, (int)pos.y, 0);
        
        pos = newTile;
        map.SetCell((int)pos.x, (int)pos.y, 3);
        action = "Idle";

        tween.InterpolateProperty(this, "position",                         //Property to interpolate
            this.Position, new Vector2((pos.x * 64) + 32, (pos.y * 64) + 16),//initVal,FinalVal
            0.33f,                                                          //Duration
            Tween.TransitionType.Sine, Tween.EaseType.Out);                  //Tween says Trans rights
        tween.Start();
        animPlayer.Play("Move");

    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //GESTION DES MOUVEMENTS

    //GESTION DES ANIMATIONS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    async private void MidBeatAnimManager()
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
        if      ((packet & 0b0001_0001) != 0) direction = "Down";
        else if ((packet & 0b0010_0010) != 0) direction = "Left";
        else if ((packet & 0b0100_0100) != 0) direction = "Right";
        else if ((packet & 0b1000_1000) != 0) direction = "Up";
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
        GD.Print(packet);//--------------------------------------------------------------
        if(packet == 0)
        {
            this.Play("FailedInput");
            action = "Idle";
            MidBeatAnimManager();
            return;
        }
        //If the player moves, AskMovement returns true and skips AskAtk
        //This structure is meant to simplify the override for Pirate
        AskAtk(AskMovement());
        

        this.Play(action + direction);
        MidBeatAnimManager();

        //VALUES RESETS
        //>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<
        packet = 0;
        //>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<
        //VALUES RESETS
    }//End of BeatUpdate

}
