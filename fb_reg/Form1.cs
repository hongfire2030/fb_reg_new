using HttpRequest;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using static fb_reg.Utility;
using static fb_reg.Form1Util;
using OtpNet;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Rssdp;
using static fb_reg.ServerApi;

namespace fb_reg
{
    public partial class Form1 : Form
    {
        static string zuesProxyKey = "";
        int totalRun = 0;
        int fail = 0;
        int regOk = 0;
        int veriOk = 0;
        int accDieCaptcha = 0;
        double percent = 0;
        int regNvrOk = 0;
        int totalSucc = 0;
        string statusSim = "";
        int openFacebookFailCount = 0;
        int noVerified = 0;
        DateTime localDate = DateTime.Now;
        public bool isServer;
        int numberOfPhoneCodeTextnow = 0;
        int numberOfPhoneOtp = 0;
        public string[] prefixTextNow;
        string selectedDeviceID;
        int countdown = 50;
        string selectedDevice;

        Dictionary<string, string> checkMail = new Dictionary<string, string>();
        List<DeviceObject> listDeviceObject;

        string currentSim2 = Constant.VIETNAM_MOBILE;
        List<string> contacts;
        int WaitAddContactCount = 80;
        int numberContact = 90;
        string activeDuoiMail = "";
        int otp = 0;




        static CountdownEvent countdown1;
        static int upCount = 0;
        static object lockObj = new object();
        const bool resolveNames = true;

        public Form1()
        {
            InitializeComponent();
            dataGridView.CellContentClick += dgv_CellContentClick;
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            codeKeyTextNowTextBox.Text = Properties.Settings.Default.cookie;
            cookieCodeTextNowtextBox.Text = Properties.Settings.Default.cookieCodeTextNow;
            otpKeyTextBox.Text = Properties.Settings.Default.otpmmoKey;
            drkKeyTextBox.Text = Properties.Settings.Default.drkKey;
            drkDomainTextbox.Text = Properties.Settings.Default.drkDomain;
            statusTextBox.Text = Properties.Settings.Default.statusTextbox;
            statusSim = Properties.Settings.Default.statusTextbox;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.serverapi))
            {
                serverPathTextBox.Text = Properties.Settings.Default.serverapi;
            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.serverCacheLocal))
            {
                serverCacheMailTextbox.Text = Properties.Settings.Default.serverCacheLocal;
            }

            tmProxyTextBox.Text = Properties.Settings.Default.tmProxyKey;
            dtProxyTextBox.Text = Properties.Settings.Default.dtProxy;
            activeDuoiMailtextBox.Text = Properties.Settings.Default.activeDuoiMail;
            activeDuoiMail = activeDuoiMailtextBox.Text;
            Device.ExecuteCMD("adb devices");

            
        }
    
    
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            timerAvailableSellGmail.Start();
            changeSim2Timer.Start();
            startStoptimer.Start();
            randPhone2Typetimer.Start();
            countAccMoiTimer.Start();
            checkVeriTimer.Start();
            resetDuoiMailtimer.Start();
            CheckActionSpeed();
            isServer = true;

            listDeviceObject = new List<DeviceObject>();
            GoogleSheet.Initial();

            List<string> listDevices___ = Device.GetDevices();
            if (runBoxLancheckBox.Checked)
            {
                listDevices___ = Device.GetLanDevices(ipRangeLantextBox.Text);
            }
            List<string> listDevices = listDevices___;
            if (listDevices___ != null)
            {
                //listDevices = listDevices___.OrderBy(q => q).ToList();
                listDevices.Sort();

            }
            List<string> tamp = new List<string>();
            List<string> tamp2 = new List<string>();
            for (int i = 0; i < listDevices.Count; i++)
            {
                if (listDevices[i].Length > 19)
                {
                    tamp2.Add(listDevices[i]);

                }
                else
                {
                    tamp.Add(listDevices[i]);
                }
            }
            listDevices.Clear();
            for (int i = 0; i < tamp.Count; i++)
            {
                listDevices.Add(tamp[i]);
            }
            for (int i = 0; i < tamp2.Count; i++)
            {
                listDevices.Add(tamp2[i]);
            }

            if (listDevices != null && listDevices.Count > 0)
            {
                selectedDevice = (string)listDevices[listDevices.Count - 1];
                for (int i = 0; i < listDevices.Count; i++)
                {

                    DeviceObject device = new DeviceObject();

                    string temp = listDevices[i];
                    device.deviceId = temp;
                    device.status = "Initial";
                    device.isFinish = true;
                    device.changeSim = false;
                    device.clearCacheLite = false;
                    device.clearCacheFailCount = 2;
                    device.clearCacheLiveCount = 2;
                    device.currentStatus = Constant.REG;
                    device.proxy = new Proxy();
                    listDeviceObject.Add(device);

                    device.allEmuStatus = Properties.Settings.Default.allEmuStatus;
                    device.simStatus = Properties.Settings.Default.simStatus;
                    device.emuStatus = Properties.Settings.Default.emuStatus;
                    device.numberClearAccSetting = 0;
                }
            }

            InitialData(listDeviceObject);

            timeZoneComboBox.SelectedIndex = 0;
            shoplikeTextBox1.Text = Properties.Settings.Default.ShoplikeKey;
            tinsoftTextBox.Text = Properties.Settings.Default.TinsoftKey;
            FastProxyTextBox.Text = Properties.Settings.Default.FastproxyKey;
            tinProxyTextBox.Text = Properties.Settings.Default.tinproxyKey;
            dtProxyTextBox.Text = Properties.Settings.Default.dtProxy;
            drkDomainTextbox.Text = Properties.Settings.Default.drkDomain;
            fixPasswordtextBox.Text = Properties.Settings.Default.FixPassword;
            zuesProxyKeytextBox.Text = Properties.Settings.Default.zuesProxyKey;
            zuesProxyKey = Properties.Settings.Default.zuesProxyKey;

            prefixTextNow = File.ReadAllLines("prefix_phone_codetextnow.txt");
            CARRY_CODE = File.ReadAllLines("data/carrycode.txt").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            Control.CheckForIllegalCrossThreadCalls = false;
            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //string add = "D’Alia";

            //const string WithoutUnicodeCharacter = "an ANSI character:Æ";

            //bool hasUnicode;
            //var regexItem = new Regex("^[a-zA-Z ]*$");

            //bool cc = regexItem.IsMatch(add);
            ////true
            //hasUnicode = ContainsUnicodeCharacter(add);
            //Console.WriteLine(hasUnicode);

            //false
            //hasUnicode = ContainsUnicodeCharacter(WithoutUnicodeCharacter);
            //generateName();

            //ServerApi.GetAllPopularName();
            serverOnlineCheckBox.Checked = true;
            LoadFbVersion();
        }
        public bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;

            return input.Any(c => c > MaxAnsiCode);
        }
        static string _generateFileName(int sequence)
        {
            DateTime currentDateTime = DateTime.Now;
            return string.Format("{0}-{1:000}-{2:000}.eml",
                currentDateTime.ToString("yyyyMMddHHmmss", new CultureInfo("en-US")),
                currentDateTime.Millisecond,
                sequence);
        }


        public bool StartProxy(OrderObject order, DeviceObject device)
        {
            string deviceID = device.deviceId;
            if (order.hasproxy)
            {
                TurnOffAirPlane(deviceID, false);
                if (!proxy4GcheckBox.Checked && !deviceID.Contains(":"))
                {   // Check wifi before
                    string ssid = "";
                    for (int i = 0; i < 4; i++)
                    {
                        ssid = GetWifiName(deviceID);
                        LogStatus(device, "Wifi:" + ssid);
                        if (!ssid.Contains("unknown"))
                        {
                            break;
                        }
                        Thread.Sleep(10000);
                        ssid = GetWifiName(deviceID);
                        LogStatus(device, "Wifi:" + ssid);
                        if (!ssid.Contains("unknown"))
                        {
                            break;
                        }
                        else
                        {
                            Device.DisableWifi(deviceID);
                            Thread.Sleep(1000);
                            Device.EnableWifi(deviceID);
                            Device.OpenWifiSetting(deviceID);
                            LogStatus(device, "Bật wifi lại - chờ láy ip");
                            Thread.Sleep(20000);
                            ssid = GetWifiName(deviceID);
                            LogStatus(device, "Wifi:" + ssid);
                            if (!ssid.Contains("unknown"))
                            {
                                break;
                            }
                        }
                        if (i == 3)
                        {
                            LogStatus(device, "Can not access wifi");
                            return false;
                        }
                        Thread.Sleep(10000);
                    }
                    ssid = GetWifiName(deviceID);
                    if (ssid.Contains("unknown"))
                    {
                        LogStatus(device, "Không Thể start proxy - Chạy lại");
                        return false;
                    }
                }

                if (order.proxyWfi)
                {
                    return true;
                }
                if (proxy4GcheckBox.Checked)
                {
                    Device.DisableWifi(deviceID);
                }
                if (!order.proxyWfi && device.globalTotal % 50 == 0)
                {
                    LogStatus(device, "Chay set proxy mới   -tttttttttttttt-----------");
                    if (!SetProxySuperProxy(device))
                    {
                        LogStatus(device, "Can not set proxy ----ttttttttt-------return ");
                        Thread.Sleep(6000);
                        device.loadNewProxy = true;
                        return false;
                    }
                    else
                    {

                        device.loadNewProxy = false;
                    }
                }

                if (proxyCMDcheckBox.Checked)
                {
                    string proxyPort = device.proxy.host + ":" + device.proxy.port;
                    Device.SetProxyCmd(deviceID, proxyPort);
                } else
                {

                    bool runProxy = true;

                    //if (order.isReverify)
                    //{
                    //    if (device.globalTotal % 2 == 0)
                    //    {
                    //        runProxy = true;
                    //    } else
                    //    {
                    //        runProxy = false;
                    //    }
                    //} else
                    //{
                    //    runProxy = true;
                    //}

                    if (runProxy)
                    {
                        if (device.loadNewProxy)
                        {
                            LogStatus(device, "Chay set proxy mới   -tttttttttttttt-----------");

                            if (!SetProxySuperProxy(device))
                            {
                                LogStatus(device, "Can not set proxy ----ttttttttt-------return ", 6000);
                                device.loadNewProxy = true;
                                return false;
                            }
                            else
                            {

                                device.loadNewProxy = false;
                                return true;
                            }
                        } else
                        {
                            if (device.globalTotal != 0 && needEditProxycheckBox.Checked && !EditProxySuperProxy(device))
                            {
                                LogStatus(device, "Can not EDIT proxy ----ttttttttt-------return ", 6000);
                                device.loadNewProxy = true;
                                return false;
                            }
                            else
                            {

                                device.loadNewProxy = false;
                                return true;
                            }
                        }
                    }
                }

                if (!superProxycheckBox.Checked && s5ProxyradioButton.Checked)
                {
                    string domain = "";
                    if (tinsoftRadioButton.Checked)
                    {
                        domain = ProxyDomain.Tinsoft.ToString();
                    }
                    else if (fastProxyRadioButton.Checked)
                    {
                        domain = ProxyDomain.fastProxy.ToString();
                    }
                    else if (zuesProxyradioButton.Checked)
                    {
                        domain = ProxyDomain.zuesProxy.ToString();
                    }
                    else if (zuesProxy4G.Checked)
                    {
                        domain = ProxyDomain.zuesProxy4G.ToString();
                    }
                    else if (tunProxyradioButton.Checked)
                    {
                        domain = ProxyDomain.tunProxy.ToString();
                    }
                    else if (impulseradioButton.Checked)
                    {
                        domain = ProxyDomain.impulseProxy.ToString();
                    }
                    else if (shopLike1RadioButton.Checked)
                    {
                        domain = ProxyDomain.Softlike.ToString();
                    }
                    else if (tinProxyRadioButton.Checked)
                    {
                        device.currentPublicIp = GetPublicIp(device);
                        dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
                        if (string.IsNullOrEmpty(device.currentPublicIp))
                        {
                            LogStatus(device, "Gỡ proxy khi không lấy được ip");

                            RemoveAllProxy(deviceID);
                        }
                        domain = ProxyDomain.TinProxy.ToString();
                    }
                    else if (tmProxyRadioButton.Checked)
                    {
                        domain = ProxyDomain.TmProxy.ToString();
                    }
                    else if (dtProxyRadioButton.Checked)
                    {
                        device.currentPublicIp = GetPublicIp(device);
                        dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
                        if (string.IsNullOrEmpty(device.currentPublicIp))
                        {
                            // TODO
                        }
                        domain = ProxyDomain.dtProxy.ToString();
                    }
                    int proxyTime = 4;
                    if (getProyx20timecheckBox.Checked)
                    {
                        proxyTime = 20;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        LogStatus(device, "Getting proxy ... lần : " + (i + 1));
                        if (device.proxy != null && device.proxy.hasProxy)
                        {
                            break;
                        }
                        device.proxy = Utility.GetProxy(domain, device.keyProxy, device.currentPublicIp, locationProxyTextBox.Text);
                        // Handle timeout
                        if (device.proxy != null && device.proxy.isWait)
                        {
                            LogStatus(device, "Proxy timeout:" + device.proxy.timeout);
                            Thread.Sleep(device.proxy.timeout * 1000 + 5000);
                            device.proxy = Utility.GetProxy(domain, device.keyProxy, device.currentPublicIp, locationProxyTextBox.Text);
                        }
                        if (device.proxy != null && device.proxy.hasProxy)
                        {
                            break;
                        }
                        Thread.Sleep(3000);
                    }

                    if (device.proxy != null && !device.proxy.hasProxy)
                    {
                        LogStatus(device, "error:" + device.proxy.message);


                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DeepPink;
                        Thread.Sleep(10000);
                    }
                }

                if (proxy4GcheckBox.Checked)
                {
                    Device.DisableWifi(deviceID);
                }
            }
            else
            {
                Device.DisableWifi(deviceID);
            }
            return true;
        }
        public OrderObject PreProcess(DeviceObject device, OrderObject order)
        {

            try
            {
                if (device.blockCount >= Convert.ToInt32(maxAccBlockRuntextBox.Text))
                {
                    LogStatus(device, "Xóa cache fblite và Acc in setting");
                    OverLockAction(device, order);
                }
            }
            catch (Exception ex)
            {
                LogStatus(device, ex.Message);
            }

            string serverIp = ServerApi.GetServerIp(serverCacheMailTextbox.Text);
            if (!string.IsNullOrEmpty(serverIp) && serverIp != serverPathTextBox.Text)
            {
                serverPathTextBox.Text = serverIp;
            }

            order.moiTruocProxy = moiTruocProxycheckBox.Checked;
            //Device.ForceStop(device.deviceId, "com.android.vending");
            //Device.ForceStop(device.deviceId, "com.google.android.gms");
            //Device.ForceStop(device.deviceId, "com.google.android.youtube");
            //Device.ForceStop(device.deviceId, "com.google.android.gsf");
            //Device.ForceStop(device.deviceId, "com.google.android.gsf.login");
            //Device.ForceStop(device.deviceId, "com.android.chrome");
            //Device.ForceStop(device.deviceId, "com.google.android.ext.services");
            //Device.ForceStop(device.deviceId, "com.google.android.googlequicksearchbox");
            LogStatus(device, "Chạy preprocesss-------------");



            Phone.CODE_TEXT_NOW_KEY = codeKeyTextNowTextBox.Text;
            Phone.OTPMMO_KEY = otpKeyTextBox.Text;
            Phone.DRK_KEY = drkKeyTextBox.Text;

            Utility.ghiChuTrenAvatar = gichuTrenAvatarcheckBox.Checked;
            device.regByProxy = "";
            if (device == null)
            {
                order.error_code = -1;
                return order;
            }
            string deviceID = device.deviceId;
            //GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi);
            if (device.installFb)
            {
                LogStatus(device, "Install facebook " + fbVersioncomboBox.SelectedItem.ToString());
                ReInstallFb(deviceID, false);
                device.installFb = false;
            } else if (device.installFb449)
            {
                LogStatus(device, "Install fb 449  ----------------");
                InstallFb449(device);
                device.installFb449 = false;
            } else if (device.installLatestFb)
            {
                LogStatus(device, "Install latest facebook ----------------");
                InstallLatestFb(device);
                device.installLatestFb = false;
            } else if (device.updateFb)
            {
                LogStatus(device, "Install update facebook " + fbVersioncomboBox.SelectedItem.ToString());
                ReInstallFb(deviceID, true);
                device.updateFb = false;
            }

            if (reinstallBusinesscheckBox.Checked && device.reInstallBusiness > Convert.ToInt32(reinstallFbliteTextbox.Text))
            {
                if (isScreenLock(deviceID) && !holdingCheckBox.Checked)
                {
                    LogStatus(device, "Screen is locking screen - Opening it");
                    Device.Unlockphone(deviceID);
                }
                LogStatus(device, "Reinstall Businessssssssssss", 1000);
                if (!FbUtil.ReinstallBusinsess(deviceID))
                {
                    LogStatus(device, "Không thể install business ---- kiểm tra", 20000);
                }
                device.reInstallBusiness = 0;
                device.showVersion = true;
            }

            device.reInstallBusiness++;

            if (reinstallFbLiteCheckBox.Checked && device.reInstallFbLite > Convert.ToInt32(reinstallFbliteTextbox.Text))
            {
                LogStatus(device, "reboot device ------------------------------");
                Device.RebootByCmd(deviceID);
                Thread.Sleep(65000);

                if (isScreenLock(deviceID) && !holdingCheckBox.Checked)
                {
                    LogStatus(device, "Screen is locking screen - Opening it");
                    Device.Unlockphone(deviceID);
                }
                LogStatus(device, "Reinstall Fblite");
                FbUtil.ReinstallFblite(deviceID);
                device.reInstallFbLite = 0;
                device.showVersion = true;
            }

            device.reInstallFbLite++;

            if (reinstallFbCheckBox.Checked && device.reInstallFb >= Convert.ToInt32(reinstallFbCountTextBox.Text))
            {

                if (doitenVncheckBox.Checked)
                {
                    LogStatus(device, "Reinstall Facebook 449 ");
                    InstallFb449(device);
                }
                else
                {
                    LogStatus(device, "Reinstall Facebook " + fbVersioncomboBox.SelectedItem.ToString());
                    ReInstallFb(deviceID);
                }

                device.reInstallFb = 0;
                device.showVersion = true;
            }

            device.restartCount++;
            device.reInstallFb++;

            if (installfblitecheckBox.Checked)
            {
                FbUtil.InstallMissingFbLite(deviceID);
            }
            if (installMissingMessengercheckBox.Checked)
            {
                FbUtil.InstallMissingMessenger(deviceID);
            }

            FbUtil.InstallMissingBusiness(deviceID);
            Device.SelectLabanKeyboard(deviceID);

            if (isScreenLock(deviceID) && !holdingCheckBox.Checked)
            {
                LogStatus(device, "Screen is locking screen - Opening it");
                Device.Unlockphone(deviceID);
            }

            if (order.proxyFromServer)
            {
                LogStatus(device, "Get proxy from server");

                Proxy pp = GetProxyFromServer();
                if (pp != null)
                {

                    Device.EnableWifi(deviceID);
                    device.keyProxy = "Host:" + pp.host + "  -Port:" + pp.port;
                    order.hasproxy = true;
                    if (device.proxy == null || device.proxy.host != pp.host)
                    {
                        device.loadNewProxy = true;
                    }
                    device.proxy = pp;
                    
                }
                else
                {
                    device.keyProxy = "";
                    Device.DisableWifi(deviceID);
                }

            } else if (order.proxyWfi)
            {
                StopProxySuper(device);
                Device.EnableWifi(deviceID);
                Proxy pp = new Proxy();
                pp.host = "1.1.1.1";
                pp.port = "1111";
                device.keyProxy = "Host:" + pp.host + "  -Port:" + pp.port;
                order.hasproxy = true;
                if (device.proxy == null || device.proxy.host != pp.host)
                {
                    device.loadNewProxy = true;
                }
                device.proxy = pp;
            }
            else
            {
                StopProxySuper(device);
                device.keyProxy = "";
                if ((tunProxyradioButton.Checked || s5ProxyradioButton.Checked) && device.proxy != null && device.proxy.hasProxy)
                {
                    order.hasproxy = true;
                }
                else
                {
                    Device.DisableWifi(deviceID);
                }
            }
            if (!order.moiTruocProxy)
            {
                if (!StartProxy(order, device))
                {
                    Device.DisableWifi(deviceID);
                    order.error_code = -1;
                    return order;
                }
                else
                {
                    if (showIpcheckBox.Checked)
                    {
                        device.currentPublicIp = Device.GetPublicIpSmartProxy(deviceID);
                        dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
                    }
                }
            }
            device.regStatus = "";

            if (device.index == 0) statusTextBox.Text = statusSim;

            device.pushAccMoi = false;
            if (accMoiCheckBox.Checked) device.pushAccMoi = true;

            Utility.unsignText = unsignCheckBox.Checked;
            Utility.adbKeyboard = adbKeyCheckBox.Checked;
            Utility.labanKeyboard = true;
            Utility.inputString = inputStringCheckbox.Checked;



            if (!randomMailPhoneCheckBox.Checked && !randomMailPhoneSimCheckBox.Checked)
            {
                device.regByMail = regByMailCheckBox.Checked;
            }

            if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
            {
                LogStatus(device, "Screen is locking screen - Opening it");
                Device.Unlockphone(deviceID);
            }
            if (device.status.Contains(Constant.DEEMED))
            {
                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Coral;
                try
                {
                    int breakTime = Convert.ToInt32(timebreakDeadLocktextBox.Text);

                    device.startLock = DateTime.Now;
                    int timebreak = Convert.ToInt32(timebreakDeadLocktextBox.Text);

                    LogStatus(device, "Fb đang khóa, tạm nghỉ " + timebreak + " từ:" + DateTime.Now.GetDateTimeFormats('T')[0]);

                    dataGridView.Rows[device.index].Cells[6].Value = false;

                    device.blockCount = 0;
                    string dddd = ChangeSimAction(device);
                    if (!string.IsNullOrEmpty(dddd))
                    {
                        dataGridView.Rows[device.index].Cells[14].Value = dddd;
                    }
                    order.error_code = -1;
                    return order;
                }
                catch (Exception e)
                {
                    LogStatus(device, "exception:" + e.Message.ToString());
                }
            }

            if (vietCheckbox.Checked)
            {
                Language.language = Constant.LANGUAGE_VN;
            }
            else
            {
                Language.language = Constant.LANGUAGE_US;
            }




            LogStatus(device, "Clear app facebook");
            FbUtil.ClearCacheFb(device, oldClearCachecheckBox.Checked);
            LogStatus(device, "Clear app facebook -----xong");

            if (findPhonecheckBox.Checked)
            {
                Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);

                LogStatus(device, "Tìm số dt chưa có tk fb --------");

                if (!WaitAndTapXML(deviceID, 20, "descbạnquênmậtkhẩuư\\?checkablefal"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 59.2, 69.6);
                }
                WaitAndTapXML(deviceID, 4, "từ chối");
                WaitAndTapXML(deviceID, 3, "từ chối");
                if (CheckTextExist(deviceID, "tìm tài khoản", 2))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        // Get phone from server
                        order.regPhone = ServerApi.GetPhones(true, device.network);

                        InputText(deviceID, order.regPhone, true);

                        WaitAndTapXML(deviceID, 3, "tiếp tục");
                        Thread.Sleep(4000);
                        if (CheckTextExist(deviceID, new string[] { "tiếp tục", "thử cách khác", "chọn tài khoản" }))
                        //if (CheckTextExist(deviceID, "thử cách khác", 3))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 7.1, 6.9); //
                            Thread.Sleep(1000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.7, 25.1);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (WaitAndTapXML(deviceID, 2, "tạotàikhoảnmớiresourceid"))
                    {
                        WaitAndTapXML(deviceID, 2, "bắt đầu");
                        Thread.Sleep(2000);
                    }
                }

            }

            if (randomNewContactCheckBox.Checked || randomOldContactCheckBox.Checked)
            {
                LogStatus(device, "Random contact ");
                RandomContact(deviceID);
                bool check = CheckTextExist(deviceID, new string[] { "thêmtàikhoản", "add" });
                Device.Home(deviceID);
                if (check)
                {
                    LogStatus(device, "Không thể thêm danh bạ - chạy lại");
                    order.error_code = -1;
                    return order;
                }

            }

            ActionHandle(device); // PreProcess

            if (deviceFakerPlusCheckBox.Checked)
            {
                FbUtil.FakerPlusChange(deviceID, rebootFakerpluscheckBox.Checked);
            }


            Device.Home(deviceID);

            if (showFbVersionCheckBox.Checked || device.showVersion)
            {
                string fbVersion = Device.GetVersionFB(deviceID);
                string fbLiteVersion = Device.GetVersionFBLite(deviceID);
                string fbBusinessVersion = Device.GetVersionFBBusiness(deviceID);
                string version = "fb:" + fbVersion + "-lite:" + fbLiteVersion + "-Business:" + fbBusinessVersion;
                dataGridView.Rows[device.index].Cells[10].Value = version;
                device.showVersion = false;
            }
            if (clearCacheFBcheckBox.Checked)
            {
                int maxNumberClearAccSetting = 5;
                try
                {
                    maxNumberClearAccSetting = Convert.ToInt32(numberClearAccSettingTextBox.Text);
                }
                catch (Exception ddddd)
                {

                }
                if (device.numberClearAccSetting >= maxNumberClearAccSetting)
                {
                    LogStatus(device, "Clear Account facebook in setting");
                    FbUtil.ClearAccountFbInSetting(deviceID, clearAllAccSettingcheckBox.Checked);
                    device.numberClearAccSetting = -1;
                }
                device.numberClearAccSetting++;
            }

            if (isScreenLock(deviceID) && !holdingCheckBox.Checked)
            {
                LogStatus(device, "Screen is locking screen - Opening it");
                Device.Unlockphone(deviceID);
            }

            string ddd = ChangeSimAction(device);
            if (!string.IsNullOrEmpty(ddd))
            {
                dataGridView.Rows[device.index].Cells[14].Value = ddd;
            }
            if (device.globalTotal % 7 == 0 && changer60checkBox.Checked && Device.CheckAppInstall(deviceID, "com.phoneinfo.changerpro"))
            {
                LogStatus(device, "Change infor device");

                Device.ClearCache(deviceID, "com.phoneinfo.changerpro");
                Device.PermissionAppCallPhone(deviceID, "com.phoneinfo.changerpro");
                Device.PermissionAppReadContact(deviceID, "com.phoneinfo.changerpro");
                Device.PermissionAppReadPhoneState(deviceID, "com.phoneinfo.changerpro");

                Device.OpenApp(deviceID, "com.phoneinfo.changerpro");

                WaitAndTapXML(deviceID, 2, "changerproidedtimeiclassandroid");
                if (!WaitAndTapXML(deviceID, 2, "randomresourceidcomphoneinfochangerproidranandroidi"))
                {
                    // tap random
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.2, 28.2);
                }
                Device.Back(deviceID);
                if (!WaitAndTapXML(deviceID, 2, "applyclassandroid"))
                {
                    LogStatus(device, "Không the bật changer 60");
                    Thread.Sleep(30000);
                    return order;
                }

                Device.RebootDevice(deviceID);
                
                LogStatus(device, "Changer 60 phone finish");
            }

            if (clearFbLiteCheckBox.Checked && openfblitecheckBox.Checked)
            {
                FbUtil.ClearCacheFbLite(deviceID, clearAccSettingcheckBox.Checked);
                Thread.Sleep(1000);
                Device.ClearCache(deviceID, Constant.MESSENGER_PACKAGE);

                Thread.Sleep(1000);
                Device.OpenApp(deviceID, Constant.FACEBOOK_LITE_PACKAGE);
                LogStatus(device, "Clear cache Messenger, fb lite");
                Thread.Sleep(5000);
                Utility.WaitAndTapXML(deviceID, 2, Language.AllowAll());
                Utility.WaitAndTapXML(deviceID, 2, Language.AllowAll());
            }

            if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
            {
                LogStatus(device, "Screen is locking screen - Opening it");
                Device.Unlockphone(deviceID);
            }




            device.isBlocking = false;
LOGIN_FB_LITE:
            if (order.loginAccMoiLite)
            {
                LogRegStatus(dataGridView, device, "Mồi Fbliteeeeee");
                LogStatus(device, "Login acc moi fblite");
                Device.KillApp(deviceID, Constant.FACEBOOK_LITE_PACKAGE);
                Device.ClearCache(deviceID, Constant.FACEBOOK_LITE_PACKAGE);

                device.accMoi = GetAccMoi();

                if (device.accMoi != null)
                {
                    Device.OpenApp(deviceID, Constant.FACEBOOK_LITE_PACKAGE);
                    Thread.Sleep(10000);


                    if (order.usDeviceLanguage)
                    {
                        if (WaitAndTapXML(deviceID, 3, "mobilenumberoremailcheckable"))
                        {
                            Thread.Sleep(1000);
                            InputText(deviceID, device.accMoi.uid, true);
                            WaitAndTapXML(deviceID, 1, "passwordcheckablef");
                            Thread.Sleep(1000);
                            InputText(deviceID, device.accMoi.pass, true);
                            WaitAndTapXML(deviceID, 2, "logincheckable");
                            LogStatus(device, "đăng nhập fblite giao diện mới thành công --------------------", 10000);
                        }
                        else
                        {
                            WaitAndTapXML(deviceID, 10, "Allowresource");
                            if (CheckImageExist(deviceID, FBLITE_DANG_NHAP_IMG))
                            {
                                if (!LoginFbLite(deviceID, device.accMoi))
                                {

                                }
                            }
                            else
                            {

                                LogStatus(device, "Giao diện mới fblite", 1000);
                                if (WaitAndTapXML(deviceID, 1, "mobilenumberoremailcheckable"))
                                {
                                    Thread.Sleep(1000);
                                    InputText(deviceID, device.accMoi.uid, true);
                                    WaitAndTapXML(deviceID, 1, "passwordcheckablef");
                                    Thread.Sleep(1000);
                                    InputText(deviceID, device.accMoi.pass, true);
                                    WaitAndTapXML(deviceID, 2, "logincheckable");
                                    LogStatus(device, "đăng nhập fblite giao diện mới thành công --------------------", 10000);

                                }
                                else
                                {
                                    // gỡ tài khoản
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.8, 16.3);
                                    Thread.Sleep(1200);
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 31.2, 28.7);
                                    Thread.Sleep(1200);
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 67.4, 54.8);
                                    WaitAndTapXML(deviceID, 10, Language.AllowAll());
                                    Thread.Sleep(1200);
                                    if (CheckImageExist(deviceID, FBLITE_DANG_NHAP_IMG))
                                    {
                                        LoginFbLite(deviceID, device.accMoi);
                                    }
                                    else
                                    {
                                        if (EpAccMoicheckBox.Checked)
                                        {
                                            LogStatus(device, "không thể mở fblite", 5000);
                                            order.error_code = Constant.CAN_NOT_OPEN_FB_LITE_CODE;
                                            return order;
                                        }
                                    }
                                }
                            }
                        }
                    } else
                    {
                        if (WaitAndTapXML(deviceID, 1, "sốdiđộnghoặcemailcheckable"))
                        {
                            Thread.Sleep(1000);
                            InputText(deviceID, device.accMoi.uid, true);
                            WaitAndTapXML(deviceID, 1, "mậtkhẩucheckablef");
                            Thread.Sleep(1000);
                            InputText(deviceID, device.accMoi.pass, true);
                            WaitAndTapXML(deviceID, 2, "đăng nhập checkable");
                            LogStatus(device, "đăng nhập fblite giao diện mới thành công --------------------", 10000);
                        }
                        else
                        {
                            WaitAndTapXML(deviceID, 10, Language.AllowAll());
                            if (CheckImageExist(deviceID, FBLITE_DANG_NHAP_IMG))
                            {
                                if (!LoginFbLite(deviceID, device.accMoi))
                                {

                                }
                            }
                            else
                            {

                                LogStatus(device, "Giao diện mới fblite", 1000);
                                if (WaitAndTapXML(deviceID, 1, "sốdiđộnghoặcemailcheckable"))
                                {
                                    Thread.Sleep(1000);
                                    InputText(deviceID, device.accMoi.uid, true);
                                    WaitAndTapXML(deviceID, 1, "mậtkhẩucheckablef");
                                    Thread.Sleep(1000);
                                    InputText(deviceID, device.accMoi.pass, true);
                                    WaitAndTapXML(deviceID, 2, "đăng nhập checkable");
                                    LogStatus(device, "đăng nhập fblite giao diện mới thành công --------------------", 10000);

                                }
                                else
                                {
                                    // gỡ tài khoản
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.8, 16.3);
                                    Thread.Sleep(1200);
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 31.2, 28.7);
                                    Thread.Sleep(1200);
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 67.4, 54.8);
                                    WaitAndTapXML(deviceID, 10, Language.AllowAll());
                                    Thread.Sleep(1200);
                                    if (CheckImageExist(deviceID, FBLITE_DANG_NHAP_IMG))
                                    {
                                        LoginFbLite(deviceID, device.accMoi);
                                    }
                                    else
                                    {
                                        if (EpAccMoicheckBox.Checked)
                                        {
                                            LogStatus(device, "không thể mở fblite", 5000);
                                            order.error_code = Constant.CAN_NOT_OPEN_FB_LITE_CODE;
                                            return order;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.5, 89.5); // ok
                }
                else
                {
                    LogStatus(device, "Không thể lấy acc mồi-----------------", 5000);
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Goldenrod;
                    if (EpAccMoicheckBox.Checked)
                    {

                        order.error_code = Constant.CAN_NOT_GET_ACC_CODE;

                        Thread.Sleep(3000);
                        return order;
                    }
                }
            }
            if (order.loginAccMoiKatana)
            {
            LOGIN_KATANA:
                LogRegStatus(dataGridView, device, "Mồi Katanaaaaaaaaaaaaa");
                LogStatus(device, "Login acc moi katana");

                if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                {
                    LogStatus(device, "--- Can not open facebook->  try again 1");
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

                    if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                    {
                        LogStatus(device, "Can not open facebook -> try again 2 - > reboot device - chờ 5 phút");

                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.BlueViolet;

                        LogStatus(device, "Check fb install and fblite install");
                        if (!Device.CheckAppInstall(deviceID, Constant.FACEBOOK_PACKAGE))
                        {
                            LogStatus(device, "Fb not install -> install it");
                            if (!FbUtil.InstallMissingFb(deviceID))
                            {
                                LogStatus(device, "Check file APK trong 'data/fb'------------");
                                Thread.Sleep(5000);
                            }
                        }
                        else
                        {
                            Thread.Sleep(1000);
                            //Device.RebootByCmd(deviceID);
                            Thread.Sleep(30000);
                        }
                        LogStatus(device, "Không thể mở fb lần 2, chạy lại ", 5000);
                        Device.RebootByCmd(deviceID);
                        //device.loadNewProxy = true;
                        //Thread.Sleep(30000);
                        order.error_code = -1;
                        return order;
                    }
                }
                device.accMoi = GetAccMoi();

                if (device.accMoi == null)
                {
                    device.accMoi = GetAccMoi();
                }
                if (device.accMoi == null || string.IsNullOrEmpty(device.accMoi.uid))
                {
                    LogStatus(device, "Không thể lấy acc mồi-----------------", 5000);
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Goldenrod;
                    if (EpAccMoicheckBox.Checked)
                    {

                        order.error_code = Constant.CAN_NOT_GET_ACC_CODE;

                        Thread.Sleep(3000);
                        return order;
                    }
                    else
                    {
                        goto MOI_KATANA_OK;
                    }

                }

                LogStatus(device, "Login bằng usename/pass - Check acc:" + device.accMoi.uid + " pass:" + device.accMoi.pass);

                if (WaitAndTapXML(deviceID, 1, "đăngnhậpbằngtàikhoảnkháccheckable"))
                {
                    CheckTextExist(deviceID, "descsốdiđộnghoặcemailchec", 10);
                }

                LogStatus(device, "Check đúng màn hình đăng nhập");
                if (!CheckTextExist(deviceID, "sốdiđộnghoặcemailresource", 2))
                {
                    LogStatus(device, "Không thấy màn hình đăng nhập - open app again");
                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Thread.Sleep(2000);
                    if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                    {
                        LogStatus(device, "Không thể mở facebook", 10000);
                        order.error_code = -1;
                        return order;
                    }
                    else
                    {
                        Device.Back(deviceID);
                        Thread.Sleep(5000);
                    }
                }

                if (!WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec"))
                {
                    Device.TapByPercent(deviceID, 15.8, 45.6); // username
                }

                Utility.InputText(deviceID, device.accMoi.uid, true);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.3, 96.7);
                Utility.WaitAndTapXML(deviceID, 4, "khẩucheckable"); // Mật khẩu
                //Thread.Sleep(1000);

                if (micerCheckBox.Checked)
                {
                    InputTextMicer(deviceID, device.accMoi.pass);
                }
                else
                {
                    Utility.InputText(deviceID, device.accMoi.pass, false);
                }
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 95.8); // xong hạ bàn phím
                //Thread.Sleep(1500);
                if (!WaitAndTapXML(deviceID, 3, "đăngnhập"))
                {
                    Device.TapByPercent(deviceID, 46.9, 44.4);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 59.9, 60.6);
                }
                //Thread.Sleep(2000);
                //LogStatus(device, "Sai thông tin đăng nhập ---- Chạy tiếp ");
                //Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                //Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                //Thread.Sleep(10000);
                // return order;
                // Check dang nhập
                for (int i = 0; i < 10; i++)
                {
                    LogStatus(device, "Check mồi katana login - " + i);
                    string xmlCheck = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, "sai", 1, xmlCheck))
                    {
                        if (epMoiThanhCongcheckBox.Checked)
                        {
                            LogStatus(device, "Sai thông tin đăng nhập ---- Chạy lại ");
                            order.error_code = -1;
                            Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(10000);
                            return order;
                        }
                        else
                        {
                            LogStatus(device, "Sai thông tin đăng nhập ---- Chạy tiếp ");
                            Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(10000);
                            return order;
                        }
                    }
                    if (CheckTextExist(deviceID, "tiếptụccheckable", 1, xmlCheck))
                    {
                        LogStatus(device, "Login thành công - tiếp tục ");
                        Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(10000);
                        return order;
                    }
                    if (WaitAndTapXML(deviceID, 1, "thửlạiresourceid", xmlCheck)
                        || WaitAndTapXML(deviceID, 1, "lưu", xmlCheck))
                    {
                        if (CheckTextExist(deviceID, "chờ một phút", 1))
                        {
                            LogStatus(device, "Chờ thiết lập tiếng việt");
                        }
                        else
                        {
                            if (moiKatanaNhanhcheckBox.Checked)
                            {
                                LogStatus(device, "Mồi Katana Nhanh -----");
                                return order;
                            }
                            Thread.Sleep(3000);
                            goto MOI_KATANA_THANH_CONG;
                        }
                    }
                    if (CheckTextExist(deviceID, "recognize", 1, xmlCheck))
                    {
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                        LogStatus(device, "We dont recoginze your device", 2000);
                        order.error_code = -1;
                        Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(10000);
                        return order;
                    }
                }

            MOI_KATANA_THANH_CONG:
                string xmlll = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "đăngnhậpcheckable", 1, xmlll) || WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec", xmlll))
                {
                    LogStatus(device, "không thể đăng nhập - thử lại");
                    Thread.Sleep(3000);
                    if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                    {
                        Device.TapByPercent(deviceID, 46.9, 44.4);
                    }
                    Thread.Sleep(20000);
                    if (CheckTextExist(deviceID, "sai", 1))
                    {
                        if (epMoiThanhCongcheckBox.Checked)
                        {
                            LogStatus(device, "Sai thông tin đăng nhập ---- Chạy lại ", 2000);
                            order.error_code = -1;
                            Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(10000);
                            return order;
                        }
                        else
                        {
                            LogStatus(device, "Sai thông tin đăng nhập ---- Chạy tiếp ", 2000);
                            Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(10000);
                            return order;
                        }
                    }
                    if (CheckTextExist(deviceID, "đăngnhậpcheckable", 1) || WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec"))
                    {
                        if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                        {
                            Device.TapByPercent(deviceID, 46.9, 44.4);
                        }
                        Thread.Sleep(20000);
                        if (CheckTextExist(deviceID, "sai", 1))
                        {
                            if (epMoiThanhCongcheckBox.Checked)
                            {
                                LogStatus(device, "Sai thông tin đăng nhập ---- Chạy lại ", 2000);
                                order.error_code = -1;
                                Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                                Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(10000);
                                return order;
                            }
                            else
                            {
                                LogStatus(device, "Sai thông tin đăng nhập ---- Chạy tiếp ", 2000);
                                Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                                Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(10000);
                                return order;
                            }
                        }
                        LogStatus(device, "không thể đăng nhập - lưu lại");
                        Thread.Sleep(3000);

                    }
                }
                if (WaitAndTapXML(deviceID, 1, "thửlạiresourceid"))
                {
                    Thread.Sleep(3000);
                }
                if (CheckTextExist(deviceID, "dùng tiếng anh", 1))
                {

                    goto MOI_KATANA_OK;
                }

                WaitAndTapXML(deviceID, 1, "lưucheckable");
                xmlll = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "saithôngtinđăngnhập", 1, xmlll))
                {
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                    order.isSuccess = false;
                    LogStatus(device, "Sai thông tin đăng nhập", 5000);
                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Thread.Sleep(10000);
                    return order;
                }
                if (CheckTextExist(deviceID, "bảo trì", 1, xmlll))
                {
                    WaitAndTapXML(deviceID, 1, "ok");
                }
                if (CheckTextExist(deviceID, "không thể đăng nhập", 1, xmlll))
                {
                    LogStatus(device, "Không thể đăng nhập", 5000);
                    return order;
                }
                WaitAndTapXML(deviceID, 1, "tiếp tục");
                // Check login máy khac.
                if (CheckTextExist(deviceID, "kiểmtrathôngbáotrênthiếtbịkhác", 3))
                {
                    if (string.IsNullOrEmpty(device.accMoi.qrCode))
                    {
                        LogStatus(device, "Acc không có qrCode");
                        Thread.Sleep(15000);
                        Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(10000);
                        return order;
                    }
                    //THU_CACH_KHAC
                    LogStatus(device, "kiểm tra thông báo trên thiết bị khác");
                    WaitAndTapXML(deviceID, 2, "thửcáchkháccheckabl");
                    WaitAndTapXML(deviceID, 2, "ứngdụngxácthựccheckable");
                    WaitAndTapXML(deviceID, 2, "tiếptụccheckable");

                    WaitAndTapXML(deviceID, 2, "mãcheckable");

                    string token = "";

                    try
                    {
                        // Get code
                        var base32Bytes = Base32Encoding.ToBytes(device.accMoi.qrCode);

                        var otp = new Totp(base32Bytes);
                        token = otp.ComputeTotp();
                    }
                    catch (Exception ex)
                    {
                        LogStatus(device, "exception token:" + ex.Message);
                        order.error_code = Constant.CAN_NOT_LOGIN_ACC_2FA;
                        return order;
                    }
                    if (string.IsNullOrEmpty(token))
                    {
                        LogStatus(device, "Không thể lấy token", 15000);

                        return order;
                    }
                    if (!moiKhong2facheckBox.Checked)
                    {
                        Utility.InputVietVNIText(deviceID, token);
                        WaitAndTapXML(deviceID, 3, "tiếptụccheckable");
                        //KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 84.5);
                        Thread.Sleep(4000);
                        if (storeAccMoicheckBox.Checked)
                        {
                            if (!WaitAndTapXML(deviceID, 4, "lưucheckablefal"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 84.5);
                            }
                        }
                        else
                        {
                            WaitAndTapXML(deviceID, 2, "lúc khác");
                        }
                        if (CheckTextExist(deviceID, "truycậpvịtrí", 3))
                        {
                            Device.TapByPercent(deviceID, 53.7, 65.1, 2000);
                        }
                        //LogStatus(device, "Open business fb ");
                        //Device.OpenApp(deviceID, "com.facebook.pages.app");
                        //Thread.Sleep(3000);
                        // Check mồi thành công
                        //LogStatus(device, "Kiểm tra mồi thành công");
                        Device.SyncAccountsSetting(deviceID);
                        Device.OpenAccountsSetting(deviceID);
                        Thread.Sleep(2000);
                        if (CheckTextExist(deviceID, "facebook"))
                        {
                            LogStatus(device, "Mồi thành công");

                        }
                        else
                        {
                            LogStatus(device, "Mồi không thành công");
                            //dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Brown;
                            if (forceMoiThanhCongcheckBox.Checked)
                            {
                                order.error_code = -1;
                                return order;
                            }
                        }
                    }
                    return order;
                }
                if (!string.IsNullOrEmpty(device.accMoi.qrCode))
                {
                    if (Utility.CheckTextExist(deviceID, "mã đăng nhập", 5))
                    {
                        string token = "";
                        WaitAndTapXML(deviceID, 2, "okresource"); // Have 2fa
                        try
                        {
                            // Get code
                            var base32Bytes = Base32Encoding.ToBytes(device.accMoi.qrCode);

                            var otp = new Totp(base32Bytes);
                            token = otp.ComputeTotp();
                        }
                        catch (Exception ex)
                        {
                            LogStatus(device, "Can not put 2fa", 5000);
                            order.error_code = Constant.CAN_NOT_LOGIN_ACC_2FA;
                            return order;
                        }

                        // Thread.Sleep(2000);
                        Device.TapByPercent(deviceID, 13.4, 39.8, 1000);



                        if (!moiKhong2facheckBox.Checked)
                        {
                            Utility.InputVietVNIText(deviceID, token);
                            if (!WaitAndTapXML(deviceID, 3, Language.Continue2Fa()))
                            {
                                LogStatus(device, "Can not put 2fa", 5000);
                                order.error_code = Constant.CAN_NOT_LOGIN_ACC_2FA;
                                return order;
                            }
                        }

                    }
                }

                if (CheckTextExist(deviceID, "khôngthểđăngnhập"))
                {
                    LogStatus(device, "Acc mồi lỗi, lấy acc mồi khác", 5000);

                    //FbUtil.ReinstallFb(deviceID);

                    ReInstallFb(deviceID);

                    device.reInstallFb = 0;
                    goto LOGIN_KATANA;
                }
                Thread.Sleep(5000);
                if (storeAccMoicheckBox.Checked)
                {
                    WaitAndTapXML(deviceID, 2, "okresource");
                }

                if (storeAccMoicheckBox.Checked)
                {
                    if (!WaitAndTapXML(deviceID, 4, "lưucheckablefal"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 84.5);
                    }
                }
                else
                {
                    WaitAndTapXML(deviceID, 2, "lúc khác");
                }
                Thread.Sleep(2000);
                if (CheckTextExist(deviceID, "truycậpvịtrí", 1))
                {
                    Device.TapByPercent(deviceID, 53.7, 65.1, 2000);
                }
                Thread.Sleep(1000);
                if (storeAccMoicheckBox.Checked)
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.1, 83.5);
                }

                if (order.loginAccMoiMessenger)
                {
                    Device.OpenApp(deviceID, Constant.MESSENGER_PACKAGE);
                    Thread.Sleep(10000);
                    if (!WaitAndTapXML(deviceID, 4, "lưucheckablefal"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 84.5);
                    }
                    Thread.Sleep(1000);
                }
            MOI_KATANA_OK:
                Thread.Sleep(100);
            }

            if (order.loginAccMoiBusiness)
            {
            LOGIN_BUSINESS:
                LogStatus(device, "Login acc moi Business");
                LogRegStatus(dataGridView, device, "Mồi Business");
                device.accMoi = GetAccMoi();


                if (device.accMoi == null)
                {
                    device.accMoi = GetAccMoi();
                }
                if (device.accMoi != null)
                {
                    Device.ClearCache(deviceID, Constant.FACEBOOK_BUSINESS_PACKAGE);
                    Device.OpenApp(deviceID, Constant.FACEBOOK_BUSINESS_PACKAGE);
                    //Thread.Sleep(20000);
                    for (int i = 0; i < 20; i++)
                    {
                        if (WaitAndTapXML(deviceID, new[] { "mậtkhẩuresourceid", "passwordresourceid" }))
                        {
                            break;
                        }
                        if (i == 19)
                        {
                            LogStatus(device, "Can not open business");
                            return order;
                        }
                    }

                    //Thread.Sleep(1000);
                    //WaitAndTapXML(deviceID, new[] { "mậtkhẩuresourceid", "passwordresourceid" });
                    if (!WaitAndTapXML(deviceID, 1, "emailorphoneresourceid")) // tap userid
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 63.8, 15.1);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.9, 18.2);
                    }

                    Thread.Sleep(1000);
                    InputText(deviceID, device.accMoi.uid, true);

                    WaitAndTapXML(deviceID, new[] { "mậtkhẩuresourceid", "passwordresourceid" });

                    InputText(deviceID, device.accMoi.pass, false);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 60.8, 34.8);


                    if (moiBusinessNhanhcheckBox.Checked)
                    {
                        Thread.Sleep(5000);
                        if (CheckTextExist(deviceID, new[] { "không phản hồi", "verify", "đóng" }))
                        {
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                            LogStatus(device, "Mồi business bị lỗi --------------", 1000);
                            //order.error_code = -1;
                            //device.chuyenQuaMoiKatana = true;
                            return order;
                        }

                        WaitAndTapXML(deviceID, 4, "thửlạiresourceid");
                        return order;
                    }
                    Thread.Sleep(6000);
                    if (CheckTextExist(deviceID, "mustverifytheiraccount", 1))
                    {
                        LogStatus(device, "Lỗi Must verify account business");
                        return order;
                    }
                    Thread.Sleep(6000);
                    for (int i = 0; i < 10; i++)
                    {
                        string xmlCheck = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "sai", 1, xmlCheck))
                        {
                            if (epMoiThanhCongcheckBox.Checked)
                            {
                                LogStatus(device, "Sai thông tin đăng nhập ---- Chạy lại ");
                                order.error_code = -1;
                                return order;
                            }
                            else
                            {
                                LogStatus(device, "Sai thông tin đăng nhập ---- Chạy tiếp ");
                                return order;
                            }
                        }
                        if (WaitAndTapXML(deviceID, 1, "thửlạiresourceid", xmlCheck)
                            || WaitAndTapXML(deviceID, 1, "lưu", xmlCheck))
                        {
                            if (CheckTextExist(deviceID, "chờ một phút", 1))
                            {
                                LogStatus(device, "Chờ thiết lập tiếng việt");
                            }
                            else
                            {
                                if (moiKatanaNhanhcheckBox.Checked)
                                {
                                    LogStatus(device, "Mồi Katana Nhanh -----");
                                    return order;
                                }
                                Thread.Sleep(3000);

                            }
                        }

                        if (CheckTextExist(deviceID, "recognize", 1, xmlCheck))
                        {
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                            LogStatus(device, "We dont recoginze your device", 2000);
                            order.error_code = -1;
                            return order;
                        }
                    }

                    if (CheckTextExist(deviceID, new[] { "không phản hồi", "verify", "đóng" }))
                    {
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSeaGreen;
                        LogStatus(device, "Mồi business bị lỗi --------------", 2000);
                        order.error_code = -1;
                        device.chuyenQuaMoiKatana = true;
                        return order;
                    }
                    WaitAndTapXML(deviceID, new[] { "dùng tiếng anh", "ok", "đóng" });

                    return order;
                }
                else
                {
                    LogStatus(device, "Không thể lấy acc mồi-----------------", 5000);
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Goldenrod;
                    if (EpAccMoicheckBox.Checked)
                    {

                        order.error_code = Constant.CAN_NOT_GET_ACC_CODE;

                        Thread.Sleep(3000);
                        return order;
                    }

                }
            }

            return order;
        }

        public OrderObject InitialOrder(DeviceObject device)
        {
            OrderObject order = new OrderObject();

            order.upContactNew = uploadContactNewCheckbox.Checked;
            order.upCoverNew = coverNewcheckBox.Checked;
            order.usDeviceLanguage = UsLanguagecheckBox.Checked;
            order.proxyWfi = proxyWificheckBox.Checked;
            order.changePhoneNumber = changePhoneNumbercheckBox.Checked;
            order.forceAvatarUs = forceAvatarUsCheckBox.Checked;
            order.loginByUserPassword = loginByUserPassCheckBox.Checked;
            order.hasproxy = false;
            order.getHotmailKieumoi = getHotmailKieumoicheckBox.Checked;
            if (!randomProxySim2checkBox.Checked)
            {
                order.proxyFromServer = proxyFromServercheckBox.Checked;
                order.removeProxy = removeProxy2checkBox.Checked;
            }

            Phone.DRK_DOMAIN = drkDomainTextbox.Text;
            order.isSuccess = false;
            order.isVeriOk = false;
            order.pushAvatar = false;
            order.hasOtp = false;

            order.accType = Constant.ACC_TYPE_NORMAL;
            try
            {
                order.numberOfFriendRequest = Convert.ToInt32(numberOfFriendRequestTextBox.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                order.numberOfFriendRequest = 5;
            }
            if (tempmailLolradioButton.Checked)
            {
                order.tempmailType = Constant.TEMP_TEMPMAIL_LOL;
            }
            else if (generatorEmailradioButton.Checked)
            {
                order.tempmailType = Constant.TEMP_GENERATOR_EMAIL;
            }
            else if (oneSecradioButton.Checked)
            {
                order.tempmailType = Constant.TEMP_GENERATOR_1_SEC_EMAIL;
            }
            else if (sellGmailradioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_SELL_GMAIL;
            }
            else if (dichvuGmailradioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_DICH_VU_GMAIL;
            }
            else if (dichvugmail2radioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_DICH_VU_GMAIL2;
            }
            else if (fakemailgeneratorradioButton.Checked)
            {
                order.tempmailType = Constant.FAKE_MAIL;
            }
            else if (fakeEmailradioButton.Checked)
            {
                order.tempmailType = Constant.TEMP_FAKE_EMAIL;
            }
            else if (MailOtpRadioButton.Checked)
            {
                order.tempmailType = Constant.MAIL_OTP;
            }
            else if (gmail30minradioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_30_MIN;
            }
            else if (sellGmailServerradioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_SELL_GMAIL_SERVER;
            }
            else if (gmailOtpRadioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_OTP_GMAIL;
            }
            else if (superTeamRadioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_SUPERTEAM;
            }
            else if (gmail48hradioButton.Checked)
            {
                order.tempmailType = Constant.GMAIL_48h;
            }
            else
            {
                order.tempmailType = Constant.GMAIL_DICH_VU_GMAIL;
            }

            order.nvrUpAvatar = nvrUpAvatarCheckBox.Checked;
            
            order.reupFullInfoAcc = reupFullCheckBox.Checked;
            order.doitenAcc = doitenVncheckBox.Checked;
            order.veriNvrOutSite = veriNvrBenNgoaiCheckBox.Checked;
            order.language = Constant.LANGUAGE_VN;
            if (hotmailRadioButton.Checked) order.emailType = Constant.HOTMAIL_TYPE;
            if (outlookDomainRadioButton.Checked) order.emailType = Constant.OUTLOOK_DOMAIN;
            if (outlookRadioButton.Checked) order.emailType = Constant.OUTLOOK;

            if (randomPhoneCheckBox.Checked)
            {
                Random rrrr = new Random();
                int temp = rrrr.Next(1, 5);

                if (temp == 1) order.dauso = true;

                if (temp == 3) order.prefixTextnow = true;

                if (temp == 4) order.dauso12 = true;

                if (temp == 5) order.carryCodePhone = true;
            }
            else
            {
                order.dauso = dausoCheckBox.Checked;
                order.americaPhone = americaPhoneCheckBox.Checked;
                order.prefixTextnow = prefixTextNowCheckBox.Checked;
                order.dauso12 = dauso12CheckBox.Checked;
                order.carryCodePhone = carryCodecheckBox.Checked;
            }

            if (maleCheckbox.Checked)
            {
                order.gender = Constant.MALE;
            }
            else if (femaleCheckbox.Checked)
            {
                order.gender = Constant.FEMALE;
            }
            else
            {
                order.gender = GetGender(order);// Random gender
            }

            order.code = "2fa";
            order.has2Fa = set2faCheckbox.Checked;
            order.hasAvatar = runAvatarCheckbox.Checked;
            order.isHotmail = !TempMailcheckBox.Checked;
            order.isReverify = verifyAccNvrCheckBox.Checked;
            order.loginAccMoiLite = moiFbLitecheckBox.Checked;
            order.loginAccMoiKatana = moiKatanacheckBox.Checked;

            if (moiFbLitecheckBox.Checked && moiKatanacheckBox.Checked)
            {
                Random dddfdsf = new Random();
                int kkkk = dddfdsf.Next(1, 100);
                if (kkkk < 50)
                {
                    order.loginAccMoiLite = false;
                    order.loginAccMoiKatana = true;
                }
                else
                {
                    order.loginAccMoiLite = true;
                    order.loginAccMoiKatana = false;
                }
            }



            order.loginAccMoiMessenger = moiMessengercheckBox.Checked;
            if (moiMessengercheckBox.Checked)
            {
                //order.loginAccMoiKatana = true;
                //moiKatanacheckBox.Checked = true;
            }
            order.loginAccMoiBusiness = moiBusinesscheckBox.Checked;
            if (InputEnglishNameCheckbox.Checked)
            {
                order.language = Constant.LANGUAGE_US;
            }
            else
            {
                order.language = Constant.LANGUAGE_VN;
            }

            order.veriAcc = verifiedCheckbox.Checked;
            order.veriDirectHotmail = veriHotmailCheckBox.Checked;
            order.veriByPhone = veriPhoneCheckBox.Checked;
            order.veriDirectByPhone = veriDirectByPhoneCheckBox.Checked;
            order.phoneT.isDirect = veriDirectByPhoneCheckBox.Checked;
            order.checkLogin = checkLoginCheckBox.Checked;
            order.veriBackup = veriBackupCheckBox.Checked;


            if (randomRegVericheckBox.Checked)
            {
                Random rrrrrr = new Random();

                int checkNum = rrrrrr.Next(1, 100);
                if (checkNum > 50) // reg acc
                {

                    order.isReverify = false;

                    checkNum = rrrrrr.Next(100, 200);
                    if (checkNum > 130)
                    {
                        LogRegStatus(dataGridView, device, "Regggggggggg");
                        order.veriAcc = true;
                    } else
                    {
                        LogRegStatus(dataGridView, device, "NoVeri");
                        order.veriAcc = false;
                    }

                    order.loginAccMoiLite = true;
                }
                else
                {
                    LogRegStatus(dataGridView, device, "Re Verriririr");
                    order.isReverify = true;
                    order.veriAcc = true;
                    order.loginAccMoiLite = false;
                }
            }


            if (drkCheckBox.Checked)
            {
                order.phoneT.source = Constant.DRK_TEXTNOW;
            }
            else
            {
                if (textnowCheckbox.CheckState == CheckState.Indeterminate)
                {
                    order.phoneT.source = Constant.PHONE_RAND_TEXTNOW;
                    Random ran = new Random();
                    int nn = ran.Next(1, 150);
                    if (nn <= 50)
                    {
                        order.phoneT.source = Constant.CODE_TEXTNOW;
                    }
                    else if (nn > 50 && nn <= 100)
                    {
                        order.phoneT.source = Constant.OTP_MMO_TEXTNOW;
                    }
                    else
                    {
                        order.phoneT.source = Constant.DRK_TEXTNOW;
                    }
                }
                else if (textnowCheckbox.CheckState == CheckState.Checked)
                {
                    order.phoneT.source = Constant.CODE_TEXTNOW;
                }
                else
                {
                    order.phoneT.source = Constant.OTP_MMO_TEXTNOW;
                }
            }

            if (randomVeriCheckBox.Checked)
            {
                Random num = new Random();
                if (num.Next(1, 100) % 3 == 0)
                {
                    order.veriAcc = true;
                }
            }

            homeCheckBox.Checked = order.hasAvatar;

            return order;
        }

        public void Autoclone(DeviceObject device, OrderObject order)
        {
            try
            {
                device.log = "";

                var watchAll = System.Diagnostics.Stopwatch.StartNew();
                order.phoneT.cookie = cookieCodeTextNowtextBox.Text;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                string uiXML = "";
                string password = "";

                int yearOld = 0;
                string name = "";

                Random ran = new Random();
                string deviceID = device.deviceId;

                if (reupRegCheckBox.Checked)  // togle status
                {
                    if (device.currentStatus == Constant.REUP)
                    {
                        device.currentStatus = Constant.REG;
                        order.reupFullInfoAcc = false;
                        order.veriAcc = true;
                        order.has2Fa = false;
                    }
                    else
                    {
                        device.currentStatus = Constant.REUP;
                        order.reupFullInfoAcc = true;
                        order.veriAcc = false;
                    }
                }
                Device.EnableMobileData(deviceID);

                device.running = true;
                string running = dataGridView.Rows[device.index].Cells[6].Value.ToString();
                if (holdingCheckBox.Checked || running != "True")
                {
                    device.running = false;
                    dataGridView.Rows[device.index].Cells[13].Value = "Đang tạm nghỉ";
                    Thread.Sleep(10 * 1000);
                    return;
                }
                // todo
                //dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
                Device.PermissionReadContact(deviceID);
                Device.PermissionCallPhone(deviceID);
                Device.PermissionReadPhoneState(deviceID);


                if (device.resetCount >= 20)
                {
                    device.resetCount = 0;
                    Device.RebootByCmd(deviceID);
                    LogStatus(device, "Reboot device sau 20 lần");
                    Thread.Sleep(40000);

                    return;
                }

                if (forceIp4CheckBox.Checked
                    //&& device.currentIPType != Constant.ACTION_CHANGE2IP4
                    )
                {
                    device.action = Constant.ACTION_CHANGE2IP4;
                    Change2Ip(device, device.action); // force change ip
                }

                if (forceIp6checkBox.Checked
                    //&& device.currentIPType != Constant.ACTION_CHANGE2IP6
                    )
                {
                    device.action = Constant.ACTION_CHANGE2IP6;
                    Change2Ip(device, device.action); // force change ip
                }


                //dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;

                Device.Home(deviceID);
                device.resetCount = 0;
                if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked && dataGridView.Rows[device.index].Cells[6].Value.ToString() == "True")
                {
                    LogStatus(device, "Screen is locking screen - Opening it");
                    Device.Unlockphone(deviceID);
                }

                int delay = Convert.ToInt32(delayTextbox.Text) + ran.Next(100, 500);
                string selectedDeviceName = "s7";

                if (order.veriNvrOutSite)
                {
                    LogStatus(device, "Clear app fb");

                    Thread.Sleep(500);
                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    Thread.Sleep(500);

                    Thread.Sleep(1000);
                    Device.ClearCache(deviceID, "com.facebook.orca");
                    // Login
                    LogStatus(device, "Open app fb");
                    FbUtil.OpenFacebookAppVerify(device);
                    Account acc = getNvrOutsite();
                    LogStatus(device, "Check login");
                    if (acc != null)
                    {


                        Device.PermissionReadContact(deviceID);
                        Device.PermissionCallPhone(deviceID);
                        Device.PermissionReadPhoneState(deviceID);
                        Device.PermissionCamera(deviceID);

                        order.account = acc;
                        order.uid = acc.uid;
                        password = acc.pass;

                        order.language = Constant.LANGUAGE_VN;
                        LogStatus(device, "Check acc:" + acc.uid + " pass:" + acc.pass);
                        Device.TapByPercent(deviceID, 15.8, 45.6); // username
                        Device.TapByPercent(deviceID, 30.4, 45.8, 1000);

                        Utility.InputVietVNIText(deviceID, acc.uid);
                        Utility.WaitAndTapXML(deviceID, 4, "khẩucheckable"); // Mật khẩu

                        Utility.InputText(deviceID, acc.pass, false);

                        Thread.Sleep(500);

                        if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.9, 44.4);
                        }

                        if (CheckTextExist(deviceID, "bảo trì"))
                        {
                            WaitAndTapXML(deviceID, 1, "ok");
                        }

                        // Store infor login
                        Device.TapByPercent(deviceID, 10.0, 96.6, 2000);

                        if (CheckLock(device, deviceID))
                        {
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            WriteFileLog(acc.note, "Acc_Login_die.txt");
                            return;
                        }

                        device.isBlocking = false;
                        device.blockCount = 0;

                        order.isSuccess = true;
                        order.veriBackup = true;
                        order.veriAcc = false;
                        goto REG_VERY;
                    }
                }
                if (order.reupFullInfoAcc)
                {

                    //FbUtil.ClearAccountFbInSetting(deviceID, true);

                    order.veriAcc = true;
                    LogStatus(device, "Clear app fb");

                    Thread.Sleep(500);
                    LogStatus(device, "Start Reup full Infor acc");
                    if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                    {
                        LogStatus(device, "--- Can not open facebook->  try again 1");
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(1000);
                        if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                        {
                            LogStatus(device, "Can not open facebook -> try again 2");

                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.BlueViolet;
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(1000);
                            if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                            {
                                LogStatus(device, "Can not open facebook -> return");
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                                Thread.Sleep(4000);
                                return;
                            }
                        }
                    }
                    order.isRun = true;
                    Account acc = FbUtil.GetAccWaitInfo(true, deviceID, order.language);

                    if (acc == null || string.IsNullOrEmpty(acc.uid))
                    {
                        LogStatus(device, "Hết Acc up 2fa -> veri acc");

                        order.veriAcc = true;
                        order.isReverify = true;
                        order.has2Fa = false;
                        verifiedCheckbox.Checked = true;
                        randomVersionSaudiecheckBox.Checked = true;
                        moiFbLitecheckBox.Checked = true;
                        return;
                    }

                    Device.PermissionReadContact(deviceID);
                    Device.PermissionCallPhone(deviceID);
                    Device.PermissionReadPhoneState(deviceID);
                    Device.PermissionCamera(deviceID);
                    order.account = acc;
                    order.uid = acc.uid;
                    order.oldType = acc.type;
                    if (!string.IsNullOrEmpty(order.oldType) && order.oldType.Contains("friend"))
                    {
                        order.hasAddFriend = true;
                    }
                    if (string.IsNullOrEmpty(acc.uid))
                    {
                        LogStatus(device, "Không lấy được acc -----------");
                        Thread.Sleep(15000);
                        return;
                    }
                    order.qrCode = acc.qrCode; // store again
                    if (order.has2Fa)  // Không bật 2fa
                    {
                        if (!string.IsNullOrEmpty(acc.qrCode)) // Acc có sẵn 2fa -> không cần bật 2fa
                        {
                            order.has2Fa = false;
                            order.set2FaSuccess = true;
                        }
                    }

                    password = acc.pass;
                    order.gender = acc.gender;
                    order.language = acc.language;
                    //order.language = Constant.LANGUAGE_US; // todo
                    order.accType = Constant.ACC_TYPE_REUP + "_" + acc.type;
                    if (order.doitenAcc)
                    {
                        order.accType = Constant.ACC_TYPE_REUP_DOI_TEN + "_" + acc.type;
                    }
                    if (!string.IsNullOrEmpty(acc.email))
                    {
                        MailObject mail = new MailObject
                        {
                            email = acc.email,
                            password = acc.emailPass

                        };
                        if (mail.password == Constant.TEMPMAIL || mail.password == Constant.GMAIL_SELL_GMAIL)
                        {
                            order.isHotmail = false;
                        }
                        order.currentMail = mail;
                    }
                    order.checkAccHasAvatar = acc.hasAvatar;

                    LogStatus(device, acc.uid + " pass:" + acc.pass);

                    InputEnglishNameCheckbox.Invoke(new MethodInvoker(() =>
                    {
                        if (acc.language == Constant.LANGUAGE_US)
                        {
                            InputEnglishNameCheckbox.Checked = true;
                        }
                        else
                        {
                            InputEnglishNameCheckbox.Checked = false;
                        }
                    }));

                    string uidFromCookie = FbUtil.GetUidFromCookie(acc.cookie);
                    if (!acc.hasAvatar)
                    {
                        LogStatus(device, "Login bằng username /pass");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(uidFromCookie) && !order.loginByUserPassword)
                        {
                            ServerApi.GetBackupAcc(acc.uid);
                            Thread.Sleep(1000);
                        }
                    }
                    bool CheckAuthFile = ExtractZipAuth(acc.uid);


                    if (!CheckAuthFile) // login bằng user pass
                    {
                        try
                        {
                            var dir = new DirectoryInfo("Authentication/" + acc.uid);
                            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                            dir.Delete(true);
                            File.Delete("Authentication/" + acc.uid + ".zip");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("ex:" + ex.Message);
                        }
                    lOGIN_AGAIN:
                        LogStatus(device, "Login bằng usename/pass - Check acc:" + acc.uid + " pass:" + acc.pass);
                        string xxxml = GetUIXml(deviceID);
                        if (!WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec", xxxml))
                        {
                            if (WaitAndTapXML(deviceID, 1, "đăngnhậpbằngtàikhoảnkháccheckable", xxxml))
                            {
                                Thread.Sleep(2000);
                                CheckTextExist(deviceID, "descsốdiđộnghoặcemailchec", 10);
                            }
                            //Thread.Sleep(1400);

                            LogStatus(device, "Check đúng màn hình đăng nhập");
                            if (!CheckTextExist(deviceID, "sốdiđộnghoặcemailresource", 2))
                            {
                                LogStatus(device, "Không thấy màn hình đăng nhập - open app again");
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(2000);
                                if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                                {
                                    LogStatus(device, "Không thể mở facebook");
                                    Thread.Sleep(10000);
                                    goto STORE_INFO;
                                }
                                else
                                {
                                    Device.Back(deviceID);
                                    Thread.Sleep(5000);
                                }
                            }
                            if (!WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec"))
                            {
                                Device.TapByPercent(deviceID, 15.8, 45.6); // username
                            }
                        }
                        //KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.0, 26.2); // xóa so dt cũ

                        Device.MoveEndTextbox(deviceID);
                        Device.DeleteChars(deviceID, 15);



                        Utility.InputText(deviceID, acc.uid, true);
                        Thread.Sleep(1000);
                        Device.TapByPercent(deviceID, 89.0, 95.7); // Hạ bàn phím

                        if (!Utility.WaitAndTapXML(deviceID, 4, "khẩucheckable"))
                        {
                            // Mật khẩu
                            //Thread.Sleep(1000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 80.1, 35.7);
                        }
                        if (micerCheckBox.Checked)
                        {
                            InputTextMicer(deviceID, acc.pass);
                        }
                        else
                        {
                            Utility.InputText(deviceID, acc.pass, false);
                        }

                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 95.2); // xong
                        if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                        {
                            Device.TapByPercent(deviceID, 46.9, 44.4);
                        }
                        Thread.Sleep(2000);
                        string xmlll;
                        for (int i = 0; i < 15; i++)
                        {
                            xmlll = GetUIXml(deviceID);
                            if (order.doitenAcc && CheckTextExist(deviceID, new string[] { "nhắntin", "thêm ảnh", "lúc khác", "lưucheckable" }, xmlll))
                            {
                                goto DOI_TEN_VN;
                            }
                            if (CheckTextExist(deviceID, "nhập mã xác nhận"))
                            {
                                LogStatus(device, "Acc chưa veri - bỏ acc");
                                return;
                            }
                            if (!CheckTextExist(deviceID, acc.uid, 1, xmlll))
                            {
                                break;
                            }
                        }

                        if (CheckTextExist(deviceID, "trang này không hiển thị", 1))
                        {
                            LogStatus(device, "trang này không hiển thị");
                            Thread.Sleep(10000);
                            return;
                        }
                        if (order.doitenAcc && CheckTextExist(deviceID, new string[] { "nhắntin", "thêm ảnh", "lúc khác", "lưucheckable" }))
                        {
                            goto DOI_TEN_VN;
                        }


                        xmlll = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "đăngnhậpcheckable", 1, xmlll)
                            || WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec", xmlll))
                        {
                            LogStatus(device, "không thể đăng nhập - thử lại");
                            Thread.Sleep(3000);
                            if (order.doitenAcc && CheckTextExist(deviceID, new string[] { "nhắntin", "thêm ảnh", "lúc khác" }))
                            {
                                goto DOI_TEN_VN;
                            }
                            if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                            {
                                Device.TapByPercent(deviceID, 46.9, 44.4);
                            }
                            Thread.Sleep(20000);
                            if (order.doitenAcc && CheckTextExist(deviceID, new string[] { "nhắntin", "thêm ảnh", "lúc khác" }))
                            {
                                goto DOI_TEN_VN;
                            }
                            if (CheckTextExist(deviceID, "sai", 1))
                            {
                                LogStatus(device, "Sai thông tin đăng nhập, xóa acc");
                                Thread.Sleep(10000);
                                return;
                            }
                            if (CheckTextExist(deviceID, "đăngnhậpcheckable", 1) || WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec"))
                            {
                                if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                                {
                                    Device.TapByPercent(deviceID, 46.9, 44.4);
                                }
                                Thread.Sleep(20000);
                                if (CheckTextExist(deviceID, "sai", 1))
                                {
                                    LogStatus(device, "Sai thông tin đăng nhập, xóa acc");
                                    Thread.Sleep(10000);
                                    return;
                                }
                                LogStatus(device, "không thể đăng nhập - lưu lại");
                                Thread.Sleep(3000);

                                if (order.doitenAcc)
                                {
                                    order.accType = Constant.ACC_TYPE_DOI_TEN_KHong_login;
                                }

                                goto STORE_INFO;
                            }
                        }

                        if (order.doitenAcc && CheckTextExist(deviceID, new string[] { "nhắntin", "thêm ảnh", "lúc khác" }))
                        {
                            goto DOI_TEN_VN;
                        }


                        if (WaitAndTapXML(deviceID, 3, "tiếp tục dùng tiếng anh mỹ"))
                        {
                            WaitAndTapXML(deviceID, 2, "tiếp tục dùng tiếng anh mỹ");
                            Thread.Sleep(3000);
                        }
                        //if (order.doitenAcc)
                        //{
                        //    WaitAndTapXML(deviceID, 1, "luckhaccheckable");
                        //} else
                        //{
                        WaitAndTapXML(deviceID, 1, "lưucheckable");
                        //}


                        xmlll = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "Kiểm tra email", 1, xmlll))
                        {
                            LogStatus(device, "Xác nhận qua email, ----------- bỏ acc");
                            Thread.Sleep(1000);
                            return;
                        }
                        if (CheckTextExist(deviceID, "saithôngtinđăngnhập", 1, xmlll))
                        {
                            LogStatus(device, "sai thông tin đăng nhập, gõ lại password");
                            if (!WaitAndTapXML(deviceID, 1, "okresource"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.2, 58.1); // ok
                                Thread.Sleep(1000);
                            }
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 75.2, 51.5); // tap to password
                            Device.DeleteChars(deviceID, acc.pass.Length + 1);
                            Device.DeleteAllChars(deviceID);
                            Utility.InputText(deviceID, acc.pass, false);

                            Thread.Sleep(1500);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 95.2); // xong
                            if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                            {
                                Device.TapByPercent(deviceID, 46.9, 44.4);
                            }

                            for (int i = 0; i < 15; i++)
                            {
                                if (!CheckTextExist(deviceID, acc.uid, 1))
                                {
                                    break;
                                }
                            }
                            xmlll = GetUIXml(deviceID);
                            if (CheckTextExist(deviceID, "trang này không hiển thị", 1, xmlll))
                            {
                                LogStatus(device, "trang này không hiển thị");
                                Thread.Sleep(10000);
                                return;
                            }

                        }
                        if (CheckTextExist(deviceID, "saithôngtinđăngnhập", 1, xmlll)
                            || CheckTextExist(deviceID, "session", 1, xmlll)
                            || CheckTextExist(deviceID, "hết hạn", 1, xmlll)
                            || CheckTextExist(deviceID, "lỗi", 1, xmlll))
                        {
                            if (order.doitenAcc)
                            {
                                order.accType = Constant.ACC_TYPE_DOI_TEN_KHong_login;
                            }
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.4, 57.9);
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            LogStatus(device, "Sai thông tin đăng nhập");
                            Thread.Sleep(3000);
                            goto STORE_INFO;
                        }
                        if (CheckTextExist(deviceID, "bảo trì", 1, xmlll))
                        {
                            WaitAndTapXML(deviceID, 1, "ok");
                        }
                        if (CheckTextExist(deviceID, "không thể đăng nhập", 1, xmlll))
                        {
                            LogStatus(device, "Không thể đăng nhập");
                            Thread.Sleep(10000);
                            return;
                        }

                        if (CheckTextExist(deviceID, "kiểmtrathôngbáotrênthiếtbịkhác", 1))
                        {
                            if (string.IsNullOrEmpty(acc.qrCode))
                            {
                                LogStatus(device, "Acc bị lỗi, bỏ acc");
                                Thread.Sleep(13000);
                                return;
                            }
                            //THU_CACH_KHAC
                            LogStatus(device, "kiểm tra thông báo trên thiết bị khác");
                            WaitAndTapXML(deviceID, 2, "thửcáchkháccheckabl");
                            WaitAndTapXML(deviceID, 2, "ứngdụngxácthựccheckable");
                            WaitAndTapXML(deviceID, 2, "tiếptụccheckable");

                            WaitAndTapXML(deviceID, 2, "mãcheckable");

                            string token = "";

                            try
                            {
                                // Get code
                                var base32Bytes = Base32Encoding.ToBytes(acc.qrCode);

                                var otp2fa = new Totp(base32Bytes);
                                token = otp2fa.ComputeTotp();
                            }
                            catch (Exception ex)
                            {
                                LogStatus(device, "Khong thể lấy token:" + ex.Message);
                                order.error_code = Constant.CAN_NOT_LOGIN_ACC_2FA;
                                return;
                            }

                            if (string.IsNullOrEmpty(token))
                            {
                                LogStatus(device, "Không thể lấy token ------------");
                                Thread.Sleep(15000);
                                return;
                            }
                            Utility.InputVietVNIText(deviceID, token);
                            Thread.Sleep(1000);

                            WaitAndTapXML(deviceID, 2, "tiếp tục");

                            if (!WaitAndTapXML(deviceID, 3, "lưucheckablefal"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 84.5);
                            }

                            if (CheckTextExist(deviceID, "truycậpvịtrí", 3))
                            {
                                Device.TapByPercent(deviceID, 53.7, 65.1, 2000);
                            }

                        }
                        else if (Utility.CheckTextExist(deviceID, "mã đăng nhập", 5))
                        {

                            WaitAndTapXML(deviceID, 2, "okresource"); // Have 2fa
                            if (!string.IsNullOrEmpty(acc.qrCode))
                            {
                                // Get code
                                var base32Bytes = Base32Encoding.ToBytes(acc.qrCode);

                                var otp2fa = new Totp(base32Bytes);
                                string token = otp2fa.ComputeTotp();

                                Thread.Sleep(2000);
                                Device.TapByPercent(deviceID, 13.4, 39.8, 1000);

                                Utility.InputVietVNIText(deviceID, token);

                                if (!WaitAndTapXML(deviceID, 3, Language.Continue2Fa()))
                                {
                                    LogStatus(device, "Can not put 2fa");
                                    return;
                                }
                            }
                            else
                            {
                                LogStatus(device, "Acc:" + acc.uid + " -pass:" + acc.pass + " -- missing 2fa code");
                                return;
                            }
                        }
                        if (order.doitenAcc)
                        {
                            goto DOI_TEN_VN;
                        }
                        string uiXML1 = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, Language.AllowAccessLocationDialog(), 1, uiXML1))
                        {
                            Device.TapByPercent(deviceID, 52.9, 65.6, 1000);
                        }
                        WaitAndTapXML(deviceID, 1, "từ chối", uiXML1);

                        if (CheckTextExist(deviceID, "xác nhận", 1, uiXML1)
                            || CheckTextExist(deviceID, "Nhập email hợp lệ")
                            || CheckTextExist(deviceID, "Nhập mã xác nhận"))
                        {

                            LogStatus(device, "Acc chưa được veri ---- lỗi - bỏ acc", 10000);
                            return;
                            order.veriNhapMaXacNhan = true;
                            LogStatus(device, "Đã vào màn hình xác nhận code 222222222");

                            order.veriNhapMaXacNhan = true;

                            Thread.Sleep(2000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.5, 48.3);

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.8, 51.5);

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.5, 54.1);
                            if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                            {
                                goto PUT_MAIL;
                            }
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.1, 60.3);
                            Thread.Sleep(3000);
                            if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                            {
                                goto PUT_MAIL;
                            }
                            else
                            {
                                string teeemp = GetUIXml(deviceID);

                                if (WaitAndTapXML(deviceID, 1, "xác nhận bằng email", teeemp)
                                     || WaitAndTapXML(deviceID, 1, "xác nhận qua email", teeemp))
                                {
                                    goto PUT_MAIL;
                                }
                                else
                                {
                                    if (CheckTextExist(deviceID, "Hủy"))
                                    {
                                        Device.Back(deviceID);
                                        WaitAndTapXML(deviceID, 1, "email");
                                        goto PUT_MAIL;
                                    }
                                    else
                                    {
                                        Thread.Sleep(5000);
                                        if (WaitAndTapXML(deviceID, 2, "emailcheckable")
                                            || WaitAndTapXML(deviceID, 1, "xác nhận bằng email", teeemp)
                                            || WaitAndTapXML(deviceID, 1, "xác nhận qua email", teeemp))
                                        {
                                            goto PUT_MAIL;
                                        }
                                        else
                                        {
                                            LogStatus(device, "Kiểm tra máy có giao diện mới - reup info");
                                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;

                                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.4, 15.8);
                                            Thread.Sleep(1000);

                                            for (int i = 0; i < 7; i++)
                                            {
                                                string xxxmmm = GetUIXml(deviceID);
                                                if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                                                {
                                                    LogStatus(device, "Nhập email -------------", 1000);
                                                    goto PUT_MAIL;
                                                }
                                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                                                {
                                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                                }
                                            }
                                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                            Thread.Sleep(2000);
                                            for (int i = 0; i < 2; i++)
                                            {
                                                string xxxmmm = GetUIXml(deviceID);
                                                if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                                                {
                                                    LogStatus(device, "Nhập email -------------", 1000);
                                                    goto PUT_MAIL;
                                                }
                                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                                                {
                                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                                }
                                            }
                                            //if (WaitAndTapXML(deviceID, 1, "continue"))
                                            //{
                                            //    WaitAndTapXML(deviceID, 2, "chophepresource");
                                            //    Thread.Sleep(65000);
                                            //    goto VERIFY_BY_EMAIL;

                                            //}
                                            Device.ScreenShoot(deviceID, false, "capture_.png");
                                            string info = GetUIXml(deviceID);
                                            System.IO.File.WriteAllText("local\\Err_" + deviceID + "XML.txt", info);
                                            Thread.Sleep(60000);
                                        }
                                    }
                                }
                            }
                        }
                        if (CheckTextExist(deviceID, "Lưu thông tin", 1))
                        {
                          
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 79.0, 95.1);
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        LogStatus(device, "Download Authentication success");

                        Thread.Sleep(1100);
                        FbUtil.PushBackupFb(acc.uid, deviceID);
                        order.gender = acc.gender;

                        try
                        {
                            var dir = new DirectoryInfo("Authentication/" + acc.uid);
                            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                            dir.Delete(true);
                            File.Delete("Authentication/" + acc.uid + ".zip");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("ex:" + ex.Message);
                        }
                        LogStatus(device, "Push Authentication finish");

                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

                        FbUtil.OpenFacebookApp(deviceID);
                        if (CheckTextExist(deviceID, "xác nhận", 1))
                        {
                            LogStatus(device, "Acc chưa được veri ---- lỗi");
                            Thread.Sleep(5000);
                            return;
                        }
                    }
                    regOk++;
                    device.isBlocking = false;
                    device.blockCount = 0;

                    order.isSuccess = true;

                    if (CheckTextExist(deviceID, "Lưu thông tin", 1))
                    {
                       
                        Device.TapByPercent(deviceID, 79.0, 95.1, 1000);
                    }
                    if (WaitAndTapXML(deviceID, 1, "tiếp"))
                    {
                        LogStatus(device, "Tiếp ---------------");
                    }
                    if (CheckTextExist(deviceID, "xác nhận"))
                    {
                     
                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }

                DOI_TEN_VN:
                    if (order.doitenAcc)
                    {
                        regOk++;
                        device.isBlocking = false;
                        device.blockCount = 0;

                        order.isSuccess = true;
                        LogStatus(device, "Chạy tính năng đổi tên VN");

                        if (DoiTenViet(device, acc.gender, acc.pass))
                        {
                            order.language = Constant.LANGUAGE_VN;
                            LogStatus(device, "Change name ok -------");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Green;
                            goto STORE_INFO;
                        }
                        DOI_TEN_LAI:
                        Device.GotoFbAccountSettings(deviceID);
                        Thread.Sleep(4000);
                        if (!WaitAndTapXML(deviceID, 2, "càiđặtchungcheckablefa"))
                        {
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(1000);
                            Device.GotoFbAccountSettings(deviceID);
                            Thread.Sleep(13000);
                            if (CheckTextExist(deviceID, "nhập mã xác nhận"))
                            {
                                LogStatus(device, "Acc chưa veri - bỏ acc");
                                return ;
                            }
                            if (!WaitAndTapXML(deviceID, 5, "càiđặtchungcheckablefa"))
                            {
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                                order.isSuccess = false;
                                order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_ERR;
                                goto STORE_INFO;
                            }
                        }
                        Thread.Sleep(1000);
                        if (!CheckTextExist(deviceID, "thôngtincánhânvàtàikhoảnresourceid", 5))
                        {
                            if (CheckTextExist(deviceID, "hànhvitựđộng", 1))
                            {
                                LogStatus(device, "detect tự động fb----------------");
                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.AliceBlue;
                                order.isSuccess = false;
                                return;
                            }
                            order.language = Constant.LANGUAGE_US;
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_ERR;
                            goto STORE_INFO;
                        }
                        Thread.Sleep(1000);

                        if (CheckTextExist(deviceID, "tên", 1))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.7, 15.1); // tap tên box
                        } else
                        {
                            Thread.Sleep(1000);
                            goto DOI_TEN_VN;
                        }
                        

                        if (!CheckTextExist(deviceID, "xemlạithayđổiresourcei", 4))
                        {
                            if (CheckTextExist(deviceID, "bạnkhôngthểđổitên", 1))
                            {
                                LogStatus(device, "Tên đã được đổi trước đó rồi -------");
                                order.language = Constant.LANGUAGE_VN;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Green;
                                order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_DA_DOI_TEN;
                                order.isSuccess = true;
                                goto STORE_INFO;
                            }

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.1, 26.1);
                            Thread.Sleep(1000);
                            if (!CheckTextExist(deviceID, "xemlạithayđổiresourcei", 3))
                            {
                                if (CheckTextExist(deviceID, "bạnkhôngthểđổitên", 1))
                                {
                                    LogStatus(device, "Tên đã được đổi trước đó rồi -------");
                                    order.language = Constant.LANGUAGE_VN;
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Green;
                                    order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_DA_DOI_TEN;
                                    order.isSuccess = true;
                                    goto STORE_INFO;
                                }
                                else
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.1, 26.1);
                                    fail++;
                                    device.blockCount++;
                                    device.isBlocking = true;
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                                    order.isSuccess = false;
                                    order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_ERR;
                                    goto STORE_INFO;
                                }
                            }
                        }

                        Thread.Sleep(3000);

                        //order.language = Constant.LANGUAGE_VN;
                        string lastname = " " + GetLastName(Constant.LANGUAGE_VN, acc.gender, name3wordcheckBox.Checked);

                        string firstname = " " + GetFirtName(Constant.LANGUAGE_VN, acc.gender, nameVnUscheckBox.Checked);


                        NameObject nnn = ServerApi.GetNameLocalCache(serverCacheMailTextbox.Text, acc.gender, Constant.LANGUAGE_VN);
                        if (nnn != null && !string.IsNullOrEmpty(nnn.name))
                        {
                            firstname = nnn.name;
                            lastname = nnn.lastname;
                        }
                        if (lastname.Contains("Ð") || lastname.Contains("đ") || ConvertToUnsign(lastname).ToLower().StartsWith("d"))
                        {
                            lastname = GetLastName(Constant.LANGUAGE_VN, acc.gender, name3wordcheckBox.Checked);
                        }
                        if (firstname.Contains("Ð") || firstname.Contains("đ") || ConvertToUnsign(firstname).ToLower().StartsWith("d"))
                        {
                            firstname = GetFirtName(Constant.LANGUAGE_VN, acc.gender, nameVnUscheckBox.Checked);
                        }


                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 22.5); // tap họ box
                        Thread.Sleep(2000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 22.5); // tap họ box
                        Thread.Sleep(1000);

                        Device.DeleteChars(deviceID, 40);
                        Device.DeleteAllChars(deviceID);
                        InputVietVNIText(deviceID, lastname);

                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 35.4); // tap tên box
                        Device.DeleteChars(deviceID, 40);
                        Device.DeleteAllChars(deviceID);
                        InputVietVNIText(deviceID, firstname);

                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.8, 24.2); // tap ra ngoài hạ bàn phím

                        WaitAndTapXML(deviceID, 1, "xemlạithayđổiresourcei");

                        if (!CheckTextExist(deviceID, "xem trước", 2))
                        {
                            order.language = Constant.LANGUAGE_US;
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_ERR;
                            goto STORE_INFO;
                        }
                        Thread.Sleep(2000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.1, 45.4); // tap password
                        InputText(deviceID, acc.pass, false);

                        if (!WaitAndTapXML(deviceID, 2, "lưuthayđổiresourceid"))
                        {
                            order.language = Constant.LANGUAGE_US;
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            order.accType = Constant.ACC_TYPE_REUP_DOI_TEN_ERR;
                            goto STORE_INFO;
                        }
                        order.language = Constant.LANGUAGE_VN;
                        LogStatus(device, "Change name ok -------");
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Green;
                        goto STORE_INFO;
                    }

                    goto UPLOAD_AVATAR;
                }

                if (order.checkLogin) CheckLogin(device, order);


RE_VERI_NVR:
if (order.isReverify)
                {

                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    Device.ClearCache(deviceID, Constant.FACEBOOK_LITE_PACKAGE);
                    FbUtil.ClearAccountFbInSetting(deviceID, true);
                    LogStatus(device, "Start Verify Acc---------------------");

                    if (!loginByUserPassCheckBox.Checked)
                    {
                        if (!FbUtil.OpenFacebookAppRegnormal(device, device.clearCacheLite, order.loginAccMoiKatana, fastcheckBox.Checked))
                        {

                            LogStatus(device, "--- Can not open facebook->  try again 1");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(1000);
                            if (!FbUtil.OpenFacebookAppRegnormal(device, device.clearCacheLite, order.loginAccMoiKatana, fastcheckBox.Checked))
                            {
                                LogStatus(device, "Can not open facebook -> try again 2");
                                if (order.proxyFromServer)
                                {
                                    LogStatus(device, "Can not open facebook khi có proxy - đổi port -> return");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                                    StopProxySuper(device);
                                    return;
                                }
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.BlueViolet;
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(1000);
                                if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                                {
                                    LogStatus(device, "Can not open facebook -> return");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                                    Thread.Sleep(4000);
                                    return;
                                }
                            }
                        }
                    } else
                    {
                        if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                        {

                            LogStatus(device, "--- Can not open facebook->  try again 1");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(1000);
                            if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                            {
                                LogStatus(device, "Can not open facebook -> try again 2");
                                if (order.proxyFromServer)
                                {
                                    LogStatus(device, "Can not open facebook khi có proxy - đổi port -> return");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                                    StopProxySuper(device);
                                    return;
                                }
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.BlueViolet;
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(1000);
                                if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                                {
                                    LogStatus(device, "Can not open facebook -> return");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                                    Thread.Sleep(4000);
                                    return;
                                }
                            }
                        }
                    }
                    /// Open facebook xong
                    order.isRun = true;
                    string byDevice = deviceID;
                    if (!nvrByDeviceCheckBox.Checked)
                    {
                        byDevice = "";
                    }

                    Account acc = FbUtil.GetAccNoveri(true, byDevice, order.language, forceDungMayCheckBox.Checked);
                    Device.PermissionReadContact(deviceID);
                    Device.PermissionCallPhone(deviceID);
                    Device.PermissionReadPhoneState(deviceID);
                    Device.PermissionCamera(deviceID);
                    WaitAndTapXML(deviceID, 1, "chophépresourceid");
                    if (acc == null)
                    {
                        LogStatus(device, "Hết acc -------------reg nvr---");
                        order.veriAcc = false;
                        order.isReverify = false;

                        goto REG_NORMAL;
                    }

                    if (!string.IsNullOrEmpty(acc.createdAt))
                    {
                        LogStatus(device, acc.message + "-" + acc.createdAt.Substring(0, 17) + "-" + acc.uid + " pass:" + acc.pass);
                    }
                    else
                    {
                        LogStatus(device, acc.message + "-" + acc.uid + " pass:" + acc.pass);
                    }

                    order.account = acc;
                    order.uid = acc.uid;
                    order.language = acc.language;
                    password = acc.pass;
                    order.gender = acc.gender;
                    order.accType = Constant.ACC_TYPE_VERI_BACKUP;
                    string uidFromCookie = FbUtil.GetUidFromCookie(acc.cookie);


                    if (!string.IsNullOrEmpty(uidFromCookie) && !order.loginByUserPassword)
                    {
                        ServerApi.GetBackupAcc(acc.uid);
                        Thread.Sleep(1000);
                    }

                    bool CheckAuthFile = ExtractZipAuth(acc.uid);

                    if (CheckAuthFile)
                    {
                        try
                        {
                            FbUtil.PushBackupFb(acc.uid, deviceID);
                            order.gender = acc.gender;
                            var dir = new DirectoryInfo("Authentication/" + acc.uid);
                            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                            dir.Delete(true);
                            File.Delete("Authentication/" + acc.uid + ".zip");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("ex---------:" + ex.Message);
                        }
                        LogStatus(device, "Push Authentication finish");

                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    }  // login by username/ password
                    else
                    {
                        try
                        {
                            File.Delete("Authentication/" + acc.uid + ".zip");
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("ex---------:" + ex.Message);
                        }
                        if (string.IsNullOrEmpty(uidFromCookie))
                        {
                            LogStatus(device, "Cookie error, can not push backup");
                        }

                        LogStatus(device, "Login bằng usename/pass - Check acc:" + acc.uid + " pass:" + acc.pass);

                        string xxxml = GetUIXml(deviceID);
                        if (!WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec", xxxml))
                        {
                            if (WaitAndTapXML(deviceID, 1, "đăngnhậpbằngtàikhoảnkháccheckable", xxxml))
                            {
                                Thread.Sleep(2000);
                                CheckTextExist(deviceID, "descsốdiđộnghoặcemailchec", 10);
                            }

                            LogStatus(device, "Check đúng màn hình đăng nhập");
                            if (!CheckTextExist(deviceID, "sốdiđộnghoặcemailresource", 2))
                            {
                                LogStatus(device, "Không thấy màn hình đăng nhập - open app again");
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(2000);
                                if (!FbUtil.OpenFacebookApp2Login(device, device.clearCacheLite))
                                {
                                    LogStatus(device, "Không thể mở facebook");
                                    Thread.Sleep(10000);
                                    goto STORE_INFO;
                                }
                                else
                                {
                                    Device.Back(deviceID);
                                    Thread.Sleep(5000);
                                }
                            }
                            if (!WaitAndTapXML(deviceID, 1, "descsốdiđộnghoặcemailchec"))
                            {
                                Device.TapByPercent(deviceID, 15.8, 45.6); // username
                            }
                        }

                        Device.MoveEndTextbox(deviceID);
                        Device.DeleteChars(deviceID, 15);



                        Utility.InputText(deviceID, acc.uid, true);
                        if (!Utility.WaitAndTapXML(deviceID, 4, "khẩucheckable"))
                        {
                            // Mật khẩu
                            //Thread.Sleep(1000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 80.1, 35.7);
                        }
                        if (micerCheckBox.Checked)
                        {
                            InputTextMicer(deviceID, acc.pass);
                        }
                        else
                        {
                            Utility.InputText(deviceID, acc.pass, false);
                        }

                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 95.2); // xong
                        if (!WaitAndTapXML(deviceID, 2, "đăngnhập"))
                        {
                            Device.TapByPercent(deviceID, 46.9, 44.4);
                        }
                        Thread.Sleep(2000);


                        Thread.Sleep(3000);

                        for (int i = 0; i < 5; i++)
                        {
                            uiXML = GetUIXml(deviceID);
                            if (WaitAndTapXML(deviceID, 2, Language.UploadContactDialog2(), uiXML))
                            {
                                LogStatus(device, "Tải danh bạ lên");
                                Thread.Sleep(1000);
                                uiXML = GetUIXml(deviceID);
                                WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);
                            }
                            //if (WaitAndTapXML(deviceID, 1, "descbậtcheckable", uiXML) || WaitAndTapXML(deviceID, 1, "descbật", uiXML))
                            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                            {
                                LogStatus(device, "Đang tải danh bạ lên - bật");
                                WaitAndTapXML(deviceID, 2, Language.AllowAll());
                                if (CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    for (int k = 0; k < WaitAddContactCount; k++)
                                    {
                                        if (!CheckTextExist(deviceID, "đồng bộ"))
                                        {
                                            Console.WriteLine("đồng bộ:" + k);
                                            break;
                                        }
                                    }
                                }
                            }

                            if (CheckTextExist(deviceID, "Lưu thông tin", 1, uiXML))
                            {
                                Device.TapByPercent(deviceID, 79.0, 95.1, 1000);
                            }
                            if (CheckTextExist(deviceID, "Xác nhận tài khoản", 1, uiXML)
                                || CheckTextExist(deviceID, "xác nhận bằng số điện thoại", 1, uiXML))
                            {
                                goto ENTER_CODE_CONFIRM_EMAIL;
                            }
                            if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, uiXML))
                            {
                                if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                                {
                                    LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:BlanchedAlmond");

                                    fail++;
                                    device.blockCount++;
                                    device.isBlocking = true;
                                    order.isSuccess = false;
                                    LogStatus(device, "Acc die truoc khi nhập mail 1111");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                                    Thread.Sleep(10000);
                                    return;
                                }
                            }
                        }
                    }






                    // Check screen sau khi đăng nhập
                    Thread.Sleep(5000);
                    for (int i = 0; i < 12; i++)
                    {
                        uiXML = GetUIXml(deviceID);
                        if (WaitAndTapXML(deviceID, 1, "tiếp tục dùng tiếng anh mỹ", uiXML))
                        {
                            LogStatus(device, "Tiếp tục dùng tiếng anh mỹ : " + i, 3000);
                        }
                        if (WaitAndTapXML(deviceID, 1, "continuecheckable", uiXML))
                        {
                            if (WaitAndTapXML(deviceID, 3, "chophépresourceid"))
                            {
                                Thread.Sleep(60000);
                            } else
                            {
                                Thread.Sleep(4000);
                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1))
                                {
                                    break;
                                }
                                Thread.Sleep(10000);
                            }
                        }
                        if (CheckTextExist(deviceID, new string[] {"chúngtôiđãgửiđếnsố", "xácnhậntàikhoảnresourceid", "xác nhận bằng số điện thoại", "nhậpmãxácnhận",
                            "nhập số di động hợp lệ", "nhập email hợp lệ", "xác nhận", "xácnhậntàikhoảnfacebookcủabạn", "tiếp tục dùng tiếng anh mỹ"}))
                        {
                            break;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        uiXML = GetUIXml(deviceID);


                        if (CheckTextExist(deviceID, "chúngtôiđãgửiđếnsố",1, uiXML))
                        {
                            LogStatus(device, "Vào màn hình nhập mã xác nhận gửi đến số 444444444444");
                            if (WaitAndTapXML(deviceID, 1, "tôikhôngnhậnđượcmãcheckable", uiXML))
                            {
                                if (WaitAndTapXML(deviceID, 3, "xác nhận bằng email"))
                                {
                                    if (WaitAndTapXML(deviceID, 2, "descemailcheckablefal"))
                                    {
                                        order.veriNhapMaXacNhan = true;
                                        goto PUT_MAIL;
                                    }
                                }
                            }
                        }


                        if (WaitAndTapXML(deviceID, 1, "tiếp tục dùng tiếng anh mỹ", uiXML))
                        {
                            LogStatus(device, "Tiếp tục dùng tiếng anh mỹ : " + i, 3000);
                        }

                        if (CheckTextExist(deviceID, "xácnhậntàikhoảnfacebookcủabạn", 1, uiXML))
                        {
                            LogStatus(device, "Trường hợp mới -- xác nhận tài khoản facebook", 3000);
                            if (!WaitAndTapXML(deviceID, 1, "continuecheckable", uiXML))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.0, 79.2);
                            }
                            Thread.Sleep(3000);
                            WaitAndTapXML(deviceID, 1, "chophépresourceid");
                            Thread.Sleep(60000);

                            uiXML = GetUIXml(deviceID);
                            if (CheckTextExist(deviceID, "nhậpmãxácnhận", 1, uiXML)) {

                            } else
                            {
                                if (WaitAndTapXML(deviceID, 1, "gửimãquasmscheckable", uiXML))
                                {
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                                    LogStatus(device, "Vào màn hình mới---------", 1000);
                                    WaitAndTapXML(deviceID, 1, "tiếptụccheckable");
                                    Thread.Sleep(2000);
                                }
                                if (CheckTextExist(deviceID, "gửimãquasmscheckable", 1))
                                {
                                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                    Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                    LogStatus(device, "Bị treo màn hình Gửi mã sms", 3000);
                                }
                            }
                        }



                        if (CheckTextExist(deviceID, new string[] { "xácnhậntàikhoảnresourceid", "xác nhận bằng số điện thoại" }, uiXML))
                        {
                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }


                        if (CheckTextExist(deviceID, "nhậpmãxácnhận", 1, uiXML)
                            || CheckTextExist(deviceID, "nhập số di động hợp lệ", 1, uiXML)
                            || CheckTextExist(deviceID, "nhập email hợp lệ", 1, uiXML)
                            || CheckTextExist(deviceID, "xác nhận", 1, uiXML))
                        {
                            order.veriNhapMaXacNhan = true;
                            LogStatus(device, "Đã vào màn hình xác nhận code 222222222");

                            order.veriNhapMaXacNhan = true;

                            Thread.Sleep(2000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.5, 48.3);

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.8, 51.5);

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.5, 54.1);
                            if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                            {
                                goto PUT_MAIL;
                            }
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.1, 60.3);
                            Thread.Sleep(3000);
                            if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                            {
                                goto PUT_MAIL;
                            }

                            string teeemp = GetUIXml(deviceID);

                            if (WaitAndTapXML(deviceID, new string[] { "xác nhận bằng email", "xác nhận qua email" }, teeemp))
                            {
                                goto PUT_MAIL;
                            }

                            if (CheckTextExist(deviceID, "Hủy"))
                            {
                                Device.Back(deviceID);
                                WaitAndTapXML(deviceID, 1, "email");
                                goto PUT_MAIL;
                            }

                            Thread.Sleep(5000);
                            if (WaitAndTapXML(deviceID, 2, "emailcheckable")
                                || WaitAndTapXML(deviceID, 1, "xác nhận bằng email", teeemp)
                                || WaitAndTapXML(deviceID, 1, "xác nhận qua email", teeemp))
                            {
                                goto PUT_MAIL;
                            }

                            LogStatus(device, "Kiểm tra máy có giao diện mới");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                            if (WaitAndTapXML(deviceID, 1, "continue"))
                            {
                                Thread.Sleep(2000);
                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 6))
                                {
                                    Thread.Sleep(2000);
                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                }
                            }
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.4, 15.8);
                            Thread.Sleep(1000);

                            for (int k = 0; k < 7; k++)
                            {
                                string xxxmmm = GetUIXml(deviceID);
                                if (WaitAndTapXML(deviceID, 1, "tiếp tục dùng tiếng anh mỹ", xxxmmm))
                                {
                                    LogStatus(device, "Tiếp tục dùng tiếng anh mỹ : " + i, 3000);
                                }
                                if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                                {
                                    LogStatus(device, "Nhập email -------------", 1000);
                                    goto PUT_MAIL;
                                }
                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                                {
                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                }
                            }
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Thread.Sleep(2000);
                            for (int k = 0; k < 2; k++)
                            {
                                string xxxmmm = GetUIXml(deviceID);
                                if (WaitAndTapXML(deviceID, 1, "tiếp tục dùng tiếng anh mỹ", xxxmmm))
                                {
                                    LogStatus(device, "Tiếp tục dùng tiếng anh mỹ : " + i);
                                }
                                if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                                {
                                    LogStatus(device, "Nhập email -------------", 1000);
                                    goto PUT_MAIL;
                                }
                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                                {
                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                }
                            }

                            Device.ScreenShoot(deviceID, false, "capture_.png");
                            string info = GetUIXml(deviceID);
                            System.IO.File.WriteAllText("local\\Err_" + deviceID + "XML.txt", info);
                            Thread.Sleep(60000);
                        }
                        //
                        //Device.GotoFbConfirm(deviceID);
                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(1000);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(30000);
                    }




                    for (int i = 0; i < 5; i++)
                    {

                        uiXML = GetUIXml(deviceID);

                        if (CheckTextExist(deviceID, "nhập email hợp lệ", 1, uiXML))
                        {
                            if (!veriAccRegMailcheckBox.Checked)
                            {
                                LogStatus(device, "Acc reg by mail, email khong hop le.");
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                                Thread.Sleep(10000);
                                return;
                            }


                       
                            order.veriNhapMaXacNhan = true;
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 59.2, 31.2);
                            if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                            {
                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                order.isSuccess = false;
                                LogStatus(device, "Acc die truoc khi nhập mail 1134434311 color:DarkRed");
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkRed;
                                Thread.Sleep(10000);
                                return;
                            }
                            goto PUT_MAIL;

                        }
                        if (CheckTextExist(deviceID, "xácnhậntàikhoảnresourceid", 1, uiXML)
                                || CheckTextExist(deviceID, "xác nhận bằng số điện thoại", 1, uiXML)
                                || CheckTextExist(deviceID, "nhập email hợp lệ", 1, uiXML))
                        {
                         
                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                        else
                        {
                            if (CheckTextExist(deviceID, "nhậpmãxácnhận", 1, uiXML)
                            || CheckTextExist(deviceID, "nhập số di động hợp lệ", 1, uiXML)
                            || CheckTextExist(deviceID, "nhập email hợp lệ", 1, uiXML)
                            || CheckTextExist(deviceID, "xác nhận", 1, uiXML))
                            {
                                if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                                {
                                    fail++;
                                    device.blockCount++;
                                    device.isBlocking = true;
                                    order.isSuccess = false;
                                    LogStatus(device, "Acc die truoc khi nhập mail 1111 color:DarkOrchid");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkOrchid;
                                    Thread.Sleep(10000);
                                    return;
                                }
                                LogStatus(device, "Đã vào màn hình xác nhận code33333333333");

                                order.veriNhapMaXacNhan = true;

                                Thread.Sleep(2000);
                                Device.TapByPercent(deviceID, 50.5, 48.3, 1000);

                                Device.TapByPercent(deviceID, 37.8, 51.5, 1000);

                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.5, 54.1);
                                if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                                {
                                    goto PUT_MAIL;
                                }
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.1, 60.3);
                                Thread.Sleep(3000);
                                if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                                {
                                    goto PUT_MAIL;
                                }
                                else
                                {
                                    string teeemp = GetUIXml(deviceID);

                                    if (WaitAndTapXML(deviceID, 1, "xác nhận bằng email", teeemp)
                                         || WaitAndTapXML(deviceID, 1, "xác nhận qua email", teeemp))
                                    {
                                        goto PUT_MAIL;
                                    }
                                    else
                                    {
                                        if (CheckTextExist(deviceID, "Hủy"))
                                        {
                                            Device.Back(deviceID);
                                            WaitAndTapXML(deviceID, 1, "email");
                                            goto PUT_MAIL;
                                        }
                                        else
                                        {
                                            LogStatus(device, "Kiểm tra máy có giao diện mới");
                                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.4, 15.8);
                                            Thread.Sleep(1000);

                                            for (int k = 0; k < 7; k++)
                                            {
                                                string xxxmmm = GetUIXml(deviceID);
                                                if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                                                {
                                                    LogStatus(device, "Nhập email -------------", 1000);
                                                    goto PUT_MAIL;
                                                }
                                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                                                {
                                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                                }
                                            }
                                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                            Thread.Sleep(2000);
                                            for (int k = 0; k < 2; k++)
                                            {
                                                string xxxmmm = GetUIXml(deviceID);
                                                if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                                                {
                                                    LogStatus(device, "Nhập email -------------", 1000);
                                                    goto PUT_MAIL;
                                                }
                                                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                                                {
                                                    goto ENTER_CODE_CONFIRM_EMAIL;
                                                }
                                            }
                                            //if (WaitAndTapXML(deviceID, 1, "continue"))
                                            //{
                                            //    WaitAndTapXML(deviceID, 2, "chophepresource");
                                            //    Thread.Sleep(65000);
                                            //    goto VERIFY_BY_EMAIL;

                                            //}
                                            Device.ScreenShoot(deviceID, false, "capture_.png");
                                            string info = GetUIXml(deviceID);
                                            System.IO.File.WriteAllText("local\\Err_" + deviceID + "XML.txt", info);
                                            Thread.Sleep(60000);
                                        }

                                    }
                                }
                            }
                            if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                            {
                                LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:DarkGoldenrod");

                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                order.isSuccess = false;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkGoldenrod;
                                Thread.Sleep(10000);
                                return;
                            }

                            if (WaitAndTapXML(deviceID, 2, Language.UploadContactDialog2(), uiXML))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.9, 87.4);
                                LogStatus(device, "Tải danh bạ lên");
                            }
                            //if (WaitAndTapXML(deviceID, 1, "descbậtcheckable", uiXML) || WaitAndTapXML(deviceID, 1, "descbật", uiXML))
                            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                            {
                                LogStatus(device, "Đang tải danh bạ lên- bật");
                                if (CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    for (int k = 0; k < WaitAddContactCount; k++)
                                    {
                                        if (!CheckTextExist(deviceID, "đồng bộ"))
                                        {
                                            Console.WriteLine("đồng bộ:" + k);
                                            break;
                                        }
                                    }
                                }
                            }
                            WaitAndTapXML(deviceID, 2, Language.AllowAll());

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 88.3, 14.3);
                            Thread.Sleep(2000);
                            if (!CheckTextExist(deviceID, Language.UpdateEmailVeri()))
                            {
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                FbUtil.OpenFacebookApp(deviceID);
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 88.3, 14.3);
                            }
                        }
                    }
                    string uixml = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, "xácnhậntàikhoảnresourceid", 1, uixml)
                                || CheckTextExist(deviceID, "xác nhận bằng số điện thoại", 1, uixml))
                    {
              
                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }
                    uiXML = GetUIXml(deviceID);
                    if (WaitAndTapXML(deviceID, 2, Language.UploadContactDialog2(), uiXML))
                    {
                        WaitAndTapXML(deviceID, 2, Language.AllowAll());
               
                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }
                    if (CheckTextExist(deviceID, "textthêmảnhresourceid", 1, uiXML)
                        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML))
                    {
                
                        Thread.Sleep(1000);
                        goto UPLOAD_AVATAR;
                    }
                    if (CheckTextExist(deviceID, "sai mật khẩu", 1, uixml)
                        || Utility.CheckTextExist(deviceID, "cần có mã đăng nhập", 1, uixml))
                    {
                        LogStatus(device, "sai mật khẩu");
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                        order.isSuccess = false;
                        return;
                    }
                    if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                    {
                        LogStatus(device, "checklivewall - Acc die --- , return color:Ivory");
                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Ivory;
                        order.isSuccess = false;
                        return;
                    }
                    LogStatus(device, "Can not go to confirm screen- bỏ acc");
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                    //Utility.storeAccWithThread(isServer, order, deviceID,
                    //    password, order.currentMail.toString(), acc.qrCode, order.gender, yearOld, Constant.FALSE, device.log);
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                    order.isSuccess = false;
                    return;
                }
                Console.WriteLine("pre-----1 : " + watch.ElapsedMilliseconds);
            REG_NORMAL:
                Device.Home(deviceID);
                if (!FbUtil.OpenFacebookAppRegnormal(device, device.clearCacheLite, order.loginAccMoiKatana, fastcheckBox.Checked))
                {

                    if (WaitAndTapXML(deviceID, 2, "thửlạiresourceid"))
                    {
                        LogStatus(device, "Thử lại lần nữa");
                    }
                    LogStatus(device, "Can not load Vietnamese, Try again 1", 2000);
                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    FbUtil.ClearAccountFbInSetting(deviceID, true);
                    Device.PermissionAppReadContact(deviceID, Constant.FACEBOOK_PACKAGE);
                    //dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Brown;
                    openFacebookFailCount++;
                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

                    if (!FbUtil.OpenFacebookAppRegnormal(device, device.clearCacheLite, order.loginAccMoiKatana, fastcheckBox.Checked))
                    {
                        if (order.proxyFromServer)
                        {
                            LogStatus(device, "Can not open facebook khi có proxy - đổi port -> return");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                            StopProxySuper(device);
                            return;
                        }
                        //dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkCyan;

                        if (CheckTextExist(deviceID, Language.Next(), 1, uiXML))
                        {
                            LogStatus(device, "Open fb ok -------------");
                        }
                        else
                        {
                            for (int i = 2; i < 4; i++)
                            {
                                LogStatus(device, "Can not load Vietnamese, Try again " + i);
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkCyan;
                                if (order.removeProxy || (device.proxy != null && !device.proxy.hasProxy))
                                {
                                    if (FbUtil.ChangeIpByAirplane(device))
                                    {
                                        LogStatus(device, "Change ip successful: ");
                                    }
                                }

                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                if (FbUtil.OpenFacebookAppRegnormal(device, device.clearCacheLite, order.loginAccMoiKatana, fastcheckBox.Checked))
                                {
                                    device.clearCache = true;
                                    break;
                                }
                            }
                        }
                    }
                }


                if (CheckTextExist(deviceID, "thửlạiresource", 1))
                {
                    LogStatus(device, "vào màn hình lỗi --- chạy lại ");
                    return;
                    Thread.Sleep(3000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 57.8, 86.9);
                    Thread.Sleep(3000);
                }


                order.isRun = true;
                if (order.veriDirectByPhone )
                {
                    order.phoneT = Phone.TryToGetPhone(order, 10);
                    if (!IsDigitsOnly(order.phoneT.phone))
                    {
                        LogStatus(device, "Can not get phone:" + order.phoneT.message);
                        order.veriDirectByPhone = false;
                        order.phoneT.phone = GeneratePhonePrefix(prefixTextNow);
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        order.isSuccess = false;
                        Thread.Sleep(5000);
                        return;
                    }
                }


                password = GeneratePassword();
                if (order.usDeviceLanguage || CheckTextExist(deviceID, "your name", 1))
                {
                    name = FlowNormalNewUS(order, device, order.gender, password, yearOld, delay, selectedDeviceName);
                    // checklock
                    if (name == Constant.FAIL)
                    {
                        LogStatus(device, "checklivewall - Check live acc -Acc die --- , return--- color:Orange");

                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        device.isSuccess = false;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        LogRegFailStatus(device);
                        return;
                    }
                    for (int i = 0; i < 2; i ++)
                    {
                        string xxxm = GetUIXml(deviceID);

                        if (CheckTextExist(deviceID, "need more information", 1, xxxm))
                        {
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                        }
                       
                        string uidLocal = FbUtil.GetUid(deviceID);


                        if (string.IsNullOrEmpty(uidLocal) || FbUtil.CheckLiveWall(uidLocal) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Check live acc -Acc die --- , return--- color:Orange");

                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            device.isSuccess = false;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                            return;
                        }
                        if (WaitAndTapXML(deviceID, 1, "try another way"))
                        {
                            Thread.Sleep(3000);
                        }

                        if (CheckTextExist(deviceID, new string[] { "confirmation code" , "confirm  your facebook account"}, xxxm))
                        {

                            if (WaitAndTapXML(deviceID, 1, "send code via sms"))
                            {
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                                LogStatus(device, "Vào màn hình mới---------", 1000);
                                WaitAndTapXML(deviceID, 1, "tiếptụccheckable");
                                
                                Utility.Log("Xác nhận tài khoản", status);
                            }
                           

                    
                            LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 2000);

                            regNvrOk++;

                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                    }

                } else if (CheckTextExist(deviceID, "bạn tên gì", 2))
                {
                    name = FlowNormalNew(order, device, order.gender, password, yearOld, delay, selectedDeviceName);
                    if (showIpcheckBox.Checked)
                    {
                        device.currentPublicIp = device.currentPublicIp + "--" + Device.GetPublicIpSmartProxy(deviceID);
                        dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
                    }

                    if (name == "xac_nhan")
                    {

                        Utility.Log("Xác nhận tài khoản", status);

                        LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 2000);

                        regNvrOk++;

                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }
                    if (name == Constant.FAIL)
                    {
                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSalmon;
                        //LogStatus(device, "Sai thông tin đăng nhập ------------------", 10000);
                        order.isSuccess = false;
                        order.error_code = 101;
                        LogRegFailStatus(device);
                        return;
                    }
                    //Thread.Sleep(3000);
                    //if (WaitAndTapXML(deviceID, 1, "tôi đồng ý checkable"))
                    //{
                    //    Thread.Sleep(2000);
                    //    WaitAndTapXML(deviceID, 1, "tôi đồng ý checkable");
                    //    Thread.Sleep(5000);
                    string uidLocal111 = FbUtil.GetUid(deviceID);


                    if (string.IsNullOrEmpty(uidLocal111) || FbUtil.CheckLiveWall(uidLocal111) == Constant.DIE)
                    {
                        LogStatus(device, "checklivewall - Check live acc -Acc die --- , return--- color:Orange");

                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        device.isSuccess = false;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        LogRegFailStatus(device);
                        return;
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                
                if (CheckTextExist(deviceID, "saithôngtinđăngnhập", 1))
                    {

                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSalmon;
                        LogStatus(device, "Sai thông tin đăng nhập ------------------", 10000);
                        order.isSuccess = false;
                        order.error_code = 101;
                        LogRegFailStatus(device);
                        return;
                    }

                    string xml = GetUIXml(deviceID);
                    if (Utility.CheckTextExist(deviceID, "Xác nhận", 1, xml))
                    {
                        if (WaitAndTapXML(deviceID, 1, "gửimãquasmscheckable", xml))
                        {
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                            LogStatus(device, "Vào màn hình mới---------", 1000);
                            WaitAndTapXML(deviceID, 1, "tiếptụccheckable");
                            Thread.Sleep(2000);
                        }
                        if (CheckTextExist(deviceID, "gửimãquasmscheckable", 1))
                        {
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            LogStatus(device, "Bị treo màn hình Gửi mã sms", 3000);
                        }

                        LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 100);

                        regNvrOk++;

                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        string xxxm = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "tiếptụcdùngtiếnganhmỹresourceid", 1, xxxm))
                        {
                            break;
                        }
                        if (CheckTextExist(deviceID, "Xác nhận", 1, xxxm))
                        {
                            if (WaitAndTapXML(deviceID, 1, "gửimãquasmscheckable"))
                            {
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                                LogStatus(device, "Vào màn hình mới---------", 1000);
                                WaitAndTapXML(deviceID, 1, "tiếptụccheckable");
                                Thread.Sleep(2000);
                            }
                            if (CheckTextExist(deviceID, "gửimãquasmscheckable", 1))
                            {
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                LogStatus(device, "Bị treo màn hình Gửi mã sms", 3000);
                            }
                            Utility.Log("Xác nhận tài khoản", status);
     
                            LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 2000);

                            regNvrOk++;

                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                    }

                    if (WaitAndTapXML(deviceID, 2, "tiếptụcdùngtiếnganhmỹresourceid"))
                    {
                        Thread.Sleep(15000);

                        if (CheckLock(device, deviceID))
                        {
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                            return;
                        }
                       
                        if (WaitAndTapXML(deviceID, 1, "try another way"))
                        {
                            Thread.Sleep(3000);
                        }

                        if (Utility.CheckTextExist(deviceID, "Xác nhận", 5))
                        {

                            if (WaitAndTapXML(deviceID, 1, "gửimãquasmscheckable"))
                            {
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                                LogStatus(device, "Vào màn hình mới---------", 1000);
                                WaitAndTapXML(deviceID, 1, "tiếptụccheckable");
                                Thread.Sleep(2000);
                            }
                            if (CheckTextExist(deviceID, "gửimãquasmscheckable", 1))
                            {
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                LogStatus(device, "Bị treo màn hình Gửi mã sms", 3000);
                            }
                            Utility.Log("Xác nhận tài khoản", status);
  
                            LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 2000);

                            regNvrOk++;

                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                        else
                        {
                            Device.ScreenShoot(deviceID, false, "Reg_new_screen.png");
                            return;
                        }
                    }
                    else
                    {
                        string uidLocal = FbUtil.GetUid(deviceID);


                        if (string.IsNullOrEmpty(uidLocal) || FbUtil.CheckLiveWall(uidLocal) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Check live acc -Acc die --- , return--- color:Orange");

                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            device.isSuccess = false;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                            return;
                        }

                        if (CheckLock(device, deviceID))
                        {
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                            return;
                        }
                        if (Utility.CheckTextExist(deviceID, "Xác nhận", 1))
                        {

                            if (WaitAndTapXML(deviceID, 1, "gửimãquasmscheckable"))
                            {
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Gold;
                                LogStatus(device, "Vào màn hình mới---------", 1000);
                                WaitAndTapXML(deviceID, 1, "tiếptụccheckable");
                                Thread.Sleep(2000);
                            }

                            if (CheckTextExist(deviceID, "gửimãquasmscheckable", 1))
                            {
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                LogStatus(device, "Bị treo màn hình Gửi mã sms", 3000);
                            }
                            Utility.Log("Xác nhận tài khoản", status);

                            LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 2000);

                            regNvrOk++;

                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }

                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(10000);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(10000);
                        if (WaitAndTapXML(deviceID, 1, "tiếptụcdùngtiếnganhmỹresourceid"))
                        {
                            Thread.Sleep(5000);

                            if (CheckLock(device, deviceID, uiXML))
                            {
                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                                order.isSuccess = false;
                                LogRegFailStatus(device);
                                return;
                            }

                            if (Utility.CheckTextExist(deviceID, "Xác nhận", 1))
                            {
                                Utility.Log("Xác nhận tài khoản", status);
                    

                                //dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green1;
                                LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1", 2000);
                

                                regNvrOk++;

                                goto ENTER_CODE_CONFIRM_EMAIL;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
                else if (CheckTextExist(deviceID, "tiếp", 2))
                {
                    string ssss = GetUIXml(deviceID);
                    LogStatus(device, "Vào màn hình tạo tài khoản kiểu cũ ----------------");
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.0, 69.5);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.0, 69.5);

                    openFacebookFailCount = 0;
                    LogStatus(device, "Allow all permission");

                    WaitAndTapXML(deviceID, 1, Language.AllowAll());
                    WaitAndTapXML(deviceID, 1, Language.AllowAll());

                    // Random Information
                    yearOld = ran.Next(Convert.ToInt32(yearOldFrom.Text), Convert.ToInt32(yearOldTo.Text));

                    if (fixPasswordCheckbox.Checked)
                    {
                        password = fixPasswordtextBox.Text;
                        //order.accType = Constant.ACC_TYPE_FIX_PASSWORD;
                    }
                    if (CheckTextExist(deviceID, "bạn tên gì", 1))
                    {
                        name = FlowNormal2(order, device, order.gender, password, yearOld, delay, selectedDeviceName);
                    }
                    else
                    {
                        name = FlowNormal(order, device, order.gender, password, yearOld, delay, selectedDeviceName);
                    }
                } 

                else
                {
                    Console.WriteLine("main: " + watch.ElapsedMilliseconds);
                    goto REG_NORMAL;
                }
                Console.WriteLine("main: " + watch.ElapsedMilliseconds);
                if (string.IsNullOrEmpty(name))
                {
                    LogStatus(device, "Phone error, return");
                    order.isSuccess = false;
                    return;
                }
                LogStatus(device, "Tap sign up button");

                if (!WaitAndTapXML(deviceID, 8, Language.SignUp()))
                {
                    LogStatus(device, "can not find signup Color.Orange");
                    order.isSuccess = false;
                    LogRegFailStatus(device);
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                    order.isSuccess = false;
                    Thread.Sleep(5000);
                    return;
                }

                for (int i = 0; i < 8; i++)
                {
                    if (i == 2)
                    {
                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(1000);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(3000);
                    }
                    uiXML = GetUIXml(deviceID);
                    WaitAndTapXML(deviceID, 1, "textthửlạiresourceid", uiXML);
                    Console.WriteLine("Get uixml:" + watch.ElapsedMilliseconds);
                    LogStatus(device, "Đang sign up lần:" + (i + 1));
                    if (WaitAndTapXML(deviceID, 1, Language.SignUp(), uiXML))
                    {
                        continue;
                    }

                    if (CheckTextExist(deviceID, "chọn tên của bạn", 1, uiXML))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.8, 37.8);
                        Next(deviceID);
                        continue;
                    }

                    if (WaitAndTapXML(deviceID, 2, "không, tạo tài khoản mới", uiXML))
                    {
                        LogStatus(device, "Có tài khoản rồi, tạo tài khoản mới");
                        continue;
                    }

                    if (CheckTextExist(deviceID, "gần đây", 1, uiXML)
                        || CheckTextExist(deviceID, "already in use", 1, uiXML))
                    {
                        LogStatus(device, "Số điện thoại đã được dùng:" + order.phoneT.phone);
                        if (order.veriDirectByPhone)
                        {
                            WriteFileLog(order.phoneT.phone + "|" + order.phoneT.source + "|" + order.phoneT.message, "phone_da_dung_roi_textNow.txt");
                        }
                        else
                        {
                            WriteFileLog(order.phoneT.phone, "phone_da_dung_roi.txt");
                        }
                        order.isSuccess = false;
                        LogRegFailStatus(device);
                        Thread.Sleep(5000);
                        return;
                    }

                    if (Utility.WaitAndTapXML(deviceID, 3, Language.NotNow(), uiXML))
                    {
                        Utility.Log("Have popup Not Now", status);
                        Thread.Sleep(delay);
                        for (int k = 0; k < 60; k++)
                        {
                            if (!CheckTextExist(deviceID, "đang đăng nhập", 1))
                            {
                                break;
                            }
                            LogStatus(device, "Đang đăng nhập lần: " + k);
                        }
                        Thread.Sleep(delay);
                        Device.EnterPress(deviceID);

                        Thread.Sleep(1000);
                        Device.EnterPress(deviceID);
                        Thread.Sleep(1000);
                        if (!CheckTextExist(deviceID, "xác nhận", 1))
                        {
                            Thread.Sleep(4000);
                            Device.EnterPress(deviceID);
                        }

                        Thread.Sleep(delay);
            
                        LogStatus(device, "Reg OK 1 Constant.green1");
                        //dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green1;
                        Console.WriteLine("Check text not now:" + watch.ElapsedMilliseconds);
                        Thread.Sleep(2000);

                        regNvrOk++;


                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }
                    if (CheckTextExist(deviceID, "thêm ảnh"))
                    {
                        LogStatus(device, "Màn hình up avatar");
           

                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightPink;
                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(2000);
                        Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(5000);

                        regNvrOk++;

                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }
                    if (Utility.CheckTextExist(deviceID, "Xác nhận", 1, uiXML))
                    {
                        Utility.Log("Xác nhận tài khoản", status);
               
                        LogStatus(device, "Reg OK ---------Xác nhận tài khoản------- 1 - Constant.green1");
    
                        Thread.Sleep(2000);
                        regNvrOk++;

                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }

                    if (CheckLock(device, deviceID, uiXML))
                    {
                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        order.isSuccess = false;
                        LogRegFailStatus(device);
                        return;
                    }
                    if (WaitAndTapXML(deviceID, 2, Language.UploadContactDialog2(), uiXML))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.9, 87.4);
                        LogStatus(device, "tải danh bạ lên");
                        Console.WriteLine("Check text tải danh bạ lên:" + watch.ElapsedMilliseconds);
                        continue;
                    }
                    if (WaitAndTapXML(deviceID, 2, Language.UploadContactDialog(), uiXML))
                    {
                        continue;
                    }

                    if (CheckTextExist(deviceID, "deemed", 1, uiXML))
                    {
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Brown;
                        device.isSuccess = false;

                        LogStatus(device, "Bị lock đỏ rồi - Nghỉ 1 phút");
                        LogRegFailStatus(device);
                        Thread.Sleep(60000);
                        return;
                    }
                    if (CheckTextExist(deviceID, "đang tạo tài khoản", 1, uiXML))
                    {
                        for (int m = 0; m < 50; m++)
                        {
                            if (!CheckTextExist(deviceID, "đang tạo tài khoản", 1))
                            {
                                break;
                            }
                        }
                    }

                    if (CheckTextExist(deviceID, "đang đăng nhập", 1, uiXML))
                    {
                        for (int m = 0; m < 20; m++)
                        {
                            if (!CheckTextExist(deviceID, "đang đăng nhập", 1))
                            {
                                break;
                            }
                        }
                    }
                    WaitAndTapXML(deviceID, 1, "bật resource", uiXML);
                    WaitAndTapXML(deviceID, 1, "bật", uiXML);
                    if (CheckTextExist(deviceID, "Bắt đầu", 1, uiXML)
                        || CheckTextExist(deviceID, "nhắntin", 1, uiXML))
                    {
                        string uidLocal11;
                        if (!string.IsNullOrEmpty(order.uid))
                        {
                            uidLocal11 = order.uid;
                        }
                        else
                        {
                            uidLocal11 = FbUtil.GetUid(deviceID);
                        }
                        if (FbUtil.CheckLiveWall(uidLocal11) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Check live acc --Acc die --- , return-- color:IndianRed");

                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.IndianRed;
                            device.isSuccess = false;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                            return;
                        }

                        LogStatus(device, "Vào màn hình chính ");
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Khaki;
                        Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        Thread.Sleep(2000);
                        FbUtil.OpenFacebookApp(deviceID);
                   
                        regNvrOk++;
                        if (WaitAndTapXML(deviceID, 1, "cho phép", uiXML))
                        {
                            LogStatus(device, "màn hình mới - cho phép");
                            Thread.Sleep(5000);
                        }
                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }

                    if (i > 1)
                    {
                        string uidLocal;
                        if (!string.IsNullOrEmpty(order.uid))
                        {
                            uidLocal = order.uid;
                        }
                        else
                        {
                            uidLocal = FbUtil.GetUid(deviceID);
                        }

                        if (string.IsNullOrEmpty(uidLocal) || FbUtil.CheckLiveWall(uidLocal) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Check live acc -Acc die --- , return--- color:Orange");

                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            device.isSuccess = false;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            LogRegFailStatus(device);
                            return;
                        }
                    }

                }

                if (!CheckTextExist(deviceID, "Xác nhận tài khoản", 1))
                {
                    LogStatus(device, "Sign up quá lâu");
                    order.isSuccess = false;
                    LogRegFailStatus(device);
                    return;
                }

            ENTER_CODE_CONFIRM_EMAIL:

                if (order.veriDirectByPhone && CheckTextExist(deviceID, "cập nhật số di động", 1)) // Phone in used
                {
                    order.veriByPhone = true;
                    order.veriDirectByPhone = false;
                    order.phoneT.isDirect = true;
                }

                if (order.veriDirectHotmail || order.veriDirectByPhone) goto PUT_OTP;

                if (order.veriByPhone)
                {
                    string ui = GetUIXml(deviceID);
                    if (!WaitAndTapXML(deviceID, 2, Language.ChangePhoneNumber(), ui))
                    {
                        WaitAndTapXML(deviceID, 2, "bằng số điện thoại", ui);
                    }

                    Thread.Sleep(1000);

                    if (!WaitAndTapXML(deviceID, 2, "textsốđiệnthoạidiđộngresourceid", ui))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.6, 25.7);
                    }
                    if (CheckTextExist(deviceID, "cập nhật số di động", 1, ui))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.5, 32.0);
                    }

                    Thread.Sleep(1000);

                    if (order.phoneT.isDirect)
                    {
                        LogStatus(device, "Số điện thoại đã được dùng loại 2:" + order.phoneT.phone);
                        WriteFileLog(order.phoneT.phone + "|" + order.phoneT.source + "|" + order.phoneT.message, "phone_da_dung_roi_textNow22.txt");
                    }

                    // Try to get another phone
                    for (int i = 0; i < 5; i++)
                    {
                        order.phoneT = Phone.GetPhoneTextNow(order.phoneT);
                        if (IsDigitsOnly(order.phoneT.phone))
                        {
                            order.currentMail.email = order.phoneT.phone;
                            order.currentMail.password = "phone";
                        }

                        if (IsDigitsOnly(order.phoneT.phone))
                        {
                            if (order.phoneT.source == Constant.CODE_TEXTNOW)
                            {
                                numberOfPhoneCodeTextnow++;
                            }
                            else
                            {
                                numberOfPhoneOtp++;
                            }
                            order.currentMail.email = order.phoneT.phone;
                            order.currentMail.password = "phone";
                            LogStatus(device, "phone:" + order.phoneT.phone + "-source:" + order.phoneT.source + " lần:" + (i + 1));
                            if (i > 0)
                            {
                                Device.DeleteChars(deviceID, 13);
                            }
                            if (i == 3)
                            {
                                string uidLocal;
                                if (!string.IsNullOrEmpty(order.uid))
                                {
                                    uidLocal = order.uid;
                                }
                                else
                                {
                                    uidLocal = FbUtil.GetUid(deviceID);
                                }
                                if (FbUtil.CheckLiveWall(uidLocal) == Constant.DIE)
                                {
                                    LogStatus(device, "checklivewall - Check live acc ---- color: LightSlateGray");
                                    LogStatus(device, "Acc die --- , return");

                                    fail++;
                                    device.blockCount++;
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSlateGray;
                                    Thread.Sleep(10000);

                                    order.isSuccess = false;
                                    return;
                                }
                            }
                            Thread.Sleep(500);
                            Device.InputText(deviceID, "+1" + order.phoneT.phone);
                            Thread.Sleep(1000);
                            LogStatus(device, "Tap button Cập nhật số điện thoại --1");
                            uiXML = GetUIXml(deviceID);
                            WaitAndTapXML(deviceID, 2, "tiếp tụcresource", uiXML);
                            WaitAndTapXML(deviceID, 2, "cập nhật số di động", uiXML);
                            Thread.Sleep(1000);
                            uiXML = GetUIXml(deviceID);
                            LogStatus(device, "Tap button cập nhật số điện thoại --2");
                            WaitAndTapXML(deviceID, 2, "tiếp tụcresource", uiXML);
                            WaitAndTapXML(deviceID, 2, "cập nhật số di động", uiXML);

                            Thread.Sleep(5000);
                            if (CheckTextExist(deviceID, "Nhập mã từ sms"))
                            {
                                goto PUT_OTP;
                            }
                        }
                        else
                        {
                            LogStatus(device, "Can not get phone again -> Reg no veri:" + order.phoneT.message);

                            order.veriAcc = veriMailAfterPhonecheckBox.Checked;
                            order.veriByPhone = false;
                            order.veriDirectByPhone = false;
                            if (order.veriAcc)
                            {
                                for (int ii = 0; ii < 5; ii++)
                                {
                                    Device.Back(deviceID);
                                    if (CheckTextExist(deviceID, "Xác nhận tài khoản", 1))
                                    {
                                        goto REG_VERY;
                                    }
                                }
                            }
                            order.veriAcc = false;

                            goto REG_VERY;
                        }
                    }

                    LogStatus(device, "Phone duplicate too much -> Veri by mail:" + order.phoneT.message);

                    order.veriAcc = veriMailAfterPhonecheckBox.Checked;
                    order.veriByPhone = false;
                    order.veriDirectByPhone = false;
                    if (order.veriAcc)
                    {
                        for (int ii = 0; ii < 5; ii++)
                        {
                            Device.Back(deviceID);
                            if (CheckTextExist(deviceID, "Xác nhận tài khoản", 1))
                            {
                                goto REG_VERY;
                            }
                        }
                    }
                    order.veriAcc = false;

                    goto REG_VERY;
                }
            REG_VERY:
                if (!order.veriAcc) // Reg noveri
                {
                    // Reg noveri
                    device.isBlocking = false;
                    device.blockCount = 0;
                    order.isSuccess = true;

                    regOk++;
                    //totalSucc++;
                    string regBy = "phone";
                    if (device.regByMail)
                    {
                        regBy = "mail";
                    }
                    if (order.veriNvrOutSite)
                    {
                        regBy = "nvr outsite";
                    }
                    LogStatus(device, "Reg noveri OK - by " + regBy + "-" + order.phoneT.phone);
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green2;

                    if (order.nvrUpAvatar)
                    {
                        LogStatus(device, "NVR111 -   Up Avatar profile");

                        UploadAvatarProfile(deviceID, order, noveriCoverCheckBox.Checked, order.uploadContact); /// test code
                    }

                    //string uidNvr;
                    //if (!string.IsNullOrEmpty(order.uid))
                    //{
                    //    uidNvr = order.uid;
                    //}
                    //else
                    //{
                    //    uidNvr = FbUtil.GetUid(deviceID);
                    //}

                    //if (FbUtil.CheckLiveWall(uidNvr) == Constant.DIE)
                    //{
                    //    LogStatus(device, "checklivewall - Check live acc ---- color:Orange ");
                    //    LogStatus(device, "Acc die --- , return");

                    //    fail++;
                    //    device.blockCount++;
                    //    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                    //    Thread.Sleep(10000);
                    //    order.isSuccess = false;
                    //    return;
                    //}

                    if (order.uploadContact && order.upContactNew)
                    {
                        if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color: MidnightBlue");


                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.MidnightBlue;
                            Thread.Sleep(10000);
                            order.isSuccess = false;
                            return;
                        }
                        if (UploadContact(device, order.numberOfFriendRequest))
                        {
                            storeAccWithThread(isServer, order, deviceID,
                                    password, "noveri|tempmail", device.androidId, order.gender, yearOld, Constant.FALSE, device.log);
                            LogStatus(device, "Reg noveri success - has friend");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green3;
                            return;
                        }
                        else
                        {
                            Utility.storeAccWithThread(isServer, order, deviceID,
                             password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                            LogStatus(device, "Reg noveri success - No Contact");
                            return;
                        }
                    }
                    else
                    {
                        Utility.storeAccWithThread(isServer, order, deviceID,
                        password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                        LogStatus(device, "Reg noveri success - No Contact");
                        FbUtil.ClearAccountFbInSetting(deviceID, true);
                        return;
                    }
                }

                string cookies = FbUtil.GetCookieFromPhone(deviceID);
                string uid;
                if (!string.IsNullOrEmpty(order.uid))
                {
                    uid = order.uid;
                }
                else
                {
                    uid = FbUtil.GetUidFromCookie(cookies);
                }

                if (resendCheckBox.Checked)
                {
                    int timeResend = 10000;
                    try
                    {
                        timeResend = Convert.ToInt32(resendTextBox.Text) * 1000;
                    }
                    catch (Exception ex)
                    {
                        timeResend = 10000;
                    }
                    if (timeResend > 0)
                    {
                        LogStatus(device, "Nghỉ một chút ------------");
                        Thread.Sleep(timeResend);
                    }
                    else
                    {
                        LogStatus(device, "Thời gian nghỉ bị lỗi ");
                        Thread.Sleep(10000);
                    }

                    if (!WaitAndTapXML(deviceID, 1, "gửi lại"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 29.3, 48.7);
                    }
                    Thread.Sleep(3000);
                }

                if (sleep1MinuteCheckBox.Checked)
                {
                    int timeSleep = 60000;
                    try
                    {
                        timeSleep = Convert.ToInt32(delayTimeTextBox.Text) * 1000;
                    }
                    catch (Exception ex)
                    {
                        timeSleep = 60000;
                    }
                    if (timeSleep > 0)
                    {
                        LogStatus(device, "Nghỉ một chút ------------");
                        Thread.Sleep(timeSleep);
                    }
                    else
                    {
                        LogStatus(device, "Thời gian nghỉ bị lỗi ");
                        Thread.Sleep(60000);
                    }
                }

                if (FbUtil.CheckLiveWall(uid) == Constant.DIE)
                {
                    LogStatus(device, "Acc die trước khi nhập mail - Acc die --- , return color: MediumPurple");

                    fail++;
                    device.blockCount++;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.MediumPurple;
                    Thread.Sleep(10000);
                    order.isSuccess = false;
                    return;
                }

                if (order.veriBackup)
                {
                    FbUtil.PullBackupFb(uid, deviceID);
                    Thread.Sleep(2000);
                    LogStatus(device, "Pull backup finish");

                    Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                    LogStatus(device, "Clear cache facebook");

                    if (!FbUtil.OpenFacebookApp(device, device.clearCacheLite, 3))
                    {
                        LogStatus(device, "Can not open facebook -> return Color.Bisque");
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
                        Thread.Sleep(4000);
                        order.isSuccess = false;
                        return;
                    }


                    Device.PermissionReadContact(deviceID);
                    Device.PermissionCallPhone(deviceID);
                    Device.PermissionReadPhoneState(deviceID);
                    Device.PermissionCamera(deviceID);
                    FbUtil.PushBackupFb(uid, deviceID);

                    try
                    {
                        var dir = new DirectoryInfo("Authentication/" + uid);
                        dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                        dir.Delete(true);
                        File.Delete("Authentication/" + uid + ".zip");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("ex:" + ex.Message);
                    }
                    LogStatus(device, "Push Authentication finish");

                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

                    Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);

                    for (int i = 0; i < 2; i++)
                    {
                        uiXML = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "xác nhận", 1, uiXML))
                        {
                     
                            goto VERIFY_BY_EMAIL;
                        }
                        Device.GotoFbConfirm(deviceID);
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        uiXML = GetUIXml(deviceID);

                        if (CheckTextExist(deviceID, "Xác nhận", 1, uiXML))
                        {
                      
                            goto VERIFY_BY_EMAIL;
                        }
                        else
                        {
                            if (FbUtil.CheckLiveWall(uid) == Constant.DIE)
                            {
                                LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color: MediumSlateBlue");

                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.MediumSlateBlue;
                                Thread.Sleep(10000);
                                order.isSuccess = false;
                                return;
                            }

                            if (WaitAndTapXML(deviceID, 2, Language.UploadContactDialog2(), uiXML))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.9, 87.4);
                                LogStatus(device, "Tải danh bạ lên");
                            }

                            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                            {
                                LogStatus(device, "Đang tải danh bạ lên - bật");
                                WaitAndTapXML(deviceID, 2, Language.AllowAll());

                                if (CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    for (int k = 0; k < WaitAddContactCount; k++)
                                    {
                                        if (!CheckTextExist(deviceID, "đồng bộ"))
                                        {
                                            Console.WriteLine("đồng bộ:" + k);
                                            break;
                                        }
                                    }
                                }
                            }

                            WaitAndTapXML(deviceID, 2, Language.AllowAll());

                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 88.3, 14.3);
                            Thread.Sleep(2000);
                            if (!CheckTextExist(deviceID, Language.UpdateEmailVeri()))
                            {
                                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                                //Thread.Sleep(2000);
                                FbUtil.OpenFacebookApp(deviceID);
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 88.3, 14.3);
                            }
                        }
                    }
                }
            VERIFY_BY_EMAIL:
                string uiXML0 = GetUIXml(deviceID);

                if (CheckTextExist(deviceID, "nhậpmãxácnhận", 1, uiXML0)
                            || CheckTextExist(deviceID, "nhập số di động hợp lệ", 1, uiXML0)
                            || CheckTextExist(deviceID, "nhập email hợp lệ", 1, uiXML0)
                            || CheckTextExist(deviceID, "xác nhận", 1, uiXML0))
                {
                    LogStatus(device, "Đã vào màn hình xác nhận code 111111");

                    order.veriNhapMaXacNhan = true;

                    Thread.Sleep(2000);
                    if (!WaitAndTapXML(deviceID, 3, "tôikhôngnhậnđượcmãcheckable"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.5, 48.3);
                    }
                    Thread.Sleep(2000);
                    if (!WaitAndTapXML(deviceID, 3, "xácnhậnbằngemailresourceidclassandroidviewviewpackagecomfacebookkatanacontentdescxácnhậnbằngemailcheckablefa"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.8, 51.5);
                    }
                    Thread.Sleep(3000);

                    //KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.5, 54.1);
                    if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                    {
                        goto PUT_MAIL;
                    }
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.1, 60.3);
                    Thread.Sleep(3000);
                    if (WaitAndTapXML(deviceID, 2, "emailcheckable"))
                    {
                        goto PUT_MAIL;
                    }

                    string teeemp = GetUIXml(deviceID);

                    if (WaitAndTapXML(deviceID, 1, "xác nhận bằng email", teeemp)
                            || WaitAndTapXML(deviceID, 1, "xác nhận qua email", teeemp)
                            || WaitAndTapXML(deviceID, 1, "Nhập email", teeemp))
                    {
                        goto PUT_MAIL;
                    }

                    if (CheckTextExist(deviceID, "Hủy"))
                    {
                        Device.Back(deviceID);
                        WaitAndTapXML(deviceID, 1, "email");
                        goto PUT_MAIL;
                    }

                    LogStatus(device, "Kiểm tra máy có giao diện mới");
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                    if (WaitAndTapXML(deviceID, 1, "continue"))
                    {
                        Thread.Sleep(2000);
                        if (CheckTextExist(deviceID, "nhập mã xác nhận", 6))
                        {
                            Thread.Sleep(20000);
                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                    }
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.4, 15.8);
                    Thread.Sleep(1000);

                    for (int i = 0; i < 7; i++)
                    {
                        string xxxmmm = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                        {
                            LogStatus(device, "Nhập email -------------", 1000);
                            goto PUT_MAIL;
                        }
                        if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                        {
                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                    }
                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Thread.Sleep(2000);
                    for (int i = 0; i < 2; i++)
                    {
                        string xxxmmm = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "Nhập email", 1, xxxmmm))
                        {
                            LogStatus(device, "Nhập email -------------", 1000);
                            goto PUT_MAIL;
                        }
                        if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xxxmmm))
                        {
                            goto ENTER_CODE_CONFIRM_EMAIL;
                        }
                    }

                    Device.ScreenShoot(deviceID, false, "capture_.png");
                    string info = GetUIXml(deviceID);
                    System.IO.File.WriteAllText("local\\Err_" + deviceID + "XML.txt", info);
                    Thread.Sleep(60000);
                }
                uiXML = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, Language.UpdateEmailVeri(), 1, uiXML))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.8, 27.6);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.8, 27.6);
                }
                else
                {
                    if (!WaitAndTapXML(deviceID, 1, Language.ConfirmByEmail(), uiXML)
                         && !WaitAndTapXML(deviceID, 1, Language.ChangeEmailAddress(), uiXML))
                    {
                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        order.isSuccess = false;
                        LogVeriFailStatus(device);
                        return;
                    }
                }

                LogStatus(device, "Tap text box email address");

            PUT_MAIL:

                if (!order.veriNhapMaXacNhan)
                {
                    WaitAndTapXML(deviceID, 2, "địachỉemailcheckable"); // tap to email textbox
                }
                if (!WaitAndTapXML(deviceID, 1, "emailresourceid"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 76.2, 32.2);
                }
                if (IsMailEmpty(order.currentMail))
                {
                    if (order.isHotmail)
                    {
                        order.currentMail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);
                        if (order.currentMail == null || order.currentMail.status == Constant.FAIL)
                        {
                            if (getTrustMailcheckBox.Checked)
                            {
                                order.currentMail = Mail.GetTrustMailVandong();
                            }
                            if (order.currentMail == null || order.currentMail.status == Constant.FAIL)
                            {
                                LogStatus(device, "Hotmail error: ------------get gmail", 2000);
                                order.isHotmail = false;
                                order.tempmailType = Constant.GMAIL_SUPERTEAM;
                                LogStatus(device, "Get super gmail ");
                                order.currentMail = Mail.GetTempmail("", order.tempmailType, serverCacheMailTextbox.Text.Trim());

                                if (order.currentMail == null || order.currentMail.status == Constant.FAIL)
                                {
                                    order.tempmailType = Constant.TEMP_GENERATOR_1_SEC_EMAIL;
                                    LogStatus(device, "Get tempmail 1 sec   ", 2000);
                                    order.currentMail = Mail.GetTempmail("", order.tempmailType, serverCacheMailTextbox.Text.Trim());

                                }

                                if (order.currentMail != null)
                                {
                                    LogStatus(device, "Gmail ok kkkkkkkk: " + order.currentMail.message);
                                }
                                else
                                {




                                    // store nvr
                                    Utility.storeAccWithThread(isServer, order, deviceID,
                                password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                                    fail++;
                                    device.blockCount++;
                                    device.isBlocking = true;
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                                    order.isSuccess = false;
                                    Thread.Sleep(30000);
                                    return;

                                }
                            }
                        }
                    }
                    else
                    {
                        string duoimail = "";
                        if (fixDuoiMailCheckBox.Checked)
                        {
                            duoimail = fixDuoiMailTextBox.Text;
                        }

                        try
                        {
                            string duoiMail = activeDuoiMail;
                            if (randomDuoiMailcheckBox.Checked) duoiMail = "";
                            if (fixDuoiMailCheckBox.Checked) duoiMail = fixDuoiMailTextBox.Text;
                            if (getDuoiMailFromServercheckBox.Checked) duoiMail = GetDuoiMailFromServer();
                            int count = 10;
                            if (forceSellgmailcheckBox.Checked)
                            {
                                count = 40;
                            }
                            for (int i = 0; i < count; i++)
                            {
                                LogStatus(device, "Get mail lần:" + i);
                                order.currentMail = Mail.GetTempmail(duoiMail, order.tempmailType, serverCacheMailTextbox.Text.Trim());

                                if (!IsMailEmpty(order.currentMail))
                                {
                                    if (!checkMail.ContainsKey(order.currentMail.email))
                                    {
                                        LogStatus(device, "mail:" + order.currentMail.email);
                                        //LogRegStatus(dataGridView, device, order.tempmailType + " - " + order.currentMail.email);
                                        checkMail.Add(order.currentMail.email, order.currentMail.email);
                                        break;
                                    }
                                    else
                                    {
                                        LogStatus(device, "Color.DarkMagenta Trùng mail:" + order.currentMail.email);
                                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkMagenta;
                                        Thread.Sleep(10000);
                                    }
                                }
                                Thread.Sleep(10000);
                                if (thoatGmailcheckBox.Checked && i > 1)
                                {
                                    break;
                                }
                            }
                            int countTime = 30;
                            if (chuyenHotmailNhanhcheckBox.Checked)
                            {
                                countTime = 5;
                            }
                            if (!forceSellgmailcheckBox.Checked)
                            {
                                for (int i = 0; i < countTime; i++)
                                {
                                    if (!IsMailEmpty(order.currentMail))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Thread.Sleep(10000);
                                    }
                                    if (order.tempmailType == Constant.GMAIL_DICH_VU_GMAIL)
                                    {
                                        order.tempmailType = Constant.GMAIL_SUPERTEAM;
                                        LogStatus(device, "Get gmail super");
                                    }
                                    else if (order.tempmailType == Constant.GMAIL_SUPERTEAM)
                                    {
                                        order.tempmailType = Constant.GMAIL_SELL_GMAIL;
                                        LogStatus(device, "Get sell gmail ");
                                    }
                                    else if (order.tempmailType == Constant.GMAIL_SELL_GMAIL)
                                    {
                                        order.tempmailType = Constant.GMAIL_OTP_GMAIL;
                                    }
                                    else
                                    {
                                        order.tempmailType = Constant.GMAIL_DICH_VU_GMAIL;
                                        LogStatus(device, "Get dich vu gmail ");
                                    }
                                    if (!getDvgmcheckBox.Checked && order.tempmailType == Constant.GMAIL_DICH_VU_GMAIL)
                                    {
                                        continue;
                                    }
                                    if (!getSellMailCheckbox.Checked && order.tempmailType == Constant.GMAIL_SELL_GMAIL)
                                    {
                                        continue;
                                    }
                                    if (!getSuperMailcheckBox.Checked && order.tempmailType == Constant.GMAIL_SUPERTEAM)
                                    {
                                        continue;
                                    }
                                    order.currentMail = Mail.GetTempmail(duoiMail, order.tempmailType, serverCacheMailTextbox.Text.Trim());
                                }
                            }
                            if (IsMailEmpty(order.currentMail) && forceGmailcheckBox.Checked)
                            {
                                LogStatus(device, "Không thể lấy gmail -> store nvr - color: DarkSeaGreen");
                                // store nvr
                                Utility.storeAccWithThread(isServer, order, deviceID,
                            password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                                fail++;
                                device.blockCount++;
                                device.isBlocking = true;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                                order.isSuccess = false;
                                return;
                            }

                            if (IsMailEmpty(order.currentMail)) // get hotmail
                            {
                                order.isHotmail = true;
                                order.currentMail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 1, otpVandongcheckBox.Checked);
                                LogStatus(device, "Get hotmail");
                            }
                            if (IsMailEmpty(order.currentMail)) // get tempmail
                            {
                                order.isHotmail = false;
                                LogStatus(device, " tempmail generator");
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkGoldenrod;

                                order.tempmailType = Constant.TEMP_GENERATOR_1_SEC_EMAIL;
                                order.currentMail = Mail.GetTempmail(duoiMail, order.tempmailType, serverCacheMailTextbox.Text.Trim());
                                Thread.Sleep(2000);
                            }
                            if (!IsMailEmpty(order.currentMail))
                            {

                                Thread.Sleep(3000);
                            }

                        }
                        catch (Exception ex)
                        {
                            LogStatus(device, "Lỗi trang mail exception");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkMagenta;
                            return;
                        }
                    }
                }

                if (IsMailEmpty(order.currentMail))
                {
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                    LogStatus(device, "Không thể lấy mail, kiểm tra hết mail - nghỉ 5 phút");
                    //Utility.storeAccWithThread(isServer, order, deviceID,
                    //                            password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                    Thread.Sleep(600000);
                    goto VERIFY_BY_EMAIL;
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;

                    order.isSuccess = false;
                    LogVeriFailStatus(device);
                    return;
                }
                LogStatus(device, "Put :" + order.currentMail.email + "-" + order.currentMail.source + " --- " + order.currentMail.message);
                if (order.isHotmail)
                {
                    LogRegStatus(dataGridView, device, "Hotmail - " + order.currentMail.email + " - " + order.currentMail.message + order.currentMail.source);
                }
                else
                {
                    LogRegStatus(dataGridView, device, order.tempmailType + " - " + order.currentMail.email + " - " + order.currentMail.message);
                }

                if (order.veriNhapMaXacNhan)
                {
                    InputMail(deviceID, order.currentMail.email, inputStringMailCheckBox.Checked);
                    //Thread.Sleep(3000);
                }
                else
                {
                    InputMail(deviceID, order.currentMail.email, inputStringMailCheckBox.Checked);
                }
                
                LogStatus(device, "Nhập mail xonggggggggggggggg", 2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 95.2); // xong
                if (!CheckTextExist(deviceID, order.currentMail.email.Replace(".", ""), 1))
                {
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightPink;
                    LogStatus(device, "Nhập mail thất bại- Nhập mail lại", 25000);
                    if (!WaitAndTapXML(deviceID, 1, "emailresourceid"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 76.2, 32.2);
                    }
                    InputMail(deviceID, order.currentMail.email, inputStringMailCheckBox.Checked);
                    Thread.Sleep(3000);
                }
                Utility.Log("Tap button continue", status);
                uiXML = GetUIXml(deviceID);
                if (!WaitAndTapXML(deviceID, 1, "tiếp", uiXML))
                {
                    WaitAndTapXML(deviceID, 2, Language.UpdateEmailVeri(), uiXML);
                    WaitAndTapXML(deviceID, 2, "cập nhật", uiXML);

                    Utility.WaitAndTapXML(deviceID, 10, Language.Continue(), uiXML);
                }


                for (int i = 0; i < 20; i++)
                {
                    if (!CheckTextExist(deviceID, "Đang xử lý"))
                    {
                        break;
                    }
                }
                uiXML = GetUIXml(deviceID);
                if (WaitAndTapXML(deviceID, 1, Language.UpdateEmailVeri(), uiXML)
                    || WaitAndTapXML(deviceID, 2, "cập nhật", uiXML))
                {
                    LogStatus(device, "Cập nhật tài khoản mail mới");
                }
                if (CheckTextExist(deviceID, "nhập mã xác nhận", 1))
                {
                    goto PUT_OTP;
                }
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 10 + ran.Next(10, 80), 31.7);
                Thread.Sleep(1500);
                uiXML = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "Tài khoản xác nhận", 1, uiXML))
                {
                    LogStatus(device, "Acc đã được veri");
                    order.isSuccess = false;
                    LogVeriFailStatus(device);
                    return;
                }
                if (CheckTextExist(deviceID, "tài khoản Facebook khác", 1, uiXML))
                {
                    LogStatus(device, "Lỗi email, chạy lại");
                    Thread.Sleep(10000);
                    order.isSuccess = false;
                    return;
                }
                if (CheckTextExist(deviceID, Language.EmailInCorrect(), 1, uiXML) || CheckTextExist(deviceID, "email hợp lệ", 1, uiXML)
                    || CheckTextExist(deviceID, "có tài khoản liên kết", 1, uiXML))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 95.4, 22.7);
                    WaitAndTapXML(deviceID, 2, "địachỉemailcheckable");
                    Device.MoveEndTextbox(deviceID);
                    Device.DeleteChars(deviceID, order.currentMail.email.Length);

                    string uid1;
                    if (!string.IsNullOrEmpty(order.uid))
                    {
                        uid1 = order.uid;
                    }
                    else
                    {
                        uid1 = FbUtil.GetUid(deviceID);
                    }
                    if (FbUtil.CheckLiveWall(uid1) == Constant.DIE)
                    {
                        LogStatus(device, "checklivewall - Acc die, return color: Orchid ");

                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orchid;
                        LogVeriFailStatus(device);
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }
                    if (boAccNhapMailSaicheckBox.Checked)
                    {
                        LogStatus(device, "Nhập mail saiiiiii22222iiii,  return color: Orchid ");
                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orchid;
                        LogVeriFailStatus(device);
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }

                    string duoimail = "";
                    if (fixDuoiMailCheckBox.Checked)
                    {
                        duoimail = fixDuoiMailTextBox.Text;
                    }

                    order.currentMail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);
                    LogStatus(device, "Email không hợp lệ - dùng mail khác:");
                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    Thread.Sleep(10000);

                    goto VERIFY_BY_EMAIL;
                }
                if (!CheckTextExist(deviceID, "nhập mã xác nhận", 10))
                {
                    if (boAccNhapMailSaicheckBox.Checked)
                    {
                        LogStatus(device, "Nhập mail sai333333iiiiiiiii,  return color: Orchid ");
                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orchid;
                        LogVeriFailStatus(device);
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSalmon;
                    LogStatus(device, "Nhập mail thất bại", 20000);

                    goto VERIFY_BY_EMAIL;
                }
                LogStatus(device, "Check acc live/die");

                Log("Check enter screen enter code - if not - return", status);

            PUT_OTP:

                LogStatus(device, " Chờ 4s check lock");
                Thread.Sleep(4000);
                uid = order.uid;
                if (string.IsNullOrEmpty(order.uid))
                {
                    uid = FbUtil.GetUid(deviceID);
                }
                if (FbUtil.CheckLiveWall(uid) == Constant.DIE)
                {
                    LogStatus(device, "checklivewall - Acc die, return color: LightSalmon");

                    fail++;
                    device.blockCount++;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightSalmon;
                    LogVeriFailStatus(device);
                    Thread.Sleep(1000);
                    order.isSuccess = false;
                    return;
                }

                string otp = "";

                if (order.veriByPhone || order.veriDirectByPhone)
                {
                    if (order.veriByPhone)
                    {
                        Thread.Sleep(1000);
                    }
                    LogStatus(device, "Bắt đầu get otp phone");
                    otp = GetPhoneCode(order.phoneT);
                }
                else
                {
                    if (order.currentMail.password == "tempmail")
                    {
                        LogStatus(device, "Bắt đầu get otp tempmail");
                        if (order.tempmailType == Constant.TEMP_FAKE_EMAIL)
                        {
                            LogStatus(device, "Open mail web:" + order.currentMail.email);
                            string url = "";
                            if (order.tempmailType == Constant.TEMP_GENERATOR_EMAIL)
                            {
                                url = "https://emailfake.com/" + order.currentMail.email;
                                Device.OpenWeb(deviceID, url);
                            }

                            LogStatus(device, "get code mail:" + order.currentMail.email);
                            Thread.Sleep(2000);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        }
                        else if (order.tempmailType == Constant.TEMP_GENERATOR_EMAIL)
                        {
                            LogStatus(device, "Open mail web:" + order.currentMail.email);
                            string url = "";
                            if (order.tempmailType == Constant.TEMP_GENERATOR_EMAIL)
                            {
                                url = "https://generator.email/" + order.currentMail.email;
                                Device.OpenWeb(deviceID, url);
                            }

                            LogStatus(device, "get code mail:" + order.currentMail.email);
                            Thread.Sleep(2000);
                            Device.OpenApp(deviceID, Constant.FACEBOOK_PACKAGE);
                        }
                    }
                    else if (order.currentMail.password == Constant.GMAIL_SELL_GMAIL)
                    {
                        LogStatus(device, "Start Get Code Mail - " + order.tempmailType);
                    }
                    else
                    {
                        LogStatus(device, "Start Get Code Mail - Hotmail");
                    }

                    otp = GetOtp(deviceID, order.tempmailType, order.currentMail, 20);

                    LogStatus(device, "Get Code Mail" + otp);
                }

                if (otp == Constant.FAIL || string.IsNullOrEmpty(otp))
                {
                    if (order.veriDirectByPhone)
                    {
                        LogStatus(device, "Get code fail: Storing noveri");
                        Utility.storeAccWithThread(isServer, order, deviceID,
                            password, order.phoneT.phone + "|phone", "", order.gender, yearOld, Constant.FALSE, device.log);

                        noVerified++;
                        device.noveri++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        LogVeriFailStatus(device);
                        order.isSuccess = false;
                        return;
                    }
                    if (order.veriByPhone)  // Try again
                    {
                        WaitAndTapXML(deviceID, 2, Language.ChangePhoneNumber());
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 24.6, 24.6); // Tap to input phone
                        if (CheckTextExist(deviceID, "cập nhật số điện"))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.5, 32.0);
                        }

                        Thread.Sleep(1000);

                        order.phoneT = Phone.GetPhoneTextNow(order.phoneT);
                        if (IsDigitsOnly(order.phoneT.phone))
                        {
                            order.currentMail.email = order.phoneT.phone;
                            order.currentMail.password = "phone";
                        }

                        Device.InputText(deviceID, "+1" + order.phoneT.phone);
                        //Thread.Sleep(1000);
                        uiXML = GetUIXml(deviceID);
                        WaitAndTapXML(deviceID, 2, "tiếp tục resource", uiXML);
                        WaitAndTapXML(deviceID, 2, "cập nhật số điện", uiXML);
                        Thread.Sleep(1000);
                        for (int i = 0; i < 2; i++)
                        {
                            if (!WaitAndTapXML(deviceID, 2, Language.ConfirmResource()))
                            {
                                LogStatus(device, "Get phone fail " + (i + 1));
                                Device.DeleteChars(deviceID, 13);
                                order.phoneT = Phone.GetPhoneTextNow(order.phoneT);
                                if (IsDigitsOnly(order.phoneT.phone))
                                {
                                    order.currentMail.email = order.phoneT.phone;
                                    order.currentMail.password = "phone";
                                }

                                Device.InputText(deviceID, "+1" + order.phoneT.phone);
                                //Thread.Sleep(1000);

                                uiXML = GetUIXml(deviceID);
                                WaitAndTapXML(deviceID, 2, "tiếp tục resource", uiXML);
                                WaitAndTapXML(deviceID, 2, "cập nhật số điện", uiXML);
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                break;
                            }
                            if (i == 1)
                            {
                                LogStatus(device, "Get phone fail: bỏ acc");

                                noVerified++;
                                device.noveri++;
                                device.blockCount++;
                                device.isBlocking = true;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                                order.isSuccess = false;
                                return; // Can not get phone
                            }
                        }

                        otp = GetPhoneCode(order.phoneT);

                        if (otp == Constant.FAIL || string.IsNullOrEmpty(otp))
                        {
                            LogStatus(device, "Get code fail: bỏ acc");

                            noVerified++;
                            device.noveri++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            order.isSuccess = false;
                            return;
                        }
                    }
                    else
                    {
                        LogStatus(device, "Get otp mail again");
                        if (CheckTextExist(deviceID, Language.ConfirmByEmail()))
                        {
                            goto VERIFY_BY_EMAIL;
                        }
                        order.currentMail.mailRepository = null;
                        if (!string.IsNullOrEmpty(order.currentMail.refreshToken))
                        {
                            order.currentMail.accessToken = ServerApi.GetAccessToken(order.currentMail.clientId, order.currentMail.refreshToken);
                        }
                        otp = GetOtp(deviceID, order.tempmailType, order.currentMail, 20);
                        if (otp == Constant.FAIL || string.IsNullOrEmpty(otp))
                        {
                            LogStatus(device, "Get OTP fail: bỏ acc----------------------------");
                            return;
                        }
                    }
                }

                if (WaitAndTapXML(deviceID, 1, "mãxácnhậncheckable") || WaitAndTapXML(deviceID, 1, "mãxácnhậnresource"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 39.6, 28.4);
                    Thread.Sleep(1000);
                    LogStatus(device, "Confirm code: " + otp);

                    InputConfirmCode(deviceID, otp);
                    order.hasOtp = true;
                    if (choPutOtpcheckBox.Checked)
                    {
                        LogStatus(device, "Chờ put otp : 60s");
                        Thread.Sleep(60000);
                    }
                    LogStatus(device, "Put code finish");
                    //dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green2;
                }
                else
                {
                    LogStatus(device, "veri acc - Can not go to confirm screen -------------------");
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Azure;
                    Device.ScreenShoot(deviceID, false, "capture_confirm_.png");
                    string info = GetUIXml(deviceID);
                    System.IO.File.WriteAllText("local\\Err_" + deviceID + "XML.txt", info);
                    Thread.Sleep(10000);
                    if (order.veriAcc && !order.isReverify)
                    {
                        // TODO
                        // Delete acc in wait to veri
                        //....

                        Utility.storeAccWithThread(isServer, order, deviceID,
                            password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                        return;
                    }


                    noVerified++;
                    device.noveri++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                    order.isSuccess = false;
                    LogVeriFailStatus(device);
                    return;
                }

                Thread.Sleep(1000);
                if (order.veriNhapMaXacNhan)
                {
                    WaitAndTapXML(deviceID, 1, "tiếp");
                    Thread.Sleep(5000);
                    WaitAndTapXML(deviceID, 3, "ok");
                }
                else
                {
                    if (!WaitAndTapXML(deviceID, 8, Language.ConfirmResource()))
                    {
                        Device.TapByPercentDelay(deviceID, 49.4, 39.4);
                        Device.TapByPercentDelay(deviceID, 49.4, 39.4);
                    }
                    if (order.isReverify && CheckTextExist(deviceID, "Lưu thông tin"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 74.0 + ran.Next(1, 10), 95.6);
                    }
                }

                LogStatus(device, "Start Check confirm code");
                for (int i = 0; i < 20; i++)
                {
                    if (!CheckTextExist(deviceID, "Đang xác nhận"))
                    {
                        break;
                    }
                }
                uiXML = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "Nhập mã xác nhận", 1, uiXML))
                {

                    LogStatus(device, "Vào lại màn hình nhập mã xác nhận---1---check again", 10000);
                    if (CheckTextExist(deviceID, "Nhập mã xác nhận", 1))
                    {
                        string bbb1;
                        if (!string.IsNullOrEmpty(order.uid))
                        {
                            bbb1 = order.uid;
                        }
                        else
                        {
                            bbb1 = FbUtil.GetUid(deviceID);
                        }
                        if (FbUtil.CheckLiveWall(bbb1) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Acc die sau khi nhap code --- , return - Nghỉ 1 phút color: Yellow");


                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                            Thread.Sleep(6000);
                            order.isSuccess = false;
                            return;
                        }
                        Device.GotoFbProfileEdit(deviceID);
                        Thread.Sleep(1000);

                        if (CheckTextExist(deviceID, "tryagain", 1))
                        {
                            order.hasAvatar = false;

                            goto STORE_INFO;
                        }

                        if (UploadAvatarProfile(deviceID, order, coverCheckBox.Checked) == -1)
                        {
                            order.hasAvatar = false;

                            goto STORE_INFO;
                        }

                        goto SET_2FA;
                    }
                }
                //if (CheckTextExist(deviceID, "thêm ảnh", 1, uiXML)
                //        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML))
                //{
                //    if (noSuggestCheckbox.Checked && !order.hasAvatar && !order.has2Fa)
                //    {
                //        goto SET_2FA;
                //    }
                //    if (WaitAndTapXML(deviceID, 2, "textthêmảnhresourceid", uiXML)
                //        || WaitAndTapXML(deviceID, 1, "thêmảnhcheckablef", uiXML)) // Vào luồng up avatar cơ bản
                //    {
                //        LogStatus(device, "Upload avatar normal -------------");
                //        if (FbUtil.UploadAvatarNormal(deviceID))
                //        {
                //            //order.accType = Constant.ACC_TYPE_UP_AVATAR_NORMAL;
                //            //order.checkAccHasAvatar = true;
                //            order.hasAddFriend = UploadContact2(device, 5);

                //            goto SET_2FA;
                //        } else
                //        {
                //            int check = UploadAvatarProfile(deviceID, order, false);
                //            if (check == -1)
                //            {
                //                order.hasAvatar = false;

                //                goto STORE_INFO;
                //            }
                //            order.hasAddFriend = UploadContact2(device, 5);

                //            goto SET_2FA;
                //        }

                //        if (WaitAndTapXML(deviceID, 1, "Xong"))
                //        {
                //            order.checkAccHasAvatar = true;
                //            order.hasAddFriend = UploadContact2(device, 5);

                //            goto SET_2FA;
                //        }
                //    }
                //}
                //if (CheckTextExist(deviceID, "Lưu thông tin", 1, uiXML)
                //    || WaitAndTapXML(deviceID, 1, "ok", uiXML)
                //    || CheckTextExist(deviceID, "Nhập mã xác nhận", 1, uiXML))
                //{
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 74.0 + ran.Next(1, 10), 85.6);
                LogStatus(device, "lưu thông tin-------------------------------3333");
                Thread.Sleep(11000);
                string bbb;
                if (!string.IsNullOrEmpty(order.uid))
                {
                    bbb = order.uid;
                }
                else
                {
                    bbb = FbUtil.GetUid(deviceID);
                }
                if (FbUtil.CheckLiveWall(bbb) == Constant.DIE)
                {
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                    if (chuyenQuaveriGmailcheckBox.Checked)
                    {
                        LogStatus(device, "Chuyển qua veri gmaillllllllllllllllllll ------------: yellow");
                        order.error_code = 101;
                        device.chuyenQuaVeriGmail = true;
                    }
                    else
                    {
                        if (order.loginAccMoiBusiness && chuyenQuaMoiKatanacheckBox.Checked)
                        {
                            LogStatus(device, "Chuyển qua mồi katana ------------: yellow");
                            order.error_code = 101;
                            device.chuyenQuaMoiKatana = true;
                        }
                        else if (randomVersioncheckBox.Checked)
                        {
                            LogStatus(device, "Chuyển qua version khác -----------------");
                            order.error_code = 101;
                            device.randomVersion = true;
                        }
                        else
                        {
                            LogStatus(device, "checklivewall - Acc die sau khi nhap code --- , return - Nghỉ 1 phút color: Yellow");
                            fail++;
                            device.blockCount++;

                            Thread.Sleep(6000);
                        }
                    }

                    order.isSuccess = false;
                    return;
                }

                LogStatus(device, "Acc ok, upload avatar profile -----------1111-");
                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green1;
                if (moiAccRegThanhCongcheckBox.Checked)
                {
                    device.clearCache = false;
                }
                if (logProxycheckBox.Checked && order.hasproxy)
                {
                    string ip = Device.GetPublicIpLumtest(deviceID);
                    GoogleSheet.WriteAccount(ip, "proxy_logs");
                }
                if (catProxySauVericheckBox.Checked && order.hasproxy)  // has proxy
                {

                    Device.DisableWifi(deviceID);
                    //Device.AirplaneOn(deviceID);
                    //Thread.Sleep(1000);
                    //Device.AirplaneOff(deviceID);

                    LogStatus(device, "Stop proxy - disable wifi", 3000);
                    // Check internet
                    Device.GotoFbProfileEdit(deviceID);
                    Thread.Sleep(5000);
                    if (CheckTextExist(deviceID, "internet"))
                    {
                        goto STORE_INFO;
                    }
                }

                int check = -1;
                if (order.hasAvatar)
                {
                    check = UploadAvatarProfile(deviceID, order, coverCheckBox.Checked);
                    LogStatus(device, "Upload avatar xong: " + check);
                }
                if (order.upContactNew)
                {
                    order.hasAddFriend = UploadContact2(device, order.numberOfFriendRequest);
                    LogStatus(device, "UploadContact xong: " + order.hasAddFriend);
                }
                
                if (!order.hasAddFriend && CheckTextExist(deviceID, "nhập mã xác nhận", 1))
                {
                    LogStatus(device, "Veri fail mà không biết ----", 2000);
                    order.isSuccess = false;
                    Utility.storeAccWithThread(isServer, order, deviceID,
                                password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    order.isSuccess = false;

                    return;
                }
                if (order.hasAvatar && check == -1)
                {
                    order.hasAvatar = false;

                    goto STORE_INFO;
                }
                
                goto SET_2FA;

                //}

                uiXML = GetUIXml(deviceID);

                if (CheckTextExist(deviceID, "truycậpvịtrí", 1, uiXML))
                {
                    Device.TapByPercent(deviceID, 53.7, 65.1, 2000);
                    uiXML = GetUIXml(deviceID);
                }
                if (CheckTextExist(deviceID, "thêm ảnh", 1, uiXML)
                        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML))
                {
                    if (noSuggestCheckbox.Checked && !order.hasAvatar && !order.has2Fa)
                    {
                        goto SET_2FA;
                    }
                    goto UPLOAD_AVATAR;
                }
                if (CheckTextExist(deviceID, Language.ConfirmCodeError(), 1, uiXML) // Check confirm code error
                        || CheckTextExist(deviceID, Language.ConfirmCodeNotCorrect(), 1, uiXML))
                {
                    //Utility.storeAccWithThread(isServer, order, deviceID,
                    //    password, order.currentMail.toString(), "", order.gender, yearOld, Constant.FALSE, device.log);

                    noVerified++;
                    device.noveri++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                    order.isSuccess = false;
                    LogVeriFailStatus(device);
                    return;
                }
                if (CheckLock(device, deviceID, uiXML))
                {
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                    if (chuyenQuaveriGmailcheckBox.Checked)
                    {
                        LogStatus(device, "Chuyển qua veri gmaillllllllllllllllllll ------------: yellow");
                        order.error_code = 101;
                        device.chuyenQuaVeriGmail = true;
                    }
                    else
                    {
                        if (order.loginAccMoiBusiness && chuyenQuaMoiKatanacheckBox.Checked)
                        {
                            LogStatus(device, "Chuyển qua mồi katana ------------: yellow");
                            order.error_code = 101;
                            device.chuyenQuaMoiKatana = true;
                        }
                        else if (randomVersioncheckBox.Checked)
                        {
                            LogStatus(device, "Chuyển qua version khác -----------------");
                            order.error_code = 101;
                            device.randomVersion = true;
                        }
                        else
                        {
                            LogStatus(device, "checklivewall - Acc die sau khi nhap code --- , return - Nghỉ 1 phút color: Yellow");
                            fail++;
                            device.blockCount++;

                            Thread.Sleep(6000);
                        }
                    }

                    return;
                }
                string uidd11dd1;
                if (!string.IsNullOrEmpty(order.uid))
                {
                    uidd11dd1 = order.uid;
                }
                else
                {
                    uidd11dd1 = FbUtil.GetUid(deviceID);
                }
                if (FbUtil.CheckLiveWall(uidd11dd1) == Constant.DIE)
                {
                    LogStatus(device, "checklivewall - Acc die sau khi nhap code --- , return - Nghỉ 1 phút color: Yellow");

                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                    fail++;
                    device.blockCount++;
                    if (chuyenQuaveriGmailcheckBox.Checked)
                    {
                        LogStatus(device, "Chuyển qua veri gmaillllllllllllllllllll ------------: yellow");
                        order.error_code = 101;
                        device.chuyenQuaVeriGmail = true;
                    }
                    else
                    {
                        if (order.loginAccMoiBusiness && chuyenQuaMoiKatanacheckBox.Checked)
                        {
                            LogStatus(device, "Chuyển qua mồi katana ------------: yellow");
                            order.error_code = 101;
                            device.chuyenQuaMoiKatana = true;
                        }
                        else if (randomVersioncheckBox.Checked)
                        {
                            LogStatus(device, "Chuyển qua version khác -----------------");
                            order.error_code = 101;
                            device.randomVersion = true;
                        }
                        else
                        {
                            LogStatus(device, "checklivewall - Acc die sau khi nhap code --- , return - Nghỉ 1 phút color: Yellow");
                            fail++;
                            device.blockCount++;

                            Thread.Sleep(6000);
                        }
                    }

                    order.isSuccess = false;
                    return;
                }

                if (CheckTextExist(deviceID, "Xác nhận tài khoản", 1, uiXML))
                {
                    if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                    {
                        LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color: DarkSlateBlue");

                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSlateBlue;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }
                    if (CheckTextExist(deviceID, "thêmảnh", 1, uiXML)
                        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML))
                    {
                        goto UPLOAD_AVATAR;
                    }

                    LogStatus(device, "Put code fail- retry ---------------");
                    Device.TapByPercent(deviceID, 36.3, 28.0, 500);
                    Device.TapByPercent(deviceID, 36.3, 28.0);

                    Device.DeleteChars(deviceID, 7);
                    //Utility.InputText(deviceID, code, false);
                    InputConfirmCode(deviceID, otp);
                    Thread.Sleep(1000);
                    if (!WaitAndTapXML(deviceID, 10, Language.ConfirmResource()))
                    {
                        Device.TapByPercentDelay(deviceID, 49.4, 39.4);
                        Device.TapByPercentDelay(deviceID, 49.4, 39.4);
                    }

                    Thread.Sleep(3000);
                    if (CheckTextExist(deviceID, "Xác nhận tài khoản", 1, uiXML))
                    {
                        LogStatus(device, "Acc verify fail ----------------------");
                        // Check lock
                        string uidddd1;
                        if (!string.IsNullOrEmpty(order.uid))
                        {
                            uidddd1 = order.uid;
                        }
                        else
                        {
                            uidddd1 = FbUtil.GetUid(deviceID);
                        }
                        if (FbUtil.CheckLiveWall(uidddd1) == Constant.DIE)
                        {
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;

                            fail++;
                            device.blockCount++;
                            if (chuyenQuaveriGmailcheckBox.Checked)
                            {
                                LogStatus(device, "Chuyển qua veri gmaillllllllllllllllllll ------------: yellow");
                                order.error_code = 101;
                                device.chuyenQuaVeriGmail = true;
                            }
                            else
                            {
                                if (order.loginAccMoiBusiness && chuyenQuaMoiKatanacheckBox.Checked)
                                {
                                    LogStatus(device, "Chuyển qua mồi katana ------------: yellow");
                                    order.error_code = 101;
                                    device.chuyenQuaMoiKatana = true;
                                }
                                else
                                {
                                    LogStatus(device, "checklivewall - Acc die sau khi nhap code --- , return - Nghỉ 1 phút color: Yellow");
                                    fail++;
                                    device.blockCount++;

                                    Thread.Sleep(6000);
                                }
                            }

                            order.isSuccess = false;
                            return;
                        }
                        if (CheckTextExist(deviceID, "thêmảnh"))
                        {
                            goto UPLOAD_AVATAR;
                        }
                        //Utility.storeAccWithThread(isServer, order, deviceID,
                        //    password, order.currentMail.toString(), "", order.gender, yearOld, Constant.FALSE, device.log);

                        noVerified++;
                        device.noveri++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Yellow;
                        order.isSuccess = false;
                        return;
                    }
                }

                if (!order.isReverify) // Reg direct
                {
                    uiXML = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, "textthêmảnhresourceid", 1, uiXML)
                        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML)
                        || CheckTextExist(deviceID, "thêmảnhcheckablef", 1))
                    {
                        order.isVeriOk = true;
                        if (noSuggestCheckbox.Checked && !order.hasAvatar && !order.has2Fa)
                        {
                            goto SET_2FA;
                        }
                        goto UPLOAD_AVATAR;
                    }
                    else
                    {
                        // Check lock
                        string uidddd1;
                        if (!string.IsNullOrEmpty(order.uid))
                        {
                            uidddd1 = order.uid;
                        }
                        else
                        {
                            uidddd1 = FbUtil.GetUid(deviceID);
                        }
                        if (FbUtil.CheckLiveWall(uidddd1) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Acc die --- , return color:Firebrick color: Firebrick");

                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Firebrick;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            return;
                        }

                        if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                        {
                            LogStatus(device, "Đang tải danh bạ lên - bật");
                            uiXML = GetUIXml(deviceID);
                            if (CheckTextExist(deviceID, "nhập liên hệ", 1, uiXML)
                                || CheckTextExist(deviceID, "bỏ qua bước này", 1, uiXML))
                            {
                                if (!WaitAndTapXML(deviceID, 1, "nhập liên hệ resource"))
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 76.8, 63.5);

                                }
                                WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);
                            }
                            WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);

                            if (CheckTextExist(deviceID, "đồng bộ"))
                            {
                                for (int k = 0; k < WaitAddContactCount; k++)
                                {
                                    if (!CheckTextExist(deviceID, "đồng bộ"))
                                    {
                                        Console.WriteLine("đồng bộ:" + k);
                                        break;
                                    }
                                }
                            }
                            LogStatus(device, "Đồng bộ danh bạ xong - add 5 friend");
                            Thread.Sleep(5000);
                            //uiXML = GetUIXml(deviceID);
                            //if (WaitAndTapXML(deviceID, 3, "Thêm 5 người bạn", uiXML)
                            //    || WaitAndTapXML(deviceID, 3, "Thêm 5 bạn", uiXML)
                            //    || WaitAndTapXML(deviceID, 3, "Thêm 10 người bạn", uiXML)
                            //    || WaitAndTapXML(deviceID, 3, "Thêm 10 bạn", uiXML))
                            if (FindImageAndTap(deviceID, THEM_5BB, 10))
                            {
                                if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                                {
                                    Device.TapByPercent(deviceID, 48.0, 96.8);
                                }
                                Device.TapByPercent(deviceID, 48.0, 96.8);
                                Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                                WaitAndTapXML(deviceID, 1, "Xong");
                                order.hasAddFriend = true;
                                LogStatus(device, "Đã thêm 5 friend bình thường");
                                Thread.Sleep(2000);

                            }
                            WaitAndTapXML(deviceID, 1, Language.Next());
                            //uiXML = GetUIXml(deviceID);
                            //WaitAndTapXML(deviceID, 1, "Thêm 5 bạn bè", uiXML);
                            FindImageAndTap(deviceID, THEM_5BB, 1);
                            goto UPLOAD_AVATAR;
                        }


                        LogStatus(device, "Can't find Choose From Gallery -> bỏ NoVeri");

                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                        //Utility.storeAccWithThread(isServer, order, deviceID,
                        //    password, order.currentMail.toString(), "", order.gender, yearOld, Constant.FALSE, device.log);

                        noVerified++;
                        device.noveri++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }
                }
                else // Re verify acc noveri
                {
                    regOk++;
                    device.isBlocking = false;
                    device.blockCount = 0;

                    order.isVeriOk = true;
                    order.isSuccess = true;
                    if (addStatusCheckBox.Checked)
                    {
                        LogStatus(device, "Add status");
                        FbUtil.PostStatus(deviceID);
                    }

                    if (addFriendCheckBox.Checked)
                    {
                        FbUtil.AddFriend(deviceID);
                    }
                    if (!order.hasAvatar && !order.has2Fa)
                    {
                        goto UPLOAD_AVATAR;
                    }
                    if (order.account != null && string.IsNullOrEmpty(order.account.qrCode))
                    {
                        string uixxml = GetUIXml(deviceID);
                        LogStatus(device, "Reupload contact-----");
                        if (CheckTextExist(deviceID, "truycậpvịtrí", 1, uixxml))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                            Thread.Sleep(2000);

                        }
                        //if (WaitAndTapXML(deviceID, 1, "descbậtcheckable", uixxml) || WaitAndTapXML(deviceID, 1, "descbật", uixxml))
                        if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                        {
                            if (CheckTextExist(deviceID, "đồng bộ"))
                            {
                                for (int k = 0; k < WaitAddContactCount; k++)
                                {
                                    if (!CheckTextExist(deviceID, "đồng bộ"))
                                    {
                                        Console.WriteLine("đồng bộ:" + k);
                                        break;
                                    }
                                }
                            }

                            if (FindImageAndTap(deviceID, THEM_5BB, 1))
                            {

                                WaitAndTapXML(deviceID, 1, Language.AllowAll());
                                if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                }
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                                WaitAndTapXML(deviceID, 1, "Xong");
                            }
                        }
                        if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:MediumBlue");


                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.MediumBlue;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            return;
                        }
                        order.hasAddFriend = UploadContact(device, order.numberOfFriendRequest);

                    }
                    //goto SET_2FA;

                    uiXML = GetUIXml(deviceID);
                    LogStatus(device, "Check have Choose from gallery");
                    if (CheckTextExist(deviceID, "textthêmảnhresourceid", 1, uiXML)
                        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML))
                    {
                        // Bỏ qua
                        LogStatus(device, "Check have Choose from gallery -> skip");
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.1, 7.5);
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.1, 7.5);
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.6, 63.4);
                        Thread.Sleep(5000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.1, 7.5);
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.0, 59.7);
                        Thread.Sleep(1000);

                        if (addStatusCheckBox.Checked)
                        {
                            LogStatus(device, "Add status");
                            FbUtil.PostStatus(deviceID);
                        }
                        if (addFriendCheckBox.Checked)
                        {
                            FbUtil.AddFriend(deviceID);
                        }
                        goto SET_2FA;
                    }
                    WaitAndTapXML(deviceID, 2, "OKresource", uiXML);
                    WaitAndTapXML(deviceID, 1, "bật", uiXML);

                    if (CheckLock(device, deviceID))
                    {

                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        order.isSuccess = false;
                        return;
                    }
                    // Done
                    regOk++;
                    device.isBlocking = false;
                    device.blockCount = 0;

                    order.isVeriOk = true;
                    order.isSuccess = true;

                    if (addStatusCheckBox.Checked)
                    {
                        LogStatus(device, "Add status");
                        FbUtil.PostStatus(deviceID);
                    }

                    if (order.hasAvatar)
                    {
                        LogStatus(device, "Begin check Up avatar ");
                        uiXML = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "textthêmảnhresourceid", 1, uiXML)
                        || CheckTextExist(deviceID, Language.ChooseFromGallery(), 1, uiXML)
                            || CheckTextExist(deviceID, Language.ChooseAvatar(), 1, uiXML))
                        {
                            goto UPLOAD_AVATAR;
                        }

                        if (UploadAvatarProfile(deviceID, order, coverCheckBox.Checked) == -1)
                        {
                            order.hasAvatar = false;

                            goto STORE_INFO;
                        }
                    }
                    if (coverCheckBox.Checked)
                    {
                        LogStatus(device, " Start Add Cover");
                        FbUtil.AddSingleCover(deviceID);
                    }
                    if (addFriendCheckBox.Checked)
                    {
                        FbUtil.AddFriend(deviceID);
                    }
                    goto SET_2FA;
                }

            UPLOAD_AVATAR:

                if (holdingCheckBox.Checked || dataGridView.Rows[device.index].Cells[6].Value.ToString() != "True")
                {
                    device.running = false;
                    order.isSuccess = true;
                    goto STORE_INFO;
                }
                LogStatus(device, "UPLOAD_AVATAR");
                if (!string.IsNullOrEmpty(order.uid))
                {
                    uidd11dd1 = order.uid;
                }
                else
                {
                    uidd11dd1 = FbUtil.GetUid(deviceID);
                }
                if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                {
                    LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:DarkSlateGray");

                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSlateGray;
                    Thread.Sleep(1000);
                    order.isSuccess = false;
                    return;
                }

                order.isSuccess = true;
                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green3;
                if (!order.hasAvatar)
                {
                    goto SET_2FA;
                }
                if (CheckTextExist(deviceID, "xác nhận đó là bạn"))
                {
                    LogStatus(device, "Acc die captcha ----------------------");

                    accDieCaptcha++;
                    accDieCapchalabel.Text = accDieCaptcha + " accs Die captcha";
                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkCyan;
                    Thread.Sleep(60000);
                    order.isSuccess = false;
                    return;
                }
                Device.PushAvatar(deviceID, order);
                for (int i = 0; i < 13; i++)
                {
                    LogStatus(device, "round:" + (i + 1));
                    uiXML = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, "truycậpvịtrí", 1, uiXML))
                    {
                        LogStatus(device, "bỏ qua khi truy cập vị trí");
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                        Thread.Sleep(2000);

                    }
                    FindImageAndTap(deviceID, LUU_THONG_TIN_DANG_NHAP, 1);
                    if (Utility.CheckTextExist(deviceID, Language.Message(), 1, uiXML) && order.reupFullInfoAcc)
                    {
                        WaitAndTapXML(deviceID, 1, "thêm bạn bè");
                        break;
                    }
                    if (WaitAndTapXML(deviceID, 1, "tìmbạnbèresourceid", uiXML))
                    {
                        LogStatus(device, "Vào màn hình thêm 15 bạnh bè");
                        Thread.Sleep(2000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 90.1);
                        Thread.Sleep(1000);
                        for (int tt = 0; tt < 5; tt++)
                        {
                            WaitAndTapXML(deviceID, 1, "thêmbạnbèresourceid");
                        }
                        Device.Back(deviceID);
                    }
                    if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                    {
                        LogStatus(device, "Đang tải danh bạ lên - bật");
                        uiXML = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "nhập liên hệ", 1, uiXML)
                            || CheckTextExist(deviceID, "bỏ qua bước này", 1, uiXML))
                        {
                            if (!WaitAndTapXML(deviceID, 1, "nhập liên hệ resource"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 76.8, 63.5);

                            }
                            WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);
                        }
                        WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);

                        if (CheckTextExist(deviceID, "đồng bộ"))
                        {
                            for (int k = 0; k < WaitAddContactCount; k++)
                            {
                                if (!CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    Console.WriteLine("đồng bộ:" + k);
                                    break;
                                }
                            }
                        }
                        LogStatus(device, "Đồng bộ danh bạ xong - add 5 friend");
                        Thread.Sleep(5000);

                        if (FindImageAndTap(deviceID, THEM_5BB, 10))
                        {

                            Thread.Sleep(2000);
                            if (FindImageAndTap(deviceID, THEM_5BB, 3))
                            {
                                Thread.Sleep(7000);
                                if (FindImageAndTap(deviceID, THEM_5BB, 3))
                                {
                                    LogStatus(device, "Không thể thêm 5 bạn - bấm không chạy - > bỏ qua");
                                    WaitAndTapXML(deviceID, 1, "bỏ qua");
                                    WaitAndTapXML(deviceID, 3, "Thêm 5 bạn");
                                }
                                WaitAndTapXML(deviceID, 1, "Xong");

                                Thread.Sleep(5000);
                                if (WaitAndTapXML(deviceID, 1, "lưucheckable"))
                                {
                                    LogStatus(device, "Đã lưu acc mới reg -- thành công ---4910-----");
                                }
                                break;
                            }
                            else
                            {
                                if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                                {
                                    Device.TapByPercent(deviceID, 48.0, 96.8);
                                }
                                Device.TapByPercent(deviceID, 48.0, 96.8);
                                Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                                WaitAndTapXML(deviceID, 1, "Xong");
                                order.hasAddFriend = true;
                                LogStatus(device, "Đã thêm 5 friend bình thường");
                                Thread.Sleep(2000);
                                break;
                            }
                        }
                        else
                        {
                            if (WaitAndTapXML(deviceID, 1, "tiếp"))
                            {
                                Thread.Sleep(2000);
                            }
                        }
                    }
                    if (WaitAndTapXML(deviceID, 1, "lưucheckable"))
                    {
                        LogStatus(device, "Đã lưu acc mới reg -- thành công ------4939--");
                    }
                    if (WaitAndTapXML(deviceID, 2, "textthêmảnhresourceid", uiXML)
                        || WaitAndTapXML(deviceID, 1, "thêmảnhcheckablef", uiXML)) // Vào luồng up avatar cơ bản
                    {
                        Thread.Sleep(1000);
                        if (avatarByCameraCheckBox.Checked && WaitAndTapXML(deviceID, 1, "chụp ảnh"))
                        {
                            WaitAndTapXML(deviceID, 2, Language.AllowAll());
                            WaitAndTapXML(deviceID, 2, Language.AllowAll());
                            Thread.Sleep(2000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.9, 92.5); // chụp hình button
                            Thread.Sleep(4000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.0, 94.6); // ok 
                            WaitAndTapXML(deviceID, 2, Language.Save());

                            Thread.Sleep(15000);
                        }
                        if (!WaitAndTapXML(deviceID, 1, "chọntừthưviệncheckable"))
                        {
                            Device.TapByPercent(deviceID, 49.1, 55.8);
                        }

                        //WaitAndTapXML(deviceID, 1, "chọntừthưviện");

                        if (order.reupFullInfoAcc && order.account != null && order.account.hasAvatar)
                        {
                            if (!WaitAndTapXML(deviceID, 1, "bỏ qua"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.6, 7.4);
                            }
                        }


                        LogStatus(device, "Upload avatar normal -------------");
                        if (FbUtil.UploadAvatarNormal(deviceID, order))
                        {
                            //order.accType = Constant.ACC_TYPE_UP_AVATAR_NORMAL;
                        }

                        if (WaitAndTapXML(deviceID, 1, "Xong"))
                        {
                            LogStatus(device, "Bam giao dien moi  - xong");
                            Thread.Sleep(3000);
                        }
                        if (FindImageAndTap(deviceID, BAT_DANH_BA, 15))
                        {
                            LogStatus(device, "Đang tải danh bạ lên - bật - sau khi up avatar normal -- ");
                            uiXML = GetUIXml(deviceID);
                            if (CheckTextExist(deviceID, "nhập liên hệ", 1, uiXML)
                                || CheckTextExist(deviceID, "bỏ qua bước này", 1, uiXML))
                            {
                                if (!WaitAndTapXML(deviceID, 1, "nhập liên hệ resource"))
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 76.8, 63.5);

                                }
                                WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);
                            }
                            WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);

                            if (CheckTextExist(deviceID, "đồng bộ"))
                            {
                                for (int k = 0; k < WaitAddContactCount; k++)
                                {
                                    if (!CheckTextExist(deviceID, "đồng bộ"))
                                    {
                                        Console.WriteLine("đồng bộ:" + k);
                                        break;
                                    }
                                }
                            }
                            LogStatus(device, "Đồng bộ danh bạ xong - add 5 friend");
                            Thread.Sleep(10000);

                            if (FindImageAndTap(deviceID, THEM_5BB, 3))
                            {
                                Thread.Sleep(15000);
                                if (WaitAndTapXML(deviceID, 3, "Thêm 5 người bạn")
                                || WaitAndTapXML(deviceID, 3, "Thêm 5 bạn"))
                                {
                                    LogStatus(device, "Không thể thêm bạn bình thường , bỏ qua");
                                    Device.TapByPercent(deviceID, 48.0, 96.8); // bỏ qua

                                    WaitAndTapXML(deviceID, 3, "Thêm 5 bạn");
                                    Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                                }
                                else
                                {
                                    LogStatus(device, "Đã thêm 5 friend bình thường");
                                }


                                WaitAndTapXML(deviceID, 1, "Xong");
                                order.hasAddFriend = true;

                                Thread.Sleep(2000);
                                if (WaitAndTapXML(deviceID, 1, "lưucheckable"))
                                {
                                    LogStatus(device, "Đã lưu acc mới reg -- thành công ---5038-----");
                                }
                                break;
                            }
                            WaitAndTapXML(deviceID, 1, Language.Next());
                            uiXML = GetUIXml(deviceID);
                            goto THEM_BAN_BE;
                        }
                        FindImageAndTap(deviceID, THEM_5BB, 3);
                        goto THEM_BAN_BE;
                    }
                    if (WaitAndTapXML(deviceID, 1, "lưucheckable"))
                    {
                        LogStatus(device, "Đã lưu acc mới reg -- thành công --------");
                    }
                    if (avatarByCameraCheckBox.Checked && CheckTextExist(deviceID, "chụp ảnh"))
                    {
                        if (avatarByCameraCheckBox.Checked && WaitAndTapXML(deviceID, 1, "chụp ảnh"))
                        {
                            WaitAndTapXML(deviceID, 2, Language.AllowAll());
                            WaitAndTapXML(deviceID, 2, Language.AllowAll());
                            Thread.Sleep(2000);
                            Device.TapByPercent(deviceID, 46.9, 92.5, 4000); // chụp hình button
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.0, 94.6); // ok 
                            WaitAndTapXML(deviceID, 2, Language.Save());

                            Thread.Sleep(15000);
                        }
                    }
                    if (WaitAndTapXML(deviceID, 1, "textthêmảnhresourceid", uiXML)
                    || WaitAndTapXML(deviceID, 1, Language.ChooseFromGallery(), uiXML))
                    {
                        //Thread.Sleep(1000);
                        WaitAndTapXML(deviceID, 1, "chọntừthưviện");
                        Device.TapByPercent(deviceID, 49.1, 55.8);
                        WaitAndTapXML(deviceID, 1, Language.ChooseFromGallery());
                        if (order.reupFullInfoAcc && order.account != null && order.account.hasAvatar)
                        {
                            if (!WaitAndTapXML(deviceID, 1, "bỏ qua"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.6, 7.4);
                            }
                        }
                        FbUtil.UploadAvatarNormal(deviceID, order);
                        uiXML = GetUIXml(deviceID);
                        //WaitAndTapXML(deviceID, 1, "Thêm 5 bạn bè", uiXML);
                        FindImageAndTap(deviceID, THEM_5BB, 1);
                    }
                    WaitAndTapXML(deviceID, 1, "nhập liên hệ", uiXML);

                    if (WaitAndTapXML(deviceID, 1, "Xong"))
                    {
                        LogStatus(device, "Bam giao dien moi  - xong");
                        Thread.Sleep(3000);
                    }
                    if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                    {
                        LogStatus(device, "Đang tải danh bạ lên - bật - 2");
                        uiXML = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "nhập liên hệ", 1, uiXML)
                            || CheckTextExist(deviceID, "bỏ qua bước này", 1, uiXML))
                        {
                            if (!WaitAndTapXML(deviceID, 1, "nhập liên hệ resource"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 76.8, 63.5);

                            }
                            WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);
                        }
                        WaitAndTapXML(deviceID, 2, Language.AllowAll(), uiXML);

                        if (CheckTextExist(deviceID, "đồng bộ"))
                        {
                            for (int k = 0; k < WaitAddContactCount; k++)
                            {
                                if (!CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    Console.WriteLine("đồng bộ:" + k);
                                    break;
                                }
                            }
                        }
                        LogStatus(device, "Đồng bộ danh bạ xong - add 5 friend");

                        if (FindImageAndTap(deviceID, THEM_5BB, 1))
                        {
                            if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                            {
                                Device.TapByPercent(deviceID, 48.0, 96.8);
                            }
                            Device.TapByPercent(deviceID, 48.0, 96.8);
                            WaitAndTapXML(deviceID, 1, Language.YESresource());
                            if (WaitAndTapXML(deviceID, 1, "Xong"))
                            {
                                LogStatus(device, "Đã xong, vào màn hình chính -------------");
                            }
                        }
                        uiXML = GetUIXml(deviceID);
                        goto THEM_BAN_BE;
                    }

                    WaitAndTapXML(deviceID, 1, Language.AllowAll(), uiXML);
                    if (CheckTextExist(deviceID, "tìm bạn bè", 1, uiXML)
                        || CheckTextExist(deviceID, "tìm kiếm bạn bè", 1, uiXML))
                    {
                        Device.TapByPercent(deviceID, 48.0, 96.8, 300);
                        Device.TapByPercent(deviceID, 49.1, 55.8, 1000);
                    }
                    if (CheckTextExist(deviceID, "Lưu thông tin", 1, uiXML))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.0, 60.1);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                        Device.EnterPress(deviceID);
                        Thread.Sleep(1500);
                        if (CheckTextExist(deviceID, "truycậpvịtrí", 1)) Device.TapByPercent(deviceID, 53.7, 65.1, 2000);
                    }
                    if (CheckTextExist(deviceID, "nhập liên hệ", 1, uiXML)
                        || CheckTextExist(deviceID, "bỏ qua bước này", 1, uiXML))
                    {
                        if (!WaitAndTapXML(deviceID, 1, "nhập liên hệ resource")) Device.TapByPercent(deviceID, 76.8, 63.5);
                        WaitAndTapXML(deviceID, 2, Language.AllowAll());
                    }
                THEM_BAN_BE:
                    if (!order.hasAddFriend &&
                        CheckTextExist(deviceID, "thêm bạn bè", 1, uiXML))
                    {

                        LogStatus(device, "Invite 5 Friend");
                        if (CheckTextExist(deviceID, "đồng bộ", 1, uiXML))
                        {
                            for (int k = 0; k < WaitAddContactCount; k++)
                            {
                                if (!CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    Console.WriteLine("đồng bộ---------------------:" + k);
                                    break;
                                }
                                Thread.Sleep(2000);
                                LogStatus(device, "Đang đồng bộ danh bạ - lần: " + k);
                            }
                        }
                        if (CheckTextExist(deviceID, "Bạn bè", 2, uiXML))
                        {
                            if (CheckTextExist(deviceID, "tiếp", 1))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.6, 7.1);
                                Thread.Sleep(1000);
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.6, 7.1);
                                Thread.Sleep(1000);
                            }
                            uiXML = GetUIXml(deviceID);
                            //if (!WaitAndTapXML(deviceID, 1, "Thêm 5 người bạn", uiXML))
                            if (!FindImageAndTap(deviceID, THEM_5BB, 1))
                            {

                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);

                                Thread.Sleep(1000);
                                if (CheckTextExist(deviceID, "index1textthêmbạnbè", 1))
                                {
                                    // bỏ qua
                                    WaitAndTapXML(deviceID, 1, "descbỏquacheckable");
                                    break;
                                }
                            }
                            else
                            {
                                order.hasAddFriend = true;
                            }
                        }
                        else
                        {
                            LogStatus(device, "Upload avatar quá lâu-----");
                        }
                        WaitAndTapXML(deviceID, 1, Language.AllowAll());
                        if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                        {
                            LogStatus(device, "Invite All Fail, Tap");
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                        }
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                        Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                        WaitAndTapXML(deviceID, 1, "Xong");
                    }
                    if (WaitAndTapXML(deviceID, 5, Language.InviteAll(), uiXML))
                    {
                        LogStatus(device, "Invite All 222222222222222");
                        Thread.Sleep(500);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.0, 55.0);

                    }
                    if (CheckLock(device, deviceID, uiXML))
                    {

                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                        order.isSuccess = false;
                        return;
                    }
                    // Check "xác nhận"
                    if (CheckTextExist(deviceID, "messenger", 1, uiXML))
                    {
                        Device.Back(deviceID);
                    }
                    // Check home screen and break
                    if (Utility.CheckTextExist(deviceID, Language.Message()))
                    {
                        break;
                    }

                    if (order.hasAvatar && order.account != null && !order.account.hasAvatar)
                    {
                        if (verifyAccNvrCheckBox.Checked || reupFullCheckBox.Checked)
                        {
                            LogStatus(device, "Reup avatar -:");
                            if (CheckTextExist(deviceID, "truycậpvịtrí", 1))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                                Thread.Sleep(2000);

                            }

                            if (UploadAvatarProfile(deviceID, order, false) == -1)
                            {
                                order.hasAvatar = false;

                                goto STORE_INFO;
                            }
                        }
                        string ui = GetUIXml(deviceID);
                        if (order.reupFullInfoAcc && CheckTextExist(deviceID, "nhập liên hệ", 1, ui))
                        {
                            if (!WaitAndTapXML(deviceID, 1, "bỏ qua"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.1, 6.8);
                            }
                            goto UPLOAD_AVATAR;
                        }
                        // Check "xác nhận"
                        if (CheckTextExist(deviceID, "messenger", 1, ui))
                        {
                            Device.Back(deviceID);
                        }
                    }
                    if (WaitAndTapXML(deviceID, 1, "Xong"))
                    {
                        LogStatus(device, "Bam giao dien moi  - xong");
                        Thread.Sleep(3000);
                    }
                    regOk++;
                    device.isBlocking = false;
                    device.blockCount = 0;

                    order.isSuccess = true;

                    if (!Utility.CheckTextExist(deviceID, Language.Message()))
                    {
                        Device.TabPress(deviceID);
                        Thread.Sleep(500);
                        Device.EnterPress(deviceID);
                    }
                    if (addStatusCheckBox.Checked)
                    {
                        LogStatus(device, "Add status");
                        FbUtil.PostStatus(deviceID);
                    }

                    if (profileCheckBox.Checked)
                    {
                        LogStatus(device, "Add profile");
                        Thread.Sleep(1000);
                        FbUtil.AddProfile(deviceID);
                        Thread.Sleep(1000);
                    }
                    if (addFriendCheckBox.Checked)
                    {
                        LogStatus(device, "Add friend");
                        FbUtil.AddFriend(deviceID);
                    }

                    if (coverCheckBox.Checked)
                    {
                        LogStatus(device, "Add Cover");
                        FbUtil.AddSingleCover(deviceID);
                    }
                    if (!order.hasAddFriend
                        || (order.account != null && string.IsNullOrEmpty(order.account.qrCode)))
                    {
                        LogStatus(device, "Reupload contact 11111");
                        string uixxml = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "truycậpvịtrí", 1, uixxml))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                            Thread.Sleep(2000);
                            if (CheckTextExist(deviceID, "truycậpvịtrí", 1))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                                Thread.Sleep(2000);

                            }
                        }

                        if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                        {

                            if (CheckTextExist(deviceID, "đồng bộ"))
                            {
                                for (int k = 0; k < WaitAddContactCount; k++)
                                {
                                    if (!CheckTextExist(deviceID, "đồng bộ"))
                                    {
                                        Console.WriteLine("đồng bộ:" + k);
                                        break;
                                    }
                                }
                            }

                            if (FindImageAndTap(deviceID, THEM_5BB, 7))
                            {

                                WaitAndTapXML(deviceID, 1, Language.AllowAll());
                                if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                }
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                                WaitAndTapXML(deviceID, 1, "Xong");
                            }
                        }
                        if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                        {
                            LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:Honeydew");


                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Honeydew;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            return;
                        }
                        LogStatus(device, "UPload contact 2");
                        order.hasAddFriend = UploadContact2(device, order.numberOfFriendRequest);
                        if (!order.hasAddFriend && CheckTextExist(deviceID, "nhập mã xác nhận", 1))
                        {
                            LogStatus(device, "Veri fail mà không biết ----", 2000);
                            order.isSuccess = false;
                            Utility.storeAccWithThread(isServer, order, deviceID,
                                        password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                            order.isSuccess = false;
                            Thread.Sleep(30000);
                            return;

                        }
                        LogStatus(device, "Upload contact 2 done");
                    }
                    if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                    {
                        LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:Lavender");


                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Lavender;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }

                    FbUtil.BackToFbHome(deviceID);
                    goto SET_2FA;
                }

                // Check avatar
                Device.GotoFbProfileEdit(deviceID);

                Thread.Sleep(2000);
                if (CheckTextExist(deviceID, new[] {"nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,3",
                    "index0texteditresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepassword"})) // đã có avatar rồi
                {
                    LogStatus(device, "Đã up avatar thành công-----------");
                    order.checkAccHasAvatar = true;
                    Device.Back(deviceID);
                }


            SET_2FA:

                if (holdingCheckBox.Checked || dataGridView.Rows[device.index].Cells[6].Value.ToString() != "True")
                {
                    device.running = false;
                    order.isSuccess = true;
                    goto STORE_INFO;
                }
                if (CheckTextExist(deviceID, "xác nhận"))
                {

                    if (order.reupFullInfoAcc)
                    {
                        LogStatus(device, "Acc chưa veri- Lưu nvr --- , return color:Maroon");

                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Maroon;
                        Utility.storeAccWithThread(isServer, order, deviceID,
                                    password, "noveri|tempmail", device.androidId, order.gender, yearOld, Constant.FALSE, device.log);
                        Thread.Sleep(10000);
                        order.isSuccess = false;
                        return;
                    }
                    else
                    {
                  
                        order.currentMail = null;
                        goto ENTER_CODE_CONFIRM_EMAIL;
                    }

                }
                string uidddd;
                if (!string.IsNullOrEmpty(order.uid))
                {
                    uidddd = order.uid;
                }
                else
                {
                    uidddd = FbUtil.GetUid(deviceID);
                }
                if (FbUtil.CheckLiveWall(uidddd) == Constant.DIE)
                {
                    LogStatus(device, "checklivewall - Acc die --- , return color:Maroon");

                    fail++;
                    device.blockCount++;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Maroon;
                    Thread.Sleep(1000);
                    order.isSuccess = false;
                    return;
                }

                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green4;
                if (CheckTextExist(deviceID, Language.AllowAccessLocationDialog(), 1))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 65.6);
                    Thread.Sleep(1000);
                }

                if (order.hasAvatar && !order.checkAccHasAvatar)
                {
                    LogStatus(device, "Recheck - Avatar");

                    if (UploadAvatarProfile(deviceID, order, coverCheckBox.Checked) == -1)
                    {
                        order.hasAvatar = false;

                        goto STORE_INFO;
                    }
                    LogStatus(device, "Ket thuc Recheck - Avatar");
                }
                if (coverCheckBox.Checked)
                {
                    //LogStatus(device, "Start up cover ------------------");
                    //UploadCoverProfile(deviceID, order, true, true);
                    //LogStatus(device, "End up cover ------------------");
                }
                if (miniProfileCheckBox.Checked)
                {
                    ProfileMini(deviceID);
                }
                //UploadCoverProfile(deviceID, order, true);
                if (!order.isReverify && (forceReupContactCheckBox.Checked || !order.hasAddFriend))
                {
                    LogStatus(device, "FORCE - Reupload contact");
                    if (CheckTextExist(deviceID, "truycậpvịtrí", 1))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                        Thread.Sleep(2000);
                        if (CheckTextExist(deviceID, "truycậpvịtrí", 1))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                            Thread.Sleep(2000);

                        }
                    }
                    if (FbUtil.CheckLiveWall(order.uid) == Constant.DIE)
                    {
                        LogStatus(device, "checklivewall - Acc check live die -> FileCLone/Die_CheckLive color:DarkMagenta");

                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkMagenta;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }
                    bool isExist = false;

                    for (int i = 0; i < order.numberOfFriendRequest; i++)
                    {
                        if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè resource", "Addfriend", "friendcheckable" }))
                        {

                        }
                        else
                        {
                            break;
                        }
                        isExist = true;
                    }
                    order.hasAddFriend = isExist;
                    if (!order.hasAddFriend)
                    {
                        order.hasAddFriend = UploadContact2(device, order.numberOfFriendRequest);
                        if (!order.hasAddFriend && CheckTextExist(deviceID, "nhập mã xác nhận", 1))
                        {
                            LogStatus(device, "Veri fail mà không biết ----", 2000);
                            order.isSuccess = false;
                            Utility.storeAccWithThread(isServer, order, deviceID,
                                        password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                            fail++;
                            device.blockCount++;
                            device.isBlocking = true;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                            order.isSuccess = false;
                            Thread.Sleep(30000);
                            return;

                        }
                    }


                }

                if (order.has2Fa)
                {
                    LogStatus(device, "Bật 2fa");
                    device.status = "Set 2fa";
                    if (set2faWebCheckBox.Checked)
                    {
                        order.qrCode = Set2faWeb(deviceID, password);
                        if (CheckTextExist(deviceID, "textnhậpmậtkhẩuresourcei"))
                        {
                            LogStatus(device, "Nhâp mật khẩu set2fa web");
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.9, 23.9);
                            Thread.Sleep(1000);
                            InputText(deviceID, password, false);
                            Thread.Sleep(1000);
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.8, 30.0); // tiếp tục
                        }
                        //order.accType = Constant.ACC_TYPE_SET_2FA_WEB;
                    }
                    else
                    {
                        if (set2faLoai2checkBox.Checked)
                        {
                            //order.accType = Constant.ACC_TYPE_2FA_SECURITY_SETTING;
                            order.qrCode = Set2faSecuritySettings(order, device, password);
                            if (!string.IsNullOrEmpty(order.qrCode))
                            {
                                order.set2FaSuccess = true;
                            }
                        }
                        else
                        {

                            order.qrCode = Set2faSettings(order, device, password);
                            if (order.qrCode == "store")
                            {
                                order.qrCode = "";
                                order.set2FaSuccess = false;
                                goto STORE_INFO;
                            }
                            if (!string.IsNullOrEmpty(order.qrCode))
                            {
                                //order.accType = order.accType + "_" + Constant.ACC_TYPE_2FA_SETTING;
                                order.set2FaSuccess = true;
                            }
                        }
                    }
                }
                else
                {
                    order.isSuccess = true;
                    goto STORE_INFO;
                }

                if (WaitAndTapXML(deviceID, 2, "textmậtkhẩuresourceid"))
                {
                    if (micerCheckBox.Checked)
                    {
                        InputTextMicer(deviceID, password);
                    }
                    else
                    {
                        Utility.InputText(deviceID, password, false);
                    }
                    Thread.Sleep(1000);

                    if (!WaitAndTapXML(deviceID, 2, "text tiếp tục resource"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.8, 34.8);
                    }
                    Thread.Sleep(1500);
                    if (CheckTextExist(deviceID, "sai mật khẩu"))
                    {
                        LogStatus(device, "sai mật khẩu");

                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                        order.isSuccess = false;
                        return;
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        uiXML = GetUIXml(deviceID);
                        if (CheckTextExist(deviceID, "xác thực 2 yếu tố đang bật", 1, uiXML))
                        {
                            WaitAndTapXML(deviceID, 5, "xongcheckable", uiXML);
                            order.set2FaSuccess = true;
                            break;
                        }
                    }
                }
                if (!order.set2FaSuccess)
                {
                    uiXML = GetUIXml(deviceID);
                    if (!string.IsNullOrEmpty(order.qrCode)
                    && (
                    CheckTextExist(deviceID, "sao chép mã", 1, uiXML)
                    || CheckTextExist(deviceID, " đang bật", 1, uiXML)))
                    {
                        order.set2FaSuccess = true;
                    }
                }

                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green5;
                if (!order.set2FaSuccess)
                {
                    LogStatus(device, "Can not set 2fa -> Set 2fa again");

                    if (!string.IsNullOrEmpty(order.uid))
                    {
                        uidddd = order.uid;
                    }
                    else
                    {
                        uidddd = FbUtil.GetUid(deviceID);
                    }
                    if (FbUtil.CheckLiveWall(uidddd) == Constant.DIE)
                    {
                        LogStatus(device, "Can not set 2fa - checklivewall - Acc die --- , return color:RoyalBlue");

                        fail++;
                        device.blockCount++;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.RoyalBlue;
                        Thread.Sleep(1000);
                        order.isSuccess = false;
                        return;
                    }

                    if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                    {
                        if (CheckTextExist(deviceID, "đồng bộ"))
                        {
                            for (int k = 0; k < WaitAddContactCount; k++)
                            {
                                if (!CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    Console.WriteLine("đồng bộ:" + k);
                                    break;
                                }
                            }
                        }

                        if (FindImageAndTap(deviceID, THEM_5BB, 1))
                        {

                            WaitAndTapXML(deviceID, 1, Language.AllowAll());
                            if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                            }
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                            Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                            WaitAndTapXML(deviceID, 1, "Xong");
                        }
                    }
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Azure;
                    Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                    if (order.has2Fa && !order.set2FaSuccess)
                    {
                        device.status = "Set 2fa";
                        if (set2faLoai2checkBox.Checked)
                        {

                            order.qrCode = Set2faSettings(order, device, password);
                            if (!string.IsNullOrEmpty(order.qrCode))
                            {
                                //order.accType = order.accType + "_" + Constant.ACC_TYPE_2FA_SETTING;
                                order.set2FaSuccess = true;
                            }
                        }
                        else
                        {
                            order.qrCode = Set2faSecuritySettings(order, device, password);
                            if (!string.IsNullOrEmpty(order.qrCode))
                            {

                                order.set2FaSuccess = true;
                            }
                        }
                    }
                    if (WaitAndTapXML(deviceID, 2, "textmậtkhẩuresourceid"))
                    {
                        LogStatus(device, "Nhập lại password");

                        if (micerCheckBox.Checked)
                        {
                            InputTextMicer(deviceID, password);
                        }
                        else
                        {
                            Utility.InputText(deviceID, password, false);
                        }
                        Thread.Sleep(1000);

                        if (!WaitAndTapXML(deviceID, 2, "text tiếp tục resource"))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.8, 34.8);
                        }

                        Thread.Sleep(1500);
                        if (CheckTextExist(deviceID, "sai mật khẩu"))
                        {
                            LogStatus(device, "sai mật khẩu");
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                            order.isSuccess = false;
                            return;
                        }
                        for (int i = 0; i < 12; i++)
                        {
                            uiXML = GetUIXml(deviceID);
                            if (CheckTextExist(deviceID, "xác thực 2 yếu tố đang bật", 1, uiXML)
                                )
                            {
                                WaitAndTapXML(deviceID, 5, "xongcheckable", uiXML);
                                order.set2FaSuccess = true;
                                break;
                            }
                        }
                    }
                    Thread.Sleep(1000);
                    if (!order.set2FaSuccess)
                    {
                        uiXML = GetUIXml(deviceID);
                        if (!string.IsNullOrEmpty(order.qrCode)
                        && (
                        CheckTextExist(deviceID, "sao chép mã", 1, uiXML)
                        || CheckTextExist(deviceID, "đang bật", 1, uiXML)))
                        {
                            order.set2FaSuccess = true;
                        }
                    }
                    if (!order.set2FaSuccess)
                    {
                        LogStatus(device, "Can not set 2fa");
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Lavender;

                        if (!string.IsNullOrEmpty(order.uid))
                        {
                            uidddd = order.uid;
                        }
                        else
                        {
                            uidddd = FbUtil.GetUid(deviceID);
                        }
                        if (FbUtil.CheckLiveWall(uidddd) == Constant.DIE)
                        {

                            LogStatus(device, "Can not set 2fa - checklivewall - Acc die --- , return color:LightYellow");

                            fail++;
                            device.blockCount++;
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightYellow;
                            Thread.Sleep(1000);
                            order.isSuccess = false;
                            return;
                        }
                        if (!order.set2FaSuccess)
                        {
                            uiXML = GetUIXml(deviceID);
                            if (!string.IsNullOrEmpty(order.qrCode)
                            && (
                            CheckTextExist(deviceID, "sao chép mã", 1, uiXML)
                            || CheckTextExist(deviceID, " đang bật", 1, uiXML)))
                            {
                                order.set2FaSuccess = true;
                            }
                        }

                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green5;
                        if (!order.set2FaSuccess)
                        {
                            LogStatus(device, "Can not set 2fa -> Set 2fa lần 3 - set bằng web");

                            if (!string.IsNullOrEmpty(order.uid))
                            {
                                uidddd = order.uid;
                            }
                            else
                            {
                                uidddd = FbUtil.GetUid(deviceID);
                            }
                            if (FbUtil.CheckLiveWall(uidddd) == Constant.DIE)
                            {
                                LogStatus(device, "Can not set 2fa - checklivewall - Acc die --- , return color:RoyalBlue");

                                fail++;
                                device.blockCount++;
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.RoyalBlue;
                                Thread.Sleep(1000);
                                order.isSuccess = false;
                                return;
                            }

                            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                            {
                                if (CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    for (int k = 0; k < WaitAddContactCount; k++)
                                    {
                                        if (!CheckTextExist(deviceID, "đồng bộ"))
                                        {
                                            Console.WriteLine("đồng bộ:" + k);
                                            break;
                                        }
                                    }
                                }

                                if (FindImageAndTap(deviceID, THEM_5BB, 1))
                                {

                                    WaitAndTapXML(deviceID, 1, Language.AllowAll());
                                    if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                                    {
                                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                    }
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                                    Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                                    WaitAndTapXML(deviceID, 1, "Xong");
                                }
                            }
                            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Azure;
                            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                            if (order.has2Fa)
                            {
                                device.status = "Set 2fa";

                                order.qrCode = Set2faWeb(deviceID, password);
                                //order.accType = Constant.ACC_TYPE_SET_2FA_WEB;
                            }
                            if (WaitAndTapXML(deviceID, 2, "textmậtkhẩuresourceid"))
                            {
                                LogStatus(device, "Nhập lại password");

                                if (micerCheckBox.Checked)
                                {
                                    InputTextMicer(deviceID, password);
                                }
                                else
                                {
                                    Utility.InputText(deviceID, password, false);
                                }
                                Thread.Sleep(1000);

                                if (!WaitAndTapXML(deviceID, 2, "text tiếp tục resource"))
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.8, 34.8);
                                }

                                Thread.Sleep(1500);
                                if (CheckTextExist(deviceID, "sai mật khẩu"))
                                {
                                    LogStatus(device, "sai mật khẩu");
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Red;
                                    order.isSuccess = false;
                                    return;
                                }
                                for (int i = 0; i < 12; i++)
                                {
                                    uiXML = GetUIXml(deviceID);
                                    if (CheckTextExist(deviceID, "xác thực 2 yếu tố đang bật", 1, uiXML))
                                    {
                                        WaitAndTapXML(deviceID, 5, "xongcheckable", uiXML);
                                        order.set2FaSuccess = true;
                                        break;
                                    }
                                }
                            }
                            if (CheckTextExist(deviceID, "textnhậpmậtkhẩuresourcei"))
                            {
                                LogStatus(device, "Nhâp mật khẩu set2fa web");
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.9, 23.9);
                                Thread.Sleep(1000);
                                InputText(deviceID, password, false);
                                Thread.Sleep(1000);
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.8, 30.0); // tiếp tục
                            }
                            Thread.Sleep(1000);
                            if (!string.IsNullOrEmpty(order.qrCode))
                            {
                                Thread.Sleep(10000);
                            }
                            if (!order.set2FaSuccess)
                            {
                                uiXML = GetUIXml(deviceID);
                                if (!string.IsNullOrEmpty(order.qrCode)
                                && (
                                CheckTextExist(deviceID, "sao chép mã", 1, uiXML)
                                || CheckTextExist(deviceID, "đang bật", 1, uiXML)))
                                {
                                    order.set2FaSuccess = true;
                                }
                            }
                            else
                            {
                                //order.language = Constant.LANGUAGE_US;
                                //order.accType = Constant.ACC_TYPE_SET_2FA_WEB;
                            }
                            if (!order.set2FaSuccess)
                            {
                                LogStatus(device, "Can not set 2fa");
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Lavender;

                                if (!string.IsNullOrEmpty(order.uid))
                                {
                                    uidddd = order.uid;
                                }
                                else
                                {
                                    uidddd = FbUtil.GetUid(deviceID);
                                }
                                if (FbUtil.CheckLiveWall(uidddd) == Constant.DIE)
                                {

                                    LogStatus(device, "Can not set 2fa - checklivewall - Acc die --- , return color:LightYellow");

                                    fail++;
                                    device.blockCount++;
                                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.LightYellow;
                                    Thread.Sleep(1000);
                                    order.isSuccess = false;
                                    return;
                                }
                            }
                        }
                    }
                }
                order.isSuccess = true;
                LogStatus(device, "2fa:--" + order.qrCode);

            STORE_INFO:
                watchAll.Stop();
                if (!string.IsNullOrEmpty(order.oldType) && order.oldType.Contains("friend"))
                {
                    order.hasAddFriend = true;
                }
                if (order.doitenAcc)
                {

                    if (!order.account.hasAvatar)
                    {
                        UploadAvatarProfile(deviceID, order, false);

                    }
                    if (!string.IsNullOrEmpty(order.oldType) && order.oldType.Contains("friend"))
                    {
                        order.hasAddFriend = true;
                    } else
                    {
                        if (order.upContactNew)
                        {
                            RandomContact(deviceID);
                            order.hasAddFriend = UploadContact2(device, order.numberOfFriendRequest, true);
                        }
                        //if (chayepdanbacheckBox.Checked && !order.hasAddFriend)
                        //{
                        //    LogStatus(device, "Chạy lần đầu chưa có friend, random contact chạy lại");
                        //    RandomContact(deviceID);
                        //    order.hasAddFriend = UploadContact2(device, 4, true);
                        //}
                    }

                    if (!order.hasAddFriend && CheckTextExist(deviceID, "nhập mã xác nhận", 1))
                    {
                        LogStatus(device, "Veri fail mà không biết ----", 2000);
                        order.isSuccess = false;
                        Utility.storeAccWithThread(isServer, order, deviceID,
                                    password, "noveri|tempmail", "", order.gender, yearOld, Constant.FALSE, device.log);
                        fail++;
                        device.blockCount++;
                        device.isBlocking = true;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        order.isSuccess = false;
                        Thread.Sleep(30000);
                        return;

                    }
                }
                if (order.hasAvatar && !order.checkAccHasAvatar)
                {
                    LogStatus(device, "Recheck - Avatar trước khi lưu");
                    FbUtil.UploadAvatarProfile3(deviceID, order);
                }
                // Set active Duoimail
                if (!order.isHotmail)
                {
                    if (order.currentMail != null && !string.IsNullOrEmpty(order.currentMail.email) && order.tempmailType == Constant.TEMP_GENERATOR_EMAIL)
                    {
                        string[] tempdd = order.currentMail.email.Split('@');
                        activeDuoiMail = tempdd[1];
                        if (luuDuoiMailcheckBox.Checked) AddDuoiMailToServer(activeDuoiMail);

                        Properties.Settings.Default.activeDuoiMail = activeDuoiMail;
                        Properties.Settings.Default.Save();
                        activeDuoiMailtextBox.Invoke(new MethodInvoker(() =>
                        {
                            activeDuoiMailtextBox.Text = activeDuoiMail;
                        }));
                    }

                }


                bool checkStoreOK = storeAccWithThread(isServer, order, deviceID,
                    password, order.currentMail.toString(), order.qrCode, order.gender, yearOld, Constant.TRUE, device.log);
                LogStatus(device, "Stored information");

                if (!checkStoreOK)
                {
                    Text = Text + "------------------------------Kiểm tra kết nối server";
                    // Check server ip
                    string serverIp = ServerApi.GetServerIp(serverCacheMailTextbox.Text);
                    if (!string.IsNullOrEmpty(serverIp) && serverIp != serverPathTextBox.Text)
                    {
                        serverPathTextBox.Text = serverIp;
                    }
                    
                }
                else
                {
                    Text = Text.Replace("------------------------------Kiểm tra kết nối server", "");

                    LogStatus(device, "Clear acc cache in setting ----------");
                    FbUtil.ClearAccountFbInSetting(deviceID, clearAllAccSettingcheckBox.Checked);
                }
                if (addAccSettingCheckBox.Checked)
                {
                    Device.OpenSetting(deviceID);
                    Thread.Sleep(1000);
                    Device.Swipe(deviceID, 200, 2000, 200, 100);
                    Thread.Sleep(1000);
                    Utility.WaitAndTapXML(deviceID, 2, "tàikhoảnresource");
                    WaitAndTapXML(deviceID, 2, "thêm tài khoản");
                    WaitAndTapXML(deviceID, 2, "facebook");
                    Device.Home(deviceID);
                }
                if (reupRegCheckBox.Checked && order.reupFullInfoAcc)
                {
                    // open fblite
                    Device.OpenApp(deviceID, "com.facebook.lite");
                    Thread.Sleep(5000);
                    WaitAndTapXML(deviceID, 2, Language.AllowAll());
                    WaitAndTapXML(deviceID, 2, Language.AllowAll());

                    Device.OpenApp(deviceID, "com.facebook.orca");
                }
            }
            catch (Exception e)
            {
                LogStatus(device, e.Message);
                Thread.Sleep(10000);
                device.blockCount++;
                device.isBlocking = true;
                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
            }
        }

        public bool TurnOnUploadContactFb(DeviceObject device)
        {
            string deviceID = device.deviceId;
            LogStatus(device, "Bật tính năng đồng bộ danh bạ fb");
            Device.GotoFbSettings(deviceID);
            if (CheckTextExist(deviceID, "cài đặt", 10))
            {

                Device.Swipe(deviceID, 50, 1000, 60, 500);
                Device.Swipe(deviceID, 50, 1000, 60, 500);
                Device.Swipe(deviceID, 50, 1000, 60, 600);
                if (WaitAndTapXML(deviceID, 2, "tải danh bạ lên"))
                {
                    if (CheckTextExist(deviceID, "tắt", 2))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.0, 18.6);
                        if (WaitAndTapXML(deviceID, 3, "bắt đầu"))
                        {
                            for (int k = 0; k < WaitAddContactCount; k++)
                            {
                                if (!CheckTextExist(deviceID, "đồng bộ"))
                                {
                                    Console.WriteLine("đồng bộ:" + k);
                                    break;
                                }
                            }
                            Device.Back(deviceID);
                            Device.Back(deviceID);
                        }
                    }
                }
            }

            return true;
        }
        public void ReInstallFb(string deviceID, bool checkUpdate = true)
        {
            if (fbVersioncomboBox.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(fbVersioncomboBox.SelectedItem.ToString()))
                {
                    string path = "data\\fb\\" + fbVersioncomboBox.SelectedItem.ToString();
                    if (checkUpdate)
                    {
                        string currentVersion = Device.GetVersionFB(deviceID);

                        if (path.Contains(currentVersion.Replace("\\r", "")))
                        {
                            return;
                        }
                    }
                    if (File.Exists(path))
                    {
                        Device.Uninstall(deviceID, Constant.FACEBOOK_PACKAGE);

                        Thread.Sleep(1000);
                        Device.InstallApp(deviceID, path);
                    }
                }
            }
        }


        public void InstallFb449(DeviceObject device)
        {
            try
            {
                string[] fileNames = Directory.GetFiles("data\\449", "*.apk");
                if (fileNames != null && fileNames.Length > 0)
                {
                    string path = fileNames[0];
                    if (File.Exists(path))
                    {
                        Device.Uninstall(device.deviceId, Constant.FACEBOOK_PACKAGE);

                        Thread.Sleep(1000);
                        Device.InstallApp(device.deviceId, path);
                    }
                }
            }
            catch (Exception ex)
            {
                LogStatus(device, ex.Message);
            }

        }
        public void InstallLatestFb(DeviceObject device)
        {
            try
            {
                string[] fileNames = Directory.GetFiles("data\\fb", "*.apk");
                if (fileNames != null && fileNames.Length > 0)
                {
                    string path = fileNames[fileNames.Length - 1];
                    if (File.Exists(path))
                    {
                        Device.Uninstall(device.deviceId, Constant.FACEBOOK_PACKAGE);

                        Thread.Sleep(1000);
                        Device.InstallApp(device.deviceId, path);
                    }
                }
            }
            catch (Exception ex)
            {
                LogStatus(device, ex.Message);
            }

        }
        public void OverLockAction(DeviceObject device, OrderObject order)
        {

            string deviceID = device.deviceId;
            if (randomVersionSaudiecheckBox.Checked)
            {
                LogStatus(device, "Chuyển qua version khác ddddddddd-----------------");
                order.error_code = 101;
                device.randomVersionSauDie = true;
            }
            if (randomProxySim2checkBox.Checked)
            {
                LogStatus(device, "Reg fail nhiều quá, thay đổi cách chạy proxy: " + !order.proxyFromServer, 3000);
                order.proxyFromServer = !order.proxyFromServer;
                order.removeProxy = !order.proxyFromServer;
                if (order.proxyFromServer)
                {
                    device.loadNewProxy = false;
                }
                else
                {
                    device.keyProxy = "";
                }
            }

            if (randomMoicheckBox.Checked)
            {

                Random rr = new Random();
                int mm = rr.Next(1, 120);
                if (mm < 20)
                {
                    order.loginAccMoiLite = false;
                    order.loginAccMoiKatana = false;
                    order.loginAccMoiBusiness = false;
                    order.loginAccMoiMessenger = false;
                    LogStatus(device, "Chạy không mồi -------------");
                }
                else if (mm >= 20 && mm < 50)
                {
                    order.loginAccMoiLite = true;
                    order.loginAccMoiKatana = false;
                    order.loginAccMoiBusiness = false;
                    order.loginAccMoiMessenger = false;
                    LogStatus(device, "Chạy mồi Fbliteeeee -------------");
                }
                else if (mm >= 50 && mm < 80)
                {
                    order.loginAccMoiLite = false;
                    order.loginAccMoiKatana = true;
                    order.loginAccMoiBusiness = false;
                    order.loginAccMoiMessenger = false;
                    LogStatus(device, "Chạy mồi Katanaaaaaa -------------");
                }
                else if (mm >= 80 && mm < 120)
                {
                    order.loginAccMoiLite = false;
                    order.loginAccMoiKatana = false;
                    order.loginAccMoiBusiness = true;
                    order.loginAccMoiMessenger = false;
                    LogStatus(device, "Chạy mồi Businesssssssss -------------");
                }
            }


            if (reinstallSaudiecheckBox.Checked)
            {
                LogStatus(device, "Reinstall Facebook Sau khi die quá " + maxAccBlockRuntextBox.Text);
                ReInstallFb(deviceID);
                device.reInstallFb = 0;
            }
            else
            {
                if (openfblitecheckBox.Checked)
                {
                    FbUtil.ClearCacheFbLite(deviceID, clearAccSettingcheckBox.Checked);
                    Thread.Sleep(1000);
                    Device.ClearCache(deviceID, "com.facebook.orca");
                    Thread.Sleep(1000);
                    Device.OpenApp(deviceID, "com.facebook.lite");
                    Thread.Sleep(5000);
                    WaitAndTapXML(deviceID, 2, Language.AllowAll());
                    WaitAndTapXML(deviceID, 2, Language.AllowAll());
                }

            }
            device.clearCacheLite = true;

            if (randomMailPhoneSimCheckBox.Checked)
            {
                Random rann = new Random();
                int rr = rann.Next(0, 100);
                if (0 < rr && rr < 50)
                {
                    device.regByMail = !device.regByMail;
                    device.blockCount = 0;
                    LogStatus(device, "Block quá " + maxAccBlockRuntextBox.Text + "  lần, Change Mail/Phone");
                    return;
                }

            }
            else if (randomMailPhoneCheckBox.Checked)
            {
                device.regByMail = !device.regByMail;
                device.blockCount = 0;
                LogStatus(device, "Block quá " + maxAccBlockRuntextBox.Text + "  lần, Change Mail/Phone");
                return;
            }

            if (changeDeviceEmuCheckBox.Checked)
            {
                ChangeSimAction(device);
                LogStatus(device, "Block quá " + maxAccBlockRuntextBox.Text + "  lần, Change device emu");
            }
            if (!Device.CheckAppInstall(deviceID, "com.device.emulator.pro"))
            {
                Device.RebootDevice(device.deviceId);
            }
            else
            {
                if (changeSimCheckBox.Checked)
                {
                    string randomSim = RandomSim(device.network);
                    if (changeSimType2CheckBox.Checked)
                    {
                        randomSim = RandomSim2(device.network);
                    }
                    if (randomAllSimCheckBox.Checked)
                    {
                        Random rrr = new Random();
                        int nnn = rrr.Next(1, 100);
                        if (nnn > 50)
                        {
                            randomSim = RandomSim(device.network);
                        }
                        else
                        {
                            randomSim = RandomSim2(device.network);
                        }
                    }




                    string phone = "";

                    if (order.changePhoneNumber)
                    {
                        LogStatus(device, "Tìm số dt chưa có tk fb --------");
                        Device.ClearCache(deviceID, Constant.FACEBOOK_PACKAGE);
                        FbUtil.OpenFacebookApp2Login(device, false);
                        if (!WaitAndTapXML(deviceID, 2, "descbạnquênmậtkhẩuư\\?checkablefal"))
                        {

                        }

                        WaitAndTapXML(deviceID, 2, "từ chối");
                        WaitAndTapXML(deviceID, 2, "từ chối");

                        if (CheckTextExist(deviceID, "tìm tài khoản", 1))
                        {
                            for (int i = 0; i < 100; i++)
                            {
                                // Get phone from server
                                phone = ServerApi.GetPhones(true, randomSim);

                                InputText(deviceID, phone, true);

                                WaitAndTapXML(deviceID, 3, "tiếp tục");
                                Thread.Sleep(4000);
                                if (CheckTextExist(deviceID, new string[] { "tiếp tục", "thử cách khác" }))
                                //if (CheckTextExist(deviceID, "thử cách khác", 3))
                                {
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 7.1, 6.9); //
                                    Thread.Sleep(1000);
                                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.7, 25.1);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }


                    }
                    device.newSim = randomSim;
                    device.changeSim = true;
                    phone = phone.Replace("+84", "0");
                    device.devicePhone = phone;
                    string ddd = ChangeSimAction(device);
                    if (!string.IsNullOrEmpty(ddd))
                    {
                        dataGridView.Rows[device.index].Cells[14].Value = ddd;
                    }

                }
            }


            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Bisque;
            runningCheckBox.Checked = false;
            device.blockCount = 0;
        }


        public bool StopProxySuper(DeviceObject device)
        {
            Device.ForceStop(device.deviceId, "com.scheler.superproxy");
            //string deviceID = device.deviceId;
            //Device.OpenApp(deviceID, "com.scheler.superproxy");
            ////Thread.Sleep(2000);
            //WaitAndTapXML(deviceID, 3, "proxies&#10;tab1of3checkable");
            ////Thread.Sleep(1000);
            //if (!WaitAndTapXML(deviceID, 3, "stopcheckable"))
            //{
            //    LogStatus(device, "không thầy nút stoppppppp", 3000);
            //}
            //Device.Home(deviceID);
            return true;
        }
        public bool StartProxySuper(DeviceObject device)
        {
            string deviceID = device.deviceId;
            Device.OpenApp(deviceID, "com.scheler.superproxy");
            //Thread.Sleep(2000);
            WaitAndTapXML(deviceID, 3, "proxies&#10;tab1of3checkable");
            //Thread.Sleep(1000);
            if (!WaitAndTapXML(deviceID, 3, "startcheckable"))
            {
                LogStatus(device, "không thầy nút start", 3000);
            }
            Device.Home(deviceID);
            return true;
        }
        void Process(DeviceObject device)
        {
            SERVER_LOCAL = serverCacheMailTextbox.Text;
            localDate = DateTime.Now;

            string startTime = localDate.ToString(new CultureInfo("en-US"));
            if (device != null)
            {
                device.startTime = startTime;
            }
            string deviceID = device.deviceId;
            if (!Device.CheckAppInstall(deviceID, Constant.FACEBOOK_PACKAGE))
            {
                LogStatus(device, "Fb not install -> install it");
                if (!FbUtil.InstallMissingFb(deviceID))
                {
                    LogStatus(device, "Check file APK trong 'data/fb'------------");
                    Thread.Sleep(5000);
                }
            }
            if (!Device.CheckAppInstall(deviceID, Constant.FACEBOOK_LITE_PACKAGE))
            {
                LogStatus(device, "Fblite not install -> install it");
                if (!FbUtil.InstallMissingFblite(deviceID))
                {
                    LogStatus(device, "Check file APK trong 'data/fb'------------");
                    Thread.Sleep(5000);
                }
            }

            while (true)
            {
                try
                {

                    Device.AdbConnect(deviceID);
                    //FbUtil.AddSingleCover(deviceID);

                //ServerApi.GetBackupAcc("61562184406467");
                //ServerApi.Get1SecMail();
                // Testcode
                //Device.RebootDevice(deviceID);
                //AvatarObject dd = ServerApi.GetAvatarLocalCache(SERVER_LOCAL, Constant.FEMALE);
                //Mail.GetTrustMailVandong();
                //GetHotmail(device, serverCacheMailTextbox.Text, "", true, 5, otpVandongcheckBox.Checked);
                //ServerApi.Get1SecMail();
                //MailObject mmm = new MailObject();
                //mmm.email = "9cdmbac33rvj@dpptd.com";
                //ServerApi.GetAllSubject1SecMail(mmm);
                //try
                //{
                //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

                //    inMail.mailRepository = new MailRepository("imap-mail.outlook.com", 993, true, inMail.email, inMail.password); // Re login

                //    for (int i = 0; i < time; i++)
                //    {
                //        List<string> mmm = inMail.mailRepository.GetAllMailSubjects();
                //        ////truy xuất nội dung từng mail
                //        foreach (string mail in mmm)
                //        {
                //            Console.WriteLine("subject:" + mail);
                //            code = FindCodeInSubject(mail);
                //            if (code != Constant.FAIL)
                //            {
                //                break;
                //            }
                //        }

                //        if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                //        {
                //            break;
                //        }
                //        Thread.Sleep(5000);
                //        if (forceStopGetOtp && i > 3)
                //        {
                //            break;
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("Get code mail:" + inMail.email + "|" + inMail.password + " - " + ex.Message);
                //    return Constant.FAIL;
                //}
                //if (code != Constant.FAIL)
                //{
                //    return code;
                //}
               // MailObject mail = GetHotmail(device, serverCacheMailTextbox.Text,  Constant.OUTLOOK_DOMAIN, true, 5 , false); 
                BEGIN_:

                    if (proxyWificheckBox.Checked || (proxyFromServercheckBox.Checked && !proxy4GcheckBox.Checked))
                    {

                    }
                    else
                    {
                        Device.AirplaneOff(deviceID);
                        Device.EnableMobileData(deviceID);
                        Device.DisableWifi(deviceID);

                        //StopProxySuper(device);
                    }


                    string running = dataGridView.Rows[device.index].Cells[6].Value.ToString();
                    if (holdingCheckBox.Checked || running != "True" || device.deviceId.Length > 20)
                    {
                        if (device.running)
                        {
                            device.keyProxy = "";
                        }

                        device.running = false;
                        dataGridView.Rows[device.index].Cells[13].Value = "Đang tạm nghỉ";
                        Thread.Sleep(10 * 1000);
                        continue;
                    }
                    device.realSim = Device.GetRealSim(deviceID);
                    dataGridView.Rows[device.index].Cells[9].Value = device.realSim;

                    string ss = Device.GetIpSimProtocol(deviceID);
                    dataGridView.Rows[device.index].Cells[8].Value = ss;
                    if (proxyWificheckBox.Checked || (proxyFromServercheckBox.Checked && !proxy4GcheckBox.Checked))
                    {

                    }
                    else
                    {
                        if (ss == Constant.NO_INTERNET)
                        {
                            LogStatus(device, "Thử Chuyển qua ip4   fffffffffffffffff");
                            device.action = Constant.ACTION_CHANGE2IP4;
                            Change2Ip(device, device.action); // force change ip
                            Thread.Sleep(5000);
                            Device.RebootByCmd(deviceID);
                            Thread.Sleep(5000);
                            ss = Device.GetIpSimProtocol(deviceID);
                            dataGridView.Rows[device.index].Cells[8].Value = ss;
                            if (ss == Constant.NO_INTERNET)
                            {
                                LogStatus(device, "Vẫn không có mạng   fffffffffffffffff");
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkKhaki;
                                dataGridView.Rows[device.index].Cells[6].Value = false;

                                goto BEGIN_;
                            }
                        }
                    }


                    if (doitenVncheckBox.Checked && device.totalInHour > 4 && device.successInHour < 2)
                    {
                        dataGridView.Rows[device.index].Cells[6].Value = false;
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkKhaki;
                        device.blockCount = 0;
                        device.totalInHour = 0;
                        device.successInHour = 0;
                        LogStatus(device, "Đổi tên lỗi nhiều quá, tạm dừng kiểm tra lại --------", 5000);
                    }
                    if (proxyWificheckBox.Checked || (proxyFromServercheckBox.Checked && !proxy4GcheckBox.Checked))
                    {

                    }
                    else
                    {
                        FbUtil.ChangeIpByAirplane(device);
                    }



                    dataGridView.Rows[device.index].Cells[13].Value = "-Reg normal";
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    device.isProxyRuning = false;
                    LogStatus(device, "Check fb install and fblite install");
                    if (!Device.CheckAppInstall(deviceID, Constant.FACEBOOK_PACKAGE))
                    {
                        LogStatus(device, "Fb not install -> install it");
                        if (!FbUtil.InstallMissingFb(deviceID))
                        {
                            LogStatus(device, "Check file APK trong 'data/fb'------------");
                            Thread.Sleep(5000);
                        }
                    }

                    OrderObject order = InitialOrder(device);
                    //Device.PushAvatar(deviceID, order);
                    if (device.chuyenQuaMoiKatana)
                    {
                        order.loginAccMoiKatana = true;
                        order.loginAccMoiBusiness = false;
                        device.chuyenQuaMoiKatana = false;
                    }
                    if (device.chuyenQuaVeriGmail)
                    {
                        order.isHotmail = false;
                        device.chuyenQuaVeriGmail = false;
                    }
                    if (device.randomVersion || device.randomVersionSauDie)
                    {
                        string[] fileNames = Directory.GetFiles("data\\fb", "*.apk");
                        for (int i = 0; i < 10; i++)
                        {
                            string name = fileNames.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            if (name != device.fbVersion)
                            {
                                //string path = "data\\fb\\" + name;
                                LogStatus(device, "Install new version:" + name);
                                device.fbVersion = name;
                                Device.Uninstall(deviceID, Constant.FACEBOOK_PACKAGE);
                                Thread.Sleep(1000);
                                Device.InstallApp(deviceID, name);
                                Thread.Sleep(1000);
                                string fbVersion = Device.GetVersionFB(deviceID);
                                string fbLiteVersion = Device.GetVersionFBLite(deviceID);
                                string fbBusinessVersion = Device.GetVersionFBBusiness(deviceID);
                                string version = "fb:" + fbVersion + "-lite:" + fbLiteVersion + "-Business:" + fbBusinessVersion;
                                dataGridView.Rows[device.index].Cells[10].Value = version;
                                device.randomVersion = false;
                                device.randomVersionSauDie = false;
                                break;
                            }
                        }
                    }
                    LogStatus(device, "Đang gỡ proxy--------");

                    if (removeProxy2checkBox.Checked)
                    {
                        order.hasproxy = false;
                    }

                    if (Device.IsInstallApp(deviceID, "com.scheler.superproxy") && (device.fistTime || order.removeProxy))
                    {
                        if (order.removeProxy)
                        {
                            device.keyProxy = "";
                        }
                    }
                    if (device.fistTime)
                    {
                        LogStatus(device, "Chạy lần đầu tiên mở app");
                        
                        Device.PortraitRotate(deviceID);
                        device.showVersion = true;

                        CheckOnSim(device);
                    }
                    LogStatus(device, "Pre process");

                    running = dataGridView.Rows[device.index].Cells[6].Value.ToString();
                    if (holdingCheckBox.Checked || running != "True")
                    {
                        device.running = false;
                        dataGridView.Rows[device.index].Cells[13].Value = "Đang tạm nghỉ";
                        Thread.Sleep(10 * 1000);
                        continue;
                    }
                    Device.Home(deviceID);
                    order = PreProcess(device, order);


                    running = dataGridView.Rows[device.index].Cells[6].Value.ToString();
                    if (holdingCheckBox.Checked || running != "True")
                    {
                        device.running = false;
                        dataGridView.Rows[device.index].Cells[13].Value = "Đang tạm nghỉ";
                        Thread.Sleep(10 * 1000);
                        continue;
                    }
                    if (order == null || order.error_code == -1 || order.error_code == Constant.CAN_NOT_OPEN_FB_LITE_CODE)
                    {
                        continue;
                    }
                    if (order != null && order.error_code == Constant.CAN_NOT_GET_ACC_CODE)
                    {
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.DarkKhaki;
                        LogStatus(device, "Không thể lấy acc mồi - nghỉ 6s", 6000);
                        continue;
                    }
                    if (order != null && order.moiTruocProxy)
                    {
                        if (!StartProxy(order, device))
                        {
                            Device.DisableWifi(deviceID);
                            continue;
                        }
                        else
                        {
                            if (showIpcheckBox.Checked)
                            {
                                device.currentPublicIp = Device.GetPublicIpSmartProxy(deviceID);
                                dataGridView.Rows[device.index].Cells[8].Value = device.currentPublicIp;
                            }
                        }
                    }
                    Console.WriteLine("pre process:" + watch.ElapsedMilliseconds);
                    WaitAndTapXML(deviceID, new[] { "ok", "hủy" });


                    Autoclone(device, order);

                    if (order.hasproxy)
                    {
                        StopProxySuper(device);
                    }
                    if (device.fistTime)
                    {
                        showFbVersionCheckBox.Checked = false;
                    }

                    totalRun++;
                    device.fistTime = false;

                    LogStatus(device, "Đang gỡ proxy--------");

                    if (openMessengerCheckBox.Checked)
                    {
                        LogStatus(device, "Open messenger");
                        Device.ClearCache(deviceID, "com.facebook.orca");
                        Device.OpenApp(deviceID, "com.facebook.orca");
                        Thread.Sleep(5000);
                    }
                    LogStatus(device, "Sim status: " + device.simStatus);
                    if (!moiAccRegThanhCongcheckBox.Checked)
                    {
                        device.clearCache = true;
                    }
                    try
                    {
                        if (!order.isSuccess)
                        {
                            device.clearCache = true;
                            int maxFailOnOffSim = Convert.ToInt32(onOffSimCountTextBox.Text);
                            if (randomOnOffSimcheckBox.Checked && device.onOffSimCount >= maxFailOnOffSim)
                            {
                                device.onOffSimCount = 0; // reset
                                device.changeSim = true;
                                if (device.simStatus == Constant.TURN_ON_SIM)
                                {
                                    device.newSim = Constant.TURN_OFF_SIM;
                                    LogStatus(device, "Đang turn off sim - Chuyển sang Turn on sim");
                                    Thread.Sleep(2000);
                                }
                                else
                                {
                                    device.newSim = Constant.TURN_ON_SIM;
                                    LogStatus(device, "Đang turn on sim - Chuyển sang Turn off sim");
                                    Thread.Sleep(2000);
                                }
                            }
                            device.onOffSimCount++;
                        }
                        else
                        {
                            device.onOffSimCount = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogStatus(device, ex.Message);
                    }

                    if (order.isSuccess)
                    {
                        device.successInHour++;
                        device.globalSuccess++;
                        totalSucc++;
                        dataGridView.Rows[device.index].Cells[12].Value = "";
                        //device.clearCache = false;
                        device.clearCacheFailCount = 0;
                        device.clearCacheLiveCount++;
                        //WriteFileLog(device.currentIpInfo, "log_ip_success.txt");

                        device.veriNvrFailCount = 0;
                    }
                    else
                    {
                        if (order.isReverify)
                        {
                            device.veriNvrFailCount++;
                        }
                        device.clearCacheLiveCount = 0;
                        device.clearCacheFailCount++;
                        device.clearCache = true;
                        if (removeAccFblitecheckBox.Checked)
                        {
                            LogStatus(device, "Gỡ acc fblite");
                            Device.KillApp(deviceID, "com.facebook.lite");
                            Device.ClearCache(deviceID, "com.facebook.lite");

                            Thread.Sleep(1000);
                            Device.OpenApp(deviceID, "com.facebook.lite");

                            Thread.Sleep(15000);
                            WaitAndTapXML(deviceID, 10, Language.AllowAll());
                            if (!CheckImageExist(deviceID, FBLITE_DANG_NHAP_IMG))
                            {
                                // gỡ tài khoản
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.8, 16.3);
                                Thread.Sleep(1200);
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 31.2, 28.7);
                                Thread.Sleep(1200);
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 67.4, 54.8);
                                WaitAndTapXML(deviceID, 10, Language.AllowAll());
                                Thread.Sleep(1200);
                            }
                        }
                        int timeSleep = 60000;
                        try
                        {
                            timeSleep = Convert.ToInt32(delayAfterDieTextBox.Text) * 1000;
                        }
                        catch (Exception ex)
                        {
                            timeSleep = 60000;
                        }
                        if (timeSleep > 0)
                        {
                            LogStatus(device, "Nghỉ sau moi lan die----");
                            Thread.Sleep(timeSleep);
                            if (order.error_code == 101)
                            {

                            }
                            else
                            {
                                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                            }

                        }
                        else
                        {
                            LogStatus(device, "Thời gian nghỉ bị lỗi ");
                            Thread.Sleep(60000);
                        }
                        Thread.Sleep(timeSleep);
                    }

                    if (device.veriNvrFailCount >= Convert.ToInt32(maxAccBlockRuntextBox.Text))
                    {

                        if (randomVersionAfterverifailcheckBox.Checked)
                        {
                            LogStatus(device, "Veri fail quá nhiều - Random Version", 1000);
                            device.randomVersionSauDie = true;
                        } else
                        {
                            if (order.hasproxy)
                            {
                                Device.RebootByCmd(deviceID);
                                LogStatus(device, "Veri fail quá nhiều - Reboot devices", 1000);
                                LogRegStatus(dataGridView, device, "Veri fail quá nhiều - reboot devices");
                            } else
                            {
                                LogStatus(device, "Veri fail quá nhiều - tạm dừng", 1000);
                                LogRegStatus(dataGridView, device, "Veri fail quá nhiều - tạm dừng");
                                dataGridView.Rows[device.index].Cells[6].Value = false;
                            }
                        }

                        device.veriNvrFailCount = 0;
                    }
                    if (order.hasOtp)
                    {
                        this.otp++;

                    }
                    try
                    {
                        double percent = 100f * totalSucc / this.otp;
                        percent = Math.Round(percent, 1);
                        otplabel.Text = totalSucc + "/" + this.otp + "=" + percent;
                    }
                    catch (Exception ex)
                    {

                    }
                    // Delay after reg
                    int timeDelayAfterReg = 5;
                    try
                    {
                        timeDelayAfterReg = Convert.ToInt32(delayAfterRegTextBox.Text) * 1000;
                        Thread.Sleep(timeDelayAfterReg);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (order.isRun)
                    {
                        device.totalInHour++;
                        device.globalTotal++;
                        device.cycle++;
                    }
                    if (order.isVeriOk)
                    {
                        veriOk++;
                    }
                    try
                    {
                        double percent = 100f * totalSucc / otp;
                        percent = Math.Round(percent, 1);
                        otplabel.Text = totalSucc + "/" + otp + "=" + percent;
                    }
                    catch (Exception ex)
                    {

                    }
                    totalLabel.Invoke(new MethodInvoker(() =>
                    {
                        percent = 0;
                        try
                        {
                            if (totalSucc > 0)
                            {
                                percent = 100f * totalSucc / regNvrOk;
                                percent = Math.Round(percent, 1);
                            }

                        }
                        catch (Exception ex)
                        {
                            percent = 0;
                        }
                        totalLabel.Text = totalSucc + " / " + regNvrOk + " - " + percent;
                    }));
                    if (totalSucc % 100 == 0)
                    {
                        checkMail.Clear();
                    }
                    watch.Stop();
                    long second = watch.ElapsedMilliseconds / 1000;

                    reportPhoneLabel.Invoke(new MethodInvoker(() =>
                    {
                        reportPhoneLabel.Text = "Number phone CodeTextNow:" + numberOfPhoneCodeTextnow + " - otpmmo:" + numberOfPhoneOtp;
                    }));

                    Console.WriteLine($"Execution Time: {second} ms");

                    device.duration = Convert.ToInt32(second);

                    device.isFinish = true;
                }
                catch (Exception ex)
                {
                    LogStatus(device, "Exception All:" + ex.Message);
                }
            }
        }

        public void RemoveAllProxy(string deviceID)
        {
            Device.RemoveProxy(deviceID);
            if (Device.IsInstallApp(deviceID, "com.cell47.College_Proxy"))
            {
                FbUtil.RemoveProxyCollege(deviceID);
            }
            if (Device.IsInstallApp(deviceID, "org.proxydroid"))
            {
                FbUtil.RemoveProxyDroid(deviceID);
            }
            if (Device.IsInstallApp(deviceID, "net.typeblog.socks"))
            {
                FbUtil.RemoveProxySockDroid(deviceID);
            }
        }

        public bool UploadAvatarProfileReupInfo(string deviceID, OrderObject order, bool hasCover, bool needBacktohome = true)
        {
            Device.GotoFbProfileEdit(deviceID);
            string ui;
            for (int i = 0; i < 15; i++)
            {
                ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, new[] {"nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,3",
                    "index0texteditresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepassword"}, ui))
                {
                    order.checkAccHasAvatar = true;
                    Device.Back(deviceID);
                    Thread.Sleep(1000);
                    return true;
                }
                if (WaitAndTapXML(deviceID, 2, "nút ảnh đại diện checkable", ui))
                {
                    break;
                }
            }
            if (!FindImageAndTap(deviceID, CHO_PHEP_TRUY_CAP, 1))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.5, 73.0);
            }

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.1, 61.2); // tap cho phep

            ui = GetUIXml(deviceID);
            if (CheckTextExist(deviceID, "xác nhận", 1, ui))
            {
                return false;
            }
            if (CheckTextExist(deviceID, "messenger", 1, ui))
            {
                return false;
            }

            Device.PushAvatar(deviceID, order);

            if (!Utility.WaitAndTapXML(deviceID, 1, Language.AllowAll()))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 58.0);
            }
            Thread.Sleep(1000);
            if (CheckTextExist(deviceID, "bạnkhôngcóbấtkỳảnhhoặcvideo", 2))
            {
                if (CheckTextExist(deviceID, "hủy"))
                {
                    FbUtil.UploadAvatarProfile3(deviceID, order);
                }
                else
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.2, 7.3);
                    WaitAndTapXML(deviceID, 2, Language.AllowAll());
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.3, 66.9);
                    Thread.Sleep(2000);
                    Device.Back(deviceID);
                    Thread.Sleep(1500);
                }
            }

            if (FindImage(deviceID, THU_VIEN_ANH, 1))
            {
                Device.TapByPercent(deviceID, 12.9, 23.8, 1000); //  choose avatar image

                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                {
                    Device.TapByPercent(deviceID, 52.9, 23.8, 1000); //  choose avatar image

                    if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
                    }
                }
            }
            else
            {
                Device.TapByPercent(deviceID, 52.9, 23.8, 1000); //  choose avatar image

                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
                }
            }

            for (int i = 0; i < 15; i++)
            {
                ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, new[] {"nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,3",
                    "index0texteditresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepassword"})) // đã có avatar rồi
                {
                    order.checkAccHasAvatar = true;
                    Device.Back(deviceID);
                    Thread.Sleep(1000);
                    return true;
                }
            }

            if (hasCover)
            {
                FbUtil.AddSingleCover(deviceID);
            }

            if (needBacktohome)
            {
                FbUtil.BackToFbHome(deviceID);
            }

            return true;
        }

        public int UploadAvatarProfile(string deviceID, OrderObject order, bool hasCover, bool needBacktohome = true)
        {
            if (order.checkAccHasAvatar)
            {
                return 1;
            }
            if (!order.hasAvatar)
            {
                return -1;
            }

            string ui = "";

            Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);

            Device.GotoFbProfileEdit(deviceID);
            if (order.account != null && !string.IsNullOrEmpty(order.account.uid) && FbUtil.CheckLiveWall(order.account.uid) == Constant.DIE)
            {

                fail++;

                order.isSuccess = false;

                return -1;
            }

            Device.PushAvatar(deviceID, order);


            bool checkUpAvatar = false;
            for (int i = 0; i < 6; i++)
            {
                if (FindImageAndTap(deviceID, CHECK_AVATAR_PROFILE, 1))
                {
                    checkUpAvatar = true;
                    break;
                }

                ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "tryagain", 1, ui))
                {
                    return -1;
                }
                if (CheckTextExist(deviceID, "create account", 1, ui)
                    && CheckTextExist(deviceID, "next", 1, ui))
                {
                    Device.GotoFbProfileEdit(deviceID);
                    Thread.Sleep(5000);
                    break;
                }
                if (WaitAndTapXML(deviceID,
                    new[] {
                    "chỉnhsửaresourceid",
                    "chỉnhsửacheckable"}, ui)) // đã có avatar rồi
                {
                    order.checkAccHasAvatar = true;

                    Device.Back(deviceID);
                    Thread.Sleep(1000);
                    return 1;
                }
                if (WaitAndTapXML(deviceID, 2, "nút ảnh đại diện checkable", ui))
                {
                    checkUpAvatar = true;
                    break;
                }
            }
            if (!checkUpAvatar && !FindImageAndTap(deviceID, CHECK_AVATAR_PROFILE, 1))
            {
                Device.GotoFbProfileEdit(deviceID);
                Thread.Sleep(3000);
            }
            if (!checkUpAvatar && !FindImageAndTap(deviceID, CHECK_AVATAR_PROFILE, 1))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 27.4);
            }
        UP_AVATAR:

            //if (!WaitAndTapXML(deviceID, 2, "allowaccesscheckable"))
            //{
            if (!FindImageAndTap(deviceID, CHO_PHEP_TRUY_CAP, 3))
            {
                if (WaitAndTapXML(deviceID, 1, "thêmảnhcheckable"))
                {
                    FbUtil.UploadAvatarNormal(deviceID, order);
                    return 1;
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 48.7);
                Thread.Sleep(1000);
                if (!CheckTextExist(deviceID, "cho phép", 2))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 73.6);
                }
            }
            

            if (!WaitAndTapXML(deviceID, 2, "chophépresourceid"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 73.6);
            }
            if (!WaitAndTapXML(deviceID, 2, "chophépresourceid"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.1, 61.2); // tap cho phep
            }


            if (FindImage(deviceID, THU_VIEN_ANH, 2) || CheckTextExist(deviceID, "gầnđây", 1))
            {
                Device.TapByPercent(deviceID, 12.9, 23.8, 1000); //  choose avatar image

                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
                    Thread.Sleep(1000);
                    if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
                    }
                }
            }
            else
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
                Thread.Sleep(1000);
                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
                }
            }

            for (int i = 0; i < 15; i++)
            {
                ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, new string[] {"chỉnhsửaresourceid", "chỉnhsửacheckable"}, ui)) // đã có avatar rồi
                {
                    order.checkAccHasAvatar = true;

                    break;
                }
            }


            if (order.upCoverNew)
            {
                Device.PushCover(deviceID);
                // Tap Anh bìa
                if (WaitAndTapXML(deviceID, 1, "thêmảnhbìacheckable"))
                {
                    if (FindImage(deviceID, THU_VIEN_ANH, 2) || CheckTextExist(deviceID, "gầnđây", 1))
                    {
                        Device.TapByPercent(deviceID, 12.9, 23.8, 1000); //  choose avatar image

                        if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
                            Thread.Sleep(1000);
                            if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
                            }
                        }
                    }
                    else
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
                        Thread.Sleep(1000);
                        if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
                        }
                    }
                }
                for (int i = 0; i < 15; i++)
                {
                    ui = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, new[] { "chỉnhsửaảnhbìa" }, ui)) // đã có avatar rồi
                    {
                        break;
                    }
                }
            }
            
            //if (needBacktohome)
            //{
            //    FbUtil.BackToFbHome(deviceID);
            //}

            return 1;
        }

        public bool UploadCoverProfile(string deviceID, OrderObject order, bool hasCover, bool needBacktohome = true)
        {
            //            if (order.checkAccHasCover)
            //            {
            //                return true;
            //            }
            //            Device.PushCoverAvatar(deviceID, order);

            //            string ui = "";
            //            if (!WaitAndTapXML(deviceID, 1, "thêmảnhbìacheckable")) // đã có cover rồi
            //            {
            //                if (FindAndTap(deviceID, BAT_DANH_BA, 1))
            //                {

            //                    if (CheckTextExist(deviceID, "đồng bộ"))
            //                    {
            //                        for (int k = 0; k < WaitAddContactCount; k++)
            //                        {
            //                            if (!CheckTextExist(deviceID, "đồng bộ"))
            //                            {
            //                                Console.WriteLine("đồng bộ:" + k);
            //                                break;
            //                            }
            //                        }
            //                    }

            //                    //if (WaitAndTapXML(deviceID, 1, "Thêm 5 người bạn"))
            //                    if (FindAndTap(deviceID, THEM_5BB, 1))
            //                    {

            //                        WaitAndTapXML(deviceID, 1, Language.AllowAll());
            //                        if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
            //                        {
            //                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
            //                        }
            //                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
            //                        Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
            //                        WaitAndTapXML(deviceID, 1, "Xong");
            //                    }
            //                }
            //                if (WaitAndTapXML(deviceID, 1, "lưucheckable"))
            //                {
            //                    //LogStatus(device, "Đã lưu acc mới reg -- thành công --------");
            //                }
            //                Device.GotoFbProfileEdit(deviceID);
            //                Thread.Sleep(2000);
            //            }
            //            else
            //            {
            //                goto UP_AVATAR;
            //            }


            //            for (int i = 0; i < 15; i++)
            //            {
            //                ui = GetUIXml(deviceID);
            //                if (CheckTextExist(deviceID, "nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,10", 1, ui)
            //                    || CheckTextExist(deviceID, "nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,1962", 1, ui)) // đã có avatar rồi
            //                {
            //                    order.checkAccHasCover = true;
            //                    Device.Back(deviceID);
            //                    Thread.Sleep(1000);
            //                    return true;
            //                }

            //                if (WaitAndTapXML(deviceID, 2, "thêmảnhbìacheckable", ui))
            //                {
            //                    break;
            //                }

            //                if (i == 14)
            //                {
            //                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.7, 91.0);
            //                }
            //            }
            //UP_AVATAR:
            //            Thread.Sleep(2000);
            //            if (CheckTextExist(deviceID, "xác nhận", 1, ui))
            //            {
            //                return false;
            //            }
            //            if (FindImage(deviceID, THU_VIEN_ANH, 1))
            //            {
            //                goto UP_COVER;
            //            }
            //                if (!FindAndTap(deviceID, CHO_PHEP_TRUY_CAP, 3))
            //            {
            //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 48.7);
            //                Thread.Sleep(1000);
            //                if (!CheckTextExist(deviceID, "cho phép", 2))
            //                {
            //                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 73.6);
            //                }
            //            }

            //            if (!CheckTextExist(deviceID, "cho phép", 2))
            //            {
            //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 73.6);
            //            }
            //            Thread.Sleep(1000);
            //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.1, 61.2); // tap cho phep

            //            if (!WaitAndTapXML(deviceID, 3, Language.AllowAll(), ui))
            //            {
            //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 58.0);
            //            }
            //            if (!WaitAndTapXML(deviceID, 3, Language.AllowAll(), ui))
            //            {
            //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 58.0);
            //            }
            //            ui = GetUIXml(deviceID);
            //            if (CheckTextExist(deviceID, "textthêmảnhresourceid", 1, ui)
            //                 || WaitAndTapXML(deviceID, 1, Language.ChooseFromGallery(), ui))
            //            {
            //                if (order.reupFullInfoAcc && order.account != null && order.account.hasAvatar)
            //                {
            //                    if (!WaitAndTapXML(deviceID, 1, "bỏ qua"))
            //                    {
            //                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.6, 7.4);
            //                    }

            //                }
            //                FbUtil.UploadAvatarNormal(deviceID);
            //                return true;
            //            }

            //UP_COVER:
            //            if (FindImage(deviceID, THU_VIEN_ANH, 1))
            //            {
            //                Device.TapByPercent(deviceID, 12.9, 23.8, 1000); //  choose avatar image

            //                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
            //                {
            //                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
            //                    Thread.Sleep(1000);
            //                    if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
            //                    {
            //                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
            //                Thread.Sleep(1000);
            //                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
            //                {
            //                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
            //                }
            //            }

            //            for (int i = 0; i < 7; i++)
            //            {
            //                ui = GetUIXml(deviceID);
            //                if (CheckTextExist(deviceID, "nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,10", 1, ui)
            //                        || CheckTextExist(deviceID, "nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,1962", 1, ui)) // đã có avatar rồi
            //                {
            //                    order.checkAccHasCover = true;
            //                    if (miniProfileCheckBox.Checked)
            //                    {
            //                        ProfileMini(deviceID);
            //                    }

            //                    if (descriptionCheckBox.Checked)
            //                    {
            //                        MoTaBanThan(deviceID);
            //                    }


            //                    Device.Back(deviceID);
            //                    Thread.Sleep(1000);
            //                    return true;
            //                }
            //            }
            //            if (miniProfileCheckBox.Checked)
            //            {
            //                ProfileMini(deviceID);
            //            }
            //            if (descriptionCheckBox.Checked)
            //            {
            //                MoTaBanThan(deviceID);
            //            }


            //            if (needBacktohome)
            //            {
            //                FbUtil.BackToFbHome(deviceID);
            //            }

            return true;
        }

        public static void MoTaBanThan(string deviceID)
        {
            Device.GotoFbProfileEdit(deviceID);
            Thread.Sleep(2000);
            if (!WaitAndTapXML(deviceID, 1, "môtảbảnthânresourceid"))
            {
                Device.Swipe(deviceID, 222, 1000, 333, 700);
                WaitAndTapXML(deviceID, 1, "môtảbảnthânresourceid");
            }
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 36.9, 26.0);
            Random rn = new Random();
            string sss = Constant.statusOfFb.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            InputVietVNIText(deviceID, sss);

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.1, 7.5);

        }
        public static void ProfileMini(string deviceID)
        {
            Device.GotoFbProfileEdit(deviceID);
            Thread.Sleep(2000);
            if (!WaitAndTapXML(deviceID, 1, "tỉnhthànhphốhiệntạicheckable"))
            {
                Device.Swipe(deviceID, 222, 1300, 333, 700);
                WaitAndTapXML(deviceID, 1, "tỉnhthànhphốhiệntạicheckable");
            }

            Thread.Sleep(2500);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 39.4, 83.4);
            WaitAndTapXML(deviceID, 2, "tỉnhthànhphốhiệntạicheckable");
            Thread.Sleep(2500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 29.6, 14.5);
            Thread.Sleep(1500);
            Random rn = new Random();
            string sss = Constant.provices.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            InputVietVNIText(deviceID, sss);

            Thread.Sleep(1500);

            int ran = rn.Next(21, 50);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.2, ran);
            Thread.Sleep(1000);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.1, 96.6); // lưu
            Thread.Sleep(5000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.1, 96.6); // share
            WaitAndTapXML(deviceID, 1, "share");
            WaitAndTapXML(deviceID, 1, "chia sẻ");
            Thread.Sleep(2000);

            Device.Swipe(deviceID, 22, 1000, 55, 500);

            Thread.Sleep(1000);
            if (!WaitAndTapXML(deviceID, 1, "descthêmquêquáncheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 57.6, 70.5);

            }
            Thread.Sleep(3000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 37.1, 13.7);
            Thread.Sleep(3000);
            sss = Constant.provices.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            InputVietVNIText(deviceID, sss);

            Thread.Sleep(1500);

            ran = rn.Next(21, 50);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.2, ran);
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.1, 96.6); // lưu
            Thread.Sleep(5000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.1, 96.6); // share
            WaitAndTapXML(deviceID, 1, "share");
            WaitAndTapXML(deviceID, 1, "chia sẻ");
            Thread.Sleep(3000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.1, 96.6); // lưu
        }
        public void UploadAvatarProfile2(string deviceID, OrderObject order, bool hasCover, bool uploadContact)
        {
            Device.PushAvatar(deviceID, order); // Reveri acc 
            if (order.checkAccHasAvatar)
            {
                return;
            }

            Device.GotoFbProfileEdit(deviceID);
            for (int i = 0; i < 10; i++)
            {
                string ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, new[] {"nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,3",
                    "index0texteditresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepassword"}, ui))
                {
                    order.checkAccHasAvatar = true;
                    return;
                }
                if (WaitAndTapXML(deviceID, 1, "nút ảnh đại diện checkable", ui))
                {
                    break;
                }
                if (CheckTextExist(deviceID, "Đã xảy ra lỗi", 1, ui))
                {
                    return;
                }
            }

            if (!FindImageAndTap(deviceID, CHO_PHEP_TRUY_CAP, 1))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.5, 73.0);
            }

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.1, 61.2); // tap cho phep

            if (!Utility.WaitAndTapXML(deviceID, 2, Language.AllowAll()))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 58.0);
            }

            if (FindImage(deviceID, THU_VIEN_ANH, 1))
            {
                Device.TapByPercent(deviceID, 12.9, 23.8, 1000); //  choose avatar image

                if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.5, 8.0);
                }
            }
            else
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 23.8); //  choose avatar image
            }

            Thread.Sleep(1000);
            if (!Utility.WaitAndTapXML(deviceID, 3, Language.Save()))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 8.0);
            }

            for (int i = 0; i < 15; i++)
            {
                if (CheckTextExist(deviceID, new[] {"nodeindex0textchỉnhsửaresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepasswordfalseselectedfalseboundsxxx1115,3",
                    "index0texteditresourceidclassandroidwidgettextviewpackagecomfacebookkatanacontentdesccheckablefalsecheckedfalseclickablefalseenabledtruefocusablefalsefocusedfalsescrollablefalselongclickablefalsepassword"})) // đã có avatar rồi
                {
                    order.checkAccHasAvatar = true;
                    Device.Back(deviceID);
                    Thread.Sleep(1000);
                    return;
                }
            }
            if (!uploadContact)
            {
                Thread.Sleep(5000);
            }
            if (hasCover)
            {
                FbUtil.AddSingleCover(deviceID);
            }
        }

        public MailObject GetHotmail(DeviceObject device, string server, string hotmailType, bool hotmailKieumoi, int time, bool otpVandong)
        {
            MailObject mail = new MailObject();

            for (int i = 0; i < time; i++) // retry get mail 10 times// hotmail die
            {
                mail = Mail.GetHotmail(server, hotmailType, hotmailKieumoi);

                LogStatus(device, "Số mail lấy: " + (i + 1) + " mail:" + mail.email + " pass: " + mail.password + "|" + mail.source, 2000);

                mail.otpVandong = otpVandong;
                mail = CheckLiveHotmailByOAuth2(mail);
                if (mail != null && mail.status != Constant.FAIL)
                {
                    return mail;
                }
                //else
                //{
                //    LogStatus(device, "Check mail lần 222222 -------------", 30000);
                //    mail = CheckLiveHotmailByOAuth2(mail);
                //    if (mail != null && mail.status != Constant.FAIL)
                //    {
                //        return mail;
                //    }
                //    else
                //    {
                //        LogStatus(device, "Check mail lần 333333 -------------", 30000);
                //        mail = CheckLiveHotmailByOAuth2(mail);
                //        if (mail != null && mail.status != Constant.FAIL)
                //        {
                //            return mail;
                //        }
                //        else
                //        {
                //            mail.otpVandong = !otpVandong;
                //            mail = CheckLiveHotmailByOAuth2(mail);
                //            if (mail != null && mail.status != Constant.FAIL)
                //            {
                //                return mail;
                //            }
                //        }
                //    }
                //}
            }

            return mail;
        }
        public bool UploadContact2(DeviceObject device, int numberOfFriendRequest, bool force = false)
        {
            bool result = false;
            string deviceID = device.deviceId;
            Device.GotoFbFriendRequests(deviceID);
            Thread.Sleep(1500);
            //if (CheckTextExist(deviceID, new[] { "tryagain", "connect" }))
            //{
            //    return false;
            //}
            //if (CheckImageExist(deviceID, KHONG_CO_LOI_MOI_NAO))
            //{
            //    return false;
            //}


            string uiXML = GetUIXml(deviceID);
            if (FindImageAndTap(deviceID, TAI_DANH_BA_LEN, 1))
            {
                goto UPLOAD_CONTACT_2;
            }
            if (force || CheckTextExist(deviceID, new string[] { "tảilêndanhbạmới", "tảidanhbạ", "uploadcontacts" }))
            {
                goto UPLOAD_CONTACTS;
            }


            if (CheckTextExist(deviceID, new string[] { "khôngcólờimờimới", "dataset", "nhữngngườigửichobạnlờimờikếtbạnsẽxuấthiệnởđây" }, uiXML))
            {
                return false;
            }

            if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, uiXML))
            {
                return false;
            }
            if (CheckTextExist(deviceID, "tìm bạn bè", 1, uiXML)
                || CheckTextExist(deviceID, "nhập liên hệ", 1, uiXML)
                            || CheckTextExist(deviceID, "bỏ qua", 1, uiXML))
            {
                if (!WaitAndTapXML(deviceID, 1, "bỏquacheckable"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 7.5);
                }

                WaitAndTapXML(deviceID, 2, "bắt đầu");

                WaitAndTapXML(deviceID, 2, Language.AllowAll());
                Thread.Sleep(5000);
            }


            bool isExit = false;

            string uix = "";
            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè", "thêmbạnbèresourceid", "thêm bạn bè resource", "Addfriend", "friendcheckable" }))
                {
                    result = true;
                }
                else
                {
                    break;
                }
                isExit = true;
            }
            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXMLUnsign(deviceID, 1, "xácnhậnlờimờikếtbạn"))
                {
                    result = true;
                }
                else
                {
                    break;
                }
                isExit = true;
            }
            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXML(deviceID, 1, "làmbạnbè"))
                {
                    result = true;
                }
                else
                {
                    break;
                }
                isExit = true;
            }


            if (isExit)
            {
                return true;
            }

        UPLOAD_CONTACTS:
            if (!WaitAndTapXML(deviceID, 1, "tảidanhbạlêncheckable"))
            {

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.5, 68.6); // tap tải danh bạ lên - không check uixml được
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.1, 59.5);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.6, 68.6);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.3, 73.2);
                uiXML = GetUIXml(deviceID);
            } else
            {
                Thread.Sleep(2000);
            }


        UPLOAD_CONTACT_2:
            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
            {
                LogStatus(device, "Đang tải danh bạ lên - bật");
                WaitAndTapXML(deviceID, 2, Language.AllowAll());

                if (CheckTextExist(deviceID, "đồng bộ"))
                {
                    for (int k = 0; k < WaitAddContactCount; k++)
                    {
                        if (!CheckTextExist(deviceID, "đồng bộ"))
                        {
                            Console.WriteLine("đồng bộ:" + k);
                            break;
                        }
                    }
                }
            }
            if (!WaitAndTapXML(deviceID, 3, Language.Begin(), uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.3, 93.8); // Tap 'bat dau'
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.0, 92.4);
            }
            WaitAndTapXML(deviceID, 2, Language.AllowAll());

            for (int i = 0; i < 15; i++)
            {
                if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè", "thêmbạnbèresourceid", "thêm bạn bè resource", "Addfriend", "friendcheckable" }))
                {
                    result = true;
                    break;
                }
            }


            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè", "thêmbạnbèresourceid", "thêm bạn bè resource", "Addfriend", "friendcheckable" }))
                {
                    result = true;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
        public bool UploadContact(DeviceObject device, int numberOfFriendRequest)
        {
            string deviceID = device.deviceId;
            bool result = false;
            Device.GotoFbFriendRequests(deviceID);
            string uiXML = GetUIXml(deviceID);
            if (CheckImageExist(deviceID, KHONG_CO_LOI_MOI_NAO))
            {
                return false;
            }
            if (CheckTextExist(deviceID, Language.AllowAccessLocationDialog(), 1, uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.9, 65.6);
                Thread.Sleep(1000);
            }
            WaitAndTapXML(deviceID, 1, "từ chối", uiXML);

            if (!CheckTextExist(deviceID, "bạn bè", 10))
            {
                return false;
            }
            if (CheckTextExist(deviceID, "tìm thấy"))
            {
                FbUtil.BackToFbHome(deviceID);
                Device.Swipe(deviceID, 400, 1500, 500, 300);
                WaitAndTapXML(deviceID, 2, "tìm bạn bè");
            }
            if (CheckTextExist(deviceID, "dataset", 1, uiXML))
            {
                return false;
            }
            bool isExit = false;

            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè resource", "Addfriend", "friendcheckable" }))
                {
                    result = true;
                }
                else
                {
                    break;
                }
                isExit = true;
            }
            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXMLUnsign(deviceID, 1, "xácnhậnlờimờikếtbạn"))
                {
                    result = true;
                }
                else
                {
                    break;
                }
                isExit = true;
            }
            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXML(deviceID, 1, "làmbạnbè"))
                {
                    result = true;
                }
                else
                {
                    break;
                }
                isExit = true;
            }

            if (isExit)
            {
                return true;
            }
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.5, 68.6); // tap tải danh bạ lên - không check uixml được
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.1, 59.5);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.6, 68.6);
            uiXML = GetUIXml(deviceID);
            if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
            {
                LogStatus(device, "Đang tải danh bạ lên - bật");
                WaitAndTapXML(deviceID, 2, Language.AllowAll());

                if (CheckTextExist(deviceID, "đồng bộ"))
                {
                    for (int k = 0; k < WaitAddContactCount; k++)
                    {
                        if (!CheckTextExist(deviceID, "đồng bộ"))
                        {
                            Console.WriteLine("đồng bộ:" + k);
                            break;
                        }
                    }
                }
            }


            if (!WaitAndTapXML(deviceID, 3, Language.Begin(), uiXML))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.3, 93.8); // Tap 'bat dau'
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.0, 92.4);
            }
            WaitAndTapXML(deviceID, 2, Language.AllowAll());
            for (int i = 0; i < 15; i++)
            {
                if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè resource", "Addfriend", "friendcheckable" }))
                {
                    result = true;
                    break;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 0; i < numberOfFriendRequest; i++)
            {
                if (WaitAndTapXML(deviceID, new[] { "thêm bạn bè resource", "Addfriend" }))
                {
                    result = true;
                }
                else
                {
                    break;
                }

            }
            return result;
        }

        public bool ScreenNameSuggestion(string deviceID)
        {
            if (Utility.CheckTextExist(deviceID, Language.SelectYourName()))
            {
                Utility.Log("select your name after create", status);
                Utility.WaitAndTapXML(deviceID, 2, "checkabletrue");
                Next(deviceID);
                Thread.Sleep(1000);
            }
            return true;
        }
        public string FlowMobile(OrderObject order, DeviceObject device, string gender, string password, int yearOld, int delay
            , string selectedDeviceName)
        {
            string deviceID = device.deviceId;
            string phone = "";
            Thread.Sleep(delay);
            string[] ll = Utility.GetCordText(deviceID, "edittext");
            if (ll == null)
            {
                fail++;
                return Constant.FAIL;
            }
            Device.TapDelay(deviceID, Convert.ToInt32(ll[2]) - 10, Convert.ToInt32(ll[3]) - 10);

            if (device.regByMail || order.veriDirectHotmail)
            {
                Utility.WaitAndTapXML(deviceID, 3, "SignUpWithemail");
                Thread.Sleep(1000);
                MailObject mail = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language);
                if (order.veriDirectHotmail)
                {
                    mail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);

                    order.currentMail = mail;
                }
                LogStatus(device, "Reg by Mail:" + mail.email);
                Utility.InputVietVNIText(deviceID, mail.email);
            }
            else
            {
                if (order.veriDirectByPhone)
                {
                    if (!IsDigitsOnly(order.phoneT.phone))
                    {
                        LogStatus(device, "Can not get phone:" + order.phoneT.message);
                        order.veriDirectByPhone = false;
                        order.phoneT.phone = GeneratePhonePrefix(prefixTextNow);
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;

                        Thread.Sleep(5000);
                        return "";
                    }
                    phone = "+1" + order.phoneT.phone;
                }

                else if (americaPhoneCheckBox.Checked)
                {
                    phone = "+1" + GeneratePhoneAmerica();
                }
                else if (prefixTextNowCheckBox.Checked)
                {
                    phone = "+1" + GeneratePhonePrefix(prefixTextNow);
                }
                else
                {

                    if (randomPhoneCheckBox.Checked)
                    {
                        phone = Utility.GeneratePhoneNumber(isServer, dausotextbox.Text,
                            randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked,
                            usPhoneCheckBox.Checked, "");
                    }
                    else
                    {
                        phone = Utility.GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked, device.network);
                    }
                    LogStatus(device, "Reg by Phone server---:" + phone);
                    Thread.Sleep(1000);

                }

                device.currentMobileReg = phone;
                Utility.InputVietVNIText(deviceID, Utility.ConvertToUnsign(phone));
                LogStatus(device, "Reg by Phone---:" + phone);
            }

            Thread.Sleep(delay);
            Next(deviceID);

            Thread.Sleep(3000);

            Next(deviceID);

            Utility.WaitAndTapXML(deviceID, 2, "edittext");
            Utility.InputText(deviceID, password, inputStringCheckbox.Checked);

            Thread.Sleep(1000 + delay);
            Next(deviceID);
            string name = InputName(device, order, deviceID, gender);
            LogStatus(device, "Name:" + name);
            if (name == Constant.FAIL)
            {
                fail++;
                return Constant.FAIL;
            }

            Utility.Log("Input Birthday", status);
            Thread.Sleep(delay);

            if (!InputBirthday(deviceID, selectedDeviceName, yearOld))
            {
                fail++;
                return Constant.FAIL;
            }

            Utility.Log("Input gender", status);
            InputGender(device, gender);

            Next(deviceID);
            return name + "|" + phone;
        }

        public string FlowNormalFbLiteLD(OrderObject order, string deviceID, string gender, string password, int yearOld, int delay,
            string selectedDeviceName)
        {
            string name = InputNameLiteLD(order, deviceID, gender);
            if (name == Constant.FAIL)
            {
                fail++;
                return Constant.FAIL;
            }
            Device.TapByPercentDelay(deviceID, 75.5, 59.1, 15000); // Allow phone

            Thread.Sleep(1000);
            Device.TapByPercentDelay(deviceID, 50.0, 52.0); // sign up with email
            MailObject email = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language);
            Utility.InputUsText(deviceID, email.email, inputStringCheckbox.Checked);
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.5, 42.3);// Next
            Thread.Sleep(1000);


            Utility.Log("Input Birthday", status);
            Thread.Sleep(delay);

            if (!InputBirthdayFbLiteLD(deviceID))
            {
                fail++;
                return Constant.FAIL;
            }

            Utility.Log("Input gender", status);
            InputGenderFbLiteLD(deviceID, gender);

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 24.3, 33.4);
            Thread.Sleep(1000);
            Utility.InputUsText(deviceID, password, inputStringCheckbox.Checked); // Password

            Thread.Sleep(3000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.3, 40.7); // Next
            Thread.Sleep(200);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.3, 40.7); // Next
            return name;
        }

        public string FlowNormalFbLite(OrderObject order, string deviceID, string gender, string password, int yearOld, int delay,
            string selectedDeviceName)
        {
            string name = InputNameFbLite(order, deviceID, gender);
            if (name == Constant.FAIL)
            {
                fail++;
                return Constant.FAIL;
            }
            Utility.WaitAndTapXML(deviceID, 3, "Allowresource");
            Thread.Sleep(1000);
            Utility.WaitAndTapXML(deviceID, 3, "Allowresource");
            Thread.Sleep(3000);

            string phone = Utility.GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked);
            if (randomPhoneCheckBox.Checked)
            {
                phone = Utility.GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked);
            }
            Utility.InputVietVNIText(deviceID, Utility.ConvertToUnsign(phone));
            Thread.Sleep(1000);
            Device.TapByPercentDelay(deviceID, 47.6, 35.0);
            Thread.Sleep(1000);


            Utility.Log("Input Birthday", status);
            Thread.Sleep(delay);

            if (!InputBirthday(deviceID, selectedDeviceName, yearOld))
            {
                fail++;
                return Constant.FAIL;
            }


            Utility.Log("Input gender", status);
            InputGenderFbLite(deviceID, gender);


            Utility.InputText(deviceID, password, inputStringCheckbox.Checked); // Password

            Thread.Sleep(5000);
            Device.TapByPercentDelay(deviceID, 50.0, 35.6); // Next
            return name;
        }


        public string FlowNormal(OrderObject order, DeviceObject device, string gender, string password, int yearOld, int delay,
            string selectedDeviceName)
        {
            string deviceID = device.deviceId;
            string name = InputName(device, order, deviceID, gender);
            LogStatus(device, "Name:" + name);
            string phone = "";
            if (name == Constant.FAIL)
            {
                fail++;
                return Constant.FAIL;
            }

            Thread.Sleep(delay);

            if (!InputBirthday(deviceID, selectedDeviceName, yearOld))
            {
                fail++;
                return Constant.FAIL;
            }
            InputGender(device, gender);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.3, 69.5);

            Thread.Sleep(delay);

            if (device.regByMail || order.veriDirectHotmail || order.veriDirectGmail)
            {

                Utility.WaitAndTapXML(deviceID, 3, Language.SignUpWithemail());
                Thread.Sleep(1000);
                MailObject mail = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language); // random mail
                if (order.veriDirectHotmail)
                {
                    mail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);

                    order.currentMail = mail;
                }
                else
                {
                    if (forceVeriAccRegBMailcheckBox.Checked && veriaccgmailCheckBox.Checked)
                    {
                        order.veriAcc = true;
                    }
                }
                if (Constant.ACC_TYPE_FIX_PASSWORD != order.accType)
                {
                    order.accType = Constant.ACC_TYPE_REG_BY_MAIL;
                }

                LogStatus(device, "Reg by Mail:" + mail.email);
                Utility.InputUsText(deviceID, mail.email, true);
                LogRegStatus(dataGridView, device, "Reg by mail");
                LogStatus(device, "Bấm next --", 1000);
                Next(deviceID);
            }
            else //////////////////  Reg by phone
            {
                //LogStatus(device, "Log mail giả");
                //Utility.WaitAndTapXML(deviceID, 3, Language.SignUpWithemail());
                //Thread.Sleep(1000);
                //MailObject mail = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language); // random mail


                //LogStatus(device, "Reg by Mail:" + mail.email);
                //Utility.InputVietVNIText(deviceID, mail.email);

                //Next(deviceID);


                //Device.Back(deviceID);
                //Device.Back(deviceID);

                //Utility.WaitAndTapXML(deviceID, 3, "số di động");
                //Thread.Sleep(1000);
                LogRegStatus(dataGridView, device, "Reg by phone");
                Device.TapDelay(deviceID, 1360, 959);
                if (order.carryCodePhone)
                {
                    phone = GeneratePhoneCarryCode();
                }
                else if (order.dauso12)
                {
                    phone = GeneratePhone12Prefix(dauso12TextBox.Text.Split(','));
                }
                else if (order.dauso)
                {
                    phone = GeneratePhonePrefix(dausotextbox.Text.Split(','));
                }
                else if (order.veriDirectByPhone)
                {
                    if (!IsDigitsOnly(order.phoneT.phone))
                    {
                        LogStatus(device, "Can not get phone:" + order.phoneT.message);
                        order.veriDirectByPhone = false;
                        order.phoneT.phone = GeneratePhonePrefix(prefixTextNow);
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;

                        Thread.Sleep(5000);
                        return "";
                    }
                    phone = "+1" + order.phoneT.phone;
                }
                else if (order.americaPhone)
                {
                    phone = GeneratePhoneAmerica();
                }
                else if (order.prefixTextnow)
                {
                    phone = "+1" + GeneratePhonePrefix(prefixTextNow);
                }
                else
                {
                    if (randomPhoneCheckBox.Checked)
                    {
                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked);
                    }
                    else
                    {
                        var listNetwork = new ArrayList();
                        if (vinaphoneCheckbox.Checked)
                        {
                            listNetwork.Add(Constant.VINAPHONE);
                        }
                        if (viettelCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETTEL);
                        }
                        if (mobiphoneCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.MOBI);
                        }
                        if (vietnamMobileCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETNAM_MOBILE);
                        }

                        string network = "";
                        if (listNetwork != null && listNetwork.Count > 0)
                        {
                            network = (string)listNetwork.ToArray().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                        }

                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked, network);

                    }

                    string check = checkNetwork(phone);
                    LogStatus(device, "Phone server:" + phone + "-" + check);

                    Thread.Sleep(1000);
                }
                if (!order.veriDirectByPhone)
                {
                    order.phoneT.phone = phone;
                    LogRegStatus(dataGridView, device, phone);
                }
                if (!order.veriDirectByPhone)
                {
                    order.phoneT.phone = phone;
                    LogRegStatus(dataGridView, device, phone);
                }
                if (!string.IsNullOrEmpty(order.regPhone))
                {
                    phone = order.regPhone;
                }
                LogStatus(device, "Reg by Phone flow normal:" + phone);
                device.log = phone;
                //phone = phone.Replace("+84", "0");
                InputText(deviceID, Utility.ConvertToUnsign(phone), inputStringMailCheckBox.Checked);

                Next(deviceID);

                string ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "Mật khẩu", 1, ui))
                {
                    goto INPUT_PASSWORD;
                }

                if (CheckTextExist(deviceID, "khôngthểxácthựcsốdiđộng", 1, ui)
                    || CheckTextExist(deviceID, "already in use", 1, ui)
                    || CheckTextExist(deviceID, "gần đây", 1, ui))
                {
                    LogStatus(device, "Phone đã được sử dụng");
                    Thread.Sleep(5000);
                    return "";
                }
            }


        INPUT_PASSWORD:



            if (micerCheckBox.Checked)
            {
                InputTextMicer(deviceID, password);
            }
            else
            {
                InputMail(deviceID, password, false);
            }
            Console.WriteLine("Pass:" + password);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 44.2);

            return name + "|" + phone;
        }

        public string FlowNormalNewUS(OrderObject order, DeviceObject device, string gender, string password, int yearOld, int delay,
            string selectedDeviceName)
        {
            string deviceID = device.deviceId;
            string name = InputNameNewUS(device, order, deviceID, gender);
            LogStatus(device, "Name:" + name);
            string phone = "";
            if (name == Constant.FAIL)
            {
                fail++;
                LogStatus(device, "Nhập tên bị sai", 5000);
                return Constant.FAIL;
            }

            //Thread.Sleep(delay);

            if (!InputBirthdayNewUS(deviceID))
            {
                //fail++;
                LogStatus(device, "Nhập ngày sinh bị sai", 1000);
                //return Constant.FAIL;
            }
            if (!InputGenderNewUS(device, gender))
            {
                LogStatus(device, "Nhập giới tính sai", 1000);
                fail++;
                return Constant.FAIL;
            }



            //Thread.Sleep(2000);
            //WaitAndTapXML(deviceID, 3, "chophépresourceid");
            WaitAndTapXML(deviceID, 3, "từchốiresourceid");
            if (device.regByMail || order.veriDirectHotmail || order.veriDirectGmail)
            {
                if (!CheckTextExist(deviceID, "email của bạn là gì", 3))
                {
                    Utility.WaitAndTapXML(deviceID, 3, "Bằng email");
                    Thread.Sleep(3000);
                }

                Thread.Sleep(1000);
                MailObject mail = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language); // random mail
                if (order.veriDirectHotmail)
                {
                    mail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);

                    order.currentMail = mail;
                }
                else
                {
                    if (forceVeriAccRegBMailcheckBox.Checked && veriaccgmailCheckBox.Checked)
                    {
                        order.veriAcc = true;
                    }
                }
                if (Constant.ACC_TYPE_FIX_PASSWORD != order.accType)
                {
                    order.accType = Constant.ACC_TYPE_REG_BY_MAIL;
                }
                if (!WaitAndTapXML(deviceID, 1, "descemailcheckablefalsechecked"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.7, 31.5);
                }
                LogStatus(device, "Reg by Mail:" + mail.email, 1000);
                Utility.InputUsText(deviceID, mail.email, inputStringMailCheckBox.Checked);
                LogRegStatus(dataGridView, device, "Reg by mail");
                //Next(deviceID);
            }
            else //////////////////  Reg by phone
            {
                bool checkScreen = false;
                for (int i = 0; i < 6; i++)
                {
                    if (CheckTextExist(deviceID, new string[] {"your mobile number", "số di động của bạn là gì", "số di động hoặc email" }))
                    {
                        checkScreen = true;
                        break;
                    }
                }
                if (!checkScreen)
                {
                    LogStatus(device, "Không thấy màn hình nhập số điện thoại", 1000);
                    if (CheckTextExist(deviceID, "email của bạn là gì", 2))
                    {
                        Device.Back(deviceID);
                        if (WaitAndTapXML(deviceID, 3, "đăngkýbằngsốdiđộngcheckable"))
                        {
                            LogStatus(device, "Màn hình đăng ký bằng email", 2000);
                        }
                        else
                        {
                            Device.Back(deviceID);
                            if (!WaitAndTapXML(deviceID, 3, "đăngkýbằngsốdiđộngcheckable"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.4, 56.2);
                            }
                        }
                    }
                }

                LogRegStatus(dataGridView, device, "Reg by phone");
                //Device.TapDelay(deviceID, 1360, 959);
                if (order.carryCodePhone)
                {
                    phone = GeneratePhoneCarryCode();
                }
                else if (order.dauso12)
                {
                    phone = GeneratePhone12Prefix(dauso12TextBox.Text.Split(','));
                }
                else if (order.dauso)
                {
                    phone = GeneratePhonePrefix(dausotextbox.Text.Split(','));
                }
                else if (order.veriDirectByPhone)
                {
                    if (!IsDigitsOnly(order.phoneT.phone))
                    {
                        LogStatus(device, "Can not get phone:" + order.phoneT.message);
                        order.veriDirectByPhone = false;
                        order.phoneT.phone = GeneratePhonePrefix(prefixTextNow);
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;

                        Thread.Sleep(5000);
                        return "";
                    }
                    phone = "+1" + order.phoneT.phone;
                }
                else if (order.americaPhone)
                {
                    phone = GeneratePhoneAmerica();
                }
                else if (order.prefixTextnow)
                {
                    phone = "+1" + GeneratePhonePrefix(prefixTextNow);
                }
                else
                {
                    if (randomPhoneCheckBox.Checked)
                    {
                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked);
                    }
                    else
                    {
                        var listNetwork = new ArrayList();
                        if (vinaphoneCheckbox.Checked)
                        {
                            listNetwork.Add(Constant.VINAPHONE);
                        }
                        if (viettelCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETTEL);
                        }
                        if (mobiphoneCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.MOBI);
                        }
                        if (vietnamMobileCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETNAM_MOBILE);
                        }

                        string network = "";
                        if (listNetwork != null && listNetwork.Count > 0)
                        {
                            network = (string)listNetwork.ToArray().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                        }

                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked, network);

                    }

                    string check = checkNetwork(phone);
                    LogStatus(device, "Phone server:" + phone + "-" + check);

                    //Thread.Sleep(1000);
                }
                if (!order.veriDirectByPhone)
                {
                    order.phoneT.phone = phone;
                    LogRegStatus(dataGridView, device, phone);
                }

                if (!string.IsNullOrEmpty(order.regPhone))
                {
                    phone = order.regPhone;
                }
                LogStatus(device, "Reg by Phone flow normal:" + phone);
                device.log = phone;
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 87.9, 31.8); // tap x xóa phone cũ
                //Thread.Sleep(500);
                Device.MoveEndTextbox(deviceID);
                Device.DeleteChars(deviceID, 12);
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                //phone = phone.Replace("+84", "0");
                InputText(deviceID, Utility.ConvertToUnsign(phone), inputStringMailCheckBox.Checked);
            }
            // Device.TapByPercent(deviceID, 89.0, 95.7); // Hạ bàn phím
            Thread.Sleep(1000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.6, 47.9);// tiếp
            if (!WaitAndTapXML(deviceID, 2, "nextcheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.6, 47.9);// tiếp
            }

            for (int i = 0; i < 4; i++)
            {
                string xxxml = GetUIXml(deviceID);
                if (WaitAndTapXML(deviceID, 1, "continue creating account", xxxml))
                {
                    Thread.Sleep(1000);
                    break;
                }
            }

            CheckTextExist(deviceID, "password", 3);
            if (micerCheckBox.Checked)
            {
                InputTextMicer(deviceID, password);
            }
            else
            {
                InputMail(deviceID, password, false);
            }
            Console.WriteLine("Pass:" + password);

            if (!WaitAndTapXML(deviceID, 2, "nextcheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.7, 38.9); // tiếp
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 79.6, 50.9); // tiếp

                WaitAndTapXML(deviceID, 15, "tiếp tục dùng tiếng anh mỹ");
                WaitAndTapXML(deviceID, 12, "lưucheckable");
                if (CheckTextExist(deviceID, "mã xác nhận", 3))
                {
                    return "xac_nhan";
                }
                else
                {
                    return Constant.FAIL;
                }
            }
            if (!WaitAndTapXML(deviceID, 9, "savecheckable"))
            {
                if (WaitAndTapXML(deviceID, 2, "tiếp tục dùng tiếng anh mỹ"))
                {
                    if (!WaitAndTapXML(deviceID, 3, "savecheckable"))
                    {
                        fail++;
                        LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                        if (!WaitAndTapXML(deviceID, 3, "savecheckable"))
                        {
                            if (!WaitAndTapXML(deviceID, 3, "tôi đồng ý checkable"))
                            {
                                fail++;
                                return Constant.FAIL;
                            }
                            else
                            {
                                return name + "|" + phone;
                            }
                        }
                    }
                }
                else
                {
                    fail++;
                    LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                    if (!WaitAndTapXML(deviceID, 1, "savecheckable"))
                    {
                        if (!WaitAndTapXML(deviceID, 3, "I agree checkable"))
                        {
                            fail++;
                            return Constant.FAIL;
                        }
                        else
                        {
                            return name + "|" + phone;
                        }
                    }
                }

            }
            //Thread.Sleep(7000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.6, 34.5); // Lúc khác lưu
            //Thread.Sleep(1000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 59.6); // đồng ý

            if (!WaitAndTapXML(deviceID, 4, "I agree checkable"))
            {
                if (!WaitAndTapXML(deviceID, 1, "savecheckable"))
                {
                    if (WaitAndTapXML(deviceID, 2, "tiếp tục dùng tiếng anh mỹ"))
                    {
                        if (!WaitAndTapXML(deviceID, 3, "savecheckable"))
                        {
                            fail++;
                            LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                            if (!WaitAndTapXML(deviceID, 3, "savecheckable"))
                            {
                                if (!WaitAndTapXML(deviceID, 1, "I Agree checkable"))
                                {
                                    fail++;
                                    return Constant.FAIL;
                                }
                                else
                                {
                                    return name + "|" + phone;
                                }
                            }
                        }
                    }
                    else
                    {
                        fail++;
                        LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                        if (!WaitAndTapXML(deviceID, 3, "saveheckable"))
                        {
                            if (!WaitAndTapXML(deviceID, 1, "I agree checkable"))
                            {
                                fail++;
                                return Constant.FAIL;
                            }
                            else
                            {
                                return name + "|" + phone;
                            }
                        }
                    }

                }
                //Thread.Sleep(7000);
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.6, 34.5); // Lúc khác lưu
                //Thread.Sleep(1000);
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 59.6); // đồng ý

                if (!WaitAndTapXML(deviceID, 4, "I agree checkable"))
                {
                    fail++;
                    LogStatus(device, "không thấy màn hình đồng ý", 5000);
                    return Constant.FAIL;
                }
                Thread.Sleep(3000);
                if (CheckTextExist(deviceID, "I agree checkable", 1))
                {
                    fail++;
                    LogStatus(device, "Không thể tạo tài khoản");
                    return Constant.FAIL;
                }
            }

            if (WaitAndTapXML(deviceID, 4, "I agree checkable"))
            {
                LogStatus(device, "Check màn hình mới");
            }
            if (CheckTextExist(deviceID, "I agree ", 1))
            {
                fail++;
                LogStatus(device, "Không thể tạo tài khoản");
                return Constant.FAIL;
            }
            LogStatus(device, "Check các màn hình");
            for (int i = 0;i < 10;i ++)
            {
                string xxmlll = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "confirm your mobile number with an sms", 1, xxmlll))
                {
                    WaitAndTapXML(deviceID, 1, "continue", xxmlll);
                    Thread.Sleep(3000);
                    // Check livewall
                    if (CheckTextExist(deviceID, "continue", 1))
                    {
                        return Constant.FAIL;
                    }
                }
            }
            return name + "|" + phone;
        }

        public string FlowNormalNew(OrderObject order, DeviceObject device, string gender, string password, int yearOld, int delay,
            string selectedDeviceName)
        {
            string deviceID = device.deviceId;
            string name = InputNameNew(device, order, deviceID, gender);
            LogStatus(device, "Name:" + name);
            string phone = "";
            if (name == Constant.FAIL)
            {
                fail++;
                LogStatus(device, "Nhập tên bị sai", 5000);
                return Constant.FAIL;
            }

            //Thread.Sleep(delay);

            if (!InputBirthdayNew(deviceID))
            {
                //fail++;
                LogStatus(device, "Nhập ngày sinh bị sai", 1000);
                //return Constant.FAIL;
            }
            if (!InputGenderNew(device, gender))
            {
                LogStatus(device, "Nhập giới tính sai", 1000);
                fail++;
                return Constant.FAIL;
            }



            //Thread.Sleep(2000);
            //WaitAndTapXML(deviceID, 3, "chophépresourceid");
            WaitAndTapXML(deviceID, 3, "từchốiresourceid");
            if (device.regByMail || order.veriDirectHotmail || order.veriDirectGmail)
            {
                if (!CheckTextExist(deviceID, "email của bạn là gì", 3))
                {
                    Utility.WaitAndTapXML(deviceID, 3, "Bằng email");
                    Thread.Sleep(3000);
                }

                Thread.Sleep(1000);
                MailObject mail = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language); // random mail
                if (order.veriDirectHotmail)
                {
                    mail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);

                    order.currentMail = mail;
                }
                else
                {
                    if (forceVeriAccRegBMailcheckBox.Checked && veriaccgmailCheckBox.Checked)
                    {
                        order.veriAcc = true;
                    }
                }
                if (Constant.ACC_TYPE_FIX_PASSWORD != order.accType)
                {
                    order.accType = Constant.ACC_TYPE_REG_BY_MAIL;
                }
                if (!WaitAndTapXML(deviceID, 1, "descemailcheckablefalsechecked"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.7, 31.5);
                }
                LogStatus(device, "Reg by Mail:" + mail.email, 1000);
                Utility.InputUsText(deviceID, mail.email, inputStringMailCheckBox.Checked);
                LogRegStatus(dataGridView, device, "Reg by mail");
                //Next(deviceID);
            }
            else //////////////////  Reg by phone
            {
                //if (!CheckTextExist(deviceID, "số di động của bạn là gì", 5) 
                //    && !CheckTextExist(deviceID, "số di động hoặc email", 1))
                //{

                //}

                bool checkScreen = false;
                for (int i = 0; i < 6; i++)
                {
                    if (CheckTextExist(deviceID, new string[] { "số di động của bạn là gì", "số di động hoặc email" }))
                    {
                        checkScreen = true;
                        break;
                    }
                }
                if (!checkScreen)
                {
                    LogStatus(device, "Không thấy màn hình nhập số điện thoại", 1000);
                    if (CheckTextExist(deviceID, "email của bạn là gì", 2))
                    {
                        Device.Back(deviceID);
                        if (WaitAndTapXML(deviceID, 3, "đăngkýbằngsốdiđộngcheckable"))
                        {
                            LogStatus(device, "Màn hình đăng ký bằng email", 2000);
                        }
                        else
                        {
                            Device.Back(deviceID);
                            if (!WaitAndTapXML(deviceID, 3, "đăngkýbằngsốdiđộngcheckable"))
                            {
                                KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.4, 56.2);
                            }
                        }
                    }
                }

                LogRegStatus(dataGridView, device, "Reg by phone");
                //Device.TapDelay(deviceID, 1360, 959);
                if (order.carryCodePhone)
                {
                    phone = GeneratePhoneCarryCode();
                }
                else if (order.dauso12)
                {
                    phone = GeneratePhone12Prefix(dauso12TextBox.Text.Split(','));
                }
                else if (order.dauso)
                {
                    phone = GeneratePhonePrefix(dausotextbox.Text.Split(','));
                }
                else if (order.veriDirectByPhone)
                {
                    if (!IsDigitsOnly(order.phoneT.phone))
                    {
                        LogStatus(device, "Can not get phone:" + order.phoneT.message);
                        order.veriDirectByPhone = false;
                        order.phoneT.phone = GeneratePhonePrefix(prefixTextNow);
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;

                        Thread.Sleep(5000);
                        return "";
                    }
                    phone = "+1" + order.phoneT.phone;
                }
                else if (order.americaPhone)
                {
                    phone = GeneratePhoneAmerica();
                }
                else if (order.prefixTextnow)
                {
                    phone = "+1" + GeneratePhonePrefix(prefixTextNow);
                }
                else
                {
                    if (randomPhoneCheckBox.Checked)
                    {
                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked);
                    }
                    else
                    {
                        var listNetwork = new ArrayList();
                        if (vinaphoneCheckbox.Checked)
                        {
                            listNetwork.Add(Constant.VINAPHONE);
                        }
                        if (viettelCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETTEL);
                        }
                        if (mobiphoneCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.MOBI);
                        }
                        if (vietnamMobileCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETNAM_MOBILE);
                        }

                        string network = "";
                        if (listNetwork != null && listNetwork.Count > 0)
                        {
                            network = (string)listNetwork.ToArray().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                        }

                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked, network);

                    }

                    string check = checkNetwork(phone);
                    LogStatus(device, "Phone server:" + phone + "-" + check);

                    //Thread.Sleep(1000);
                }
                if (!order.veriDirectByPhone)
                {
                    order.phoneT.phone = phone;
                    LogRegStatus(dataGridView, device, phone);
                }

                if (!string.IsNullOrEmpty(order.regPhone))
                {
                    phone = order.regPhone;
                }
                LogStatus(device, "Reg by Phone flow normal:" + phone);
                device.log = phone;
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 87.9, 31.8); // tap x xóa phone cũ
                //Thread.Sleep(500);
                Device.MoveEndTextbox(deviceID);
                Device.DeleteChars(deviceID, 12);
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                //phone = phone.Replace("+84", "0");
                InputText(deviceID, Utility.ConvertToUnsign(phone), inputStringMailCheckBox.Checked);
            }
            // Device.TapByPercent(deviceID, 89.0, 95.7); // Hạ bàn phím
            Thread.Sleep(1000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.6, 47.9);// tiếp
            if (!WaitAndTapXML(deviceID, 2, "tiếpcheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.6, 47.9);// tiếp
            }

            for (int i = 0; i < 4; i ++)
            {
                string xxm = GetUIXml(deviceID);

                if (CheckTextExist(deviceID, "tạo mật khẩu", 1, xxm))
                {
                    break;
                }

                if (WaitAndTapXML(deviceID, 1, "tiếptụctạotàikhoảncheckable", xxm))
                {
                    Thread.Sleep(1000);
                    break;
                }
            }

        Thread.Sleep(2000);

            if (micerCheckBox.Checked)
            {
                InputTextMicer(deviceID, password);
            }
            else
            {
                InputMail(deviceID, password, false);
            }
            Console.WriteLine("Pass:" + password);

            if (!WaitAndTapXML(deviceID, 2, "tiếpcheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.7, 38.9); // tiếp
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 79.6, 50.9); // tiếp

                WaitAndTapXML(deviceID, 15, "tiếp tục dùng tiếng anh mỹ");
                WaitAndTapXML(deviceID, 12, "lưucheckable");
                if (CheckTextExist(deviceID, "mã xác nhận", 3))
                {
                    return "xac_nhan";
                }
                else
                {
                    return Constant.FAIL;
                }
            }
            if (!WaitAndTapXML(deviceID, 9, "lưucheckable"))
            {
                if (WaitAndTapXML(deviceID, 2, "tiếp tục dùng tiếng anh mỹ"))
                {
                    if (!WaitAndTapXML(deviceID, 3, "lưucheckable"))
                    {
                        fail++;
                        LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                        if (!WaitAndTapXML(deviceID, 3, "lưucheckable"))
                        {
                            if (!WaitAndTapXML(deviceID, 3, "tôi đồng ý checkable"))
                            {
                                fail++;
                                return Constant.FAIL;
                            }
                            else
                            {
                                return name + "|" + phone;
                            }
                        }
                    }
                }
                else
                {
                    fail++;
                    LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                    if (!WaitAndTapXML(deviceID, 1, "lưucheckable"))
                    {
                        if (!WaitAndTapXML(deviceID, 3, "tôi đồng ý checkable"))
                        {
                            fail++;
                            return Constant.FAIL;
                        }
                        else
                        {
                            return name + "|" + phone;
                        }
                    }
                }

            }
            //Thread.Sleep(7000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.6, 34.5); // Lúc khác lưu
            //Thread.Sleep(1000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 59.6); // đồng ý

            if (!WaitAndTapXML(deviceID, 4, "tôi đồng ý checkable"))
            {
                if (!WaitAndTapXML(deviceID, 1, "lưucheckable"))
                {
                    if (WaitAndTapXML(deviceID, 2, "tiếp tục dùng tiếng anh mỹ"))
                    {
                        if (!WaitAndTapXML(deviceID, 3, "lưucheckable"))
                        {
                            fail++;
                            LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                            if (!WaitAndTapXML(deviceID, 3, "lưucheckable"))
                            {
                                if (!WaitAndTapXML(deviceID, 1, "tôi đồng ý checkable"))
                                {
                                    fail++;
                                    return Constant.FAIL;
                                }
                                else
                                {
                                    return name + "|" + phone;
                                }
                            }
                        }
                    }
                    else
                    {
                        fail++;
                        LogStatus(device, "Không thấy màn hình lưu thông tin", 5000);
                        if (!WaitAndTapXML(deviceID, 3, "lưucheckable"))
                        {
                            if (!WaitAndTapXML(deviceID, 1, "tôi đồng ý checkable"))
                            {
                                fail++;
                                return Constant.FAIL;
                            }
                            else
                            {
                                return name + "|" + phone;
                            }
                        }
                    }

                }

                if (!WaitAndTapXML(deviceID, 4, "tôi đồng ý checkable"))
                {
                    fail++;
                    LogStatus(device, "không thấy màn hình đồng ý", 5000);
                    return Constant.FAIL;
                }
                Thread.Sleep(3000);
                if (CheckTextExist(deviceID, "tôi đồng ý checkable", 1))
                {
                    fail++;
                    LogStatus(device, "Không thể tạo tài khoản");
                    return Constant.FAIL;
                }
            } else
            {
                // Check change screen
                for (int i = 0;i < 15;i ++)
                {
                    string xmlll = GetUIXml(deviceID);
                    if (!CheckTextExist(deviceID, "cácđiềukhoản", 1, xmlll))
                    {
                        break;
                    }
                }

                if (WaitAndTapXML(deviceID, 2, "thử lại resource"))
               
                for (int i = 0; i < 3; i ++)
                {
                    string xmlll = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, "nhập mã xác nhận", 1, xmlll))
                    {
                        return "xac_nhan";
                    }
                    if (WaitAndTapXML(deviceID, 1, "gửi mã qua sms", xmlll))
                    {
                        Thread.Sleep(1000);
                        if (CheckTextExist(deviceID, "nhập mã xác nhận", 2))
                        {
                            return "xac_nhan";
                        }
                    }
                    if (WaitAndTapXML(deviceID, 1, "continue", xmlll))
                    {
                        Thread.Sleep(3000);
                    }
                    if (CheckTextExist(deviceID, "cần thêm thông tin", 1, xmlll))
                    {
                        return Constant.FAIL;
                    }
                }
                    
                
            }

            return name + "|" + phone;
        }

        public string FlowNormal2(OrderObject order, DeviceObject device, string gender, string password, int yearOld, int delay,
           string selectedDeviceName)
        {
            string deviceID = device.deviceId;
            string name = InputName2(device, order, deviceID, gender);
            LogStatus(device, "Name:" + name);
            string phone = "";
            if (name == Constant.FAIL)
            {
                fail++;
                return Constant.FAIL;
            }

            Thread.Sleep(3000);

            if (!InputBirthday2(deviceID))
            {
                fail++;
                return Constant.FAIL;
            }
            InputGender(device, gender);

            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.8, 64.5);
            //Next(deviceID);
            Thread.Sleep(delay);

            if (device.regByMail || order.veriDirectHotmail || order.veriDirectGmail)
            {
                Utility.WaitAndTapXML(deviceID, 3, Language.SignUpWithemail());
                Thread.Sleep(1000);
                MailObject mail = Mail.MailGenerator(mailSuffixtextBox.Text, gender, order.language); // random mail
                if (order.veriDirectHotmail)
                {
                    mail = GetHotmail(device, serverCacheMailTextbox.Text, order.emailType, order.getHotmailKieumoi, 5, otpVandongcheckBox.Checked);

                    order.currentMail = mail;
                }

                LogStatus(device, "Reg by Mail:" + mail.email);
                Utility.InputVietVNIText(deviceID, mail.email);
                LogRegStatus(dataGridView, device, "Reg by mail");
                Next(deviceID);
            }
            else //////////////////  Reg by phone
            {
                LogRegStatus(dataGridView, device, "Reg by phone");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 75.7, 32.8);
                if (order.carryCodePhone)
                {
                    phone = GeneratePhoneCarryCode();
                }
                else if (order.dauso12)
                {
                    phone = GeneratePhone12Prefix(dauso12TextBox.Text.Split(','));
                }
                else if (order.dauso)
                {
                    phone = GeneratePhonePrefix(dausotextbox.Text.Split(','));
                }
                else if (order.veriDirectByPhone)
                {
                    if (!IsDigitsOnly(order.phoneT.phone))
                    {
                        LogStatus(device, "Can not get phone:" + order.phoneT.message);
                        order.veriDirectByPhone = false;
                        order.phoneT.phone = GeneratePhonePrefix(prefixTextNow);
                        dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;

                        Thread.Sleep(5000);
                        return "";
                    }
                    phone = "+1" + order.phoneT.phone;
                }
                else if (order.americaPhone)
                {
                    phone = GeneratePhoneAmerica();
                }
                else if (order.prefixTextnow)
                {
                    phone = "+1" + GeneratePhonePrefix(prefixTextNow);
                }
                else
                {
                    if (randomPhoneCheckBox.Checked)
                    {
                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked);
                    }
                    else
                    {
                        var listNetwork = new ArrayList();
                        if (vinaphoneCheckbox.Checked)
                        {
                            listNetwork.Add(Constant.VINAPHONE);
                        }
                        if (viettelCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETTEL);
                        }
                        if (mobiphoneCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.MOBI);
                        }
                        if (vietnamMobileCheckBox.Checked)
                        {
                            listNetwork.Add(Constant.VIETNAM_MOBILE);
                        }

                        string network = "";
                        if (listNetwork != null && listNetwork.Count > 0)
                        {
                            network = (string)listNetwork.ToArray().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                        }

                        phone = GeneratePhoneNumber(isServer, dausotextbox.Text, randomPrePhoneCheckbox.Checked, usePhoneLocalCheckBox.Checked, usPhoneCheckBox.Checked, network);

                    }
                    if (!order.veriDirectByPhone)
                    {
                        order.phoneT.phone = phone;
                        LogRegStatus(dataGridView, device, phone);
                    }
                    //phone = phone.Replace("+84", "0");
                    LogStatus(device, "Reg phone server:" + phone);
                    //LogRegStatus(dataGridView, device, device.network);
                    Thread.Sleep(1000);

                    //order.phoneT.phone = phone;
                }
                if (!order.veriDirectByPhone)
                {
                    order.phoneT.phone = phone;
                    LogRegStatus(dataGridView, device, phone);
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 75.7, 32.8);
                Thread.Sleep(1000);
                LogStatus(device, "Reg by Phone flow normal:" + phone);
                device.log = phone;

                InputVietVNIText(deviceID, Utility.ConvertToUnsign(phone));

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.8, 47.9);

                string ui = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "Mật khẩu", 1, ui))
                {
                    goto INPUT_PASSWORD;
                }

                if (CheckTextExist(deviceID, "khôngthểxácthựcsốdiđộng", 1, ui)
                    || CheckTextExist(deviceID, "already in use", 1, ui)
                    || CheckTextExist(deviceID, "gần đây", 1, ui))
                {
                    LogStatus(device, "Phone đã được sử dụng");
                    Thread.Sleep(5000);
                    return "";
                }
            }


        INPUT_PASSWORD:

            if (micerCheckBox.Checked)
            {
                InputTextMicer(deviceID, password);
            }
            else
            {
                InputMail(deviceID, password, inputStringMailCheckBox.Checked);
            }
            Console.WriteLine("Pass:" + password);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.2, 43.3);

            Thread.Sleep(2500);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.8, 29.3);
            Thread.Sleep(3000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.2, 75.8);
            return name + "|" + phone;
        }
        public string GetGender(OrderObject order)
        {
            string gender;

            if (!string.IsNullOrEmpty(order.gender))
            {
                gender = order.gender;
            }

            else
            {
                Random r = new Random();
                int rand = r.Next(100);

                if (rand > 50)
                {
                    gender = Constant.FEMALE;
                }
                else
                {
                    gender = Constant.MALE;
                }
            }
            return gender;
        }

        public bool DoiTenViet(DeviceObject device, string gender, string pass)
        {
            string lastname = GetLastName(Constant.LANGUAGE_VN, gender, name3wordcheckBox.Checked);
            
            string firstname = GetFirtName(Constant.LANGUAGE_VN, gender, nameVnUscheckBox.Checked);
            

            NameObject nnn = ServerApi.GetNameLocalCache(serverCacheMailTextbox.Text, gender, Constant.LANGUAGE_VN);
            if (nnn != null && !string.IsNullOrEmpty(nnn.name))
            {
                firstname = nnn.name;
                lastname = nnn.lastname;
            }
            if (lastname.Contains("Ð") || lastname.Contains("đ") || ConvertToUnsign(lastname).ToLower().StartsWith("d"))
            {
                lastname = GetLastName(Constant.LANGUAGE_VN, gender, name3wordcheckBox.Checked);
            }
            if (firstname.Contains("Ð") || firstname.Contains("đ") || ConvertToUnsign(firstname).ToLower().StartsWith("d"))
            {
                firstname = GetFirtName(Constant.LANGUAGE_VN, gender, nameVnUscheckBox.Checked);
            }

            string deviceID = device.deviceId;
            Device.GotoFbSettings(deviceID);
            Thread.Sleep(3000);
            if (moiFbLitecheckBox.Checked)
            {
                WaitAndTapXML(deviceID, 1, "luônchọnresourceid");
            }
            if (CheckTextExist(deviceID, "nhập mã xác nhận"))
            {
                LogStatus(device, "Acc chưa veri - bỏ acc");
                return false;
            }
            if (WaitAndTapXML(deviceID, 1, "thôngtincánhânvàtàikhoảncheckable"))
            {
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.3, 22.1); // tap tên
                Thread.Sleep(3000);
                if (CheckTextExist(deviceID, "bạnkhôngthểđổitêntrênfacebook", 2))
                {
                    LogStatus(device, "Acc đã được đổi tên trước đó");
                    return true;
                }
                else
                {

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 22.5); // tap họ box
                    Thread.Sleep(2000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 22.5); // tap họ box
                    Thread.Sleep(1000);

                    Device.DeleteChars(deviceID, 40);
                    Device.DeleteAllChars(deviceID);
                    InputVietVNIText(deviceID, lastname);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 35.4); // tap tên box
                    Device.DeleteChars(deviceID, 40);
                    Device.DeleteAllChars(deviceID);
                    InputVietVNIText(deviceID, firstname);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.8, 24.2); // tap ra ngoài hạ bàn phím

                    WaitAndTapXML(deviceID, 1, "xemlạithayđổiresourcei");

                    if (!CheckTextExist(deviceID, "xem trước", 2))
                    {
                        return false;
                    }

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.1, 45.4); // tap password
                    InputText(deviceID, pass, false);

                    if (!WaitAndTapXML(deviceID, 2, "lưuthayđổiresourceid"))
                    {
                        return false;
                    }

                    LogStatus(device, "Change name ok -------");
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Green;
                    return true;
                }
            }
            FindImageAndTap(deviceID, FA_OK, 2); // nhảy màn hình confirm
            if (!CheckTextExistTime(deviceID, new string[] { "bảngtin", "thông báo", "bảng feed" }, 4))
            {
                Device.KillApp(deviceID, Constant.FACEBOOK_PACKAGE);
                Device.GotoFbSettings(deviceID);
                if (!CheckTextExistTime(deviceID, new string[] { "bảngtin", "thông báo", "bảng feed" }, 4))
                {
                    return false;
                }
            }
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 70.6, 20.6); // Tap trung tâm điều khiển


            if (!WaitAndTapXML(deviceID, 3, "desctrangcánhâncheckable"))
            {
                if (!WaitAndTapXML(deviceID, new[] { "dùng tiếng anh", "ok", "đóng" }))
                {
                    WaitAndTapXML(deviceID, 2, "tiếp tục dùng tiếng anh mỹ");
                }
                Thread.Sleep(3000);
                Device.GotoFbSettings(deviceID);
                Thread.Sleep(3000);
                FindImageAndTap(deviceID, FA_OK, 4); // nhảy màn hình confirm
                Thread.Sleep(3000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 70.6, 28.6); // Tap trung tâm điều khiển

                if (!WaitAndTapXML(deviceID, 3, "desctrangcánhâncheckable"))
                {

                    return false;
                }
            }
            if (!WaitAndTapXML(deviceID, 3, "facebookresourceid"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.4, 37.0);
            }
            if (!WaitAndTapXML(deviceID, 3, "desctêncheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.2, 41.7);
            }
            Thread.Sleep(3000);
            if (CheckTextExist(deviceID, "đã đổi tên", 1))
            {

                return true;
            }
            CheckTextExist(deviceID, "tên", 3);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 80.6, 27.1); // tap tên
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.2, 18.5);
            Thread.Sleep(1000);
            firstname = " " + firstname;
            InputVietVNIText(deviceID, firstname);

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.0, 36.0); // họ
                                                                      // Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 88.1, 35.3);
            Thread.Sleep(1000);
            lastname = " " + lastname;
            InputVietVNIText(deviceID, lastname);

            WaitAndTapXML(deviceID, 3, "descxemlạithayđổicheckable");
            WaitAndTapXML(deviceID, 3, "desc lưu thayđổicheckable");
            Thread.Sleep(4000);

            if (CheckTextExist(deviceID, "tên người dùng", 5))
            {
                LogStatus(device, "Đổi tên thành công");
                return true;
            }

            return false;
        }

        public string Set2faSettings(OrderObject order, DeviceObject device, string password, bool tryAgain = false)
        {
            try
            {
                string deviceID = device.deviceId;
            START_2FA:
                string uiXML = GetUIXml(deviceID);

                if (CheckTextExist(deviceID, "truycậpvịtrí", 1, uiXML))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                    Thread.Sleep(2000);
                }

                if (FindImageAndTap(deviceID, THEM_5BB, 1))
                {
                    LogStatus(device, "Thêm 5 người bạn - trong 2fa");
                    Thread.Sleep(3000);
                }
                Device.GotoFbSettings(deviceID);

                FindImageAndTap(deviceID, FA_OK, 3);

                string qrCode = "";
                if (WaitAndTapXML(deviceID, 1, "mậtkhẩuvàbảomậtcheckable")
                    || WaitAndTapXML(deviceID, 1, "mậtkhẩuvàbảomậtresourceid"))
                {
                    WaitAndTapXML(deviceID, 3, "dùngtínhnăngxácthực2yếutốresourceid");
                    Thread.Sleep(3000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.6, 95.2);// Continue



                    CheckTextExist(deviceID, "thiết lập", 5);

                    Device.TapByPercentDelay(deviceID, 38.4, 46.3);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.9, 74.5);



                    Thread.Sleep(1000);
                    qrCode = Device.ReadClipboardAndroid(deviceID, fbLiteCheckbox.Checked);

                    if (string.IsNullOrEmpty(qrCode))
                    {
                        LogStatus(device, "set2fa2 - Can not copy QR code ----------");

                        Device.TapByPercentDelay(deviceID, 38.4, 46.3);

                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.9, 74.5);


                        Thread.Sleep(1000);
                        qrCode = Device.ReadClipboardAndroid(deviceID, fbLiteCheckbox.Checked);
                        if (string.IsNullOrEmpty(qrCode))
                        {
                            Thread.Sleep(2000);
                            return null;
                        }
                    }
                    var base32Bytes1 = Base32Encoding.ToBytes(qrCode);

                    var otp1 = new Totp(base32Bytes1);
                    string token1 = otp1.ComputeTotp();
                    if (string.IsNullOrWhiteSpace(token1))
                    {
                        return null;
                    }
                    Thread.Sleep(2000);
                    Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                    Thread.Sleep(500);

                    Thread.Sleep(500);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 40.6, 29.5);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 34.7, 30.3);
                    Thread.Sleep(500);
                    Utility.InputConfirmCode(deviceID, token1);

                    Thread.Sleep(500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.3, 95.2); // Hạ bàn phím
                    Thread.Sleep(500);

                    Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                    Device.TapByPercentDelay(deviceID, 54.0, 90.7);
                    Thread.Sleep(2200);
                    if (CheckTextExist(deviceID, "mậtkhẩu", 3))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 43.1, 25.7);
                        Thread.Sleep(1000);
                        InputText(deviceID, password, false);
                        Thread.Sleep(1000);
                        if (!WaitAndTapXML(deviceID, 2, "texttiếptụcresourceid"))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.0, 34.0);
                        }


                        if (CheckTextExist(deviceID, "yếu tố đang bật", 5))
                        {
                            return qrCode;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                    Device.TapByPercentDelay(deviceID, 54.0, 90.7);
                    LogStatus(device, "set2fa2 - " + qrCode);

                    Thread.Sleep(2000);
                    string ddd = GetUIXml(deviceID);
                    if (CheckTextExist(deviceID, "đang bật", 1, ddd) || CheckTextExist(deviceID, "mãcủabạn", 1, ddd)
                        || CheckTextExist(deviceID, "mãkhôiphục", 1, ddd))
                    {
                        return qrCode;
                    }
                    else
                    {
                        return "";
                    }
                }

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 21.5, 31.2); // tap mat khau va bao mat

                if (!CheckTextExist(deviceID, "trangcánhân", 5))
                {
                    Device.Back(deviceID);
                    Thread.Sleep(2000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 21.5, 31.2); // tap mat khau va bao mat
                    if (!CheckTextExist(deviceID, "trangcánhân", 4))
                    {

                        return "";
                    }
                }

                WaitAndTapXML(deviceID, 5, "mậtkhẩuvàbảomậtcheckablefal");

                WaitAndTapXML(deviceID, 2, "xác thực 2 yếu tố");
                WaitAndTapXML(deviceID, 2, "facebookcheckable");

                Thread.Sleep(2000);
                if (!order.isHotmail)
                {
                    return "store";
                }
                LogStatus(device, "Lấy code từ hotmail");
                string code = GetCode2faInMail(order.currentMail, 5);

                if (!string.IsNullOrEmpty(code) && code != Constant.FAIL)
                {

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.0, 56.2);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 72.0, 56.2);
                    LogStatus(device, "nhập code:" + code);
                    InputText(deviceID, code, false);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.3, 95.2); // Hạ bàn phím
                    WaitAndTapXML(deviceID, 2, "tiếp tục");
                }
                else
                {
                    return "store";
                }
                if (CheckTextExist(deviceID, "không thể thực hiện", 2))
                {
                    return "store";
                }
                if (WaitAndTapXML(deviceID, 2, "mậtkhẩucheckable"))
                {
                    InputText(deviceID, password, true);
                    if (!WaitAndTapXML(deviceID, 2, "tiếp tục"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.4, 49.5);
                    }
                }

                Thread.Sleep(4000);
                if (CheckTextExist(deviceID, "ứngdụngxácthựcresourceid", 2))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.6, 95.2);// Continue
                }
                else
                {
                    return "";
                }

                Thread.Sleep(2000);
                if (!CheckTextExist(deviceID, "Hướng dẫn thiết lập", 5))
                {
                    return "";
                }
                string uiXml = GetUIXml(deviceID);

                if (!string.IsNullOrEmpty(order.qrCode) && CheckTextExist(deviceID, "xácthực2yếutốđangbật", 1, uiXml) && tryAgain)
                {
                    order.set2FaSuccess = true;
                    return order.qrCode;
                }
                if (CheckTextExist(deviceID, "đăngnhậpbằngtàikhoảnkhác", 1, uiXml))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.7, 48.0);
                }
                if (CheckTextExist(deviceID, "đang bật", 1, uiXml) || CheckTextExist(deviceID, "khôi phục", 1, uiXml))
                {
                    LogStatus(device, "set2fa - 2fa đang bật, tắt đi bật lại");
                    Thread.Sleep(2000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.4, 39.0);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.1, 38.9);

                    if (CheckTextExist(deviceID, "khôi phục", 1))
                    {
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.4, 39.0);
                        Thread.Sleep(1000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 6.1, 38.9);
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(1000);
                    if (!WaitAndTapXML(deviceID, 3, "tắtcheckable"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.2, 89.4);
                    }
                    Thread.Sleep(1000);

                    if (CheckTextExist(deviceID, "nhập mật khẩu", 2))
                    {
                        LogStatus(device, "Nhập lại password");
                        Device.TabPress(deviceID);

                        InputText(deviceID, password, false);

                        Thread.Sleep(1000);
                        if (!WaitAndTapXML(deviceID, 2, "text tiếp tục resource"))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.8, 34.8);
                        }
                        Thread.Sleep(4000);
                    }
                    Thread.Sleep(2000);
                    if (!WaitAndTapXML(deviceID, 1, "đăngnhậpbằngmãtừđiệnthoạicủabạncũngnhưmậtkhẩu"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.1, 66.6);
                    }
                }
                if (CheckTextExist(deviceID, "đổimậtkhẩu", 1, uiXml))
                {
                    Device.Back(deviceID);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 26.1, 73.6);
                    Thread.Sleep(1000);
                }

            SET_CODE_2FA_FAST:


                CheckTextExist(deviceID, "thiết lập", 5);

                //Device.TapByPercentDelay(deviceID, 38.4, 46.3);

                //Device.TapByPercent(deviceID, 42.9, 74.5, 1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 18.1, 63.9);
                Thread.Sleep(1000);
                qrCode = Device.ReadClipboardAndroid(deviceID, fbLiteCheckbox.Checked);

                if (string.IsNullOrEmpty(qrCode))
                {
                    LogStatus(device, "set2fa - Can not copy QR code ----------");

                    Device.TapByPercentDelay(deviceID, 38.4, 46.3);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.9, 74.5);


                    Thread.Sleep(1000);
                    qrCode = Device.ReadClipboardAndroid(deviceID, fbLiteCheckbox.Checked);
                    if (string.IsNullOrEmpty(qrCode))
                    {
                        Thread.Sleep(2000);
                        return "";
                    }
                }
                var base32Bytes = Base32Encoding.ToBytes(qrCode);

                var otp = new Totp(base32Bytes);
                string token = otp.ComputeTotp();

                if (string.IsNullOrWhiteSpace(token))
                {
                    return "";
                }

                Thread.Sleep(2000);
                Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                Thread.Sleep(500);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 40.6, 29.5);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 34.7, 30.3);
                Thread.Sleep(500);
                Utility.InputConfirmCode(deviceID, token);

                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.3, 95.2); // Hạ bàn phím
                Thread.Sleep(1500);
                if (!WaitAndTapXML(deviceID, 2, "xong"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.3, 87.4);
                }


                if (CheckTextExist(deviceID, "2 yếu tố đang bật", 3))
                {
                    if (!WaitAndTapXML(deviceID, 2, "xong"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.3, 87.4);
                    }
                    return qrCode;
                }
                else
                {
                    return "";
                }
                LogStatus(device, qrCode);
                return qrCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public string Set2faSecuritySettings(OrderObject order, DeviceObject device, string password, bool tryAgain = false)
        {
            try
            {
            // test code
            //return "";
            START_2FA:
                string deviceID = device.deviceId;
                LogStatus(device, "set2fa - -2");
                string uiXML = GetUIXml(deviceID);
                //if (WaitAndTapXML(deviceID, 1, "Thêm 5 người bạn", uiXML))
                if (FindImageAndTap(deviceID, THEM_5BB, 1))
                {
                    Thread.Sleep(3000);
                }
                if (CheckTextExist(deviceID, "truycậpvịtrí", 1, uiXML))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 65.1);
                    Thread.Sleep(2000);

                }

                Device.GotoFbSecuritySettings(deviceID);
                Thread.Sleep(5000);
                if (!CheckTextExist(deviceID, "mật khẩu và bảo mật", 5))
                {
                    return "";
                }
                uiXML = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "mởbằngfacebook", 1, uiXML))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.7, 78.1);
                    Thread.Sleep(2000);
                    uiXML = GetUIXml(deviceID);
                }
                if (CheckTextExist(deviceID, "mởbằnglite", 1, uiXML))
                {
                    FindImageAndTap(deviceID, CHOOSE_FB, 1);
                    Thread.Sleep(2000);
                    uiXML = GetUIXml(deviceID);
                }

                if (FindImageAndTap(deviceID, BAT_DANH_BA, 1))
                {

                    if (CheckTextExist(deviceID, "đồng bộ"))
                    {
                        for (int k = 0; k < WaitAddContactCount; k++)
                        {
                            if (!CheckTextExist(deviceID, "đồng bộ"))
                            {
                                Console.WriteLine("đồng bộ:" + k);
                                break;
                            }
                        }
                    }

                    //if (WaitAndTapXML(deviceID, 1, "Thêm 5 người bạn"))
                    if (FindImageAndTap(deviceID, THEM_5BB, 1))
                    {

                        WaitAndTapXML(deviceID, 1, Language.AllowAll());
                        if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                        {
                            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                        }
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                        Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                        WaitAndTapXML(deviceID, 1, "Xong");
                    }
                    goto START_2FA;
                }
                //if (WaitAndTapXML(deviceID, 1, "Thêm 5 người bạn", uiXML) || WaitAndTapXML(deviceID, 1, "Thêm 4 người bạn", uiXML))
                if (FindImageAndTap(deviceID, THEM_5BB, 1))
                {
                    WaitAndTapXML(deviceID, 1, Language.AllowAll());
                    if (!WaitAndTapXML(deviceID, 1, Language.InviteAll()))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                    }
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 96.8);
                    Utility.WaitAndTapXML(deviceID, 1, Language.YESresource());
                    WaitAndTapXML(deviceID, 1, "Xong");

                    goto START_2FA;
                }



                if (!FindImageAndTap(deviceID, FB_2FA, 3))
                { // check
                    LogStatus(device, "set2fa2 - Can not set2fa");
                    return "";
                }


                string uiXml = GetUIXml(deviceID);

                if (CheckTextExist(deviceID, "đăngnhậpbằngtàikhoảnkhác", 1, uiXml))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.7, 48.0);
                }

                if (CheckTextExist(deviceID, "đổimậtkhẩu", 1, uiXml))
                {
                    Device.Back(deviceID);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 26.1, 73.6);
                    Thread.Sleep(1000);
                }

                if (WaitAndTapXML(deviceID, 2, Language.Usetwofactorauthenticationresourceid()))
                {

                    goto SET_CODE_2FA_FAST;
                }
                else
                {

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 60.1, 66.8);
                    if (WaitAndTapXML(deviceID, 2, Language.Usetwofactorauthenticationresourceid()))
                    {

                        goto SET_CODE_2FA_FAST;
                    }
                    else
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 30.6, 75.9);
                        if (WaitAndTapXML(deviceID, 2, Language.Usetwofactorauthenticationresourceid()))
                        {

                            goto SET_CODE_2FA_FAST;
                        }
                        return "";
                    }
                }


            SET_CODE_2FA_FAST:

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.6, 95.2);// Continue

                string qrCode = "";

                CheckTextExist(deviceID, "thiết lập", 5);

                Device.TapByPercentDelay(deviceID, 38.4, 46.3);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.9, 74.5);



                Thread.Sleep(1000);
                qrCode = Device.ReadClipboardAndroid(deviceID, fbLiteCheckbox.Checked);

                if (string.IsNullOrEmpty(qrCode))
                {
                    LogStatus(device, "set2fa2 - Can not copy QR code ----------");

                    Device.TapByPercentDelay(deviceID, 38.4, 46.3);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.9, 74.5);


                    Thread.Sleep(1000);
                    qrCode = Device.ReadClipboardAndroid(deviceID, fbLiteCheckbox.Checked);
                    if (string.IsNullOrEmpty(qrCode))
                    {
                        Thread.Sleep(2000);
                        return "";
                    }
                }
                var base32Bytes = Base32Encoding.ToBytes(qrCode);

                var otp = new Totp(base32Bytes);
                string token = otp.ComputeTotp();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return "";
                }
                Thread.Sleep(2000);
                Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                Thread.Sleep(500);

                Thread.Sleep(500);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 40.6, 29.5);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 34.7, 30.3);
                Thread.Sleep(500);
                Utility.InputConfirmCode(deviceID, token);

                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.3, 95.2); // Hạ bàn phím
                Thread.Sleep(500);
                Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                Device.TapByPercentDelay(deviceID, 54.0, 90.7);
                Thread.Sleep(500);

                Device.TapByPercentDelay(deviceID, 50.6, 95.3);
                Device.TapByPercentDelay(deviceID, 54.0, 90.7);
                LogStatus(device, "set2fa2 - " + qrCode);
                Thread.Sleep(2000);
                if (CheckTextExist(deviceID, "textnhậpmậtkhẩuresourcei"))
                {
                    LogStatus(device, "Nhâp mật khẩu set2fa web");
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.9, 23.9);
                    Thread.Sleep(1000);
                    InputText(deviceID, password, false);
                    Thread.Sleep(1000);
                    if (!WaitAndTapXML(deviceID, 3, "texttiếptụcresourceid"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.8, 34.8);
                    }
                    Thread.Sleep(10000);
                    WaitAndTapXML(deviceID, 1, "xong");
                }
                Thread.Sleep(5000);
                string ddd = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "đang bật", 1, ddd) || CheckTextExist(deviceID, "mãcủabạn", 1, ddd)
                    || CheckTextExist(deviceID, "mãkhôiphục", 1, ddd))
                {
                    return qrCode;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
        public string Set2faWeb(string deviceID, string password)
        {
            try
            {
            SET_2FA:
                Device.GotoFbSettingWeb(deviceID);
                if (CheckTextExist(deviceID, "mật khẩu", 5))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 40.4, 23.3); // textbox
                    InputText(deviceID, password, false);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 30.3);
                    Thread.Sleep(4000);
                    if (CheckTextExist(deviceID, "xácthực2yếutốđangbật", 3))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.1, 39.5); // tắt 2fa cũ 
                        Thread.Sleep(3000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.9, 62.0); // tắt 2fa

                        Thread.Sleep(3000);

                    }
                }
                if (!CheckTextExist(deviceID, "xác thực", 40))
                {
                    return "";
                }

                Thread.Sleep(5000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.7, 51.4); // Click dùng ứng dụng xác thực
                Thread.Sleep(2000);
                if (CheckTextExist(deviceID, "mật khẩu", 1))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 40.4, 23.3); // textbox
                    InputText(deviceID, password, false);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 30.3);

                }
                if (!CheckTextExist(deviceID, "xác thực", 10))
                {
                    return "";
                }

                Random random2 = new Random();
                int num7 = random2.Next(0, 99999);
                Bitmap bitmap = KAutoHelper.ADBHelper.ScreenShoot(deviceID, true, "screenShoot.png");
                bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + "QR" + num7.ToString() + ".PNG");
                Image image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "QR" + num7.ToString() + ".PNG");
                QRCodeDecoder qrcodeDecoder = new QRCodeDecoder();
                string input2 = qrcodeDecoder.Decode(new QRCodeBitmapImage(image as Bitmap));
                string text13 = Regex.Match(input2, "secret=[0-9a-zA-Z_.%-]{0,}").ToString();
                string QrCode = text13.Replace("secret=", "");

                image.Dispose();
                Image image2 = Image.FromFile("QR" + num7.ToString() + ".PNG");
                image2.Dispose();
                File.Delete("QR" + num7.ToString() + ".PNG");
                var base32Bytes = Base32Encoding.ToBytes(QrCode);

                var otp = new Totp(base32Bytes);
                string token = otp.ComputeTotp();

                if (string.IsNullOrWhiteSpace(token))
                {
                    return "";
                }

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 99.1); // Next
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 43.1, 44.6); // textbox

                InputText(deviceID, token, false);
                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 54.8);

                Thread.Sleep(4000);
                if (CheckTextExist(deviceID, "xác thực", 5))
                {
                    if (CheckTextExist(deviceID, "xácthực2yếutốđangbật", 1))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.3, 61.0);

                        return QrCode;
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
        public void CheckLogin(DeviceObject device, OrderObject order)
        {
            string deviceID = device.deviceId;
            FbUtil.OpenFacebookAppVerify(device);
            Account acc = FbUtil.GetAccCheckLogin();
            LogStatus(device, "Check login");
            if (acc != null)
            {
                LogStatus(device, "Check acc:" + acc.uid + " pass:" + acc.pass);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.8, 45.6); // username
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 30.4, 45.8);
                Thread.Sleep(1000);
                Utility.InputVietVNIText(deviceID, acc.uid);
                Utility.WaitAndTapXML(deviceID, 4, "khẩucheckable"); // Mật khẩu

                Utility.InputText(deviceID, acc.pass, false);

                Thread.Sleep(500);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.9, 44.4);
                Thread.Sleep(2000);// wait login
                                   // Check 2fa
                if (Utility.CheckTextExist(deviceID, "cáº§ncã³mã£ä‘äƒng"))
                {
                    WaitAndTapXML(deviceID, 2, "okresource"); // Have 2fa
                    if (!string.IsNullOrEmpty(acc.qrCode))
                    {
                        // Get code
                        //RequestHTTP httpRequest = new RequestHTTP();
                        //string faCode = httpRequest.Request("GET", "http://2fa.live/tok/" + acc.qrCode, null).ToString();
                        //string token = Regex.Match(faCode, ":\"(.*?)\"}$").Groups[1].ToString();
                        //Thread.Sleep(2000);
                        var base32Bytes = Base32Encoding.ToBytes(acc.qrCode);

                        var otp = new Totp(base32Bytes);
                        string token = otp.ComputeTotp();
                        Thread.Sleep(2000);
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 13.4, 39.8);
                        Thread.Sleep(1000);
                        Utility.InputVietVNIText(deviceID, token);

                        WaitAndTapXML(deviceID, 3, Language.Continue2Fa());
                    }
                    else
                    {
                        LogStatus(device, "Acc:" + acc.uid + " -pass:" + acc.pass + " -- missing 2fa code");
                    }
                }
                else
                {
                    // Acc don't have 2fa
                    if (order.has2Fa)
                    {
                        //goto SET_2FA;
                    }
                }
                Thread.Sleep(10000);
                // Store infor login
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 10.0, 96.6);

                Thread.Sleep(2000);// Wait to go to home screen;

                if (CheckLock(device, deviceID))
                {

                    fail++;
                    device.blockCount++;
                    device.isBlocking = true;
                    dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Orange;
                    WriteFileLog(acc.note, "Acc_Login_die.txt");
                    return;
                }


                WriteFileLog(acc.note, "Acc_Logined.txt");

                LogStatus(device, "Add status");
                FbUtil.PostStatus(deviceID);
                LogStatus(device, "Add more friend");
                FbUtil.AddFriend(deviceID);
                device.isBlocking = false;
                device.blockCount = 0;

                order.isSuccess = true;
                dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Constant.green3;
                return;
            }
        }
        public bool CheckLock(DeviceObject device, string deviceID, string xml = "")
        {
            Utility.Log("Check account lock", status);
            if (CheckTextExist(deviceID, new[] { "chúng tôi cần thêm thông tin", Language.AccountDisabled(), "somethingwentwrong", "tin nhắn văn bản" }))
            {
                LogStatus(device, "checklock - Acc locked");
                Thread.Sleep(1000);
                return true;
            }

            return false;
        }

        public void LogoutFromLock(string deviceID)
        {
            //Thread.Sleep(1000);
            //Device.TapByPercentDelay(deviceID, 50.9, 39.9);
            //Device.TapByPercentDelay(deviceID, 51.1, 54.9);

            //Thread.Sleep(3000);
            //Device.TapByPercentDelay(deviceID, 50.9, 36.6);
            //Device.TapDelay(deviceID, 1163, 1600);
            //Thread.Sleep(3000);

            //if (!Utility.CheckTextExist(deviceID, "â€¢"))
            //{

            //    Device.ClearCache(deviceID, "com.facebook.katana");
            //    return;
            //}
        }


        public string InputNameFbLite(OrderObject order, string deviceID, string gender)
        {
            try
            {
                Thread.Sleep(1000);

                string lastname = Utility.GetLastName(order.language, gender, name3wordcheckBox.Checked);
                string name = Utility.GetFirtName(order.language, gender);

                Thread.Sleep(1000);
                Utility.Log("Text: Name:" + name, status);

                Utility.InputVietVNIText(deviceID, name);
                Thread.Sleep(2000);

                Device.TabPress(deviceID);
                Utility.Log("Input text: lastname:" + lastname, status);

                Utility.InputVietVNIText(deviceID, lastname);
                Thread.Sleep(2000);

                Device.TapByPercentDelay(deviceID, 47.6, 35.0);
                Thread.Sleep(300);
                if (Utility.CheckTextExist(deviceID, "Birthday"))
                {
                    return name + "|" + lastname;
                }
                else
                {
                    Utility.StoreErrorName(name);
                }
                ScreenNameSuggestion(deviceID);
                if (Utility.CheckTextExist(deviceID, "what their friends call them"))
                {
                    Utility.StoreErrorName(name);
                    string[] aa = Utility.GetCordText(deviceID, "edittext");
                    if (aa == null)
                    {
                        fail++;
                        return Constant.FAIL;
                    }
                    Device.TapDelay(deviceID, Convert.ToInt32(aa[2]) - 10, Convert.ToInt32(aa[3]) - 10);
                    name = Utility.GetFirtName(order.language, gender);

                    Utility.InputVietVNIText(deviceID, name);

                    Device.TapByPercentDelay(deviceID, 47.6, 35.0);
                    if (Utility.CheckTextExist(deviceID, "what their friends call them"))
                    {
                        fail++;
                        return Constant.FAIL;
                    }
                }
                ScreenNameSuggestion(deviceID);
                return name + "|" + lastname;
            }
            catch (Exception e)
            {
                Utility.Log(e.Message, status);
                return Constant.FAIL;
            }
        }

        public string InputNameLiteLD(OrderObject order, string deviceID, string gender)
        {
            try
            {
                Thread.Sleep(2000);
                Utility.WaitAndTapXML(deviceID, 1, "NONE OF THE ABOVE");
                string lastname = Utility.GetLastName(order.language, gender, name3wordcheckBox.Checked);
                string name = Utility.GetFirtName(order.language, gender);

                Thread.Sleep(1000);
                Utility.Log("Text: Name:" + name, status);

                Utility.InputUsText(deviceID, name, inputStringCheckbox.Checked);
                Thread.Sleep(2000);

                Device.TabPress(deviceID);
                Utility.Log("Input text: lastname:" + lastname, status);

                Utility.InputUsText(deviceID, lastname, inputStringCheckbox.Checked);
                Thread.Sleep(2000);

                Device.TapByPercentDelay(deviceID, 49.1, 40.8); // Next button

                return name + "|" + lastname;
            }
            catch (Exception e)
            {
                Utility.Log(e.Message, status);
                return Constant.FAIL;
            }
        }

        public string InputName(DeviceObject device, OrderObject order, string deviceID, string gender)
        {
            try
            {

                string lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);

                string firstname = GetFirtName(order.language, gender, nameVnUscheckBox.Checked);

                if (order.language == Constant.LANGUAGE_VN && !nameVnUscheckBox.Checked)
                {
                    if (Ten_Nam != null && Ten_Nu != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = Ten_Nam.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = Ten_Nu.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }
                else
                {
                    lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    if (FemaleName != null && MaleName != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = FemaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }

                NameObject nnn = ServerApi.GetNameLocalCache(serverCacheMailTextbox.Text, gender, order.language);
                if (nnn != null && !string.IsNullOrEmpty(nnn.name))
                {
                    firstname = nnn.name;
                    lastname = nnn.lastname;
                }

                LogStatus(device, "name:" + lastname + " " + firstname);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 39.2, 31.7);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 36.9, 39.5);
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                if (order.language == Constant.LANGUAGE_VN)
                {
                    InputVietVNIText(deviceID, lastname);
                }
                else
                {
                    InputText(deviceID, lastname, false);
                }

                Thread.Sleep(1300);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 57.0, 31.4);
                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 57.0, 31.4);
                Thread.Sleep(1500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 86.7, 39.3);
                Device.DeleteAllChars(deviceID);
                Thread.Sleep(1000);
                if (order.language == Constant.LANGUAGE_VN)
                {
                    InputVietVNIText(deviceID, firstname);
                }
                else
                {
                    InputText(deviceID, firstname, false);
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.8, 43.2);

                Thread.Sleep(1100);
                if (Utility.CheckTextExist(deviceID, Language.Birthday()))
                {
                    return firstname + "|" + lastname;
                }
                else
                {
                    lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);

                    firstname = GetFirtName(order.language, gender, nameVnUscheckBox.Checked);

                    if (order.language == Constant.LANGUAGE_VN && !nameVnUscheckBox.Checked)
                    {
                        if (Ten_Nam != null && Ten_Nu != null)
                        {
                            if (gender == Constant.MALE)
                            {
                                firstname = Ten_Nam.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            }
                            else
                            {
                                firstname = Ten_Nu.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            }
                        }
                    }
                    else
                    {
                        lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        if (FemaleName != null && MaleName != null)
                        {
                            if (gender == Constant.MALE)
                            {
                                firstname = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            }
                            else
                            {
                                firstname = FemaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            }
                        }
                    }

                    Thread.Sleep(1000);
                    if (order.language == Constant.LANGUAGE_VN)
                    {
                        InputVietVNIText(deviceID, lastname);
                    }
                    else
                    {
                        InputText(deviceID, lastname, false);
                    }

                    Thread.Sleep(1300);

                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.9, 29.6); // xóa tên
                    Thread.Sleep(1000);
                    if (order.language == Constant.LANGUAGE_VN)
                    {
                        InputVietVNIText(deviceID, firstname);
                    }
                    else
                    {
                        InputText(deviceID, firstname, false);
                    }
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.8, 43.2);

                    Thread.Sleep(1100);
                    if (Utility.CheckTextExist(deviceID, Language.Birthday()))
                    {
                        return firstname + "|" + lastname;
                    }
                    else
                    {
                        return "";
                    }
                }

                return "";
            }
            catch (Exception e)
            {
                Utility.Log(e.Message, status);
                return Constant.FAIL;
            }
        }
        public string InputNameNewUS(DeviceObject device, OrderObject order, string deviceID, string gender)
        {
            try
            {
                string middleName = GetMidleName(order.language, gender);
                string lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);

                string firstname = GetFirtName(order.language, gender, nameVnUscheckBox.Checked);


                if (lastname.Contains("Ð") || lastname.Contains("đ") || ConvertToUnsign(lastname).ToLower().StartsWith("d"))
                {
                    lastname = GetLastName(Constant.LANGUAGE_VN, gender, name3wordcheckBox.Checked);
                }

                if (firstname.Contains("Ð") || firstname.Contains("đ") || ConvertToUnsign(firstname).ToLower().StartsWith("d"))
                {
                    firstname = GetFirtName(Constant.LANGUAGE_VN, gender, nameVnUscheckBox.Checked);
                }

                if (middleName.Contains("Ð") || middleName.Contains("đ") || ConvertToUnsign(middleName).ToLower().StartsWith("d"))
                {
                    middleName = GetFirtName(Constant.LANGUAGE_VN, gender, nameVnUscheckBox.Checked);
                }
                if (order.language == Constant.LANGUAGE_VN && !nameVnUscheckBox.Checked)
                {
                    if (Ten_Nam != null && Ten_Nu != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = Ten_Nam.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = Ten_Nu.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }
                else
                {
                    if (name3wordcheckBox.Checked)
                    {
                        lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault() + " " + middleName;
                    }
                    else
                    {
                        lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    }

                    if (FemaleName != null && MaleName != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = FemaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }

                NameObject nnn = ServerApi.GetNameLocalCache(serverCacheMailTextbox.Text, gender, order.language);
                if (nnn != null && !string.IsNullOrEmpty(nnn.name))
                {
                    firstname = nnn.name;
                    lastname = nnn.lastname;
                }
                LogStatus(device, "name:" + lastname + " " + firstname);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 45.4, 26.5); // tab họ
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                if (order.language == Constant.LANGUAGE_VN)
                {
                    InputVietVNIText(deviceID, lastname);
                }
                else
                {
                    InputText(deviceID, lastname, false);
                }

                //Thread.Sleep(1300);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 26.2);
                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 26.2);
                //Thread.Sleep(1500);
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                if (order.language == Constant.LANGUAGE_VN)
                {
                    InputVietVNIText(deviceID, firstname);
                }
                else
                {
                    InputText(deviceID, firstname, false);
                }
                //if (!WaitAndTapXML(deviceID, 1, "tiếp"))
                //{
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 35.2);
                //}

                if (!WaitAndTapXML(deviceID, 3, "nextcheckable"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.8, 36.3);
                }


                for (int i = 0; i < 7; i++)
                {

                    if (CheckTextExist(deviceID, new string[] { "setdate", "đặtngày" }))
                    {
                        return firstname + "|" + lastname;
                    }
                }

                if (CheckTextExist(deviceID, "select your name", 1))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.9, 31.5);
                    Thread.Sleep(2000);
                    WaitAndTapXML(deviceID, 2, "nextcheckable");
                    Thread.Sleep(3000);
                } else if (CheckTextExist(deviceID, "in everyday"))
                {
                    
                    LogStatus(device, "Tên không thường dùng - đổi tên khác", 500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 33.0, 26.6); // tap Họ
                    Thread.Sleep(500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.2, 25.5); // xóa
                    lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);
                    firstname = GetFirtName(order.language, gender, name3wordcheckBox.Checked);
                    Thread.Sleep(1000);
                    InputVietVNIText(deviceID, lastname);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 85.1, 25.7); // tap tên
                    Thread.Sleep(500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.4, 25.0); // xóa tên
                    InputVietVNIText(deviceID, firstname);
                    if (!WaitAndTapXML(deviceID, 3, "nextcheckable"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.8, 36.3);

                    }
                }


                if (!CheckTextExist(deviceID, "setdate", 5))
                {
                    if (!WaitAndTapXML(deviceID, 3, "tiếpcheckable"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.8, 36.3);
                    }
                    return Constant.FAIL;
                }


                return "";
            }
            catch (Exception e)
            {
                Utility.Log(e.Message, status);
                return Constant.FAIL;
            }
        }
        public string InputNameNew(DeviceObject device, OrderObject order, string deviceID, string gender)
        {
            try
            {
                //if (!CheckTextExist(deviceID, "bạntêngì"))
                //{
                //    return Constant.FAIL;
                //}
                string middleName = GetMidleName(order.language, gender);
                string lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);

                string firstname = GetFirtName(order.language, gender, nameVnUscheckBox.Checked);


                if (lastname.Contains("Ð") || lastname.Contains("đ") || ConvertToUnsign(lastname).ToLower().StartsWith("d"))
                {
                    lastname = GetLastName(Constant.LANGUAGE_VN, gender, name3wordcheckBox.Checked);
                }

                if (firstname.Contains("Ð") || firstname.Contains("đ") || ConvertToUnsign(firstname).ToLower().StartsWith("d"))
                {
                    firstname = GetFirtName(Constant.LANGUAGE_VN, gender, nameVnUscheckBox.Checked);
                }

                if (middleName.Contains("Ð") || middleName.Contains("đ") || ConvertToUnsign(middleName).ToLower().StartsWith("d"))
                {
                    middleName = GetFirtName(Constant.LANGUAGE_VN, gender, nameVnUscheckBox.Checked);
                }
                if (order.language == Constant.LANGUAGE_VN && !nameVnUscheckBox.Checked)
                {
                    if (Ten_Nam != null && Ten_Nu != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = Ten_Nam.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = Ten_Nu.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }
                else
                {
                    if (name3wordcheckBox.Checked)
                    {
                        lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault() + " " + middleName;
                    }
                    else
                    {
                        lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    }

                    if (FemaleName != null && MaleName != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = FemaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }
                NameObject nnn = ServerApi.GetNameLocalCache(serverCacheMailTextbox.Text, gender, order.language);
                if (nnn != null && !string.IsNullOrEmpty(nnn.name))
                {
                    firstname = nnn.name;
                    lastname = nnn.lastname;
                }

                LogStatus(device, "name:" + lastname + " " + firstname);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 45.4, 26.5); // tab họ
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                if (order.language == Constant.LANGUAGE_VN)
                {
                    InputVietVNIText(deviceID, lastname);
                }
                else
                {
                    InputText(deviceID, lastname, false);
                }

                //Thread.Sleep(1300);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 26.2);
                Thread.Sleep(500);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 26.2);
                //Thread.Sleep(1500);
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                if (order.language == Constant.LANGUAGE_VN)
                {
                    InputVietVNIText(deviceID, firstname);
                }
                else
                {
                    InputText(deviceID, firstname, false);
                }
                //if (!WaitAndTapXML(deviceID, 1, "tiếp"))
                //{
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 53.7, 35.2);
                //}

                if (!WaitAndTapXML(deviceID, 3, "tiếpcheckable"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.8, 36.3);
                }


                for (int i = 0; i < 7; i++)
                {

                    if (CheckTextExist(deviceID, new string[] { "setdate", "đặtngày" }))
                    {
                        return firstname + "|" + lastname;
                    }
                }


                //Thread.Sleep(4100);
                if (CheckTextExist(deviceID, "chọntêncủabạn"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.9, 31.5);
                    Thread.Sleep(2000);
                    WaitAndTapXML(deviceID, 2, "tiếpcheckable");
                    Thread.Sleep(3000);
                }
                else
                {
                    LogStatus(device, "Tên không thường dùng - đổi tên khác", 500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 33.0, 26.6); // tap Họ
                    Thread.Sleep(500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 42.2, 25.5); // xóa
                    lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);
                    firstname = GetFirtName(order.language, gender, name3wordcheckBox.Checked);
                    Thread.Sleep(1000);
                    InputVietVNIText(deviceID, lastname);
                    Thread.Sleep(1000);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 85.1, 25.7); // tap tên
                    Thread.Sleep(500);
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.4, 25.0); // xóa tên
                    InputVietVNIText(deviceID, firstname);
                    if (!WaitAndTapXML(deviceID, 3, "tiếpcheckable"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.8, 36.3);

                    }
                }


                if (!CheckTextExist(deviceID, "setdate", 5))
                {
                    if (!WaitAndTapXML(deviceID, 3, "tiếpcheckable"))
                    {
                        KAutoHelper.ADBHelper.TapByPercent(deviceID, 68.8, 36.3);
                    }
                    return Constant.FAIL;
                }


                return "";
            }
            catch (Exception e)
            {
                Utility.Log(e.Message, status);
                return Constant.FAIL;
            }
        }
        public string InputName2(DeviceObject device, OrderObject order, string deviceID, string gender)
        {
            try
            {
                Thread.Sleep(1000);
                string lastname = GetLastName(order.language, gender, name3wordcheckBox.Checked);

                string firstname = GetFirtName(order.language, gender);

                if (vietCheckbox.Checked)
                {
                    if (order.language == Constant.LANGUAGE_VN)
                    {
                        if (Ten_Nam != null && Ten_Nu != null)
                        {
                            if (gender == Constant.MALE)
                            {
                                firstname = Ten_Nam.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            }
                            else
                            {
                                firstname = Ten_Nu.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                            }
                        }
                    }
                }
                else
                {
                    lastname = LastName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                    if (FemaleName != null && MaleName != null)
                    {
                        if (gender == Constant.MALE)
                        {
                            firstname = MaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                        else
                        {
                            firstname = FemaleName.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                        }
                    }
                }
                Device.SelectAdbKeyboard(deviceID);
                LogStatus(device, "name:" + lastname + " " + firstname);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 24.5, 30.0);
                Thread.Sleep(1000);
                Device.InputStringAdbKeyboard(deviceID, lastname);
                //Thread.Sleep(1000);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 66.5, 28.6);
                Thread.Sleep(1000);
                Device.InputStringAdbKeyboard(deviceID, firstname);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.5, 41.0);

                Thread.Sleep(300);
                Device.StopAdbKeyboard(deviceID);
                if (Utility.CheckTextExist(deviceID, Language.Birthday()))
                {
                    return firstname + "|" + lastname;
                }

                Utility.StoreErrorName(firstname + "|" + lastname);
                return firstname + "|" + lastname;
            }
            catch (Exception e)
            {
                Utility.Log(e.Message, status);
                return Constant.FAIL;
            }
        }
        public bool InputBirthdayFbLiteLD(string deviceID)
        {
            try
            {
                Random random = new Random();
                int month = random.Next(1, 12);
                Utility.InputVietVNIText(deviceID, month + "");
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 16.1, 34.8); // date
                int date = random.Next(1, 28);
                Utility.InputVietVNIText(deviceID, date + "");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 28.7, 35.6); // year
                int year = random.Next(1, 9);
                Utility.InputVietVNIText(deviceID, year + "");
                NumberKeyboardLD(deviceID, 1);
                NumberKeyboardLD(deviceID, 9);
                NumberKeyboardLD(deviceID, 9);
                NumberKeyboardLD(deviceID, year);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 42.4); // next
                //Device.TapByPercentDelay(deviceID, 21.2, 29.1);
                //Thread.Sleep(1000);
                //Random ran = new Random();
                //string year = ran.Next(20, 35) + "";
                ////Utility.InputVietText(deviceID, year, false);
                //Device.TapByPercentDelay(deviceID, 15.2, 64.2);
                //Thread.Sleep(300);
                //Device.TapByPercentDelay(deviceID, 82.4, 80.5);
                //Thread.Sleep(300);
                //Device.TapByPercentDelay(deviceID, 82.4, 80.5);
                //Thread.Sleep(300);
                //Device.TapByPercentDelay(deviceID, 82.4, 80.5);
                //Thread.Sleep(1000);
                //Device.TapByPercentDelay(deviceID, 50.5, 36.2);
                //Thread.Sleep(1000);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }
        public void NumberKeyboardLD(string deviceID, int number)
        {
            switch (number)
            {
                case 0:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.2, 94.8);
                    break;
                case 1:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.8, 66.7);
                    break;
                case 2:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.1, 66.6);
                    break;
                case 3:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 83.9, 67.6);
                    break;
                case 4:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.3, 76.5);
                    break;
                case 5:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.6, 76.1);
                    break;
                case 6:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.7, 76.4);
                    break;
                case 7:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 16.5, 85.9);
                    break;
                case 8:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.8, 85.6);
                    break;
                case 9:
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.1, 84.8);
                    break;


            }
        }
        public bool InputBirthdayFbLite(string deviceID)
        {
            try
            {
                Random random = new Random();
                Device.TapByPercentDelay(deviceID, 21.2, 29.1);
                Thread.Sleep(1000);
                Random ran = new Random();
                string year = ran.Next(20, 35) + "";
                //Utility.InputVietText(deviceID, year, false);
                Device.TapByPercentDelay(deviceID, 15.2, 64.2);
                Thread.Sleep(300);
                Device.TapByPercentDelay(deviceID, 82.4, 80.5);
                Thread.Sleep(300);
                Device.TapByPercentDelay(deviceID, 82.4, 80.5);
                Thread.Sleep(300);
                Device.TapByPercentDelay(deviceID, 82.4, 80.5);
                Thread.Sleep(1000);
                Device.TapByPercentDelay(deviceID, 50.5, 36.2);
                Thread.Sleep(1000);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }

        public bool InputBirthdayA30(string deviceID, int yearOld)
        {
            try
            {
                //Next(deviceID);
                int baseY;
                int baseX;
                string[] yearCord = Utility.GetCordText(deviceID, "2textresourceidclassandroidwidgetNumber");
                string[] monthCord = Utility.GetCordText(deviceID, "1textresourceidclassandroidwidgetNumber");
                if (yearCord == null)
                {
                    return false;
                }
                baseY = (Convert.ToInt32(yearCord[1]) + Convert.ToInt32(yearCord[3])) / 2;
                int center = (Convert.ToInt32(monthCord[0]) + Convert.ToInt32(monthCord[2])) / 2;

                baseX = center - (Convert.ToInt32(monthCord[2]) - Convert.ToInt32(monthCord[0]));

                Random random = new Random();
                int randomMonth = random.Next(500);

                Random n = new Random();

                Device.Swipe(deviceID, baseX, baseY, baseX, baseY + baseX + randomMonth); // Day

                Device.Swipe(deviceID, Convert.ToInt32(monthCord[0]), baseY, Convert.ToInt32(monthCord[0]), baseY + Convert.ToInt32(monthCord[0]) + randomMonth); // Month
                Device.Swipe(deviceID, Convert.ToInt32(yearCord[0]), baseY, Convert.ToInt32(yearCord[0]), baseY + Convert.ToInt32(yearCord[0]) + randomMonth); // Year
                Device.Swipe(deviceID, Convert.ToInt32(yearCord[0]), baseY, Convert.ToInt32(yearCord[0]), baseY + Convert.ToInt32(yearCord[0]) + randomMonth); // Year
                Device.Swipe(deviceID, Convert.ToInt32(yearCord[0]), baseY, Convert.ToInt32(yearCord[0]), baseY + Convert.ToInt32(yearCord[0]) + randomMonth); // Year
                Device.Swipe(deviceID, Convert.ToInt32(yearCord[0]), baseY, Convert.ToInt32(yearCord[0]), baseY + Convert.ToInt32(yearCord[0]) + randomMonth); // Year
                Device.Swipe(deviceID, Convert.ToInt32(yearCord[0]), baseY, Convert.ToInt32(yearCord[0]), baseY + Convert.ToInt32(yearCord[0]) + randomMonth); // Year

                Next(deviceID);
                Thread.Sleep(1000);
                if (Utility.CheckTextExist(deviceID, Language.Birthday()))
                {
                    Next(deviceID);
                    Thread.Sleep(1000);
                    Random ran = new Random();
                    string year = ran.Next(20, 35) + "";
                    Utility.InputVietVNIText(deviceID, year);
                    Next(deviceID);
                    Thread.Sleep(1000);
                    WaitAndTapXML(deviceID, 2, "Okresource");

                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }

        public bool InputBirthdayLD(string deviceID)
        {
            try
            {
                int baseY;

                string[] yearCord = Utility.GetCordText(deviceID, "2textresourceidclassandroidwidgetNumber");
                if (yearCord == null || yearCord.Length < 4)
                {
                    return false;
                }
                baseY = (Convert.ToInt32(yearCord[1]) + Convert.ToInt32(yearCord[3])) / 2;
                Random random = new Random();
                int randomSwipe = random.Next(200, 250);

                Random n = new Random();
                Device.Swipe(deviceID, 167, baseY, 167, baseY + randomSwipe);// ngay
                Thread.Sleep(1000);
                Device.Swipe(deviceID, 270, baseY, 270, baseY + randomSwipe); // thang

                Device.Swipe(deviceID, 370, baseY, 370, baseY + randomSwipe); // nam
                Device.Swipe(deviceID, 370, baseY, 370, baseY + randomSwipe);
                Device.Swipe(deviceID, 370, baseY, 370, baseY + randomSwipe);
                Device.Swipe(deviceID, 370, baseY, 370, baseY + randomSwipe);
                Device.Swipe(deviceID, 370, baseY, 370, baseY + randomSwipe);
                Thread.Sleep(1000);

                Next(deviceID);
                Thread.Sleep(1000);
                if (Utility.CheckTextExist(deviceID, "Birthday"))
                {
                    Next(deviceID);
                    Thread.Sleep(1000);
                    Random ran = new Random();
                    string year = ran.Next(20, 35) + "";
                    Utility.InputVietVNIText(deviceID, year);
                    Next(deviceID);
                    Thread.Sleep(1000);
                    WaitAndTapXML(deviceID, 2, "Okresource");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }
        public bool InputBirthday(string deviceID, string selectedDeviceName, int yearOld)
        {

            if (selectedDeviceName == "a30" || selectedDeviceName == "a7Cook")
            {
                return InputBirthdayA30(deviceID, yearOld);
            }
            return InputBirthdayS7(deviceID);

        }
        public bool InputBirthday2(string deviceID)
        {
            try
            {
                int baseY;
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.8, 35.2);

                baseY = (761 + 1391) / 2;
                Random random = new Random();
                int randomSwipe = random.Next(500);

                Random n = new Random();
                Device.Swipe(deviceID, 496, baseY, 496, baseY + 496 + randomSwipe);// Month
                //Thread.Sleep(1000);
                Device.Swipe(deviceID, 720, baseY, 720, baseY + 496 + randomSwipe); // Day

                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);

                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 69.3, 68.8);
                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 71.8, 45.0);
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }

        public bool InputBirthdayS7(string deviceID)
        {
            try
            {
                int baseY;
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.8, 35.2);

                baseY = (761 + 1391) / 2;
                Random random = new Random();
                int randomSwipe = random.Next(500);

                Random n = new Random();
                Device.Swipe(deviceID, 496, baseY, 496, baseY + 496 + randomSwipe);// Month

                Device.Swipe(deviceID, 720, baseY, 720, baseY + 496 + randomSwipe); // Day

                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.5, 70.2);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }

        public bool InputBirthdayNewUS(string deviceID)
        {
            try
            {
                //CheckTextExist(deviceID, "setdate", 7);
                //{
                //    return false;
                //}
                Thread.Sleep(1000);
                int baseY;
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 29.6, 49.9);

                baseY = (761 + 1391) / 2;
                Random random = new Random();
                int randomSwipe = random.Next(200, 500);

                Random n = new Random();
                Device.Swipe(deviceID, 496, baseY, 496, baseY + 496 + randomSwipe);// Month
                randomSwipe = random.Next(400, 500);
                Device.Swipe(deviceID, 720, baseY, 720, baseY + 496 + randomSwipe); // Day
                randomSwipe = random.Next(200, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                randomSwipe = random.Next(300, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                randomSwipe = random.Next(340, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                randomSwipe = random.Next(360, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 70.4, 69.0); // set
                if (!WaitAndTapXML(deviceID, 2, "nextcheckable"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 61.5, 41.1);// tiếp
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }
        public bool InputBirthdayNew(string deviceID)
        {
            try
            {
                //CheckTextExist(deviceID, "setdate", 7);
                //{
                //    return false;
                //}
                Thread.Sleep(1000);
                int baseY;
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 29.6, 49.9);

                baseY = (761 + 1391) / 2;
                Random random = new Random();
                int randomSwipe = random.Next(200, 500);

                Random n = new Random();
                Device.Swipe(deviceID, 496, baseY, 496, baseY + 496 + randomSwipe);// Month
                randomSwipe = random.Next(400, 500);
                Device.Swipe(deviceID, 720, baseY, 720, baseY + 496 + randomSwipe); // Day
                randomSwipe = random.Next(200, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                randomSwipe = random.Next(300, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                randomSwipe = random.Next(340, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);
                randomSwipe = random.Next(360, 500);
                Device.Swipe(deviceID, 952, baseY, 952, baseY + 496 + randomSwipe);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 70.4, 69.0); // set
                if (!WaitAndTapXML(deviceID, 2, "tiếpcheckable"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 61.5, 41.1);// tiếp
                }


                //Thread.Sleep(1000);
                //KAutoHelper.ADBHelper.TapByPercent(deviceID, 61.5, 41.1);// tiếp
                //Thread.Sleep(4000);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }


        public bool InputBirthday(string deviceID)
        {
            try
            {
                int baseY;
                int baseX;
                string[] yearCord = Utility.GetCordText(deviceID, "2textresourceidclassandroidwidgetNumber");
                string[] dayCord = Utility.GetCordText(deviceID, "1textresourceidclassandroidwidgetNumber");
                if (yearCord == null)
                {
                    return false;
                }
                baseY = (Convert.ToInt32(yearCord[1]) + Convert.ToInt32(yearCord[3])) / 2;
                int centerDay = (Convert.ToInt32(dayCord[0]) + Convert.ToInt32(dayCord[2])) / 2;

                baseX = centerDay - (Convert.ToInt32(dayCord[2]) - Convert.ToInt32(dayCord[0]));

                Random random = new Random();
                int randomMonth = random.Next(500);

                Random n = new Random();
                if (Utility.CheckAndTap(deviceID, "2textresourceidclassandroidwidgetNumber")) // year
                {
                    int year = n.Next(Convert.ToInt32(yearOldFrom.Text), Convert.ToInt32(yearOldTo.Text));
                    if (year < 18)
                    {
                        year = n.Next(20, 35);
                    }
                    string yearS = Convert.ToString(year);
                    //Device.InputText(deviceID, yearS);
                    InputVietVNIText(deviceID, yearS);
                    Thread.Sleep(1000);
                }
                if (Utility.CheckAndTap(deviceID, "1textresourceidclassandroidwidgetNumber")) // day
                {
                    string days = Convert.ToString(n.Next(1, 28));
                    //Device.InputText(deviceID, days);
                    InputVietVNIText(deviceID, days);
                    Thread.Sleep(1000);
                }
                Device.Swipe(deviceID, baseX, baseY, baseX, baseY + baseX + randomMonth);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                fail++;
                return false;
            }
        }

        public void InputGender(DeviceObject device, string gender)
        {
            string deviceID = device.deviceId;
            LogStatus(device, "Input gender " + gender);
            if (gender == Constant.FEMALE)
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.4, 34.6);

            }
            else
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 41.6);
            }
        }

        public bool InputGenderNewUS(DeviceObject device, string gender)
        {
            if (!CheckTextExist(device.deviceId, "your gender", 8))
            {
                return false;
            }
            string deviceID = device.deviceId;
            LogStatus(device, "Input gender " + gender);
            if (gender == Constant.FEMALE)
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.2, 28.1);
            }
            else
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.2, 35.7);
            }
            if (!WaitAndTapXML(deviceID, 2, "nextcheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.6, 58.8); // Tiếp
            }
            //Thread.Sleep(1000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.6, 58.8); // Tiếp
            return true;
        }


        public bool InputGenderNew(DeviceObject device, string gender)
        {
            if (!CheckTextExist(device.deviceId, "giớitínhcủabạnlàgì", 8))
            {
                return false;
            }
            string deviceID = device.deviceId;
            LogStatus(device, "Input gender " + gender);
            if (gender == Constant.FEMALE)
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.2, 28.1);
            }
            else
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.2, 35.7);
            }
            if (!WaitAndTapXML(deviceID, 2, "tiếpcheckable"))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.6, 58.8); // Tiếp
            }
            //Thread.Sleep(1000);
            //KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.6, 58.8); // Tiếp
            return true;
        }

        public void InputGenderFbLite(string deviceID, string gender)
        {
            if (gender == Constant.FEMALE)
            {
                Device.TapByPercentDelay(deviceID, 94.6, 27.7);
            }
            else
            {
                Device.TapByPercentDelay(deviceID, 95.0, 33.9);
            }
            Thread.Sleep(3000);
        }

        public void InputGenderFbLiteLD(string deviceID, string gender)
        {
            if (gender == Constant.FEMALE)
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.7, 32.9);
            }
            else
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.8, 39.6);
            }
            Thread.Sleep(3000);
        }

        public void InputGenderLD(string deviceID, string gender)
        {
            if (gender == Constant.FEMALE)
            {
                Device.TapByPercentDelay(deviceID, 92.5, 37.7);
            }
            else
            {
                Device.TapByPercentDelay(deviceID, 92.8, 45.3);
            }
            Thread.Sleep(3000);
        }

        private void NextLD(string deviceID)
        {
            Device.TapByPercent(deviceID, 49.7, 40.5);
        }
        private void Next(string deviceID)
        {
            if (vietCheckbox.Checked)
            {
                Utility.WaitAndTapXML(deviceID, 3, Language.Next());
            }
            else
            {

                if (fbLiteCheckbox.Checked)
                {
                    Utility.WaitAndTapXML(deviceID, 3, "clickabletrue");
                }
                else
                {
                    Utility.WaitAndTapXML(deviceID, 3, "nextresource");
                }

            }

            Thread.Sleep(500);
        }

        private void resetStatus_Click(object sender, EventArgs e)
        {
            fail = 0;
            totalRun = 0;
            regOk = 0;
            noVerified = 0;
            localDate = DateTime.Now;

            var culture = new CultureInfo("en-US");
            string startTime = localDate.ToString(culture);

            label2.Text = "Start:" + startTime;
            reportLabel.Text = "";

            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                DeviceObject device = listDeviceObject[i];
                device.globalSuccess = 0;
                device.globalTotal = 0;
                device.cycle = 0;
                device.successInHour = 0;
                device.totalInHour = 0;
                reportLabel.Text = "";
                totalSucc = 0;
                regNvrOk = 0;
                percent = 0;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                DeviceObject device = listDeviceObject[i];
                Task t = new Task(() => ProcessStopproxy(device));
                t.Start();
            }
        }
        void ProcessStopproxy(DeviceObject device)
        {
            StopProxySuper(device);
        }
            private void downloadAvatarBtn_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Utility.DownloadAvatar(status);
            });
            t.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                listDeviceObject[i].totalInHour = 0;


                listDeviceObject[i].successInHour = 0;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                string mail = GetHotMailApi();
                using (StreamWriter HDD = new StreamWriter("local/hotmail.txt", true))
                {
                    HDD.WriteLine(mail);
                    HDD.Close();
                }
                Thread.Sleep(1000);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> listNewDevices = Device.GetDevices();
            if (listNewDevices != null && listNewDevices.Count > 0)
            {

                // Find new device
                List<String> newDevices = new List<string>();
                for (int i = 0; i < listNewDevices.Count; i++)
                {
                    if (!checkDeviceExist(listNewDevices[i]))
                    {

                        // Add to list devices
                        DeviceObject device = new DeviceObject();
                        device.deviceId = listNewDevices[i];
                        device.status = "Initial";
                        device.isFinish = true;
                        device.runningStatus = Constant.RUNNING;
                        listDeviceObject.Add(device);
                        int index = dataGridView.Rows.Count - 1;
                        dataGridView.Rows.Add();
                        dataGridView.Rows[index].Cells[0].Value = (index + 1) + "";
                        dataGridView.Rows[index].Cells[1].Value = device.deviceId;
                        dataGridView.Rows[index].Cells[2].Value = device.successInHour + "/" + device.totalInHour;
                        dataGridView.Rows[index].Cells[3].Value = device.globalSuccess + "/" + device.globalTotal;
                        dataGridView.Rows[index].Cells[4].Value = device.duration;
                        dataGridView.Rows[index].Cells[5].Value = device.status;
                        dataGridView.Rows[index].Cells[6].Value = true;
                        device.index = index;
                        //Device.SetScreenTimeout(device.deviceId, 10 * 60 * 1000);
                        Task t = new Task(() => Process(listDeviceObject[index]));
                        t.Start();
                    }
                }
            }
        }
        public void LoadDataInit()
        {
            string serverIp = ServerApi.GetServerIp(serverCacheMailTextbox.Text);
            if (!string.IsNullOrEmpty(serverIp) && serverIp != serverPathTextBox.Text)
            {
                serverPathTextBox.Text = serverIp;
            }
            if (serverOnlineCheckBox.Checked)
            {
                ServerApi.SERVER_HOST = serverPathTextBox.Text;
            }
            try
            {
                checkFacebookOpenImage = (Bitmap)Bitmap.FromFile("img/fb_check_open.png");
            }
            catch
            {
                MessageBox.Show("Thiếu file check facebook open");
            }
            // delete all file /folder in Authentiation
            System.IO.DirectoryInfo di = new DirectoryInfo("Authentication");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            runAllBtn.Enabled = false;
            button11.Enabled = false;
            localDate = DateTime.Now;

            string startTime = localDate.ToString(new CultureInfo("en-US"));
            label2.Invoke(new MethodInvoker(() =>
            {
                label2.Text = "Start:" + startTime;
                Text = "Fb Reg - LONGFB.NET" + " - " + startTime;
            }));

            if (listDeviceObject.Count < 1)
            {
                status.Text = "Don't have devices";
                return;
            }
            // Load name
            Ten_Nam = LoadData("data/ten_nam.txt");
            Ten_Nu = LoadData("data/ten_nu.txt");

            //string[] rawName = File.ReadAllLines("data/raw_name.txt").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            //List<string> rawNameList = (List<string>)rawName.Shuffle();
            //List<string> MaleNameRaw = new List<string>();
            //List<string> FemaleNameRaw = new List<string>();
            //for (int i = 0; i < rawNameList.Count; i++)
            //{
            //    string[] temp = rawName[i].Split('\t');
            //    if (temp.Length > 4)
            //    {
            //        if (!MaleNameRaw.Contains(temp[1].Trim()))
            //        {
            //            MaleNameRaw.Add(temp[1].Trim());
            //        }
            //        if (!FemaleNameRaw.Contains(temp[3].Trim()))
            //        {
            //            FemaleNameRaw.Add(temp[3].Trim());
            //        }
            //    }
            //}

            FemaleName = LoadData("data/female_name.txt");
            MaleName = LoadData("data/male_name.txt");
            LastName = LoadData("data/lastname.txt");

            AllName = new List<string>();
            AllName.AddRange(Ten_Nam);
            AllName.AddRange(Ten_Nu);
            AllName.AddRange(FemaleName);
            AllName.AddRange(MaleName);

            contacts = LoadData("data/contact.txt");
            FbUtil.descriptionList = LoadData("data/mo_ta_ban_than.txt");
            FbUtil.cityList = LoadData("data/thanh_pho.txt");
            FbUtil.schoolList = LoadData("data/truong_hoc.txt");
            FbUtil.universityList = LoadData("data/truong_dai_hoc.txt");
            FbUtil.companyList = LoadData("data/cong_ty.txt");
            Utility.bestwishList = LoadData("data/bestwish.txt");
            //string path = @"F:\project\fb_reg\fb_reg\bin\Debug\img\avatar_vn\Nu\100000023121338.jpg";
            //string ppp = changeImage(path);
            LoadAccMoi();
            LoadAvatar();
        }
        public void LoadAccMoi()
        {
            //string[] readText = File.ReadAllLines("data/acc_moi.txt");
            //accMois.Clear();
            //foreach (string s in readText)
            //{
            //    if (!accMois.ContainsKey(s))
            //    {
            //        // Checklive

            //        //string[] temp = s.Split('|');
            //        //if (FbUtil.CheckLiveWall(temp[0]) == Constant.DIE)
            //        //{

            //        //} else
            //        //{
            //        accMois.Add(s, 0);
            //        //}


            //    }
            //}

            //accMoilabel.Invoke(new MethodInvoker(() =>
            //{
            //    accMoilabel.Text = accMois.Count + " acc mồi";
            //}));



        }
        public void LoadAvatar()
        {
            string[] AvatarFilesFemale = Directory.GetFiles(@"img\avatar_vn\Nu");
            string[] AvatarFilesMale = Directory.GetFiles(@"img\avatar_vn\Nam");

            for (int i = 0; i < AvatarFilesFemale.Length; i++)
            {

                string name = Path.GetFileName(AvatarFilesFemale[i]);
                dictAvatarFemalePath.Add(name, AvatarFilesFemale[i]);
            }
            for (int i = 0; i < AvatarFilesMale.Length; i++)
            {

                string name = Path.GetFileName(AvatarFilesMale[i]);
                dictAvatarMalePath.Add(name, AvatarFilesMale[i]);
            }
        }

        public Proxy GetProxyFromServer()
        {
            Proxy proxy = new Proxy();
            for (int i = 0; i < 10; i++)
            {
                proxy = ServerApi.GetProxyFromServer(serverCacheMailTextbox.Text, proxyP1checkBox.Checked, proxy3checkBox.Checked);

                if (proxy == null || string.IsNullOrEmpty(proxy.host))
                {
                    Thread.Sleep(5000);
                    continue;
                }
                proxy.hasProxy = true;

                
                return proxy;
            }

            return null;
        }
        public Account GetAccMoi()
        {
            Account acc = new Account();

            for (int i = 0; i < 10; i++)
            {
                acc = ServerApi.GetAccMoiLocalCache(serverCacheMailTextbox.Text);

                if (acc == null || string.IsNullOrEmpty(acc.uid) || string.IsNullOrEmpty(acc.pass))
                {
                    continue;
                }

                // Checklive
                if (FbUtil.CheckLiveWall(acc.uid) == Constant.DIE)
                {
                    continue;
                }

                accMoilabel.Invoke(new MethodInvoker(() =>
                {
                    accMoilabel.Text = "id:" + acc.uid;
                }));
                return acc;
            }

            return null;
        }
        public bool checkDeviceExist(string deviceID)
        {
            for (int k = 0; k < listDeviceObject.Count; k++)
            {
                if (deviceID == listDeviceObject[k].deviceId)
                {
                    return true;
                }
            }
            return false;
        }

        private void dausotextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GetCodeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mailTextbox.Text))
            {
                codeLabel.Text = "Nhap mail vao>>>";
                return;
            }
            MailObject mail = new MailObject();
            mail.email = mailTextbox.Text.Split('|')[0];
            mail.password = mailTextbox.Text.Split('|')[1];
            codeLabel.Text = "code:" + Utility.GetOtp("", "", mail, 10);
        }

        private void ldPlayerCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void InstallApk_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "apk",
                Filter = "apk files (*.apk)|*.apk",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            int numberOfThread = listDeviceObject.Count;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Start install app: " + openFileDialog1.FileName);
                installApk.Enabled = false;
                installApk.BackColor = Color.Red;
                installApk.Text = "Đang install";
                List<Thread> list = new List<Thread>();
                Task t = new Task(() =>
                {
                    for (int k = 0; k < numberOfThread; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            if (deviceID.ToLower().Contains("offline"))
                            {
                                continue;
                            }
                            string fileName = openFileDialog1.FileName;
                            Thread T3 = new Thread(() => threadInstallApk(deviceID, fileName));
                            T3.IsBackground = true;
                            T3.Start();
                            list.Add(T3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                    }
                    foreach (Thread thread2 in list)
                    {
                        thread2.Join();
                    }
                    MessageBox.Show("Finish install Apk");

                    installApk.Invoke(new MethodInvoker(() =>
                    {
                        installApk.Enabled = true;
                        installApk.BackColor = Color.Green;
                        installApk.Text = "Install Apk";
                    }));

                });
                t.Start();
            }
        }
        static void threadInstallApk(string deviceID, string fileName)
        {
            try
            {
                Device.InstallApp(deviceID, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void threadInstallApkFB(string deviceID, string fileName)
        {
            try
            {
                Device.Uninstall(deviceID, Constant.FACEBOOK_PACKAGE);

                Thread.Sleep(1000);
                Device.InstallApp(deviceID, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void RunCMD(string deviceID, string cmd)
        {
            try
            {
                Device.ExecuteCMD(cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public class AutoClosingMessageBox1
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox1(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void setWifiButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                try
                {
                    for (int k = 0; k < listDeviceObject.Count; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            Device.SetWifi(deviceID, ssidTextBox.Text, wifiPassTextBox.Text);
                            //AutoClosingMessageBox.Show(deviceID + " Set Wifi finish:" + k + "/" + listDeviceObject.Count, "Set Wifi", 1500);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    MessageBox.Show("Set Wifi finish");
                }
                catch (Exception exx)
                {
                    Console.WriteLine(exx.Message);
                }
            }

            );
            t.Start();
        }

        private void removProxyButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                try
                {
                    for (int k = 0; k < listDeviceObject.Count; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            Device.RemoveProxy(deviceID);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    MessageBox.Show("Finish Remove Proxy");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            t.Start();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                string deviceID = listDeviceObject[i].deviceId;
                Device.Unlockphone(deviceID);
                Thread.Sleep(200);
            }
            MessageBox.Show("Unlock phone finish");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                try
                {
                    for (int i = 0; i < listDeviceObject.Count; i++)
                    {
                        string deviceID = listDeviceObject[i].deviceId;
                        if (Utility.isScreenLock(deviceID))
                        {
                            Device.Unlockphone(deviceID);
                        }
                        //FbUtil.ChangeEmu(deviceID);
                        Thread.Sleep(200);
                        if (Utility.isScreenLock(deviceID))
                        {
                            Device.Unlockphone(deviceID);
                        }
                    }
                    MessageBox.Show("Change Device Emu phone finish");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            t.Start();

        }

        private void buttonSetProxy_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "apk files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                MessageBox.Show("Start set Proxy: " + openFileDialog1.FileName);

                // Read a text file line by line.  
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                    Console.WriteLine(line);
                int min = lines.Length;
                if (listDeviceObject.Count < min)
                {
                    min = listDeviceObject.Count;
                }
                for (int k = 0; k < min; k++)
                {
                    try
                    {
                        string proxy = lines[k];
                        if (!string.IsNullOrEmpty(proxy))
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            //Device.SetProxy(deviceID, proxy);
                            //AutoClosingMessageBox.Show(deviceID + " Set proxy finish:" + k + "/" + min, "Set Proxy", 1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                MessageBox.Show("Set proxy finish");
            }
        }

        private void rebootAllbutton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                try
                {
                    for (int i = 0; i < listDeviceObject.Count; i++)
                    {
                        string deviceID = listDeviceObject[i].deviceId;

                        Device.RebootByCmd(deviceID);

                        //AutoClosingMessageBox.Show(deviceID + "Reboot finish:" + i + "/" + listDeviceObject.Count, "Caption", 1000);
                    }
                    MessageBox.Show("Restart all phone finish");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
            t.Start();
        }

        private void setTimeZoneButton_Click(object sender, EventArgs e)
        {
            try
            {
                string timezone = timeZoneComboBox.Text;
                for (int k = 0; k < listDeviceObject.Count; k++)
                {

                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        Device.SetTimeZone(deviceID, timezone);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                MessageBox.Show("Finish Set time zone to " + timezone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void uninstallFbBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to uninstall facebook app?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Task t = new Task(() =>
                {
                    try
                    {
                        for (int i = 0; i < listDeviceObject.Count; i++)
                        {
                            string deviceID = listDeviceObject[i].deviceId;
                            try
                            {
                                Device.Uninstall(deviceID, Constant.FACEBOOK_PACKAGE);
                                //Thread.Sleep(200);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }

                            //AutoClosingMessageBox.Show(deviceID + "Reboot finish:" + i + "/" + listDeviceObject.Count, "Caption", 1000);
                        }
                        MessageBox.Show("Uninstall facebook finish");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
                t.Start();
            }
        }

        private void change2Viettel_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VIETTEL;
                }
            });
            t.Start();

        }

        public void FlashSim(string deviceID)
        {

            FbUtil.FlashSim(deviceID);
        }

        private void vinaButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VINAPHONE;
                }
            });
            t.Start();

        }



        private void vietnamButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VIETNAM_MOBILE;
                }
            });
            t.Start();

        }


        private void mobiButton_Click(object sender, EventArgs e)
        {

            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.MOBI;
                }
            });
            t.Start();

        }

        private void beelineButton_Click(object sender, EventArgs e)
        {

            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.BEELINE;
                }
            });
            t.Start();
        }

        private void rootAdbButton_Click(object sender, EventArgs e)
        {
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    string deviceID = listDeviceObject[k].deviceId;

                    Thread T3 = new Thread(() => EnableRootAdb(deviceID));
                    T3.IsBackground = true;
                    T3.Start();
                    list.Add(T3);
                }
                foreach (Thread thread2 in list)
                {
                    thread2.Join();
                }
                MessageBox.Show("Finish Change to Mobiphone");
            });
            t.Start();
        }

        public void EnableRootAdb(string deviceID)
        {
            Device.Unlockphone(deviceID);

            if (Device.AdbRoot(deviceID))
            {
                return;
            }

            Thread.Sleep(3000);
            Device.ClearCache(deviceID, "com.android.settings");
            Thread.Sleep(2000);
            Device.OpenDebugingSetting(deviceID);
            Thread.Sleep(2000);
            for (int i = 0; i < 10; i++)
            {
                Device.Swipe(deviceID, 500, 1200, 500, 900);
                if (Utility.WaitAndTapXML(deviceID, 1, "rooteddebuggingresourceid"))
                {
                    Device.AdbRoot(deviceID);
                    return;
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    string deviceID = listDeviceObject[k].deviceId;

                    Thread T3 = new Thread(() => InitialChangeAndroidID(deviceID));
                    T3.IsBackground = true;
                    T3.Start();
                    list.Add(T3);
                }
                foreach (Thread thread2 in list)
                {
                    thread2.Join();
                }
                MessageBox.Show("Finish Change to Mobiphone");
            });
            t.Start();
        }

        public void InitialChangeAndroidID(string deviceID)
        {
            Device.OpenApp(deviceID, "com.liamw.root.androididchanger");

            Thread.Sleep(1000);

            WaitAndTapXML(deviceID, 2, "Continue");
            Thread.Sleep(1000);
            WaitAndTapXML(deviceID, 2, "okresource");
            Thread.Sleep(1000);
            Device.TapByPercentDelay(deviceID, 92.9, 6.9); // New iD
            Thread.Sleep(1000);
            Device.TapByPercentDelay(deviceID, 46.5, 21.9); // Save new id
        }

        private void brightCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    string deviceID = listDeviceObject[k].deviceId;

                    Thread T3 = new Thread(() => SetBright(brightCheckBox.Checked, deviceID));
                    T3.IsBackground = true;
                    T3.Start();
                    list.Add(T3);
                }
                foreach (Thread thread2 in list)
                {
                    thread2.Join();
                }

            });
            t.Start();

        }

        public void SetBright(bool max, string deviceID)
        {
            if (max)
            {
                Device.MaxBright(deviceID);
            }
            else
            {
                Device.MinBright(deviceID);
            }
        }

        private void airplaneEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    string deviceID = listDeviceObject[k].deviceId;

                    Thread T3 = new Thread(() => SetAirplane(airplaneEnableCheckBox.Checked, deviceID));
                    T3.IsBackground = true;
                    T3.Start();
                    list.Add(T3);
                }
                foreach (Thread thread2 in list)
                {
                    thread2.Join();
                }
                MessageBox.Show("Finish Turn on Airplane");
            });
            t.Start();
        }
        public void SetAirplane(bool airplane, string deviceID)
        {
            if (airplane)
            {
                Device.AirplaneOn(deviceID);
            }
            else
            {
                Device.AirplaneOff(deviceID);
            }
        }

        private void RunAll_Click(object sender, EventArgs e)
        {
            LoadDataInit();
            ProcessWithThreadPoolMethodAsync();

        }
        async Task ProcessWithThreadPoolMethodAsync()
        {
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                Task t = new Task(() => Process(listDeviceObject[i]));
                t.Start();
                await Task.Delay(1000);
            }

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    dataGridView.EndEdit();
                    if ((string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == "False")
                    {
                        Device.MaxBright(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                    }
                    else
                    {
                        if (dataGridView.Rows[e.RowIndex].Cells[1] != null)
                        {
                            Device.MinBright(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteFileLog("dgv_CellContentClick-ex:" + ex.Message, "log.txt");
            }
        }

        public string ChangeSimAction(DeviceObject device)
        {
            device.clearCache = true;

            if (device.successInHour > 0)
            {
                device.changeSim = false;
                return "";
            }
            string deviceID = device.deviceId;
            string newSim = device.newSim;
            if (!device.changeSim)
            {
                return "";
            }
            if (newSim == Constant.TURN_ON_SIM)
            {

            }
            else
            {
                if (!device.changeSim || newSim == device.network)
                {
                    return "";
                }
            }

            LogStatus(device, "Device is changing sim to " + device.newSim);
            device.changeSim = false;
            device.network = newSim;
            if (newSim == Constant.TURN_ON_SIM || newSim == Constant.TURN_OFF_SIM)
            {
                device.simStatus = device.network;
            }
            device.blockCount = 0;
            return FbUtil.ChangeEmu(device, newSim, countrytextBox.Text);
        }

        public void MonkeyTest(string deviceID, bool run)
        {
            Random r = new Random();

            // Home
            if (run)
            {
                Thread.Sleep(r.Next(1, 5) * 1000);
                Device.ClearCache(deviceID, "com.instagram.android");
                Device.ClearCache(deviceID, "com.android.messaging");
                Device.ClearCache(deviceID, "com.android.vending");
                Device.Home(deviceID);

                // Make a phone call
                Device.MakePhoneCall(deviceID, "900");

                Thread.Sleep(r.Next(1, 5) * 1000);
                // Open instagram
                Device.OpenApp(deviceID, "com.instagram.android");
                //Device.MonkeyTouch(deviceID, "com.instagram.android");
                Thread.Sleep(r.Next(1, 5) * 1000);
                FbUtil.OpenMessengerApp(deviceID, fbLiteCheckbox);

                Thread.Sleep(12000);
                Device.OpenApp(deviceID, "com.android.vending");

                Thread.Sleep(r.Next(1, 5) * 1000);

                Device.OpenApp(deviceID, "com.android.messaging");

                Thread.Sleep(r.Next(1, 5) * 1000);
                Device.OpenWeb(deviceID, "");
            }
        }

        // Change device emu
        //public void ChangeEmu(string deviceID)
        //{
        //    Device.OpenApp(deviceID, "com.device.emulator.pro");
        //    Thread.Sleep(2000);
        //    Device.TurnOffAutoRotate(deviceID);
        //    Device.PortraitRotate(deviceID);
        //    Device.Swipe(deviceID, 500, 1500, 500, 500);
        //    Thread.Sleep(1000);

        //    Device.TapByPercentDelay(deviceID, 89.9, 58.5); // Un check Sim

        //    // Press random
        //    Device.TapByPercentDelay(deviceID, 71.8, 7.7);
        //    Thread.Sleep(2000);

        //    Device.TapByPercentDelay(deviceID, 89.9, 58.5); // Check sim
        //    Thread.Sleep(1000);

        //    // Flash seting
        //    Device.TapByPercentDelay(deviceID, 84.7, 7.2);
        //    Thread.Sleep(6000);
        //    Device.TapByPercentDelay(deviceID, 82.4, 93.6); // press ok
        //    Thread.Sleep(1000);
        //    Device.RebootDevice(deviceID);

        //    Thread.Sleep(50 * 1000);
        //}

        private void CloneBtn_Click(object sender, EventArgs e)
        {


            string selectedDeviceName = "s7";

            if (string.IsNullOrEmpty(selectedDeviceName))
            {
                selectedDeviceName = "s7";
            }

            Task t = new Task(() =>
            {

                localDate = DateTime.Now;

                string startTime = localDate.ToString(new CultureInfo("en-US"));
                label2.Invoke(new MethodInvoker(() =>
                {
                    label2.Text = "Start:" + startTime;
                }));

                while (true)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    DeviceObject device = FindDeviceFromDeviceID(selectedDevice);
                    if (fbLiteCheckbox.Checked)
                    {
                        RegFbLite(device);
                    }
                    else
                    {
                        Autoclone(device, InitialOrder(device));
                    }

                    totalRun++;

                    watch.Stop();
                    long second = watch.ElapsedMilliseconds / 1000;
                    Console.WriteLine($"Execution Time: {second} ms");
                    reportLabel.Invoke(new MethodInvoker(() =>
                    {
                        string status = " -TotalReg:" + totalRun + " --regOk:" + regOk + " --regFail:" + fail + "  --NoVerified:" + noVerified + " time:" + second;
                        reportLabel.Text = status;
                    }));
                }
            });
            t.Start();
        }

        public DeviceObject FindDeviceFromDeviceID(string deviceID)
        {
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                if (deviceID == listDeviceObject[i].deviceId)
                {
                    return listDeviceObject[i];
                }
            }
            return null;
        }
        public void RegFbLiteLD(DeviceObject device)
        {
        }
        public void RegFbLite(DeviceObject device)
        {
        }

        public void LogStatus(DeviceObject device, string text, int sleep = 0)
        {

            string key = device.keyProxy;
            //if (!string.IsNullOrEmpty(device.keyProxy) && device.keyProxy.Length > 6)
            //{
            //    key = " : " + device.keyProxy.Substring(device.keyProxy.Length - 6, 6);
            //}
            device.status = text + "-block:" + device.blockCount + key;

            UpdateStatus2(device);
            if (sleep > 0)
            {
                Thread.Sleep(sleep);
            }
        }

        public void LogRegFailStatus(DeviceObject device)
        {
            dataGridView.Rows[device.index].Cells[12].Value = device.regByMail + " - Reg fail";
        }

        public void LogVeriFailStatus(DeviceObject device)
        {
            dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Cyan;
            dataGridView.Rows[device.index].Cells[12].Value = device.regByMail + " - Veri fail";
        }

        private void TurnOnSimButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_ON_SIM;

                }
            });
            t.Start();
        }

        private void enableSimButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.ENABLE_SIM;
                }
            });
            t.Start();
        }

        private void change2Ip4Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Set device change to ip v4");

            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    //if (listDeviceObject[k].currentIPType != Constant.ACTION_CHANGE2IP4)
                    //{
                    listDeviceObject[k].action = Constant.ACTION_CHANGE2IP4;
                    //listDeviceObject[k].currentIPType = Constant.ACTION_CHANGE2IP4;
                    //}
                }
            });
            t.Start();
        }

        public void ActionHandle(DeviceObject device)
        {
            if (string.IsNullOrEmpty(device.action))
            {
                return;
            }
            if (randomIp46CheckBox.Checked)
            {
                Random ddd = new Random();
                int kkk = ddd.Next(1, 100);
                if (kkk < 50)
                {

                    device.action = Constant.ACTION_CHANGE2IP6;
                }
                else
                {
                    device.action = Constant.ACTION_CHANGE2IP4;
                }
            }
            if (device.action == Constant.ACTION_CHANGE_SIM)
            {
                string ddd = ChangeSimAction(device);
                if (!string.IsNullOrEmpty(ddd))
                {
                    dataGridView.Rows[device.index].Cells[14].Value = ddd;
                }
            }
            string ss = Device.GetIpSimProtocol(device.deviceId);

            if (device.action == Constant.ACTION_CHANGE2IP4 && ss == Constant.IP4)
            {
                LogStatus(device, "Đang là ip4 rồi, không cần change", 1000);
                device.action = "";
                return;
            }
            if (device.action == Constant.ACTION_CHANGE2IP6 && ss == Constant.IP6)
            {
                LogStatus(device, "Đang là ip666666 rồi, không cần change", 1000);
                device.action = "";
                return;
            }
            Change2Ip(device, device.action); // Action handle before run

            LogStatus(device, "Change sim finish ------ stop change sim", 1000);
            device.action = "";
        }

        public void Change2Ip(DeviceObject device, string action)
        {
            try
            {
                LogStatus(device, "Device is changing to IP " + action);
                string deviceID = device.deviceId;

                Device.Unlockphone(deviceID);
                Thread.Sleep(2000);
                Device.OpenRoamingSetting(deviceID);
                Thread.Sleep(5000);

                if (action == Constant.ACTION_CHANGE2IP3G)
                {
                    WaitAndTapXML(deviceID, 1, "textloạimạngưutiên");
                    WaitAndTapXML(deviceID, 1, "3gresource");

                    return;
                }
                if (action == Constant.ACTION_CHANGE2IP4G)
                {
                    WaitAndTapXML(deviceID, 1, "textloạimạngưutiên");
                    if (!WaitAndTapXML(deviceID, 1, "LTE")) WaitAndTapXML(deviceID, 1, "đề xuất");

                    return;
                }

                Device.Swipe(deviceID, 500, 2000, 500, 1000);
                Thread.Sleep(2000);
                Device.TapByPercent(deviceID, 32.0, 96.9); // tap advanced
                Thread.Sleep(2000);
                Device.Swipe(deviceID, 500, 2000, 500, 1000);
                Thread.Sleep(2000);
                Utility.WaitAndTapXML(deviceID, 3, Language.AccessPointNames()); // tên diểm truy cập
                Thread.Sleep(2000);
                Device.TapByPercent(deviceID, 21.3, 15.1);
                Thread.Sleep(2000);
                Device.Swipe(deviceID, 500, 2000, 500, 1000);
                Thread.Sleep(2000);
                Utility.WaitAndTapXML(deviceID, 2, Language.APNProtocol());
                Thread.Sleep(2000);
                if (action == Constant.ACTION_CHANGE2IP4)
                {
                    Device.TapByPercent(deviceID, 14.3, 45.1);
                }
                else if (action == Constant.ACTION_CHANGE2IP6)
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.6, 51.2);
                }
                else if (action == Constant.ACTION_CHANGE2IP4_6)
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.0, 58.2);
                }

                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.7, 7.1); // setting
                Thread.Sleep(1000);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 59.2, 14.1); // Lưu

                Thread.Sleep(1000);
                Device.Back(deviceID);
                Thread.Sleep(1000);
                Device.Back(deviceID);
                Thread.Sleep(1000);
                Device.Back(deviceID);
                Thread.Sleep(1000);
                Device.Back(deviceID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string GetIpType(DeviceObject device)
        {
            try
            {
                string result = "";

                string deviceID = device.deviceId;

                Device.Unlockphone(deviceID);
                Thread.Sleep(2000);
                Device.OpenRoamingSetting(deviceID);
                Thread.Sleep(5000);

                Device.Swipe(deviceID, 500, 2000, 500, 1000);
                Thread.Sleep(2000);
                Device.TapByPercent(deviceID, 32.0, 96.9); // tap advanced
                Thread.Sleep(2000);
                Device.Swipe(deviceID, 500, 2000, 500, 1000);
                Thread.Sleep(2000);
                Utility.WaitAndTapXML(deviceID, 3, Language.AccessPointNames()); // tên diểm truy cập
                Thread.Sleep(2000);
                Device.TapByPercent(deviceID, 21.3, 15.1);
                Thread.Sleep(2000);
                Device.Swipe(deviceID, 500, 2000, 500, 1000);
                Thread.Sleep(2000);
                Utility.WaitAndTapXML(deviceID, 2, Language.APNProtocol());
                Thread.Sleep(2000);

                string xml = GetUIXml(device.deviceId);

                if (CheckTextExist(deviceID, "ipv6resourceidandroididtext1classandroidwidgetcheckedtextviewpackagecomandroidsettingscontentdesccheckabletruecheckedtrue", 1, xml))
                {
                    result = "IP6";
                }
                else if (CheckTextExist(deviceID, "ipv4resourceidandroididtext1classandroidwidgetcheckedtextviewpackagecomandroidsettingscontentdesccheckabletruecheckedtrue", 1, xml))
                {
                    result = "IP4";
                }
                else if (CheckTextExist(deviceID, "pv4ipv6resourceidandroididtext1classandroidwidgetcheckedtextviewpackagecomandroidsettingscontentdesccheckabletruecheckedtrue", 1, xml))
                {
                    result = "IP4/6";
                }
                else
                {
                    result = "Không xác đinh";
                }

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.8, 64.7); // Hủy
                Thread.Sleep(1000);
                Device.Back(deviceID);
                Thread.Sleep(1000);
                Device.Back(deviceID);
                Thread.Sleep(1000);
                Device.Back(deviceID);
                Thread.Sleep(1000);
                Device.Back(deviceID);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";
        }

        private void runingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (runningCheckBox.Checked)
                {
                    dataGridView.Rows[i].Cells[6].Value = runningCheckBox.Checked;
                    if (i < listDeviceObject.Count)
                    {
                        listDeviceObject[i].blockCount = 0;
                    }
                }
            }
        }

        private void viettelTeleButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VIETTEL_TELECOM;
                }
            });
            t.Start();
        }

        private void viettelMobileButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VIETTEL_MOBILE;
                }
            });
            t.Start();
        }

        private void vnMobiButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VN_MOBIPHONE;
                }
            });
            t.Start();
        }

        private void change2Ip6Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Set device change to ip v6");

            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    //if (listDeviceObject[k].currentIPType != Constant.ACTION_CHANGE2IP6)
                    //{
                    listDeviceObject[k].action = Constant.ACTION_CHANGE2IP6;
                    //    listDeviceObject[k].currentIPType = Constant.ACTION_CHANGE2IP6;
                    //}
                }
            });
            t.Start();
        }

        private void change2Ip46Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Set device change to ip v4-6");

            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].action = Constant.ACTION_CHANGE2IP4_6;
                    //listDeviceObject[k].currentIPType = Constant.ACTION_CHANGE2IP4_6;
                }

            });
            t.Start();
        }

        public string UpdateStatus(List<DeviceObject> devices)
        {
            string result = "";
            if (devices == null || devices.Count < 1) return "";

            for (int i = 0; i < devices.Count; i++)
            {
                DeviceObject device = devices[i];
                double globalPercent = 0;
                if (device.totalInHour > 0)
                {
                    device.percentInHour = (Convert.ToDouble(device.successInHour) / device.totalInHour) * 100;
                }
                if (device.globalTotal > 0)
                {
                    globalPercent = Convert.ToDouble(device.globalSuccess) / device.globalTotal * 100;
                }
                string rate = "In a half Hour:" + device.successInHour
                    + "/" + device.totalInHour + "_"
                    + "- total success:" + device.globalSuccess + "/" + device.globalTotal;
                rate = String.Format("{0,-45}", rate);
                result = result + String.Format("{0,-26}", devices[i].deviceId) + " - " + rate + " - "
                    + String.Format("{0,20}", devices[i].status) + "\n";
                dataGridView.Rows[i].Cells[0].Value = (i + 1) + "";
                dataGridView.Rows[i].Cells[1].Value = device.deviceId;
                dataGridView.Rows[i].Cells[2].Value = device.successInHour + "/" + device.totalInHour + " = " + Math.Round(device.percentInHour, 1) + " %";
                dataGridView.Rows[i].Cells[3].Value = device.globalSuccess + "/" + device.globalTotal + " = " + Math.Round(globalPercent, 1) + "%";
                dataGridView.Rows[i].Cells[4].Value = device.duration;
                dataGridView.Rows[i].Cells[5].Value = device.status;
                dataGridView.Rows[i].Cells[7].Value = device.network + " " + device.currentMobileReg;

                if (device.status.Contains(Constant.DEEMED)) dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Coral;

                if (device.status.Contains(Constant.ACCOUNT_BLOCK)) dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Orange;

                if (device.status == Constant.DEVICE_HOLDING) dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            }

            return "";
        }

        public string UpdateStatus2(DeviceObject device)
        {
            try
            {
                string result = "";
                double globalPercent = 0;
                if (device.totalInHour > 0)
                {
                    device.percentInHour = (Convert.ToDouble(device.successInHour) / device.totalInHour) * 100;
                }
                if (device.globalTotal > 0)
                {
                    globalPercent = Convert.ToDouble(device.globalSuccess) / device.globalTotal * 100;
                }
                string rate = "In a half Hour:" + device.successInHour
                    + "/" + device.totalInHour + "_"
                    + "- total success:" + device.globalSuccess + "/" + device.globalTotal;
                rate = String.Format("{0,-45}", rate);
                result = result + String.Format("{0,-26}", device.deviceId) + " - " + rate + " - "
                    + String.Format("{0,20}", device.status) + "\n";
                int i = device.index;
                dataGridView.Rows[i].Cells[0].Value = Convert.ToString((i + 1));
                dataGridView.Rows[i].Cells[1].Value = Convert.ToString(device.deviceId);
                string temp = device.successInHour + "/" + device.totalInHour + " = " + Math.Round(device.percentInHour, 1) + " %";
                dataGridView.Rows[i].Cells[2].Value = temp;
                string temp2 = device.globalSuccess + "/" + device.globalTotal + " = " + Math.Round(globalPercent, 1) + "%";
                dataGridView.Rows[i].Cells[3].Value = temp2;
                dataGridView.Rows[i].Cells[4].Value = Convert.ToString(device.duration);
                dataGridView.Rows[i].Cells[5].Value = Convert.ToString(device.status);
                dataGridView.Rows[i].Cells[7].Value = Convert.ToString(device.network + " " + device.currentMobileReg);
                if (!string.IsNullOrEmpty(device.network))
                {
                    if (device.networkStatus == Constant.TURN_ON_SIM || device.networkStatus == Constant.TURN_OFF_SIM)
                    {
                        device.simStatus = device.network;
                        Properties.Settings.Default.simStatus = device.simStatus;
                        Properties.Settings.Default.Save();
                    }
                    if (device.network == Constant.TURN_ON_EMU || device.network == Constant.TURN_OFF_EMU)
                    {
                        device.emuStatus = device.network;
                        Properties.Settings.Default.emuStatus = device.emuStatus;
                        Properties.Settings.Default.Save();
                    }
                    if (device.network == Constant.TURN_ON_ALL)
                    {
                        device.emuStatus = Constant.TURN_ON_EMU;
                        device.simStatus = Constant.TURN_ON_SIM;
                        device.allEmuStatus = device.network;
                        Properties.Settings.Default.allEmuStatus = device.allEmuStatus;
                        Properties.Settings.Default.simStatus = device.simStatus;
                        Properties.Settings.Default.emuStatus = device.emuStatus;
                        Properties.Settings.Default.Save();
                    }
                    if (device.network == Constant.TURN_OFF_ALL)
                    {
                        device.emuStatus = Constant.TURN_OFF_EMU;
                        device.simStatus = Constant.TURN_OFF_SIM;
                        device.allEmuStatus = device.network;
                        Properties.Settings.Default.allEmuStatus = device.allEmuStatus;
                        Properties.Settings.Default.simStatus = device.simStatus;
                        Properties.Settings.Default.emuStatus = device.emuStatus;
                        Properties.Settings.Default.Save();
                    }
                    statusSim = device.simStatus + "-" + device.emuStatus + "-" + device.allEmuStatus;
                }

                if (device.status.Contains(Constant.DEEMED))
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Coral;

                }
                if (device.status.Contains(Constant.ACCOUNT_BLOCK))
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Orange;

                }
                if (device.status == Constant.DEVICE_HOLDING)
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex:" + ex.Message);
                return "";
            }
        }

        public void InitialData(List<DeviceObject> devices)
        {
            for (int i = 0; i < devices.Count; i++)
            {
                DeviceObject device = devices[i];
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = (i + 1) + "";
                dataGridView.Rows[i].Cells[1].Value = device.deviceId;
                dataGridView.Rows[i].Cells[2].Value = device.successInHour + "/" + device.totalInHour;
                dataGridView.Rows[i].Cells[3].Value = device.globalSuccess + "/" + device.globalTotal;
                dataGridView.Rows[i].Cells[4].Value = device.duration;
                dataGridView.Rows[i].Cells[5].Value = device.status;
                dataGridView.Rows[i].Cells[6].Value = true;

                device.index = i;
            }
        }

        private void vnVinaphoneButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VN_VINAPHONE;
                }
            });
            t.Start();
        }

        private void verifyAccCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            verifiedCheckbox.Checked = verifyAccNvrCheckBox.Checked;

            if (verifiedCheckbox.Checked)
            {
                veriDirectByPhoneCheckBox.Checked = false;
                reupFullCheckBox.Checked = false;
                sleep1MinuteCheckBox.Checked = false;
                veriBackupCheckBox.Checked = false;
                set2faWebCheckBox.Checked = false;
                randomNewContactCheckBox.Checked = true;
                moiFbLitecheckBox.Checked = false;
                moiKatanacheckBox.Checked = false;
                moiBusinesscheckBox.Checked = false;
                //loginByUserPassCheckBox.Checked = false;
            }
            if (!verifiedCheckbox.Checked)
            {
                //accMoiFbLitecheckBox.Checked = true;
                randomNewContactCheckBox.Checked = false;
            }
        }

        private void inputStringCheckbox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void adbKeyboardCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }
        public bool CheckFinishAll()
        {
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                if (!listDeviceObject[i].isFinish)
                {
                    return false;
                }
            }

            return true;
        }

        public bool SetReadyAll()
        {
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                listDeviceObject[i].isReady = true;
                listDeviceObject[i].isFinish = false;
            }
            return true;
        }

        public void ChangeInforDevice(DeviceObject device)
        {
            string deviceID = device.deviceId;
            if (androidIDCheckBox.Checked)
            {
                Device.RandomAndroidID(deviceID);
            }
        }
        public void VerifyAcc(DeviceObject device)
        {
            string deviceID = device.deviceId;
            string[] List = FileUtil.GetAndDeleteLine("noveri.txt").Split('|');
            if (List == null || List.Length < 2)
            {
                return;
            }
            string uid = List[0];
            string pass = List[1];

            KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.6, 46.0);
            Thread.Sleep(500);
            Utility.InputVietVNIText(deviceID, uid);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 15.3, 54.3);
            Thread.Sleep(500);
            Utility.InputVietVNIText(deviceID, pass);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.4, 62.7);
            Thread.Sleep(4000);
        }



        private void checkFBInstalledBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Start Check FB installed");
            foreach (DeviceObject device in listDeviceObject)
            {
                Task t = new Task(() => CheckDeviceInfo(device));
                t.Start();
            }
        }

        public string CheckOnSim(DeviceObject device)
        {
            string info = "Check ";
            string deviceID = device.deviceId;
            LogStatus(device, "Kiểm tra tình trạng on/off sim");
            Device.Unlockphone(deviceID);
            // Check turn on sim
            Device.KillApp(deviceID, "com.device.emulator.pro");
            Device.OpenApp(deviceID, "com.device.emulator.pro");
            Thread.Sleep(2000);

            Device.Swipe(deviceID, 500, 2200, 500, 1450);

            if (CheckTextExist(deviceID, Language.TurnOffOperatorSim()))
            {
                info = info + "-Turn OFF sim";
                device.network = Constant.TURN_OFF_SIM;
            }
            else if (CheckTextExist(deviceID, Language.TurnOnOperatorSim()))
            {
                info = info + "-Turn ON sim";
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 64.1, 70.7); // tap edit 
                Thread.Sleep(2000);
                device.network = Constant.TURN_ON_SIM;
                string xml = GetUIXml(deviceID);
                if (CheckTextExist(deviceID, "vinaphoneresourceid", 1, xml))
                {
                    device.network = Constant.TURN_ON_SIM + "_vinaphone";
                }
                else if (CheckTextExist(deviceID, "viettelsourceid", 1, xml))
                {
                    device.network = Constant.TURN_ON_SIM + "_viettel";
                }
                else if (CheckTextExist(deviceID, "vietnamobileresourceid", 1, xml))
                {
                    device.network = Constant.TURN_ON_SIM + "_vietnammobile";
                }
                else if (CheckTextExist(deviceID, "mobifoneresourceid", 1, xml))
                {
                    device.network = Constant.TURN_ON_SIM + "_mobifone";
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 57.3, 69.1);
            }
            else
            {
                info = info + "-Unknown Sim";

            }
            LogStatus(device, "Kiểm tra tình trạng on/off sim -------xong");
            device.info = info;
            return info;
        }
        public void CheckDeviceInfo(DeviceObject device)
        {
            string info = "";
            string deviceID = device.deviceId;
            string fb = "";
            string fbLite = "";
            string messenger = "";
            if (Device.CheckAppInstall(deviceID, Constant.FACEBOOK_PACKAGE))
            {
                fb = "-Has fb-";
            }
            else
            {
                fb = "-No fb-";
            }
            if (Device.CheckAppInstall(deviceID, "com.facebook.lite"))
            {
                fbLite = "-Has fblite-";
            }
            else
            {
                fbLite = "-No fblite-";
            }

            if (Device.CheckAppInstall(deviceID, "com.facebook.orca"))
            {
                messenger = "-Has Messenger-";
            }
            else
            {
                messenger = "-No Messenger-";
            }

            info = fb + fbLite + messenger;
            // Check turn on sim
            string[] emus = { "imei", "deviceid", "adsid", "gsfid", "serial", "bmac",
                    "wmac", "wssid", "mobno" };
            Device.Unlockphone(deviceID);
            Device.KillApp(deviceID, "com.device.emulator.pro");
            Device.OpenApp(deviceID, "com.device.emulator.pro");
            Thread.Sleep(2000);
            int checkEmu = 0;
            for (int i = 0; i < emus.Length; i++)
            {
                string key = emus[i];

                if (!CheckTextExist(deviceID, Language.TurnOffEMU(key)))
                {
                    break;
                }
                checkEmu++;
                Thread.Sleep(200);
                Device.Swipe(deviceID, 500, 1500, 500, 1350);
            }

            if (checkEmu > (emus.Length - 2))
            {
                info = info + "-Turn OFF emu";
            }
            else
            {
                info = info + "-Turn ON emu";
            }

            if (CheckTextExist(deviceID, Language.TurnOffOperatorSim()))
            {
                info = info + "-Turn OFF sim";
            }
            else if (CheckTextExist(deviceID, Language.TurnOnOperatorSim()))
            {
                info = info + "-Turn ON sim";
            }
            else
            {
                info = info + "-Unknown Sim";

            }
            dataGridView.Rows[device.index].Cells[5].Value = info;

        }
        private void installMissingFBbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "apk",
                Filter = "apk files (*.apk)|*.apk",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            int numberOfThread = listDeviceObject.Count;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Start install app: " + openFileDialog1.FileName);
                List<Thread> list = new List<Thread>();
                Task t = new Task(() =>
                {
                    for (int k = 0; k < numberOfThread; k++)
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        if (deviceID.ToLower().Contains("offline")
                            || Device.CheckAppInstall(deviceID, Constant.FACEBOOK_PACKAGE))
                        {
                            continue;
                        }
                        string fileName = openFileDialog1.FileName;
                        Thread T3 = new Thread(() => threadInstallApk(deviceID, fileName));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    foreach (Thread thread2 in list)
                    {
                        thread2.Join();
                    }
                    MessageBox.Show("Finish install Apk");
                });
                t.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CheckActionSpeed();
        }
        public void CheckActionSpeed()
        {
            try
            {
                int count = 0;
                speedlabel.Text = count + " Acc/h";
                int minSpeed = Convert.ToInt32(minSpeedTextBox.Text);

                if (count < minSpeed && autoSpeedCheckBox.Checked)
                {
                    if (countdown == 50)
                    {
                        Task t = new Task(() =>
                        {
                            for (int k = 0; k < listDeviceObject.Count; k++)
                            {
                                listDeviceObject[k].changeSim = true;
                                listDeviceObject[k].newSim = Constant.VIETNAM_MOBILE;
                            }
                        });
                        t.Start();
                    }
                }
                countdown--;
                if (countdown <= 0)
                {
                    countdown = 10;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void turnoffSimButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_OFF_SIM;
                }
            });
            t.Start();
        }

        private void rmFbliteButton_Click(object sender, EventArgs e)
        {
            if (vietCheckbox.Checked)
            {
                Language.language = Constant.LANGUAGE_VN;
            }
            else
            {
                Language.language = Constant.LANGUAGE_US;
            }
            MessageBox.Show("Start UnInstall FB Lite");
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        if (deviceID.ToLower().Contains("offline")) continue;

                        Thread T3 = new Thread(() => UnInstallFblite(deviceID));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
                foreach (Thread thread2 in list) thread2.Join();

                MessageBox.Show("Finish UnInstall FB Lite");
            });
            t.Start();
        }

        public void UnInstallFblite(string deviceID)
        {
            //FbUtil.ClearCacheFb(device);
            Thread.Sleep(500);
            FbUtil.ClearCacheFbLite(deviceID, clearAccSettingcheckBox.Checked);

            Thread.Sleep(1000);
            Device.Uninstall(deviceID, "com.facebook.lite");
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            removeProxy2checkBox.Checked = false;
            moiKatanacheckBox.Checked = false;
            moiFbLitecheckBox.Checked = true;
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                if (listDeviceObject[i] != null)
                {
                    listDeviceObject[i].loadNewProxy = false;
                }
            }
            ResetCount();
            LoadProxy();
        }
        public void LoadProxy()
        {
            Properties.Settings.Default.ShoplikeKey = shoplikeTextBox1.Text;

            Properties.Settings.Default.TinsoftKey = tinsoftTextBox.Text;

            Properties.Settings.Default.FastproxyKey = FastProxyTextBox.Text;

            Properties.Settings.Default.Save();
            string[] keys = null;

            if (shopLike1RadioButton.Checked)
            {
                keys = shoplikeTextBox1.Text.Split(
                    new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (tinsoftRadioButton.Checked)
            {
                //keys = Regex.Split(tinsoftTextBox.Text, Environment.NewLine);
                keys = tinsoftTextBox.Text.Split(
                    new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (tinProxyRadioButton.Checked)
            {
                keys = tinProxyTextBox.Text.Split(
                    new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (tmProxyRadioButton.Checked)
            {
                keys = tmProxyTextBox.Text.Split(
                    new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (dtProxyRadioButton.Checked)
            {
                keys = dtProxyTextBox.Text.Split(
                    new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (fastProxyRadioButton.Checked || zuesProxyradioButton.Checked || impulseradioButton.Checked || zuesProxy4G.Checked || s5ProxyradioButton.Checked
                || tunProxyradioButton.Checked)
            {
                keys = FastProxyTextBox.Text.Split(
                    new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }

            if (keys != null && keys.Length > 0)
            {
                int k = 0;
                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    string key = "";
                    if (k < keys.Length && !listDeviceObject[i].deviceId.Contains("offline"))
                    {
                        key = keys[k];
                        k++;
                    }
                    if (!string.IsNullOrEmpty(key))
                    {
                        listDeviceObject[i].keyProxy = key;
                        //string end = key.Substring(key.Length - 6, 6);
                        if (fastProxyRadioButton.Checked || zuesProxyradioButton.Checked || impulseradioButton.Checked)
                        {
                            //end = key.Substring(key.Length - 27, 10);
                            Proxy p = new Proxy();
                            string[] kk = key.Split(':');
                            if (kk == null)
                            {

                            }
                            else
                            {
                                p.host = kk[0];
                                p.port = kk[1];

                                p.hasProxy = true;
                                if (zuesProxyradioButton.Checked)
                                {
                                    p.proxyId = kk[2];
                                }
                                //else
                                //{
                                //    p.username = kk[2];
                                //    p.pass = kk[3];
                                //}
                                listDeviceObject[i].proxy = p;
                            }
                        }
                        else if (zuesProxy4G.Checked || s5ProxyradioButton.Checked
                            )
                        {
                            //end = key.Substring(key.Length - 27, 10);
                            Proxy p = new Proxy();
                            string[] kk = key.Split(':');
                            if (kk == null)
                            {

                            }
                            else
                            {
                                p.host = kk[0];
                                p.port = kk[1];

                                p.hasProxy = true;

                                if (kk.Length > 2)
                                {
                                    p.username = kk[2];
                                    p.pass = kk[3];
                                }

                                if (kk.Length > 4)
                                {
                                    p.proxyId = kk[4];
                                }

                                listDeviceObject[i].proxy = p;
                            }
                            LogStatus(listDeviceObject[i], "proxy: " + key);
                        } else if (tunProxyradioButton.Checked)
                        {
                            Proxy p = new Proxy();
                            p.hasProxy = true;
                            key = key.Replace('@', ':');
                            string[] kk = key.Split(':');
                            p.host = kk[2];
                            p.port = kk[3];
                            p.username = kk[0];
                            p.pass = kk[1];
                            listDeviceObject[i].proxy = p;
                        }
                    }
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetCount();
        }
        public void ResetCount()
        {
            otp = 0;
            accDieCaptcha = 0;
            veriOk = 0;
            totalSucc = 0;
            regNvrOk = 0;
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                listDeviceObject[i].blockCount = 0;
                listDeviceObject[i].cycle = 0;
                listDeviceObject[i].duration = 0;
                listDeviceObject[i].globalSuccess = 0;
                listDeviceObject[i].globalTotal = 0;
                listDeviceObject[i].percentInHour = 0;
                listDeviceObject[i].successInHour = 0;
                listDeviceObject[i].totalInHour = 0;
            }
            string startTime = DateTime.Now.ToString("HH:mm:ss tt");
            label2.Invoke(new MethodInvoker(() =>
            {
                label2.Text = "Start:" + startTime;
                Text = "Fb Reg - start" + " - " + startTime;
            }));


            totalLabel.Invoke(new MethodInvoker(() =>
            {
                totalLabel.Text = totalSucc + " / " + regNvrOk + " - " + percent;
                Text = "Fb Reg - start" + " - " + startTime;
            }));
        }

        private void turnOffEmuButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {

                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_OFF_EMU;
                }
            });
            t.Start();
        }

        private void dgrdResults_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void dgrdResults_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //handle the row selection on right click
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    dataGridView.CurrentCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    dataGridView.Rows[e.RowIndex].Selected = true; // Can leave these here - doesn't hurt
                    dataGridView.Focus();

                    selectedDeviceID = Convert.ToString(dataGridView.Rows[e.RowIndex].Cells[1].Value);
                }
                catch (Exception)
                { }
            }
        }

        private void CallStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Device.MakePhoneCall(selectedDeviceID, "0932104257");
            });
            t.Start();
        }

        private void BrightStripMenuItem_Click(object sender, EventArgs e)
        {
            Device.MaxBright(selectedDeviceID);
        }

        private void DarkStripMenuItem_Click(object sender, EventArgs e)
        {
            Device.MinBright(selectedDeviceID);
        }
        private void CaptureStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int i = 0; i < 1000; i++)
                {

                }
            });
            t.Start();
        }
        private void RebootStripMenuItem4_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Device.RebootDevice(selectedDeviceID);
            });
            t.Start();

        }

        private void changeSimUsButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.US_PHONE;
                }
            });
            t.Start();
        }

        private void vietSimButton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.VIET_PHONE;
                }
            });
            t.Start();
        }

        private void executeAdbButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mailTextbox.Text)) return;

            MessageBox.Show("Start: " + mailTextbox.Text);
            executeAdbButton.Enabled = false;
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        string cmd = string.Format(Device.CONSOLE_ADB + " {1}", deviceID, mailTextbox.Text);
                        if (deviceID.ToLower().Contains("offline")) continue;

                        Thread T3 = new Thread(() => RunCMD(deviceID, cmd));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                }
                foreach (Thread thread2 in list) thread2.Join();

                MessageBox.Show("Finish Run cmd:" + mailTextbox.Text);
                executeAdbButton.Invoke(new MethodInvoker(() =>
                {
                    executeAdbButton.Enabled = true;
                }));

            });
            t.Start();
        }

        private void cookieTextNowTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.cookie = codeKeyTextNowTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void cookieCodeTextNowtextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.cookieCodeTextNow = cookieCodeTextNowtextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void turnOnEmubutton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_ON_EMU;
                }
            });
            t.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.otpmmoKey = otpKeyTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void veriDirectByPhoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (veriDirectByPhoneCheckBox.Checked)
            {
                verifyAccNvrCheckBox.Checked = !veriDirectByPhoneCheckBox.Checked;
                verifiedCheckbox.Checked = !veriDirectByPhoneCheckBox.Checked;
                prefixTextNowCheckBox.Checked = false;
            }
        }

        private void getPhoneCodeTextNowbutton_Click(object sender, EventArgs e)
        {
            Thread T3 = new Thread(() => UpdatePrefix());
            T3.IsBackground = true;
            T3.Start();
        }
        public void UpdatePrefix()
        {
            Dictionary<String, String> dictPhones = new Dictionary<string, string>();
            getPhoneCodeTextNowbutton.ForeColor = Color.Red;

            if (!File.Exists("data/textnowPhone.txt")) return;

            string[] lines = File.ReadAllLines("data/textnowPhone.txt");
            if (lines != null && lines.Length > 1000)
            {
                for (int k = 0; k < lines.Length; k++)
                {
                    string phone = lines[k].Substring(0, 8);
                    if (!dictPhones.ContainsKey(phone)) dictPhones.Add(phone, phone);
                }
            }

            Thread.Sleep(1000);

            if (dictPhones.Count < 1) return;

            string line = "";
            foreach (KeyValuePair<string, string> entry in dictPhones) line = line + entry.Key + "\r\n";

            if (File.Exists("prefix_phone_codetextnow.txt")) File.Delete("prefix_phone_codetextnow.txt");

            WriteFileLine(line, "prefix_phone_codetextnow.txt");
            Thread.Sleep(1000);
            prefixTextNow = File.ReadAllLines("prefix_phone_codetextnow.txt");
            getPhoneCodeTextNowbutton.ForeColor = Color.Green;
        }

        private void prefixTextNowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (prefixTextNowCheckBox.Checked)
            {
                veriDirectByPhoneCheckBox.Checked = false;
            }
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            if (anError.Context == DataGridViewDataErrorContexts.Commit) MessageBox.Show("Commit error");

            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange) MessageBox.Show("Cell change");

            if (anError.Context == DataGridViewDataErrorContexts.Parsing) MessageBox.Show("parsing error");

            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl) MessageBox.Show("leave control error");

            //if ((anError.Exception) is ConstraintException)
            //{
            //    DataGridView view = (DataGridView)sender;
            //    view.Rows[anError.RowIndex].ErrorText = "an error";
            //    view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

            //    anError.ThrowException = false;
            //}
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Thread T3 = new Thread(() => UpdatePrefix());
            T3.IsBackground = true;
            T3.Start();
        }

        private void veriHotmailCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (veriHotmailCheckBox.Checked)
            {
                verifiedCheckbox.Checked = true;
                veriDirectByPhoneCheckBox.Checked = false;
            }
        }

        private void forceIp4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            change2Ip6Button.Enabled = !forceIp4CheckBox.Checked;
            if (forceIp4CheckBox.Checked) forceIp6checkBox.Checked = !forceIp4CheckBox.Checked;
        }

        private void forceIp6checkBox_CheckedChanged(object sender, EventArgs e)
        {
            change2Ip4Button.Enabled = !forceIp6checkBox.Checked;
            if (forceIp6checkBox.Checked)
            {
                forceIp4CheckBox.Checked = !forceIp6checkBox.Checked;
            }
        }

        private void set2faCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void reupFullCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (reupFullCheckBox.Checked)
            {
                //set2faCheckbox.Checked = true;
                verifyAccNvrCheckBox.Checked = false;
                verifiedCheckbox.Checked = false;
                //accMoiFbLitecheckBox.Checked = false;
                randomNewContactCheckBox.Checked = false;
                //moiKatanacheckBox.Checked = false;
                //moiBusinesscheckBox.Checked = false;
                miniProfileCheckBox.Checked = false;
                otp = 0;
                accDieCaptcha = 0;
                veriOk = 0;
                totalSucc = 0;
                regNvrOk = 0;
                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    listDeviceObject[i].blockCount = 0;
                    listDeviceObject[i].cycle = 0;
                    listDeviceObject[i].duration = 0;
                    listDeviceObject[i].globalSuccess = 0;
                    listDeviceObject[i].globalTotal = 0;
                    listDeviceObject[i].percentInHour = 0;
                    listDeviceObject[i].successInHour = 0;
                    listDeviceObject[i].totalInHour = 0;
                }
                string startTime = DateTime.Now.ToString("HH:mm:ss tt");
                label2.Invoke(new MethodInvoker(() =>
                {
                    label2.Text = "Start:" + startTime;
                    Text = "Fb Reg - start" + " - " + startTime;
                }));


                totalLabel.Invoke(new MethodInvoker(() =>
                {
                    totalLabel.Text = totalSucc + " / " + regNvrOk + " - " + percent;
                    Text = "Fb Reg - start" + " - " + startTime;
                }));
            }
            else
            {
                moiBusinesscheckBox.Checked = true;
                randomNewContactCheckBox.Checked = true;
                // miniProfileCheckBox.Checked = true;
            }
        }

        private void drkKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.drkKey = drkKeyTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void drkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (drkCheckBox.Checked) textnowCheckbox.Checked = false;
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void veriBackupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            verifiedCheckbox.Checked = veriBackupCheckBox.Checked;
        }

        private void shoplikeTextBox1_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShoplikeKey = shoplikeTextBox1.Text;
            Properties.Settings.Default.Save();
        }

        private void tinsoftTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TinsoftKey = tinsoftTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void removeProxyButton_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < listDeviceObject.Count; i++) listDeviceObject[i].removeProxy = true;
        }

        private void changeSim2Timer_Tick(object sender, EventArgs e)
        {
            if (changeAllSim2checkBox.Checked)
            {
                currentSim2 = RandomSim2(currentSim2);
                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    listDeviceObject[i].newSim = currentSim2;
                    listDeviceObject[i].changeSim = true;
                }
            }
        }

        private void installFacebookButton_Click(object sender, EventArgs e)
        {
            string[] filefbList = Directory.GetFiles("data\\fb", "*.apk");
            if (filefbList == null || filefbList.Length <= 0) return;

            int m = 0;
            Dictionary<string, string> deviceIDVersionMap = new Dictionary<string, string>();
            for (int i = 0; i < listDeviceObject.Count; i++)
            {
                //FbUtil.InstallFb(listDeviceObject[i].deviceId, filefbList[k]);

                deviceIDVersionMap.Add(listDeviceObject[i].deviceId, filefbList[m]);
                m++;

                if (m == filefbList.Length)
                {
                    m = 0;
                }
                Console.WriteLine("i = " + i + " m = " + m);
            }

            int numberOfThread = listDeviceObject.Count;
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                installFacebookButton.Invoke(new MethodInvoker(() =>
                {
                    installFacebookButton.Enabled = true;
                    installFacebookButton.BackColor = Color.Red;
                    installFacebookButton.Text = "Installing Apk fb";
                }));
                for (int k = 0; k < numberOfThread; k++)
                {
                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        if (deviceID.ToLower().Contains("offline")) continue;

                        string fileName = deviceIDVersionMap[deviceID];
                        Thread T3 = new Thread(() => FbUtil.InstallFb(deviceID, fileName));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                }
                foreach (Thread thread2 in list) thread2.Join();

                MessageBox.Show("Finish install Apk");

                installFacebookButton.Invoke(new MethodInvoker(() =>
                {
                    installFacebookButton.Enabled = true;
                    installFacebookButton.BackColor = Color.Green;
                    installFacebookButton.Text = "Install Finish fb";
                }));

            });
            t.Start();
        }

        private void drkDomainTextbox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.drkDomain = drkDomainTextbox.Text;
            Properties.Settings.Default.Save();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "apk",
                Filter = "apk files (*.apk)|*.apk",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            int numberOfThread = listDeviceObject.Count;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Start install app: " + openFileDialog1.FileName);
                installApkFbButton.Enabled = false;
                installApkFbButton.BackColor = Color.Red;
                installApkFbButton.Text = "Đang install FB";
                List<Thread> list = new List<Thread>();
                Task t = new Task(() =>
                {
                    for (int k = 0; k < numberOfThread; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            if (deviceID.ToLower().Contains("offline")) continue;

                            string fileName = openFileDialog1.FileName;
                            Thread T3 = new Thread(() => threadInstallApkFB(deviceID, fileName));
                            T3.IsBackground = true;
                            T3.Start();
                            list.Add(T3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                    }
                    foreach (Thread thread2 in list) thread2.Join();

                    MessageBox.Show("Finish install Apk");

                    installApkFbButton.Invoke(new MethodInvoker(() =>
                    {
                        installApkFbButton.Enabled = true;
                        installApkFbButton.BackColor = Color.Green;
                        installApkFbButton.Text = "Install Apk fb";
                    }));
                });
                t.Start();
            }
        }

        private void statusTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.statusTextbox = statusTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void verifiedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (verifiedCheckbox.Checked)
            {
                reinstallFbCheckBox.Checked = false;
                miniProfileCheckBox.Checked = false;
                doitenVncheckBox.Checked = false;
                //sleep1MinuteCheckBox.Checked = verifiedCheckbox.Checked;
                reupFullCheckBox.Checked = false;
                randomNewContactCheckBox.Checked = true;
                otp = 0;
                accDieCaptcha = 0;
                veriOk = 0;
                totalSucc = 0;
                regNvrOk = 0;
                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    listDeviceObject[i].blockCount = 0;
                    listDeviceObject[i].cycle = 0;
                    listDeviceObject[i].duration = 0;
                    listDeviceObject[i].globalSuccess = 0;
                    listDeviceObject[i].globalTotal = 0;
                    listDeviceObject[i].percentInHour = 0;
                    listDeviceObject[i].successInHour = 0;
                    listDeviceObject[i].totalInHour = 0;
                    listDeviceObject[i].installFb = true;
                    listDeviceObject[i].showVersion = true;
                }
                string startTime = DateTime.Now.ToString("HH:mm:ss tt");
                label2.Invoke(new MethodInvoker(() =>
                {
                    label2.Text = "Start:" + startTime;
                    Text = "Fb Reg - start" + " - " + startTime;
                }));


                totalLabel.Invoke(new MethodInvoker(() =>
                {
                    totalLabel.Text = totalSucc + " / " + regNvrOk + " - " + percent;
                    Text = "Fb Reg - start" + " - " + startTime;
                }));
            }
        }

        private void reImportContactButton_Click(object sender, EventArgs e)
        {
            reImportContactButton.Enabled = false;
            reImportContactButton.BackColor = Color.Red;
            reImportContactButton.Text = "Đang reimport";

            int numberOfThread = listDeviceObject.Count;
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < numberOfThread; k++)
                {
                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        if (deviceID.ToLower().Contains("offline"))
                        {
                            continue;
                        }

                        Thread T3 = new Thread(() => reImportContact(deviceID));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                }
                foreach (Thread thread2 in list)
                {
                    thread2.Join();
                }
                MessageBox.Show("Reimport contact finish");

                reImportContactButton.Invoke(new MethodInvoker(() =>
                {
                    reImportContactButton.Enabled = true;
                    reImportContactButton.BackColor = Color.Green;
                    reImportContactButton.Text = "Import Contact";
                }));
            });
            t.Start();
        }
        static void reImportContact(string deviceID)
        {
            try
            {
                Device.ClearContact(deviceID);
                // Delete file 
                Device.DeleteAllFileAlarms(deviceID);
                Random rn = new Random();

                DirectoryInfo di = new DirectoryInfo(@"data/sdt");
                FileInfo[] files = di.GetFiles("*.vcf");
                string str = "";
                foreach (FileInfo file in files) Console.WriteLine(file.Name);

                string randomFilePath = files[rn.Next(0, files.Length)].FullName;
                Device.PushFile2Alarms(deviceID, randomFilePath);
                Thread.Sleep(1000);

                //Device.RebootDevice(deviceID);

                Device.OpenApp(deviceID, "com.android.documentsui");
                Thread.Sleep(3000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 8.7, 8.3); // click setting
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 28.0, 57.6);
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 24.1, 29.9); // click alarm

                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 29.8, 39.3); // Click file contact
                Thread.Sleep(3000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.0, 54.8); // Click ok

                Thread.Sleep(7000);

                Device.OpenApp(deviceID, "com.android.contacts");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string createProxyFile(Proxy proxy)
        {
            if (proxy == null || !proxy.hasProxy)
            {
                return "";
            }
            System.IO.Directory.CreateDirectory("data/proxy");

            string fileName = "data/proxy/" + proxy.port + ".txt";
            try
            {
                File.Delete(fileName);
            }
            catch (IOException ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }
            string _vcf = "socks5://" + proxy.username + ":" + proxy.pass + "@" + proxy.host + ":" + proxy.port + " \"" +  proxy.port + "\" *";
            File.AppendAllText(fileName, "# superproxy:proxylist:v1\n");
            File.AppendAllText(fileName, _vcf);
            return fileName;
        }

        public bool randomProxy(Proxy proxy, string deviceID)
        {
            Device.DeleteAllFileMusic(deviceID);
            string randomFilePath = createProxyFile(proxy);

            if (string.IsNullOrEmpty(randomFilePath)) return false;
            Device.PushFile2Sdcard(deviceID, randomFilePath, proxy.port + ".txt");
            File.Delete(randomFilePath);
            
            return true;
        }

        public string RandomContactFile(string deviceID)
        {
            
            System.IO.Directory.CreateDirectory("data/contact");
            string fileName = "data/contact/" + deviceID + ".vcf";
            try
            {
                File.Delete(fileName);
            }
            catch (IOException ex)
            {
                Console.WriteLine("ex:" + ex.Message);
            }
            string _vcf = "";
            int ranso = new Random().Next(numberContact, numberContact + 30);
            Random ran = new Random();
            for (int i = 0; i < ranso; i++)
            {
                string phone = contacts[ran.Next(0, contacts.Count - 1)];
                string name = Ten_Nu[ran.Next(0, Ten_Nu.Count - 1)] + " " + i;
                _vcf += "BEGIN:VCARD\n";
                _vcf += "VERSION:2.1\n";
                _vcf += "FN:" + name + "\n";
                _vcf += "TEL;CELL:" + phone + "\n";
                _vcf += "END:VCARD\n";
            }
            File.AppendAllText(fileName, _vcf);
            return fileName;
        }
        

        public void RandomContact(string deviceID)
        {
            try
            {
                Device.DeleteTxtSdcard(deviceID);
                string temp = deviceID;
                if (deviceID.Contains(":"))
                {
                    temp = deviceID.Replace(":", ".");
                }
                Device.ClearContact(deviceID);
                Device.DeleteAllFilePictures(deviceID);
                Random rn = new Random();
                string randomFilePath = "";
                if (randomOldContactCheckBox.Checked)
                {
                    DirectoryInfo di = new DirectoryInfo(@"data/sdt");
                    FileInfo[] files = di.GetFiles("*.vcf");
                    string str = "";
                    foreach (FileInfo file in files)
                    {
                        Console.WriteLine(file.Name);
                    }
                    randomFilePath = files[rn.Next(0, files.Length)].FullName;
                }
                if (randomNewContactCheckBox.Checked || chayuploadContactcheckBox.Checked)
                {
                   
                    randomFilePath = RandomContactFile(temp);
                }
                    

                if (string.IsNullOrEmpty(randomFilePath)) return;
                Device.PushFile2Sdcard(deviceID, randomFilePath, deviceID + ".vcf");

                Device.ClearCache(deviceID, "com.android.documentsui");
                    

                Device.OpenApp(deviceID, "com.android.documentsui");
                CheckTextExist(deviceID, "gầnđây", 4);
                Device.PortraitRotate(deviceID);
                Device.TapByPercent(deviceID, 8.7, 8.3, 1000); // click setting

                if (!WaitAndTapXML(deviceID, 2, "còntrốngresource"))
                {
                    if (!WaitAndTapXML(deviceID, 1, "smg"))
                    {
                        Device.TapByPercent(deviceID, 28.0, 57.6, 1000); // click to SM-G
                    }

                }

                //Device.TapByPercent(deviceID, 24.1, 29.9, 1000); // click alarm
                //WaitAndTapXML(deviceID, 1, "picturesresource");
                //WaitAndTapXML(deviceID, 2, deviceID);
                if (!WaitAndTapXML(deviceID, 2, temp + "vcfresourcei"))
                {
                    Device.TapByPercent(deviceID, 31.6, 78.7, 1000);// Click file contact
                }

                if (!WaitAndTapXML(deviceID, 2, "ok"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 84.0, 54.8); // Click ok
                }

                Thread.Sleep(1000);

                Device.OpenApp(deviceID, "com.android.contacts");
                File.Delete(randomFilePath);
                Thread.Sleep(1000);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void clearContact(string deviceID)
        {
            try
            {
                Device.ClearContact(deviceID);
                Device.OpenApp(deviceID, "com.android.contacts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void clearContactButton_Click(object sender, EventArgs e)
        {
            int numberOfThread = listDeviceObject.Count;
            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < numberOfThread; k++)
                {
                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        if (deviceID.ToLower().Contains("offline"))
                        {
                            continue;
                        }

                        Thread T3 = new Thread(() => clearContact(deviceID));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
                foreach (Thread thread2 in list) thread2.Join();

                MessageBox.Show("Reimport contact finish");
            });
            t.Start();
        }

        private void tinProxyTextBox_TextChanged(object sender, EventArgs e)
        {

            Properties.Settings.Default.tinproxyKey = tinProxyTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void serverPathTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.serverapi = serverPathTextBox.Text;
            ServerApi.SERVER_HOST = serverPathTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void serverOnlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (serverOnlineCheckBox.Checked)
            {
                ServerApi.SERVER_HOST = serverPathTextBox.Text;
            }
            else
            {
                ServerApi.SERVER_HOST = "http://192.168.1.32";
            }
        }

        private void getInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                string info = Device.GetDeviceInfo(selectedDeviceID);
                System.IO.File.WriteAllText(selectedDeviceID + ".txt", info);
            });
            t.Start();

        }

        private void call101ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Device.MakePhoneCall(selectedDeviceID, "\"*101%23\"");
            });
            t.Start();

        }

        private void tmProxyTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.tmProxyKey = tmProxyTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void change2Ip4_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    if (listDeviceObject[i].deviceId == selectedDeviceID)
                    {
                        listDeviceObject[i].action = Constant.ACTION_CHANGE2IP4;
                        //listDeviceObject[i].currentIPType = Constant.ACTION_CHANGE2IP4;
                        return;
                    }
                }
            });
            t.Start();
        }

        private void change2Ip6_Click(object sender, EventArgs e)
        {

            Task t = new Task(() =>
            {
                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    if (listDeviceObject[i].deviceId == selectedDeviceID)
                    {
                        listDeviceObject[i].action = Constant.ACTION_CHANGE2IP6;
                        //listDeviceObject[i].currentIPType = Constant.ACTION_CHANGE2IP6;
                        return;
                    }
                }
            });
            t.Start();
        }
        private void rebootCMDtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {

                Device.RebootByCmd(selectedDeviceID);
            });
            t.Start();
        }
        private void getxmltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                string info = GetUIXml(selectedDeviceID);
                System.IO.File.WriteAllText("local\\" + selectedDeviceID + "XML.txt", info);
                Device.ScreenShoot(selectedDeviceID, false, "capture_.png");
            });
            t.Start();
        }
        private void captureScreentoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Device.ScreenShoot(selectedDeviceID, false, "capture_.png");
            });
            t.Start();
        }
        private void button4G_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].action = Constant.ACTION_CHANGE2IP4G;
                    listDeviceObject[k].newSim = Constant.ACTION_CHANGE2IP4G;
                }
            });
            t.Start();
        }

        private void button3G_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].action = Constant.ACTION_CHANGE2IP3G;
                    listDeviceObject[k].newSim = Constant.ACTION_CHANGE2IP3G;
                }
            });
            t.Start();
        }

        private void onAllbutton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {

                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_ON_ALL;
                }
            });
            t.Start();
        }

        private void offAllbutton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {

                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_OFF_ALL;
                }
            });
            t.Start();
        }

        private void startStoptimer_Tick(object sender, EventArgs e)
        {
            DateTime t1 = DateTime.Now;
            DateTime t2 = t1;
            DateTime t3 = t1;
            DateTime t4 = t1;
            DateTime t5 = t1;
            t2 = t2.Date.AddHours(1).AddMinutes(1);
            t3 = t3.Date.AddHours(1).AddMinutes(15);

            if ((t1.TimeOfDay > t2.TimeOfDay)
            && (t1.TimeOfDay < t3.TimeOfDay)
            )
            {
                //for (int k = 0; k < listDeviceObject.Count; k++)
                //{
                //    listDeviceObject[k].reInstallFbAfterChangeName = true;

                //}
                if (chạyDoiTenDemcheckBox.Checked)
                {
                    for (int k = 0; k < listDeviceObject.Count; k++)
                    {
                        listDeviceObject[k].installFb449 = true;
                        listDeviceObject[k].showVersion = true;
                    }
                    doitenVncheckBox.Invoke(new MethodInvoker(() =>
                    {
                        doitenVncheckBox.Checked = true;
                    }));
                }
                else
                {
                    if (autoRunVeriCheckBox.Checked)
                    {
                        holdingCheckBox.Invoke(new MethodInvoker(() =>
                        {
                            holdingCheckBox.Checked = true;
                        }));
                    }
                }
            }



            t4 = t4.Date.AddHours(6).AddMinutes(0);
            t5 = t5.Date.AddHours(6).AddMinutes(15);
            if ((t1.TimeOfDay > t4.TimeOfDay)
                && (t1.TimeOfDay < t5.TimeOfDay)
                )
            {
                holdingCheckBox.Invoke(new MethodInvoker(() =>
                {
                    holdingCheckBox.Checked = false;
                }));
                //doitenVncheckBox.Invoke(new MethodInvoker(() =>
                //{
                //    doitenVncheckBox.Checked = false;
                //}));
                //reupFullCheckBox.Invoke(new MethodInvoker(() =>
                //{
                //    reupFullCheckBox.Checked = false;
                //}));

                //verifiedCheckbox.Invoke(new MethodInvoker(() =>
                //{
                //    verifiedCheckbox.Checked = true;
                //}));

                getHotmailKieumoicheckBox.Invoke(new MethodInvoker(() =>
                {
                    getHotmailKieumoicheckBox.Checked = true;
                }));


                //for (int k = 0; k < listDeviceObject.Count; k++)
                //{
                //    if (listDeviceObject[k].reInstallFbAfterChangeName)
                //    {
                //        //listDeviceObject[k].installFb = true;
                //        listDeviceObject[k].showVersion = true;
                //        //listDeviceObject[k].reInstallFbAfterChangeName = false;
                //    }

                //}

            }
        }

        private void randPhone2Typetimer_Tick(object sender, EventArgs e)
        {
            if (randPhone2TypecheckBox.Checked) randomPhoneCheckBox.Checked = !randomPhoneCheckBox.Checked;
        }

        private void countAccMoiTimer_Tick(object sender, EventArgs e)
        {
            //int count = accMois.Count(D => D.Value == 1);
            //accMoilabel.Text = count + "/" + accMois.Count;
        }

        private void dtProxyTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.dtProxy = dtProxyTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void dtProxyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //if (dtProxyRadioButton.Checked) sockDroidCheckBox.Checked = true;
        }

        private void vinaphoneCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (vinaphoneCheckbox.Checked) phoneTypeLabel.Text = "Số ĐT: Vinaphone";

            if (!vinaphoneCheckbox.Checked && !viettelCheckBox.Checked && !mobiphoneCheckBox.Checked && !vietnamMobileCheckBox.Checked)
            {
                phoneTypeLabel.Text = "";
            }
        }

        private void viettelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (viettelCheckBox.Checked) phoneTypeLabel.Text = "Số ĐT: Viettel";

            if (!vinaphoneCheckbox.Checked && !viettelCheckBox.Checked && !mobiphoneCheckBox.Checked && !vietnamMobileCheckBox.Checked)
            {
                phoneTypeLabel.Text = "";
            }
        }

        private void vietnamMobileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (vietnamMobileCheckBox.Checked) phoneTypeLabel.Text = "Số ĐT: Vietnammobi";

            if (!vinaphoneCheckbox.Checked && !viettelCheckBox.Checked && !mobiphoneCheckBox.Checked && !vietnamMobileCheckBox.Checked)
            {
                phoneTypeLabel.Text = "";
            }
        }

        private void mobiphoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mobiphoneCheckBox.Checked) phoneTypeLabel.Text = "Số ĐT: Mobiphone";

            if (!vinaphoneCheckbox.Checked && !viettelCheckBox.Checked && !mobiphoneCheckBox.Checked && !vietnamMobileCheckBox.Checked)
            {
                phoneTypeLabel.Text = "";
            }
        }

        private void viewScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                Device.ViewScreen(selectedDeviceID);
            });
            t.Start();
        }

        private void randomNewContactCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            LoadAccMoi();
        }

        private void accMoilabel_Click(object sender, EventArgs e)
        {
            //int count = accMois.Count(D => D.Value == 1);
            //accMoilabel.Text = count + "/" + accMois.Count;
        }

        private void checkVeriTimer_Tick(object sender, EventArgs e)
        {
            if (!checkDieStopCheckBox.Checked) return;

            if (!verifyAccNvrCheckBox.Checked) return;

            int countFail = 0;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                Color c = dataGridView.Rows[i].DefaultCellStyle.BackColor;

                if (c.Equals(Color.Cyan) || c.Equals(Color.Yellow) || c.Equals(Color.DarkMagenta))
                {
                    countFail++;
                }
            }
            double percent = (Convert.ToDouble(countFail) / dataGridView.Rows.Count) * 100;

            int defaultPercent = 50;
            try
            {
                defaultPercent = Convert.ToInt32(percentVeriFailTextBox.Text);
            }
            catch (Exception ex)
            {

            }
            if (percent > defaultPercent) verifyAccNvrCheckBox.Checked = false;
        }
        private void generatorEmailradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (generatorEmailradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void tempmailLolradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (tempmailLolradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void dichvuGmailradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (dichvuGmailradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void sellGmailradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sellGmailradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void fakemailgeneratorradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fakemailgeneratorradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void fakeEmailradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fakeEmailradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void resetDuoiMailtimer_Tick(object sender, EventArgs e)
        {
            activeDuoiMail = "";
            activeDuoiMailtextBox.Invoke(new MethodInvoker(() =>
            {
                activeDuoiMailtextBox.Text = "Random Đuôi Mail";
            }));
        }

        private void MailOtpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (MailOtpRadioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void dichvugmail2radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (dichvugmail2radioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void timerAvailableSellGmail_Tick(object sender, EventArgs e)
        {
            isAvailableSellGmail = true;
        }

        private void buttonTurnOnSimSubcriber_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.TURN_ON_SIM_SUBCRIBE;
                }
            });
            t.Start();
        }

        private void accMoiFbLitecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (moiFbLitecheckBox.Checked)
            {
                //moiKatanacheckBox.Checked = false;
                moiBusinesscheckBox.Checked = false;
            }
        }

        private void moiKatanacheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (moiKatanacheckBox.Checked)
            {
                moiFbLitecheckBox.Checked = false;
                moiBusinesscheckBox.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Utility.forceStopGetOtp = thoatOtpcheckBox.Checked;
        }

        private void UninstallMessenger_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to uninstall facebook app?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Task t = new Task(() =>
                {
                    try
                    {
                        for (int i = 0; i < listDeviceObject.Count; i++)
                        {
                            string deviceID = listDeviceObject[i].deviceId;
                            try
                            {
                                Device.Uninstall(deviceID, Constant.MESSENGER_PACKAGE);
                                //Thread.Sleep(200);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }
                        }
                        MessageBox.Show("Uninstall facebook finish");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
                t.Start();
            }
        }

        private void uninstallbusinessbutton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to uninstall Business app?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Task t = new Task(() =>
                {
                    try
                    {
                        for (int i = 0; i < listDeviceObject.Count; i++)
                        {
                            string deviceID = listDeviceObject[i].deviceId;
                            try
                            {
                                Device.Uninstall(deviceID, Constant.FACEBOOK_BUSINESS_PACKAGE);
                                //Thread.Sleep(200);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }
                        }
                        MessageBox.Show("Uninstall Business finish");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
                t.Start();
            }
        }

        private void moiBusinesscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (moiBusinesscheckBox.Checked)
            {
                moiKatanacheckBox.Checked = false;
                accMoiCheckBox.Checked = false;
                moiFbLitecheckBox.Checked = false;
            }
        }

        private void pushFileChargerbutton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    Device.PushChargerFile(listDeviceObject[k].deviceId);
                }
            });
            t.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn bật Changer 60 ?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                int numberOfThread = listDeviceObject.Count;

                installApkFbButton.Enabled = false;
                installApkFbButton.BackColor = Color.Red;
                installApkFbButton.Text = "Đang install FB";
                List<Thread> list = new List<Thread>();
                Task t = new Task(() =>
                {
                    for (int k = 0; k < numberOfThread; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            if (deviceID.ToLower().Contains("offline")) continue;

                            Thread T3 = new Thread(() => BatChanger60(deviceID));
                            T3.IsBackground = true;
                            T3.Start();
                            list.Add(T3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                    }
                    foreach (Thread thread2 in list) thread2.Join();

                    MessageBox.Show("Bật changer xong");

                    installApkFbButton.Invoke(new MethodInvoker(() =>
                    {
                        installApkFbButton.Enabled = true;
                        installApkFbButton.BackColor = Color.Green;
                        installApkFbButton.Text = "Bật changer xong";
                    }));
                });
                t.Start();
            }
        }
        public void BatChanger60(string deviceID)
        {
            try
            {
                if (deviceID.Contains("line") || deviceID.Contains("rec"))
                {
                    return;
                }
                if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
                {
                    //LogStatus(device, "Screen is locking screen - Opening it");
                    Device.Unlockphone(deviceID);
                }
                //LogStatus(device, "Bật expose");
                Device.OpenApp(deviceID, Constant.EXPOSE_PACKAGE);
                Thread.Sleep(2000);
                Device.TapByPercent(deviceID, 6.9, 7.7, 1500);
                Device.TapByPercent(deviceID, 27.5, 29.5, 1500);

                WaitAndTapXML(deviceID, 2, "tắtresourceidorgmeowcatedxposedmanager"); // Bật changer
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 85.1, 6.5);
                Thread.Sleep(1000);
                WaitAndTapXML(deviceID, 2, "chophépresourceid");

                if (tatcaiconlaicheckBox.Checked && CheckTextExist(deviceID, "deviceemulatorpro", 1))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.8, 15.4);
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 85.1, 6.5);
                Thread.Sleep(1000);
                Device.RebootDevice(deviceID);
                if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
                {
                    //LogStatus(device, "Screen is locking screen - Opening it");
                    Device.Unlockphone(deviceID);
                }
                Device.OpenApp(deviceID, "com.phoneinfo.changerpro");
                Thread.Sleep(2000);
                WaitAndTapXML(deviceID, 1, "okresource");
                WaitAndTapXML(deviceID, 2, "chophépresource");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn bật Changer 60 ?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                int numberOfThread = listDeviceObject.Count;

                installApkFbButton.Enabled = false;
                installApkFbButton.BackColor = Color.Red;
                installApkFbButton.Text = "Đang install FB";
                List<Thread> list = new List<Thread>();
                Task t = new Task(() =>
                {
                    for (int k = 0; k < numberOfThread; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;
                            if (deviceID.ToLower().Contains("offline")) continue;


                            Thread T3 = new Thread(() => BatChangerEmu(deviceID));
                            T3.IsBackground = true;
                            T3.Start();
                            list.Add(T3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                    }
                    foreach (Thread thread2 in list) thread2.Join();

                    MessageBox.Show("Bật changer xong");

                    installApkFbButton.Invoke(new MethodInvoker(() =>
                    {
                        installApkFbButton.Enabled = true;
                        installApkFbButton.BackColor = Color.Green;
                        installApkFbButton.Text = "Bật changer xong";
                    }));
                });
                t.Start();

            }
        }
        public void BatChangerEmu(string deviceID)
        {
            if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
            {
                Device.Unlockphone(deviceID);
            }

            Device.OpenApp(deviceID, Constant.EXPOSE_PACKAGE);
            Thread.Sleep(2000);
            Device.TapByPercent(deviceID, 6.9, 7.7, 1500);
            Device.TapByPercent(deviceID, 27.5, 29.5, 1500);

            WaitAndTapXML(deviceID, 2, "tắtresourceidorgmeowcatedxposedmanager"); // Bật changer
            Thread.Sleep(1000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 85.1, 6.5);
            Thread.Sleep(1000);
            WaitAndTapXML(deviceID, 2, "chophépresourceid");

            if (tatcaiconlaicheckBox.Checked && CheckTextExist(deviceID, "meowcat", 1))
            {
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.2, 33.0);
            }
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 85.1, 6.5);
            Thread.Sleep(1000);
            Device.RebootDevice(deviceID);
            if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
            {

                Device.Unlockphone(deviceID);
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (openFbByDeepLinkcheckBox1.Checked)
            {
                moiFbLitecheckBox.Checked = true;
                moiKatanacheckBox.Checked = false;
            }
        }

        private void serverCacheMailTextbox_TextChanged(object sender, EventArgs e)
        {
            SERVER_LOCAL = serverCacheMailTextbox.Text;
            Properties.Settings.Default.serverCacheLocal = serverCacheMailTextbox.Text;

            Properties.Settings.Default.Save();
        }

        private void releaseNoteLabel_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Ping myPing = new Ping();
            //PingReply reply = myPing.Send("192.168.1.32", 30);

            //if (reply.Status.ToString() != "Success")
            //{
            serverOnlineCheckBox.Checked = true;
            //}
            string value = GoogleSheet.GetValue("report", "B2:B2");
            if (!string.IsNullOrEmpty(value))
            {
                serverCacheMailTextbox.Text = value.Trim();
                SERVER_LOCAL = serverCacheMailTextbox.Text;
            }
            string serverHost = GoogleSheet.GetValue("report", "C3:C3");
            if (!string.IsNullOrEmpty(serverHost))
            {
                serverPathTextBox.Text = serverHost.Trim();
            }
            string serverIp = ServerApi.GetServerIp(serverCacheMailTextbox.Text);
            if (!string.IsNullOrEmpty(serverIp) && serverIp != serverPathTextBox.Text)
            {
                serverPathTextBox.Text = serverIp;
            }
        }

        private void fixPasswordtextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FixPassword = fixPasswordtextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void fixPasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gmailOtpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (gmailOtpRadioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void FastProxyTextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.FastproxyKey = FastProxyTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void fastProxyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fastProxyRadioButton.Checked)
            {
                sockDroidCheckBox.Checked = true;
                setFastProxybutton.Text = "Set FastProxy";
            }
            else
            {
                sockDroidCheckBox.Checked = false;
            }
        }

        private void setFastProxybutton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn set FastProxy ?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                int numberOfThread = listDeviceObject.Count;

                installApkFbButton.Enabled = false;
                installApkFbButton.BackColor = Color.Red;
                installApkFbButton.Text = "Đang Set Fast Proxy";
                List<Thread> list = new List<Thread>();
                //Task t = new Task(() =>
                //{
                for (int k = 0; k < numberOfThread; k++)
                {
                    try
                    {
                        DeviceObject device = listDeviceObject[k];
                        if (device.deviceId.ToLower().Contains("offline")) continue;

                        Thread T3 = new Thread(() => SetProxySuperProxy(device));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                }
                foreach (Thread thread2 in list) thread2.Join();

                MessageBox.Show("Bật changer xong");

                installApkFbButton.Invoke(new MethodInvoker(() =>
                {
                    installApkFbButton.Enabled = true;
                    installApkFbButton.BackColor = Color.Green;
                    installApkFbButton.Text = "Bật changer xong";
                }));
                //});
                //t.Start();
            }
        }
        public void SetFastProxySockDroid(DeviceObject device)
        {
            try
            {
                string deviceID = device.deviceId;
                if (deviceID.Contains("line") || deviceID.Contains("rec"))
                {
                    return;
                }
                Device.RebootDevice(deviceID);
                Thread.Sleep(1000);
                FbUtil.SetProxySockDroid(deviceID, device.proxy);
            }
            catch (Exception ex)
            {

            }

        }

        public bool SetProxySuperProxy(DeviceObject device)
        {
            try
            {
                string deviceID = device.deviceId;
                
                if (deviceID.Contains("line") || deviceID.Contains("rec"))
                {
                    return false;
                }
                if (device.proxy == null || !device.proxy.hasProxy)
                {
                    return false;
                }

                //Device.RebootDevice(deviceID);
                randomProxy(device.proxy, deviceID);
                LogStatus(device, "Bắt đầu Chạy set proxy ------------");
                Device.RemoveProxy(deviceID);
                Device.ClearCache(deviceID, "com.scheler.superproxy");

            
                Device.OpenApp(deviceID, "com.scheler.superproxy");

                for (int i = 0; i < 3; i ++)
                {
                    string xml = GetUIXml(deviceID);

                    if (WaitAndTapXML(deviceID, 1, "import proxies", xml))
                    {
                        break;
                    }
                    if (WaitAndTapXML(deviceID, 3, "proxies&#10;tab1of3checkable", xml))
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    
                }
                
                if (!WaitAndTapXML(deviceID, 2, "cho phép resourceid"))
                {
                    WaitAndTapXML(deviceID, 1, "allow resourceid");
                }

                Thread.Sleep(1000);
                // Chọn folder 
                Device.TapByPercent(deviceID, 8.7, 8.3, 1000); // click setting
                
                if  (!WaitAndTapXML(deviceID, 2, "còntrốngresource")) {
                    if (!WaitAndTapXML(deviceID, 1, "smg"))
                    {
                        Device.TapByPercent(deviceID, 34.4, 33.9, 1000); ; // click to SM-G
                    }
                    
                }
                

                //Device.TapByPercent(deviceID, 24.1, 29.9, 1000); // click alarm
                //if (!WaitAndTapXML(deviceID, 1, "musicresource"))
                //{
                //    return false;
                //}
                if (!WaitAndTapXML(deviceID, 2, device.proxy.port))
                {
                    return false;
                }
                Device.DeleteTxtSdcard(deviceID);
                
                if (!WaitAndTapXML(deviceID, 2, "socks5checkable"))
                {
                    return false;
                }
                if (!WaitAndTapXML(deviceID, 2, "startcheckable"))
                {
                    
                    LogStatus(device, "Không thấy nút start proxy - set proxy lỗi");
                    Thread.Sleep(6000);
                    return false;
                }
                
                Device.AdbConnect(deviceID);
                
                
                Thread.Sleep(1000);
                WaitAndTapXML(deviceID, 1, "okresourceid");
                LogStatus(device, "Set proxy ok");

                if (!CheckTextExist(deviceID, "stopcheckable", 5))
                {
                    LogStatus(device, "Không thấy nút stop - khong start proxy duoc proxy - set proxy lỗi", 6000);
                    return false;
                }
                Device.Home(deviceID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool EditProxySuperProxy(DeviceObject device)
        {
            try
            {
                string deviceID = device.deviceId;
                if (deviceID.Contains("line") || deviceID.Contains("rec"))
                {
                    return false;
                }
                if (device.proxy == null || !device.proxy.hasProxy)
                {
                    return false;
                }

                LogStatus(device, "Bắt đầu Chạy Edit proxy ------------");
                //Device.ForceStop(deviceID, "com.scheler.superproxy");
                Device.OpenApp(deviceID, "com.scheler.superproxy");
                //WaitAndTapXML(deviceID, 3, "proxies&#10;tab1of3checkable");


                //if (WaitAndTapXML(deviceID, 2, "socks5checkable"))
                //{
                //    LogStatus(device, "Set proxy giao diện mới", 3000);
                //}
                WaitAndTapXML(deviceID, 1, "stopcheckable");
                // open success
                if (!CheckTextExist(deviceID, "proxies", 4))
                {
                    //Thread.Sleep(6000);
                    return false;
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.3, 7.3);// tap edit 
                Thread.Sleep(800);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 81.0, 47.4); // port
                Thread.Sleep(1000);
                Device.DeleteChars(deviceID, 6);
                Device.DeleteAllChars(deviceID);
                Device.DeleteAllChars(deviceID);
                //Thread.Sleep(1000);
                InputText(deviceID, device.proxy.port, true);
                //Thread.Sleep(1000);

                KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.5, 95.3); // tiếp
                Thread.Sleep(1000);


                LogStatus(device, "Bấm nút save");
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 93.8, 7.5); // save
                Thread.Sleep(1000);

                if (!WaitAndTapXML(deviceID, 2, "startcheckable"))
                {
                    LogStatus(device, "Không thấy nút start proxy - set proxy lỗi");
                    Thread.Sleep(6000);
                    return false;
                }
                //Thread.Sleep(1000);
                WaitAndTapXML(deviceID, 1, "okresourceid");
                LogStatus(device, "Đã bấm nút start proxy ----------", 1000);

                if (!CheckTextExist(deviceID, "stopcheckable", 4))
                {
                    LogStatus(device, "Không thấy nút stop - Start proxy thất bại - EDIT proxy lỗi", 6000);
                    return false;
                }
                Device.Home(deviceID);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void superTeamRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (superTeamRadioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (zuesProxyradioButton.Checked)
            {
                setFastProxybutton.Text = "Set ZuesProxy";
                superProxycheckBox.Checked = true;
            }
            else
            {
                sockDroidCheckBox.Checked = false;
            }
        }

        private void impulseradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (impulseradioButton.Checked)
            {
                sockDroidCheckBox.Checked = true;
                setFastProxybutton.Text = "Set ZuesProxy";
                changeSimCheckBox.Checked = false;
            }
            else
            {
                sockDroidCheckBox.Checked = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadDataInit();


            List<Thread> list = new List<Thread>();
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    try
                    {
                        string deviceID = listDeviceObject[k].deviceId;
                        string cmd = string.Format(Device.CONSOLE_ADB + " reboot", deviceID, mailTextbox.Text);
                        if (deviceID.ToLower().Contains("offline")) continue;

                        Thread T3 = new Thread(() => RunCMD(deviceID, cmd));
                        T3.IsBackground = true;
                        T3.Start();
                        list.Add(T3);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                }
                foreach (Thread thread2 in list) thread2.Join();

                //MessageBox.Show("Finish Run cmd:" + mailTextbox.Text);
                executeAdbButton.Invoke(new MethodInvoker(() =>
                {
                    executeAdbButton.Enabled = true;
                }));

            });
            t.Start();
            Thread.Sleep(65000);
            ProcessWithThreadPoolMethodAsync();
        }

        private void removeProxy2checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (removeProxy2checkBox.Checked)
            {
                zuesProxyradioButton.Checked = false;
                //superTeamRadioButton.Checked = false;
                s5ProxyradioButton.Checked = false;

            }
        }

        private void gmail48hradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (gmail48hradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void zuesProxyKeytextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.zuesProxyKey = zuesProxyKeytextBox.Text;
            Properties.Settings.Default.Save();
            zuesProxyKey = zuesProxyKeytextBox.Text;
        }

        private void doitenVncheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (doitenVncheckBox.Checked)
            {
                randomRegVericheckBox.Checked = false;
                moiFbLitecheckBox.Checked = false;
                loginByUserPassCheckBox.Checked = true;
                reinstallFbCheckBox.Checked = true;
                reupFullCheckBox.Checked = true;
                ResetCount();
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].installFb449 = true;
                    listDeviceObject[k].showVersion = true;
                }
            }
            else
            {
                reupFullCheckBox.Checked = false;
                loginByUserPassCheckBox.Checked = false;
            }
        }

        private void loadFBbutton_Click(object sender, EventArgs e)
        {
            LoadFbVersion();
        }
        public void LoadFbVersion()
        {
            fbVersioncomboBox.Items.Clear();
            string[] fileNames = Directory.GetFiles("data\\fb", "*.apk");
            if (fileNames != null && fileNames.Length > 0)
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    string fileName = fileNames[i].Replace("data\\fb\\", "");
                    fbVersioncomboBox.Items.Add(fileName);
                }

            }
            if (fbVersioncomboBox.Items.Count > 0)
            {
                if (fbVersioncomboBox.Items.Count > Properties.Settings.Default.selectedFbVersionIndex)
                {
                    fbVersioncomboBox.SelectedIndex = Properties.Settings.Default.selectedFbVersionIndex;
                }
                else
                {
                    fbVersioncomboBox.SelectedIndex = 0;
                }
            }

        }

        private void InstallFbbutton_Click(object sender, EventArgs e)
        {
            ResetCount();
            for (int k = 0; k < listDeviceObject.Count; k++)
            {
                listDeviceObject[k].installFb = true;
                listDeviceObject[k].showVersion = true;
            }
        }

        private void fbVersioncomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.selectedFbVersionIndex = fbVersioncomboBox.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void moiMessengercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (moiMessengercheckBox.Checked)
            {
               // moiKatanacheckBox.Checked = true;
            }
        }

        private void randomContrybutton_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].changeSim = true;
                    listDeviceObject[k].newSim = Constant.RANDOM_COUNTRY;
                }
            });
            t.Start();
        }

        private void holdingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (holdingCheckBox.Checked)
            {
                zuesProxyradioButton.Checked = false;
                //superTeamRadioButton.Checked = false;
                s5ProxyradioButton.Checked = false;
            }
        }

        private void waitAndTapbutton_Click(object sender, EventArgs e)
        {
            string deviceID = listDeviceObject[0].deviceId;
            string xml = GetUIXml(deviceID);
            WaitAndTapXML(deviceID, 1, testTaptextBox.Text);
            xmltextBox.Text = xml;
        }

        private void s5ProxyradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (s5ProxyradioButton.Checked)
            {
                superProxycheckBox.Checked = true;
            }
        }

        private void tinsoftRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (tinsoftRadioButton.Checked)
            {
                superProxycheckBox.Checked = false;
            }
        }

        private void proxyFromServercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (proxyFromServercheckBox.Checked)
            {
                superProxycheckBox.Checked = true;
                s5ProxyradioButton.Checked = true;

                for (int i = 0; i < listDeviceObject.Count; i++)
                {
                    listDeviceObject[i].loadNewProxy = false;
                }
                removeProxy2checkBox.Checked = false;
            }
            else
            {
                zuesProxyradioButton.Checked = false;
                s5ProxyradioButton.Checked = false;
            }
        }

        private void GetRealPhonebutton_Click(object sender, EventArgs e)
        {
            System.IO.Directory.CreateDirectory("data/device_info");
            System.IO.Directory.CreateDirectory("data/device_info/sms");
            System.IO.DirectoryInfo di = new DirectoryInfo("data/device_info/sms/");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            if (MessageBox.Show("Bạn có muốn Kiểm tra thông tin điện thoại ?", "Cảnh Báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                int numberOfThread = listDeviceObject.Count;

                installApkFbButton.Enabled = false;
                installApkFbButton.BackColor = Color.Red;
                installApkFbButton.Text = "Đang Kiểm tra điện thoại";
                List<Thread> list = new List<Thread>();
                Task t = new Task(() =>
                {
                    for (int k = 0; k < numberOfThread; k++)
                    {
                        try
                        {
                            string deviceID = listDeviceObject[k].deviceId;

                            if (deviceID.ToLower().Contains("offline")) continue;

                            Thread T3 = new Thread(() => KiemTraDienThoai(deviceID));
                            T3.IsBackground = true;
                            T3.Start();
                            list.Add(T3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }

                    }
                    foreach (Thread thread2 in list) thread2.Join();

                    MessageBox.Show("Kiểm Tra Xong");

                    installApkFbButton.Invoke(new MethodInvoker(() =>
                    {
                        installApkFbButton.Enabled = true;
                        installApkFbButton.BackColor = Color.Green;
                        installApkFbButton.Text = "Kiểm tra xong";
                    }));
                });
                t.Start();
            }
        }

        public void KiemTraDienThoai(string deviceID)
        {
            try
            {
                //string deviceID = device.deviceId;
                if (deviceID.Contains("line") || deviceID.Contains("rec"))
                {
                    return;
                }
                if (Utility.isScreenLock(deviceID) && !holdingCheckBox.Checked)
                {
                    Device.Unlockphone(deviceID);
                }

                Device.AirplaneOff(deviceID);
                Thread.Sleep(5000);
                Device.MakePhoneCall(deviceID, "\"*101%23\"");

                Thread.Sleep(20000);
                string xml = GetUIXml(deviceID);
                string rawPhone = "";
                MatchCollection matchList = Regex.Matches(xml, pattern: "([\\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})");
                if (matchList != null && matchList.Count > 0)
                {
                    rawPhone = matchList[0].ToString();
                    var regex = new Regex(Regex.Escape("84"));
                    rawPhone = regex.Replace(rawPhone, "0", 1);
                    rawPhone.Replace("+", "");
                    string createText = rawPhone;
                    File.WriteAllText("data/device_info/" + deviceID + ".txt", createText);
                    for (int i = 0; i < listDeviceObject.Count; i++)
                    {
                        if (deviceID == listDeviceObject[i].deviceId)
                        {
                            dataGridView.Rows[listDeviceObject[i].index].Cells[15].Value = rawPhone;
                            break;
                        }
                    }
                    Device.ScreenShootRaw(deviceID, false, deviceID + "_phone.png");
                }

                if (!WaitAndTapXML(deviceID, 1, "hủyresourceid"))
                {
                    KAutoHelper.ADBHelper.TapByPercent(deviceID, 67.4, 61.4);
                }
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 94.1, 20.2);
                Thread.Sleep(1000);
                Device.ClearCache(deviceID, "com.android.messaging");
                Thread.Sleep(1000);
                Device.OpenApp(deviceID, "com.android.messaging");
                Thread.Sleep(2000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 89.5, 94.5); // thêm message
                Thread.Sleep(1000);
                string realSim = Device.GetRealSim(deviceID);
                if (realSim.Contains("Viettel"))
                {
                    Device.InputText(deviceID, "191");
                }
                else if (realSim.Contains("VINAPHONE"))
                {
                    Device.InputText(deviceID, "888");
                }
                else if (realSim.Contains("Mobifone"))
                {
                    Device.InputText(deviceID, "999");
                }
                else
                {
                    Device.InputText(deviceID, "789");
                }

                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 95.2); // xong
                Thread.Sleep(1000);
                KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.7, 95.2); // xong
                Thread.Sleep(5000);
                string fileName = deviceID + "_" + rawPhone + ".png";

                try
                {
                    Device.ScreenShootRaw(deviceID, false, deviceID + "_sms.png");

                    join2Image1_1(deviceID + "_phone.png", deviceID + "_sms.png", deviceID + "_join.png");

                    File.Move(deviceID + "_join.png", "data/device_info/sms/" + fileName);

                    File.Delete(deviceID + "_phone.png");
                    File.Delete(deviceID + "_sms.png");
                    File.Delete(deviceID + "_join.png");
                }
                catch
                {
                }
                Device.disableMobileData(deviceID);
            }
            catch (Exception ex)
            {

            }

        }

        private void randomProxySim2checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (randomProxySim2checkBox.Checked)
            {
                removeProxy2checkBox.Checked = false;
            }
        }

        private void reinstallFbLiteCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].getIpType = true;
                }
            });
            t.Start();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].installLatestFb = true;
                }
            });
            t.Start();
        }

        private void chạyDoiTenDemcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chạyDoiTenDemcheckBox.Checked)
            {
                autoRunVeriCheckBox.Checked = false;
            }
        }

        private void napThetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                string tttt = "\"*100*" + mathetextBox.Text + "%23\"";

                //tttt = tttt.Replace("#", "%23");
                Device.MakePhoneCall(selectedDeviceID, tttt);
            });
            t.Start();
        }

        private void oneSecradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (oneSecradioButton.Checked) TempMailcheckBox.Checked = true;
        }

        private void updateFbVersionbutton_Click(object sender, EventArgs e)
        {
            ResetCount();
            for (int k = 0; k < listDeviceObject.Count; k++)
            {
                listDeviceObject[k].updateFb = true;
                listDeviceObject[k].showVersion = true;
            }
        }

        private void pushBuildPropbutton_Click(object sender, EventArgs e)
        {

        }

        private void UsLanguagecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (UsLanguagecheckBox.Checked)
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].change2UsLanguage = true;
                    listDeviceObject[k].change2VnLanguage = false;
                }
            } else
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    listDeviceObject[k].change2UsLanguage = false;
                    listDeviceObject[k].change2VnLanguage = true;
                }
            }
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    Device.ChangeLanguageUS(listDeviceObject[k].deviceId);
                }
            });
            t.Start();
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                for (int k = 0; k < listDeviceObject.Count; k++)
                {
                    Device.ChangeLanguageVN(listDeviceObject[k].deviceId);
                }
            });
            t.Start();
        }

        private void runLanbutton_Click(object sender, EventArgs e)
        {

        }

        private void runBoxLancheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runBoxLancheckBox.Checked)
            {
                List<string> listDevices___ = Device.GetLanDevices(ipRangeLantextBox.Text);
                List<string> listDevices = listDevices___;
                if (listDevices___ != null)
                {
                    //listDevices = listDevices___.OrderBy(q => q).ToList();
                    listDevices.Sort();

                }
                List<string> tamp = new List<string>();
                List<string> tamp2 = new List<string>();
                for (int i = 0; i < listDevices.Count; i++)
                {
                    if (listDevices[i].Length > 19)
                    {
                        tamp2.Add(listDevices[i]);

                    }
                    else
                    {
                        tamp.Add(listDevices[i]);
                    }
                }
                listDevices.Clear();
                for (int i = 0; i < tamp.Count; i++)
                {
                    listDevices.Add(tamp[i]);
                }
                for (int i = 0; i < tamp2.Count; i++)
                {
                    listDevices.Add(tamp2[i]);
                }

                if (listDevices != null && listDevices.Count > 0)
                {
                    selectedDevice = (string)listDevices[listDevices.Count - 1];
                    for (int i = 0; i < listDevices.Count; i++)
                    {

                        DeviceObject device = new DeviceObject();

                        string temp = listDevices[i];
                        device.deviceId = temp;
                        device.status = "Initial";
                        device.isFinish = true;
                        device.changeSim = false;
                        device.clearCacheLite = false;
                        device.clearCacheFailCount = 2;
                        device.clearCacheLiveCount = 2;
                        device.currentStatus = Constant.REG;
                        device.proxy = new Proxy();
                        listDeviceObject.Add(device);

                        device.allEmuStatus = Properties.Settings.Default.allEmuStatus;
                        device.simStatus = Properties.Settings.Default.simStatus;
                        device.emuStatus = Properties.Settings.Default.emuStatus;
                        device.numberClearAccSetting = 0;
                    }
                }

                InitialData(listDeviceObject);

                timeZoneComboBox.SelectedIndex = 0;
                shoplikeTextBox1.Text = Properties.Settings.Default.ShoplikeKey;
                tinsoftTextBox.Text = Properties.Settings.Default.TinsoftKey;
                FastProxyTextBox.Text = Properties.Settings.Default.FastproxyKey;
                tinProxyTextBox.Text = Properties.Settings.Default.tinproxyKey;
                dtProxyTextBox.Text = Properties.Settings.Default.dtProxy;
                drkDomainTextbox.Text = Properties.Settings.Default.drkDomain;
                fixPasswordtextBox.Text = Properties.Settings.Default.FixPassword;
                zuesProxyKeytextBox.Text = Properties.Settings.Default.zuesProxyKey;
                zuesProxyKey = Properties.Settings.Default.zuesProxyKey;

                prefixTextNow = File.ReadAllLines("prefix_phone_codetextnow.txt");
                CARRY_CODE = File.ReadAllLines("data/carrycode.txt").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                Control.CheckForIllegalCrossThreadCalls = false;
                foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            
        }
    }
}

