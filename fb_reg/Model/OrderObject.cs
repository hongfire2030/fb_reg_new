using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public class OrderObject
    {
        public string proxyType = "3";
        public bool RootRom = false;
        public bool mailEdu = false;
        public bool deleteKeyProxy = false;
        public bool NAM_SERVER = false;
        public string source; // binh duong/ q7
        public string versionFb;
        public string phoneReg;
        public string phoneRegType;
        public string proxyDomain = "";
        public string pcName;
        public Proxy proxy;
        public bool upContactNew = false;
        public bool upCoverNew = false;
        public bool usDeviceLanguage = false;

        public bool changeINfo = false;
        public bool proxyWfi = false;
        public string oldType = "";
        public bool proxyFromServer = false;
        public bool proxyFromLocal = false;
        public string regPhone = "";
        public bool changePhoneNumber = false;
        public bool forceAvatarUs = false;
        public bool loginByUserPassword = false;
        public bool getHotmailKieumoi = false;
        public bool moiTruocProxy = false;
        //public bool editPortProxy = false;
        public bool hasproxy = false;
        public bool removeProxy = false;
        
        public int error_code;
        public bool loginAccMoiLite = true;

        public bool loginAccMoiKatana = true;
        public bool loginAccMoiMessenger = true;
        public bool loginAccMoiBusiness = true;
        public bool veriNhapMaXacNhan = false;
        public string accType;
        public string tempmailType;
        public string errorMessage;
        public bool pushAvatar;
        public bool pushCoverAvatar;
        public bool dauso;
        public bool carryCodePhone;
        public bool americaPhone;
        public bool prefixTextnow;
        public bool dauso12;
        public string log;
        public bool hasAddFriend = false;
        public int numberOfFriendRequest = 5;
        public bool checkAccHasAvatar = false;
        public bool checkAccHasCover = false;
        public bool checkAvatar;
        public bool isSuccess = false;
        public bool isRun = false;
        public bool isVeriOk = false;
        public bool hasOtp = false;
        public string otp = "";
        public bool nvrUpAvatar;
        public string qrCode;
        public bool isReverify;
        public bool reupFullInfoAcc;
        public bool doitenAcc;
        public bool veriNvrOutSite;
        public string uid;
        public Account account;
        public string deviceID;
        public int index;
        public string code;
        public bool hasAvatar;
        public bool hasCover;
        public bool has2Fa;
        public bool set2FaSuccess;
        public bool veriBackup = false;
        public string gender;
        public bool isHotmail;
        public string emailType;
        public int amount;
        public int currentAmount;
        public string status;
        public bool veriAcc;
        public bool veriDirectHotmail;
        public bool veriDirectGmail;
        public bool veriByPhone;
        public bool veriDirectByPhone;
        public bool checkLogin;
        public MailObject currentMail;
        public MailObject veriMail;

        public bool isEnglish;
        public string language;

        public bool verified;

        public string from;
        public string to;

        public PhoneTextNow phoneT;
        public MailRepository mailClient;
        public bool uploadContact;
        public OrderObject()
        {
            veriNhapMaXacNhan = false;
            dauso = false; 
            americaPhone = false;
            prefixTextnow = false;
            dauso12 =false;

            code = "default";
            has2Fa = false;
            hasAvatar = false;
            hasCover = false;

            isHotmail = true;
            veriAcc = false;
            veriDirectHotmail = false;
            amount = 1000000;
            currentMail = new MailObject();
            status = "Running";
            veriByPhone = false;
            veriDirectByPhone = false;
            checkLogin = false;
            phoneT = new PhoneTextNow();
            account = null;
            nvrUpAvatar = false;
            uploadContact = false;
            isSuccess = false;
            isRun = false;
            isVeriOk = false;
            set2FaSuccess = false;
            checkAccHasAvatar = false;
            numberOfFriendRequest = 5;
            hasAddFriend = false;
            log = "";
        }
    }
}
