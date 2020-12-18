using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Models
{
   public delegate void RefreshCollies();
    public class PenjualanView:ModelsShared.Models.Penjualan,ICloneable
    {

        public event RefreshCollies OnChangeColly;

        public PenjualanView()
        {
            if(this.Details==null)
            {
                this.Details = new List<ModelsShared.Models.Colly>();
            }
        }
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set {
                
               SetProperty(ref _isSelected , value);
                //if(Details!=null && !value)
                //{
                //    Details.Clear();
                //}
                OnChangeColly?.Invoke();
            }
        }
       
       

        private int _pcsSim;

        public int PcsSim
        {
            get
            {

                if (this.Details != null || this.Details.Count > 0)
                {
                    _pcsSim = Details.Count;
                }

                return _pcsSim;
            }
            set
            {
                SetProperty(ref _pcsSim, value);
            }
        }


        private double _weightSim;

        public double WeightSim
        {
            get
            {

                if (this.Details != null || this.Details.Count > 0)
                {
                    _weightSim = Details.Sum<ModelsShared.Models.Colly>(O => O.Weight);
                }

                return _weightSim;
            }
            set
            {
                SetProperty(ref _weightSim, value);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public PenjualanView Cloning()
        {
            return (PenjualanView)this.MemberwiseClone();
        }

    }
}
