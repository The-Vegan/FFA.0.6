using Godot;
using System;



public class GenericController : Node2D
{
    protected const short DOWN_MOVE =  0b0000000001;
    protected const short LEFT_MOVE =  0b0000000010;
    protected const short RIGHT_MOVE = 0b0000000100;
    protected const short UP_MOVE =    0b0000001000;
    protected const short DOWN_ATK =   0b0000010000;
    protected const short LEFT_ATK =   0b0000100000;
    protected const short RIGHT_ATK =  0b0001000000;
    protected const short UP_ATK =     0b0010000000;
    protected const short ITEM_USED =  0b0100000000;
    protected const short RESTING =    0b1000000000;

    protected Entity entity;

    protected short packet;
    public override void _Ready()
    {
        entity = (Entity)this.GetParent();
    }


}
