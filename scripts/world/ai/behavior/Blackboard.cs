using Godot;
using Godot.Collections;
using System;

namespace BehaviorTree
{
    public partial class Blackboard : RefCounted
    {
        private Dictionary<string, Variant> _data = new Dictionary<string, Variant>();

        public Variant GetData(string key)
        {
            return _data[key];
        }

        public void SetData(string key, Variant value)
        {
            _data[key] = value;
        }

        public T Get<[MustBeVariant] T>(string key)
        {
            return _data[key].As<T>();
        }

        public void Set<[MustBeVariant] T>(string key, T value)
        {
            _data[key] = Variant.From(value);
        }
    }
}