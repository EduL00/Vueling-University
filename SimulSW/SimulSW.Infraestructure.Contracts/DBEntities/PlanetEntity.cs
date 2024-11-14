namespace SimulSW.Infraestructure.Contracts.DBEntities
{
    public partial class PlanetEntity
    {
        public string Name { get; set; }
        public int OrbRotation { get; set; }
        public int OrbPeriod { get; set; }
        public string Climate { get; set; }
        public long? Population { get; set; }
        public string Urlinfo { get; set; }
    }
}