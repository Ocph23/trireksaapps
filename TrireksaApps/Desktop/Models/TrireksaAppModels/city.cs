namespace ModelsShared.Models
{
    public class City :BaseNotify
     {
          public int Id 
          { 
               get{return _id;} 
               set{ SetProperty(ref _id, value); }
          } 

          public string Province 
          {
            get => _province;
            set => SetProperty(ref _province, value); 
          } 

          
          public string Regency 
          {
            get => _regency;
            set => SetProperty(ref _regency, value);
        } 

          
          public string CityName 
          {
            get => _cityname;
            set => SetProperty(ref _cityname, value);
        } 

       
          public string CityCode 
          {
            get => _citycode;
            set => SetProperty(ref _citycode, value);
        } 

          private int  _id;
           private string  _province;
           private string  _regency;
           private string  _cityname;
           private string  _citycode;
      }
}


