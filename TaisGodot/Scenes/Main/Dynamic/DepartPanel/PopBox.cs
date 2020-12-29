using Godot;
using System;
using Tais;
using Tais.Run;

namespace TaisGodot.Scripts
{
    class PopBox : Panel
    {
        public const string path = "res://Scenes/Main/Dynamic/DepartPanel/PopPanel.tscn";

        public Label type;
        public ReactiveLabel num;
        public Button btn;

        public Pop gmObj;

        public override void _Ready()
        {
            type = GetNode<Label>("Type");
            num = GetNode<ReactiveLabel>("Num");
            btn = GetNode<Button>("Button");

            type.Text = gmObj.name;
            num.Assoc(gmObj.OBSProperty(x => x.num));
            btn.Connect("pressed", this, nameof(_on_Button_Pressed));
        }

        private void _on_Button_Pressed()
        {
            var panel = ResourceLoader.Load<PackedScene>(PopPanel.path).Instance() as PopPanel;
            panel.gmObj = gmObj;
            MainScene.instance.AddChild(panel);
        }
    }
}
