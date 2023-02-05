using System;
using Player;

namespace Data
{
    public class GameAction
    {
        public string Name { get; set; }
        public Action<PlayerController> Callback { get; set; }
    }
}