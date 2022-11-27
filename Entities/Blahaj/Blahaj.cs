using Godot;
using System;
using System.Collections.Generic;

public class Blahaj : Entity
{

    public override void _Ready()
    {
        base._Ready();

        animPerBeat = new byte[]{4,5,3};

        DOWNATK = new List<List<Dictionary<String, short>>>
        {
            new List<Dictionary<String, short>> //PHASE 1 :::::::::::::::::::::::::::::::::::::::
            {
                new Dictionary<string, short>
            { { "X", 0 },{ "Y", 1 },{ "DAMAGE", 54 },{ "LOCK", 0 },{ "KEY", 1 },{ "ANIM", 0 } },
                new Dictionary<string, short>
            { { "X", 0 },{ "Y", 2 },{ "DAMAGE", 0 },{ "LOCK", 1 },{ "KEY", 2 },{ "ANIM", 1 } },
                new Dictionary<string, short>
            { { "X", 1 },{ "Y", 1 },{ "DAMAGE", 0 },{ "LOCK", 1 },{ "KEY", 3 },{ "ANIM", 2 } },
                new Dictionary<string, short>
            { { "X",-1 },{ "Y", 1 },{ "DAMAGE", 0 },{ "LOCK", 1 },{ "KEY", 4 },{ "ANIM", 2 } }
            },                                  //PHASE 1 :::::::::::::::::::::::::::::::::::::::
            new List<Dictionary<string, short>> //PHASE 2 :::::::::::::::::::::::::::::::::::::::
            {
                new Dictionary<string, short>
            { { "X", 0 },{ "Y", 2 },{ "DAMAGE", 27 },{ "LOCK", 2 },{ "KEY", 5 },{ "ANIM", 0 } },

                new Dictionary<string, short>
            { { "X", 0 },{ "Y", 3 },{ "DAMAGE", 0 },{ "LOCK", 5 },{ "KEY", 8 },{ "ANIM", 2 } },
                new Dictionary<string, short>
            { { "X", 1 },{ "Y", 2 },{ "DAMAGE", 0 },{ "LOCK", 5 },{ "KEY", 9 },{ "ANIM", 3 } },
                 new Dictionary<string, short>
            { { "X",-1 },{ "Y", 2 },{ "DAMAGE", 0 },{ "LOCK", 5 },{ "KEY",10 },{ "ANIM", 3 } },


                new Dictionary<string, short>
            { { "X", 1 },{ "Y", 1 },{ "DAMAGE", 27 },{ "LOCK", 3 },{ "KEY", 6 },{ "ANIM", 1 } },

                new Dictionary<string, short>
            { { "X", 1 },{ "Y", 2 },{ "DAMAGE", 0 },{ "LOCK", 6 },{ "KEY", 9 },{ "ANIM", 3 } },
                new Dictionary<string, short>
            { { "X", 2 },{ "Y", 1 },{ "DAMAGE", 0 },{ "LOCK", 6 },{ "KEY",11 },{ "ANIM", 4 } },


                new Dictionary<string, short>
            { { "X",-1 },{ "Y", 1 },{ "DAMAGE", 27 },{ "LOCK", 4 },{ "KEY", 7 },{ "ANIM", 1 } },

                new Dictionary<string, short>
            { { "X",-1 },{ "Y", 2 },{ "DAMAGE", 0 },{ "LOCK", 7 },{ "KEY",10 },{ "ANIM", 3 } },
                new Dictionary<string, short>
            { { "X",-2 },{ "Y", 1 },{ "DAMAGE", 0 },{ "LOCK", 7 },{ "KEY",12 },{ "ANIM", 4 } },


            },                                  //PHASE 2 :::::::::::::::::::::::::::::::::::::::
            new List<Dictionary<string, short>> //PHASE 3 :::::::::::::::::::::::::::::::::::::::
            {
                new Dictionary<string, short>
            { { "X", 2 },{ "Y", 1 },{ "DAMAGE", 12 },{ "LOCK",11 },{ "KEY", 0 },{ "ANIM", 2 } },
                new Dictionary<string, short>
            { { "X", 1 },{ "Y", 2 },{ "DAMAGE", 15 },{ "LOCK", 9 },{ "KEY", 0 },{ "ANIM", 1 } },
                new Dictionary<string, short>
            { { "X", 0 },{ "Y", 3 },{ "DAMAGE", 12 },{ "LOCK", 8 },{ "KEY", 0 },{ "ANIM", 0 } },
                new Dictionary<string, short>
            { { "X",-1 },{ "Y", 2 },{ "DAMAGE", 15 },{ "LOCK",10 },{ "KEY", 0 },{ "ANIM", 1 } },
                new Dictionary<string, short>
            { { "X",-2 },{ "Y", 1 },{ "DAMAGE", 12 },{ "LOCK",12 },{ "KEY", 0 },{ "ANIM", 2 } }
            }                                   //PHASE 3 :::::::::::::::::::::::::::::::::::::::


        };


        LEFTATK = DOWNATK;
        RIGHTATK = DOWNATK;
        UPATK = DOWNATK;

    }

}
