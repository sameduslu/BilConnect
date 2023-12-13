namespace BilConnect.Data.ViewModels.PostViewModels
{
    public class NewPetAdoptionPostVM : NewPostVM
    {
        bool IsFullyVaccinated { get; set; }
        int AgeInMonths { get; set; }
    }
}
