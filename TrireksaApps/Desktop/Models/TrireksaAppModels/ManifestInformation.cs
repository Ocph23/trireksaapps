namespace ModelsShared.Models
{

    public class ManifestInformation : BaseNotify
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public ManifestType ManifestType
        {
            get { return _manifesttype; }
            set
            {
                SetProperty(ref _manifesttype, value);
            }
        }

        public int ManifestId
        {
            get { return _manifestid; }
            set
            {
                SetProperty(ref _manifestid, value);
            }
        }

        public PortType PortType
        {
            get { return _porttype; }
            set
            {
                SetProperty(ref _porttype, value);
            }
        }

        public string ArmadaName
        {
            get { return _armadaname; }
            set
            {
                SetProperty(ref _armadaname, value);
            }
        }

        public string CrewName
        {
            get { return _crewname; }
            set
            {
                SetProperty(ref _crewname, value);
            }
        }

        public string Contact
        {
            get { return _contact; }
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
            }
        }

        public string ReferenceNumber
        {
            get { return _referencenumber; }
            set
            {
                SetProperty(ref _referencenumber, value);
            }
        }

        private int _id;
        private ManifestType _manifesttype;
        private int _manifestid;
        private PortType _porttype;
        private string _armadaname;
        private string _crewname;
        private string _contact;
        private string _address;
        private string _referencenumber;
    }

}
