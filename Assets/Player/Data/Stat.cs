using UnityEngine;

namespace Data
{
    public class Stat
    {
        public const int MAX = 20;
        public const int MIN = 0;
        public int Value { get; private set; }
        public Stat()
        {
            Value = MAX;
        }

        public Stat(int value)
        {
            Value = value;
        }

        public bool IsMax => Value == MAX;
        public bool IsMin => Value == MIN;

        public static Stat operator +(Stat a, int b)
            => new(Mathf.Clamp(a.Value + b, MIN, MAX));

        public static Stat operator -(Stat a, int b)
            => new(Mathf.Clamp(a.Value - b, MIN, MAX));
    }
}