using ModelsShared;

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
