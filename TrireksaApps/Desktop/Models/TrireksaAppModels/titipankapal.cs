namespace ModelsShared.Models
{
    public class Titipankapal : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
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

        public string AgentName
        {
            get { return _agent; }
            set
            {
            SetProperty(ref    _agent , value);
            }
        }

        public int PackNumber
        {
            get { return _pack; }
            set
            {
            SetProperty(ref    _pack , value);
            }
        }

        public string Origin
        {
            get { return _origin; }
            set
            {
            SetProperty(ref    _origin , value);
            }
        }

        public string Destination
        {
            get { return _destionation; }
            set
            {
            SetProperty(ref    _destionation , value);
            }
        }

        public string OriginCode
        {
            get { return _originCode; }
            set
            {
            SetProperty(ref    _originCode , value);
            }
        }

        public string DestinationCode
        {
            get { return _destionationCode; }
            set
            {
            SetProperty(ref    _destionationCode , value);
            }
        }

        public string AgentContactName
        {
            get { return _AgentContactName; }
            set
            {
            SetProperty(ref    _AgentContactName , value);
            }
        }
        public string AgentPhone
        {
            get { return _agentPhone; }
            set
            {
            SetProperty(ref    _agentPhone , value);
            }
        }

        public string AgentHandphone
        {
            get { return _AgentHandphone; }
            set
            {
            SetProperty(ref    _AgentHandphone , value);
            }
        }

        public string ArmadaName
        {
            get { return _armadaname; }
            set
            {
            SetProperty(ref    _armadaname , value);
            }
        }

        public string CrewName
        {
            get { return _crewname; }
            set
            {
            SetProperty(ref    _crewname , value);
            }
        }


        public string CrewContact
        {
            get { return _CrewContact; }
            set
            {
            SetProperty(ref    _CrewContact , value);
            }
        }

        public string CrewAddress
        {
            get { return _CrewAddress; }
            set
            {
            SetProperty(ref    _CrewAddress , value);
            }
        }

        public string ReferenceNumber
        {
            get { return _ReferenceNumber; }
            set
            {
            SetProperty(ref    _ReferenceNumber , value);
            }
        }

        public int Jumlah
        {
            get { return _jumlah; }
            set
            {
            SetProperty(ref    _jumlah , value);
            }
        }



        private int _id;
        private PortType _porttype;
        private string _armadaname;
        private string _crewname;
        private int _code;
        private string _agent;
        private int _pack;
        private string _origin;
        private string _destionation;
        private string _originCode;
        private string _destionationCode;
        private string _AgentContactName;
        private string _agentPhone;
        private string _AgentHandphone;
        private string _CrewContact;
        private string _CrewAddress;
        private string _ReferenceNumber;
        private int _jumlah;
    }
}
