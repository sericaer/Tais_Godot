using Godot;
using System;

namespace TaisGodot.Scripts
{
	public class ReactiveLabel : Label
	{
		private IDisposable reactiveDispose;

        internal void Assoc<T>(IObservable<T> data, Func<string, string> adpt = null)
        {
            this.adpt = adpt;
            reactiveDispose = data.Subscribe(x=>this.SetValue(x));
        }

        protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			reactiveDispose?.Dispose();
		}

		private Func<string, string> adpt;

		private void SetValue<T>(T value)
		{ 
			Text = adpt!=null ? adpt(value.ToString()) : value.ToString();
		}
	}
}
