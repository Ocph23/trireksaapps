using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.Models
{
    public class Newcolly :ModelsShared.Models.Colly
    {

        public Newcolly()
        {
            this.PropertyChanged += (x, y) =>
            {
                CalculateWeight();
            };
        }

        private int _jlh;
        private double _tweight;

        public override TypeOfWeight TypeOfWeight
        {
            get
            {
                return base.TypeOfWeight;
            }

            set
            {

                if (value == TypeOfWeight.Weight)
                    ReadonlyVolume = false;
                else
                    ReadonlyVolume = true;
                base.TypeOfWeight = value;
               
            }
        }
        public int Jumlah
        {

            get
            {
                
                    if (_jlh == 0)
                    _jlh = 1;

                return _jlh;
            }
            set
            {
               SetProperty(ref _jlh , value);
                CalculateWeight();
            }
        }

        public override double Weight
        {
            get
            {
                return base.Weight;
            }

            set
            {
                base.Weight = value;
                CalculateWeight();
            }
        }

        private void CalculateWeight()
        {
            if (this.TypeOfWeight ==ModelsShared.Models.TypeOfWeight.Volume)
            {
               TWeight =Jumlah * (this.Longer * this.Wide * this.Hight) / 1000000;
            }
            else if (this.TypeOfWeight ==ModelsShared. Models.TypeOfWeight.WeightVolume)
            {
                if (WeightVolume <= 0)
                    WeightVolume = new ApplicationConfig().DevideWeightVolume;
               TWeight = Jumlah *(this.Longer * this.Wide * this.Hight) / WeightVolume;
            }
            else
            {
                this.Longer = 0;
                this.Wide = 0;
                this.Hight = 0;
                TWeight = Jumlah * Weight;
              
            }

        }

        public double TWeight
        {
            get
            {
               
                return _tweight;
            }
            set
            {
              SetProperty(ref  _tweight , value);
            }

        }



        private bool _volumeItemShow;
     

        public bool ReadonlyVolume
        {
            get { return _volumeItemShow; }
            set
            {
                 SetProperty(ref _volumeItemShow , value);
            }
        }



    }
}
