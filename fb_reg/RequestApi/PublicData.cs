﻿using ActiveUp.Net.Security.OpenPGP.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fb_reg.RequestApi
{
    public static class PublicData
    {
        public static bool ForceHotmail = false;
        public static bool ForceGmail = false;
        public static DataGridView dataGridView;
        public static List<DeviceObject> listDeviceObject = new List<DeviceObject>();
        public static string CacheServerUri = "http://hes09ez92az.sn.mynetname.net:8081";
        public static string AccessTokenSuperGmailVip = "Y10UF406JFC27BEV";

        public static string AccessTokenSuperGmailNormal = "GYTR1AOXBYVCGZBX";

        public static string AccessTokenSuperGmailCurrent = "GYTR1AOXBYVCGZBX";

        public static string AccessTokenDvgmVip = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ";
        public static string AccessTokenDvgmNormal = "aFRHZyHBxFrlRd3vY6V0pV2DPuyKSsDR";
        public static string AccessTokenDvgmCurrent = "aFRHZyHBxFrlRd3vY6V0pV2DPuyKSsDR";

        public static int maxMail = 1;

        public static string AccessTokenThueSimGmail = "44d568423b4d344595c6aae53337eae182f1a9bd";


        public static string AccessTokenShopMail9999Current = "107e65bf0ab9ed85cb4a27b5a305c0af";
        public static string AccessTokenShopMail9999Normal = "cbadb4b11fd2f0562daeca96038c78d3";
        public static string AccessTokenShopMail9999Vip = "107e65bf0ab9ed85cb4a27b5a305c0af";

        public static string AccessTokenOtpCheap = "xjPDwF4LDnnJPquFRToQ";
        public static string AccessTokengmailHvl = "HkGJioJy38Ilxpfw96ax9A2wcZxk3CMY1YeNzTyMxo";
        public static bool GetMailThuesim = false;
        public static bool GetMailThuesimVip = false;
        public static bool GetMailDvgm = false;
        public static bool GetMailDvgmNormal = false;
        public static bool GetMailSptLocal = false;
        public static bool GetMailSptNormal = false;
        public static bool GetShopgmailLocal = true;
        public static bool GetHvlMaillocal = true;
        public static bool GetGmailUnlimit = false;
        public static string TokenUnlimit = "8vnz9yfkcdsjmp6lnoosuju5990hec3jesfsq7yeiz7xwt1mgyvshouq5dt7g8exttoyan1722140071";
        public static List<string> wifilist = new List<string>();

        public static string ServerCacheMail = "";

        public static bool ThoatGmail = false;
        public static string FetchMailLog = "";
        public static System.Windows.Forms.Label PublicmaxMaillabel;
        public static System.Windows.Forms.TextBox PublicmaxThreadMailTextbox;
        public static int MaxThreadGetMail = 1;
    }
}
