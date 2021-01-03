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
		public const string path = "res://Scenes/Main/Dynamic/EconomyPanel/AdjustPanel2.tscn";

		public Adjust gmObj;

		private Label name;
		private Label value;
		private Slider slider;

		public override void _Ready()
		{
			name = GetNode<Label>("HBoxContainer/Label");
			slider = GetNode<Slider>("HBoxContainer/HSlider");

			value = GetNode<Label>("HBoxContainer/Value");

			name.Text = gmObj.name;

			//for (int i = 0; i < gmObj.rates.Length; i++)
			//{
			//	CreateLevelToggle(i+1);
			//}

			gmObj.OBSProperty(x => x.currLevel).Subscribe(l =>
			{
				slider.Value = l;
			}).EndWith(this);

			if (gmObj.IsDefType<AdjustTaxDef>())
			{
				GMRoot.runner.economy.OBSProperty(x => x.incomes).Subscribe(incomes =>
				{
					value.Text = incomes.SelectMany(income => income[IncomeDetail.TYPE.POP_TAX]).Sum(t => t.value).ToString();
				});
			}
		}

		//private void CreateLevelToggle(int index)
		//{
		//	Button btn = new Button();
		//	btn.Name = $"LEVEL{index}";
		//	btn.Text = btn.Name;
		//	//btn.Icon = ResourceLoader.Load<Texture>("res://Resources/image/ui/adjust_level_button.png");
		//	btn.ToggleMode = true;
		//	btn.Group = group;

		//	levelContainer.AddChild(btn);

		//	btn.Connect("pressed", this, nameof(_on_Level_Selected), new Godot.Collections.Array(){ index });
		//}

		private void _on_Level_Selected(int level)
		{
			gmObj.currLevel = level;
		}

		private void _on_HSlider_value_changed(float value)
		{
			gmObj.currLevel = (int)value;
		}
	}
}
