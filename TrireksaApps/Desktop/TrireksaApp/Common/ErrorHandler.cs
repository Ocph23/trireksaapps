using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Common
{
    public class ErrorHandler
    {
        private Dictionary<string, string> Collection { get; set; }
        public ErrorHandler()
        {
            Collection = new Dictionary<string, string>();
        }



        public void AddError(string propertyName, string value)
        {
            if (!Collection.ContainsKey(propertyName))
            {
                Collection.Add(propertyName, value);

            }
            
        }


        public int ErrorCount
        {
            get
            {
                return Collection.Count;
            }
        }

        public void DeleteError(string propertyName)
        {
            if (Collection.ContainsKey(propertyName))
            {
                Collection.Remove(propertyName);
            }

        }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}
