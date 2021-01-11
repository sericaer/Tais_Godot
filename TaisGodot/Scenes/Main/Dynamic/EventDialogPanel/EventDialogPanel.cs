using System;
using Godot;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Run;

namespace TaisGodot.Scripts
{
    class EventDialogPanel : Panel
    {
        internal const string path = "res://Scenes/Main/Dynamic/EventDialogPanel/EventDialogPanel.tscn";
        internal IEvent gmObj;

        Label title;
        Label content;

        public override void _Ready()
        {
            title = GetNode<Label>("CenterContainer/PanelContainer/VBoxContainer/Title");

            title.Text = TranslateServerEx.Translate(gmObj.title_format, gmObj.title_objs);

        }
    }
}
