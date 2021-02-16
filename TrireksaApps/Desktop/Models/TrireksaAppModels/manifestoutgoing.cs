using System;
using System.Collections.Generic;


namespace ModelsShared.Models
{
    public class Manifestoutgoing : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
            }
        }

        public int AgentId
        {
            get { return _agentid; }
            set
            {
            SetProperty(ref    _agentid , value);
            }
        }


        public int Code
        {
            get { return _code; }
            set
            {
            SetProperty(ref    _code , value);
            }
        }

        public PortType PortType
        {
            get { return _porttype; }
            set
            {
            SetProperty(ref    _porttype , value);
            }
        }

        public int ReferenceID
        {
            get { return _referenceid; }
            set
            {
            SetProperty(ref    _referenceid , value);
            }
        }

        public int Origin
        {
            get { return _origin; }
            set
            {
            SetProperty(ref    _origin , value);
            }
        }

        public int Destination
        {
            get { return _destionation; }
            set
            {
            SetProperty(ref    _destionation , value);
            }
        }

        public DateTime? OnOriginPort
        {
            get { return _onoriginport; }
            set
            {
            SetProperty(ref    _onoriginport , value);
            }
        }

        public DateTime? OnDestinationPort
        {
            get { return _ondestinationport; }
            set
            {
            SetProperty(ref    _ondestinationport , value);
            }
        }

        public DateTime CreatedDate
        {
            get { return _createddate; }
            set
            {
            SetProperty(ref    _createddate , value);
            }
        }

        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set
            {
            SetProperty(ref    _updatedate , value);
            }
        }

        public string UsersId
        {
            get { return _userid; }
            set
            {
            SetProperty(ref    _userid , value);
            }
        }


        public string ManifestCode
        {
            get
            {
                if (this.Code > 0)
                    return ModelHelpers.GenerateManifestOutGoingCode(this.Code, this.CreatedDate);
                else
                    return string.Empty;
            }
        }

        public ManifestInformation Information { get;  set; }
        public Users Users{ get;  set; }
       
        public Agent Agent {
            get
            {
                return _agent;
            }
            set
            {
            SetProperty(ref    _agent , value);
            }
        }
        public Port OriginNavigation
        {
            get { return _originPort; }
            set
            {
            SetProperty(ref    _originPort , value);
            }
        }

        public Port DestinationNavigation
        {
            get { return _destionationPort; }
            set
            {
            SetProperty(ref    _destionationPort , value);
            }
        }

        public List<Packinglist> PackingList { get; set; }

        private int _id;
        private int _code;
        private PortType _porttype;
        private int _referenceid;
        private int _origin;
        private int _destionation;
        private DateTime? _onoriginport;
        private DateTime? _ondestinationport;
        private DateTime _createddate;
        private DateTime _updatedate;
        private string _userid;
        private int _agentid;
        private Agent _agent;
        private Port _originPort;
        private Port _destionationPort;



    }

}
