using Godot;
using System;
using System.Reactive.Linq;
using Tais;
using Tais.API;
using Tais.Run;
using DynamicData;

namespace TaisGodot.Scripts
{
    class WarnContainer : HBoxContainer
    {
        public override void _Ready()
        {
            GMRoot.runner.warns.Connect().OnItemAdded(x=>
            {

            }).Subscribe().EndWith(this);

            GMRoot.runner.warns.Connect().OnItemRemoved(x =>
            {

            }).Subscribe().EndWith(this);
        }
    }
}
