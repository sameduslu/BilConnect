namespace BilConnect.Models.PostModels
{
    public class PetAdoptionPost : Post
    {
        bool IsFullyVaccinated { get; set; }
        int AgeInMonths { get; set; }
    }
}
