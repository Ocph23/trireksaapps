using System.Collections.Generic;

namespace ModelsShared.Models
{
    public class PenjualanOfYear
    {
        private int _tahun;
        public int Tahun
        {
            get { return _tahun; }
            set { _tahun = value; }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private double _total;
        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }


        private double _cod;
        public double COD
        {
            get { return _cod; }
            set { _cod = value; }
        }

        private double _credit;
        public double Credit
        {
            get { return _credit; }
            set { _credit = value; }
        }

        private double _chash;
        public double Chash
        {
            get { return _chash; }
            set { _chash = value; }
        }




        public List<PenjualanOfMonth> Months { get; set; }


    }
}
