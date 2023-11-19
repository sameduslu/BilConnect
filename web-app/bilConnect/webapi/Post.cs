namespace webapi
{
    public class Post
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UserRate { get; set; }
        public bool InactivePost { get; set; }
        public int Id { get; set; }
        public Post(string Title, string Description, int Price, int UserRate, int ID)
        {
            this.Title = Title;
            this.Description = Description;
            this.Price = Price;
            this.UserRate = UserRate;
            this.InactivePost = false;
            this.Id = ID;
        }
    }
}
