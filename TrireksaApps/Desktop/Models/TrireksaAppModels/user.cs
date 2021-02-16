using System;
using System.Collections.Generic;

namespace ModelsShared.Models
{
    public class Users : BaseNotify
    {
        public string Id
        {
            get { return _id; }
            set
            {
            SetProperty(ref    _id , value);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
            SetProperty(ref    _email , value);
            }
        }

        public bool EmailConfirmed
        {
            get { return _emailconfirmed; }
            set
            {
            SetProperty(ref    _emailconfirmed , value);
            }
        }

        public string PasswordHash
        {
            get { return _passwordhash; }
            set
            {
            SetProperty(ref    _passwordhash , value);
            }
        }

        public string SecurityStamp
        {
            get { return _securitystamp; }
            set
            {
            SetProperty(ref    _securitystamp , value);
            }
        }

        public string PhoneNumber
        {
            get { return _phonenumber; }
            set
            {
            SetProperty(ref    _phonenumber , value);
            }
        }

        public bool PhoneNumberConfirmed
        {
            get { return _phonenumberconfirmed; }
            set
            {
            SetProperty(ref    _phonenumberconfirmed , value);
            }
        }

        public bool TwoFactorEnabled
        {
            get { return _twofactorenabled; }
            set
            {
            SetProperty(ref    _twofactorenabled , value);
            }
        }

        public DateTime? LockoutEndDateUtc
        {
            get { return _lockoutenddateutc; }
            set
            {
            SetProperty(ref    _lockoutenddateutc , value);
            }
        }

        public bool LockoutEnabled
        {
            get { return _lockoutenabled; }
            set
            {
            SetProperty(ref    _lockoutenabled , value);
            }
        }

        public int AccessFailedCount
        {
            get { return _accessfailedcount; }
            set
            {
            SetProperty(ref    _accessfailedcount , value);
            }
        }

        public string UserName
        {
            get { return _username; }
            set
            {
            SetProperty(ref    _username , value);
            }
        }
        public virtual ICollection<Userclaims> Userclaims { get; set; }
        public virtual ICollection<Userlogins> Userlogins { get; set; }
        public virtual Userprofile Userprofile { get; set; }
        public virtual ICollection<Userrole> Userrole { get; set; }

        private string _id;
        private string _email;
        private bool _emailconfirmed;
        private string _passwordhash;
        private string _securitystamp;
        private string _phonenumber;
        private bool _phonenumberconfirmed;
        private bool _twofactorenabled;
        private DateTime? _lockoutenddateutc;
        private bool _lockoutenabled;
        private int _accessfailedcount;
        private string _username;
    }
}


