using Restaurant.Models.Classes;

namespace Restaurant.Services
{
    public class TableChoisis
    {
        public Table TbChoisis=null;

        public void ChoisirTable(Table tb)
        {
            TbChoisis = tb;
        }
        public void UncheckTable()
        {
            TbChoisis = null;
        }
    }
}
