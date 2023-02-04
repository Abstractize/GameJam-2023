using System;
using Player;

namespace Data
{
    public class GameAction
    {
        public string Name { get; set; }
        public Action<NetworkPlayer> Callback { get; set; }
    }
}