using Godot;
using System;

namespace TaisGodot.Scripts
{
	public class SpeedContrl : HBoxContainer
	{
		[Signal]
		public delegate void DaysInc();

		public static void UnPause()
		{
			sysPause = false;
		}

		public static void Pause()
		{
			sysPause = true;
		}

		public override void _Ready()
		{
			if (!OS.HasFeature("ReleaseApp"))
			{
				MAX_SPEED = 20;
				speed = MAX_SPEED;
			}
			else
			{
				speed = 2;
			}
		}

		private void _on_CheckBox_toggled(bool button_pressed)
		{
			usrPause = button_pressed;
		}
		
		private void _on_Button_Inc_pressed()
		{
			if (speed < MAX_SPEED)
			{
				speed++;
			}

			GetNode<Button>("Button_Dec").Disabled = false;
			if (speed == MAX_SPEED)
			{
				GetNode<Button>("Button_Inc").Disabled = true;
			}
		}
		private void _on_Button_Dec_pressed()
		{
			if (speed > MIN_SPEED)
			{
				speed--;
			}

			GetNode<Button>("Button_Inc").Disabled = false;
			if (speed == MIN_SPEED)
			{
				GetNode<Button>("Button_Dec").Disabled = true;
			}
		}

		private void _on_Timer_timeout()
		{
			if (isPause)
			{
				return;
			}

			EmitSignal(nameof(DaysInc));
		}

		private static int MAX_SPEED = 5;
		private static int MIN_SPEED = 1;

		private static bool isPause
		{
			get
			{
				return usrPause||sysPause;
			}
		}

		private static bool usrPause;
		private static bool sysPause;

		private int speed
		{
			get
			{
				return _speed;
			}
			set
			{
				_speed = value;
				GetNode<Timer>("Timer").WaitTime = 2.0f / _speed;
			}
		}

		private static int _speed = 1;
	}
}
