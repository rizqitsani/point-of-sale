using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale
{
    class Transaction
    {
        public double Total { get; set; }
        public string MetodePembayaran { get; set; }

        public Transaction(double total, string metodePembayaran)
        {
            Total = total;
            MetodePembayaran = metodePembayaran;
        }
    }
}
