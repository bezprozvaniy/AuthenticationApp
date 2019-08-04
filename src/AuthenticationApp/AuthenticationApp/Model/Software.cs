namespace AuthenticationApp.Model
{
    public class Software
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long UserId { get; set; }

        public Software()
        {

        }

        public Software(string name) : this()
        {
            this.Name = name;
        }
    }
}
