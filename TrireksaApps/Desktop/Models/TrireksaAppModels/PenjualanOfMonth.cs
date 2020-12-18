using System;

namespace ModelsShared.Models
{
    public class PenjualanOfMonth :BaseNotify
    {
        private DateTime _tanggal;
        public DateTime Tanggal
        {
            get { return _tanggal; }
            set {SetProperty(ref _tanggal , value); }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set {SetProperty(ref _weight , value); }
        }

        private double _total;
        public double Total
        {
            get { return _total; }
            set {SetProperty(ref _total , value); }
        }


        private double _cod;
        public double COD
        {
            get { return _cod; }
            set {SetProperty(ref _cod , value); }
        }

        private double _credit;
        public double Credit
        {
            get { return _credit; }
            set {SetProperty(ref _credit , value); }
        }

        private double _chash;
        public double Chash
        {
            get { return _chash; }
            set {SetProperty(ref _chash , value); }
        }


    }
}
