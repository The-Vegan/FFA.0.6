using Godot;
using System;

public class DamageTile : AnimatedSprite
{
    private Entity source;
    private Vector2 coordinates;
    private String animation;
    private SpriteFrames texture;

    private bool flippedH = false, flippedV = false;
    private short damage;
    

    public void InitDamageTile(Entity attacker,Vector2 pos,String anim,SpriteFrames sf,short punch,bool flippedX, bool flippedY)
    {
        this.source = attacker;
        this.coordinates = pos;
        this.animation = anim;
        this.flippedH = flippedX;
        this.flippedV = flippedY;

#pragma warning disable CS0618 // Type or member is obsolete
        this.SetSpriteFrames(sf);
#pragma warning restore CS0618 // Type or member is obsolete

        this.damage = punch;
    }

    public override void _Ready()
    {
        GD.Print("DamageTile created");

        this.Play(animation);
        this.FlipH = flippedH;
        this.FlipV = flippedV;
        //play the animation

        this.Connect("animation_finished", this, "queue_free");
    }

}
