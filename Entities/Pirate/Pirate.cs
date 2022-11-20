using Godot;
using System;
using System.Collections.Generic;

public class Pirate : Entity
{
    /// A FAIRE
    /// RESOUDRE BUG DE ATKMOVE
    /// A FAIRE
    private List<List<Dictionary<String, short>>> DOWNMOVEATK = new List<List<Dictionary<string, short>>>();
    private List<List<Dictionary<String, short>>> LEFTMOVEATK = new List<List<Dictionary<string, short>>>();
    private List<List<Dictionary<String, short>>> RIGHTMOVEATK = new List<List<Dictionary<string, short>>>();
    private List<List<Dictionary<String, short>>> UPMOVEATK = new List<List<Dictionary<string, short>>>();

    public override void _Ready()
    {
        base._Ready();

        this.atkFolder = "res://Entities/Pirate/atk/";
        

        this.animPerBeat = new byte[] {5};
        {//Declare downAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X", -1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 0 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });

            DOWNATK.Add(frame1);
        }//Declare downAtkTiles
        {//Declare leftAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X", -1 },{ "Y", -1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", -1 },{ "Y", 0 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", -1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });

            LEFTATK.Add(frame1);
        }//Declare leftAtkTiles
        {//Declare rightAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();


            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", 0 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", -1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });


            RIGHTATK.Add(frame1);
        }//Declare rightAtkTiles
        {//Declare upAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", -1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 0 },{ "Y", -1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", -1 },{ "Y", -1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });


            UPATK.Add(frame1);
        }//Declare upAtkTiles
        {//Declare downMoveAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X",-1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-1 },{ "Y", 2 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 0 },{ "Y", 2 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", 2 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 3 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 4 } });

            DOWNMOVEATK.Add(frame1);
        }//Declare downMoveAtkTiles
        {//Declare leftMoveAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X",-1 },{ "Y",-1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-2 },{ "Y",-1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-2 },{ "Y", 0 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-2 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 3 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 4 } });

            LEFTMOVEATK.Add(frame1);
        }//Declare leftMoveAtkTiles
        {//Declare rightMoveAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 2 },{ "Y", 1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 2 },{ "Y", 0 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 2 },{ "Y",-1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 3 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y",-1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 4 } });

            RIGHTMOVEATK.Add(frame1);
        }//Declare rightMoveAtkTiles
        {//Declare upMoveAtkTiles

            List<Dictionary<String, short>> frame1 = new List<Dictionary<String, short>>();

            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y",-1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 0 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 1 },{ "Y",-2 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 1 } });
            frame1.Add(new Dictionary<string, short>
            { { "X", 0 },{ "Y",-2 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 2 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-1 },{ "Y",-2 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 3 } });
            frame1.Add(new Dictionary<string, short>
            { { "X",-1 },{ "Y",-1 },{ "DAMAGE", 42 },{ "LOCK", 0 },{ "KEY", 0 },{ "ANIM", 4 } });

            UPMOVEATK.Add(frame1);
        }//Declare upMoveAtkTiles


    }

    protected override short PacketParser(short packetToParse)
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
        if      ((packetToParse & 0b0001_0000) != 0) parsedPacket |= 16;
        else if ((packetToParse & 0b0010_0000) != 0) parsedPacket |= 32;
        else if ((packetToParse & 0b0100_0000) != 0) parsedPacket |= 64;
        else if ((packetToParse & 0b1000_0000) != 0) parsedPacket |= 128;

        if (item)
        {
            if (parsedPacket >= 16) parsedPacket >>= 4;//offsets attack into move
            parsedPacket |= 256;//enable item flag
        }

        return parsedPacket;
    }

    protected override void AskAtk(bool hasAlreadyMoved)
    {
        if ((packet & 0b1111_0000) == 0) return;
        action = "Atk";

        if (hasAlreadyMoved)
        {
            if      (packet == 0b0001_0001) map.CreateAtk(this, DOWNMOVEATK, atkFolder + "DownMoveAtk", animPerBeat, flippableAnim);
            else if (packet == 0b0010_0010) map.CreateAtk(this, LEFTMOVEATK, atkFolder + "LeftMoveAtk", animPerBeat, flippableAnim);
            else if (packet == 0b0100_0100) map.CreateAtk(this, RIGHTMOVEATK, atkFolder + "RightMoveAtk", animPerBeat, flippableAnim);
            else if (packet == 0b1000_1000) map.CreateAtk(this, UPMOVEATK, atkFolder + "UpMoveAtk", animPerBeat, flippableAnim);
            else return;
        }
        else
        {
            if      ((packet & 0b0001_0000) != 0) map.CreateAtk(this, DOWNATK, atkFolder + "DownAtk", animPerBeat, flippableAnim);
            else if ((packet & 0b0010_0000) != 0) map.CreateAtk(this, LEFTATK, atkFolder + "LeftAtk", animPerBeat, flippableAnim);
            else if ((packet & 0b0100_0000) != 0) map.CreateAtk(this, RIGHTATK, atkFolder + "RightAtk", animPerBeat, flippableAnim);
            else if ((packet & 0b1000_0000) != 0) map.CreateAtk(this, UPATK, atkFolder + "UpAtk", animPerBeat, flippableAnim);
        }
    }
}
