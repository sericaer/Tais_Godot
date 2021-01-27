using Godot;
using System;
using Tais.API;

namespace TaisGodot.Scripts
{
	public class ConsoleCtrl : Control
	{
		public static bool isCmdInited = false;

		Node consoleNode;

		public override void _Ready()
		{
			consoleNode = GetNode("/root/Console");

			consoleNode.Set("is_vaild", true);

			if (!isCmdInited)
			{
				Godot.Object obj1 = consoleNode.Call("add_command", "GET", this, "GetValue") as Godot.Object;
				obj1 = obj1.Call("add_argument", "name", Variant.Type.String) as Godot.Object;
				obj1 = obj1.Call("register") as Godot.Object;

				Godot.Object obj2 = consoleNode.Call("add_command", "SET", this, "SetValue") as Godot.Object;
				obj2 = obj2.Call("add_argument", "name", Variant.Type.String) as Godot.Object;
				obj2 = obj2.Call("add_argument", "value", Variant.Type.String) as Godot.Object;
				obj2 = obj2.Call("register") as Godot.Object;

				isCmdInited = true;
			}

			consoleNode.GetChild<Control>(0).Connect("visibility_changed", this, nameof(PauseToggle));
		}

		public override void _ExitTree()
		{
			consoleNode.Set("is_vaild", false);
		}

		public void GetValue(String name)
		{
			consoleNode.Call("write_line", VisitorGroup.GetValue(name).ToString());
		}

		public void SetValue(String name, String value)
		{
			consoleNode.Call("write_line", VisitorGroup.SetValue(name, value));
		}

		public void PauseToggle()
		{
			if(consoleNode.GetChild<Control>(0).Visible)
			{
				SpeedContrl.Pause();
			}
			else
			{
				SpeedContrl.UnPause();
			}
		}

	}
}

