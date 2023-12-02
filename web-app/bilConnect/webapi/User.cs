namespace webapi
{
    public class User
    {
        List<Post> Posts = new List<Post>();
        private int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private string Password { get; set; }
        public int UserRate { get; set; }
        static int ID = 0;
        public bool CreatePost(string title, string description, int price)
        {
            if (title != null && description != null && price >= 0)
            {
                Posts.Add(new Post(title, description, price, this.UserRate, ID++));
                return true;
            }
            else return false;
        }
        public bool FlagAsInactive(int PostId)
        {
            bool found = false;
            for (int i = 0; i < Posts.Count && !found; ++i)
            {
                if (Posts[i].Id == PostId)
                {
                    Posts[i].InactivePost = true;
                    found = true;
                }
            }
            if (!found)
                return false;
            else
                return true;
        }
        public bool DeletePost(int PostId)
        {
            bool found = false;
            for (int i = 0; i < Posts.Count && !found; ++i)
            {
                if (Posts[i].Id == PostId)
                {
                    Posts.RemoveAt(i);
                    found = true;
                }
            }
            if (!found)
                return false;
            else
                return true;
        }
        public User(int ID, string Name, string Email, string Password)
        {
            this.Id = ID;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
            this.UserRate = -1;
        }
    }
}
