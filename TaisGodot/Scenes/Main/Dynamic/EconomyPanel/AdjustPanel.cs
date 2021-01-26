using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Tais;
using Tais.API;
using Tais.Run;

namespace TaisGodot.Scripts
{
	class AdjustPanel : PanelContainer
	{
		public const string path = "res://Scenes/Main/Dynamic/EconomyPanel/AdjustPanel.tscn";

		public IAdjust gmObj;

		private Label name;
		private Label value;
		private List<Button> buttons;

		public override void _Ready()
		{
			name = GetNode<Label>("HBoxContainer/Label");
			buttons = GetNode("HBoxContainer").GetChildren<Button>().ToList();
			value = GetNode<Label>("HBoxContainer/Value");

			name.Text = gmObj.type.ToString();


			for(int i=0; i< buttons.Count; i++)
			{
				buttons[i].Connect("pressed", this, nameof(_on_Button_pressed), new Godot.Collections.Array { i });
			}

			gmObj.OBSProperty(x => x.level).Subscribe(level =>
			{
				GD.Print("level:", level);
				buttons[level - 1].Pressed = true;
			}).EndWith(this);

			gmObj.OBSProperty(x => x.min_level).Subscribe(min_level =>
			{
				GD.Print("min_level:", min_level);
				for (int i=0; i< min_level-1; i++)
				{
					buttons[i].Disabled = true;
				}
				for (int i = min_level-1; i < 10; i++)
				{
					buttons[i].Disabled = false;
				}
			}).EndWith(this);

			if (gmObj.type == ADJUST_TYPE.POP_TAX)
			{
				GMRoot.runner.economy.OBSProperty(x => x.incomes).Subscribe(incomes =>
				{
					value.Text = incomes.SelectMany(income => income[IncomeDetail.TYPE.POP_TAX]).Sum(t => t.value).ToString();
				}).EndWith(this);
			}

			if (gmObj.type == ADJUST_TYPE.CHAOTING_TAX)
			{
				GMRoot.runner.economy.OBSProperty(x => x.outputs).Subscribe(outputs =>
				{
					value.Text = outputs.Select(output => output[OutputDetail.TYPE.CHAOTING_YEAR_TAX]).Sum().ToString();
				}).EndWith(this);
			}
		}

		private void _on_Button_pressed(int value)
		{
			gmObj.level = (value + 1);
		}
	}
}
