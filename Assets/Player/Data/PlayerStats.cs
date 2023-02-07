namespace Data
{
    public class PlayerStats
    {
        public Stat Hunger { get; set; } = new();
        public Stat Sleep { get; set; } = new();
        public Stat Fun { get; set; } = new();
        public Stat Hygiene { get; set; } = new();
    }

}
