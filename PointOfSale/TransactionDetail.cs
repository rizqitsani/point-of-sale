using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale
{
    class TransactionDetail
    {
        public string NamaBarang { get; set; }
        public int JumlahBarang { get; set; }

        public TransactionDetail(string namaBarang, int jumlahBarang)
        {
            NamaBarang = namaBarang;
            JumlahBarang = jumlahBarang;
        }
    }
}
