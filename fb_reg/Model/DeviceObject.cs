using ActiveUp.Net.Security.OpenPGP.Packets;
using System;

namespace fb_reg
{
    public class DeviceObject
    {
        public string otp1;
        public string currentIp;
        public string owner = "";
        public string currentRom = "";
        public bool UpdateRom = false;
        public bool ForceUpdateRom = false;
        public string changeRom = "";
        public bool needRebootAfterClear = false;
        public int currentProxyCount = 0;
        public Proxy currentProxy;
        public int countSuccess;
        public bool VeriOk = false;
        public OrderObject order;
        public bool change2UsLanguage = false;
        public bool change2VnLanguage = true;
        public string ipLan;
        public string macAddress;
        public bool reInstallFbAfterChangeName = false;
        public string fbVersion = "";
        public bool getIpType = false;
        public bool showVersion = false;
        public bool randomVersion = false;
        public bool randomVersionSauDie = false;
        public bool chuyenQuaMoiKatana = false;
        public bool chuyenQuaVeriHotmail = false;
        public bool chuyenQuaRegNvr = false;
        public bool chuyenQuaVeriGmail = false;
        public bool chuyenProxy2P1 = false;
        public bool chuyenVeri4g = false;

        public bool installFb = false;
        public bool updateFb = false;
        public bool installFb449 = false;
        public bool installLatestFb = false;
        public string info;
        public bool loadNewProxy = false;
        //public bool editPortProxy = true;
        public bool fistTime = true;
        public bool running = true;
        public int onOffSimCount = 0;
        public string realSim;
        public string simStatus;
        public string emuStatus;
        public string allEmuStatus;
        public string log;
        public bool isFinish;
        public bool isReady;
        public int index;
        public string deviceId;
        public string deviceIP;
        public string status;
        public string adbStatus;
        public int duration;
        public int totalInHour;
        public int successInHour;
        public bool isSuccess = false;
        public int noveri;
        public string startTime;
        public int globalTotal;
        public int cycle;
        public int globalSuccess;
        public double percentInHour;
        public int blockCount;
        public int blockCountOtp;
        public int runningStatus;
        public bool clearCache = true;
        public bool clearCacheLite = false;
        public DateTime startLock;
        public string network;
        public string networkStatus;
        public string currentPublicIp;
        public string currentIpInfo;
        //public string currentIPType;
        public string currentMobileReg;
        public bool changeSim = false;
        public string devicePhone = "";
        
        public string newSim;
        public string action;
        public string reVerifyAcc;
        
        public bool isBlocking = false;
        public bool regByMail;
        public string keyProxy;
        
        public Proxy proxyDevice;
        public bool isRunProxy = false;
        public int reInstallFbLite = 0;
        public int reInstallBusiness = 0;
        public int reInstallFb = 0;
        public int resetCount = 0;
        public int changeSim2Count = 0;
        public int clearCacheFailCount = 0;
        public int clearCacheLiveCount = 0;
        public string androidId = "";
        public string currentStatus;
        public bool pushAccMoi = true;
        public string regStatus = "";
        public string regByProxy = "";
        public Account accMoi;
        public int restartCount = 0;
        public bool isProxyRuning = false;
        public int numberClearAccSetting = 0;

        public int veriNvrFailCount = 0;
    }
}
