﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    public class Dimention  :BaseNotify
    {
        private double _Hight;
        private double _Long;
        private double _Wide;

        public Dimention()
        {

        }

        public Dimention(double Long, double Wide,double Hight)
        {
            this.Long = Long;
            this.Wide = Wide;
            this.Hight = Hight;
        }

        public double Hight
        {
            get
            {
                return _Hight;
            }

            private set
            {
                 SetProperty(ref  _Hight , value);
            }
        }

        public double Long
        {
            get
            {
                return _Long;
            }

            private set
            {
                SetProperty(ref _Long, value);
            }
        }

        public double Wide
        {
            get
            {
                return _Wide;
            }

            private set
            {
                SetProperty(ref _Wide, value);
            }
        }
    }
}
