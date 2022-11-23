﻿using Godot;
using System;

public class ControllerPlayer : GenericController
{


  public override void _Process(float _delta)
  {
        if (ScanInput()) 
        { 
            entity.SetPacket(packet);
            packet = 0;
        }
  }
    private bool ScanInput()
    {
        
        bool pressed = false;
        if (Input.IsActionJustPressed("MoveDown")) 
        { 
            packet |= DOWN_MOVE;
            pressed = true;
        }
            
        if (Input.IsActionJustPressed("MoveLeft")) 
        {
            packet |= LEFT_MOVE;
            pressed = true;
        }
            
        if (Input.IsActionJustPressed("MoveRight")) 
        {
            packet |= RIGHT_MOVE;
            pressed = true;
        }
            
        if (Input.IsActionJustPressed("MoveUp")) 
        {
            packet |= UP_MOVE;
            pressed = true;
        }



        return pressed;
    }

}
