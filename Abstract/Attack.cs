using Godot;
using System;
using System.Collections.Generic;

public class Attack : Node2D
{
    //TECHNICAL
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected byte currentBeat = 0;
    protected byte maxBeat;
    protected List<List<Dictionary<String, short>>> packagedAtkData;
    protected List<byte> keyChain;

    protected Vector2 gridPos;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //TECHNICAL

    //DEPENDANCIES
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected Entity source;
    protected Level level;
    protected String folderPath;
    protected String beatAnimPath;

    protected PackedScene damageTileScene = GD.Load("res://Abstract/DamageTile.tscn") as PackedScene;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //DEPENDANCIES

    //ANIMATIONS
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    protected bool flipableAnims = false;
    protected byte[] animations;
    //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\\
    //ANIMATIONS
    public void InitAtk(Entity attacker ,List<List<Dictionary<String, short>>> atkData ,String path ,byte[] collumns ,bool flipable)
    {
        this.source = attacker;
        this.packagedAtkData = atkData;
        this.folderPath = path;
        this.animations = collumns;
        this.flipableAnims = flipable;


    }

    public override void _Ready()
    {
        
    }

    private void BeatAtkUpdate()
    {

    }
}
