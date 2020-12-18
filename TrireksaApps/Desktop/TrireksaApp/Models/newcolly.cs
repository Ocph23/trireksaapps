using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;

namespace TrireksaApp.Models
{
    public class Newcolly :ModelsShared.Models.Colly
    {
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
               TWeight = Jumlah *(this.Longer * this.Wide * this.Hight) / 6000;
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
     

        public bool EnabledVolume
        {
            get { return _volumeItemShow; }
            set
            {
                 SetProperty(ref _volumeItemShow , value);
            }
        }



    }
}
