using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TaisGodot.Scripts
{
    public static class ObjectExtensions
    {
        public static IEnumerable<T> GetChildren<T>(this Node node) where T : Node
        {
            List<T> rslt = new List<T>();
            foreach(var child in node.GetChildren())
            {
                if(child is T)
                {
                    rslt.Add(child as T);
                }
            }

            return rslt;
        }

        public static T GetParentRecursion<T>(this Node node) where T : Node
        {
            var curr = node;
            while(curr != null)
            {
                if(curr is T)
                {
                    return (T)curr;
                }

                curr = curr.GetParent();
            }

            return null;
        }

        public static void EndWith(this IDisposable disposable, Node node)
        {
            var wait = node.ToSignal(node, "tree_exited");
            wait.OnCompleted(disposable.Dispose);

        }
    }
}
