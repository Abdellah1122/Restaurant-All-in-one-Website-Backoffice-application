﻿
using Restaurant.Models.Enums;

namespace Restaurant.Models.Classes
{
    public class Table
    {
        public int Id { get; set; }
        public string MatriculeTable { get; set; } = string.Empty;
        public StatutTable statut { get; set; }
        public int NbrFoisReserve { get; set; }
        public int NbrFoisOccupe { get; set; }
    }
}
