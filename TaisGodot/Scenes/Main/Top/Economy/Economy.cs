using Godot;
using System;
using Tais;
using Tais.Run;

namespace TaisGodot.Scripts
{
	public class Economy : Button
	{
		private ReactiveLabel value;

		private IEconomy gmObj;

		public override void _Ready()
		{
			value = GetNode<ReactiveLabel>("HBoxContainer/Value");

			gmObj = GMRoot.runner.economy;

			value.Assoc(gmObj.OBSProperty(x => x.currValue));

			Connect("pressed", this, nameof(on_Button_Pressed));
		}

        private void on_Button_Pressed()
        {
			var panel = ResourceLoader.Load<PackedScene>(EconomyPanel.path).Instance();
			MainScene.instance.AddChild(panel);
		}
    }
}

