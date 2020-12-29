﻿using ModelsShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;

namespace TrireksaApp.Common
{
    public class AppConfiguration : BaseNotify
    {
        private readonly Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private int _EliminateCityId;

        public int EliminateCityId
        {
            get
            {
                if (_EliminateCityId <= 0)
                    _EliminateCityId = GetIntValue("EliminateCityId");
                return _EliminateCityId;
            }
            set
            {
                UpdateKey("EliminateCityId", value.ToString());
                SetProperty(ref _EliminateCityId, value);
            }
        }


        private int _WitoutCityId;

        public int WitoutCityId
        {
            get
            {
                if (_WitoutCityId <= 0)
                    _WitoutCityId = GetIntValue("WitoutCityId");
                return _WitoutCityId;
            }
            set
            {
                UpdateKey("WitoutCityId", value.ToString());
                SetProperty(ref _WitoutCityId, value);
            }
        }


       


        private string theme;

        public string Theme
        {
            get
            {
                if (string.IsNullOrEmpty(theme))
                    theme = GetStringValue("Theme");
                return theme;
            }
            set
            {
                UpdateKey("Theme", value.ToString());
                SetProperty(ref theme, value);
            }
        }


        private string apparanceColor;

        public string ApparanceColor
        {
            get
            {
                if (string.IsNullOrEmpty(apparanceColor))
                    apparanceColor = GetStringValue("ApparanceColor");
                return apparanceColor;
            }
            set
            {

                UpdateKey("ApparanceColor", value.ToString());
                SetProperty(ref apparanceColor, value);
            }
        }

        internal string GetUserName()
        {
            return GetStringValue("UserName");
        }

        internal async void UpdateUserName(string value)
        {
            await Task.Delay(2000);
            UpdateKey("UserName", value);
        }

        //
        private int GetIntValue(string KeyName)
        {
            int vResult = 0;

            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (int)asr.GetValue(KeyName, typeof(int));
            }
            return vResult;
        }

        private string GetStringValue(string KeyName)
        {
            string vResult = string.Empty;
            if (this.KeyExists(KeyName))
            {
                AppSettingsReader asr = new AppSettingsReader();
                vResult = (string)asr.GetValue(KeyName, typeof(string));
            }
            return vResult;
        }

        public void UpdateKey(string strKey, string newValue)
        {
            if (!KeyExists(strKey))
            {
                // Add an Application Setting.
                config.AppSettings.Settings.Add(strKey, newValue);
                config.Save(ConfigurationSaveMode.Modified, true);
                // Save the configuration file.
                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                // Add an Application Setting.

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            if (node.Attributes[0].Value.Equals(strKey))
                            {
                                node.Attributes[1].Value = newValue;
                            }
                        }
                    }
                }

                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        public bool KeyExists(string strKey)
        {
            bool IsExists = false;
            foreach (string item in config.AppSettings.Settings.AllKeys)
            {
                if (item == strKey)
                {
                    IsExists = true;
                }
            }
            return IsExists;
        }


        //Account 

        private string address;

        public string Address
        {
            get
            {
                if (string.IsNullOrEmpty(address))
                    address = GetStringValue("Address");
                return address;
            }
            set
            {
                UpdateKey("Address", value.ToString());
                SetProperty(ref address, value);
            }
        }


        private string companyName;

        public string CompanyName
        {
            get
            {
                if (string.IsNullOrEmpty(companyName))
                    companyName = GetStringValue("CompanyName");
                return companyName;
            }
            set
            {
                UpdateKey("CompanyName", value.ToString());
                SetProperty(ref companyName, value);
            }
        }


        private string bankName;

        public string BankName
        {
            get
            {
                if (string.IsNullOrEmpty(bankName))
                    bankName = GetStringValue("BankName");
                return bankName;
            }
            set
            {
                UpdateKey("BankName", value.ToString());
                SetProperty(ref bankName, value);
            }
        }


        private string accountName;

        public string AccountName
        {
            get
            {
                if (string.IsNullOrEmpty(accountName))
                    accountName = GetStringValue("AccountName");
                return accountName;
            }
            set
            {
                UpdateKey("AccountName", value.ToString());
                SetProperty(ref accountName, value);
            }
        }


        private string rekeningName;

        public string RekeningName
        {
            get
            {
                if (string.IsNullOrEmpty(rekeningName))
                    rekeningName = GetStringValue("RekeningName");
                return rekeningName;
            }
            set
            {
                UpdateKey("RekeningName", value.ToString());
                SetProperty(ref rekeningName, value);
            }
        }


        private string npwp;

        public string NPWP
        {
            get
            {
                if (string.IsNullOrEmpty(npwp))
                    npwp = GetStringValue("NPWP");
                return npwp;
            }
            set
            {
                UpdateKey("NPWP", value.ToString());
                SetProperty(ref npwp, value);
            }
        }

    }
}
