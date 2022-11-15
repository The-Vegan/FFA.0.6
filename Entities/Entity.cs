using Godot;
using System;

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

    //ANIMATION
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    bool damaged = false;
    protected String Direction = "Down";
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

    protected short PacketParser(short packetToParse)
    {
        //Rest
        if (packetToParse >= 512) return 512;

        short parsedPacket = 0;
        bool item = false;
        if (packetToParse >= 256) item = true;

        if ((packetToParse & 0b0000_0001) != 0) parsedPacket = 1;
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

        

        if      ((packet & 0b0001) != 0) map.MoveEntity(this, pos + Vector2.Down);
        else if ((packet & 0b0010) != 0) map.MoveEntity(this, pos + Vector2.Left);
        else if ((packet & 0b0100) != 0) map.MoveEntity(this, pos + Vector2.Right);
        else if ((packet & 0b1000) != 0) map.MoveEntity(this, pos + Vector2.Up);
        
    }

    public void Moved(Vector2 newTile)
    {

        
        pos = newTile;

        this.Play("Idle" + Direction);
        
        MidBeatAnimManager();

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
            this.Play("Wait" + Direction);
        }

    }
    private void DirectionSetter()
    {
        if      ((packet & 0b0001_0001) != 0) Direction = "Down";
        else if ((packet & 0b0010_0010) != 0) Direction = "Left";
        else if ((packet & 0b0100_0100) != 0) Direction = "Right";
        else if ((packet & 0b1000_1000) != 0) Direction = "Up";
    }
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //GESTION DES ANIMATIONS



    protected void BeatUpdate()
    {
        if (stun != 0) { cooldown = 0; stun--; return; }
        if (cooldown != 0){cooldown--;return;}

        DirectionSetter();

        if ((packet & 0b1111) != 0)//check for movement
        {
            AskMovement();
        }





        //VALUES RESETS
        //>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<
        packet = 0;
        //>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<
        //VALUES RESETS
    }//End of BeatUpdate

}
