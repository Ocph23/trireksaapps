using System.Collections.Generic;

namespace ModelsShared.Models
{
    public class Ship : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
            SetProperty(ref    _name , value);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
            SetProperty(ref    _description , value);
            }
        }

        public string Route
        {
            get { return _route; }
            set
            {
            SetProperty(ref    _route , value);
            }
        }

        public List<string> RouteView
        {

            get
            {
                if (!string.IsNullOrEmpty(this.Route))
                {
                    var x = this.Route.Split(';');
                    var list = new List<string>();
                    foreach (var item in x)
                    {
                        list.Add(item);
                    }
                    return list;
                }

                else
                    return null;
            }
            set
            {
                Route = string.Empty;
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        Route += item;
                        if (item.LastIndexOf(item) <= (value.Count - 2))
                        {
                            Route += ";";
                        }
                    }
                }
            }
        }

        private int _id;
        private string _name;
        private string _description;
        private string _route;
    }
}
