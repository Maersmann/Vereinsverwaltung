﻿using Data.Types.OptionTypes;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logic.Core.OptionenLogic
{
    public class BackendLogic
    {
        private static readonly string FILENAME = "conf.ini";

        private readonly FileIniDataParser parser;
        private IniData iniData;
        private readonly string iniPath;

        private string ip;
        private string url;
        private BackendProtokollTypes typ;
        
        public int? port;
        public BackendLogic()
        {
            ip = "";
            iniPath = "";
            port = null;
            url = "";
            typ = BackendProtokollTypes.http;

            iniPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Vereinsverwaltung\\";
            if (!Directory.Exists(iniPath))
                Directory.CreateDirectory(iniPath);
            iniPath += FILENAME;
            parser = new FileIniDataParser();
        }

        public string GetBackendIP()
        {
            return ip;
        }

        public string GetURL()
        {
            var URL = "http://";
            if (typ.Equals(BackendProtokollTypes.https))
                URL = "https://";
            if (this.url.Length == 0)
                URL += GetBackendIP();
            else
                URL += GetBackendURL();

            if (port.HasValue)
                URL += ":" + port.Value;
            return URL;
        }

        public BackendProtokollTypes GetProtokollTyp()
        {
            return typ;
        }

        public bool IstINIVorhanden()
        {
            return File.Exists(iniPath);
        }

        public void LoadData()
        {

            if (File.Exists(iniPath))
            {
                iniData = parser.ReadFile(iniPath);
                LoadBackendSettings();
            }
        }
        public void SaveData(string ip, BackendProtokollTypes protokollTyp, int? port, string url)
        {
            this.ip = ip;
            this.typ = protokollTyp;
            this.port = port;
            this.url = url;

            iniData = new IniData();
            iniData.Sections.Add("Backend-Settings");

            iniData["Backend-Settings"]["IP"] =ip;
            iniData["Backend-Settings"]["URL"] = url;
            iniData["Backend-Settings"]["Protokoll"] = Convert.ToString(Convert.ToInt32(protokollTyp));
            iniData["Backend-Settings"]["Port"] = Convert.ToString(port);

            parser.WriteFile(iniPath, iniData);
        }


        private void LoadBackendSettings()
        {
            if (IsFieldVorhanden("Backend-Settings", "IP"))
                ip = iniData["Backend-Settings"]["IP"];
            if (IsFieldVorhanden("Backend-Settings", "URL"))
                url = iniData["Backend-Settings"]["URL"];

            if ((IsFieldVorhanden("Backend-Settings", "Protokoll")) && int.TryParse(iniData.Sections["Backend-Settings"].GetKeyData("Protokoll").Value, out _))
                typ = (BackendProtokollTypes)int.Parse(iniData["Backend-Settings"]["Protokoll"]);
            if ((IsFieldVorhanden("Backend-Settings", "Port")) && int.TryParse(iniData.Sections["Backend-Settings"].GetKeyData("Port").Value, out _))
                port = int.Parse(iniData["Backend-Settings"]["Port"]);
        }

        public int? GetBackendPort()
        {
            return port;
        }
        public string GetBackendURL()
        {
            return url;
        }
        private bool IsFieldVorhanden(string section, string key)
        {
            return iniData.Sections[section].GetKeyData(key) != null;
        }
    }
}
