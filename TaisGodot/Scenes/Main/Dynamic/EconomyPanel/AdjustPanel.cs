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

		public Adjust gmObj;

		private Label name;
		private Label value;
		private Container levelContainer;
		private ButtonGroup group;

		public override void _Ready()
		{
			name = GetNode<Label>("HBoxContainer/Label");
			//value = GetNode<Label>("");
			levelContainer = GetNode<Container>("HBoxContainer");

			group = new ButtonGroup();

			name.Text = gmObj.name;
			for (int i = 0; i < gmObj.rates.Length; i++)
			{
				CreateLevelToggle(i+1);
			}

			gmObj.OBSProperty(x => x.currLevel).Subscribe(l =>
			{
				var toggle = levelContainer.GetChildren<Button>().Single(x => x.Name == $"LEVEL{l}");
				toggle.Pressed = true;
			}).EndWith(this);

			//if(gmObj.IsDefType<AdjustTaxDef>())
			//{
			//	GMRoot.runner.economy.OBSProperty(x => x.incomes).Subscribe(incomes =>
			//	{
			//		value.Text = incomes.SelectMany(income => income[IncomeDetail.TYPE.POP_TAX]).Sum(t => t.value).ToString();
			//	});
			//}
		}

		private void CreateLevelToggle(int index)
		{
			Button btn = new Button();
			btn.Name = $"LEVEL{index}";
			btn.Text = btn.Name;
			//btn.Icon = ResourceLoader.Load<Texture>("res://Resources/image/ui/adjust_level_button.png");
			btn.ToggleMode = true;
			btn.Group = group;

			levelContainer.AddChild(btn);

			btn.Connect("pressed", this, nameof(_on_Level_Selected), new Godot.Collections.Array(){ index });
		}

		private void _on_Level_Selected(int level)
		{
			gmObj.currLevel = level;
		}
	}
}