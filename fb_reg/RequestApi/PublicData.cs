using ActiveUp.Net.Security.OpenPGP.Packets;
using Emgu.CV.Saliency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace fb_reg.RequestApi
{
    public enum RunningStatus
    {
        RunningNormal,
        Paused,
        Probing
    }
    public static class PublicData
    {
        public static bool omitgmail = false;
        public static bool omitphone = false;
        public static bool doiHotmail = false;
        public static bool khonglaymailtrave = false;
        public static int delay = 6;
        public static int totalSuccess = 0;
        //public static int totalFailure = 0;
        public static int totalRun = 0;
        public static DateTime nextCheckTime = DateTime.MinValue;
        public static int maxRate = 85;
        public static RunningStatus RunStatus = RunningStatus.RunningNormal; //Idle, Running, Paused, Stopped
        public static int numberOfFriend = 0;
        public static bool proxyUbuntu = false;
        public static bool nameUbuntu = false;
        public static Queue<string> last100Mails = new Queue<string>();
        public static readonly Random _rng = new Random();
        public static string includeProxy = "";
        public static string exceptionProxy = "us,vn";
        public static bool exceptionusvn = false;
        public static bool vandong = false;
        public static bool unlimit = true;
        public static bool global = false;
        public static bool stopAll = false;
        public static bool needReuseMail = true;
        public static bool isSuccess = false;
        public static bool getMailCache = false;
        public static int countSuccessVeribackup = 0;
        public static bool showIP = false;
        public static bool ChangeBase64 = false;
        public static bool ForceHotmail = false;
        public static bool ForceGmail = false;
        public static DataGridView dataGridView;
        public static List<DeviceObject> listDeviceObject = new List<DeviceObject>();


        public static string CacheMailUbuntu = "http://148.113.207.13:18080";

        public static string LogProxyCountry = "http://148.113.207.13:18090";
        public static string NameServerUbuntu = "http://148.113.207.13:8000";
        public static string NameServer = "http://hes09ez92az.sn.mynetname.net:8081";
        public static string ProxyServer = "http://148.113.207.13:8010";
        public static string AvatarServer = "http://148.113.207.13:8001";
        public static string CacheServerUri = "http://hes09ez92az.sn.mynetname.net:8081";
        public static string LogServerUri = "http://hes09ez92az.sn.mynetname.net:8082";
        public static string LogHotmailServerUri = "http://hes09ez92az.sn.mynetname.net:8083";
        public static string AccessTokenSuperGmailVip = "GYTR1AOXBYVCGZBX";

        public static string AccessTokenSuperGmailNormal = "Y10UF406JFC27BEV";

        public static string AccessTokenSuperGmailCurrent = "GYTR1AOXBYVCGZBX";

        public static string AccessTokenDvgmVip = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ";
        public static string AccessTokenDvgmNormal = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ";
        public static string AccessTokenDvgmCurrent = "PtcRfCJe0UjBk4iJ2umU98ZnE7rzp0sJ";

        public static int maxMail = 1;

        public static string AccessTokenThueSimGmail = "44d568423b4d344595c6aae53337eae182f1a9bd";
        public static string UrlThuesim = "http://thuesim.app:8080";

        public static string AccessTokenShopMail9999Current = "107e65bf0ab9ed85cb4a27b5a305c0af";
        public static string AccessTokenShopMail9999Normal = "cbadb4b11fd2f0562daeca96038c78d3";
        public static string AccessTokenShopMail9999Vip = "107e65bf0ab9ed85cb4a27b5a305c0af";

        public static string AccessTokenOtpCheap = "xjPDwF4LDnnJPquFRToQ";
        public static string AccessTokengmailHvl = "HkGJioJy38Ilxpfw96ax9A2wcZxk3CMY1YeNzTyMxo";
        public static string AccessTokengmailShopgmailmmo = "he88E0i86xjL8Z27zbGk1nDDDm3vKixK";
        public static string AccessTokengmailClonenha = "83a87da6eda428457ad9e8b72dccce37GlpWHeVrTkfvUR710XY4nQoyP6zs2wLF";

        public static int soLanChoMail = 90;
        public static bool cho_mail = false;
        public static string SourceClonenha = "clonenha";   
        public static bool GetMailThuesim = false;
        public static bool GetMailThuesimVip = false;
        public static bool GetMailDvgm = false;
        public static bool GetMailDvgmNormal = false;
        public static bool GetMailSptNormal = false;
        public static bool GetMailSptVip = false;
        public static bool GetShopgmailLocal = true;
        public static bool GetHvlMaillocal = false;
        public static bool GetShopgmailmmoLocal = true;
        public static bool GetClonenhaLocal = false;
        public static bool GetGmailUnlimit = false;
        public static string TokenUnlimit = "8vnz9yfkcdsjmp6lnoosuju5990hec3jesfsq7yeiz7xwt1mgyvshouq5dt7g8exttoyan1722140071";
        public static List<string> wifilist = new List<string>();
        public static int delayAfterDie = 300000;
        public static int delayAfterReg = 300000;



        public static List<string> listIdUnlimit = new List<string>();
        public static List<string> listIdVandong = new List<string>() { "1", "2", "3"};
        public static List<string> listIdTrustVandong = new List<string>() { "5", "6", "59", "60" };
        public static List<string> listIdTrustUnlimit = new List<string>();

        public static bool tramaildie = false;
        public static bool checkMail = true;
        public static bool ThoatGmail = false;
        public static string FetchMailLog = "";
        public static System.Windows.Forms.Label PublicmaxMaillabel;
        public static System.Windows.Forms.TextBox PublicmaxThreadMailTextbox;
        public static int MaxThreadGetMail = 1;
        public static bool ChayChamLai = false;
        public static int ChayChamlaiDelay = 3000;
    }
}
