﻿namespace RSLib.Dynamics
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Dynamic Bool", menuName = "RSLib/Dynamics/Bool")]
    public class Bool : ScriptableObject
    {
        public struct ValueChangedEventArgs
        {
            public bool Previous;
            public bool New;
        }

        [SerializeField] private bool _value = false;

        public delegate void ValueChangedEventHandler(ValueChangedEventArgs args);
        public event ValueChangedEventHandler ValueChanged;

        public bool Value
        {
            get => _value;
            set
            {
                ValueChangedEventArgs valueChangedEventArgs = new ValueChangedEventArgs()
                {
                    Previous = _value,
                    New = value
                };

                _value = value;
                ValueChanged?.Invoke(valueChangedEventArgs);
            }
        }
    }
}