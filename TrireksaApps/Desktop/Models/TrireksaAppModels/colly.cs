using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace ModelsShared.Models 
{ 
     public class Colly : BaseNotify,ICloneable
    {
          public int Id 
          {
            get => _id;
            set=>SetProperty(ref _id,value); 
          } 

          public int PenjualanId
        {
            get { return _penjualanId; }
            set => SetProperty(ref _penjualanId, value);
        } 

          public int CollyNumber 
          {
            get { return _collynumber; }
            set => SetProperty(ref _collynumber, value);
            }

        public double WeightVolume
        {
            get { return _devideWeightVolume; }
            set => SetProperty(ref _devideWeightVolume, value);
        }

        public virtual double Weight 
          {
                get
                {
                    if (this.TypeOfWeight == TypeOfWeight.Volume)
                    {
                        _weight = (this.Longer * this.Wide * this.Hight) / 1000000;
                        return _weight;
                    }
                    else if (this.TypeOfWeight == TypeOfWeight.WeightVolume)
                    {
                        _weight = (this.Longer * this.Wide * this.Hight) / WeightVolume;
                        return _weight;
                    }
                    return _weight;

                }
                set => SetProperty(ref _weight, value);
            }
         

          public virtual double Longer
        {
            get { return _long; }
            set => SetProperty(ref _long, value);
        } 

          public virtual double Hight 
          {
            get { return _hight; }
            set => SetProperty(ref _hight, value);
        } 

          public virtual double Wide 
          {
            get { return _wide; }
            set => SetProperty(ref _wide, value);
        } 

          public virtual TypeOfWeight TypeOfWeight 
          {
            get { return _typeofweight; }
            set => SetProperty(ref _typeofweight, value);
        } 

          public bool IsSended
        {
            get { return _issended; }
            set => SetProperty(ref _issended, value);
        }


        public Dimention Dimention
        {
            get
            {
                _dimention = new Dimention(this.Longer, this.Wide, this.Hight);
                return _dimention;
            }
            set
            {

                SetProperty(ref _dimention, value);
            }
        }

        public int STT { get; set; }
        private Dimention _dimention;
           private int  _collynumber;
           private double  _weight;
           private double  _long;
           private double  _hight;
           private double  _wide;
           private TypeOfWeight  _typeofweight;
           private bool _issended;
        private int _penjualanId;
        private int _id;
        private double _devideWeightVolume;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Colly GetClone()
        {
            return (Colly)this.MemberwiseClone();
        }
    }
}


