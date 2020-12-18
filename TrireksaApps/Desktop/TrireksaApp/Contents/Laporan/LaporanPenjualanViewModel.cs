using Microsoft.Reporting.WinForms;
using ModelsShared;
using ModelsShared.Models;
using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.CollectionsBase;
using TrireksaApp.Common;

namespace TrireksaApp.Contents.Laporan
{
  public  class LaporanPenjualanViewModel:BaseNotify
    {
        private ModelsShared.Models.Customer _shiperSelected;

        public ModelsShared.Models.Customer ShiperSelected
        {
            get
            {
                return _shiperSelected;
            }
            set
            {
               SetProperty(ref _shiperSelected , value);
            }
        }
    }
}
