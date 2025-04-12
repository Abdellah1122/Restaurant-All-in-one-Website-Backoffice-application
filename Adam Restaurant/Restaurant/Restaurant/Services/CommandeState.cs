using Restaurant.Models.Classes;

namespace Restaurant.Services
{
    public class CommandeState
    {
        public event Action? AddedSmtg;
        public Commande? commande { get; set; }
        public void AddCommande(Commande c)
        {
            commande = c;
            AddedSmtg?.Invoke();
        }
        public void RemoveCommande()
        {
            commande = null;
            AddedSmtg?.Invoke();
        }
    }
}
