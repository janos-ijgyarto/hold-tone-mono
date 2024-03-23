using System.Collections.Generic;
using System.Linq;

namespace BehaviorTree
{
    public abstract partial class Composite : Behavior
    {
        protected Behavior[] _children;

        public override void _Ready()
        {
            _children = GetChildren().OfType<Behavior>().ToArray();
        }
    };
}