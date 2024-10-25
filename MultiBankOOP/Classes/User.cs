namespace Classes
{
    public class User
    {
        public string Id { get; set; }
        public string Pin { get; set; }

        List<Movement> Movements { get; set; }
        public int Money { get; set; }

        public User() 
        {
            Id = "0";
            Pin = "0";
            Movements = new List<Movement>();
            Money = 0;
        }

        public User (string id, string pin, int money)
        {
            Id = id;
            Pin = pin;
            Movements = new List<Movement>();
            Money = money;
        }
    }

}
