using System;
using System.Threading.Tasks;

namespace fb_reg
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.runAllBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.reportLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.InputEnglishNameCheckbox = new System.Windows.Forms.CheckBox();
            this.femaleCheckbox = new System.Windows.Forms.CheckBox();
            this.maleCheckbox = new System.Windows.Forms.CheckBox();
            this.set2faCheckbox = new System.Windows.Forms.CheckBox();
            this.runAvatarCheckbox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dausotextbox = new System.Windows.Forms.TextBox();
            this.resetStatus = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.yearOldFrom = new System.Windows.Forms.TextBox();
            this.yearOldTo = new System.Windows.Forms.TextBox();
            this.TempMailcheckBox = new System.Windows.Forms.CheckBox();
            this.downloadAvatarBtn = new System.Windows.Forms.Button();
            this.randomPrePhoneCheckbox = new System.Windows.Forms.CheckBox();
            this.noSuggestCheckbox = new System.Windows.Forms.CheckBox();
            this.delayTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fbLiteCheckbox = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.inputStringCheckbox = new System.Windows.Forms.CheckBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accInHalfHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalSuccess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunningCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Sim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.publicIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealSim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.turnSim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VeriStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.simstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proxy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.verifyCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.addDeviceButton = new System.Windows.Forms.Button();
            this.regByMailCheckBox = new System.Windows.Forms.CheckBox();
            this.verifiedCheckbox = new System.Windows.Forms.CheckBox();
            this.maxAccContinueTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeBreakTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mailTextbox = new System.Windows.Forms.TextBox();
            this.getCodeButton = new System.Windows.Forms.Button();
            this.codeLabel = new System.Windows.Forms.Label();
            this.airplaneCheckBox = new System.Windows.Forms.CheckBox();
            this.proxyCheckBox = new System.Windows.Forms.CheckBox();
            this.totalLabel = new System.Windows.Forms.Label();
            this.usePhoneLocalCheckBox = new System.Windows.Forms.CheckBox();
            this.installApk = new System.Windows.Forms.Button();
            this.changeDeviceEmuCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ssidTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.wifiPassTextBox = new System.Windows.Forms.TextBox();
            this.setWifiButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.rebootAllbutton = new System.Windows.Forms.Button();
            this.timeZoneComboBox = new System.Windows.Forms.ComboBox();
            this.setTimeZoneButton = new System.Windows.Forms.Button();
            this.usPhoneCheckBox = new System.Windows.Forms.CheckBox();
            this.maxAccBlockRuntextBox = new System.Windows.Forms.TextBox();
            this.timebreakDeadLocktextBox = new System.Windows.Forms.TextBox();
            this.uninstallFbBtn = new System.Windows.Forms.Button();
            this.Block = new System.Windows.Forms.Label();
            this.viettelButton = new System.Windows.Forms.Button();
            this.vinaButton = new System.Windows.Forms.Button();
            this.vietnamButton = new System.Windows.Forms.Button();
            this.holdingCheckBox = new System.Windows.Forms.CheckBox();
            this.mobiButton = new System.Windows.Forms.Button();
            this.beelineButton = new System.Windows.Forms.Button();
            this.vietCheckbox = new System.Windows.Forms.CheckBox();
            this.rootAdbButton = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.androidIDCheckBox = new System.Windows.Forms.CheckBox();
            this.brightCheckBox = new System.Windows.Forms.CheckBox();
            this.addFriendCheckBox = new System.Windows.Forms.CheckBox();
            this.profileCheckBox = new System.Windows.Forms.CheckBox();
            this.airplaneEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.mailSuffixtextBox = new System.Windows.Forms.TextBox();
            this.miniProfileCheckBox = new System.Windows.Forms.CheckBox();
            this.turnOnSimButton = new System.Windows.Forms.Button();
            this.clearCacheCheckBox = new System.Windows.Forms.CheckBox();
            this.change2Ip4Button = new System.Windows.Forms.Button();
            this.runningCheckBox = new System.Windows.Forms.CheckBox();
            this.changeSimCheckBox = new System.Windows.Forms.CheckBox();
            this.viettelTeleButton = new System.Windows.Forms.Button();
            this.viettelMobileButton = new System.Windows.Forms.Button();
            this.vnMobiButton = new System.Windows.Forms.Button();
            this.clearFbLiteCheckBox = new System.Windows.Forms.CheckBox();
            this.forgotCheckBox = new System.Windows.Forms.CheckBox();
            this.change2Ip6Button = new System.Windows.Forms.Button();
            this.change2Ip46Button = new System.Windows.Forms.Button();
            this.unsignCheckBox = new System.Windows.Forms.CheckBox();
            this.vnVinaphoneButton = new System.Windows.Forms.Button();
            this.randomMailPhoneSimCheckBox = new System.Windows.Forms.CheckBox();
            this.verifyAccNvrCheckBox = new System.Windows.Forms.CheckBox();
            this.autoRunVeriCheckBox = new System.Windows.Forms.CheckBox();
            this.homeCheckBox = new System.Windows.Forms.CheckBox();
            this.checkFBInstalledBtn = new System.Windows.Forms.Button();
            this.randomMailPhoneCheckBox = new System.Windows.Forms.CheckBox();
            this.installMissingFBbutton = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.minSpeedTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.autoSpeedCheckBox = new System.Windows.Forms.CheckBox();
            this.speedlabel = new System.Windows.Forms.Label();
            this.autoVeriMailCheckBox = new System.Windows.Forms.CheckBox();
            this.turnoffSimButton = new System.Windows.Forms.Button();
            this.rmFbliteButton = new System.Windows.Forms.Button();
            this.randomPhoneCheckBox = new System.Windows.Forms.CheckBox();
            this.changeSimType2CheckBox = new System.Windows.Forms.CheckBox();
            this.randomAllSimCheckBox = new System.Windows.Forms.CheckBox();
            this.randomVeriCheckBox = new System.Windows.Forms.CheckBox();
            this.vietUsCheckBox = new System.Windows.Forms.CheckBox();
            this.veriHotmailCheckBox = new System.Windows.Forms.CheckBox();
            this.vinaphoneCheckbox = new System.Windows.Forms.CheckBox();
            this.viettelCheckBox = new System.Windows.Forms.CheckBox();
            this.mobiphoneCheckBox = new System.Windows.Forms.CheckBox();
            this.vietnamMobileCheckBox = new System.Windows.Forms.CheckBox();
            this.shoplikeTextBox1 = new System.Windows.Forms.TextBox();
            this.loadTinsoftButton = new System.Windows.Forms.Button();
            this.adbKeyCheckBox = new System.Windows.Forms.CheckBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.turnOffEmuButton = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.getInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.change2Ip4 = new System.Windows.Forms.ToolStripMenuItem();
            this.change2Ip6 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getxmltoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureScreentoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebootCmdtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Call101toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.napThetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDevicetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.veriContactCheckBox = new System.Windows.Forms.CheckBox();
            this.veriPhoneCheckBox = new System.Windows.Forms.CheckBox();
            this.nvrUpAvatarCheckBox = new System.Windows.Forms.CheckBox();
            this.textnowCheckbox = new System.Windows.Forms.CheckBox();
            this.checkLoginCheckBox = new System.Windows.Forms.CheckBox();
            this.coverCheckBox = new System.Windows.Forms.CheckBox();
            this.noveriCoverCheckBox = new System.Windows.Forms.CheckBox();
            this.loginByUserPassCheckBox = new System.Windows.Forms.CheckBox();
            this.addStatusCheckBox = new System.Windows.Forms.CheckBox();
            this.veriDirectByPhoneCheckBox = new System.Windows.Forms.CheckBox();
            this.americaPhoneCheckBox = new System.Windows.Forms.CheckBox();
            this.changeSimUsButton = new System.Windows.Forms.Button();
            this.vietSimButton = new System.Windows.Forms.Button();
            this.micerCheckBox = new System.Windows.Forms.CheckBox();
            this.executeAdbButton = new System.Windows.Forms.Button();
            this.codeKeyTextNowTextBox = new System.Windows.Forms.TextBox();
            this.clearCacheFBcheckBox = new System.Windows.Forms.CheckBox();
            this.accMoiCheckBox = new System.Windows.Forms.CheckBox();
            this.cookieCodeTextNowtextBox = new System.Windows.Forms.TextBox();
            this.turnOnEmubutton = new System.Windows.Forms.Button();
            this.otpKeyTextBox = new System.Windows.Forms.TextBox();
            this.reportPhoneLabel = new System.Windows.Forms.Label();
            this.getPhoneCodeTextNowbutton = new System.Windows.Forms.Button();
            this.prefixTextNowCheckBox = new System.Windows.Forms.CheckBox();
            this.phoneInQueuelabel = new System.Windows.Forms.Label();
            this.veriMailAfterPhonecheckBox = new System.Windows.Forms.CheckBox();
            this.forceIp4CheckBox = new System.Windows.Forms.CheckBox();
            this.forceIp6checkBox = new System.Windows.Forms.CheckBox();
            this.reupFullCheckBox = new System.Windows.Forms.CheckBox();
            this.descriptionCheckBox = new System.Windows.Forms.CheckBox();
            this.drkKeyTextBox = new System.Windows.Forms.TextBox();
            this.drkCheckBox = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numberOfFriendRequestTextBox = new System.Windows.Forms.TextBox();
            this.showFbVersionCheckBox = new System.Windows.Forms.CheckBox();
            this.drkDomainTextbox = new System.Windows.Forms.TextBox();
            this.nvrByDeviceCheckBox = new System.Windows.Forms.CheckBox();
            this.startButtonGroupBox = new System.Windows.Forms.GroupBox();
            this.getDecisioncheckBox = new System.Windows.Forms.CheckBox();
            this.button17 = new System.Windows.Forms.Button();
            this.Rombutton = new System.Windows.Forms.Button();
            this.romgroupBox = new System.Windows.Forms.GroupBox();
            this.A13radioButton = new System.Windows.Forms.RadioButton();
            this.A9radioButton = new System.Windows.Forms.RadioButton();
            this.A11radioButton = new System.Windows.Forms.RadioButton();
            this.A10radioButton = new System.Windows.Forms.RadioButton();
            this.tuongtacnhecheckBox = new System.Windows.Forms.CheckBox();
            this.randomProxyDatacheckBox = new System.Windows.Forms.CheckBox();
            this.forcestopDiecheckBox = new System.Windows.Forms.CheckBox();
            this.android11checkBox = new System.Windows.Forms.CheckBox();
            this.chuyenVeri4gcheckBox = new System.Windows.Forms.CheckBox();
            this.giulaiportcheckBox = new System.Windows.Forms.CheckBox();
            this.proxySharecheckBox = new System.Windows.Forms.CheckBox();
            this.setMaxMailbutton = new System.Windows.Forms.Button();
            this.getMailcheckBox = new System.Windows.Forms.CheckBox();
            this.maxMailtextBox = new System.Windows.Forms.TextBox();
            this.maxMaillabel = new System.Windows.Forms.Label();
            this.virtualDevicetextBox = new System.Windows.Forms.TextBox();
            this.rootRom11checkBox = new System.Windows.Forms.CheckBox();
            this.docMailEducheckBox = new System.Windows.Forms.CheckBox();
            this.p2ProxycheckBox = new System.Windows.Forms.CheckBox();
            this.soLanLayMailtextBox = new System.Windows.Forms.TextBox();
            this.boquaProxyVncheckBox = new System.Windows.Forms.CheckBox();
            this.checkVericheckBox = new System.Windows.Forms.CheckBox();
            this.chaydocheckBox = new System.Windows.Forms.CheckBox();
            this.checkTopProxycheckBox = new System.Windows.Forms.CheckBox();
            this.chuyenKeyVnicheckBox = new System.Windows.Forms.CheckBox();
            this.uuTienChay4GcheckBox = new System.Windows.Forms.CheckBox();
            this.namServercheckBox = new System.Windows.Forms.CheckBox();
            this.InitialPhonecheckBox = new System.Windows.Forms.CheckBox();
            this.proxyKeycheckBox = new System.Windows.Forms.CheckBox();
            this.ipRangeLantextBox = new System.Windows.Forms.TextBox();
            this.showInfoDevicecheckBox = new System.Windows.Forms.CheckBox();
            this.UsLanguagecheckBox = new System.Windows.Forms.CheckBox();
            this.p3ProxycheckBox = new System.Windows.Forms.CheckBox();
            this.p1ProxycheckBox = new System.Windows.Forms.CheckBox();
            this.proxyWificheckBox = new System.Windows.Forms.CheckBox();
            this.proxyCMDcheckBox = new System.Windows.Forms.CheckBox();
            this.findPhonecheckBox = new System.Windows.Forms.CheckBox();
            this.changePhoneNumbercheckBox = new System.Windows.Forms.CheckBox();
            this.proxy4GcheckBox = new System.Windows.Forms.CheckBox();
            this.chạyDoiTenDemcheckBox = new System.Windows.Forms.CheckBox();
            this.getHotmailKieumoicheckBox = new System.Windows.Forms.CheckBox();
            this.moiKatanaNhanhcheckBox = new System.Windows.Forms.CheckBox();
            this.epMoiThanhCongcheckBox = new System.Windows.Forms.CheckBox();
            this.superProxycheckBox = new System.Windows.Forms.CheckBox();
            this.moiAccRegThanhCongcheckBox = new System.Windows.Forms.CheckBox();
            this.randomProxySim2checkBox = new System.Windows.Forms.CheckBox();
            this.proxyFromServercheckBox = new System.Windows.Forms.CheckBox();
            this.name3wordcheckBox = new System.Windows.Forms.CheckBox();
            this.gichuTrenAvatarcheckBox = new System.Windows.Forms.CheckBox();
            this.doitenVncheckBox = new System.Windows.Forms.CheckBox();
            this.nameUsVncheckBox = new System.Windows.Forms.CheckBox();
            this.nameVnUscheckBox = new System.Windows.Forms.CheckBox();
            this.forceGmailcheckBox = new System.Windows.Forms.CheckBox();
            this.button11 = new System.Windows.Forms.Button();
            this.showIpcheckBox = new System.Windows.Forms.CheckBox();
            this.reinstallSaudiecheckBox = new System.Windows.Forms.CheckBox();
            this.changer60checkBox = new System.Windows.Forms.CheckBox();
            this.clearAllAccSettingcheckBox = new System.Windows.Forms.CheckBox();
            this.thoatOtpcheckBox = new System.Windows.Forms.CheckBox();
            this.thoatGmailcheckBox = new System.Windows.Forms.CheckBox();
            this.storeAccMoicheckBox = new System.Windows.Forms.CheckBox();
            this.set2faLoai2checkBox = new System.Windows.Forms.CheckBox();
            this.veriaccgmailCheckBox = new System.Windows.Forms.CheckBox();
            this.randomIp46CheckBox = new System.Windows.Forms.CheckBox();
            this.veriAccRegMailcheckBox = new System.Windows.Forms.CheckBox();
            this.laymailkhaccheckBox = new System.Windows.Forms.CheckBox();
            this.regByGmailcheckBox = new System.Windows.Forms.CheckBox();
            this.checkDieStopCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkChangeIpcheckBox = new System.Windows.Forms.CheckBox();
            this.hvlgmailcheckBox = new System.Windows.Forms.CheckBox();
            this.shopgmailLocalcheckBox = new System.Windows.Forms.CheckBox();
            this.sptVipcheckBox = new System.Windows.Forms.CheckBox();
            this.sptLocalcheckBox = new System.Windows.Forms.CheckBox();
            this.dvgmNormalcheckBox = new System.Windows.Forms.CheckBox();
            this.thuesimVipcheckBox = new System.Windows.Forms.CheckBox();
            this.nghi5phutsaudiecheckBox = new System.Windows.Forms.CheckBox();
            this.nghi1phutsaudiecheckBox = new System.Windows.Forms.CheckBox();
            this.dvgmcheckVipBox = new System.Windows.Forms.CheckBox();
            this.otpCheapcheckBox = new System.Windows.Forms.CheckBox();
            this.thuesimcheckBox = new System.Windows.Forms.CheckBox();
            this.oneSecradioButton = new System.Windows.Forms.RadioButton();
            this.getSuperMailcheckBox = new System.Windows.Forms.CheckBox();
            this.getSellMailCheckbox = new System.Windows.Forms.CheckBox();
            this.getDvgmcheckBox = new System.Windows.Forms.CheckBox();
            this.superTeamRadioButton = new System.Windows.Forms.RadioButton();
            this.forceVeriAccRegBMailcheckBox = new System.Windows.Forms.CheckBox();
            this.gmail30minradioButton = new System.Windows.Forms.RadioButton();
            this.dichvugmail2radioButton = new System.Windows.Forms.RadioButton();
            this.MailOtpRadioButton = new System.Windows.Forms.RadioButton();
            this.fakeEmailradioButton = new System.Windows.Forms.RadioButton();
            this.fakemailgeneratorradioButton = new System.Windows.Forms.RadioButton();
            this.tempmailLolradioButton = new System.Windows.Forms.RadioButton();
            this.generatorEmailradioButton = new System.Windows.Forms.RadioButton();
            this.dichvuGmailradioButton = new System.Windows.Forms.RadioButton();
            this.sellGmailradioButton = new System.Windows.Forms.RadioButton();
            this.forceSellgmailcheckBox = new System.Windows.Forms.CheckBox();
            this.otplabel = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.randomNewContactCheckBox = new System.Windows.Forms.CheckBox();
            this.removeProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.inputStringMailCheckBox = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.locationProxyTextBox = new System.Windows.Forms.TextBox();
            this.forcePortraitCheckBox = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.reupRegCheckBox = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.maxLiveClearTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.delayAfterDieTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.maxFailClearTextBox = new System.Windows.Forms.TextBox();
            this.veriByProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.releaseNoteLabel = new System.Windows.Forms.Label();
            this.forceDungMayCheckBox = new System.Windows.Forms.CheckBox();
            this.avatarByCameraCheckBox = new System.Windows.Forms.CheckBox();
            this.delayAfterRegTextBox = new System.Windows.Forms.TextBox();
            this.orderGroupBox = new System.Windows.Forms.GroupBox();
            this.tamdungKiemTraAvatarcheckBox = new System.Windows.Forms.CheckBox();
            this.clearAccsettingsauregcheckBox = new System.Windows.Forms.CheckBox();
            this.uploadContactNewCheckbox = new System.Windows.Forms.CheckBox();
            this.coverNewcheckBox = new System.Windows.Forms.CheckBox();
            this.changeAllSim2checkBox = new System.Windows.Forms.CheckBox();
            this.gmail48hradioButton = new System.Windows.Forms.RadioButton();
            this.gmailOtpRadioButton = new System.Windows.Forms.RadioButton();
            this.sellGmailServerradioButton = new System.Windows.Forms.RadioButton();
            this.luuDuoiMailcheckBox = new System.Windows.Forms.CheckBox();
            this.getDuoiMailFromServercheckBox = new System.Windows.Forms.CheckBox();
            this.randomDuoiMailcheckBox = new System.Windows.Forms.CheckBox();
            this.veriBackupCheckBox = new System.Windows.Forms.CheckBox();
            this.runBoxLancheckBox = new System.Windows.Forms.CheckBox();
            this.chayuploadContactcheckBox = new System.Windows.Forms.CheckBox();
            this.statusSpeedlabel = new System.Windows.Forms.Label();
            this.alwaysChangeAirplaneCheckBox = new System.Windows.Forms.CheckBox();
            this.chuyenHotmailNhanhcheckBox = new System.Windows.Forms.CheckBox();
            this.otpVandongcheckBox = new System.Windows.Forms.CheckBox();
            this.getTrustMailcheckBox = new System.Windows.Forms.CheckBox();
            this.fastcheckBox = new System.Windows.Forms.CheckBox();
            this.rootRomcheckBox = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.fixDuoiMailTextBox = new System.Windows.Forms.TextBox();
            this.chayepdanbacheckBox = new System.Windows.Forms.CheckBox();
            this.choPutOtpcheckBox = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.mathetextBox = new System.Windows.Forms.TextBox();
            this.accDieCapchalabel = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.randPhone2TypecheckBox = new System.Windows.Forms.CheckBox();
            this.randomOldContactCheckBox = new System.Windows.Forms.CheckBox();
            this.setFastProxybutton = new System.Windows.Forms.Button();
            this.randomRegVericheckBox = new System.Windows.Forms.CheckBox();
            this.fixDuoiMailCheckBox = new System.Windows.Forms.CheckBox();
            this.openFbByDeepLinkcheckBox1 = new System.Windows.Forms.CheckBox();
            this.activeDuoiMailtextBox = new System.Windows.Forms.TextBox();
            this.phoneTypeLabel = new System.Windows.Forms.Label();
            this.fbInfoLabel = new System.Windows.Forms.Label();
            this.deviceFakerPlusCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.devicesTabPage = new System.Windows.Forms.TabPage();
            this.allcheckBox = new System.Windows.Forms.CheckBox();
            this.checkAllcheckBox = new System.Windows.Forms.CheckBox();
            this.errortextBox = new System.Windows.Forms.TextBox();
            this.settingTabPage = new System.Windows.Forms.TabPage();
            this.forceChangeWificheckBox = new System.Windows.Forms.CheckBox();
            this.randomWificheckBox = new System.Windows.Forms.CheckBox();
            this.loadWifiListbutton = new System.Windows.Forms.Button();
            this.wifiListtextBox = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.updateFbVersionbutton = new System.Windows.Forms.Button();
            this.boAccNhapMailSaicheckBox = new System.Windows.Forms.CheckBox();
            this.EpAccMoicheckBox = new System.Windows.Forms.CheckBox();
            this.GetRealPhonebutton = new System.Windows.Forms.Button();
            this.catProxySauVericheckBox = new System.Windows.Forms.CheckBox();
            this.xmltextBox = new System.Windows.Forms.TextBox();
            this.waitAndTapbutton = new System.Windows.Forms.Button();
            this.testTaptextBox = new System.Windows.Forms.TextBox();
            this.Country = new System.Windows.Forms.Label();
            this.countrytextBox = new System.Windows.Forms.TextBox();
            this.InstallFbbutton = new System.Windows.Forms.Button();
            this.loadFBbutton = new System.Windows.Forms.Button();
            this.fbVersioncomboBox = new System.Windows.Forms.ComboBox();
            this.uninstallbusinessbutton = new System.Windows.Forms.Button();
            this.UninstallMessenger = new System.Windows.Forms.Button();
            this.emailTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.outlookRadioButton = new System.Windows.Forms.RadioButton();
            this.hotmailRadioButton = new System.Windows.Forms.RadioButton();
            this.outlookDomainRadioButton = new System.Windows.Forms.RadioButton();
            this.carryCodecheckBox = new System.Windows.Forms.CheckBox();
            this.clearContactButton = new System.Windows.Forms.Button();
            this.dauso12CheckBox = new System.Windows.Forms.CheckBox();
            this.dauso12TextBox = new System.Windows.Forms.TextBox();
            this.proxyGroupBox = new System.Windows.Forms.GroupBox();
            this.proxyFromLocalcheckBox = new System.Windows.Forms.CheckBox();
            this.wwProxyradioButton = new System.Windows.Forms.RadioButton();
            this.tunProxyradioButton = new System.Windows.Forms.RadioButton();
            this.s5ProxyradioButton = new System.Windows.Forms.RadioButton();
            this.zuesProxy4G = new System.Windows.Forms.RadioButton();
            this.impulseradioButton = new System.Windows.Forms.RadioButton();
            this.zuesProxyradioButton = new System.Windows.Forms.RadioButton();
            this.FastProxyTextBox = new System.Windows.Forms.TextBox();
            this.fastProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.dtProxyTextBox = new System.Windows.Forms.TextBox();
            this.dtProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.tmProxyTextBox = new System.Windows.Forms.TextBox();
            this.tmProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.allowIpTextBox = new System.Windows.Forms.TextBox();
            this.tinProxyTextBox = new System.Windows.Forms.TextBox();
            this.tinProxyRadioButton = new System.Windows.Forms.RadioButton();
            this.removeProxyButton = new System.Windows.Forms.Button();
            this.tinsoftTextBox = new System.Windows.Forms.TextBox();
            this.tinsoftRadioButton = new System.Windows.Forms.RadioButton();
            this.shopLike1RadioButton = new System.Windows.Forms.RadioButton();
            this.controlGroupBox = new System.Windows.Forms.GroupBox();
            this.veriDirectGmailcheckBox = new System.Windows.Forms.CheckBox();
            this.reinstallBusinesscheckBox = new System.Windows.Forms.CheckBox();
            this.randomContrybutton = new System.Windows.Forms.Button();
            this.zuesProxyKeytextBox = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.numberClearAccSettingTextBox = new System.Windows.Forms.TextBox();
            this.dausoCheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.reinstallFbCountTextBox = new System.Windows.Forms.TextBox();
            this.reinstallFbCheckBox = new System.Windows.Forms.CheckBox();
            this.reinstallFbLiteCheckBox = new System.Windows.Forms.CheckBox();
            this.reinstallFbliteTextbox = new System.Windows.Forms.TextBox();
            this.installfblitecheckBox = new System.Windows.Forms.CheckBox();
            this.changeSimGroupBox = new System.Windows.Forms.GroupBox();
            this.offAllbutton = new System.Windows.Forms.Button();
            this.onAllbutton = new System.Windows.Forms.Button();
            this.button3G = new System.Windows.Forms.Button();
            this.button4G = new System.Windows.Forms.Button();
            this.networkSimGroupBox = new System.Windows.Forms.GroupBox();
            this.changeSim2Timer = new System.Windows.Forms.Timer(this.components);
            this.installFacebookButton = new System.Windows.Forms.Button();
            this.forceAvatarUsCheckBox = new System.Windows.Forms.CheckBox();
            this.sleep1MinuteCheckBox = new System.Windows.Forms.CheckBox();
            this.installApkFbButton = new System.Windows.Forms.Button();
            this.delayTimeTextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.randomProxySimCheckBox = new System.Windows.Forms.CheckBox();
            this.resendTextBox = new System.Windows.Forms.TextBox();
            this.resendCheckBox = new System.Windows.Forms.CheckBox();
            this.changeProxyDroidCheckBox = new System.Windows.Forms.CheckBox();
            this.changeProxyByCollegeCheckBox = new System.Windows.Forms.CheckBox();
            this.serverOnlineCheckBox = new System.Windows.Forms.CheckBox();
            this.serverPathTextBox = new System.Windows.Forms.TextBox();
            this.veriNvrBenNgoaiCheckBox = new System.Windows.Forms.CheckBox();
            this.startStoptimer = new System.Windows.Forms.Timer(this.components);
            this.randPhone2Typetimer = new System.Windows.Forms.Timer(this.components);
            this.openfblitecheckBox = new System.Windows.Forms.CheckBox();
            this.moiFbLitecheckBox = new System.Windows.Forms.CheckBox();
            this.removeAccFblitecheckBox = new System.Windows.Forms.CheckBox();
            this.accMoilabel = new System.Windows.Forms.Label();
            this.clearAccSettingcheckBox = new System.Windows.Forms.CheckBox();
            this.addAccSettingCheckBox = new System.Windows.Forms.CheckBox();
            this.forceReupContactCheckBox = new System.Windows.Forms.CheckBox();
            this.openMessengerCheckBox = new System.Windows.Forms.CheckBox();
            this.onOffSimCountTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.countAccMoiTimer = new System.Windows.Forms.Timer(this.components);
            this.sockDroidCheckBox = new System.Windows.Forms.CheckBox();
            this.set2faWebCheckBox = new System.Windows.Forms.CheckBox();
            this.percentVeriFailTextBox = new System.Windows.Forms.TextBox();
            this.checkVeriTimer = new System.Windows.Forms.Timer(this.components);
            this.label24 = new System.Windows.Forms.Label();
            this.resetDuoiMailtimer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timerAvailableSellGmail = new System.Windows.Forms.Timer(this.components);
            this.removeProxy2checkBox = new System.Windows.Forms.CheckBox();
            this.moiKatanacheckBox = new System.Windows.Forms.CheckBox();
            this.rebootFakerpluscheckBox = new System.Windows.Forms.CheckBox();
            this.installMissingMessengercheckBox = new System.Windows.Forms.CheckBox();
            this.moiBusinesscheckBox = new System.Windows.Forms.CheckBox();
            this.moiKhong2facheckBox = new System.Windows.Forms.CheckBox();
            this.getProyx20timecheckBox = new System.Windows.Forms.CheckBox();
            this.randomOnOffSimcheckBox = new System.Windows.Forms.CheckBox();
            this.forceMoiThanhCongcheckBox = new System.Windows.Forms.CheckBox();
            this.startWithtextBox = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.soLanChangeIptextBox = new System.Windows.Forms.TextBox();
            this.pushFileChargerbutton = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.tatcaiconlaicheckBox = new System.Windows.Forms.CheckBox();
            this.restartAfterchangecheckBox = new System.Windows.Forms.CheckBox();
            this.serverCacheMailTextbox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.fixPasswordtextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.fixPasswordCheckbox = new System.Windows.Forms.CheckBox();
            this.moiMessengercheckBox = new System.Windows.Forms.CheckBox();
            this.logProxycheckBox = new System.Windows.Forms.CheckBox();
            this.moiTruocProxycheckBox = new System.Windows.Forms.CheckBox();
            this.chuyenQuaMoiKatanacheckBox = new System.Windows.Forms.CheckBox();
            this.moiBusinessNhanhcheckBox = new System.Windows.Forms.CheckBox();
            this.randomMoicheckBox = new System.Windows.Forms.CheckBox();
            this.chuyenQuaveriGmailcheckBox = new System.Windows.Forms.CheckBox();
            this.randomVersioncheckBox = new System.Windows.Forms.CheckBox();
            this.button13 = new System.Windows.Forms.Button();
            this.randomVersionSaudiecheckBox = new System.Windows.Forms.CheckBox();
            this.randomVersionAfterverifailcheckBox = new System.Windows.Forms.CheckBox();
            this.ResetStatustimer = new System.Windows.Forms.Timer(this.components);
            this.chuyenquanvrcheckBox = new System.Windows.Forms.CheckBox();
            this.changeProxy2P1checkBox = new System.Windows.Forms.CheckBox();
            this.chuyenQuaHotmailcheckBox = new System.Windows.Forms.CheckBox();
            this.UpdateStatusSheettimer = new System.Windows.Forms.Timer(this.components);
            this.button16 = new System.Windows.Forms.Button();
            this.InforMailtimer = new System.Windows.Forms.Timer(this.components);
            this.forceRebootAfterClearcheckBox = new System.Windows.Forms.CheckBox();
            this.HideRootbutton = new System.Windows.Forms.Button();
            this.ScanDevicetimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.startButtonGroupBox.SuspendLayout();
            this.romgroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.orderGroupBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.devicesTabPage.SuspendLayout();
            this.settingTabPage.SuspendLayout();
            this.emailTypeGroupBox.SuspendLayout();
            this.proxyGroupBox.SuspendLayout();
            this.controlGroupBox.SuspendLayout();
            this.changeSimGroupBox.SuspendLayout();
            this.networkSimGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // runAllBtn
            // 
            this.runAllBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runAllBtn.ForeColor = System.Drawing.Color.Crimson;
            this.runAllBtn.Location = new System.Drawing.Point(7, 12);
            this.runAllBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.runAllBtn.Name = "runAllBtn";
            this.runAllBtn.Size = new System.Drawing.Size(132, 32);
            this.runAllBtn.TabIndex = 2;
            this.runAllBtn.Text = "Run All Normal";
            this.runAllBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.runAllBtn.UseVisualStyleBackColor = true;
            this.runAllBtn.Click += new System.EventHandler(this.RunAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(663, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 32);
            this.label2.TabIndex = 12;
            this.label2.Text = "Start Time:";
            this.label2.Visible = false;
            // 
            // reportLabel
            // 
            this.reportLabel.AutoSize = true;
            this.reportLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportLabel.Location = new System.Drawing.Point(70, 4);
            this.reportLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.reportLabel.Name = "reportLabel";
            this.reportLabel.Size = new System.Drawing.Size(20, 24);
            this.reportLabel.TabIndex = 13;
            this.reportLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 32);
            this.label1.TabIndex = 14;
            // 
            // status
            // 
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(32022, 3070);
            this.status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(4446, 863);
            this.status.TabIndex = 17;
            this.status.Text = "Status";
            this.status.Visible = false;
            // 
            // InputEnglishNameCheckbox
            // 
            this.InputEnglishNameCheckbox.AutoSize = true;
            this.InputEnglishNameCheckbox.Checked = true;
            this.InputEnglishNameCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.InputEnglishNameCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputEnglishNameCheckbox.ForeColor = System.Drawing.Color.Red;
            this.InputEnglishNameCheckbox.Location = new System.Drawing.Point(1814, 625);
            this.InputEnglishNameCheckbox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.InputEnglishNameCheckbox.Name = "InputEnglishNameCheckbox";
            this.InputEnglishNameCheckbox.Size = new System.Drawing.Size(229, 36);
            this.InputEnglishNameCheckbox.TabIndex = 19;
            this.InputEnglishNameCheckbox.Text = "English Name";
            this.InputEnglishNameCheckbox.UseVisualStyleBackColor = true;
            // 
            // femaleCheckbox
            // 
            this.femaleCheckbox.AutoSize = true;
            this.femaleCheckbox.Location = new System.Drawing.Point(509, 254);
            this.femaleCheckbox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.femaleCheckbox.Name = "femaleCheckbox";
            this.femaleCheckbox.Size = new System.Drawing.Size(103, 29);
            this.femaleCheckbox.TabIndex = 21;
            this.femaleCheckbox.Text = "Female";
            this.femaleCheckbox.UseVisualStyleBackColor = true;
            this.femaleCheckbox.CheckedChanged += new System.EventHandler(this.femaleCheckbox_CheckedChanged);
            // 
            // maleCheckbox
            // 
            this.maleCheckbox.AutoSize = true;
            this.maleCheckbox.Location = new System.Drawing.Point(509, 283);
            this.maleCheckbox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.maleCheckbox.Name = "maleCheckbox";
            this.maleCheckbox.Size = new System.Drawing.Size(81, 29);
            this.maleCheckbox.TabIndex = 20;
            this.maleCheckbox.Text = "Male";
            this.maleCheckbox.UseVisualStyleBackColor = true;
            this.maleCheckbox.CheckedChanged += new System.EventHandler(this.mailCheckBox_CheckedChanged_1);
            // 
            // set2faCheckbox
            // 
            this.set2faCheckbox.AutoSize = true;
            this.set2faCheckbox.Location = new System.Drawing.Point(5, 48);
            this.set2faCheckbox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.set2faCheckbox.Name = "set2faCheckbox";
            this.set2faCheckbox.Size = new System.Drawing.Size(65, 29);
            this.set2faCheckbox.TabIndex = 22;
            this.set2faCheckbox.Text = "2fa";
            this.set2faCheckbox.UseVisualStyleBackColor = true;
            this.set2faCheckbox.CheckedChanged += new System.EventHandler(this.set2faCheckbox_CheckedChanged);
            // 
            // runAvatarCheckbox
            // 
            this.runAvatarCheckbox.AutoSize = true;
            this.runAvatarCheckbox.Checked = true;
            this.runAvatarCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runAvatarCheckbox.Location = new System.Drawing.Point(5, 31);
            this.runAvatarCheckbox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.runAvatarCheckbox.Name = "runAvatarCheckbox";
            this.runAvatarCheckbox.Size = new System.Drawing.Size(95, 29);
            this.runAvatarCheckbox.TabIndex = 23;
            this.runAvatarCheckbox.Text = "Avatar";
            this.runAvatarCheckbox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 436);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "Đầu số";
            // 
            // dausotextbox
            // 
            this.dausotextbox.Location = new System.Drawing.Point(7, 745);
            this.dausotextbox.Margin = new System.Windows.Forms.Padding(2);
            this.dausotextbox.Name = "dausotextbox";
            this.dausotextbox.Size = new System.Drawing.Size(711, 29);
            this.dausotextbox.TabIndex = 26;
            this.dausotextbox.Text = "0394,0399,0393,0392,0965,0968,0325,0328,0336,0337,0332,0344,0870,0877,0878,0879,0" +
    "918,0964";
            this.dausotextbox.TextChanged += new System.EventHandler(this.dausotextbox_TextChanged);
            // 
            // resetStatus
            // 
            this.resetStatus.Location = new System.Drawing.Point(26762, 3010);
            this.resetStatus.Margin = new System.Windows.Forms.Padding(2);
            this.resetStatus.Name = "resetStatus";
            this.resetStatus.Size = new System.Drawing.Size(3551, 912);
            this.resetStatus.TabIndex = 27;
            this.resetStatus.Text = "Reset status";
            this.resetStatus.UseVisualStyleBackColor = true;
            this.resetStatus.Visible = false;
            this.resetStatus.Click += new System.EventHandler(this.resetStatus_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(690, 165);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 25);
            this.label5.TabIndex = 31;
            this.label5.Text = "Tuổi";
            this.label5.Visible = false;
            // 
            // yearOldFrom
            // 
            this.yearOldFrom.Location = new System.Drawing.Point(32022, 6399);
            this.yearOldFrom.Margin = new System.Windows.Forms.Padding(2);
            this.yearOldFrom.Name = "yearOldFrom";
            this.yearOldFrom.Size = new System.Drawing.Size(1476, 29);
            this.yearOldFrom.TabIndex = 32;
            this.yearOldFrom.Text = "1983";
            this.yearOldFrom.Visible = false;
            // 
            // yearOldTo
            // 
            this.yearOldTo.Location = new System.Drawing.Point(32022, 6399);
            this.yearOldTo.Margin = new System.Windows.Forms.Padding(2);
            this.yearOldTo.Name = "yearOldTo";
            this.yearOldTo.Size = new System.Drawing.Size(1368, 29);
            this.yearOldTo.TabIndex = 33;
            this.yearOldTo.Text = "1995";
            this.yearOldTo.Visible = false;
            // 
            // TempMailcheckBox
            // 
            this.TempMailcheckBox.AutoSize = true;
            this.TempMailcheckBox.ForeColor = System.Drawing.Color.Red;
            this.TempMailcheckBox.Location = new System.Drawing.Point(280, 144);
            this.TempMailcheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.TempMailcheckBox.Name = "TempMailcheckBox";
            this.TempMailcheckBox.Size = new System.Drawing.Size(193, 29);
            this.TempMailcheckBox.TabIndex = 34;
            this.TempMailcheckBox.Text = "Veri By TempMail";
            this.TempMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // downloadAvatarBtn
            // 
            this.downloadAvatarBtn.Location = new System.Drawing.Point(32022, 5851);
            this.downloadAvatarBtn.Margin = new System.Windows.Forms.Padding(2);
            this.downloadAvatarBtn.Name = "downloadAvatarBtn";
            this.downloadAvatarBtn.Size = new System.Drawing.Size(3855, 1092);
            this.downloadAvatarBtn.TabIndex = 35;
            this.downloadAvatarBtn.Text = "Download Avatar";
            this.downloadAvatarBtn.UseVisualStyleBackColor = true;
            this.downloadAvatarBtn.Click += new System.EventHandler(this.downloadAvatarBtn_Click);
            // 
            // randomPrePhoneCheckbox
            // 
            this.randomPrePhoneCheckbox.AutoSize = true;
            this.randomPrePhoneCheckbox.Checked = true;
            this.randomPrePhoneCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomPrePhoneCheckbox.Location = new System.Drawing.Point(509, 219);
            this.randomPrePhoneCheckbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.randomPrePhoneCheckbox.Name = "randomPrePhoneCheckbox";
            this.randomPrePhoneCheckbox.Size = new System.Drawing.Size(197, 29);
            this.randomPrePhoneCheckbox.TabIndex = 37;
            this.randomPrePhoneCheckbox.Text = "Phone from server";
            this.randomPrePhoneCheckbox.UseVisualStyleBackColor = true;
            // 
            // noSuggestCheckbox
            // 
            this.noSuggestCheckbox.AutoSize = true;
            this.noSuggestCheckbox.Location = new System.Drawing.Point(284, 169);
            this.noSuggestCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.noSuggestCheckbox.Name = "noSuggestCheckbox";
            this.noSuggestCheckbox.Size = new System.Drawing.Size(137, 29);
            this.noSuggestCheckbox.TabIndex = 39;
            this.noSuggestCheckbox.Text = "No suggest";
            this.noSuggestCheckbox.UseVisualStyleBackColor = true;
            // 
            // delayTextbox
            // 
            this.delayTextbox.Location = new System.Drawing.Point(32022, 5062);
            this.delayTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.delayTextbox.Name = "delayTextbox";
            this.delayTextbox.Size = new System.Drawing.Size(2006, 29);
            this.delayTextbox.TabIndex = 40;
            this.delayTextbox.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(597, 101);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 25);
            this.label7.TabIndex = 41;
            this.label7.Text = "Delay";
            // 
            // fbLiteCheckbox
            // 
            this.fbLiteCheckbox.AutoSize = true;
            this.fbLiteCheckbox.Location = new System.Drawing.Point(283, 75);
            this.fbLiteCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.fbLiteCheckbox.Name = "fbLiteCheckbox";
            this.fbLiteCheckbox.Size = new System.Drawing.Size(90, 29);
            this.fbLiteCheckbox.TabIndex = 42;
            this.fbLiteCheckbox.Text = "Fb lite";
            this.fbLiteCheckbox.UseVisualStyleBackColor = true;
            this.fbLiteCheckbox.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 7200000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // inputStringCheckbox
            // 
            this.inputStringCheckbox.AutoSize = true;
            this.inputStringCheckbox.Checked = true;
            this.inputStringCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inputStringCheckbox.Location = new System.Drawing.Point(394, 177);
            this.inputStringCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.inputStringCheckbox.Name = "inputStringCheckbox";
            this.inputStringCheckbox.Size = new System.Drawing.Size(152, 29);
            this.inputStringCheckbox.TabIndex = 43;
            this.inputStringCheckbox.Text = "Truyền chuỗi";
            this.inputStringCheckbox.UseVisualStyleBackColor = true;
            this.inputStringCheckbox.CheckedChanged += new System.EventHandler(this.inputStringCheckbox_CheckedChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView.ColumnHeadersHeight = 46;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.device,
            this.accInHalfHour,
            this.totalSuccess,
            this.Duration,
            this.StatusCol,
            this.RunningCol,
            this.Sim,
            this.publicIp,
            this.RealSim,
            this.fbVersion,
            this.turnSim,
            this.VeriStatus,
            this.RegStatus,
            this.simstatus,
            this.phoneNumber,
            this.Proxy,
            this.verifyCol});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(1, 1, 1, 10);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 80;
            this.dataGridView.RowTemplate.Height = 19;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1770, 798);
            this.dataGridView.TabIndex = 46;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgrdResults_CellMouseDown);
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
            this.dataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgrdResults_MouseClick);
            // 
            // No
            // 
            this.No.FillWeight = 6.984753F;
            this.No.HeaderText = "No";
            this.No.MinimumWidth = 10;
            this.No.Name = "No";
            this.No.Width = 30;
            // 
            // device
            // 
            this.device.FillWeight = 15.13624F;
            this.device.HeaderText = "DeviceID";
            this.device.MinimumWidth = 10;
            this.device.Name = "device";
            this.device.Width = 140;
            // 
            // accInHalfHour
            // 
            this.accInHalfHour.FillWeight = 19.69811F;
            this.accInHalfHour.HeaderText = "Acc In 120 min";
            this.accInHalfHour.MinimumWidth = 10;
            this.accInHalfHour.Name = "accInHalfHour";
            this.accInHalfHour.Width = 90;
            // 
            // totalSuccess
            // 
            this.totalSuccess.FillWeight = 25.68445F;
            this.totalSuccess.HeaderText = "Total Success";
            this.totalSuccess.MinimumWidth = 10;
            this.totalSuccess.Name = "totalSuccess";
            this.totalSuccess.Width = 90;
            // 
            // Duration
            // 
            this.Duration.FillWeight = 33.51242F;
            this.Duration.HeaderText = "Duration";
            this.Duration.MinimumWidth = 10;
            this.Duration.Name = "Duration";
            this.Duration.Width = 60;
            // 
            // StatusCol
            // 
            this.StatusCol.FillWeight = 162.4828F;
            this.StatusCol.HeaderText = "Status";
            this.StatusCol.MinimumWidth = 10;
            this.StatusCol.Name = "StatusCol";
            this.StatusCol.Width = 350;
            // 
            // RunningCol
            // 
            this.RunningCol.FalseValue = "False";
            this.RunningCol.FillWeight = 23.09947F;
            this.RunningCol.HeaderText = "Running";
            this.RunningCol.MinimumWidth = 9;
            this.RunningCol.Name = "RunningCol";
            this.RunningCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RunningCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.RunningCol.TrueValue = "True";
            this.RunningCol.Width = 30;
            // 
            // Sim
            // 
            this.Sim.FillWeight = 75.41367F;
            this.Sim.HeaderText = "Sim";
            this.Sim.MinimumWidth = 10;
            this.Sim.Name = "Sim";
            this.Sim.Width = 10;
            // 
            // publicIp
            // 
            this.publicIp.FillWeight = 376.8388F;
            this.publicIp.HeaderText = "Public IP";
            this.publicIp.MinimumWidth = 10;
            this.publicIp.Name = "publicIp";
            this.publicIp.Width = 200;
            // 
            // RealSim
            // 
            this.RealSim.FillWeight = 251.3097F;
            this.RealSim.HeaderText = "RealSim";
            this.RealSim.MinimumWidth = 10;
            this.RealSim.Name = "RealSim";
            this.RealSim.Width = 50;
            // 
            // fbVersion
            // 
            this.fbVersion.HeaderText = "Version";
            this.fbVersion.MinimumWidth = 8;
            this.fbVersion.Name = "fbVersion";
            this.fbVersion.Width = 150;
            // 
            // turnSim
            // 
            this.turnSim.HeaderText = "TurnSim";
            this.turnSim.MinimumWidth = 6;
            this.turnSim.Name = "turnSim";
            this.turnSim.Width = 30;
            // 
            // VeriStatus
            // 
            this.VeriStatus.HeaderText = "Veri Status";
            this.VeriStatus.MinimumWidth = 8;
            this.VeriStatus.Name = "VeriStatus";
            this.VeriStatus.Width = 10;
            // 
            // RegStatus
            // 
            this.RegStatus.HeaderText = "Reg Status";
            this.RegStatus.MinimumWidth = 8;
            this.RegStatus.Name = "RegStatus";
            this.RegStatus.Width = 450;
            // 
            // simstatus
            // 
            this.simstatus.HeaderText = "Sim Status";
            this.simstatus.MinimumWidth = 8;
            this.simstatus.Name = "simstatus";
            this.simstatus.Width = 10;
            // 
            // phoneNumber
            // 
            this.phoneNumber.HeaderText = "Phone";
            this.phoneNumber.MinimumWidth = 8;
            this.phoneNumber.Name = "phoneNumber";
            this.phoneNumber.Width = 10;
            // 
            // Proxy
            // 
            this.Proxy.HeaderText = "Proxy";
            this.Proxy.MinimumWidth = 8;
            this.Proxy.Name = "Proxy";
            this.Proxy.Width = 30;
            // 
            // verifyCol
            // 
            this.verifyCol.HeaderText = "Verify";
            this.verifyCol.MinimumWidth = 8;
            this.verifyCol.Name = "verifyCol";
            this.verifyCol.Width = 30;
            // 
            // addDeviceButton
            // 
            this.addDeviceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addDeviceButton.Location = new System.Drawing.Point(1949, 67);
            this.addDeviceButton.Margin = new System.Windows.Forms.Padding(2);
            this.addDeviceButton.Name = "addDeviceButton";
            this.addDeviceButton.Size = new System.Drawing.Size(131, 28);
            this.addDeviceButton.TabIndex = 47;
            this.addDeviceButton.Text = "Add Devices";
            this.addDeviceButton.UseVisualStyleBackColor = true;
            this.addDeviceButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // regByMailCheckBox
            // 
            this.regByMailCheckBox.AutoSize = true;
            this.regByMailCheckBox.Location = new System.Drawing.Point(10, 41);
            this.regByMailCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.regByMailCheckBox.Name = "regByMailCheckBox";
            this.regByMailCheckBox.Size = new System.Drawing.Size(139, 29);
            this.regByMailCheckBox.TabIndex = 48;
            this.regByMailCheckBox.Text = "Reg by mail";
            this.regByMailCheckBox.UseVisualStyleBackColor = true;
            // 
            // verifiedCheckbox
            // 
            this.verifiedCheckbox.AutoSize = true;
            this.verifiedCheckbox.Checked = true;
            this.verifiedCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.verifiedCheckbox.ForeColor = System.Drawing.Color.Red;
            this.verifiedCheckbox.Location = new System.Drawing.Point(538, 98);
            this.verifiedCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.verifiedCheckbox.Name = "verifiedCheckbox";
            this.verifiedCheckbox.Size = new System.Drawing.Size(104, 29);
            this.verifiedCheckbox.TabIndex = 49;
            this.verifiedCheckbox.Text = "Verified";
            this.verifiedCheckbox.UseVisualStyleBackColor = true;
            this.verifiedCheckbox.CheckedChanged += new System.EventHandler(this.verifiedCheckbox_CheckedChanged);
            // 
            // maxAccContinueTextBox
            // 
            this.maxAccContinueTextBox.Location = new System.Drawing.Point(190, 466);
            this.maxAccContinueTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.maxAccContinueTextBox.Name = "maxAccContinueTextBox";
            this.maxAccContinueTextBox.Size = new System.Drawing.Size(33, 29);
            this.maxAccContinueTextBox.TabIndex = 50;
            this.maxAccContinueTextBox.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 416);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 25);
            this.label3.TabIndex = 51;
            this.label3.Text = "Max";
            // 
            // timeBreakTextBox
            // 
            this.timeBreakTextBox.Location = new System.Drawing.Point(608, 503);
            this.timeBreakTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.timeBreakTextBox.Name = "timeBreakTextBox";
            this.timeBreakTextBox.Size = new System.Drawing.Size(52, 29);
            this.timeBreakTextBox.TabIndex = 52;
            this.timeBreakTextBox.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(668, 223);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 25);
            this.label8.TabIndex = 53;
            this.label8.Text = "Time break";
            // 
            // mailTextbox
            // 
            this.mailTextbox.Location = new System.Drawing.Point(411, 471);
            this.mailTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.mailTextbox.Name = "mailTextbox";
            this.mailTextbox.Size = new System.Drawing.Size(220, 29);
            this.mailTextbox.TabIndex = 54;
            // 
            // getCodeButton
            // 
            this.getCodeButton.Location = new System.Drawing.Point(302, 537);
            this.getCodeButton.Margin = new System.Windows.Forms.Padding(2);
            this.getCodeButton.Name = "getCodeButton";
            this.getCodeButton.Size = new System.Drawing.Size(116, 36);
            this.getCodeButton.TabIndex = 55;
            this.getCodeButton.Text = "Get Code";
            this.getCodeButton.UseVisualStyleBackColor = true;
            this.getCodeButton.Click += new System.EventHandler(this.GetCodeButton_Click);
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(330, 453);
            this.codeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(66, 25);
            this.codeLabel.TabIndex = 56;
            this.codeLabel.Text = "Code:";
            // 
            // airplaneCheckBox
            // 
            this.airplaneCheckBox.AutoSize = true;
            this.airplaneCheckBox.Checked = true;
            this.airplaneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.airplaneCheckBox.Location = new System.Drawing.Point(284, 121);
            this.airplaneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.airplaneCheckBox.Name = "airplaneCheckBox";
            this.airplaneCheckBox.Size = new System.Drawing.Size(110, 29);
            this.airplaneCheckBox.TabIndex = 57;
            this.airplaneCheckBox.Text = "Airplane";
            this.airplaneCheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyCheckBox
            // 
            this.proxyCheckBox.AutoSize = true;
            this.proxyCheckBox.Location = new System.Drawing.Point(509, 134);
            this.proxyCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.proxyCheckBox.Name = "proxyCheckBox";
            this.proxyCheckBox.Size = new System.Drawing.Size(88, 29);
            this.proxyCheckBox.TabIndex = 61;
            this.proxyCheckBox.Text = "Proxy";
            this.proxyCheckBox.UseVisualStyleBackColor = true;
            this.proxyCheckBox.Visible = false;
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("MS Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.ForeColor = System.Drawing.Color.Red;
            this.totalLabel.Location = new System.Drawing.Point(1796, 961);
            this.totalLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(60, 42);
            this.totalLabel.TabIndex = 63;
            this.totalLabel.Text = " 0";
            // 
            // usePhoneLocalCheckBox
            // 
            this.usePhoneLocalCheckBox.AutoSize = true;
            this.usePhoneLocalCheckBox.Location = new System.Drawing.Point(509, 183);
            this.usePhoneLocalCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.usePhoneLocalCheckBox.Name = "usePhoneLocalCheckBox";
            this.usePhoneLocalCheckBox.Size = new System.Drawing.Size(178, 29);
            this.usePhoneLocalCheckBox.TabIndex = 65;
            this.usePhoneLocalCheckBox.Text = "Use phone local";
            this.usePhoneLocalCheckBox.UseVisualStyleBackColor = true;
            // 
            // installApk
            // 
            this.installApk.ForeColor = System.Drawing.Color.Crimson;
            this.installApk.Location = new System.Drawing.Point(14, 469);
            this.installApk.Margin = new System.Windows.Forms.Padding(2);
            this.installApk.Name = "installApk";
            this.installApk.Size = new System.Drawing.Size(93, 38);
            this.installApk.TabIndex = 67;
            this.installApk.Text = "Install Apk";
            this.installApk.UseVisualStyleBackColor = true;
            this.installApk.Click += new System.EventHandler(this.InstallApk_Click);
            // 
            // changeDeviceEmuCheckBox
            // 
            this.changeDeviceEmuCheckBox.AutoSize = true;
            this.changeDeviceEmuCheckBox.Location = new System.Drawing.Point(395, 368);
            this.changeDeviceEmuCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.changeDeviceEmuCheckBox.Name = "changeDeviceEmuCheckBox";
            this.changeDeviceEmuCheckBox.Size = new System.Drawing.Size(213, 29);
            this.changeDeviceEmuCheckBox.TabIndex = 68;
            this.changeDeviceEmuCheckBox.Text = "Change device emu";
            this.changeDeviceEmuCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(627, 370);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 25);
            this.label6.TabIndex = 69;
            this.label6.Text = "Wifi";
            // 
            // ssidTextBox
            // 
            this.ssidTextBox.Location = new System.Drawing.Point(630, 380);
            this.ssidTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ssidTextBox.Name = "ssidTextBox";
            this.ssidTextBox.Size = new System.Drawing.Size(111, 29);
            this.ssidTextBox.TabIndex = 70;
            this.ssidTextBox.Text = "Caffe_Mon";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(690, 126);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 25);
            this.label9.TabIndex = 71;
            this.label9.Text = "Pass";
            // 
            // wifiPassTextBox
            // 
            this.wifiPassTextBox.Location = new System.Drawing.Point(630, 397);
            this.wifiPassTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.wifiPassTextBox.Name = "wifiPassTextBox";
            this.wifiPassTextBox.Size = new System.Drawing.Size(111, 29);
            this.wifiPassTextBox.TabIndex = 72;
            this.wifiPassTextBox.Text = "123456789";
            // 
            // setWifiButton
            // 
            this.setWifiButton.ForeColor = System.Drawing.Color.Blue;
            this.setWifiButton.Location = new System.Drawing.Point(629, 424);
            this.setWifiButton.Margin = new System.Windows.Forms.Padding(2);
            this.setWifiButton.Name = "setWifiButton";
            this.setWifiButton.Size = new System.Drawing.Size(99, 29);
            this.setWifiButton.TabIndex = 73;
            this.setWifiButton.Text = "Set Wifi";
            this.setWifiButton.UseVisualStyleBackColor = true;
            this.setWifiButton.Click += new System.EventHandler(this.setWifiButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(481, 530);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 36);
            this.button1.TabIndex = 74;
            this.button1.Text = "Remove Proxy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.removProxyButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1212, 518);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 74);
            this.button2.TabIndex = 75;
            this.button2.Text = "Open Screen All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(333, 584);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 27);
            this.button4.TabIndex = 76;
            this.button4.Text = "Change Emu All";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(478, 573);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(101, 31);
            this.button5.TabIndex = 77;
            this.button5.Text = "Set Proxy";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.buttonSetProxy_Click);
            // 
            // rebootAllbutton
            // 
            this.rebootAllbutton.Location = new System.Drawing.Point(601, 528);
            this.rebootAllbutton.Margin = new System.Windows.Forms.Padding(2);
            this.rebootAllbutton.Name = "rebootAllbutton";
            this.rebootAllbutton.Size = new System.Drawing.Size(100, 31);
            this.rebootAllbutton.TabIndex = 78;
            this.rebootAllbutton.Text = "Reboot All";
            this.rebootAllbutton.UseVisualStyleBackColor = true;
            this.rebootAllbutton.Click += new System.EventHandler(this.rebootAllbutton_Click);
            // 
            // timeZoneComboBox
            // 
            this.timeZoneComboBox.FormattingEnabled = true;
            this.timeZoneComboBox.Items.AddRange(new object[] {
            "America/Chicago",
            "Asia/Ho_Chi_Minh"});
            this.timeZoneComboBox.Location = new System.Drawing.Point(239, 470);
            this.timeZoneComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.timeZoneComboBox.Name = "timeZoneComboBox";
            this.timeZoneComboBox.Size = new System.Drawing.Size(159, 32);
            this.timeZoneComboBox.TabIndex = 79;
            // 
            // setTimeZoneButton
            // 
            this.setTimeZoneButton.Location = new System.Drawing.Point(151, 584);
            this.setTimeZoneButton.Margin = new System.Windows.Forms.Padding(2);
            this.setTimeZoneButton.Name = "setTimeZoneButton";
            this.setTimeZoneButton.Size = new System.Drawing.Size(144, 27);
            this.setTimeZoneButton.TabIndex = 80;
            this.setTimeZoneButton.Text = "Set Timezone";
            this.setTimeZoneButton.UseVisualStyleBackColor = true;
            this.setTimeZoneButton.Click += new System.EventHandler(this.setTimeZoneButton_Click);
            // 
            // usPhoneCheckBox
            // 
            this.usPhoneCheckBox.AutoSize = true;
            this.usPhoneCheckBox.Location = new System.Drawing.Point(394, 91);
            this.usPhoneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.usPhoneCheckBox.Name = "usPhoneCheckBox";
            this.usPhoneCheckBox.Size = new System.Drawing.Size(124, 29);
            this.usPhoneCheckBox.TabIndex = 81;
            this.usPhoneCheckBox.Text = "Us Phone";
            this.usPhoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // maxAccBlockRuntextBox
            // 
            this.maxAccBlockRuntextBox.Location = new System.Drawing.Point(339, 416);
            this.maxAccBlockRuntextBox.Margin = new System.Windows.Forms.Padding(2);
            this.maxAccBlockRuntextBox.Name = "maxAccBlockRuntextBox";
            this.maxAccBlockRuntextBox.Size = new System.Drawing.Size(111, 29);
            this.maxAccBlockRuntextBox.TabIndex = 82;
            this.maxAccBlockRuntextBox.Text = "6";
            // 
            // timebreakDeadLocktextBox
            // 
            this.timebreakDeadLocktextBox.Location = new System.Drawing.Point(32022, 3552);
            this.timebreakDeadLocktextBox.Margin = new System.Windows.Forms.Padding(2);
            this.timebreakDeadLocktextBox.Name = "timebreakDeadLocktextBox";
            this.timebreakDeadLocktextBox.Size = new System.Drawing.Size(1476, 29);
            this.timebreakDeadLocktextBox.TabIndex = 84;
            this.timebreakDeadLocktextBox.Text = "10";
            // 
            // uninstallFbBtn
            // 
            this.uninstallFbBtn.Location = new System.Drawing.Point(1072, 365);
            this.uninstallFbBtn.Margin = new System.Windows.Forms.Padding(2);
            this.uninstallFbBtn.Name = "uninstallFbBtn";
            this.uninstallFbBtn.Size = new System.Drawing.Size(110, 26);
            this.uninstallFbBtn.TabIndex = 85;
            this.uninstallFbBtn.Text = "Uninstall FB";
            this.uninstallFbBtn.UseVisualStyleBackColor = true;
            this.uninstallFbBtn.Click += new System.EventHandler(this.uninstallFbBtn_Click);
            // 
            // Block
            // 
            this.Block.AutoSize = true;
            this.Block.Location = new System.Drawing.Point(690, 146);
            this.Block.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Block.Name = "Block";
            this.Block.Size = new System.Drawing.Size(60, 25);
            this.Block.TabIndex = 86;
            this.Block.Text = "Block";
            // 
            // viettelButton
            // 
            this.viettelButton.Location = new System.Drawing.Point(7, 110);
            this.viettelButton.Margin = new System.Windows.Forms.Padding(2);
            this.viettelButton.Name = "viettelButton";
            this.viettelButton.Size = new System.Drawing.Size(90, 26);
            this.viettelButton.TabIndex = 87;
            this.viettelButton.Text = "Viettel";
            this.viettelButton.UseVisualStyleBackColor = true;
            this.viettelButton.Click += new System.EventHandler(this.change2Viettel_Click);
            // 
            // vinaButton
            // 
            this.vinaButton.Location = new System.Drawing.Point(103, 111);
            this.vinaButton.Margin = new System.Windows.Forms.Padding(2);
            this.vinaButton.Name = "vinaButton";
            this.vinaButton.Size = new System.Drawing.Size(90, 26);
            this.vinaButton.TabIndex = 89;
            this.vinaButton.Text = "VINA";
            this.vinaButton.UseVisualStyleBackColor = true;
            this.vinaButton.Click += new System.EventHandler(this.vinaButton_Click);
            // 
            // vietnamButton
            // 
            this.vietnamButton.Location = new System.Drawing.Point(7, 83);
            this.vietnamButton.Margin = new System.Windows.Forms.Padding(2);
            this.vietnamButton.Name = "vietnamButton";
            this.vietnamButton.Size = new System.Drawing.Size(90, 26);
            this.vietnamButton.TabIndex = 90;
            this.vietnamButton.Text = "VietnamM";
            this.vietnamButton.UseVisualStyleBackColor = true;
            this.vietnamButton.Click += new System.EventHandler(this.vietnamButton_Click);
            // 
            // holdingCheckBox
            // 
            this.holdingCheckBox.AutoSize = true;
            this.holdingCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.holdingCheckBox.ForeColor = System.Drawing.Color.Blue;
            this.holdingCheckBox.Location = new System.Drawing.Point(361, 128);
            this.holdingCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.holdingCheckBox.Name = "holdingCheckBox";
            this.holdingCheckBox.Size = new System.Drawing.Size(210, 43);
            this.holdingCheckBox.TabIndex = 91;
            this.holdingCheckBox.Text = "Tạm Dừng";
            this.holdingCheckBox.UseVisualStyleBackColor = true;
            this.holdingCheckBox.CheckedChanged += new System.EventHandler(this.holdingCheckBox_CheckedChanged);
            // 
            // mobiButton
            // 
            this.mobiButton.Location = new System.Drawing.Point(103, 82);
            this.mobiButton.Margin = new System.Windows.Forms.Padding(2);
            this.mobiButton.Name = "mobiButton";
            this.mobiButton.Size = new System.Drawing.Size(90, 26);
            this.mobiButton.TabIndex = 92;
            this.mobiButton.Text = "Mobi";
            this.mobiButton.UseVisualStyleBackColor = true;
            this.mobiButton.Click += new System.EventHandler(this.mobiButton_Click);
            // 
            // beelineButton
            // 
            this.beelineButton.Location = new System.Drawing.Point(103, 52);
            this.beelineButton.Margin = new System.Windows.Forms.Padding(2);
            this.beelineButton.Name = "beelineButton";
            this.beelineButton.Size = new System.Drawing.Size(90, 26);
            this.beelineButton.TabIndex = 93;
            this.beelineButton.Text = "Beeline";
            this.beelineButton.UseVisualStyleBackColor = true;
            this.beelineButton.Click += new System.EventHandler(this.beelineButton_Click);
            // 
            // vietCheckbox
            // 
            this.vietCheckbox.AutoSize = true;
            this.vietCheckbox.Checked = true;
            this.vietCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vietCheckbox.Location = new System.Drawing.Point(285, 249);
            this.vietCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.vietCheckbox.Name = "vietCheckbox";
            this.vietCheckbox.Size = new System.Drawing.Size(127, 29);
            this.vietCheckbox.TabIndex = 94;
            this.vietCheckbox.Text = "Tiếng Việt";
            this.vietCheckbox.UseVisualStyleBackColor = true;
            // 
            // rootAdbButton
            // 
            this.rootAdbButton.Location = new System.Drawing.Point(32022, 4240);
            this.rootAdbButton.Margin = new System.Windows.Forms.Padding(2);
            this.rootAdbButton.Name = "rootAdbButton";
            this.rootAdbButton.Size = new System.Drawing.Size(3855, 1092);
            this.rootAdbButton.TabIndex = 95;
            this.rootAdbButton.Text = "Root Adb";
            this.rootAdbButton.UseVisualStyleBackColor = true;
            this.rootAdbButton.Visible = false;
            this.rootAdbButton.Click += new System.EventHandler(this.rootAdbButton_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(32022, 8518);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(3389, 1092);
            this.button7.TabIndex = 96;
            this.button7.Text = "Init ChangeAID";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // androidIDCheckBox
            // 
            this.androidIDCheckBox.AutoSize = true;
            this.androidIDCheckBox.Checked = true;
            this.androidIDCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.androidIDCheckBox.Location = new System.Drawing.Point(284, 147);
            this.androidIDCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.androidIDCheckBox.Name = "androidIDCheckBox";
            this.androidIDCheckBox.Size = new System.Drawing.Size(130, 29);
            this.androidIDCheckBox.TabIndex = 97;
            this.androidIDCheckBox.Text = "Android ID";
            this.androidIDCheckBox.UseVisualStyleBackColor = true;
            // 
            // brightCheckBox
            // 
            this.brightCheckBox.AutoSize = true;
            this.brightCheckBox.Location = new System.Drawing.Point(10, 106);
            this.brightCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.brightCheckBox.Name = "brightCheckBox";
            this.brightCheckBox.Size = new System.Drawing.Size(85, 29);
            this.brightCheckBox.TabIndex = 98;
            this.brightCheckBox.Text = "Sáng";
            this.brightCheckBox.UseVisualStyleBackColor = true;
            this.brightCheckBox.CheckedChanged += new System.EventHandler(this.brightCheckBox_CheckedChanged);
            // 
            // addFriendCheckBox
            // 
            this.addFriendCheckBox.AutoSize = true;
            this.addFriendCheckBox.Location = new System.Drawing.Point(395, 233);
            this.addFriendCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.addFriendCheckBox.Name = "addFriendCheckBox";
            this.addFriendCheckBox.Size = new System.Drawing.Size(134, 29);
            this.addFriendCheckBox.TabIndex = 99;
            this.addFriendCheckBox.Text = "Add Friend";
            this.addFriendCheckBox.UseVisualStyleBackColor = true;
            // 
            // profileCheckBox
            // 
            this.profileCheckBox.AutoSize = true;
            this.profileCheckBox.Location = new System.Drawing.Point(395, 277);
            this.profileCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.profileCheckBox.Name = "profileCheckBox";
            this.profileCheckBox.Size = new System.Drawing.Size(92, 29);
            this.profileCheckBox.TabIndex = 100;
            this.profileCheckBox.Text = "Profile";
            this.profileCheckBox.UseVisualStyleBackColor = true;
            // 
            // airplaneEnableCheckBox
            // 
            this.airplaneEnableCheckBox.AutoSize = true;
            this.airplaneEnableCheckBox.Location = new System.Drawing.Point(285, 203);
            this.airplaneEnableCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.airplaneEnableCheckBox.Name = "airplaneEnableCheckBox";
            this.airplaneEnableCheckBox.Size = new System.Drawing.Size(113, 29);
            this.airplaneEnableCheckBox.TabIndex = 101;
            this.airplaneEnableCheckBox.Text = "Máy bay";
            this.airplaneEnableCheckBox.UseVisualStyleBackColor = true;
            this.airplaneEnableCheckBox.CheckedChanged += new System.EventHandler(this.airplaneEnableCheckBox_CheckedChanged);
            // 
            // mailSuffixtextBox
            // 
            this.mailSuffixtextBox.Location = new System.Drawing.Point(16, 620);
            this.mailSuffixtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.mailSuffixtextBox.Name = "mailSuffixtextBox";
            this.mailSuffixtextBox.Size = new System.Drawing.Size(111, 29);
            this.mailSuffixtextBox.TabIndex = 103;
            this.mailSuffixtextBox.Text = "@gmail.com";
            // 
            // miniProfileCheckBox
            // 
            this.miniProfileCheckBox.AutoSize = true;
            this.miniProfileCheckBox.Location = new System.Drawing.Point(396, 330);
            this.miniProfileCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.miniProfileCheckBox.Name = "miniProfileCheckBox";
            this.miniProfileCheckBox.Size = new System.Drawing.Size(133, 29);
            this.miniProfileCheckBox.TabIndex = 104;
            this.miniProfileCheckBox.Text = "Mini Profile";
            this.miniProfileCheckBox.UseVisualStyleBackColor = true;
            // 
            // turnOnSimButton
            // 
            this.turnOnSimButton.Location = new System.Drawing.Point(472, 410);
            this.turnOnSimButton.Margin = new System.Windows.Forms.Padding(2);
            this.turnOnSimButton.Name = "turnOnSimButton";
            this.turnOnSimButton.Size = new System.Drawing.Size(94, 30);
            this.turnOnSimButton.TabIndex = 105;
            this.turnOnSimButton.Text = "TurnOn Sim";
            this.turnOnSimButton.UseVisualStyleBackColor = true;
            this.turnOnSimButton.Click += new System.EventHandler(this.TurnOnSimButton_Click);
            // 
            // clearCacheCheckBox
            // 
            this.clearCacheCheckBox.AutoSize = true;
            this.clearCacheCheckBox.Checked = true;
            this.clearCacheCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearCacheCheckBox.Location = new System.Drawing.Point(285, 224);
            this.clearCacheCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.clearCacheCheckBox.Name = "clearCacheCheckBox";
            this.clearCacheCheckBox.Size = new System.Drawing.Size(148, 29);
            this.clearCacheCheckBox.TabIndex = 106;
            this.clearCacheCheckBox.Text = "Clear Cache";
            this.clearCacheCheckBox.UseVisualStyleBackColor = true;
            // 
            // change2Ip4Button
            // 
            this.change2Ip4Button.Location = new System.Drawing.Point(1796, 1003);
            this.change2Ip4Button.Margin = new System.Windows.Forms.Padding(2);
            this.change2Ip4Button.Name = "change2Ip4Button";
            this.change2Ip4Button.Size = new System.Drawing.Size(111, 32);
            this.change2Ip4Button.TabIndex = 107;
            this.change2Ip4Button.Text = "Change to Ip4";
            this.change2Ip4Button.UseVisualStyleBackColor = true;
            this.change2Ip4Button.Click += new System.EventHandler(this.change2Ip4Button_Click);
            // 
            // runningCheckBox
            // 
            this.runningCheckBox.AutoSize = true;
            this.runningCheckBox.Checked = true;
            this.runningCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runningCheckBox.Location = new System.Drawing.Point(889, 92);
            this.runningCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.runningCheckBox.Name = "runningCheckBox";
            this.runningCheckBox.Size = new System.Drawing.Size(22, 21);
            this.runningCheckBox.TabIndex = 108;
            this.runningCheckBox.UseVisualStyleBackColor = true;
            this.runningCheckBox.CheckedChanged += new System.EventHandler(this.runingCheckBox_CheckedChanged);
            // 
            // changeSimCheckBox
            // 
            this.changeSimCheckBox.AutoSize = true;
            this.changeSimCheckBox.Checked = true;
            this.changeSimCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.changeSimCheckBox.Location = new System.Drawing.Point(12, 196);
            this.changeSimCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.changeSimCheckBox.Name = "changeSimCheckBox";
            this.changeSimCheckBox.Size = new System.Drawing.Size(118, 29);
            this.changeSimCheckBox.TabIndex = 109;
            this.changeSimCheckBox.Text = "Auto Sim";
            this.changeSimCheckBox.UseVisualStyleBackColor = true;
            // 
            // viettelTeleButton
            // 
            this.viettelTeleButton.Location = new System.Drawing.Point(11, 111);
            this.viettelTeleButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.viettelTeleButton.Name = "viettelTeleButton";
            this.viettelTeleButton.Size = new System.Drawing.Size(111, 111);
            this.viettelTeleButton.TabIndex = 110;
            this.viettelTeleButton.Text = "Viettel Telecom";
            this.viettelTeleButton.UseVisualStyleBackColor = true;
            this.viettelTeleButton.Click += new System.EventHandler(this.viettelTeleButton_Click);
            // 
            // viettelMobileButton
            // 
            this.viettelMobileButton.Location = new System.Drawing.Point(7, 52);
            this.viettelMobileButton.Margin = new System.Windows.Forms.Padding(2);
            this.viettelMobileButton.Name = "viettelMobileButton";
            this.viettelMobileButton.Size = new System.Drawing.Size(90, 26);
            this.viettelMobileButton.TabIndex = 111;
            this.viettelMobileButton.Text = "Viettel Mobile";
            this.viettelMobileButton.UseVisualStyleBackColor = true;
            this.viettelMobileButton.Click += new System.EventHandler(this.viettelMobileButton_Click);
            // 
            // vnMobiButton
            // 
            this.vnMobiButton.Location = new System.Drawing.Point(104, 21);
            this.vnMobiButton.Margin = new System.Windows.Forms.Padding(2);
            this.vnMobiButton.Name = "vnMobiButton";
            this.vnMobiButton.Size = new System.Drawing.Size(90, 26);
            this.vnMobiButton.TabIndex = 112;
            this.vnMobiButton.Text = "VN mobi";
            this.vnMobiButton.UseVisualStyleBackColor = true;
            this.vnMobiButton.Click += new System.EventHandler(this.vnMobiButton_Click);
            // 
            // clearFbLiteCheckBox
            // 
            this.clearFbLiteCheckBox.AutoSize = true;
            this.clearFbLiteCheckBox.Location = new System.Drawing.Point(180, 268);
            this.clearFbLiteCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.clearFbLiteCheckBox.Name = "clearFbLiteCheckBox";
            this.clearFbLiteCheckBox.Size = new System.Drawing.Size(206, 29);
            this.clearFbLiteCheckBox.TabIndex = 115;
            this.clearFbLiteCheckBox.Text = "Allway Clear FbLite";
            this.clearFbLiteCheckBox.UseVisualStyleBackColor = true;
            // 
            // forgotCheckBox
            // 
            this.forgotCheckBox.AutoSize = true;
            this.forgotCheckBox.Location = new System.Drawing.Point(392, 15);
            this.forgotCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.forgotCheckBox.Name = "forgotCheckBox";
            this.forgotCheckBox.Size = new System.Drawing.Size(185, 29);
            this.forgotCheckBox.TabIndex = 116;
            this.forgotCheckBox.Text = "Forgot Password";
            this.forgotCheckBox.UseVisualStyleBackColor = true;
            this.forgotCheckBox.Visible = false;
            // 
            // change2Ip6Button
            // 
            this.change2Ip6Button.Location = new System.Drawing.Point(1798, 1077);
            this.change2Ip6Button.Margin = new System.Windows.Forms.Padding(2);
            this.change2Ip6Button.Name = "change2Ip6Button";
            this.change2Ip6Button.Size = new System.Drawing.Size(111, 31);
            this.change2Ip6Button.TabIndex = 117;
            this.change2Ip6Button.Text = "Change to Ip6";
            this.change2Ip6Button.UseVisualStyleBackColor = true;
            this.change2Ip6Button.Click += new System.EventHandler(this.change2Ip6Button_Click);
            // 
            // change2Ip46Button
            // 
            this.change2Ip46Button.Location = new System.Drawing.Point(1804, 1039);
            this.change2Ip46Button.Margin = new System.Windows.Forms.Padding(2);
            this.change2Ip46Button.Name = "change2Ip46Button";
            this.change2Ip46Button.Size = new System.Drawing.Size(105, 34);
            this.change2Ip46Button.TabIndex = 118;
            this.change2Ip46Button.Text = "Change to Ip4/6";
            this.change2Ip46Button.UseVisualStyleBackColor = true;
            this.change2Ip46Button.Click += new System.EventHandler(this.change2Ip46Button_Click);
            // 
            // unsignCheckBox
            // 
            this.unsignCheckBox.AutoSize = true;
            this.unsignCheckBox.Location = new System.Drawing.Point(286, 285);
            this.unsignCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.unsignCheckBox.Name = "unsignCheckBox";
            this.unsignCheckBox.Size = new System.Drawing.Size(134, 29);
            this.unsignCheckBox.TabIndex = 119;
            this.unsignCheckBox.Text = "Không dấu";
            this.unsignCheckBox.UseVisualStyleBackColor = true;
            this.unsignCheckBox.CheckedChanged += new System.EventHandler(this.adbKeyboardCheckBox_CheckedChanged);
            // 
            // vnVinaphoneButton
            // 
            this.vnVinaphoneButton.Location = new System.Drawing.Point(7, 21);
            this.vnVinaphoneButton.Margin = new System.Windows.Forms.Padding(2);
            this.vnVinaphoneButton.Name = "vnVinaphoneButton";
            this.vnVinaphoneButton.Size = new System.Drawing.Size(90, 26);
            this.vnVinaphoneButton.TabIndex = 120;
            this.vnVinaphoneButton.Text = "VN Vinaphone";
            this.vnVinaphoneButton.UseVisualStyleBackColor = true;
            this.vnVinaphoneButton.Click += new System.EventHandler(this.vnVinaphoneButton_Click);
            // 
            // randomMailPhoneSimCheckBox
            // 
            this.randomMailPhoneSimCheckBox.AutoSize = true;
            this.randomMailPhoneSimCheckBox.Location = new System.Drawing.Point(12, 234);
            this.randomMailPhoneSimCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.randomMailPhoneSimCheckBox.Name = "randomMailPhoneSimCheckBox";
            this.randomMailPhoneSimCheckBox.Size = new System.Drawing.Size(177, 29);
            this.randomMailPhoneSimCheckBox.TabIndex = 122;
            this.randomMailPhoneSimCheckBox.Text = "Phone/Mail/Sim";
            this.randomMailPhoneSimCheckBox.UseVisualStyleBackColor = true;
            // 
            // verifyAccNvrCheckBox
            // 
            this.verifyAccNvrCheckBox.AutoSize = true;
            this.verifyAccNvrCheckBox.ForeColor = System.Drawing.Color.Red;
            this.verifyAccNvrCheckBox.Location = new System.Drawing.Point(538, 82);
            this.verifyAccNvrCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.verifyAccNvrCheckBox.Name = "verifyAccNvrCheckBox";
            this.verifyAccNvrCheckBox.Size = new System.Drawing.Size(147, 29);
            this.verifyAccNvrCheckBox.TabIndex = 124;
            this.verifyAccNvrCheckBox.Text = "Veri Acc Nvr";
            this.verifyAccNvrCheckBox.UseVisualStyleBackColor = true;
            this.verifyAccNvrCheckBox.CheckedChanged += new System.EventHandler(this.verifyAccCheckBox_CheckedChanged);
            // 
            // autoRunVeriCheckBox
            // 
            this.autoRunVeriCheckBox.AutoSize = true;
            this.autoRunVeriCheckBox.Location = new System.Drawing.Point(15, 289);
            this.autoRunVeriCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.autoRunVeriCheckBox.Name = "autoRunVeriCheckBox";
            this.autoRunVeriCheckBox.Size = new System.Drawing.Size(155, 29);
            this.autoRunVeriCheckBox.TabIndex = 126;
            this.autoRunVeriCheckBox.Text = "Auto Stop 0H";
            this.autoRunVeriCheckBox.UseVisualStyleBackColor = true;
            // 
            // homeCheckBox
            // 
            this.homeCheckBox.AutoSize = true;
            this.homeCheckBox.Checked = true;
            this.homeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.homeCheckBox.Location = new System.Drawing.Point(394, 151);
            this.homeCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.homeCheckBox.Name = "homeCheckBox";
            this.homeCheckBox.Size = new System.Drawing.Size(90, 29);
            this.homeCheckBox.TabIndex = 127;
            this.homeCheckBox.Text = "Home";
            this.homeCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkFBInstalledBtn
            // 
            this.checkFBInstalledBtn.Location = new System.Drawing.Point(15, 402);
            this.checkFBInstalledBtn.Margin = new System.Windows.Forms.Padding(2);
            this.checkFBInstalledBtn.Name = "checkFBInstalledBtn";
            this.checkFBInstalledBtn.Size = new System.Drawing.Size(173, 30);
            this.checkFBInstalledBtn.TabIndex = 129;
            this.checkFBInstalledBtn.Text = "Check FB Installed";
            this.checkFBInstalledBtn.UseVisualStyleBackColor = true;
            this.checkFBInstalledBtn.Click += new System.EventHandler(this.checkFBInstalledBtn_Click);
            // 
            // randomMailPhoneCheckBox
            // 
            this.randomMailPhoneCheckBox.AutoSize = true;
            this.randomMailPhoneCheckBox.Location = new System.Drawing.Point(625, 82);
            this.randomMailPhoneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.randomMailPhoneCheckBox.Name = "randomMailPhoneCheckBox";
            this.randomMailPhoneCheckBox.Size = new System.Drawing.Size(177, 29);
            this.randomMailPhoneCheckBox.TabIndex = 130;
            this.randomMailPhoneCheckBox.Text = "Ran Mail/Phone";
            this.randomMailPhoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // installMissingFBbutton
            // 
            this.installMissingFBbutton.ForeColor = System.Drawing.Color.Red;
            this.installMissingFBbutton.Location = new System.Drawing.Point(14, 349);
            this.installMissingFBbutton.Margin = new System.Windows.Forms.Padding(2);
            this.installMissingFBbutton.Name = "installMissingFBbutton";
            this.installMissingFBbutton.Size = new System.Drawing.Size(93, 46);
            this.installMissingFBbutton.TabIndex = 131;
            this.installMissingFBbutton.Text = "Install Missing Facebook";
            this.installMissingFBbutton.UseVisualStyleBackColor = true;
            this.installMissingFBbutton.Click += new System.EventHandler(this.installMissingFBbutton_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // minSpeedTextBox
            // 
            this.minSpeedTextBox.Location = new System.Drawing.Point(182, 507);
            this.minSpeedTextBox.Name = "minSpeedTextBox";
            this.minSpeedTextBox.Size = new System.Drawing.Size(111, 29);
            this.minSpeedTextBox.TabIndex = 132;
            this.minSpeedTextBox.Text = "400";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(668, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 25);
            this.label10.TabIndex = 133;
            this.label10.Text = "MinSpeed";
            // 
            // autoSpeedCheckBox
            // 
            this.autoSpeedCheckBox.AutoSize = true;
            this.autoSpeedCheckBox.Location = new System.Drawing.Point(393, 64);
            this.autoSpeedCheckBox.Name = "autoSpeedCheckBox";
            this.autoSpeedCheckBox.Size = new System.Drawing.Size(137, 29);
            this.autoSpeedCheckBox.TabIndex = 134;
            this.autoSpeedCheckBox.Text = "AutoSpeed";
            this.autoSpeedCheckBox.UseVisualStyleBackColor = true;
            // 
            // speedlabel
            // 
            this.speedlabel.AutoSize = true;
            this.speedlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedlabel.ForeColor = System.Drawing.Color.Blue;
            this.speedlabel.Location = new System.Drawing.Point(663, 16);
            this.speedlabel.Name = "speedlabel";
            this.speedlabel.Size = new System.Drawing.Size(102, 32);
            this.speedlabel.TabIndex = 135;
            this.speedlabel.Text = "Speed";
            this.speedlabel.Visible = false;
            // 
            // autoVeriMailCheckBox
            // 
            this.autoVeriMailCheckBox.AutoSize = true;
            this.autoVeriMailCheckBox.Location = new System.Drawing.Point(509, 22);
            this.autoVeriMailCheckBox.Name = "autoVeriMailCheckBox";
            this.autoVeriMailCheckBox.Size = new System.Drawing.Size(155, 29);
            this.autoVeriMailCheckBox.TabIndex = 136;
            this.autoVeriMailCheckBox.Text = "Auto VeriMail";
            this.autoVeriMailCheckBox.UseVisualStyleBackColor = true;
            // 
            // turnoffSimButton
            // 
            this.turnoffSimButton.Location = new System.Drawing.Point(475, 444);
            this.turnoffSimButton.Name = "turnoffSimButton";
            this.turnoffSimButton.Size = new System.Drawing.Size(83, 27);
            this.turnoffSimButton.TabIndex = 137;
            this.turnoffSimButton.Text = "TurnOffSim";
            this.turnoffSimButton.UseVisualStyleBackColor = true;
            this.turnoffSimButton.Click += new System.EventHandler(this.turnoffSimButton_Click);
            // 
            // rmFbliteButton
            // 
            this.rmFbliteButton.Location = new System.Drawing.Point(1071, 393);
            this.rmFbliteButton.Name = "rmFbliteButton";
            this.rmFbliteButton.Size = new System.Drawing.Size(92, 26);
            this.rmFbliteButton.TabIndex = 141;
            this.rmFbliteButton.Text = "Rm FbLite";
            this.rmFbliteButton.UseVisualStyleBackColor = true;
            this.rmFbliteButton.Click += new System.EventHandler(this.rmFbliteButton_Click);
            // 
            // randomPhoneCheckBox
            // 
            this.randomPhoneCheckBox.AutoSize = true;
            this.randomPhoneCheckBox.Location = new System.Drawing.Point(179, 316);
            this.randomPhoneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.randomPhoneCheckBox.Name = "randomPhoneCheckBox";
            this.randomPhoneCheckBox.Size = new System.Drawing.Size(135, 29);
            this.randomPhoneCheckBox.TabIndex = 142;
            this.randomPhoneCheckBox.Text = "Ran Phone";
            this.randomPhoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // changeSimType2CheckBox
            // 
            this.changeSimType2CheckBox.AutoSize = true;
            this.changeSimType2CheckBox.Location = new System.Drawing.Point(12, 171);
            this.changeSimType2CheckBox.Name = "changeSimType2CheckBox";
            this.changeSimType2CheckBox.Size = new System.Drawing.Size(138, 29);
            this.changeSimType2CheckBox.TabIndex = 144;
            this.changeSimType2CheckBox.Text = "Sim Type 2";
            this.changeSimType2CheckBox.UseVisualStyleBackColor = true;
            // 
            // randomAllSimCheckBox
            // 
            this.randomAllSimCheckBox.AutoSize = true;
            this.randomAllSimCheckBox.Location = new System.Drawing.Point(12, 148);
            this.randomAllSimCheckBox.Name = "randomAllSimCheckBox";
            this.randomAllSimCheckBox.Size = new System.Drawing.Size(177, 29);
            this.randomAllSimCheckBox.TabIndex = 145;
            this.randomAllSimCheckBox.Text = "Random All Sim";
            this.randomAllSimCheckBox.UseVisualStyleBackColor = true;
            // 
            // randomVeriCheckBox
            // 
            this.randomVeriCheckBox.AutoSize = true;
            this.randomVeriCheckBox.Location = new System.Drawing.Point(11, 63);
            this.randomVeriCheckBox.Name = "randomVeriCheckBox";
            this.randomVeriCheckBox.Size = new System.Drawing.Size(124, 29);
            this.randomVeriCheckBox.TabIndex = 147;
            this.randomVeriCheckBox.Text = "Rand Veri";
            this.randomVeriCheckBox.UseVisualStyleBackColor = true;
            // 
            // vietUsCheckBox
            // 
            this.vietUsCheckBox.AutoSize = true;
            this.vietUsCheckBox.Location = new System.Drawing.Point(286, 312);
            this.vietUsCheckBox.Name = "vietUsCheckBox";
            this.vietUsCheckBox.Size = new System.Drawing.Size(96, 29);
            this.vietUsCheckBox.TabIndex = 148;
            this.vietUsCheckBox.Text = "VietUs";
            this.vietUsCheckBox.UseVisualStyleBackColor = true;
            // 
            // veriHotmailCheckBox
            // 
            this.veriHotmailCheckBox.AutoSize = true;
            this.veriHotmailCheckBox.Location = new System.Drawing.Point(11, 105);
            this.veriHotmailCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.veriHotmailCheckBox.Name = "veriHotmailCheckBox";
            this.veriHotmailCheckBox.Size = new System.Drawing.Size(143, 29);
            this.veriHotmailCheckBox.TabIndex = 149;
            this.veriHotmailCheckBox.Text = "Veri Hotmail";
            this.veriHotmailCheckBox.UseVisualStyleBackColor = true;
            this.veriHotmailCheckBox.CheckedChanged += new System.EventHandler(this.veriHotmailCheckBox_CheckedChanged);
            // 
            // vinaphoneCheckbox
            // 
            this.vinaphoneCheckbox.AutoSize = true;
            this.vinaphoneCheckbox.Checked = true;
            this.vinaphoneCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vinaphoneCheckbox.Location = new System.Drawing.Point(11, 29);
            this.vinaphoneCheckbox.Name = "vinaphoneCheckbox";
            this.vinaphoneCheckbox.Size = new System.Drawing.Size(133, 29);
            this.vinaphoneCheckbox.TabIndex = 151;
            this.vinaphoneCheckbox.Text = "Vinaphone";
            this.vinaphoneCheckbox.UseVisualStyleBackColor = true;
            this.vinaphoneCheckbox.CheckedChanged += new System.EventHandler(this.vinaphoneCheckbox_CheckedChanged);
            // 
            // viettelCheckBox
            // 
            this.viettelCheckBox.AutoSize = true;
            this.viettelCheckBox.Checked = true;
            this.viettelCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viettelCheckBox.Location = new System.Drawing.Point(11, 52);
            this.viettelCheckBox.Name = "viettelCheckBox";
            this.viettelCheckBox.Size = new System.Drawing.Size(92, 29);
            this.viettelCheckBox.TabIndex = 152;
            this.viettelCheckBox.Text = "Viettel";
            this.viettelCheckBox.UseVisualStyleBackColor = true;
            this.viettelCheckBox.CheckedChanged += new System.EventHandler(this.viettelCheckBox_CheckedChanged);
            // 
            // mobiphoneCheckBox
            // 
            this.mobiphoneCheckBox.AutoSize = true;
            this.mobiphoneCheckBox.Checked = true;
            this.mobiphoneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mobiphoneCheckBox.Location = new System.Drawing.Point(11, 104);
            this.mobiphoneCheckBox.Name = "mobiphoneCheckBox";
            this.mobiphoneCheckBox.Size = new System.Drawing.Size(136, 29);
            this.mobiphoneCheckBox.TabIndex = 153;
            this.mobiphoneCheckBox.Text = "Mobiphone";
            this.mobiphoneCheckBox.UseVisualStyleBackColor = true;
            this.mobiphoneCheckBox.CheckedChanged += new System.EventHandler(this.mobiphoneCheckBox_CheckedChanged);
            // 
            // vietnamMobileCheckBox
            // 
            this.vietnamMobileCheckBox.AutoSize = true;
            this.vietnamMobileCheckBox.Checked = true;
            this.vietnamMobileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vietnamMobileCheckBox.Location = new System.Drawing.Point(11, 78);
            this.vietnamMobileCheckBox.Name = "vietnamMobileCheckBox";
            this.vietnamMobileCheckBox.Size = new System.Drawing.Size(168, 29);
            this.vietnamMobileCheckBox.TabIndex = 154;
            this.vietnamMobileCheckBox.Text = "VietnamMobile";
            this.vietnamMobileCheckBox.UseVisualStyleBackColor = true;
            this.vietnamMobileCheckBox.CheckedChanged += new System.EventHandler(this.vietnamMobileCheckBox_CheckedChanged);
            // 
            // shoplikeTextBox1
            // 
            this.shoplikeTextBox1.Font = new System.Drawing.Font("Ink Free", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoplikeTextBox1.Location = new System.Drawing.Point(18, 41);
            this.shoplikeTextBox1.Multiline = true;
            this.shoplikeTextBox1.Name = "shoplikeTextBox1";
            this.shoplikeTextBox1.Size = new System.Drawing.Size(263, 43);
            this.shoplikeTextBox1.TabIndex = 156;
            this.shoplikeTextBox1.TextChanged += new System.EventHandler(this.shoplikeTextBox1_TextChanged);
            // 
            // loadTinsoftButton
            // 
            this.loadTinsoftButton.Location = new System.Drawing.Point(164, 374);
            this.loadTinsoftButton.Name = "loadTinsoftButton";
            this.loadTinsoftButton.Size = new System.Drawing.Size(97, 34);
            this.loadTinsoftButton.TabIndex = 157;
            this.loadTinsoftButton.Text = "LOAD";
            this.loadTinsoftButton.UseVisualStyleBackColor = true;
            this.loadTinsoftButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // adbKeyCheckBox
            // 
            this.adbKeyCheckBox.AutoSize = true;
            this.adbKeyCheckBox.Location = new System.Drawing.Point(394, 199);
            this.adbKeyCheckBox.Name = "adbKeyCheckBox";
            this.adbKeyCheckBox.Size = new System.Drawing.Size(169, 29);
            this.adbKeyCheckBox.TabIndex = 159;
            this.adbKeyCheckBox.Text = "ADB Keyboard";
            this.adbKeyCheckBox.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(339, 209);
            this.resetButton.Margin = new System.Windows.Forms.Padding(2);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(87, 30);
            this.resetButton.TabIndex = 161;
            this.resetButton.Text = "Reset Count";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // turnOffEmuButton
            // 
            this.turnOffEmuButton.Location = new System.Drawing.Point(476, 308);
            this.turnOffEmuButton.Margin = new System.Windows.Forms.Padding(2);
            this.turnOffEmuButton.Name = "turnOffEmuButton";
            this.turnOffEmuButton.Size = new System.Drawing.Size(103, 31);
            this.turnOffEmuButton.TabIndex = 162;
            this.turnOffEmuButton.Text = "Turn Off Emu";
            this.turnOffEmuButton.UseVisualStyleBackColor = true;
            this.turnOffEmuButton.Click += new System.EventHandler(this.turnOffEmuButton_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.getInfoToolStripMenuItem,
            this.change2Ip4,
            this.change2Ip6,
            this.viewScreenToolStripMenuItem,
            this.getxmltoolStripMenuItem,
            this.captureScreentoolStripMenuItem,
            this.rebootCmdtoolStripMenuItem,
            this.Call101toolStripMenuItem,
            this.napThetoolStripMenuItem,
            this.viewDevicetoolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip.ShowCheckMargin = true;
            this.contextMenuStrip.Size = new System.Drawing.Size(248, 508);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(247, 36);
            this.toolStripMenuItem1.Text = "Call";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.CallStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(247, 36);
            this.toolStripMenuItem2.Text = "Sáng";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.BrightStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(247, 36);
            this.toolStripMenuItem3.Text = "Tối";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.DarkStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(247, 36);
            this.toolStripMenuItem4.Text = "Reboot";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.RebootStripMenuItem4_Click);
            // 
            // getInfoToolStripMenuItem
            // 
            this.getInfoToolStripMenuItem.Name = "getInfoToolStripMenuItem";
            this.getInfoToolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.getInfoToolStripMenuItem.Text = "Get Info";
            this.getInfoToolStripMenuItem.Click += new System.EventHandler(this.getInfoToolStripMenuItem_Click);
            // 
            // change2Ip4
            // 
            this.change2Ip4.Name = "change2Ip4";
            this.change2Ip4.Size = new System.Drawing.Size(247, 36);
            this.change2Ip4.Text = "change2Ip4";
            this.change2Ip4.Click += new System.EventHandler(this.change2Ip4_Click);
            // 
            // change2Ip6
            // 
            this.change2Ip6.Name = "change2Ip6";
            this.change2Ip6.Size = new System.Drawing.Size(247, 36);
            this.change2Ip6.Text = "change2Ip6";
            this.change2Ip6.Click += new System.EventHandler(this.change2Ip6_Click);
            // 
            // viewScreenToolStripMenuItem
            // 
            this.viewScreenToolStripMenuItem.Name = "viewScreenToolStripMenuItem";
            this.viewScreenToolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.viewScreenToolStripMenuItem.Text = "View Screen";
            this.viewScreenToolStripMenuItem.Click += new System.EventHandler(this.viewScreenToolStripMenuItem_Click);
            // 
            // getxmltoolStripMenuItem
            // 
            this.getxmltoolStripMenuItem.Name = "getxmltoolStripMenuItem";
            this.getxmltoolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.getxmltoolStripMenuItem.Text = "Get xml screen";
            this.getxmltoolStripMenuItem.Click += new System.EventHandler(this.getxmltoolStripMenuItem_Click);
            // 
            // captureScreentoolStripMenuItem
            // 
            this.captureScreentoolStripMenuItem.Name = "captureScreentoolStripMenuItem";
            this.captureScreentoolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.captureScreentoolStripMenuItem.Text = "Capture screen";
            this.captureScreentoolStripMenuItem.Click += new System.EventHandler(this.captureScreentoolStripMenuItem_Click);
            // 
            // rebootCmdtoolStripMenuItem
            // 
            this.rebootCmdtoolStripMenuItem.Name = "rebootCmdtoolStripMenuItem";
            this.rebootCmdtoolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.rebootCmdtoolStripMenuItem.Text = "Reboot CMD";
            this.rebootCmdtoolStripMenuItem.Click += new System.EventHandler(this.rebootCMDtoolStripMenuItem_Click);
            // 
            // Call101toolStripMenuItem
            // 
            this.Call101toolStripMenuItem.Name = "Call101toolStripMenuItem";
            this.Call101toolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.Call101toolStripMenuItem.Text = "Call *101#";
            this.Call101toolStripMenuItem.Click += new System.EventHandler(this.call101ToolStripMenuItem_Click);
            // 
            // napThetoolStripMenuItem
            // 
            this.napThetoolStripMenuItem.Name = "napThetoolStripMenuItem";
            this.napThetoolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.napThetoolStripMenuItem.Text = "Nạp thẻ";
            this.napThetoolStripMenuItem.Click += new System.EventHandler(this.napThetoolStripMenuItem_Click);
            // 
            // viewDevicetoolStripMenuItem
            // 
            this.viewDevicetoolStripMenuItem.Name = "viewDevicetoolStripMenuItem";
            this.viewDevicetoolStripMenuItem.Size = new System.Drawing.Size(247, 36);
            this.viewDevicetoolStripMenuItem.Text = "View Device";
            this.viewDevicetoolStripMenuItem.Click += new System.EventHandler(this.MenuSelected_Click);
            // 
            // veriContactCheckBox
            // 
            this.veriContactCheckBox.AutoSize = true;
            this.veriContactCheckBox.Location = new System.Drawing.Point(179, 339);
            this.veriContactCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.veriContactCheckBox.Name = "veriContactCheckBox";
            this.veriContactCheckBox.Size = new System.Drawing.Size(152, 29);
            this.veriContactCheckBox.TabIndex = 164;
            this.veriContactCheckBox.Text = "Veri Danh bạ";
            this.veriContactCheckBox.UseVisualStyleBackColor = true;
            this.veriContactCheckBox.Visible = false;
            // 
            // veriPhoneCheckBox
            // 
            this.veriPhoneCheckBox.AutoSize = true;
            this.veriPhoneCheckBox.ForeColor = System.Drawing.Color.Red;
            this.veriPhoneCheckBox.Location = new System.Drawing.Point(181, 170);
            this.veriPhoneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.veriPhoneCheckBox.Name = "veriPhoneCheckBox";
            this.veriPhoneCheckBox.Size = new System.Drawing.Size(135, 29);
            this.veriPhoneCheckBox.TabIndex = 165;
            this.veriPhoneCheckBox.Text = "Veri Phone";
            this.veriPhoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // nvrUpAvatarCheckBox
            // 
            this.nvrUpAvatarCheckBox.AutoSize = true;
            this.nvrUpAvatarCheckBox.Location = new System.Drawing.Point(185, 85);
            this.nvrUpAvatarCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.nvrUpAvatarCheckBox.Name = "nvrUpAvatarCheckBox";
            this.nvrUpAvatarCheckBox.Size = new System.Drawing.Size(138, 29);
            this.nvrUpAvatarCheckBox.TabIndex = 166;
            this.nvrUpAvatarCheckBox.Text = "Nvr Up Ava";
            this.nvrUpAvatarCheckBox.UseVisualStyleBackColor = true;
            // 
            // textnowCheckbox
            // 
            this.textnowCheckbox.AutoSize = true;
            this.textnowCheckbox.Location = new System.Drawing.Point(181, 147);
            this.textnowCheckbox.Name = "textnowCheckbox";
            this.textnowCheckbox.Size = new System.Drawing.Size(116, 29);
            this.textnowCheckbox.TabIndex = 167;
            this.textnowCheckbox.Text = "TextNow";
            this.textnowCheckbox.ThreeState = true;
            this.textnowCheckbox.UseVisualStyleBackColor = true;
            // 
            // checkLoginCheckBox
            // 
            this.checkLoginCheckBox.AutoSize = true;
            this.checkLoginCheckBox.Location = new System.Drawing.Point(283, 18);
            this.checkLoginCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.checkLoginCheckBox.Name = "checkLoginCheckBox";
            this.checkLoginCheckBox.Size = new System.Drawing.Size(148, 29);
            this.checkLoginCheckBox.TabIndex = 169;
            this.checkLoginCheckBox.Text = "Check Login";
            this.checkLoginCheckBox.UseVisualStyleBackColor = true;
            // 
            // coverCheckBox
            // 
            this.coverCheckBox.AutoSize = true;
            this.coverCheckBox.Location = new System.Drawing.Point(396, 347);
            this.coverCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.coverCheckBox.Name = "coverCheckBox";
            this.coverCheckBox.Size = new System.Drawing.Size(91, 29);
            this.coverCheckBox.TabIndex = 170;
            this.coverCheckBox.Text = "Cover";
            this.coverCheckBox.UseVisualStyleBackColor = true;
            // 
            // noveriCoverCheckBox
            // 
            this.noveriCoverCheckBox.AutoSize = true;
            this.noveriCoverCheckBox.Location = new System.Drawing.Point(10, 22);
            this.noveriCoverCheckBox.Name = "noveriCoverCheckBox";
            this.noveriCoverCheckBox.Size = new System.Drawing.Size(126, 29);
            this.noveriCoverCheckBox.TabIndex = 171;
            this.noveriCoverCheckBox.Text = "Nvr Cover";
            this.noveriCoverCheckBox.UseVisualStyleBackColor = true;
            // 
            // loginByUserPassCheckBox
            // 
            this.loginByUserPassCheckBox.AutoSize = true;
            this.loginByUserPassCheckBox.Location = new System.Drawing.Point(284, 96);
            this.loginByUserPassCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.loginByUserPassCheckBox.Name = "loginByUserPassCheckBox";
            this.loginByUserPassCheckBox.Size = new System.Drawing.Size(127, 29);
            this.loginByUserPassCheckBox.TabIndex = 172;
            this.loginByUserPassCheckBox.Text = "User/pass";
            this.loginByUserPassCheckBox.UseVisualStyleBackColor = true;
            // 
            // addStatusCheckBox
            // 
            this.addStatusCheckBox.AutoSize = true;
            this.addStatusCheckBox.Location = new System.Drawing.Point(396, 256);
            this.addStatusCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.addStatusCheckBox.Name = "addStatusCheckBox";
            this.addStatusCheckBox.Size = new System.Drawing.Size(135, 29);
            this.addStatusCheckBox.TabIndex = 174;
            this.addStatusCheckBox.Text = "Add Status";
            this.addStatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // veriDirectByPhoneCheckBox
            // 
            this.veriDirectByPhoneCheckBox.AutoSize = true;
            this.veriDirectByPhoneCheckBox.Location = new System.Drawing.Point(11, 85);
            this.veriDirectByPhoneCheckBox.Name = "veriDirectByPhoneCheckBox";
            this.veriDirectByPhoneCheckBox.Size = new System.Drawing.Size(135, 29);
            this.veriDirectByPhoneCheckBox.TabIndex = 175;
            this.veriDirectByPhoneCheckBox.Text = "Veri Phone";
            this.veriDirectByPhoneCheckBox.UseVisualStyleBackColor = true;
            this.veriDirectByPhoneCheckBox.CheckedChanged += new System.EventHandler(this.veriDirectByPhoneCheckBox_CheckedChanged);
            // 
            // americaPhoneCheckBox
            // 
            this.americaPhoneCheckBox.AutoSize = true;
            this.americaPhoneCheckBox.Location = new System.Drawing.Point(509, 100);
            this.americaPhoneCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.americaPhoneCheckBox.Name = "americaPhoneCheckBox";
            this.americaPhoneCheckBox.Size = new System.Drawing.Size(66, 29);
            this.americaPhoneCheckBox.TabIndex = 176;
            this.americaPhoneCheckBox.Text = "US";
            this.americaPhoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // changeSimUsButton
            // 
            this.changeSimUsButton.Location = new System.Drawing.Point(608, 584);
            this.changeSimUsButton.Name = "changeSimUsButton";
            this.changeSimUsButton.Size = new System.Drawing.Size(76, 35);
            this.changeSimUsButton.TabIndex = 177;
            this.changeSimUsButton.Text = "US Sim";
            this.changeSimUsButton.UseVisualStyleBackColor = true;
            this.changeSimUsButton.Click += new System.EventHandler(this.changeSimUsButton_Click);
            // 
            // vietSimButton
            // 
            this.vietSimButton.Location = new System.Drawing.Point(608, 620);
            this.vietSimButton.Name = "vietSimButton";
            this.vietSimButton.Size = new System.Drawing.Size(75, 24);
            this.vietSimButton.TabIndex = 178;
            this.vietSimButton.Text = "Viet Sim";
            this.vietSimButton.UseVisualStyleBackColor = true;
            this.vietSimButton.Click += new System.EventHandler(this.vietSimButton_Click);
            // 
            // micerCheckBox
            // 
            this.micerCheckBox.AutoSize = true;
            this.micerCheckBox.Location = new System.Drawing.Point(288, 335);
            this.micerCheckBox.Name = "micerCheckBox";
            this.micerCheckBox.Size = new System.Drawing.Size(86, 29);
            this.micerCheckBox.TabIndex = 182;
            this.micerCheckBox.Text = "Micer";
            this.micerCheckBox.UseVisualStyleBackColor = true;
            // 
            // executeAdbButton
            // 
            this.executeAdbButton.Location = new System.Drawing.Point(638, 468);
            this.executeAdbButton.Name = "executeAdbButton";
            this.executeAdbButton.Size = new System.Drawing.Size(93, 26);
            this.executeAdbButton.TabIndex = 183;
            this.executeAdbButton.Text = "Execute ADB";
            this.executeAdbButton.UseVisualStyleBackColor = true;
            this.executeAdbButton.Click += new System.EventHandler(this.executeAdbButton_Click);
            // 
            // codeKeyTextNowTextBox
            // 
            this.codeKeyTextNowTextBox.Location = new System.Drawing.Point(243, 5986);
            this.codeKeyTextNowTextBox.Name = "codeKeyTextNowTextBox";
            this.codeKeyTextNowTextBox.Size = new System.Drawing.Size(4239, 29);
            this.codeKeyTextNowTextBox.TabIndex = 184;
            this.codeKeyTextNowTextBox.Text = "6eb720914aa32f3e7b7c33dee6145787";
            this.codeKeyTextNowTextBox.TextChanged += new System.EventHandler(this.cookieTextNowTextBox_TextChanged);
            // 
            // clearCacheFBcheckBox
            // 
            this.clearCacheFBcheckBox.AutoSize = true;
            this.clearCacheFBcheckBox.Checked = true;
            this.clearCacheFBcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearCacheFBcheckBox.Location = new System.Drawing.Point(395, 123);
            this.clearCacheFBcheckBox.Name = "clearCacheFBcheckBox";
            this.clearCacheFBcheckBox.Size = new System.Drawing.Size(227, 29);
            this.clearCacheFBcheckBox.TabIndex = 185;
            this.clearCacheFBcheckBox.Text = "ClearCacheSettingFb";
            this.clearCacheFBcheckBox.UseVisualStyleBackColor = true;
            // 
            // accMoiCheckBox
            // 
            this.accMoiCheckBox.AutoSize = true;
            this.accMoiCheckBox.Location = new System.Drawing.Point(509, 156);
            this.accMoiCheckBox.Name = "accMoiCheckBox";
            this.accMoiCheckBox.Size = new System.Drawing.Size(109, 29);
            this.accMoiCheckBox.TabIndex = 186;
            this.accMoiCheckBox.Text = "Acc Mồi";
            this.accMoiCheckBox.UseVisualStyleBackColor = true;
            // 
            // cookieCodeTextNowtextBox
            // 
            this.cookieCodeTextNowtextBox.Location = new System.Drawing.Point(180, 620);
            this.cookieCodeTextNowtextBox.Margin = new System.Windows.Forms.Padding(2);
            this.cookieCodeTextNowtextBox.Name = "cookieCodeTextNowtextBox";
            this.cookieCodeTextNowtextBox.Size = new System.Drawing.Size(111, 29);
            this.cookieCodeTextNowtextBox.TabIndex = 190;
            this.cookieCodeTextNowtextBox.Text = "PHPSESSID=6h10jfsfui9of9duajp2evoou3";
            this.cookieCodeTextNowtextBox.TextChanged += new System.EventHandler(this.cookieCodeTextNowtextBox_TextChanged);
            // 
            // turnOnEmubutton
            // 
            this.turnOnEmubutton.Location = new System.Drawing.Point(476, 340);
            this.turnOnEmubutton.Name = "turnOnEmubutton";
            this.turnOnEmubutton.Size = new System.Drawing.Size(103, 24);
            this.turnOnEmubutton.TabIndex = 191;
            this.turnOnEmubutton.Text = "TurnOn Emu";
            this.turnOnEmubutton.UseVisualStyleBackColor = true;
            this.turnOnEmubutton.Click += new System.EventHandler(this.turnOnEmubutton_Click);
            // 
            // otpKeyTextBox
            // 
            this.otpKeyTextBox.Location = new System.Drawing.Point(243, 4402);
            this.otpKeyTextBox.Name = "otpKeyTextBox";
            this.otpKeyTextBox.Size = new System.Drawing.Size(3855, 29);
            this.otpKeyTextBox.TabIndex = 192;
            this.otpKeyTextBox.Text = "9D4KJW21TMBAHPLR1634992093";
            this.otpKeyTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // reportPhoneLabel
            // 
            this.reportPhoneLabel.AutoSize = true;
            this.reportPhoneLabel.ForeColor = System.Drawing.Color.Red;
            this.reportPhoneLabel.Location = new System.Drawing.Point(676, 76);
            this.reportPhoneLabel.Name = "reportPhoneLabel";
            this.reportPhoneLabel.Size = new System.Drawing.Size(69, 25);
            this.reportPhoneLabel.TabIndex = 193;
            this.reportPhoneLabel.Text = "Phone";
            this.reportPhoneLabel.Visible = false;
            // 
            // getPhoneCodeTextNowbutton
            // 
            this.getPhoneCodeTextNowbutton.Location = new System.Drawing.Point(588, 335);
            this.getPhoneCodeTextNowbutton.Name = "getPhoneCodeTextNowbutton";
            this.getPhoneCodeTextNowbutton.Size = new System.Drawing.Size(146, 28);
            this.getPhoneCodeTextNowbutton.TabIndex = 194;
            this.getPhoneCodeTextNowbutton.Text = "GetPhoneCodeTextNow";
            this.getPhoneCodeTextNowbutton.UseVisualStyleBackColor = true;
            this.getPhoneCodeTextNowbutton.Click += new System.EventHandler(this.getPhoneCodeTextNowbutton_Click);
            // 
            // prefixTextNowCheckBox
            // 
            this.prefixTextNowCheckBox.AutoSize = true;
            this.prefixTextNowCheckBox.Location = new System.Drawing.Point(393, 40);
            this.prefixTextNowCheckBox.Name = "prefixTextNowCheckBox";
            this.prefixTextNowCheckBox.Size = new System.Drawing.Size(170, 29);
            this.prefixTextNowCheckBox.TabIndex = 195;
            this.prefixTextNowCheckBox.Text = "Prefix TextNow";
            this.prefixTextNowCheckBox.UseVisualStyleBackColor = true;
            this.prefixTextNowCheckBox.CheckedChanged += new System.EventHandler(this.prefixTextNowCheckBox_CheckedChanged);
            // 
            // phoneInQueuelabel
            // 
            this.phoneInQueuelabel.AutoSize = true;
            this.phoneInQueuelabel.ForeColor = System.Drawing.Color.Blue;
            this.phoneInQueuelabel.Location = new System.Drawing.Point(110, 1);
            this.phoneInQueuelabel.Name = "phoneInQueuelabel";
            this.phoneInQueuelabel.Size = new System.Drawing.Size(23, 25);
            this.phoneInQueuelabel.TabIndex = 197;
            this.phoneInQueuelabel.Text = "0";
            // 
            // veriMailAfterPhonecheckBox
            // 
            this.veriMailAfterPhonecheckBox.AutoSize = true;
            this.veriMailAfterPhonecheckBox.Checked = true;
            this.veriMailAfterPhonecheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.veriMailAfterPhonecheckBox.Location = new System.Drawing.Point(180, 290);
            this.veriMailAfterPhonecheckBox.Name = "veriMailAfterPhonecheckBox";
            this.veriMailAfterPhonecheckBox.Size = new System.Drawing.Size(99, 29);
            this.veriMailAfterPhonecheckBox.TabIndex = 199;
            this.veriMailAfterPhonecheckBox.Text = "Vr Mail";
            this.veriMailAfterPhonecheckBox.UseVisualStyleBackColor = true;
            // 
            // forceIp4CheckBox
            // 
            this.forceIp4CheckBox.AutoSize = true;
            this.forceIp4CheckBox.Location = new System.Drawing.Point(509, 47);
            this.forceIp4CheckBox.Name = "forceIp4CheckBox";
            this.forceIp4CheckBox.Size = new System.Drawing.Size(119, 29);
            this.forceIp4CheckBox.TabIndex = 200;
            this.forceIp4CheckBox.Text = "Force ip4";
            this.forceIp4CheckBox.UseVisualStyleBackColor = true;
            this.forceIp4CheckBox.CheckedChanged += new System.EventHandler(this.forceIp4CheckBox_CheckedChanged);
            // 
            // forceIp6checkBox
            // 
            this.forceIp6checkBox.AutoSize = true;
            this.forceIp6checkBox.Location = new System.Drawing.Point(509, 70);
            this.forceIp6checkBox.Name = "forceIp6checkBox";
            this.forceIp6checkBox.Size = new System.Drawing.Size(119, 29);
            this.forceIp6checkBox.TabIndex = 201;
            this.forceIp6checkBox.Text = "Force ip6";
            this.forceIp6checkBox.UseVisualStyleBackColor = true;
            this.forceIp6checkBox.CheckedChanged += new System.EventHandler(this.forceIp6checkBox_CheckedChanged);
            // 
            // reupFullCheckBox
            // 
            this.reupFullCheckBox.AutoSize = true;
            this.reupFullCheckBox.Location = new System.Drawing.Point(538, 65);
            this.reupFullCheckBox.Name = "reupFullCheckBox";
            this.reupFullCheckBox.Size = new System.Drawing.Size(125, 29);
            this.reupFullCheckBox.TabIndex = 202;
            this.reupFullCheckBox.Text = "Reup Full ";
            this.reupFullCheckBox.UseVisualStyleBackColor = true;
            this.reupFullCheckBox.CheckedChanged += new System.EventHandler(this.reupFullCheckBox_CheckedChanged);
            // 
            // descriptionCheckBox
            // 
            this.descriptionCheckBox.AutoSize = true;
            this.descriptionCheckBox.Location = new System.Drawing.Point(396, 299);
            this.descriptionCheckBox.Name = "descriptionCheckBox";
            this.descriptionCheckBox.Size = new System.Drawing.Size(103, 29);
            this.descriptionCheckBox.TabIndex = 205;
            this.descriptionCheckBox.Text = "Tiểu sử";
            this.descriptionCheckBox.UseVisualStyleBackColor = true;
            // 
            // drkKeyTextBox
            // 
            this.drkKeyTextBox.Location = new System.Drawing.Point(14, 564);
            this.drkKeyTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.drkKeyTextBox.Name = "drkKeyTextBox";
            this.drkKeyTextBox.Size = new System.Drawing.Size(158, 29);
            this.drkKeyTextBox.TabIndex = 207;
            this.drkKeyTextBox.Text = "drk key";
            this.drkKeyTextBox.TextChanged += new System.EventHandler(this.drkKeyTextBox_TextChanged);
            // 
            // drkCheckBox
            // 
            this.drkCheckBox.AutoSize = true;
            this.drkCheckBox.Location = new System.Drawing.Point(181, 124);
            this.drkCheckBox.Name = "drkCheckBox";
            this.drkCheckBox.Size = new System.Drawing.Size(68, 29);
            this.drkCheckBox.TabIndex = 208;
            this.drkCheckBox.Text = "Drk";
            this.drkCheckBox.UseVisualStyleBackColor = true;
            this.drkCheckBox.CheckedChanged += new System.EventHandler(this.drkCheckBox_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 549);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 25);
            this.label11.TabIndex = 209;
            this.label11.Text = "Drk key";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(199, 432);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(168, 25);
            this.label12.TabIndex = 210;
            this.label12.Text = "Code textnow key";
            // 
            // numberOfFriendRequestTextBox
            // 
            this.numberOfFriendRequestTextBox.Location = new System.Drawing.Point(620, 283);
            this.numberOfFriendRequestTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.numberOfFriendRequestTextBox.Name = "numberOfFriendRequestTextBox";
            this.numberOfFriendRequestTextBox.Size = new System.Drawing.Size(111, 29);
            this.numberOfFriendRequestTextBox.TabIndex = 212;
            this.numberOfFriendRequestTextBox.Text = "1";
            // 
            // showFbVersionCheckBox
            // 
            this.showFbVersionCheckBox.AutoSize = true;
            this.showFbVersionCheckBox.Checked = true;
            this.showFbVersionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFbVersionCheckBox.Location = new System.Drawing.Point(407, 80);
            this.showFbVersionCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.showFbVersionCheckBox.Name = "showFbVersionCheckBox";
            this.showFbVersionCheckBox.Size = new System.Drawing.Size(188, 29);
            this.showFbVersionCheckBox.TabIndex = 213;
            this.showFbVersionCheckBox.Text = "Show Fb Version";
            this.showFbVersionCheckBox.UseVisualStyleBackColor = true;
            // 
            // drkDomainTextbox
            // 
            this.drkDomainTextbox.Location = new System.Drawing.Point(14, 588);
            this.drkDomainTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.drkDomainTextbox.Name = "drkDomainTextbox";
            this.drkDomainTextbox.Size = new System.Drawing.Size(111, 29);
            this.drkDomainTextbox.TabIndex = 214;
            this.drkDomainTextbox.Text = "http://today.xyz";
            this.drkDomainTextbox.TextChanged += new System.EventHandler(this.drkDomainTextbox_TextChanged);
            // 
            // nvrByDeviceCheckBox
            // 
            this.nvrByDeviceCheckBox.AutoSize = true;
            this.nvrByDeviceCheckBox.Checked = true;
            this.nvrByDeviceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nvrByDeviceCheckBox.ForeColor = System.Drawing.Color.Red;
            this.nvrByDeviceCheckBox.Location = new System.Drawing.Point(18, 656);
            this.nvrByDeviceCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.nvrByDeviceCheckBox.Name = "nvrByDeviceCheckBox";
            this.nvrByDeviceCheckBox.Size = new System.Drawing.Size(167, 29);
            this.nvrByDeviceCheckBox.TabIndex = 216;
            this.nvrByDeviceCheckBox.Text = "Veri Đúng máy";
            this.nvrByDeviceCheckBox.UseVisualStyleBackColor = true;
            // 
            // startButtonGroupBox
            // 
            this.startButtonGroupBox.Controls.Add(this.getDecisioncheckBox);
            this.startButtonGroupBox.Controls.Add(this.button17);
            this.startButtonGroupBox.Controls.Add(this.Rombutton);
            this.startButtonGroupBox.Controls.Add(this.romgroupBox);
            this.startButtonGroupBox.Controls.Add(this.tuongtacnhecheckBox);
            this.startButtonGroupBox.Controls.Add(this.randomProxyDatacheckBox);
            this.startButtonGroupBox.Controls.Add(this.forcestopDiecheckBox);
            this.startButtonGroupBox.Controls.Add(this.android11checkBox);
            this.startButtonGroupBox.Controls.Add(this.chuyenVeri4gcheckBox);
            this.startButtonGroupBox.Controls.Add(this.giulaiportcheckBox);
            this.startButtonGroupBox.Controls.Add(this.proxySharecheckBox);
            this.startButtonGroupBox.Controls.Add(this.setMaxMailbutton);
            this.startButtonGroupBox.Controls.Add(this.getMailcheckBox);
            this.startButtonGroupBox.Controls.Add(this.maxMailtextBox);
            this.startButtonGroupBox.Controls.Add(this.maxMaillabel);
            this.startButtonGroupBox.Controls.Add(this.virtualDevicetextBox);
            this.startButtonGroupBox.Controls.Add(this.rootRom11checkBox);
            this.startButtonGroupBox.Controls.Add(this.docMailEducheckBox);
            this.startButtonGroupBox.Controls.Add(this.p2ProxycheckBox);
            this.startButtonGroupBox.Controls.Add(this.soLanLayMailtextBox);
            this.startButtonGroupBox.Controls.Add(this.boquaProxyVncheckBox);
            this.startButtonGroupBox.Controls.Add(this.checkVericheckBox);
            this.startButtonGroupBox.Controls.Add(this.chaydocheckBox);
            this.startButtonGroupBox.Controls.Add(this.checkTopProxycheckBox);
            this.startButtonGroupBox.Controls.Add(this.chuyenKeyVnicheckBox);
            this.startButtonGroupBox.Controls.Add(this.uuTienChay4GcheckBox);
            this.startButtonGroupBox.Controls.Add(this.namServercheckBox);
            this.startButtonGroupBox.Controls.Add(this.InitialPhonecheckBox);
            this.startButtonGroupBox.Controls.Add(this.proxyKeycheckBox);
            this.startButtonGroupBox.Controls.Add(this.ipRangeLantextBox);
            this.startButtonGroupBox.Controls.Add(this.showInfoDevicecheckBox);
            this.startButtonGroupBox.Controls.Add(this.UsLanguagecheckBox);
            this.startButtonGroupBox.Controls.Add(this.p3ProxycheckBox);
            this.startButtonGroupBox.Controls.Add(this.p1ProxycheckBox);
            this.startButtonGroupBox.Controls.Add(this.proxyWificheckBox);
            this.startButtonGroupBox.Controls.Add(this.proxyCMDcheckBox);
            this.startButtonGroupBox.Controls.Add(this.findPhonecheckBox);
            this.startButtonGroupBox.Controls.Add(this.changePhoneNumbercheckBox);
            this.startButtonGroupBox.Controls.Add(this.proxy4GcheckBox);
            this.startButtonGroupBox.Controls.Add(this.chạyDoiTenDemcheckBox);
            this.startButtonGroupBox.Controls.Add(this.getHotmailKieumoicheckBox);
            this.startButtonGroupBox.Controls.Add(this.moiKatanaNhanhcheckBox);
            this.startButtonGroupBox.Controls.Add(this.epMoiThanhCongcheckBox);
            this.startButtonGroupBox.Controls.Add(this.superProxycheckBox);
            this.startButtonGroupBox.Controls.Add(this.moiAccRegThanhCongcheckBox);
            this.startButtonGroupBox.Controls.Add(this.randomProxySim2checkBox);
            this.startButtonGroupBox.Controls.Add(this.proxyFromServercheckBox);
            this.startButtonGroupBox.Controls.Add(this.name3wordcheckBox);
            this.startButtonGroupBox.Controls.Add(this.gichuTrenAvatarcheckBox);
            this.startButtonGroupBox.Controls.Add(this.doitenVncheckBox);
            this.startButtonGroupBox.Controls.Add(this.nameUsVncheckBox);
            this.startButtonGroupBox.Controls.Add(this.nameVnUscheckBox);
            this.startButtonGroupBox.Controls.Add(this.forceGmailcheckBox);
            this.startButtonGroupBox.Controls.Add(this.showIpcheckBox);
            this.startButtonGroupBox.Controls.Add(this.reinstallSaudiecheckBox);
            this.startButtonGroupBox.Controls.Add(this.showFbVersionCheckBox);
            this.startButtonGroupBox.Controls.Add(this.changer60checkBox);
            this.startButtonGroupBox.Controls.Add(this.clearAllAccSettingcheckBox);
            this.startButtonGroupBox.Controls.Add(this.thoatOtpcheckBox);
            this.startButtonGroupBox.Controls.Add(this.thoatGmailcheckBox);
            this.startButtonGroupBox.Controls.Add(this.storeAccMoicheckBox);
            this.startButtonGroupBox.Controls.Add(this.set2faLoai2checkBox);
            this.startButtonGroupBox.Controls.Add(this.veriaccgmailCheckBox);
            this.startButtonGroupBox.Controls.Add(this.randomIp46CheckBox);
            this.startButtonGroupBox.Controls.Add(this.veriAccRegMailcheckBox);
            this.startButtonGroupBox.Controls.Add(this.laymailkhaccheckBox);
            this.startButtonGroupBox.Controls.Add(this.regByGmailcheckBox);
            this.startButtonGroupBox.Controls.Add(this.checkDieStopCheckBox);
            this.startButtonGroupBox.Controls.Add(this.groupBox1);
            this.startButtonGroupBox.Controls.Add(this.label23);
            this.startButtonGroupBox.Controls.Add(this.randomNewContactCheckBox);
            this.startButtonGroupBox.Controls.Add(this.removeProxyCheckBox);
            this.startButtonGroupBox.Controls.Add(this.inputStringMailCheckBox);
            this.startButtonGroupBox.Controls.Add(this.label19);
            this.startButtonGroupBox.Controls.Add(this.locationProxyTextBox);
            this.startButtonGroupBox.Controls.Add(this.forcePortraitCheckBox);
            this.startButtonGroupBox.Controls.Add(this.label18);
            this.startButtonGroupBox.Controls.Add(this.reupRegCheckBox);
            this.startButtonGroupBox.Controls.Add(this.label17);
            this.startButtonGroupBox.Controls.Add(this.maxLiveClearTextBox);
            this.startButtonGroupBox.Controls.Add(this.label15);
            this.startButtonGroupBox.Controls.Add(this.delayAfterDieTextBox);
            this.startButtonGroupBox.Controls.Add(this.label14);
            this.startButtonGroupBox.Controls.Add(this.maxFailClearTextBox);
            this.startButtonGroupBox.Controls.Add(this.veriByProxyCheckBox);
            this.startButtonGroupBox.Controls.Add(this.releaseNoteLabel);
            this.startButtonGroupBox.Controls.Add(this.forceDungMayCheckBox);
            this.startButtonGroupBox.Controls.Add(this.avatarByCameraCheckBox);
            this.startButtonGroupBox.Controls.Add(this.runAllBtn);
            this.startButtonGroupBox.Controls.Add(this.reupFullCheckBox);
            this.startButtonGroupBox.Controls.Add(this.verifyAccNvrCheckBox);
            this.startButtonGroupBox.Controls.Add(this.verifiedCheckbox);
            this.startButtonGroupBox.Controls.Add(this.delayAfterRegTextBox);
            this.startButtonGroupBox.Controls.Add(this.randomMailPhoneCheckBox);
            this.startButtonGroupBox.Controls.Add(this.orderGroupBox);
            this.startButtonGroupBox.Controls.Add(this.resetButton);
            this.startButtonGroupBox.Controls.Add(this.brightCheckBox);
            this.startButtonGroupBox.Controls.Add(this.TempMailcheckBox);
            this.startButtonGroupBox.Location = new System.Drawing.Point(11, 6);
            this.startButtonGroupBox.Name = "startButtonGroupBox";
            this.startButtonGroupBox.Size = new System.Drawing.Size(1395, 299);
            this.startButtonGroupBox.TabIndex = 219;
            this.startButtonGroupBox.TabStop = false;
            this.startButtonGroupBox.Text = "Start Button";
            // 
            // getDecisioncheckBox
            // 
            this.getDecisioncheckBox.AutoSize = true;
            this.getDecisioncheckBox.ForeColor = System.Drawing.Color.Red;
            this.getDecisioncheckBox.Location = new System.Drawing.Point(1216, 18);
            this.getDecisioncheckBox.Name = "getDecisioncheckBox";
            this.getDecisioncheckBox.Size = new System.Drawing.Size(149, 29);
            this.getDecisioncheckBox.TabIndex = 340;
            this.getDecisioncheckBox.Text = "Get Decision";
            this.getDecisioncheckBox.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(263, 7);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 30);
            this.button17.TabIndex = 339;
            this.button17.Text = "FORCE";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // Rombutton
            // 
            this.Rombutton.Location = new System.Drawing.Point(339, 7);
            this.Rombutton.Name = "Rombutton";
            this.Rombutton.Size = new System.Drawing.Size(55, 30);
            this.Rombutton.TabIndex = 338;
            this.Rombutton.Text = "ROM";
            this.Rombutton.UseVisualStyleBackColor = true;
            this.Rombutton.Click += new System.EventHandler(this.Rombutton_Click);
            // 
            // romgroupBox
            // 
            this.romgroupBox.Controls.Add(this.A13radioButton);
            this.romgroupBox.Controls.Add(this.A9radioButton);
            this.romgroupBox.Controls.Add(this.A11radioButton);
            this.romgroupBox.Controls.Add(this.A10radioButton);
            this.romgroupBox.Location = new System.Drawing.Point(394, 0);
            this.romgroupBox.Name = "romgroupBox";
            this.romgroupBox.Size = new System.Drawing.Size(296, 42);
            this.romgroupBox.TabIndex = 337;
            this.romgroupBox.TabStop = false;
            this.romgroupBox.Text = "ROM";
            // 
            // A13radioButton
            // 
            this.A13radioButton.AutoSize = true;
            this.A13radioButton.Location = new System.Drawing.Point(219, 18);
            this.A13radioButton.Name = "A13radioButton";
            this.A13radioButton.Size = new System.Drawing.Size(73, 29);
            this.A13radioButton.TabIndex = 294;
            this.A13radioButton.Text = "A13";
            this.A13radioButton.UseVisualStyleBackColor = true;
            // 
            // A9radioButton
            // 
            this.A9radioButton.AutoSize = true;
            this.A9radioButton.Location = new System.Drawing.Point(26, 18);
            this.A9radioButton.Name = "A9radioButton";
            this.A9radioButton.Size = new System.Drawing.Size(62, 29);
            this.A9radioButton.TabIndex = 291;
            this.A9radioButton.Text = "A9";
            this.A9radioButton.UseVisualStyleBackColor = true;
            // 
            // A11radioButton
            // 
            this.A11radioButton.AutoSize = true;
            this.A11radioButton.Location = new System.Drawing.Point(146, 18);
            this.A11radioButton.Name = "A11radioButton";
            this.A11radioButton.Size = new System.Drawing.Size(73, 29);
            this.A11radioButton.TabIndex = 293;
            this.A11radioButton.Text = "A11";
            this.A11radioButton.UseVisualStyleBackColor = true;
            // 
            // A10radioButton
            // 
            this.A10radioButton.AutoSize = true;
            this.A10radioButton.Checked = true;
            this.A10radioButton.Location = new System.Drawing.Point(81, 18);
            this.A10radioButton.Name = "A10radioButton";
            this.A10radioButton.Size = new System.Drawing.Size(73, 29);
            this.A10radioButton.TabIndex = 292;
            this.A10radioButton.TabStop = true;
            this.A10radioButton.Text = "A10";
            this.A10radioButton.UseVisualStyleBackColor = true;
            // 
            // tuongtacnhecheckBox
            // 
            this.tuongtacnhecheckBox.AutoSize = true;
            this.tuongtacnhecheckBox.Location = new System.Drawing.Point(280, 61);
            this.tuongtacnhecheckBox.Name = "tuongtacnhecheckBox";
            this.tuongtacnhecheckBox.Size = new System.Drawing.Size(164, 29);
            this.tuongtacnhecheckBox.TabIndex = 336;
            this.tuongtacnhecheckBox.Text = "Tương tác nhẹ";
            this.tuongtacnhecheckBox.UseVisualStyleBackColor = true;
            // 
            // randomProxyDatacheckBox
            // 
            this.randomProxyDatacheckBox.AutoSize = true;
            this.randomProxyDatacheckBox.Location = new System.Drawing.Point(158, 214);
            this.randomProxyDatacheckBox.Name = "randomProxyDatacheckBox";
            this.randomProxyDatacheckBox.Size = new System.Drawing.Size(119, 29);
            this.randomProxyDatacheckBox.TabIndex = 335;
            this.randomProxyDatacheckBox.Text = "Rand Pro";
            this.randomProxyDatacheckBox.UseVisualStyleBackColor = true;
            // 
            // forcestopDiecheckBox
            // 
            this.forcestopDiecheckBox.AutoSize = true;
            this.forcestopDiecheckBox.Location = new System.Drawing.Point(1115, 21);
            this.forcestopDiecheckBox.Name = "forcestopDiecheckBox";
            this.forcestopDiecheckBox.Size = new System.Drawing.Size(163, 29);
            this.forcestopDiecheckBox.TabIndex = 334;
            this.forcestopDiecheckBox.Text = "ForceStop Die";
            this.forcestopDiecheckBox.UseVisualStyleBackColor = true;
            // 
            // android11checkBox
            // 
            this.android11checkBox.AutoSize = true;
            this.android11checkBox.Checked = true;
            this.android11checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.android11checkBox.Location = new System.Drawing.Point(10, 212);
            this.android11checkBox.Name = "android11checkBox";
            this.android11checkBox.Size = new System.Drawing.Size(128, 29);
            this.android11checkBox.TabIndex = 333;
            this.android11checkBox.Text = "Android11";
            this.android11checkBox.UseVisualStyleBackColor = true;
            // 
            // chuyenVeri4gcheckBox
            // 
            this.chuyenVeri4gcheckBox.AutoSize = true;
            this.chuyenVeri4gcheckBox.Location = new System.Drawing.Point(1297, 181);
            this.chuyenVeri4gcheckBox.Name = "chuyenVeri4gcheckBox";
            this.chuyenVeri4gcheckBox.Size = new System.Drawing.Size(170, 29);
            this.chuyenVeri4gcheckBox.TabIndex = 332;
            this.chuyenVeri4gcheckBox.Text = "Chuyển veri 4g";
            this.chuyenVeri4gcheckBox.UseVisualStyleBackColor = true;
            // 
            // giulaiportcheckBox
            // 
            this.giulaiportcheckBox.AutoSize = true;
            this.giulaiportcheckBox.Location = new System.Drawing.Point(1080, 152);
            this.giulaiportcheckBox.Name = "giulaiportcheckBox";
            this.giulaiportcheckBox.Size = new System.Drawing.Size(130, 29);
            this.giulaiportcheckBox.TabIndex = 331;
            this.giulaiportcheckBox.Text = "Giữ lại port";
            this.giulaiportcheckBox.UseVisualStyleBackColor = true;
            // 
            // proxySharecheckBox
            // 
            this.proxySharecheckBox.AutoSize = true;
            this.proxySharecheckBox.Location = new System.Drawing.Point(279, 40);
            this.proxySharecheckBox.Name = "proxySharecheckBox";
            this.proxySharecheckBox.Size = new System.Drawing.Size(151, 29);
            this.proxySharecheckBox.TabIndex = 330;
            this.proxySharecheckBox.Text = "Proxy  Share";
            this.proxySharecheckBox.UseVisualStyleBackColor = true;
            // 
            // setMaxMailbutton
            // 
            this.setMaxMailbutton.Location = new System.Drawing.Point(126, 117);
            this.setMaxMailbutton.Name = "setMaxMailbutton";
            this.setMaxMailbutton.Size = new System.Drawing.Size(56, 23);
            this.setMaxMailbutton.TabIndex = 329;
            this.setMaxMailbutton.Text = "Set";
            this.setMaxMailbutton.UseVisualStyleBackColor = true;
            this.setMaxMailbutton.Click += new System.EventHandler(this.setMaxMailbutton_Click);
            // 
            // getMailcheckBox
            // 
            this.getMailcheckBox.AutoSize = true;
            this.getMailcheckBox.Location = new System.Drawing.Point(151, 30);
            this.getMailcheckBox.Name = "getMailcheckBox";
            this.getMailcheckBox.Size = new System.Drawing.Size(131, 29);
            this.getMailcheckBox.TabIndex = 328;
            this.getMailcheckBox.Text = "GET MAIL";
            this.getMailcheckBox.UseVisualStyleBackColor = true;
            this.getMailcheckBox.CheckedChanged += new System.EventHandler(this.getMailcheckBox_CheckedChanged);
            // 
            // maxMailtextBox
            // 
            this.maxMailtextBox.Location = new System.Drawing.Point(202, 117);
            this.maxMailtextBox.Name = "maxMailtextBox";
            this.maxMailtextBox.Size = new System.Drawing.Size(43, 29);
            this.maxMailtextBox.TabIndex = 327;
            this.maxMailtextBox.Text = "1";
            // 
            // maxMaillabel
            // 
            this.maxMaillabel.AutoSize = true;
            this.maxMaillabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxMaillabel.ForeColor = System.Drawing.Color.Red;
            this.maxMaillabel.Location = new System.Drawing.Point(118, 87);
            this.maxMaillabel.Name = "maxMaillabel";
            this.maxMaillabel.Size = new System.Drawing.Size(227, 59);
            this.maxMaillabel.TabIndex = 326;
            this.maxMaillabel.Text = "Max Mail";
            // 
            // virtualDevicetextBox
            // 
            this.virtualDevicetextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.virtualDevicetextBox.ForeColor = System.Drawing.Color.Red;
            this.virtualDevicetextBox.Location = new System.Drawing.Point(159, 50);
            this.virtualDevicetextBox.Name = "virtualDevicetextBox";
            this.virtualDevicetextBox.Size = new System.Drawing.Size(68, 61);
            this.virtualDevicetextBox.TabIndex = 325;
            this.virtualDevicetextBox.Text = "0";
            // 
            // rootRom11checkBox
            // 
            this.rootRom11checkBox.AutoSize = true;
            this.rootRom11checkBox.Location = new System.Drawing.Point(280, 266);
            this.rootRom11checkBox.Name = "rootRom11checkBox";
            this.rootRom11checkBox.Size = new System.Drawing.Size(197, 29);
            this.rootRom11checkBox.TabIndex = 324;
            this.rootRom11checkBox.Text = "rootRomcheckBox";
            this.rootRom11checkBox.UseVisualStyleBackColor = true;
            // 
            // docMailEducheckBox
            // 
            this.docMailEducheckBox.AutoSize = true;
            this.docMailEducheckBox.Location = new System.Drawing.Point(911, 271);
            this.docMailEducheckBox.Name = "docMailEducheckBox";
            this.docMailEducheckBox.Size = new System.Drawing.Size(151, 29);
            this.docMailEducheckBox.TabIndex = 323;
            this.docMailEducheckBox.Text = "Đọc mail edu";
            this.docMailEducheckBox.UseVisualStyleBackColor = true;
            // 
            // p2ProxycheckBox
            // 
            this.p2ProxycheckBox.AutoSize = true;
            this.p2ProxycheckBox.Location = new System.Drawing.Point(241, 207);
            this.p2ProxycheckBox.Name = "p2ProxycheckBox";
            this.p2ProxycheckBox.Size = new System.Drawing.Size(62, 29);
            this.p2ProxycheckBox.TabIndex = 322;
            this.p2ProxycheckBox.Text = "P2";
            this.p2ProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // soLanLayMailtextBox
            // 
            this.soLanLayMailtextBox.ForeColor = System.Drawing.Color.Red;
            this.soLanLayMailtextBox.Location = new System.Drawing.Point(394, 132);
            this.soLanLayMailtextBox.Name = "soLanLayMailtextBox";
            this.soLanLayMailtextBox.Size = new System.Drawing.Size(32, 29);
            this.soLanLayMailtextBox.TabIndex = 321;
            this.soLanLayMailtextBox.Text = "100";
            // 
            // boquaProxyVncheckBox
            // 
            this.boquaProxyVncheckBox.AutoSize = true;
            this.boquaProxyVncheckBox.Location = new System.Drawing.Point(280, 127);
            this.boquaProxyVncheckBox.Name = "boquaProxyVncheckBox";
            this.boquaProxyVncheckBox.Size = new System.Drawing.Size(186, 29);
            this.boquaProxyVncheckBox.TabIndex = 320;
            this.boquaProxyVncheckBox.Text = "Bỏ qua proxy VN";
            this.boquaProxyVncheckBox.UseVisualStyleBackColor = true;
            // 
            // checkVericheckBox
            // 
            this.checkVericheckBox.AutoSize = true;
            this.checkVericheckBox.BackColor = System.Drawing.Color.Aqua;
            this.checkVericheckBox.Location = new System.Drawing.Point(1069, 192);
            this.checkVericheckBox.Name = "checkVericheckBox";
            this.checkVericheckBox.Size = new System.Drawing.Size(130, 29);
            this.checkVericheckBox.TabIndex = 319;
            this.checkVericheckBox.Text = "CheckVeri";
            this.checkVericheckBox.UseVisualStyleBackColor = false;
            // 
            // chaydocheckBox
            // 
            this.chaydocheckBox.AutoSize = true;
            this.chaydocheckBox.BackColor = System.Drawing.Color.Lime;
            this.chaydocheckBox.Location = new System.Drawing.Point(1069, 173);
            this.chaydocheckBox.Name = "chaydocheckBox";
            this.chaydocheckBox.Size = new System.Drawing.Size(112, 29);
            this.chaydocheckBox.TabIndex = 318;
            this.chaydocheckBox.Text = "Chạy dò";
            this.chaydocheckBox.UseVisualStyleBackColor = false;
            // 
            // checkTopProxycheckBox
            // 
            this.checkTopProxycheckBox.AutoSize = true;
            this.checkTopProxycheckBox.Location = new System.Drawing.Point(280, 246);
            this.checkTopProxycheckBox.Name = "checkTopProxycheckBox";
            this.checkTopProxycheckBox.Size = new System.Drawing.Size(190, 29);
            this.checkTopProxycheckBox.TabIndex = 317;
            this.checkTopProxycheckBox.Text = "Check Top Proxy";
            this.checkTopProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // chuyenKeyVnicheckBox
            // 
            this.chuyenKeyVnicheckBox.AutoSize = true;
            this.chuyenKeyVnicheckBox.Location = new System.Drawing.Point(625, 66);
            this.chuyenKeyVnicheckBox.Name = "chuyenKeyVnicheckBox";
            this.chuyenKeyVnicheckBox.Size = new System.Drawing.Size(177, 29);
            this.chuyenKeyVnicheckBox.TabIndex = 315;
            this.chuyenKeyVnicheckBox.Text = "Chuyển key Vni";
            this.chuyenKeyVnicheckBox.UseVisualStyleBackColor = true;
            // 
            // uuTienChay4GcheckBox
            // 
            this.uuTienChay4GcheckBox.AutoSize = true;
            this.uuTienChay4GcheckBox.Location = new System.Drawing.Point(158, 237);
            this.uuTienChay4GcheckBox.Name = "uuTienChay4GcheckBox";
            this.uuTienChay4GcheckBox.Size = new System.Drawing.Size(177, 29);
            this.uuTienChay4GcheckBox.TabIndex = 314;
            this.uuTienChay4GcheckBox.Text = "Ưu tiên chạy 4G";
            this.uuTienChay4GcheckBox.UseVisualStyleBackColor = true;
            // 
            // namServercheckBox
            // 
            this.namServercheckBox.AutoSize = true;
            this.namServercheckBox.Location = new System.Drawing.Point(1183, 231);
            this.namServercheckBox.Name = "namServercheckBox";
            this.namServercheckBox.Size = new System.Drawing.Size(137, 29);
            this.namServercheckBox.TabIndex = 313;
            this.namServercheckBox.Text = "NamServer";
            this.namServercheckBox.UseVisualStyleBackColor = true;
            // 
            // InitialPhonecheckBox
            // 
            this.InitialPhonecheckBox.AutoSize = true;
            this.InitialPhonecheckBox.Location = new System.Drawing.Point(625, 50);
            this.InitialPhonecheckBox.Name = "InitialPhonecheckBox";
            this.InitialPhonecheckBox.Size = new System.Drawing.Size(170, 29);
            this.InitialPhonecheckBox.TabIndex = 312;
            this.InitialPhonecheckBox.Text = "Khởi tạo phone";
            this.InitialPhonecheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyKeycheckBox
            // 
            this.proxyKeycheckBox.AutoSize = true;
            this.proxyKeycheckBox.Location = new System.Drawing.Point(241, 175);
            this.proxyKeycheckBox.Name = "proxyKeycheckBox";
            this.proxyKeycheckBox.Size = new System.Drawing.Size(73, 29);
            this.proxyKeycheckBox.TabIndex = 312;
            this.proxyKeycheckBox.Text = "Key";
            this.proxyKeycheckBox.UseVisualStyleBackColor = true;
            // 
            // ipRangeLantextBox
            // 
            this.ipRangeLantextBox.Location = new System.Drawing.Point(1250, 67);
            this.ipRangeLantextBox.Name = "ipRangeLantextBox";
            this.ipRangeLantextBox.Size = new System.Drawing.Size(100, 29);
            this.ipRangeLantextBox.TabIndex = 310;
            this.ipRangeLantextBox.Text = "192.168.105.";
            // 
            // showInfoDevicecheckBox
            // 
            this.showInfoDevicecheckBox.AutoSize = true;
            this.showInfoDevicecheckBox.Location = new System.Drawing.Point(279, 85);
            this.showInfoDevicecheckBox.Name = "showInfoDevicecheckBox";
            this.showInfoDevicecheckBox.Size = new System.Drawing.Size(209, 29);
            this.showInfoDevicecheckBox.TabIndex = 269;
            this.showInfoDevicecheckBox.Text = "Show thông tin máy";
            this.showInfoDevicecheckBox.UseVisualStyleBackColor = true;
            // 
            // UsLanguagecheckBox
            // 
            this.UsLanguagecheckBox.AutoSize = true;
            this.UsLanguagecheckBox.Location = new System.Drawing.Point(280, 103);
            this.UsLanguagecheckBox.Name = "UsLanguagecheckBox";
            this.UsLanguagecheckBox.Size = new System.Drawing.Size(224, 29);
            this.UsLanguagecheckBox.TabIndex = 309;
            this.UsLanguagecheckBox.Text = "US Device Language";
            this.UsLanguagecheckBox.UseVisualStyleBackColor = true;
            this.UsLanguagecheckBox.CheckedChanged += new System.EventHandler(this.UsLanguagecheckBox_CheckedChanged);
            // 
            // p3ProxycheckBox
            // 
            this.p3ProxycheckBox.AutoSize = true;
            this.p3ProxycheckBox.Checked = true;
            this.p3ProxycheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.p3ProxycheckBox.Location = new System.Drawing.Point(241, 191);
            this.p3ProxycheckBox.Name = "p3ProxycheckBox";
            this.p3ProxycheckBox.Size = new System.Drawing.Size(62, 29);
            this.p3ProxycheckBox.TabIndex = 308;
            this.p3ProxycheckBox.Text = "P3";
            this.p3ProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // p1ProxycheckBox
            // 
            this.p1ProxycheckBox.AutoSize = true;
            this.p1ProxycheckBox.Location = new System.Drawing.Point(241, 223);
            this.p1ProxycheckBox.Name = "p1ProxycheckBox";
            this.p1ProxycheckBox.Size = new System.Drawing.Size(62, 29);
            this.p1ProxycheckBox.TabIndex = 307;
            this.p1ProxycheckBox.Text = "P1";
            this.p1ProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyWificheckBox
            // 
            this.proxyWificheckBox.AutoSize = true;
            this.proxyWificheckBox.Location = new System.Drawing.Point(158, 149);
            this.proxyWificheckBox.Name = "proxyWificheckBox";
            this.proxyWificheckBox.Size = new System.Drawing.Size(122, 29);
            this.proxyWificheckBox.TabIndex = 306;
            this.proxyWificheckBox.Text = "Proxy Wfi";
            this.proxyWificheckBox.UseVisualStyleBackColor = true;
            // 
            // proxyCMDcheckBox
            // 
            this.proxyCMDcheckBox.AutoSize = true;
            this.proxyCMDcheckBox.Location = new System.Drawing.Point(158, 168);
            this.proxyCMDcheckBox.Name = "proxyCMDcheckBox";
            this.proxyCMDcheckBox.Size = new System.Drawing.Size(134, 29);
            this.proxyCMDcheckBox.TabIndex = 304;
            this.proxyCMDcheckBox.Text = "ProxyCMD";
            this.proxyCMDcheckBox.UseVisualStyleBackColor = true;
            // 
            // findPhonecheckBox
            // 
            this.findPhonecheckBox.AutoSize = true;
            this.findPhonecheckBox.Location = new System.Drawing.Point(1200, 91);
            this.findPhonecheckBox.Name = "findPhonecheckBox";
            this.findPhonecheckBox.Size = new System.Drawing.Size(131, 29);
            this.findPhonecheckBox.TabIndex = 302;
            this.findPhonecheckBox.Text = "Tìm phone";
            this.findPhonecheckBox.UseVisualStyleBackColor = true;
            // 
            // changePhoneNumbercheckBox
            // 
            this.changePhoneNumbercheckBox.AutoSize = true;
            this.changePhoneNumbercheckBox.Location = new System.Drawing.Point(1200, 114);
            this.changePhoneNumbercheckBox.Name = "changePhoneNumbercheckBox";
            this.changePhoneNumbercheckBox.Size = new System.Drawing.Size(182, 29);
            this.changePhoneNumbercheckBox.TabIndex = 301;
            this.changePhoneNumbercheckBox.Text = "Đổi số điện thoại";
            this.changePhoneNumbercheckBox.UseVisualStyleBackColor = true;
            // 
            // proxy4GcheckBox
            // 
            this.proxy4GcheckBox.AutoSize = true;
            this.proxy4GcheckBox.Location = new System.Drawing.Point(158, 184);
            this.proxy4GcheckBox.Name = "proxy4GcheckBox";
            this.proxy4GcheckBox.Size = new System.Drawing.Size(119, 29);
            this.proxy4GcheckBox.TabIndex = 238;
            this.proxy4GcheckBox.Text = "Proxy 4G";
            this.proxy4GcheckBox.UseVisualStyleBackColor = true;
            // 
            // chạyDoiTenDemcheckBox
            // 
            this.chạyDoiTenDemcheckBox.AutoSize = true;
            this.chạyDoiTenDemcheckBox.Location = new System.Drawing.Point(280, 161);
            this.chạyDoiTenDemcheckBox.Name = "chạyDoiTenDemcheckBox";
            this.chạyDoiTenDemcheckBox.Size = new System.Drawing.Size(205, 29);
            this.chạyDoiTenDemcheckBox.TabIndex = 295;
            this.chạyDoiTenDemcheckBox.Text = "Chạy Đổi Tên Đêm";
            this.chạyDoiTenDemcheckBox.UseVisualStyleBackColor = true;
            this.chạyDoiTenDemcheckBox.CheckedChanged += new System.EventHandler(this.chạyDoiTenDemcheckBox_CheckedChanged);
            // 
            // getHotmailKieumoicheckBox
            // 
            this.getHotmailKieumoicheckBox.AutoSize = true;
            this.getHotmailKieumoicheckBox.Checked = true;
            this.getHotmailKieumoicheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.getHotmailKieumoicheckBox.Location = new System.Drawing.Point(10, 140);
            this.getHotmailKieumoicheckBox.Name = "getHotmailKieumoicheckBox";
            this.getHotmailKieumoicheckBox.Size = new System.Drawing.Size(220, 29);
            this.getHotmailKieumoicheckBox.TabIndex = 293;
            this.getHotmailKieumoicheckBox.Text = "Get Hotmail Kiểu mới";
            this.getHotmailKieumoicheckBox.UseVisualStyleBackColor = true;
            // 
            // moiKatanaNhanhcheckBox
            // 
            this.moiKatanaNhanhcheckBox.AutoSize = true;
            this.moiKatanaNhanhcheckBox.Checked = true;
            this.moiKatanaNhanhcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moiKatanaNhanhcheckBox.Location = new System.Drawing.Point(10, 274);
            this.moiKatanaNhanhcheckBox.Name = "moiKatanaNhanhcheckBox";
            this.moiKatanaNhanhcheckBox.Size = new System.Drawing.Size(194, 29);
            this.moiKatanaNhanhcheckBox.TabIndex = 292;
            this.moiKatanaNhanhcheckBox.Text = "Mồi katana nhanh";
            this.moiKatanaNhanhcheckBox.UseVisualStyleBackColor = true;
            // 
            // epMoiThanhCongcheckBox
            // 
            this.epMoiThanhCongcheckBox.AutoSize = true;
            this.epMoiThanhCongcheckBox.Location = new System.Drawing.Point(9, 257);
            this.epMoiThanhCongcheckBox.Name = "epMoiThanhCongcheckBox";
            this.epMoiThanhCongcheckBox.Size = new System.Drawing.Size(200, 29);
            this.epMoiThanhCongcheckBox.TabIndex = 291;
            this.epMoiThanhCongcheckBox.Text = "Ép mồi thành công";
            this.epMoiThanhCongcheckBox.UseVisualStyleBackColor = true;
            // 
            // superProxycheckBox
            // 
            this.superProxycheckBox.AutoSize = true;
            this.superProxycheckBox.Location = new System.Drawing.Point(1163, 65);
            this.superProxycheckBox.Name = "superProxycheckBox";
            this.superProxycheckBox.Size = new System.Drawing.Size(141, 29);
            this.superProxycheckBox.TabIndex = 284;
            this.superProxycheckBox.Text = "SuperProxy";
            this.superProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // moiAccRegThanhCongcheckBox
            // 
            this.moiAccRegThanhCongcheckBox.AutoSize = true;
            this.moiAccRegThanhCongcheckBox.Location = new System.Drawing.Point(10, 239);
            this.moiAccRegThanhCongcheckBox.Name = "moiAccRegThanhCongcheckBox";
            this.moiAccRegThanhCongcheckBox.Size = new System.Drawing.Size(241, 29);
            this.moiAccRegThanhCongcheckBox.TabIndex = 290;
            this.moiAccRegThanhCongcheckBox.Text = "Mồi acc reg thành công";
            this.moiAccRegThanhCongcheckBox.UseVisualStyleBackColor = true;
            // 
            // randomProxySim2checkBox
            // 
            this.randomProxySim2checkBox.AutoSize = true;
            this.randomProxySim2checkBox.Location = new System.Drawing.Point(158, 268);
            this.randomProxySim2checkBox.Name = "randomProxySim2checkBox";
            this.randomProxySim2checkBox.Size = new System.Drawing.Size(200, 29);
            this.randomProxySim2checkBox.TabIndex = 289;
            this.randomProxySim2checkBox.Text = "Random proxy/sim";
            this.randomProxySim2checkBox.UseVisualStyleBackColor = true;
            this.randomProxySim2checkBox.CheckedChanged += new System.EventHandler(this.randomProxySim2checkBox_CheckedChanged);
            // 
            // proxyFromServercheckBox
            // 
            this.proxyFromServercheckBox.AutoSize = true;
            this.proxyFromServercheckBox.Checked = true;
            this.proxyFromServercheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.proxyFromServercheckBox.Location = new System.Drawing.Point(158, 253);
            this.proxyFromServercheckBox.Name = "proxyFromServercheckBox";
            this.proxyFromServercheckBox.Size = new System.Drawing.Size(201, 29);
            this.proxyFromServercheckBox.TabIndex = 288;
            this.proxyFromServercheckBox.Text = "Proxy From Server";
            this.proxyFromServercheckBox.UseVisualStyleBackColor = true;
            this.proxyFromServercheckBox.CheckedChanged += new System.EventHandler(this.proxyFromServercheckBox_CheckedChanged);
            // 
            // name3wordcheckBox
            // 
            this.name3wordcheckBox.AutoSize = true;
            this.name3wordcheckBox.Location = new System.Drawing.Point(10, 156);
            this.name3wordcheckBox.Name = "name3wordcheckBox";
            this.name3wordcheckBox.Size = new System.Drawing.Size(126, 29);
            this.name3wordcheckBox.TabIndex = 287;
            this.name3wordcheckBox.Text = "Tên 3 chữ";
            this.name3wordcheckBox.UseVisualStyleBackColor = true;
            // 
            // gichuTrenAvatarcheckBox
            // 
            this.gichuTrenAvatarcheckBox.AutoSize = true;
            this.gichuTrenAvatarcheckBox.Location = new System.Drawing.Point(1271, 91);
            this.gichuTrenAvatarcheckBox.Name = "gichuTrenAvatarcheckBox";
            this.gichuTrenAvatarcheckBox.Size = new System.Drawing.Size(218, 29);
            this.gichuTrenAvatarcheckBox.TabIndex = 286;
            this.gichuTrenAvatarcheckBox.Text = "Ghi Chữ Trên Avatar";
            this.gichuTrenAvatarcheckBox.UseVisualStyleBackColor = true;
            // 
            // doitenVncheckBox
            // 
            this.doitenVncheckBox.AutoSize = true;
            this.doitenVncheckBox.Location = new System.Drawing.Point(538, 50);
            this.doitenVncheckBox.Name = "doitenVncheckBox";
            this.doitenVncheckBox.Size = new System.Drawing.Size(132, 29);
            this.doitenVncheckBox.TabIndex = 285;
            this.doitenVncheckBox.Text = "Đổi tên VN";
            this.doitenVncheckBox.UseVisualStyleBackColor = true;
            this.doitenVncheckBox.CheckedChanged += new System.EventHandler(this.doitenVncheckBox_CheckedChanged);
            // 
            // nameUsVncheckBox
            // 
            this.nameUsVncheckBox.AutoSize = true;
            this.nameUsVncheckBox.Location = new System.Drawing.Point(280, 230);
            this.nameUsVncheckBox.Name = "nameUsVncheckBox";
            this.nameUsVncheckBox.Size = new System.Drawing.Size(96, 29);
            this.nameUsVncheckBox.TabIndex = 284;
            this.nameUsVncheckBox.Text = "Họ US";
            this.nameUsVncheckBox.UseVisualStyleBackColor = true;
            // 
            // nameVnUscheckBox
            // 
            this.nameVnUscheckBox.AutoSize = true;
            this.nameVnUscheckBox.Location = new System.Drawing.Point(280, 214);
            this.nameVnUscheckBox.Name = "nameVnUscheckBox";
            this.nameVnUscheckBox.Size = new System.Drawing.Size(106, 29);
            this.nameVnUscheckBox.TabIndex = 283;
            this.nameVnUscheckBox.Text = "Tên US";
            this.nameVnUscheckBox.UseVisualStyleBackColor = true;
            // 
            // forceGmailcheckBox
            // 
            this.forceGmailcheckBox.AutoSize = true;
            this.forceGmailcheckBox.Checked = true;
            this.forceGmailcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.forceGmailcheckBox.Location = new System.Drawing.Point(10, 172);
            this.forceGmailcheckBox.Name = "forceGmailcheckBox";
            this.forceGmailcheckBox.Size = new System.Drawing.Size(143, 29);
            this.forceGmailcheckBox.TabIndex = 282;
            this.forceGmailcheckBox.Text = "Force Gmail";
            this.forceGmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button11.Location = new System.Drawing.Point(2002, 113);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(131, 27);
            this.button11.TabIndex = 281;
            this.button11.Text = "Run All Restart";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // showIpcheckBox
            // 
            this.showIpcheckBox.AutoSize = true;
            this.showIpcheckBox.Checked = true;
            this.showIpcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showIpcheckBox.Location = new System.Drawing.Point(280, 196);
            this.showIpcheckBox.Name = "showIpcheckBox";
            this.showIpcheckBox.Size = new System.Drawing.Size(111, 29);
            this.showIpcheckBox.TabIndex = 280;
            this.showIpcheckBox.Text = "Show IP";
            this.showIpcheckBox.UseVisualStyleBackColor = true;
            // 
            // reinstallSaudiecheckBox
            // 
            this.reinstallSaudiecheckBox.AutoSize = true;
            this.reinstallSaudiecheckBox.Location = new System.Drawing.Point(280, 178);
            this.reinstallSaudiecheckBox.Name = "reinstallSaudiecheckBox";
            this.reinstallSaudiecheckBox.Size = new System.Drawing.Size(179, 29);
            this.reinstallSaudiecheckBox.TabIndex = 275;
            this.reinstallSaudiecheckBox.Text = "Reinstall sau die";
            this.reinstallSaudiecheckBox.UseVisualStyleBackColor = true;
            // 
            // changer60checkBox
            // 
            this.changer60checkBox.AutoSize = true;
            this.changer60checkBox.Location = new System.Drawing.Point(408, 62);
            this.changer60checkBox.Name = "changer60checkBox";
            this.changer60checkBox.Size = new System.Drawing.Size(196, 29);
            this.changer60checkBox.TabIndex = 273;
            this.changer60checkBox.Text = "Changer 60phone";
            this.changer60checkBox.UseVisualStyleBackColor = true;
            // 
            // clearAllAccSettingcheckBox
            // 
            this.clearAllAccSettingcheckBox.AutoSize = true;
            this.clearAllAccSettingcheckBox.Checked = true;
            this.clearAllAccSettingcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearAllAccSettingcheckBox.Location = new System.Drawing.Point(1221, 269);
            this.clearAllAccSettingcheckBox.Name = "clearAllAccSettingcheckBox";
            this.clearAllAccSettingcheckBox.Size = new System.Drawing.Size(227, 29);
            this.clearAllAccSettingcheckBox.TabIndex = 274;
            this.clearAllAccSettingcheckBox.Text = "Clear all acc in setting";
            this.clearAllAccSettingcheckBox.UseVisualStyleBackColor = true;
            // 
            // thoatOtpcheckBox
            // 
            this.thoatOtpcheckBox.AutoSize = true;
            this.thoatOtpcheckBox.Checked = true;
            this.thoatOtpcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thoatOtpcheckBox.Location = new System.Drawing.Point(910, 244);
            this.thoatOtpcheckBox.Name = "thoatOtpcheckBox";
            this.thoatOtpcheckBox.Size = new System.Drawing.Size(153, 29);
            this.thoatOtpcheckBox.TabIndex = 273;
            this.thoatOtpcheckBox.Text = "Thoat get otp";
            this.thoatOtpcheckBox.UseVisualStyleBackColor = true;
            this.thoatOtpcheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // thoatGmailcheckBox
            // 
            this.thoatGmailcheckBox.AutoSize = true;
            this.thoatGmailcheckBox.Location = new System.Drawing.Point(910, 227);
            this.thoatGmailcheckBox.Name = "thoatGmailcheckBox";
            this.thoatGmailcheckBox.Size = new System.Drawing.Size(140, 29);
            this.thoatGmailcheckBox.TabIndex = 272;
            this.thoatGmailcheckBox.Text = "Thoát gmail";
            this.thoatGmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // storeAccMoicheckBox
            // 
            this.storeAccMoicheckBox.AutoSize = true;
            this.storeAccMoicheckBox.Location = new System.Drawing.Point(1164, 245);
            this.storeAccMoicheckBox.Name = "storeAccMoicheckBox";
            this.storeAccMoicheckBox.Size = new System.Drawing.Size(143, 29);
            this.storeAccMoicheckBox.TabIndex = 271;
            this.storeAccMoicheckBox.Text = "Lưu acc mồi";
            this.storeAccMoicheckBox.UseVisualStyleBackColor = true;
            // 
            // set2faLoai2checkBox
            // 
            this.set2faLoai2checkBox.AutoSize = true;
            this.set2faLoai2checkBox.Location = new System.Drawing.Point(1069, 257);
            this.set2faLoai2checkBox.Name = "set2faLoai2checkBox";
            this.set2faLoai2checkBox.Size = new System.Drawing.Size(151, 29);
            this.set2faLoai2checkBox.TabIndex = 270;
            this.set2faLoai2checkBox.Text = "Set 2fa loai 2";
            this.set2faLoai2checkBox.UseVisualStyleBackColor = true;
            // 
            // veriaccgmailCheckBox
            // 
            this.veriaccgmailCheckBox.AutoSize = true;
            this.veriaccgmailCheckBox.Checked = true;
            this.veriaccgmailCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.veriaccgmailCheckBox.Location = new System.Drawing.Point(10, 189);
            this.veriaccgmailCheckBox.Name = "veriaccgmailCheckBox";
            this.veriaccgmailCheckBox.Size = new System.Drawing.Size(160, 29);
            this.veriaccgmailCheckBox.TabIndex = 269;
            this.veriaccgmailCheckBox.Text = "Veri acc gmail";
            this.veriaccgmailCheckBox.UseVisualStyleBackColor = true;
            // 
            // randomIp46CheckBox
            // 
            this.randomIp46CheckBox.AutoSize = true;
            this.randomIp46CheckBox.Location = new System.Drawing.Point(1070, 237);
            this.randomIp46CheckBox.Name = "randomIp46CheckBox";
            this.randomIp46CheckBox.Size = new System.Drawing.Size(162, 29);
            this.randomIp46CheckBox.TabIndex = 268;
            this.randomIp46CheckBox.Text = "Random IP4/6";
            this.randomIp46CheckBox.UseVisualStyleBackColor = true;
            // 
            // veriAccRegMailcheckBox
            // 
            this.veriAccRegMailcheckBox.AutoSize = true;
            this.veriAccRegMailcheckBox.Location = new System.Drawing.Point(1069, 275);
            this.veriAccRegMailcheckBox.Name = "veriAccRegMailcheckBox";
            this.veriAccRegMailcheckBox.Size = new System.Drawing.Size(193, 29);
            this.veriAccRegMailcheckBox.TabIndex = 267;
            this.veriAccRegMailcheckBox.Text = "Veri Acc Reg Mail";
            this.veriAccRegMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // laymailkhaccheckBox
            // 
            this.laymailkhaccheckBox.AutoSize = true;
            this.laymailkhaccheckBox.Checked = true;
            this.laymailkhaccheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.laymailkhaccheckBox.Location = new System.Drawing.Point(910, 207);
            this.laymailkhaccheckBox.Name = "laymailkhaccheckBox";
            this.laymailkhaccheckBox.Size = new System.Drawing.Size(157, 29);
            this.laymailkhaccheckBox.TabIndex = 266;
            this.laymailkhaccheckBox.Text = "Lấy mail khác";
            this.laymailkhaccheckBox.UseVisualStyleBackColor = true;
            // 
            // regByGmailcheckBox
            // 
            this.regByGmailcheckBox.AutoSize = true;
            this.regByGmailcheckBox.Location = new System.Drawing.Point(910, 189);
            this.regByGmailcheckBox.Name = "regByGmailcheckBox";
            this.regByGmailcheckBox.Size = new System.Drawing.Size(154, 29);
            this.regByGmailcheckBox.TabIndex = 265;
            this.regByGmailcheckBox.Text = "Reg by Gmail";
            this.regByGmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // checkDieStopCheckBox
            // 
            this.checkDieStopCheckBox.AutoSize = true;
            this.checkDieStopCheckBox.Checked = true;
            this.checkDieStopCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDieStopCheckBox.Location = new System.Drawing.Point(910, 171);
            this.checkDieStopCheckBox.Name = "checkDieStopCheckBox";
            this.checkDieStopCheckBox.Size = new System.Drawing.Size(171, 29);
            this.checkDieStopCheckBox.TabIndex = 256;
            this.checkDieStopCheckBox.Text = "Check Die stop";
            this.checkDieStopCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkChangeIpcheckBox);
            this.groupBox1.Controls.Add(this.hvlgmailcheckBox);
            this.groupBox1.Controls.Add(this.shopgmailLocalcheckBox);
            this.groupBox1.Controls.Add(this.sptVipcheckBox);
            this.groupBox1.Controls.Add(this.sptLocalcheckBox);
            this.groupBox1.Controls.Add(this.dvgmNormalcheckBox);
            this.groupBox1.Controls.Add(this.thuesimVipcheckBox);
            this.groupBox1.Controls.Add(this.nghi5phutsaudiecheckBox);
            this.groupBox1.Controls.Add(this.nghi1phutsaudiecheckBox);
            this.groupBox1.Controls.Add(this.dvgmcheckVipBox);
            this.groupBox1.Controls.Add(this.otpCheapcheckBox);
            this.groupBox1.Controls.Add(this.thuesimcheckBox);
            this.groupBox1.Controls.Add(this.oneSecradioButton);
            this.groupBox1.Controls.Add(this.getSuperMailcheckBox);
            this.groupBox1.Controls.Add(this.getSellMailCheckbox);
            this.groupBox1.Controls.Add(this.getDvgmcheckBox);
            this.groupBox1.Controls.Add(this.superTeamRadioButton);
            this.groupBox1.Controls.Add(this.forceVeriAccRegBMailcheckBox);
            this.groupBox1.Controls.Add(this.gmail30minradioButton);
            this.groupBox1.Controls.Add(this.dichvugmail2radioButton);
            this.groupBox1.Controls.Add(this.MailOtpRadioButton);
            this.groupBox1.Controls.Add(this.fakeEmailradioButton);
            this.groupBox1.Controls.Add(this.fakemailgeneratorradioButton);
            this.groupBox1.Controls.Add(this.tempmailLolradioButton);
            this.groupBox1.Controls.Add(this.generatorEmailradioButton);
            this.groupBox1.Controls.Add(this.dichvuGmailradioButton);
            this.groupBox1.Controls.Add(this.sellGmailradioButton);
            this.groupBox1.Controls.Add(this.forceSellgmailcheckBox);
            this.groupBox1.Controls.Add(this.holdingCheckBox);
            this.groupBox1.Controls.Add(this.otplabel);
            this.groupBox1.Location = new System.Drawing.Point(431, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 184);
            this.groupBox1.TabIndex = 262;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tempmail";
            // 
            // checkChangeIpcheckBox
            // 
            this.checkChangeIpcheckBox.AutoSize = true;
            this.checkChangeIpcheckBox.Location = new System.Drawing.Point(206, 22);
            this.checkChangeIpcheckBox.Name = "checkChangeIpcheckBox";
            this.checkChangeIpcheckBox.Size = new System.Drawing.Size(186, 29);
            this.checkChangeIpcheckBox.TabIndex = 291;
            this.checkChangeIpcheckBox.Text = "Check change Ip";
            this.checkChangeIpcheckBox.UseVisualStyleBackColor = true;
            // 
            // hvlgmailcheckBox
            // 
            this.hvlgmailcheckBox.AutoSize = true;
            this.hvlgmailcheckBox.Checked = true;
            this.hvlgmailcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hvlgmailcheckBox.Location = new System.Drawing.Point(202, 62);
            this.hvlgmailcheckBox.Name = "hvlgmailcheckBox";
            this.hvlgmailcheckBox.Size = new System.Drawing.Size(113, 29);
            this.hvlgmailcheckBox.TabIndex = 290;
            this.hvlgmailcheckBox.Text = "hvlGmail";
            this.hvlgmailcheckBox.UseVisualStyleBackColor = true;
            this.hvlgmailcheckBox.CheckedChanged += new System.EventHandler(this.hvlgmailcheckBox_CheckedChanged);
            // 
            // shopgmailLocalcheckBox
            // 
            this.shopgmailLocalcheckBox.AutoSize = true;
            this.shopgmailLocalcheckBox.Location = new System.Drawing.Point(283, 62);
            this.shopgmailLocalcheckBox.Name = "shopgmailLocalcheckBox";
            this.shopgmailLocalcheckBox.Size = new System.Drawing.Size(172, 29);
            this.shopgmailLocalcheckBox.TabIndex = 289;
            this.shopgmailLocalcheckBox.Text = "shopgmail local";
            this.shopgmailLocalcheckBox.UseVisualStyleBackColor = true;
            this.shopgmailLocalcheckBox.CheckedChanged += new System.EventHandler(this.shopgmailLocalcheckBox_CheckedChanged);
            // 
            // sptVipcheckBox
            // 
            this.sptVipcheckBox.AutoSize = true;
            this.sptVipcheckBox.Location = new System.Drawing.Point(202, 79);
            this.sptVipcheckBox.Name = "sptVipcheckBox";
            this.sptVipcheckBox.Size = new System.Drawing.Size(97, 29);
            this.sptVipcheckBox.TabIndex = 288;
            this.sptVipcheckBox.Text = "SptVip";
            this.sptVipcheckBox.UseVisualStyleBackColor = true;
            this.sptVipcheckBox.CheckedChanged += new System.EventHandler(this.sptVipcheckBox_CheckedChanged_1);
            // 
            // sptLocalcheckBox
            // 
            this.sptLocalcheckBox.AutoSize = true;
            this.sptLocalcheckBox.Location = new System.Drawing.Point(202, 98);
            this.sptLocalcheckBox.Name = "sptLocalcheckBox";
            this.sptLocalcheckBox.Size = new System.Drawing.Size(120, 29);
            this.sptLocalcheckBox.TabIndex = 287;
            this.sptLocalcheckBox.Text = "Spt Local";
            this.sptLocalcheckBox.UseVisualStyleBackColor = true;
            this.sptLocalcheckBox.CheckedChanged += new System.EventHandler(this.sptNormalcheckBox_CheckedChanged);
            // 
            // dvgmNormalcheckBox
            // 
            this.dvgmNormalcheckBox.AutoSize = true;
            this.dvgmNormalcheckBox.Location = new System.Drawing.Point(283, 98);
            this.dvgmNormalcheckBox.Name = "dvgmNormalcheckBox";
            this.dvgmNormalcheckBox.Size = new System.Drawing.Size(156, 29);
            this.dvgmNormalcheckBox.TabIndex = 285;
            this.dvgmNormalcheckBox.Text = "Dvgm Normal";
            this.dvgmNormalcheckBox.UseVisualStyleBackColor = true;
            this.dvgmNormalcheckBox.CheckedChanged += new System.EventHandler(this.dvgmNormalcheckBox_CheckedChanged);
            // 
            // thuesimVipcheckBox
            // 
            this.thuesimVipcheckBox.AutoSize = true;
            this.thuesimVipcheckBox.Location = new System.Drawing.Point(13, 97);
            this.thuesimVipcheckBox.Name = "thuesimVipcheckBox";
            this.thuesimVipcheckBox.Size = new System.Drawing.Size(143, 29);
            this.thuesimVipcheckBox.TabIndex = 284;
            this.thuesimVipcheckBox.Text = "ThuesimVip";
            this.thuesimVipcheckBox.UseVisualStyleBackColor = true;
            this.thuesimVipcheckBox.CheckedChanged += new System.EventHandler(this.thuesimVipcheckBox_CheckedChanged);
            // 
            // nghi5phutsaudiecheckBox
            // 
            this.nghi5phutsaudiecheckBox.AutoSize = true;
            this.nghi5phutsaudiecheckBox.Checked = true;
            this.nghi5phutsaudiecheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nghi5phutsaudiecheckBox.Location = new System.Drawing.Point(376, 111);
            this.nghi5phutsaudiecheckBox.Name = "nghi5phutsaudiecheckBox";
            this.nghi5phutsaudiecheckBox.Size = new System.Drawing.Size(200, 29);
            this.nghi5phutsaudiecheckBox.TabIndex = 283;
            this.nghi5phutsaudiecheckBox.Text = "Nghỉ 5phut sau die";
            this.nghi5phutsaudiecheckBox.UseVisualStyleBackColor = true;
            // 
            // nghi1phutsaudiecheckBox
            // 
            this.nghi1phutsaudiecheckBox.AutoSize = true;
            this.nghi1phutsaudiecheckBox.Location = new System.Drawing.Point(375, 91);
            this.nghi1phutsaudiecheckBox.Name = "nghi1phutsaudiecheckBox";
            this.nghi1phutsaudiecheckBox.Size = new System.Drawing.Size(163, 29);
            this.nghi1phutsaudiecheckBox.TabIndex = 282;
            this.nghi1phutsaudiecheckBox.Text = "Nghỉ 1phut die";
            this.nghi1phutsaudiecheckBox.UseVisualStyleBackColor = true;
            // 
            // dvgmcheckVipBox
            // 
            this.dvgmcheckVipBox.AutoSize = true;
            this.dvgmcheckVipBox.Location = new System.Drawing.Point(283, 79);
            this.dvgmcheckVipBox.Name = "dvgmcheckVipBox";
            this.dvgmcheckVipBox.Size = new System.Drawing.Size(123, 29);
            this.dvgmcheckVipBox.TabIndex = 281;
            this.dvgmcheckVipBox.Text = "Dvgm Vip";
            this.dvgmcheckVipBox.UseVisualStyleBackColor = true;
            this.dvgmcheckVipBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_4);
            // 
            // otpCheapcheckBox
            // 
            this.otpCheapcheckBox.AutoSize = true;
            this.otpCheapcheckBox.Checked = true;
            this.otpCheapcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.otpCheapcheckBox.Location = new System.Drawing.Point(139, 67);
            this.otpCheapcheckBox.Name = "otpCheapcheckBox";
            this.otpCheapcheckBox.Size = new System.Drawing.Size(129, 29);
            this.otpCheapcheckBox.TabIndex = 280;
            this.otpCheapcheckBox.Text = "OtpCheap";
            this.otpCheapcheckBox.UseVisualStyleBackColor = true;
            this.otpCheapcheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_3);
            // 
            // thuesimcheckBox
            // 
            this.thuesimcheckBox.AutoSize = true;
            this.thuesimcheckBox.Location = new System.Drawing.Point(103, 96);
            this.thuesimcheckBox.Name = "thuesimcheckBox";
            this.thuesimcheckBox.Size = new System.Drawing.Size(118, 29);
            this.thuesimcheckBox.TabIndex = 279;
            this.thuesimcheckBox.Text = "ThueSim";
            this.thuesimcheckBox.UseVisualStyleBackColor = true;
            this.thuesimcheckBox.CheckedChanged += new System.EventHandler(this.thuesimcheckBox_CheckedChanged);
            // 
            // oneSecradioButton
            // 
            this.oneSecradioButton.AutoSize = true;
            this.oneSecradioButton.Location = new System.Drawing.Point(137, 80);
            this.oneSecradioButton.Name = "oneSecradioButton";
            this.oneSecradioButton.Size = new System.Drawing.Size(79, 29);
            this.oneSecradioButton.TabIndex = 278;
            this.oneSecradioButton.TabStop = true;
            this.oneSecradioButton.Text = "1sec";
            this.oneSecradioButton.UseVisualStyleBackColor = true;
            this.oneSecradioButton.CheckedChanged += new System.EventHandler(this.oneSecradioButton_CheckedChanged);
            // 
            // getSuperMailcheckBox
            // 
            this.getSuperMailcheckBox.AutoSize = true;
            this.getSuperMailcheckBox.Checked = true;
            this.getSuperMailcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.getSuperMailcheckBox.Location = new System.Drawing.Point(109, 79);
            this.getSuperMailcheckBox.Name = "getSuperMailcheckBox";
            this.getSuperMailcheckBox.Size = new System.Drawing.Size(59, 29);
            this.getSuperMailcheckBox.TabIndex = 276;
            this.getSuperMailcheckBox.Text = "ch";
            this.getSuperMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // getSellMailCheckbox
            // 
            this.getSellMailCheckbox.AutoSize = true;
            this.getSellMailCheckbox.Location = new System.Drawing.Point(102, 62);
            this.getSellMailCheckbox.Name = "getSellMailCheckbox";
            this.getSellMailCheckbox.Size = new System.Drawing.Size(59, 29);
            this.getSellMailCheckbox.TabIndex = 275;
            this.getSellMailCheckbox.Text = "ch";
            this.getSellMailCheckbox.UseVisualStyleBackColor = true;
            // 
            // getDvgmcheckBox
            // 
            this.getDvgmcheckBox.AutoSize = true;
            this.getDvgmcheckBox.Checked = true;
            this.getDvgmcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.getDvgmcheckBox.Location = new System.Drawing.Point(103, 45);
            this.getDvgmcheckBox.Name = "getDvgmcheckBox";
            this.getDvgmcheckBox.Size = new System.Drawing.Size(59, 29);
            this.getDvgmcheckBox.TabIndex = 274;
            this.getDvgmcheckBox.Text = "ch";
            this.getDvgmcheckBox.UseVisualStyleBackColor = true;
            // 
            // superTeamRadioButton
            // 
            this.superTeamRadioButton.AutoSize = true;
            this.superTeamRadioButton.Checked = true;
            this.superTeamRadioButton.Location = new System.Drawing.Point(11, 81);
            this.superTeamRadioButton.Name = "superTeamRadioButton";
            this.superTeamRadioButton.Size = new System.Drawing.Size(180, 29);
            this.superTeamRadioButton.TabIndex = 272;
            this.superTeamRadioButton.TabStop = true;
            this.superTeamRadioButton.Text = "gmail.superteam";
            this.superTeamRadioButton.UseVisualStyleBackColor = true;
            this.superTeamRadioButton.CheckedChanged += new System.EventHandler(this.superTeamRadioButton_CheckedChanged);
            // 
            // forceVeriAccRegBMailcheckBox
            // 
            this.forceVeriAccRegBMailcheckBox.AutoSize = true;
            this.forceVeriAccRegBMailcheckBox.Location = new System.Drawing.Point(359, 7);
            this.forceVeriAccRegBMailcheckBox.Name = "forceVeriAccRegBMailcheckBox";
            this.forceVeriAccRegBMailcheckBox.Size = new System.Drawing.Size(249, 29);
            this.forceVeriAccRegBMailcheckBox.TabIndex = 270;
            this.forceVeriAccRegBMailcheckBox.Text = "Force veri acc reg b mail";
            this.forceVeriAccRegBMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // gmail30minradioButton
            // 
            this.gmail30minradioButton.AutoSize = true;
            this.gmail30minradioButton.Location = new System.Drawing.Point(359, 38);
            this.gmail30minradioButton.Name = "gmail30minradioButton";
            this.gmail30minradioButton.Size = new System.Drawing.Size(136, 29);
            this.gmail30minradioButton.TabIndex = 267;
            this.gmail30minradioButton.Text = "gmail30min";
            this.gmail30minradioButton.UseVisualStyleBackColor = true;
            // 
            // dichvugmail2radioButton
            // 
            this.dichvugmail2radioButton.AutoSize = true;
            this.dichvugmail2radioButton.Location = new System.Drawing.Point(360, 23);
            this.dichvugmail2radioButton.Name = "dichvugmail2radioButton";
            this.dichvugmail2radioButton.Size = new System.Drawing.Size(162, 29);
            this.dichvugmail2radioButton.TabIndex = 266;
            this.dichvugmail2radioButton.Text = "DichVuGmail2";
            this.dichvugmail2radioButton.UseVisualStyleBackColor = true;
            this.dichvugmail2radioButton.CheckedChanged += new System.EventHandler(this.dichvugmail2radioButton_CheckedChanged);
            // 
            // MailOtpRadioButton
            // 
            this.MailOtpRadioButton.AutoSize = true;
            this.MailOtpRadioButton.Location = new System.Drawing.Point(359, 54);
            this.MailOtpRadioButton.Name = "MailOtpRadioButton";
            this.MailOtpRadioButton.Size = new System.Drawing.Size(105, 29);
            this.MailOtpRadioButton.TabIndex = 265;
            this.MailOtpRadioButton.Text = "MailOtp";
            this.MailOtpRadioButton.UseVisualStyleBackColor = true;
            this.MailOtpRadioButton.CheckedChanged += new System.EventHandler(this.MailOtpRadioButton_CheckedChanged);
            // 
            // fakeEmailradioButton
            // 
            this.fakeEmailradioButton.AutoSize = true;
            this.fakeEmailradioButton.Location = new System.Drawing.Point(12, 30);
            this.fakeEmailradioButton.Name = "fakeEmailradioButton";
            this.fakeEmailradioButton.Size = new System.Drawing.Size(134, 29);
            this.fakeEmailradioButton.TabIndex = 260;
            this.fakeEmailradioButton.Text = "Fake Email";
            this.fakeEmailradioButton.UseVisualStyleBackColor = true;
            this.fakeEmailradioButton.CheckedChanged += new System.EventHandler(this.fakeEmailradioButton_CheckedChanged);
            // 
            // fakemailgeneratorradioButton
            // 
            this.fakemailgeneratorradioButton.AutoSize = true;
            this.fakemailgeneratorradioButton.Location = new System.Drawing.Point(359, 69);
            this.fakemailgeneratorradioButton.Name = "fakemailgeneratorradioButton";
            this.fakemailgeneratorradioButton.Size = new System.Drawing.Size(192, 29);
            this.fakemailgeneratorradioButton.TabIndex = 259;
            this.fakemailgeneratorradioButton.Text = "fakemailgenerator";
            this.fakemailgeneratorradioButton.UseVisualStyleBackColor = true;
            this.fakemailgeneratorradioButton.CheckedChanged += new System.EventHandler(this.fakemailgeneratorradioButton_CheckedChanged);
            // 
            // tempmailLolradioButton
            // 
            this.tempmailLolradioButton.AutoSize = true;
            this.tempmailLolradioButton.Location = new System.Drawing.Point(12, 48);
            this.tempmailLolradioButton.Name = "tempmailLolradioButton";
            this.tempmailLolradioButton.Size = new System.Drawing.Size(154, 29);
            this.tempmailLolradioButton.TabIndex = 1;
            this.tempmailLolradioButton.Text = "Tempmail Lol";
            this.tempmailLolradioButton.UseVisualStyleBackColor = true;
            this.tempmailLolradioButton.CheckedChanged += new System.EventHandler(this.tempmailLolradioButton_CheckedChanged);
            // 
            // generatorEmailradioButton
            // 
            this.generatorEmailradioButton.AutoSize = true;
            this.generatorEmailradioButton.Location = new System.Drawing.Point(12, 14);
            this.generatorEmailradioButton.Name = "generatorEmailradioButton";
            this.generatorEmailradioButton.Size = new System.Drawing.Size(177, 29);
            this.generatorEmailradioButton.TabIndex = 0;
            this.generatorEmailradioButton.Text = "Generator Email";
            this.generatorEmailradioButton.UseVisualStyleBackColor = true;
            this.generatorEmailradioButton.CheckedChanged += new System.EventHandler(this.generatorEmailradioButton_CheckedChanged);
            // 
            // dichvuGmailradioButton
            // 
            this.dichvuGmailradioButton.AutoSize = true;
            this.dichvuGmailradioButton.Location = new System.Drawing.Point(12, 66);
            this.dichvuGmailradioButton.Name = "dichvuGmailradioButton";
            this.dichvuGmailradioButton.Size = new System.Drawing.Size(161, 29);
            this.dichvuGmailradioButton.TabIndex = 257;
            this.dichvuGmailradioButton.Text = "Dich Vu Gmail";
            this.dichvuGmailradioButton.UseVisualStyleBackColor = true;
            this.dichvuGmailradioButton.CheckedChanged += new System.EventHandler(this.dichvuGmailradioButton_CheckedChanged);
            // 
            // sellGmailradioButton
            // 
            this.sellGmailradioButton.AutoSize = true;
            this.sellGmailradioButton.Location = new System.Drawing.Point(102, 29);
            this.sellGmailradioButton.Name = "sellGmailradioButton";
            this.sellGmailradioButton.Size = new System.Drawing.Size(125, 29);
            this.sellGmailradioButton.TabIndex = 258;
            this.sellGmailradioButton.Text = "Sell Gmail";
            this.sellGmailradioButton.UseVisualStyleBackColor = true;
            this.sellGmailradioButton.CheckedChanged += new System.EventHandler(this.sellGmailradioButton_CheckedChanged);
            // 
            // forceSellgmailcheckBox
            // 
            this.forceSellgmailcheckBox.AutoSize = true;
            this.forceSellgmailcheckBox.Location = new System.Drawing.Point(-1, 176);
            this.forceSellgmailcheckBox.Name = "forceSellgmailcheckBox";
            this.forceSellgmailcheckBox.Size = new System.Drawing.Size(168, 29);
            this.forceSellgmailcheckBox.TabIndex = 257;
            this.forceSellgmailcheckBox.Text = "Force sellgmail";
            this.forceSellgmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // otplabel
            // 
            this.otplabel.AutoSize = true;
            this.otplabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 43F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otplabel.ForeColor = System.Drawing.Color.Red;
            this.otplabel.Location = new System.Drawing.Point(15, 115);
            this.otplabel.Name = "otplabel";
            this.otplabel.Size = new System.Drawing.Size(104, 114);
            this.otplabel.TabIndex = 278;
            this.otplabel.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(1111, 87);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 25);
            this.label23.TabIndex = 255;
            this.label23.Text = "label23";
            // 
            // randomNewContactCheckBox
            // 
            this.randomNewContactCheckBox.AutoSize = true;
            this.randomNewContactCheckBox.Checked = true;
            this.randomNewContactCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomNewContactCheckBox.Location = new System.Drawing.Point(407, 99);
            this.randomNewContactCheckBox.Name = "randomNewContactCheckBox";
            this.randomNewContactCheckBox.Size = new System.Drawing.Size(221, 29);
            this.randomNewContactCheckBox.TabIndex = 246;
            this.randomNewContactCheckBox.Text = "Random Contact Mới";
            this.randomNewContactCheckBox.UseVisualStyleBackColor = true;
            this.randomNewContactCheckBox.CheckedChanged += new System.EventHandler(this.randomNewContactCheckBox_CheckedChanged);
            // 
            // removeProxyCheckBox
            // 
            this.removeProxyCheckBox.AutoSize = true;
            this.removeProxyCheckBox.Location = new System.Drawing.Point(986, 75);
            this.removeProxyCheckBox.Name = "removeProxyCheckBox";
            this.removeProxyCheckBox.Size = new System.Drawing.Size(127, 29);
            this.removeProxyCheckBox.TabIndex = 245;
            this.removeProxyCheckBox.Text = "Xóa proxy";
            this.removeProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // inputStringMailCheckBox
            // 
            this.inputStringMailCheckBox.AutoSize = true;
            this.inputStringMailCheckBox.Checked = true;
            this.inputStringMailCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.inputStringMailCheckBox.Location = new System.Drawing.Point(10, 122);
            this.inputStringMailCheckBox.Name = "inputStringMailCheckBox";
            this.inputStringMailCheckBox.Size = new System.Drawing.Size(192, 29);
            this.inputStringMailCheckBox.TabIndex = 243;
            this.inputStringMailCheckBox.Text = "Truyền chuỗi mail";
            this.inputStringMailCheckBox.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(974, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(51, 25);
            this.label19.TabIndex = 242;
            this.label19.Text = "Tỉnh";
            // 
            // locationProxyTextBox
            // 
            this.locationProxyTextBox.Location = new System.Drawing.Point(1003, 28);
            this.locationProxyTextBox.Name = "locationProxyTextBox";
            this.locationProxyTextBox.Size = new System.Drawing.Size(100, 29);
            this.locationProxyTextBox.TabIndex = 241;
            // 
            // forcePortraitCheckBox
            // 
            this.forcePortraitCheckBox.AutoSize = true;
            this.forcePortraitCheckBox.Location = new System.Drawing.Point(986, 51);
            this.forcePortraitCheckBox.Name = "forcePortraitCheckBox";
            this.forcePortraitCheckBox.Size = new System.Drawing.Size(152, 29);
            this.forcePortraitCheckBox.TabIndex = 240;
            this.forcePortraitCheckBox.Text = "Force portrait";
            this.forcePortraitCheckBox.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label18.Location = new System.Drawing.Point(910, 145);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 25);
            this.label18.TabIndex = 237;
            this.label18.Text = "Live";
            // 
            // reupRegCheckBox
            // 
            this.reupRegCheckBox.AutoSize = true;
            this.reupRegCheckBox.Location = new System.Drawing.Point(990, 234);
            this.reupRegCheckBox.Name = "reupRegCheckBox";
            this.reupRegCheckBox.Size = new System.Drawing.Size(124, 29);
            this.reupRegCheckBox.TabIndex = 238;
            this.reupRegCheckBox.Text = "Reup Reg";
            this.reupRegCheckBox.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label17.Location = new System.Drawing.Point(910, 123);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 25);
            this.label17.TabIndex = 236;
            this.label17.Text = "Fail";
            // 
            // maxLiveClearTextBox
            // 
            this.maxLiveClearTextBox.Location = new System.Drawing.Point(965, 149);
            this.maxLiveClearTextBox.Name = "maxLiveClearTextBox";
            this.maxLiveClearTextBox.Size = new System.Drawing.Size(100, 29);
            this.maxLiveClearTextBox.TabIndex = 235;
            this.maxLiveClearTextBox.Text = "1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1164, 195);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(192, 25);
            this.label15.TabIndex = 233;
            this.label15.Text = "Nghỉ giữa mỗi lần die";
            // 
            // delayAfterDieTextBox
            // 
            this.delayAfterDieTextBox.Location = new System.Drawing.Point(1164, 211);
            this.delayAfterDieTextBox.Name = "delayAfterDieTextBox";
            this.delayAfterDieTextBox.Size = new System.Drawing.Size(100, 29);
            this.delayAfterDieTextBox.TabIndex = 232;
            this.delayAfterDieTextBox.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(967, 102);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(190, 25);
            this.label14.TabIndex = 231;
            this.label14.Text = "Số vòng clear cache";
            // 
            // maxFailClearTextBox
            // 
            this.maxFailClearTextBox.Location = new System.Drawing.Point(965, 123);
            this.maxFailClearTextBox.Name = "maxFailClearTextBox";
            this.maxFailClearTextBox.Size = new System.Drawing.Size(100, 29);
            this.maxFailClearTextBox.TabIndex = 230;
            this.maxFailClearTextBox.Text = "0";
            // 
            // veriByProxyCheckBox
            // 
            this.veriByProxyCheckBox.AutoSize = true;
            this.veriByProxyCheckBox.Location = new System.Drawing.Point(1070, 217);
            this.veriByProxyCheckBox.Name = "veriByProxyCheckBox";
            this.veriByProxyCheckBox.Size = new System.Drawing.Size(152, 29);
            this.veriByProxyCheckBox.TabIndex = 229;
            this.veriByProxyCheckBox.Text = "Veri by proxy";
            this.veriByProxyCheckBox.UseVisualStyleBackColor = true;
            // 
            // releaseNoteLabel
            // 
            this.releaseNoteLabel.AutoSize = true;
            this.releaseNoteLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.releaseNoteLabel.ForeColor = System.Drawing.Color.Navy;
            this.releaseNoteLabel.Location = new System.Drawing.Point(706, -4);
            this.releaseNoteLabel.Name = "releaseNoteLabel";
            this.releaseNoteLabel.Size = new System.Drawing.Size(194, 79);
            this.releaseNoteLabel.TabIndex = 226;
            this.releaseNoteLabel.Text = "2096";
            this.releaseNoteLabel.Click += new System.EventHandler(this.releaseNoteLabel_Click);
            // 
            // forceDungMayCheckBox
            // 
            this.forceDungMayCheckBox.AutoSize = true;
            this.forceDungMayCheckBox.Location = new System.Drawing.Point(1080, 139);
            this.forceDungMayCheckBox.Name = "forceDungMayCheckBox";
            this.forceDungMayCheckBox.Size = new System.Drawing.Size(182, 29);
            this.forceDungMayCheckBox.TabIndex = 225;
            this.forceDungMayCheckBox.Text = "Force Đúng máy";
            this.forceDungMayCheckBox.UseVisualStyleBackColor = true;
            // 
            // avatarByCameraCheckBox
            // 
            this.avatarByCameraCheckBox.AutoSize = true;
            this.avatarByCameraCheckBox.Location = new System.Drawing.Point(625, 98);
            this.avatarByCameraCheckBox.Name = "avatarByCameraCheckBox";
            this.avatarByCameraCheckBox.Size = new System.Drawing.Size(191, 29);
            this.avatarByCameraCheckBox.TabIndex = 223;
            this.avatarByCameraCheckBox.Text = "Avatar by camera";
            this.avatarByCameraCheckBox.UseVisualStyleBackColor = true;
            // 
            // delayAfterRegTextBox
            // 
            this.delayAfterRegTextBox.Location = new System.Drawing.Point(1168, 166);
            this.delayAfterRegTextBox.Name = "delayAfterRegTextBox";
            this.delayAfterRegTextBox.Size = new System.Drawing.Size(100, 29);
            this.delayAfterRegTextBox.TabIndex = 227;
            this.delayAfterRegTextBox.Text = "1";
            // 
            // orderGroupBox
            // 
            this.orderGroupBox.Controls.Add(this.tamdungKiemTraAvatarcheckBox);
            this.orderGroupBox.Controls.Add(this.clearAccsettingsauregcheckBox);
            this.orderGroupBox.Controls.Add(this.uploadContactNewCheckbox);
            this.orderGroupBox.Controls.Add(this.coverNewcheckBox);
            this.orderGroupBox.Controls.Add(this.changeAllSim2checkBox);
            this.orderGroupBox.Controls.Add(this.set2faCheckbox);
            this.orderGroupBox.Controls.Add(this.runAvatarCheckbox);
            this.orderGroupBox.Location = new System.Drawing.Point(785, 20);
            this.orderGroupBox.Name = "orderGroupBox";
            this.orderGroupBox.Size = new System.Drawing.Size(187, 100);
            this.orderGroupBox.TabIndex = 222;
            this.orderGroupBox.TabStop = false;
            this.orderGroupBox.Text = "Order";
            // 
            // tamdungKiemTraAvatarcheckBox
            // 
            this.tamdungKiemTraAvatarcheckBox.AutoSize = true;
            this.tamdungKiemTraAvatarcheckBox.Location = new System.Drawing.Point(5, 82);
            this.tamdungKiemTraAvatarcheckBox.Name = "tamdungKiemTraAvatarcheckBox";
            this.tamdungKiemTraAvatarcheckBox.Size = new System.Drawing.Size(266, 29);
            this.tamdungKiemTraAvatarcheckBox.TabIndex = 29;
            this.tamdungKiemTraAvatarcheckBox.Text = "Tạm dừng Kiểm tra Avatar";
            this.tamdungKiemTraAvatarcheckBox.UseVisualStyleBackColor = true;
            // 
            // clearAccsettingsauregcheckBox
            // 
            this.clearAccsettingsauregcheckBox.AutoSize = true;
            this.clearAccsettingsauregcheckBox.Location = new System.Drawing.Point(82, 42);
            this.clearAccsettingsauregcheckBox.Name = "clearAccsettingsauregcheckBox";
            this.clearAccsettingsauregcheckBox.Size = new System.Drawing.Size(283, 29);
            this.clearAccsettingsauregcheckBox.TabIndex = 28;
            this.clearAccsettingsauregcheckBox.Text = "Clear acc setting sau khi reg";
            this.clearAccsettingsauregcheckBox.UseVisualStyleBackColor = true;
            // 
            // uploadContactNewCheckbox
            // 
            this.uploadContactNewCheckbox.AutoSize = true;
            this.uploadContactNewCheckbox.Checked = true;
            this.uploadContactNewCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uploadContactNewCheckbox.Location = new System.Drawing.Point(82, 13);
            this.uploadContactNewCheckbox.Name = "uploadContactNewCheckbox";
            this.uploadContactNewCheckbox.Size = new System.Drawing.Size(106, 29);
            this.uploadContactNewCheckbox.TabIndex = 27;
            this.uploadContactNewCheckbox.Text = "Contact";
            this.uploadContactNewCheckbox.UseVisualStyleBackColor = true;
            // 
            // coverNewcheckBox
            // 
            this.coverNewcheckBox.AutoSize = true;
            this.coverNewcheckBox.Location = new System.Drawing.Point(5, 15);
            this.coverNewcheckBox.Name = "coverNewcheckBox";
            this.coverNewcheckBox.Size = new System.Drawing.Size(91, 29);
            this.coverNewcheckBox.TabIndex = 26;
            this.coverNewcheckBox.Text = "Cover";
            this.coverNewcheckBox.UseVisualStyleBackColor = true;
            // 
            // changeAllSim2checkBox
            // 
            this.changeAllSim2checkBox.AutoSize = true;
            this.changeAllSim2checkBox.Location = new System.Drawing.Point(5, 65);
            this.changeAllSim2checkBox.Name = "changeAllSim2checkBox";
            this.changeAllSim2checkBox.Size = new System.Drawing.Size(190, 29);
            this.changeAllSim2checkBox.TabIndex = 24;
            this.changeAllSim2checkBox.Text = "Change All Sim 2";
            this.changeAllSim2checkBox.UseVisualStyleBackColor = true;
            // 
            // gmail48hradioButton
            // 
            this.gmail48hradioButton.AutoSize = true;
            this.gmail48hradioButton.Location = new System.Drawing.Point(1804, 756);
            this.gmail48hradioButton.Name = "gmail48hradioButton";
            this.gmail48hradioButton.Size = new System.Drawing.Size(120, 29);
            this.gmail48hradioButton.TabIndex = 273;
            this.gmail48hradioButton.Text = "Gmail48h";
            this.gmail48hradioButton.UseVisualStyleBackColor = true;
            this.gmail48hradioButton.CheckedChanged += new System.EventHandler(this.gmail48hradioButton_CheckedChanged);
            // 
            // gmailOtpRadioButton
            // 
            this.gmailOtpRadioButton.AutoSize = true;
            this.gmailOtpRadioButton.ForeColor = System.Drawing.Color.Red;
            this.gmailOtpRadioButton.Location = new System.Drawing.Point(1793, 776);
            this.gmailOtpRadioButton.Name = "gmailOtpRadioButton";
            this.gmailOtpRadioButton.Size = new System.Drawing.Size(119, 29);
            this.gmailOtpRadioButton.TabIndex = 271;
            this.gmailOtpRadioButton.Text = "GmailOtp";
            this.gmailOtpRadioButton.UseVisualStyleBackColor = true;
            this.gmailOtpRadioButton.CheckedChanged += new System.EventHandler(this.gmailOtpRadioButton_CheckedChanged);
            // 
            // sellGmailServerradioButton
            // 
            this.sellGmailServerradioButton.AutoSize = true;
            this.sellGmailServerradioButton.Location = new System.Drawing.Point(1892, 453);
            this.sellGmailServerradioButton.Name = "sellGmailServerradioButton";
            this.sellGmailServerradioButton.Size = new System.Drawing.Size(178, 29);
            this.sellGmailServerradioButton.TabIndex = 268;
            this.sellGmailServerradioButton.Text = "SellGmailServer";
            this.sellGmailServerradioButton.UseVisualStyleBackColor = true;
            this.sellGmailServerradioButton.Visible = false;
            // 
            // luuDuoiMailcheckBox
            // 
            this.luuDuoiMailcheckBox.AutoSize = true;
            this.luuDuoiMailcheckBox.Location = new System.Drawing.Point(1809, 740);
            this.luuDuoiMailcheckBox.Name = "luuDuoiMailcheckBox";
            this.luuDuoiMailcheckBox.Size = new System.Drawing.Size(153, 29);
            this.luuDuoiMailcheckBox.TabIndex = 263;
            this.luuDuoiMailcheckBox.Text = "Lưu đuôi mail";
            this.luuDuoiMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // getDuoiMailFromServercheckBox
            // 
            this.getDuoiMailFromServercheckBox.AutoSize = true;
            this.getDuoiMailFromServercheckBox.Location = new System.Drawing.Point(1805, 684);
            this.getDuoiMailFromServercheckBox.Name = "getDuoiMailFromServercheckBox";
            this.getDuoiMailFromServercheckBox.Size = new System.Drawing.Size(256, 29);
            this.getDuoiMailFromServercheckBox.TabIndex = 262;
            this.getDuoiMailFromServercheckBox.Text = "Get Đuôi mail from server";
            this.getDuoiMailFromServercheckBox.UseVisualStyleBackColor = true;
            // 
            // randomDuoiMailcheckBox
            // 
            this.randomDuoiMailcheckBox.AutoSize = true;
            this.randomDuoiMailcheckBox.Location = new System.Drawing.Point(1805, 711);
            this.randomDuoiMailcheckBox.Name = "randomDuoiMailcheckBox";
            this.randomDuoiMailcheckBox.Size = new System.Drawing.Size(196, 29);
            this.randomDuoiMailcheckBox.TabIndex = 261;
            this.randomDuoiMailcheckBox.Text = "Random Đuôi mail";
            this.randomDuoiMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // veriBackupCheckBox
            // 
            this.veriBackupCheckBox.AutoSize = true;
            this.veriBackupCheckBox.Location = new System.Drawing.Point(1800, 874);
            this.veriBackupCheckBox.Name = "veriBackupCheckBox";
            this.veriBackupCheckBox.Size = new System.Drawing.Size(142, 29);
            this.veriBackupCheckBox.TabIndex = 224;
            this.veriBackupCheckBox.Text = "Veri backup";
            this.veriBackupCheckBox.UseVisualStyleBackColor = true;
            this.veriBackupCheckBox.CheckedChanged += new System.EventHandler(this.veriBackupCheckBox_CheckedChanged);
            // 
            // runBoxLancheckBox
            // 
            this.runBoxLancheckBox.AutoSize = true;
            this.runBoxLancheckBox.Location = new System.Drawing.Point(1807, 555);
            this.runBoxLancheckBox.Name = "runBoxLancheckBox";
            this.runBoxLancheckBox.Size = new System.Drawing.Size(145, 29);
            this.runBoxLancheckBox.TabIndex = 311;
            this.runBoxLancheckBox.Text = "Run BoxLan";
            this.runBoxLancheckBox.UseVisualStyleBackColor = true;
            this.runBoxLancheckBox.CheckedChanged += new System.EventHandler(this.runBoxLancheckBox_CheckedChanged);
            // 
            // chayuploadContactcheckBox
            // 
            this.chayuploadContactcheckBox.AutoSize = true;
            this.chayuploadContactcheckBox.Location = new System.Drawing.Point(1872, 788);
            this.chayuploadContactcheckBox.Name = "chayuploadContactcheckBox";
            this.chayuploadContactcheckBox.Size = new System.Drawing.Size(180, 29);
            this.chayuploadContactcheckBox.TabIndex = 305;
            this.chayuploadContactcheckBox.Text = "Chạy upContact";
            this.chayuploadContactcheckBox.UseVisualStyleBackColor = true;
            // 
            // statusSpeedlabel
            // 
            this.statusSpeedlabel.AutoSize = true;
            this.statusSpeedlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusSpeedlabel.ForeColor = System.Drawing.Color.Red;
            this.statusSpeedlabel.Location = new System.Drawing.Point(501, 293);
            this.statusSpeedlabel.Name = "statusSpeedlabel";
            this.statusSpeedlabel.Size = new System.Drawing.Size(176, 59);
            this.statusSpeedlabel.TabIndex = 332;
            this.statusSpeedlabel.Text = "Speed";
            // 
            // alwaysChangeAirplaneCheckBox
            // 
            this.alwaysChangeAirplaneCheckBox.AutoSize = true;
            this.alwaysChangeAirplaneCheckBox.Location = new System.Drawing.Point(1832, 658);
            this.alwaysChangeAirplaneCheckBox.Name = "alwaysChangeAirplaneCheckBox";
            this.alwaysChangeAirplaneCheckBox.Size = new System.Drawing.Size(248, 29);
            this.alwaysChangeAirplaneCheckBox.TabIndex = 234;
            this.alwaysChangeAirplaneCheckBox.Text = "Always change Airplane";
            this.alwaysChangeAirplaneCheckBox.UseVisualStyleBackColor = true;
            // 
            // chuyenHotmailNhanhcheckBox
            // 
            this.chuyenHotmailNhanhcheckBox.AutoSize = true;
            this.chuyenHotmailNhanhcheckBox.Checked = true;
            this.chuyenHotmailNhanhcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chuyenHotmailNhanhcheckBox.Location = new System.Drawing.Point(1801, 476);
            this.chuyenHotmailNhanhcheckBox.Name = "chuyenHotmailNhanhcheckBox";
            this.chuyenHotmailNhanhcheckBox.Size = new System.Drawing.Size(234, 29);
            this.chuyenHotmailNhanhcheckBox.TabIndex = 277;
            this.chuyenHotmailNhanhcheckBox.Text = "Chuyển hotmail nhanh";
            this.chuyenHotmailNhanhcheckBox.UseVisualStyleBackColor = true;
            // 
            // otpVandongcheckBox
            // 
            this.otpVandongcheckBox.AutoSize = true;
            this.otpVandongcheckBox.Checked = true;
            this.otpVandongcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.otpVandongcheckBox.Location = new System.Drawing.Point(1799, 514);
            this.otpVandongcheckBox.Name = "otpVandongcheckBox";
            this.otpVandongcheckBox.Size = new System.Drawing.Size(161, 29);
            this.otpVandongcheckBox.TabIndex = 299;
            this.otpVandongcheckBox.Text = "OTP vandong";
            this.otpVandongcheckBox.UseVisualStyleBackColor = true;
            // 
            // getTrustMailcheckBox
            // 
            this.getTrustMailcheckBox.AutoSize = true;
            this.getTrustMailcheckBox.Location = new System.Drawing.Point(1799, 498);
            this.getTrustMailcheckBox.Name = "getTrustMailcheckBox";
            this.getTrustMailcheckBox.Size = new System.Drawing.Size(154, 29);
            this.getTrustMailcheckBox.TabIndex = 298;
            this.getTrustMailcheckBox.Text = "Get Trustmail";
            this.getTrustMailcheckBox.UseVisualStyleBackColor = true;
            // 
            // fastcheckBox
            // 
            this.fastcheckBox.AutoSize = true;
            this.fastcheckBox.Checked = true;
            this.fastcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fastcheckBox.Location = new System.Drawing.Point(1799, 531);
            this.fastcheckBox.Name = "fastcheckBox";
            this.fastcheckBox.Size = new System.Drawing.Size(76, 29);
            this.fastcheckBox.TabIndex = 296;
            this.fastcheckBox.Text = "Fast";
            this.fastcheckBox.UseVisualStyleBackColor = true;
            // 
            // rootRomcheckBox
            // 
            this.rootRomcheckBox.AutoSize = true;
            this.rootRomcheckBox.Location = new System.Drawing.Point(1742, 44);
            this.rootRomcheckBox.Name = "rootRomcheckBox";
            this.rootRomcheckBox.Size = new System.Drawing.Size(123, 29);
            this.rootRomcheckBox.TabIndex = 316;
            this.rootRomcheckBox.Text = "Root Rom";
            this.rootRomcheckBox.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(1758, 276);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 48);
            this.button3.TabIndex = 25;
            this.button3.Text = "Load Acc Moi";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(1543, 215);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(93, 23);
            this.button12.TabIndex = 294;
            this.button12.Text = "Kiểm Tra IP4/6";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // fixDuoiMailTextBox
            // 
            this.fixDuoiMailTextBox.Location = new System.Drawing.Point(1532, 175);
            this.fixDuoiMailTextBox.Name = "fixDuoiMailTextBox";
            this.fixDuoiMailTextBox.Size = new System.Drawing.Size(100, 29);
            this.fixDuoiMailTextBox.TabIndex = 257;
            this.fixDuoiMailTextBox.Text = "likere.ga";
            // 
            // chayepdanbacheckBox
            // 
            this.chayepdanbacheckBox.AutoSize = true;
            this.chayepdanbacheckBox.Location = new System.Drawing.Point(1630, 111);
            this.chayepdanbacheckBox.Name = "chayepdanbacheckBox";
            this.chayepdanbacheckBox.Size = new System.Drawing.Size(188, 29);
            this.chayepdanbacheckBox.TabIndex = 306;
            this.chayepdanbacheckBox.Text = "Chạy ép danh bạ";
            this.chayepdanbacheckBox.UseVisualStyleBackColor = true;
            // 
            // choPutOtpcheckBox
            // 
            this.choPutOtpcheckBox.AutoSize = true;
            this.choPutOtpcheckBox.Location = new System.Drawing.Point(1539, 299);
            this.choPutOtpcheckBox.Name = "choPutOtpcheckBox";
            this.choPutOtpcheckBox.Size = new System.Drawing.Size(139, 29);
            this.choPutOtpcheckBox.TabIndex = 297;
            this.choPutOtpcheckBox.Text = "Chờ put otp";
            this.choPutOtpcheckBox.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(167, 296);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(78, 25);
            this.label30.TabIndex = 297;
            this.label30.Text = "Mã thẻ:";
            // 
            // mathetextBox
            // 
            this.mathetextBox.Location = new System.Drawing.Point(213, 293);
            this.mathetextBox.Name = "mathetextBox";
            this.mathetextBox.Size = new System.Drawing.Size(170, 29);
            this.mathetextBox.TabIndex = 296;
            // 
            // accDieCapchalabel
            // 
            this.accDieCapchalabel.AutoSize = true;
            this.accDieCapchalabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accDieCapchalabel.ForeColor = System.Drawing.Color.Red;
            this.accDieCapchalabel.Location = new System.Drawing.Point(1844, -23);
            this.accDieCapchalabel.Name = "accDieCapchalabel";
            this.accDieCapchalabel.Size = new System.Drawing.Size(40, 44);
            this.accDieCapchalabel.TabIndex = 277;
            this.accDieCapchalabel.Text = "0";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(0, 460);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(414, 29);
            this.statusTextBox.TabIndex = 227;
            this.statusTextBox.Text = "Status";
            this.statusTextBox.Visible = false;
            this.statusTextBox.TextChanged += new System.EventHandler(this.statusTextBox_TextChanged);
            // 
            // randPhone2TypecheckBox
            // 
            this.randPhone2TypecheckBox.AutoSize = true;
            this.randPhone2TypecheckBox.Location = new System.Drawing.Point(1801, 755);
            this.randPhone2TypecheckBox.Name = "randPhone2TypecheckBox";
            this.randPhone2TypecheckBox.Size = new System.Drawing.Size(146, 29);
            this.randPhone2TypecheckBox.TabIndex = 244;
            this.randPhone2TypecheckBox.Text = "Rand Phone";
            this.randPhone2TypecheckBox.UseVisualStyleBackColor = true;
            // 
            // randomOldContactCheckBox
            // 
            this.randomOldContactCheckBox.AutoSize = true;
            this.randomOldContactCheckBox.Location = new System.Drawing.Point(1206, 299);
            this.randomOldContactCheckBox.Name = "randomOldContactCheckBox";
            this.randomOldContactCheckBox.Size = new System.Drawing.Size(215, 29);
            this.randomOldContactCheckBox.TabIndex = 247;
            this.randomOldContactCheckBox.Text = "Random Contact Cũ";
            this.randomOldContactCheckBox.UseVisualStyleBackColor = true;
            // 
            // setFastProxybutton
            // 
            this.setFastProxybutton.ForeColor = System.Drawing.Color.Red;
            this.setFastProxybutton.Location = new System.Drawing.Point(1804, 469);
            this.setFastProxybutton.Name = "setFastProxybutton";
            this.setFastProxybutton.Size = new System.Drawing.Size(123, 28);
            this.setFastProxybutton.TabIndex = 279;
            this.setFastProxybutton.Text = "Set Fastproxy";
            this.setFastProxybutton.UseVisualStyleBackColor = true;
            this.setFastProxybutton.Click += new System.EventHandler(this.setFastProxybutton_Click);
            // 
            // randomRegVericheckBox
            // 
            this.randomRegVericheckBox.AutoSize = true;
            this.randomRegVericheckBox.Location = new System.Drawing.Point(1651, 239);
            this.randomRegVericheckBox.Name = "randomRegVericheckBox";
            this.randomRegVericheckBox.Size = new System.Drawing.Size(165, 29);
            this.randomRegVericheckBox.TabIndex = 264;
            this.randomRegVericheckBox.Text = "Rand Reg/Veri";
            this.randomRegVericheckBox.UseVisualStyleBackColor = true;
            this.randomRegVericheckBox.CheckedChanged += new System.EventHandler(this.randomRegVericheckBox_CheckedChanged);
            // 
            // fixDuoiMailCheckBox
            // 
            this.fixDuoiMailCheckBox.AutoSize = true;
            this.fixDuoiMailCheckBox.Location = new System.Drawing.Point(1651, 217);
            this.fixDuoiMailCheckBox.Name = "fixDuoiMailCheckBox";
            this.fixDuoiMailCheckBox.Size = new System.Drawing.Size(150, 29);
            this.fixDuoiMailCheckBox.TabIndex = 256;
            this.fixDuoiMailCheckBox.Text = "Fix Đuôi Mail";
            this.fixDuoiMailCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFbByDeepLinkcheckBox1
            // 
            this.openFbByDeepLinkcheckBox1.AutoSize = true;
            this.openFbByDeepLinkcheckBox1.Location = new System.Drawing.Point(1663, 125);
            this.openFbByDeepLinkcheckBox1.Name = "openFbByDeepLinkcheckBox1";
            this.openFbByDeepLinkcheckBox1.Size = new System.Drawing.Size(184, 29);
            this.openFbByDeepLinkcheckBox1.TabIndex = 276;
            this.openFbByDeepLinkcheckBox1.Text = "Deeplink open fb";
            this.openFbByDeepLinkcheckBox1.UseVisualStyleBackColor = true;
            this.openFbByDeepLinkcheckBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // activeDuoiMailtextBox
            // 
            this.activeDuoiMailtextBox.ForeColor = System.Drawing.Color.Red;
            this.activeDuoiMailtextBox.Location = new System.Drawing.Point(1689, 199);
            this.activeDuoiMailtextBox.Name = "activeDuoiMailtextBox";
            this.activeDuoiMailtextBox.Size = new System.Drawing.Size(169, 29);
            this.activeDuoiMailtextBox.TabIndex = 263;
            this.activeDuoiMailtextBox.Text = "Active Đuôi Mail";
            // 
            // phoneTypeLabel
            // 
            this.phoneTypeLabel.AutoSize = true;
            this.phoneTypeLabel.ForeColor = System.Drawing.Color.Red;
            this.phoneTypeLabel.Location = new System.Drawing.Point(1595, 190);
            this.phoneTypeLabel.Name = "phoneTypeLabel";
            this.phoneTypeLabel.Size = new System.Drawing.Size(75, 25);
            this.phoneTypeLabel.TabIndex = 254;
            this.phoneTypeLabel.Text = "Số ĐT:";
            // 
            // fbInfoLabel
            // 
            this.fbInfoLabel.AutoSize = true;
            this.fbInfoLabel.Location = new System.Drawing.Point(1671, 148);
            this.fbInfoLabel.Name = "fbInfoLabel";
            this.fbInfoLabel.Size = new System.Drawing.Size(73, 25);
            this.fbInfoLabel.TabIndex = 64;
            this.fbInfoLabel.Text = "FB info";
            this.fbInfoLabel.Click += new System.EventHandler(this.label13_Click);
            // 
            // deviceFakerPlusCheckBox
            // 
            this.deviceFakerPlusCheckBox.AutoSize = true;
            this.deviceFakerPlusCheckBox.Location = new System.Drawing.Point(1532, 133);
            this.deviceFakerPlusCheckBox.Name = "deviceFakerPlusCheckBox";
            this.deviceFakerPlusCheckBox.Size = new System.Drawing.Size(131, 29);
            this.deviceFakerPlusCheckBox.TabIndex = 239;
            this.deviceFakerPlusCheckBox.Text = "Faker Plus";
            this.deviceFakerPlusCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.devicesTabPage);
            this.tabControl.Controls.Add(this.settingTabPage);
            this.tabControl.Location = new System.Drawing.Point(12, 311);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1783, 850);
            this.tabControl.TabIndex = 220;
            // 
            // devicesTabPage
            // 
            this.devicesTabPage.Controls.Add(this.allcheckBox);
            this.devicesTabPage.Controls.Add(this.checkAllcheckBox);
            this.devicesTabPage.Controls.Add(this.errortextBox);
            this.devicesTabPage.Controls.Add(this.dataGridView);
            this.devicesTabPage.Controls.Add(this.runningCheckBox);
            this.devicesTabPage.Controls.Add(this.statusTextBox);
            this.devicesTabPage.Controls.Add(this.accDieCapchalabel);
            this.devicesTabPage.Location = new System.Drawing.Point(4, 33);
            this.devicesTabPage.Name = "devicesTabPage";
            this.devicesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.devicesTabPage.Size = new System.Drawing.Size(1775, 813);
            this.devicesTabPage.TabIndex = 1;
            this.devicesTabPage.Text = "Devices";
            this.devicesTabPage.UseVisualStyleBackColor = true;
            // 
            // allcheckBox
            // 
            this.allcheckBox.AutoSize = true;
            this.allcheckBox.Location = new System.Drawing.Point(1652, 7);
            this.allcheckBox.Name = "allcheckBox";
            this.allcheckBox.Size = new System.Drawing.Size(60, 29);
            this.allcheckBox.TabIndex = 279;
            this.allcheckBox.Text = "All";
            this.allcheckBox.UseVisualStyleBackColor = true;
            this.allcheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_5);
            // 
            // checkAllcheckBox
            // 
            this.checkAllcheckBox.AutoSize = true;
            this.checkAllcheckBox.Checked = true;
            this.checkAllcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAllcheckBox.Location = new System.Drawing.Point(730, 16);
            this.checkAllcheckBox.Name = "checkAllcheckBox";
            this.checkAllcheckBox.Size = new System.Drawing.Size(60, 29);
            this.checkAllcheckBox.TabIndex = 278;
            this.checkAllcheckBox.Text = "All";
            this.checkAllcheckBox.UseVisualStyleBackColor = true;
            this.checkAllcheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_2);
            // 
            // errortextBox
            // 
            this.errortextBox.Location = new System.Drawing.Point(59, 954);
            this.errortextBox.Name = "errortextBox";
            this.errortextBox.Size = new System.Drawing.Size(1241, 29);
            this.errortextBox.TabIndex = 109;
            this.errortextBox.Text = "........................";
            // 
            // settingTabPage
            // 
            this.settingTabPage.Controls.Add(this.forceChangeWificheckBox);
            this.settingTabPage.Controls.Add(this.randomWificheckBox);
            this.settingTabPage.Controls.Add(this.loadWifiListbutton);
            this.settingTabPage.Controls.Add(this.wifiListtextBox);
            this.settingTabPage.Controls.Add(this.rootRomcheckBox);
            this.settingTabPage.Controls.Add(this.button15);
            this.settingTabPage.Controls.Add(this.button14);
            this.settingTabPage.Controls.Add(this.updateFbVersionbutton);
            this.settingTabPage.Controls.Add(this.boAccNhapMailSaicheckBox);
            this.settingTabPage.Controls.Add(this.EpAccMoicheckBox);
            this.settingTabPage.Controls.Add(this.GetRealPhonebutton);
            this.settingTabPage.Controls.Add(this.catProxySauVericheckBox);
            this.settingTabPage.Controls.Add(this.xmltextBox);
            this.settingTabPage.Controls.Add(this.waitAndTapbutton);
            this.settingTabPage.Controls.Add(this.testTaptextBox);
            this.settingTabPage.Controls.Add(this.Country);
            this.settingTabPage.Controls.Add(this.countrytextBox);
            this.settingTabPage.Controls.Add(this.InstallFbbutton);
            this.settingTabPage.Controls.Add(this.loadFBbutton);
            this.settingTabPage.Controls.Add(this.fbVersioncomboBox);
            this.settingTabPage.Controls.Add(this.uninstallbusinessbutton);
            this.settingTabPage.Controls.Add(this.UninstallMessenger);
            this.settingTabPage.Controls.Add(this.emailTypeGroupBox);
            this.settingTabPage.Controls.Add(this.carryCodecheckBox);
            this.settingTabPage.Controls.Add(this.clearContactButton);
            this.settingTabPage.Controls.Add(this.dauso12CheckBox);
            this.settingTabPage.Controls.Add(this.dauso12TextBox);
            this.settingTabPage.Controls.Add(this.proxyGroupBox);
            this.settingTabPage.Controls.Add(this.controlGroupBox);
            this.settingTabPage.Controls.Add(this.changeSimGroupBox);
            this.settingTabPage.Controls.Add(this.networkSimGroupBox);
            this.settingTabPage.Controls.Add(this.uninstallFbBtn);
            this.settingTabPage.Controls.Add(this.rmFbliteButton);
            this.settingTabPage.Controls.Add(this.button2);
            this.settingTabPage.Location = new System.Drawing.Point(4, 33);
            this.settingTabPage.Name = "settingTabPage";
            this.settingTabPage.Size = new System.Drawing.Size(1775, 813);
            this.settingTabPage.TabIndex = 2;
            this.settingTabPage.Text = "Setting";
            this.settingTabPage.UseVisualStyleBackColor = true;
            // 
            // forceChangeWificheckBox
            // 
            this.forceChangeWificheckBox.AutoSize = true;
            this.forceChangeWificheckBox.Location = new System.Drawing.Point(1631, 138);
            this.forceChangeWificheckBox.Name = "forceChangeWificheckBox";
            this.forceChangeWificheckBox.Size = new System.Drawing.Size(201, 29);
            this.forceChangeWificheckBox.TabIndex = 320;
            this.forceChangeWificheckBox.Text = "Force Change Wifi";
            this.forceChangeWificheckBox.UseVisualStyleBackColor = true;
            // 
            // randomWificheckBox
            // 
            this.randomWificheckBox.AutoSize = true;
            this.randomWificheckBox.Location = new System.Drawing.Point(1630, 117);
            this.randomWificheckBox.Name = "randomWificheckBox";
            this.randomWificheckBox.Size = new System.Drawing.Size(149, 29);
            this.randomWificheckBox.TabIndex = 319;
            this.randomWificheckBox.Text = "Random Wifi";
            this.randomWificheckBox.UseVisualStyleBackColor = true;
            // 
            // loadWifiListbutton
            // 
            this.loadWifiListbutton.Location = new System.Drawing.Point(1539, 114);
            this.loadWifiListbutton.Name = "loadWifiListbutton";
            this.loadWifiListbutton.Size = new System.Drawing.Size(75, 23);
            this.loadWifiListbutton.TabIndex = 318;
            this.loadWifiListbutton.Text = "Load Wifi";
            this.loadWifiListbutton.UseVisualStyleBackColor = true;
            this.loadWifiListbutton.Click += new System.EventHandler(this.loadWifiListbutton_Click);
            // 
            // wifiListtextBox
            // 
            this.wifiListtextBox.Location = new System.Drawing.Point(1528, 48);
            this.wifiListtextBox.Multiline = true;
            this.wifiListtextBox.Name = "wifiListtextBox";
            this.wifiListtextBox.Size = new System.Drawing.Size(208, 61);
            this.wifiListtextBox.TabIndex = 317;
            this.wifiListtextBox.Text = "Home_nha_5|12345678a\r\nUniBeu5G|12345678a";
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1374, 58);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(137, 23);
            this.button15.TabIndex = 311;
            this.button15.Text = "Change Device VN";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(1374, 29);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(137, 23);
            this.button14.TabIndex = 310;
            this.button14.Text = "Change Device US";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // updateFbVersionbutton
            // 
            this.updateFbVersionbutton.Location = new System.Drawing.Point(1394, 230);
            this.updateFbVersionbutton.Name = "updateFbVersionbutton";
            this.updateFbVersionbutton.Size = new System.Drawing.Size(150, 30);
            this.updateFbVersionbutton.TabIndex = 246;
            this.updateFbVersionbutton.Text = "Update Fb Version";
            this.updateFbVersionbutton.UseVisualStyleBackColor = true;
            this.updateFbVersionbutton.Click += new System.EventHandler(this.updateFbVersionbutton_Click);
            // 
            // boAccNhapMailSaicheckBox
            // 
            this.boAccNhapMailSaicheckBox.AutoSize = true;
            this.boAccNhapMailSaicheckBox.Checked = true;
            this.boAccNhapMailSaicheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.boAccNhapMailSaicheckBox.Location = new System.Drawing.Point(1397, 156);
            this.boAccNhapMailSaicheckBox.Name = "boAccNhapMailSaicheckBox";
            this.boAccNhapMailSaicheckBox.Size = new System.Drawing.Size(220, 29);
            this.boAccNhapMailSaicheckBox.TabIndex = 245;
            this.boAccNhapMailSaicheckBox.Text = "Bỏ Acc nhập mail sai";
            this.boAccNhapMailSaicheckBox.UseVisualStyleBackColor = true;
            // 
            // EpAccMoicheckBox
            // 
            this.EpAccMoicheckBox.AutoSize = true;
            this.EpAccMoicheckBox.Location = new System.Drawing.Point(1397, 130);
            this.EpAccMoicheckBox.Name = "EpAccMoicheckBox";
            this.EpAccMoicheckBox.Size = new System.Drawing.Size(134, 29);
            this.EpAccMoicheckBox.TabIndex = 244;
            this.EpAccMoicheckBox.Text = "Ép acc moi";
            this.EpAccMoicheckBox.UseVisualStyleBackColor = true;
            // 
            // GetRealPhonebutton
            // 
            this.GetRealPhonebutton.ForeColor = System.Drawing.Color.DarkRed;
            this.GetRealPhonebutton.Location = new System.Drawing.Point(1301, 716);
            this.GetRealPhonebutton.Name = "GetRealPhonebutton";
            this.GetRealPhonebutton.Size = new System.Drawing.Size(138, 27);
            this.GetRealPhonebutton.TabIndex = 243;
            this.GetRealPhonebutton.Text = "Get Real Phone";
            this.GetRealPhonebutton.UseVisualStyleBackColor = true;
            this.GetRealPhonebutton.Click += new System.EventHandler(this.GetRealPhonebutton_Click);
            // 
            // catProxySauVericheckBox
            // 
            this.catProxySauVericheckBox.AutoSize = true;
            this.catProxySauVericheckBox.Location = new System.Drawing.Point(1397, 107);
            this.catProxySauVericheckBox.Name = "catProxySauVericheckBox";
            this.catProxySauVericheckBox.Size = new System.Drawing.Size(195, 29);
            this.catProxySauVericheckBox.TabIndex = 242;
            this.catProxySauVericheckBox.Text = "Cắt proxy sau veri";
            this.catProxySauVericheckBox.UseVisualStyleBackColor = true;
            // 
            // xmltextBox
            // 
            this.xmltextBox.Location = new System.Drawing.Point(1349, 453);
            this.xmltextBox.Multiline = true;
            this.xmltextBox.Name = "xmltextBox";
            this.xmltextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xmltextBox.Size = new System.Drawing.Size(184, 210);
            this.xmltextBox.TabIndex = 241;
            // 
            // waitAndTapbutton
            // 
            this.waitAndTapbutton.Location = new System.Drawing.Point(1349, 395);
            this.waitAndTapbutton.Name = "waitAndTapbutton";
            this.waitAndTapbutton.Size = new System.Drawing.Size(171, 35);
            this.waitAndTapbutton.TabIndex = 240;
            this.waitAndTapbutton.Text = "WaitAndTap";
            this.waitAndTapbutton.UseVisualStyleBackColor = true;
            this.waitAndTapbutton.Click += new System.EventHandler(this.waitAndTapbutton_Click);
            // 
            // testTaptextBox
            // 
            this.testTaptextBox.Location = new System.Drawing.Point(1349, 356);
            this.testTaptextBox.Name = "testTaptextBox";
            this.testTaptextBox.Size = new System.Drawing.Size(168, 29);
            this.testTaptextBox.TabIndex = 239;
            // 
            // Country
            // 
            this.Country.AutoSize = true;
            this.Country.ForeColor = System.Drawing.Color.Red;
            this.Country.Location = new System.Drawing.Point(1051, 643);
            this.Country.Name = "Country";
            this.Country.Size = new System.Drawing.Size(87, 25);
            this.Country.TabIndex = 237;
            this.Country.Text = "Country:";
            // 
            // countrytextBox
            // 
            this.countrytextBox.Location = new System.Drawing.Point(1125, 638);
            this.countrytextBox.Name = "countrytextBox";
            this.countrytextBox.Size = new System.Drawing.Size(146, 29);
            this.countrytextBox.TabIndex = 236;
            // 
            // InstallFbbutton
            // 
            this.InstallFbbutton.Location = new System.Drawing.Point(1185, 230);
            this.InstallFbbutton.Name = "InstallFbbutton";
            this.InstallFbbutton.Size = new System.Drawing.Size(130, 30);
            this.InstallFbbutton.TabIndex = 235;
            this.InstallFbbutton.Text = "Install Fb";
            this.InstallFbbutton.UseVisualStyleBackColor = true;
            this.InstallFbbutton.Click += new System.EventHandler(this.InstallFbbutton_Click);
            // 
            // loadFBbutton
            // 
            this.loadFBbutton.Location = new System.Drawing.Point(1050, 230);
            this.loadFBbutton.Name = "loadFBbutton";
            this.loadFBbutton.Size = new System.Drawing.Size(100, 30);
            this.loadFBbutton.TabIndex = 234;
            this.loadFBbutton.Text = "Load FBVersion";
            this.loadFBbutton.UseVisualStyleBackColor = true;
            this.loadFBbutton.Click += new System.EventHandler(this.loadFBbutton_Click);
            // 
            // fbVersioncomboBox
            // 
            this.fbVersioncomboBox.FormattingEnabled = true;
            this.fbVersioncomboBox.Location = new System.Drawing.Point(1053, 190);
            this.fbVersioncomboBox.Name = "fbVersioncomboBox";
            this.fbVersioncomboBox.Size = new System.Drawing.Size(464, 32);
            this.fbVersioncomboBox.TabIndex = 233;
            this.fbVersioncomboBox.SelectedIndexChanged += new System.EventHandler(this.fbVersioncomboBox_SelectedIndexChanged);
            // 
            // uninstallbusinessbutton
            // 
            this.uninstallbusinessbutton.Location = new System.Drawing.Point(1071, 421);
            this.uninstallbusinessbutton.Name = "uninstallbusinessbutton";
            this.uninstallbusinessbutton.Size = new System.Drawing.Size(141, 26);
            this.uninstallbusinessbutton.TabIndex = 232;
            this.uninstallbusinessbutton.Text = "Uninstall Business";
            this.uninstallbusinessbutton.UseVisualStyleBackColor = true;
            this.uninstallbusinessbutton.Click += new System.EventHandler(this.uninstallbusinessbutton_Click);
            // 
            // UninstallMessenger
            // 
            this.UninstallMessenger.Location = new System.Drawing.Point(1071, 452);
            this.UninstallMessenger.Name = "UninstallMessenger";
            this.UninstallMessenger.Size = new System.Drawing.Size(140, 26);
            this.UninstallMessenger.TabIndex = 231;
            this.UninstallMessenger.Text = "Uninstall Messenger";
            this.UninstallMessenger.UseVisualStyleBackColor = true;
            this.UninstallMessenger.Click += new System.EventHandler(this.UninstallMessenger_Click);
            // 
            // emailTypeGroupBox
            // 
            this.emailTypeGroupBox.Controls.Add(this.outlookRadioButton);
            this.emailTypeGroupBox.Controls.Add(this.hotmailRadioButton);
            this.emailTypeGroupBox.Controls.Add(this.outlookDomainRadioButton);
            this.emailTypeGroupBox.Location = new System.Drawing.Point(1062, 511);
            this.emailTypeGroupBox.Name = "emailTypeGroupBox";
            this.emailTypeGroupBox.Size = new System.Drawing.Size(137, 100);
            this.emailTypeGroupBox.TabIndex = 230;
            this.emailTypeGroupBox.TabStop = false;
            this.emailTypeGroupBox.Text = "Email Type";
            // 
            // outlookRadioButton
            // 
            this.outlookRadioButton.AutoSize = true;
            this.outlookRadioButton.Location = new System.Drawing.Point(24, 73);
            this.outlookRadioButton.Name = "outlookRadioButton";
            this.outlookRadioButton.Size = new System.Drawing.Size(105, 29);
            this.outlookRadioButton.TabIndex = 2;
            this.outlookRadioButton.TabStop = true;
            this.outlookRadioButton.Text = "Outlook";
            this.outlookRadioButton.UseVisualStyleBackColor = true;
            // 
            // hotmailRadioButton
            // 
            this.hotmailRadioButton.AutoSize = true;
            this.hotmailRadioButton.Location = new System.Drawing.Point(24, 43);
            this.hotmailRadioButton.Name = "hotmailRadioButton";
            this.hotmailRadioButton.Size = new System.Drawing.Size(102, 29);
            this.hotmailRadioButton.TabIndex = 1;
            this.hotmailRadioButton.TabStop = true;
            this.hotmailRadioButton.Text = "Hotmail";
            this.hotmailRadioButton.UseVisualStyleBackColor = true;
            // 
            // outlookDomainRadioButton
            // 
            this.outlookDomainRadioButton.AutoSize = true;
            this.outlookDomainRadioButton.Checked = true;
            this.outlookDomainRadioButton.Location = new System.Drawing.Point(24, 16);
            this.outlookDomainRadioButton.Name = "outlookDomainRadioButton";
            this.outlookDomainRadioButton.Size = new System.Drawing.Size(177, 29);
            this.outlookDomainRadioButton.TabIndex = 0;
            this.outlookDomainRadioButton.TabStop = true;
            this.outlookDomainRadioButton.Text = "Outlook Domain";
            this.outlookDomainRadioButton.UseVisualStyleBackColor = true;
            // 
            // carryCodecheckBox
            // 
            this.carryCodecheckBox.AutoSize = true;
            this.carryCodecheckBox.Location = new System.Drawing.Point(1074, 346);
            this.carryCodecheckBox.Name = "carryCodecheckBox";
            this.carryCodecheckBox.Size = new System.Drawing.Size(201, 29);
            this.carryCodecheckBox.TabIndex = 229;
            this.carryCodecheckBox.Text = "Carry Code Phone";
            this.carryCodecheckBox.UseVisualStyleBackColor = true;
            // 
            // clearContactButton
            // 
            this.clearContactButton.Location = new System.Drawing.Point(1073, 479);
            this.clearContactButton.Name = "clearContactButton";
            this.clearContactButton.Size = new System.Drawing.Size(145, 26);
            this.clearContactButton.TabIndex = 228;
            this.clearContactButton.Text = "Clear Contact";
            this.clearContactButton.UseVisualStyleBackColor = true;
            this.clearContactButton.Click += new System.EventHandler(this.clearContactButton_Click);
            // 
            // dauso12CheckBox
            // 
            this.dauso12CheckBox.AutoSize = true;
            this.dauso12CheckBox.Location = new System.Drawing.Point(1073, 288);
            this.dauso12CheckBox.Name = "dauso12CheckBox";
            this.dauso12CheckBox.Size = new System.Drawing.Size(127, 29);
            this.dauso12CheckBox.TabIndex = 226;
            this.dauso12CheckBox.Text = "Đầu số 12";
            this.dauso12CheckBox.UseVisualStyleBackColor = true;
            // 
            // dauso12TextBox
            // 
            this.dauso12TextBox.Location = new System.Drawing.Point(1072, 311);
            this.dauso12TextBox.Name = "dauso12TextBox";
            this.dauso12TextBox.Size = new System.Drawing.Size(501, 29);
            this.dauso12TextBox.TabIndex = 225;
            this.dauso12TextBox.Text = "81701,81702,81703,81704,81705,81706,81707,81708,81709,81801,81901,81902,81903,819" +
    "04,81905,81906,81907,81908,81909";
            // 
            // proxyGroupBox
            // 
            this.proxyGroupBox.Controls.Add(this.proxyFromLocalcheckBox);
            this.proxyGroupBox.Controls.Add(this.wwProxyradioButton);
            this.proxyGroupBox.Controls.Add(this.tunProxyradioButton);
            this.proxyGroupBox.Controls.Add(this.s5ProxyradioButton);
            this.proxyGroupBox.Controls.Add(this.zuesProxy4G);
            this.proxyGroupBox.Controls.Add(this.impulseradioButton);
            this.proxyGroupBox.Controls.Add(this.zuesProxyradioButton);
            this.proxyGroupBox.Controls.Add(this.FastProxyTextBox);
            this.proxyGroupBox.Controls.Add(this.fastProxyRadioButton);
            this.proxyGroupBox.Controls.Add(this.dtProxyTextBox);
            this.proxyGroupBox.Controls.Add(this.dtProxyRadioButton);
            this.proxyGroupBox.Controls.Add(this.tmProxyTextBox);
            this.proxyGroupBox.Controls.Add(this.tmProxyRadioButton);
            this.proxyGroupBox.Controls.Add(this.allowIpTextBox);
            this.proxyGroupBox.Controls.Add(this.tinProxyTextBox);
            this.proxyGroupBox.Controls.Add(this.tinProxyRadioButton);
            this.proxyGroupBox.Controls.Add(this.removeProxyButton);
            this.proxyGroupBox.Controls.Add(this.tinsoftTextBox);
            this.proxyGroupBox.Controls.Add(this.shoplikeTextBox1);
            this.proxyGroupBox.Controls.Add(this.tinsoftRadioButton);
            this.proxyGroupBox.Controls.Add(this.shopLike1RadioButton);
            this.proxyGroupBox.Controls.Add(this.loadTinsoftButton);
            this.proxyGroupBox.Location = new System.Drawing.Point(767, 11);
            this.proxyGroupBox.Name = "proxyGroupBox";
            this.proxyGroupBox.Size = new System.Drawing.Size(286, 836);
            this.proxyGroupBox.TabIndex = 224;
            this.proxyGroupBox.TabStop = false;
            this.proxyGroupBox.Text = "Proxy";
            // 
            // proxyFromLocalcheckBox
            // 
            this.proxyFromLocalcheckBox.AutoSize = true;
            this.proxyFromLocalcheckBox.Location = new System.Drawing.Point(124, 173);
            this.proxyFromLocalcheckBox.Name = "proxyFromLocalcheckBox";
            this.proxyFromLocalcheckBox.Size = new System.Drawing.Size(176, 29);
            this.proxyFromLocalcheckBox.TabIndex = 176;
            this.proxyFromLocalcheckBox.Text = "Proxy from local";
            this.proxyFromLocalcheckBox.UseVisualStyleBackColor = true;
            this.proxyFromLocalcheckBox.CheckedChanged += new System.EventHandler(this.proxyFromLocalcheckBox_CheckedChanged);
            // 
            // wwProxyradioButton
            // 
            this.wwProxyradioButton.AutoSize = true;
            this.wwProxyradioButton.Checked = true;
            this.wwProxyradioButton.Location = new System.Drawing.Point(20, 184);
            this.wwProxyradioButton.Name = "wwProxyradioButton";
            this.wwProxyradioButton.Size = new System.Drawing.Size(125, 29);
            this.wwProxyradioButton.TabIndex = 175;
            this.wwProxyradioButton.TabStop = true;
            this.wwProxyradioButton.Text = "WWproxy";
            this.wwProxyradioButton.UseVisualStyleBackColor = true;
            // 
            // tunProxyradioButton
            // 
            this.tunProxyradioButton.AutoSize = true;
            this.tunProxyradioButton.Location = new System.Drawing.Point(129, 252);
            this.tunProxyradioButton.Name = "tunProxyradioButton";
            this.tunProxyradioButton.Size = new System.Drawing.Size(122, 29);
            this.tunProxyradioButton.TabIndex = 174;
            this.tunProxyradioButton.Text = "TunProxy";
            this.tunProxyradioButton.UseVisualStyleBackColor = true;
            // 
            // s5ProxyradioButton
            // 
            this.s5ProxyradioButton.AutoSize = true;
            this.s5ProxyradioButton.Location = new System.Drawing.Point(186, 234);
            this.s5ProxyradioButton.Name = "s5ProxyradioButton";
            this.s5ProxyradioButton.Size = new System.Drawing.Size(112, 29);
            this.s5ProxyradioButton.TabIndex = 172;
            this.s5ProxyradioButton.Text = "S5Proxy";
            this.s5ProxyradioButton.UseVisualStyleBackColor = true;
            this.s5ProxyradioButton.CheckedChanged += new System.EventHandler(this.s5ProxyradioButton_CheckedChanged);
            // 
            // zuesProxy4G
            // 
            this.zuesProxy4G.AutoSize = true;
            this.zuesProxy4G.Location = new System.Drawing.Point(19, 258);
            this.zuesProxy4G.Name = "zuesProxy4G";
            this.zuesProxy4G.Size = new System.Drawing.Size(157, 29);
            this.zuesProxy4G.TabIndex = 171;
            this.zuesProxy4G.Text = "ZeusProxy4G";
            this.zuesProxy4G.UseVisualStyleBackColor = true;
            // 
            // impulseradioButton
            // 
            this.impulseradioButton.AutoSize = true;
            this.impulseradioButton.Location = new System.Drawing.Point(185, 212);
            this.impulseradioButton.Name = "impulseradioButton";
            this.impulseradioButton.Size = new System.Drawing.Size(105, 29);
            this.impulseradioButton.TabIndex = 170;
            this.impulseradioButton.Text = "Impulse";
            this.impulseradioButton.UseVisualStyleBackColor = true;
            this.impulseradioButton.CheckedChanged += new System.EventHandler(this.impulseradioButton_CheckedChanged);
            // 
            // zuesProxyradioButton
            // 
            this.zuesProxyradioButton.AutoSize = true;
            this.zuesProxyradioButton.Location = new System.Drawing.Point(18, 232);
            this.zuesProxyradioButton.Name = "zuesProxyradioButton";
            this.zuesProxyradioButton.Size = new System.Drawing.Size(198, 29);
            this.zuesProxyradioButton.TabIndex = 169;
            this.zuesProxyradioButton.Text = "ZuesProxyByPass";
            this.zuesProxyradioButton.UseVisualStyleBackColor = true;
            this.zuesProxyradioButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // FastProxyTextBox
            // 
            this.FastProxyTextBox.Location = new System.Drawing.Point(19, 286);
            this.FastProxyTextBox.Multiline = true;
            this.FastProxyTextBox.Name = "FastProxyTextBox";
            this.FastProxyTextBox.Size = new System.Drawing.Size(260, 81);
            this.FastProxyTextBox.TabIndex = 168;
            this.FastProxyTextBox.TextChanged += new System.EventHandler(this.FastProxyTextBox_TextChanged);
            // 
            // fastProxyRadioButton
            // 
            this.fastProxyRadioButton.AutoSize = true;
            this.fastProxyRadioButton.Location = new System.Drawing.Point(18, 209);
            this.fastProxyRadioButton.Name = "fastProxyRadioButton";
            this.fastProxyRadioButton.Size = new System.Drawing.Size(123, 29);
            this.fastProxyRadioButton.TabIndex = 167;
            this.fastProxyRadioButton.Text = "Fastproxy";
            this.fastProxyRadioButton.UseVisualStyleBackColor = true;
            this.fastProxyRadioButton.CheckedChanged += new System.EventHandler(this.fastProxyRadioButton_CheckedChanged);
            // 
            // dtProxyTextBox
            // 
            this.dtProxyTextBox.Location = new System.Drawing.Point(20, 705);
            this.dtProxyTextBox.Multiline = true;
            this.dtProxyTextBox.Name = "dtProxyTextBox";
            this.dtProxyTextBox.Size = new System.Drawing.Size(260, 115);
            this.dtProxyTextBox.TabIndex = 166;
            this.dtProxyTextBox.TextChanged += new System.EventHandler(this.dtProxyTextBox_TextChanged);
            // 
            // dtProxyRadioButton
            // 
            this.dtProxyRadioButton.AutoSize = true;
            this.dtProxyRadioButton.Location = new System.Drawing.Point(18, 677);
            this.dtProxyRadioButton.Name = "dtProxyRadioButton";
            this.dtProxyRadioButton.Size = new System.Drawing.Size(109, 29);
            this.dtProxyRadioButton.TabIndex = 165;
            this.dtProxyRadioButton.Text = "Dt proxy";
            this.dtProxyRadioButton.UseVisualStyleBackColor = true;
            this.dtProxyRadioButton.CheckedChanged += new System.EventHandler(this.dtProxyRadioButton_CheckedChanged);
            // 
            // tmProxyTextBox
            // 
            this.tmProxyTextBox.Location = new System.Drawing.Point(20, 547);
            this.tmProxyTextBox.Multiline = true;
            this.tmProxyTextBox.Name = "tmProxyTextBox";
            this.tmProxyTextBox.Size = new System.Drawing.Size(260, 118);
            this.tmProxyTextBox.TabIndex = 164;
            this.tmProxyTextBox.TextChanged += new System.EventHandler(this.tmProxyTextBox_TextChanged);
            // 
            // tmProxyRadioButton
            // 
            this.tmProxyRadioButton.AutoSize = true;
            this.tmProxyRadioButton.Location = new System.Drawing.Point(20, 524);
            this.tmProxyRadioButton.Name = "tmProxyRadioButton";
            this.tmProxyRadioButton.Size = new System.Drawing.Size(114, 29);
            this.tmProxyRadioButton.TabIndex = 163;
            this.tmProxyRadioButton.Text = "Tmproxy";
            this.tmProxyRadioButton.UseVisualStyleBackColor = true;
            // 
            // allowIpTextBox
            // 
            this.allowIpTextBox.Location = new System.Drawing.Point(18, 430);
            this.allowIpTextBox.Name = "allowIpTextBox";
            this.allowIpTextBox.Size = new System.Drawing.Size(243, 29);
            this.allowIpTextBox.TabIndex = 162;
            this.allowIpTextBox.Text = "113.22.107.142";
            this.allowIpTextBox.Visible = false;
            // 
            // tinProxyTextBox
            // 
            this.tinProxyTextBox.Location = new System.Drawing.Point(20, 442);
            this.tinProxyTextBox.Multiline = true;
            this.tinProxyTextBox.Name = "tinProxyTextBox";
            this.tinProxyTextBox.Size = new System.Drawing.Size(261, 87);
            this.tinProxyTextBox.TabIndex = 161;
            this.tinProxyTextBox.TextChanged += new System.EventHandler(this.tinProxyTextBox_TextChanged);
            // 
            // tinProxyRadioButton
            // 
            this.tinProxyRadioButton.AutoSize = true;
            this.tinProxyRadioButton.Location = new System.Drawing.Point(18, 405);
            this.tinProxyRadioButton.Name = "tinProxyRadioButton";
            this.tinProxyRadioButton.Size = new System.Drawing.Size(115, 29);
            this.tinProxyRadioButton.TabIndex = 160;
            this.tinProxyRadioButton.Text = "TinProxy";
            this.tinProxyRadioButton.UseVisualStyleBackColor = true;
            // 
            // removeProxyButton
            // 
            this.removeProxyButton.Location = new System.Drawing.Point(39, 373);
            this.removeProxyButton.Name = "removeProxyButton";
            this.removeProxyButton.Size = new System.Drawing.Size(87, 30);
            this.removeProxyButton.TabIndex = 159;
            this.removeProxyButton.Text = "Remove Proxy";
            this.removeProxyButton.UseVisualStyleBackColor = true;
            this.removeProxyButton.Visible = false;
            this.removeProxyButton.Click += new System.EventHandler(this.removeProxyButton_Click);
            // 
            // tinsoftTextBox
            // 
            this.tinsoftTextBox.Location = new System.Drawing.Point(19, 109);
            this.tinsoftTextBox.Multiline = true;
            this.tinsoftTextBox.Name = "tinsoftTextBox";
            this.tinsoftTextBox.Size = new System.Drawing.Size(261, 53);
            this.tinsoftTextBox.TabIndex = 158;
            this.tinsoftTextBox.TextChanged += new System.EventHandler(this.tinsoftTextBox_TextChanged);
            // 
            // tinsoftRadioButton
            // 
            this.tinsoftRadioButton.AutoSize = true;
            this.tinsoftRadioButton.Location = new System.Drawing.Point(19, 81);
            this.tinsoftRadioButton.Name = "tinsoftRadioButton";
            this.tinsoftRadioButton.Size = new System.Drawing.Size(96, 29);
            this.tinsoftRadioButton.TabIndex = 1;
            this.tinsoftRadioButton.Text = "Tinsoft";
            this.tinsoftRadioButton.UseVisualStyleBackColor = true;
            this.tinsoftRadioButton.CheckedChanged += new System.EventHandler(this.tinsoftRadioButton_CheckedChanged);
            // 
            // shopLike1RadioButton
            // 
            this.shopLike1RadioButton.AutoSize = true;
            this.shopLike1RadioButton.Location = new System.Drawing.Point(19, 20);
            this.shopLike1RadioButton.Name = "shopLike1RadioButton";
            this.shopLike1RadioButton.Size = new System.Drawing.Size(113, 29);
            this.shopLike1RadioButton.TabIndex = 0;
            this.shopLike1RadioButton.Text = "Shoplike";
            this.shopLike1RadioButton.UseVisualStyleBackColor = true;
            // 
            // controlGroupBox
            // 
            this.controlGroupBox.Controls.Add(this.veriDirectGmailcheckBox);
            this.controlGroupBox.Controls.Add(this.reinstallBusinesscheckBox);
            this.controlGroupBox.Controls.Add(this.randomContrybutton);
            this.controlGroupBox.Controls.Add(this.zuesProxyKeytextBox);
            this.controlGroupBox.Controls.Add(this.label29);
            this.controlGroupBox.Controls.Add(this.button6);
            this.controlGroupBox.Controls.Add(this.label22);
            this.controlGroupBox.Controls.Add(this.label21);
            this.controlGroupBox.Controls.Add(this.numberClearAccSettingTextBox);
            this.controlGroupBox.Controls.Add(this.dausoCheckBox);
            this.controlGroupBox.Controls.Add(this.label13);
            this.controlGroupBox.Controls.Add(this.reinstallFbCountTextBox);
            this.controlGroupBox.Controls.Add(this.reinstallFbCheckBox);
            this.controlGroupBox.Controls.Add(this.reinstallFbLiteCheckBox);
            this.controlGroupBox.Controls.Add(this.reinstallFbliteTextbox);
            this.controlGroupBox.Controls.Add(this.drkDomainTextbox);
            this.controlGroupBox.Controls.Add(this.numberOfFriendRequestTextBox);
            this.controlGroupBox.Controls.Add(this.label12);
            this.controlGroupBox.Controls.Add(this.label11);
            this.controlGroupBox.Controls.Add(this.drkCheckBox);
            this.controlGroupBox.Controls.Add(this.drkKeyTextBox);
            this.controlGroupBox.Controls.Add(this.descriptionCheckBox);
            this.controlGroupBox.Controls.Add(this.forceIp6checkBox);
            this.controlGroupBox.Controls.Add(this.forceIp4CheckBox);
            this.controlGroupBox.Controls.Add(this.veriMailAfterPhonecheckBox);
            this.controlGroupBox.Controls.Add(this.phoneInQueuelabel);
            this.controlGroupBox.Controls.Add(this.prefixTextNowCheckBox);
            this.controlGroupBox.Controls.Add(this.getPhoneCodeTextNowbutton);
            this.controlGroupBox.Controls.Add(this.reportPhoneLabel);
            this.controlGroupBox.Controls.Add(this.otpKeyTextBox);
            this.controlGroupBox.Controls.Add(this.turnOnEmubutton);
            this.controlGroupBox.Controls.Add(this.cookieCodeTextNowtextBox);
            this.controlGroupBox.Controls.Add(this.accMoiCheckBox);
            this.controlGroupBox.Controls.Add(this.clearCacheFBcheckBox);
            this.controlGroupBox.Controls.Add(this.codeKeyTextNowTextBox);
            this.controlGroupBox.Controls.Add(this.executeAdbButton);
            this.controlGroupBox.Controls.Add(this.installfblitecheckBox);
            this.controlGroupBox.Controls.Add(this.micerCheckBox);
            this.controlGroupBox.Controls.Add(this.vietSimButton);
            this.controlGroupBox.Controls.Add(this.changeSimUsButton);
            this.controlGroupBox.Controls.Add(this.americaPhoneCheckBox);
            this.controlGroupBox.Controls.Add(this.veriDirectByPhoneCheckBox);
            this.controlGroupBox.Controls.Add(this.addStatusCheckBox);
            this.controlGroupBox.Controls.Add(this.loginByUserPassCheckBox);
            this.controlGroupBox.Controls.Add(this.noveriCoverCheckBox);
            this.controlGroupBox.Controls.Add(this.coverCheckBox);
            this.controlGroupBox.Controls.Add(this.checkLoginCheckBox);
            this.controlGroupBox.Controls.Add(this.textnowCheckbox);
            this.controlGroupBox.Controls.Add(this.nvrUpAvatarCheckBox);
            this.controlGroupBox.Controls.Add(this.veriPhoneCheckBox);
            this.controlGroupBox.Controls.Add(this.veriContactCheckBox);
            this.controlGroupBox.Controls.Add(this.turnOffEmuButton);
            this.controlGroupBox.Controls.Add(this.adbKeyCheckBox);
            this.controlGroupBox.Controls.Add(this.veriHotmailCheckBox);
            this.controlGroupBox.Controls.Add(this.vietUsCheckBox);
            this.controlGroupBox.Controls.Add(this.randomVeriCheckBox);
            this.controlGroupBox.Controls.Add(this.randomAllSimCheckBox);
            this.controlGroupBox.Controls.Add(this.changeSimType2CheckBox);
            this.controlGroupBox.Controls.Add(this.randomPhoneCheckBox);
            this.controlGroupBox.Controls.Add(this.turnoffSimButton);
            this.controlGroupBox.Controls.Add(this.autoVeriMailCheckBox);
            this.controlGroupBox.Controls.Add(this.speedlabel);
            this.controlGroupBox.Controls.Add(this.autoSpeedCheckBox);
            this.controlGroupBox.Controls.Add(this.label10);
            this.controlGroupBox.Controls.Add(this.minSpeedTextBox);
            this.controlGroupBox.Controls.Add(this.installMissingFBbutton);
            this.controlGroupBox.Controls.Add(this.checkFBInstalledBtn);
            this.controlGroupBox.Controls.Add(this.homeCheckBox);
            this.controlGroupBox.Controls.Add(this.autoRunVeriCheckBox);
            this.controlGroupBox.Controls.Add(this.randomMailPhoneSimCheckBox);
            this.controlGroupBox.Controls.Add(this.unsignCheckBox);
            this.controlGroupBox.Controls.Add(this.forgotCheckBox);
            this.controlGroupBox.Controls.Add(this.clearFbLiteCheckBox);
            this.controlGroupBox.Controls.Add(this.changeSimCheckBox);
            this.controlGroupBox.Controls.Add(this.clearCacheCheckBox);
            this.controlGroupBox.Controls.Add(this.turnOnSimButton);
            this.controlGroupBox.Controls.Add(this.miniProfileCheckBox);
            this.controlGroupBox.Controls.Add(this.mailSuffixtextBox);
            this.controlGroupBox.Controls.Add(this.airplaneEnableCheckBox);
            this.controlGroupBox.Controls.Add(this.profileCheckBox);
            this.controlGroupBox.Controls.Add(this.addFriendCheckBox);
            this.controlGroupBox.Controls.Add(this.androidIDCheckBox);
            this.controlGroupBox.Controls.Add(this.button7);
            this.controlGroupBox.Controls.Add(this.rootAdbButton);
            this.controlGroupBox.Controls.Add(this.vietCheckbox);
            this.controlGroupBox.Controls.Add(this.Block);
            this.controlGroupBox.Controls.Add(this.timebreakDeadLocktextBox);
            this.controlGroupBox.Controls.Add(this.maxAccBlockRuntextBox);
            this.controlGroupBox.Controls.Add(this.usPhoneCheckBox);
            this.controlGroupBox.Controls.Add(this.setTimeZoneButton);
            this.controlGroupBox.Controls.Add(this.timeZoneComboBox);
            this.controlGroupBox.Controls.Add(this.rebootAllbutton);
            this.controlGroupBox.Controls.Add(this.button5);
            this.controlGroupBox.Controls.Add(this.button4);
            this.controlGroupBox.Controls.Add(this.button1);
            this.controlGroupBox.Controls.Add(this.setWifiButton);
            this.controlGroupBox.Controls.Add(this.wifiPassTextBox);
            this.controlGroupBox.Controls.Add(this.label9);
            this.controlGroupBox.Controls.Add(this.ssidTextBox);
            this.controlGroupBox.Controls.Add(this.label6);
            this.controlGroupBox.Controls.Add(this.changeDeviceEmuCheckBox);
            this.controlGroupBox.Controls.Add(this.installApk);
            this.controlGroupBox.Controls.Add(this.usePhoneLocalCheckBox);
            this.controlGroupBox.Controls.Add(this.proxyCheckBox);
            this.controlGroupBox.Controls.Add(this.airplaneCheckBox);
            this.controlGroupBox.Controls.Add(this.codeLabel);
            this.controlGroupBox.Controls.Add(this.getCodeButton);
            this.controlGroupBox.Controls.Add(this.mailTextbox);
            this.controlGroupBox.Controls.Add(this.label8);
            this.controlGroupBox.Controls.Add(this.timeBreakTextBox);
            this.controlGroupBox.Controls.Add(this.label3);
            this.controlGroupBox.Controls.Add(this.maxAccContinueTextBox);
            this.controlGroupBox.Controls.Add(this.regByMailCheckBox);
            this.controlGroupBox.Controls.Add(this.inputStringCheckbox);
            this.controlGroupBox.Controls.Add(this.fbLiteCheckbox);
            this.controlGroupBox.Controls.Add(this.label7);
            this.controlGroupBox.Controls.Add(this.delayTextbox);
            this.controlGroupBox.Controls.Add(this.noSuggestCheckbox);
            this.controlGroupBox.Controls.Add(this.randomPrePhoneCheckbox);
            this.controlGroupBox.Controls.Add(this.downloadAvatarBtn);
            this.controlGroupBox.Controls.Add(this.yearOldTo);
            this.controlGroupBox.Controls.Add(this.yearOldFrom);
            this.controlGroupBox.Controls.Add(this.label5);
            this.controlGroupBox.Controls.Add(this.resetStatus);
            this.controlGroupBox.Controls.Add(this.dausotextbox);
            this.controlGroupBox.Controls.Add(this.label4);
            this.controlGroupBox.Controls.Add(this.femaleCheckbox);
            this.controlGroupBox.Controls.Add(this.maleCheckbox);
            this.controlGroupBox.Controls.Add(this.status);
            this.controlGroupBox.Controls.Add(this.label1);
            this.controlGroupBox.Controls.Add(this.reportLabel);
            this.controlGroupBox.Controls.Add(this.label2);
            this.controlGroupBox.Controls.Add(this.nvrByDeviceCheckBox);
            this.controlGroupBox.Location = new System.Drawing.Point(7, 7);
            this.controlGroupBox.Name = "controlGroupBox";
            this.controlGroupBox.Size = new System.Drawing.Size(740, 840);
            this.controlGroupBox.TabIndex = 223;
            this.controlGroupBox.TabStop = false;
            this.controlGroupBox.Text = "Control";
            // 
            // veriDirectGmailcheckBox
            // 
            this.veriDirectGmailcheckBox.AutoSize = true;
            this.veriDirectGmailcheckBox.Location = new System.Drawing.Point(11, 121);
            this.veriDirectGmailcheckBox.Name = "veriDirectGmailcheckBox";
            this.veriDirectGmailcheckBox.Size = new System.Drawing.Size(183, 29);
            this.veriDirectGmailcheckBox.TabIndex = 261;
            this.veriDirectGmailcheckBox.Text = "Veri Direct Gmail";
            this.veriDirectGmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // reinstallBusinesscheckBox
            // 
            this.reinstallBusinesscheckBox.AutoSize = true;
            this.reinstallBusinesscheckBox.Location = new System.Drawing.Point(99, 63);
            this.reinstallBusinesscheckBox.Name = "reinstallBusinesscheckBox";
            this.reinstallBusinesscheckBox.Size = new System.Drawing.Size(196, 29);
            this.reinstallBusinesscheckBox.TabIndex = 235;
            this.reinstallBusinesscheckBox.Text = "Reinstall Business";
            this.reinstallBusinesscheckBox.UseVisualStyleBackColor = true;
            // 
            // randomContrybutton
            // 
            this.randomContrybutton.Location = new System.Drawing.Point(608, 556);
            this.randomContrybutton.Name = "randomContrybutton";
            this.randomContrybutton.Size = new System.Drawing.Size(141, 29);
            this.randomContrybutton.TabIndex = 234;
            this.randomContrybutton.Text = "Random Contry";
            this.randomContrybutton.UseVisualStyleBackColor = true;
            this.randomContrybutton.Click += new System.EventHandler(this.randomContrybutton_Click);
            // 
            // zuesProxyKeytextBox
            // 
            this.zuesProxyKeytextBox.ForeColor = System.Drawing.Color.Red;
            this.zuesProxyKeytextBox.Location = new System.Drawing.Point(233, 683);
            this.zuesProxyKeytextBox.Name = "zuesProxyKeytextBox";
            this.zuesProxyKeytextBox.Size = new System.Drawing.Size(301, 29);
            this.zuesProxyKeytextBox.TabIndex = 233;
            this.zuesProxyKeytextBox.Text = "01e0tvxzttikxnscsey3x4uu4hw1ckyz";
            this.zuesProxyKeytextBox.TextChanged += new System.EventHandler(this.zuesProxyKeytextBox_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(115, 686);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(146, 25);
            this.label29.TabIndex = 232;
            this.label29.Text = "ZuesProxy Key";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(570, 682);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(139, 54);
            this.button6.TabIndex = 231;
            this.button6.Text = "Turn on sim subcribe";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.buttonTurnOnSimSubcriber_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(147, 382);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(0, 25);
            this.label22.TabIndex = 230;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Green;
            this.label21.Location = new System.Drawing.Point(175, 372);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(213, 25);
            this.label21.TabIndex = 229;
            this.label21.Text = "Time Clear Acc Setting";
            // 
            // numberClearAccSettingTextBox
            // 
            this.numberClearAccSettingTextBox.Location = new System.Drawing.Point(126, 370);
            this.numberClearAccSettingTextBox.Name = "numberClearAccSettingTextBox";
            this.numberClearAccSettingTextBox.Size = new System.Drawing.Size(46, 29);
            this.numberClearAccSettingTextBox.TabIndex = 228;
            this.numberClearAccSettingTextBox.Text = "3";
            // 
            // dausoCheckBox
            // 
            this.dausoCheckBox.AutoSize = true;
            this.dausoCheckBox.Location = new System.Drawing.Point(7, 712);
            this.dausoCheckBox.Name = "dausoCheckBox";
            this.dausoCheckBox.Size = new System.Drawing.Size(100, 29);
            this.dausoCheckBox.TabIndex = 220;
            this.dausoCheckBox.Text = "Đầu số";
            this.dausoCheckBox.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(619, 262);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 25);
            this.label13.TabIndex = 219;
            this.label13.Text = "Friend";
            // 
            // reinstallFbCountTextBox
            // 
            this.reinstallFbCountTextBox.Location = new System.Drawing.Point(196, 37);
            this.reinstallFbCountTextBox.Name = "reinstallFbCountTextBox";
            this.reinstallFbCountTextBox.Size = new System.Drawing.Size(46, 29);
            this.reinstallFbCountTextBox.TabIndex = 218;
            this.reinstallFbCountTextBox.Text = "5";
            // 
            // reinstallFbCheckBox
            // 
            this.reinstallFbCheckBox.AutoSize = true;
            this.reinstallFbCheckBox.Checked = true;
            this.reinstallFbCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reinstallFbCheckBox.Location = new System.Drawing.Point(198, 16);
            this.reinstallFbCheckBox.Name = "reinstallFbCheckBox";
            this.reinstallFbCheckBox.Size = new System.Drawing.Size(132, 29);
            this.reinstallFbCheckBox.TabIndex = 217;
            this.reinstallFbCheckBox.Text = "Reinstall fb";
            this.reinstallFbCheckBox.UseVisualStyleBackColor = true;
            // 
            // reinstallFbLiteCheckBox
            // 
            this.reinstallFbLiteCheckBox.AutoSize = true;
            this.reinstallFbLiteCheckBox.Location = new System.Drawing.Point(100, 15);
            this.reinstallFbLiteCheckBox.Name = "reinstallFbLiteCheckBox";
            this.reinstallFbLiteCheckBox.Size = new System.Drawing.Size(156, 29);
            this.reinstallFbLiteCheckBox.TabIndex = 216;
            this.reinstallFbLiteCheckBox.Text = "Reinstall fblite";
            this.reinstallFbLiteCheckBox.UseVisualStyleBackColor = true;
            this.reinstallFbLiteCheckBox.CheckedChanged += new System.EventHandler(this.reinstallFbLiteCheckBox_CheckedChanged);
            // 
            // reinstallFbliteTextbox
            // 
            this.reinstallFbliteTextbox.Location = new System.Drawing.Point(102, 37);
            this.reinstallFbliteTextbox.Name = "reinstallFbliteTextbox";
            this.reinstallFbliteTextbox.Size = new System.Drawing.Size(58, 29);
            this.reinstallFbliteTextbox.TabIndex = 215;
            this.reinstallFbliteTextbox.Text = "10";
            // 
            // installfblitecheckBox
            // 
            this.installfblitecheckBox.AutoSize = true;
            this.installfblitecheckBox.Location = new System.Drawing.Point(180, 195);
            this.installfblitecheckBox.Name = "installfblitecheckBox";
            this.installfblitecheckBox.Size = new System.Drawing.Size(133, 29);
            this.installfblitecheckBox.TabIndex = 260;
            this.installfblitecheckBox.Text = "Install fblite";
            this.installfblitecheckBox.UseVisualStyleBackColor = true;
            // 
            // changeSimGroupBox
            // 
            this.changeSimGroupBox.Controls.Add(this.offAllbutton);
            this.changeSimGroupBox.Controls.Add(this.onAllbutton);
            this.changeSimGroupBox.Controls.Add(this.button3G);
            this.changeSimGroupBox.Controls.Add(this.button4G);
            this.changeSimGroupBox.Controls.Add(this.viettelMobileButton);
            this.changeSimGroupBox.Controls.Add(this.vnMobiButton);
            this.changeSimGroupBox.Controls.Add(this.vnVinaphoneButton);
            this.changeSimGroupBox.Controls.Add(this.beelineButton);
            this.changeSimGroupBox.Controls.Add(this.mobiButton);
            this.changeSimGroupBox.Controls.Add(this.vietnamButton);
            this.changeSimGroupBox.Controls.Add(this.vinaButton);
            this.changeSimGroupBox.Controls.Add(this.viettelButton);
            this.changeSimGroupBox.Location = new System.Drawing.Point(1077, 29);
            this.changeSimGroupBox.Name = "changeSimGroupBox";
            this.changeSimGroupBox.Size = new System.Drawing.Size(280, 143);
            this.changeSimGroupBox.TabIndex = 221;
            this.changeSimGroupBox.TabStop = false;
            this.changeSimGroupBox.Text = "Change Sim";
            // 
            // offAllbutton
            // 
            this.offAllbutton.Location = new System.Drawing.Point(201, 109);
            this.offAllbutton.Name = "offAllbutton";
            this.offAllbutton.Size = new System.Drawing.Size(80, 26);
            this.offAllbutton.TabIndex = 123;
            this.offAllbutton.Text = "Off All";
            this.offAllbutton.UseVisualStyleBackColor = true;
            this.offAllbutton.Click += new System.EventHandler(this.offAllbutton_Click);
            // 
            // onAllbutton
            // 
            this.onAllbutton.Location = new System.Drawing.Point(201, 82);
            this.onAllbutton.Name = "onAllbutton";
            this.onAllbutton.Size = new System.Drawing.Size(80, 26);
            this.onAllbutton.TabIndex = 122;
            this.onAllbutton.Text = "On All";
            this.onAllbutton.UseVisualStyleBackColor = true;
            this.onAllbutton.Click += new System.EventHandler(this.onAllbutton_Click);
            // 
            // button3G
            // 
            this.button3G.Location = new System.Drawing.Point(201, 51);
            this.button3G.Name = "button3G";
            this.button3G.Size = new System.Drawing.Size(80, 26);
            this.button3G.TabIndex = 121;
            this.button3G.Text = "3G";
            this.button3G.UseVisualStyleBackColor = true;
            this.button3G.Click += new System.EventHandler(this.button3G_Click);
            // 
            // button4G
            // 
            this.button4G.Location = new System.Drawing.Point(201, 21);
            this.button4G.Name = "button4G";
            this.button4G.Size = new System.Drawing.Size(80, 26);
            this.button4G.TabIndex = 0;
            this.button4G.Text = "4G";
            this.button4G.UseVisualStyleBackColor = true;
            this.button4G.Click += new System.EventHandler(this.button4G_Click);
            // 
            // networkSimGroupBox
            // 
            this.networkSimGroupBox.Controls.Add(this.vietnamMobileCheckBox);
            this.networkSimGroupBox.Controls.Add(this.viettelCheckBox);
            this.networkSimGroupBox.Controls.Add(this.vinaphoneCheckbox);
            this.networkSimGroupBox.Controls.Add(this.mobiphoneCheckBox);
            this.networkSimGroupBox.Location = new System.Drawing.Point(1060, 703);
            this.networkSimGroupBox.Name = "networkSimGroupBox";
            this.networkSimGroupBox.Size = new System.Drawing.Size(200, 137);
            this.networkSimGroupBox.TabIndex = 221;
            this.networkSimGroupBox.TabStop = false;
            this.networkSimGroupBox.Text = "Network sim";
            // 
            // changeSim2Timer
            // 
            this.changeSim2Timer.Interval = 300000;
            this.changeSim2Timer.Tick += new System.EventHandler(this.changeSim2Timer_Tick);
            // 
            // installFacebookButton
            // 
            this.installFacebookButton.Location = new System.Drawing.Point(1542, 31);
            this.installFacebookButton.Name = "installFacebookButton";
            this.installFacebookButton.Size = new System.Drawing.Size(111, 57);
            this.installFacebookButton.TabIndex = 221;
            this.installFacebookButton.Text = "Install multi facebook";
            this.installFacebookButton.UseVisualStyleBackColor = true;
            this.installFacebookButton.Click += new System.EventHandler(this.installFacebookButton_Click);
            // 
            // forceAvatarUsCheckBox
            // 
            this.forceAvatarUsCheckBox.AutoSize = true;
            this.forceAvatarUsCheckBox.Location = new System.Drawing.Point(1080, 91);
            this.forceAvatarUsCheckBox.Name = "forceAvatarUsCheckBox";
            this.forceAvatarUsCheckBox.Size = new System.Drawing.Size(179, 29);
            this.forceAvatarUsCheckBox.TabIndex = 222;
            this.forceAvatarUsCheckBox.Text = "Force Avatar Us";
            this.forceAvatarUsCheckBox.UseVisualStyleBackColor = true;
            // 
            // sleep1MinuteCheckBox
            // 
            this.sleep1MinuteCheckBox.AutoSize = true;
            this.sleep1MinuteCheckBox.Location = new System.Drawing.Point(1080, 57);
            this.sleep1MinuteCheckBox.Name = "sleep1MinuteCheckBox";
            this.sleep1MinuteCheckBox.Size = new System.Drawing.Size(137, 29);
            this.sleep1MinuteCheckBox.TabIndex = 223;
            this.sleep1MinuteCheckBox.Text = "Nghỉ 1 phut";
            this.sleep1MinuteCheckBox.UseVisualStyleBackColor = true;
            // 
            // installApkFbButton
            // 
            this.installApkFbButton.ForeColor = System.Drawing.Color.Red;
            this.installApkFbButton.Location = new System.Drawing.Point(1624, 77);
            this.installApkFbButton.Name = "installApkFbButton";
            this.installApkFbButton.Size = new System.Drawing.Size(145, 35);
            this.installApkFbButton.TabIndex = 224;
            this.installApkFbButton.Text = "Install apk fb";
            this.installApkFbButton.UseVisualStyleBackColor = true;
            this.installApkFbButton.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // delayTimeTextBox
            // 
            this.delayTimeTextBox.Location = new System.Drawing.Point(1464, 15);
            this.delayTimeTextBox.Name = "delayTimeTextBox";
            this.delayTimeTextBox.Size = new System.Drawing.Size(35, 29);
            this.delayTimeTextBox.TabIndex = 225;
            this.delayTimeTextBox.Text = "250";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1087, 121);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(161, 25);
            this.label16.TabIndex = 226;
            this.label16.Text = "Nghỉ sau mỗi acc";
            // 
            // randomProxySimCheckBox
            // 
            this.randomProxySimCheckBox.AutoSize = true;
            this.randomProxySimCheckBox.Location = new System.Drawing.Point(1210, 152);
            this.randomProxySimCheckBox.Name = "randomProxySimCheckBox";
            this.randomProxySimCheckBox.Size = new System.Drawing.Size(202, 29);
            this.randomProxySimCheckBox.TabIndex = 228;
            this.randomProxySimCheckBox.Text = "Random Proxy/sim";
            this.randomProxySimCheckBox.UseVisualStyleBackColor = true;
            // 
            // resendTextBox
            // 
            this.resendTextBox.Location = new System.Drawing.Point(1898, 696);
            this.resendTextBox.Name = "resendTextBox";
            this.resendTextBox.Size = new System.Drawing.Size(31, 29);
            this.resendTextBox.TabIndex = 229;
            this.resendTextBox.Text = "10";
            // 
            // resendCheckBox
            // 
            this.resendCheckBox.AutoSize = true;
            this.resendCheckBox.Location = new System.Drawing.Point(1796, 578);
            this.resendCheckBox.Name = "resendCheckBox";
            this.resendCheckBox.Size = new System.Drawing.Size(140, 29);
            this.resendCheckBox.TabIndex = 230;
            this.resendCheckBox.Text = "Gửi lại code";
            this.resendCheckBox.UseVisualStyleBackColor = true;
            // 
            // changeProxyDroidCheckBox
            // 
            this.changeProxyDroidCheckBox.AutoSize = true;
            this.changeProxyDroidCheckBox.Location = new System.Drawing.Point(1802, 361);
            this.changeProxyDroidCheckBox.Name = "changeProxyDroidCheckBox";
            this.changeProxyDroidCheckBox.Size = new System.Drawing.Size(293, 29);
            this.changeProxyDroidCheckBox.TabIndex = 231;
            this.changeProxyDroidCheckBox.Text = "Change proxy by proxy  droid";
            this.changeProxyDroidCheckBox.UseVisualStyleBackColor = true;
            this.changeProxyDroidCheckBox.Visible = false;
            // 
            // changeProxyByCollegeCheckBox
            // 
            this.changeProxyByCollegeCheckBox.AutoSize = true;
            this.changeProxyByCollegeCheckBox.Location = new System.Drawing.Point(1802, 379);
            this.changeProxyByCollegeCheckBox.Name = "changeProxyByCollegeCheckBox";
            this.changeProxyByCollegeCheckBox.Size = new System.Drawing.Size(254, 29);
            this.changeProxyByCollegeCheckBox.TabIndex = 232;
            this.changeProxyByCollegeCheckBox.Text = "Change proxy by college";
            this.changeProxyByCollegeCheckBox.UseVisualStyleBackColor = true;
            this.changeProxyByCollegeCheckBox.Visible = false;
            // 
            // serverOnlineCheckBox
            // 
            this.serverOnlineCheckBox.AutoSize = true;
            this.serverOnlineCheckBox.Checked = true;
            this.serverOnlineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.serverOnlineCheckBox.ForeColor = System.Drawing.Color.Red;
            this.serverOnlineCheckBox.Location = new System.Drawing.Point(1796, 62);
            this.serverOnlineCheckBox.Name = "serverOnlineCheckBox";
            this.serverOnlineCheckBox.Size = new System.Drawing.Size(158, 29);
            this.serverOnlineCheckBox.TabIndex = 233;
            this.serverOnlineCheckBox.Text = "Server Online";
            this.serverOnlineCheckBox.UseVisualStyleBackColor = true;
            this.serverOnlineCheckBox.CheckedChanged += new System.EventHandler(this.serverOnlineCheckBox_CheckedChanged);
            // 
            // serverPathTextBox
            // 
            this.serverPathTextBox.Location = new System.Drawing.Point(1782, 83);
            this.serverPathTextBox.Name = "serverPathTextBox";
            this.serverPathTextBox.Size = new System.Drawing.Size(130, 29);
            this.serverPathTextBox.TabIndex = 234;
            this.serverPathTextBox.Text = "https://hes09ez92az.sn.mynetname.net";
            this.serverPathTextBox.TextChanged += new System.EventHandler(this.serverPathTextBox_TextChanged);
            // 
            // veriNvrBenNgoaiCheckBox
            // 
            this.veriNvrBenNgoaiCheckBox.AutoSize = true;
            this.veriNvrBenNgoaiCheckBox.Location = new System.Drawing.Point(1210, 136);
            this.veriNvrBenNgoaiCheckBox.Name = "veriNvrBenNgoaiCheckBox";
            this.veriNvrBenNgoaiCheckBox.Size = new System.Drawing.Size(204, 29);
            this.veriNvrBenNgoaiCheckBox.TabIndex = 235;
            this.veriNvrBenNgoaiCheckBox.Text = "Veri Nvr Ben Ngoai";
            this.veriNvrBenNgoaiCheckBox.UseVisualStyleBackColor = true;
            // 
            // startStoptimer
            // 
            this.startStoptimer.Interval = 300000;
            this.startStoptimer.Tick += new System.EventHandler(this.startStoptimer_Tick);
            // 
            // randPhone2Typetimer
            // 
            this.randPhone2Typetimer.Interval = 600000;
            this.randPhone2Typetimer.Tick += new System.EventHandler(this.randPhone2Typetimer_Tick);
            // 
            // openfblitecheckBox
            // 
            this.openfblitecheckBox.AutoSize = true;
            this.openfblitecheckBox.Checked = true;
            this.openfblitecheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openfblitecheckBox.Location = new System.Drawing.Point(1802, 345);
            this.openfblitecheckBox.Name = "openfblitecheckBox";
            this.openfblitecheckBox.Size = new System.Drawing.Size(132, 29);
            this.openfblitecheckBox.TabIndex = 236;
            this.openfblitecheckBox.Text = "Open fblite";
            this.openfblitecheckBox.UseVisualStyleBackColor = true;
            this.openfblitecheckBox.Visible = false;
            // 
            // moiFbLitecheckBox
            // 
            this.moiFbLitecheckBox.AutoSize = true;
            this.moiFbLitecheckBox.Location = new System.Drawing.Point(1411, 80);
            this.moiFbLitecheckBox.Name = "moiFbLitecheckBox";
            this.moiFbLitecheckBox.Size = new System.Drawing.Size(115, 29);
            this.moiFbLitecheckBox.TabIndex = 237;
            this.moiFbLitecheckBox.Text = "Mồi fblite";
            this.moiFbLitecheckBox.UseVisualStyleBackColor = true;
            this.moiFbLitecheckBox.CheckedChanged += new System.EventHandler(this.accMoiFbLitecheckBox_CheckedChanged);
            // 
            // removeAccFblitecheckBox
            // 
            this.removeAccFblitecheckBox.AutoSize = true;
            this.removeAccFblitecheckBox.Location = new System.Drawing.Point(1624, 35);
            this.removeAccFblitecheckBox.Name = "removeAccFblitecheckBox";
            this.removeAccFblitecheckBox.Size = new System.Drawing.Size(148, 29);
            this.removeAccFblitecheckBox.TabIndex = 238;
            this.removeAccFblitecheckBox.Text = "Gỡ Acc fblite";
            this.removeAccFblitecheckBox.UseVisualStyleBackColor = true;
            // 
            // accMoilabel
            // 
            this.accMoilabel.AutoSize = true;
            this.accMoilabel.ForeColor = System.Drawing.Color.Red;
            this.accMoilabel.Location = new System.Drawing.Point(1171, 49);
            this.accMoilabel.Name = "accMoilabel";
            this.accMoilabel.Size = new System.Drawing.Size(83, 25);
            this.accMoilabel.TabIndex = 245;
            this.accMoilabel.Text = "Acc Mồi";
            this.accMoilabel.Click += new System.EventHandler(this.accMoilabel_Click);
            // 
            // clearAccSettingcheckBox
            // 
            this.clearAccSettingcheckBox.AutoSize = true;
            this.clearAccSettingcheckBox.Checked = true;
            this.clearAccSettingcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearAccSettingcheckBox.Location = new System.Drawing.Point(1802, 402);
            this.clearAccSettingcheckBox.Name = "clearAccSettingcheckBox";
            this.clearAccSettingcheckBox.Size = new System.Drawing.Size(211, 29);
            this.clearAccSettingcheckBox.TabIndex = 246;
            this.clearAccSettingcheckBox.Text = "Clear Acc In Setting";
            this.clearAccSettingcheckBox.UseVisualStyleBackColor = true;
            this.clearAccSettingcheckBox.Visible = false;
            // 
            // addAccSettingCheckBox
            // 
            this.addAccSettingCheckBox.AutoSize = true;
            this.addAccSettingCheckBox.Location = new System.Drawing.Point(1532, 103);
            this.addAccSettingCheckBox.Name = "addAccSettingCheckBox";
            this.addAccSettingCheckBox.Size = new System.Drawing.Size(176, 29);
            this.addAccSettingCheckBox.TabIndex = 247;
            this.addAccSettingCheckBox.Text = "Add acc Setting";
            this.addAccSettingCheckBox.UseVisualStyleBackColor = true;
            // 
            // forceReupContactCheckBox
            // 
            this.forceReupContactCheckBox.AutoSize = true;
            this.forceReupContactCheckBox.Location = new System.Drawing.Point(1642, 178);
            this.forceReupContactCheckBox.Name = "forceReupContactCheckBox";
            this.forceReupContactCheckBox.Size = new System.Drawing.Size(237, 29);
            this.forceReupContactCheckBox.TabIndex = 248;
            this.forceReupContactCheckBox.Text = "Force reupload contact";
            this.forceReupContactCheckBox.UseVisualStyleBackColor = true;
            // 
            // openMessengerCheckBox
            // 
            this.openMessengerCheckBox.AutoSize = true;
            this.openMessengerCheckBox.Location = new System.Drawing.Point(1624, 56);
            this.openMessengerCheckBox.Name = "openMessengerCheckBox";
            this.openMessengerCheckBox.Size = new System.Drawing.Size(189, 29);
            this.openMessengerCheckBox.TabIndex = 249;
            this.openMessengerCheckBox.Text = "Open messenger";
            this.openMessengerCheckBox.UseVisualStyleBackColor = true;
            // 
            // onOffSimCountTextBox
            // 
            this.onOffSimCountTextBox.Location = new System.Drawing.Point(1866, 235);
            this.onOffSimCountTextBox.Name = "onOffSimCountTextBox";
            this.onOffSimCountTextBox.Size = new System.Drawing.Size(43, 29);
            this.onOffSimCountTextBox.TabIndex = 250;
            this.onOffSimCountTextBox.Text = "3";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(1777, 238);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(129, 25);
            this.label20.TabIndex = 251;
            this.label20.Text = "ON/OFF SIM";
            // 
            // countAccMoiTimer
            // 
            this.countAccMoiTimer.Interval = 30000;
            this.countAccMoiTimer.Tick += new System.EventHandler(this.countAccMoiTimer_Tick);
            // 
            // sockDroidCheckBox
            // 
            this.sockDroidCheckBox.AutoSize = true;
            this.sockDroidCheckBox.Location = new System.Drawing.Point(1412, 61);
            this.sockDroidCheckBox.Name = "sockDroidCheckBox";
            this.sockDroidCheckBox.Size = new System.Drawing.Size(131, 29);
            this.sockDroidCheckBox.TabIndex = 252;
            this.sockDroidCheckBox.Text = "Sock droid";
            this.sockDroidCheckBox.UseVisualStyleBackColor = true;
            // 
            // set2faWebCheckBox
            // 
            this.set2faWebCheckBox.AutoSize = true;
            this.set2faWebCheckBox.Location = new System.Drawing.Point(1796, 600);
            this.set2faWebCheckBox.Name = "set2faWebCheckBox";
            this.set2faWebCheckBox.Size = new System.Drawing.Size(142, 29);
            this.set2faWebCheckBox.TabIndex = 253;
            this.set2faWebCheckBox.Text = "Set2fa Web";
            this.set2faWebCheckBox.UseVisualStyleBackColor = true;
            // 
            // percentVeriFailTextBox
            // 
            this.percentVeriFailTextBox.Location = new System.Drawing.Point(1829, 147);
            this.percentVeriFailTextBox.Name = "percentVeriFailTextBox";
            this.percentVeriFailTextBox.Size = new System.Drawing.Size(53, 29);
            this.percentVeriFailTextBox.TabIndex = 254;
            this.percentVeriFailTextBox.Text = "60";
            // 
            // checkVeriTimer
            // 
            this.checkVeriTimer.Interval = 100000;
            this.checkVeriTimer.Tick += new System.EventHandler(this.checkVeriTimer_Tick);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Red;
            this.label24.Location = new System.Drawing.Point(1683, 158);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 25);
            this.label24.TabIndex = 255;
            this.label24.Text = "Percent";
            // 
            // resetDuoiMailtimer
            // 
            this.resetDuoiMailtimer.Interval = 300000;
            this.resetDuoiMailtimer.Tick += new System.EventHandler(this.resetDuoiMailtimer_Tick);
            // 
            // timerAvailableSellGmail
            // 
            this.timerAvailableSellGmail.Interval = 300000;
            this.timerAvailableSellGmail.Tick += new System.EventHandler(this.timerAvailableSellGmail_Tick);
            // 
            // removeProxy2checkBox
            // 
            this.removeProxy2checkBox.AutoSize = true;
            this.removeProxy2checkBox.Location = new System.Drawing.Point(1805, 271);
            this.removeProxy2checkBox.Name = "removeProxy2checkBox";
            this.removeProxy2checkBox.Size = new System.Drawing.Size(163, 29);
            this.removeProxy2checkBox.TabIndex = 256;
            this.removeProxy2checkBox.Text = "Remove proxy";
            this.removeProxy2checkBox.UseVisualStyleBackColor = true;
            this.removeProxy2checkBox.Visible = false;
            this.removeProxy2checkBox.CheckedChanged += new System.EventHandler(this.removeProxy2checkBox_CheckedChanged);
            // 
            // moiKatanacheckBox
            // 
            this.moiKatanacheckBox.AutoSize = true;
            this.moiKatanacheckBox.Location = new System.Drawing.Point(1411, 97);
            this.moiKatanacheckBox.Name = "moiKatanacheckBox";
            this.moiKatanacheckBox.Size = new System.Drawing.Size(134, 29);
            this.moiKatanacheckBox.TabIndex = 258;
            this.moiKatanacheckBox.Text = "Moi katana";
            this.moiKatanacheckBox.UseVisualStyleBackColor = true;
            this.moiKatanacheckBox.CheckedChanged += new System.EventHandler(this.moiKatanacheckBox_CheckedChanged);
            // 
            // rebootFakerpluscheckBox
            // 
            this.rebootFakerpluscheckBox.AutoSize = true;
            this.rebootFakerpluscheckBox.Location = new System.Drawing.Point(1532, 156);
            this.rebootFakerpluscheckBox.Name = "rebootFakerpluscheckBox";
            this.rebootFakerpluscheckBox.Size = new System.Drawing.Size(191, 29);
            this.rebootFakerpluscheckBox.TabIndex = 259;
            this.rebootFakerpluscheckBox.Text = "Reboot Fakerplus";
            this.rebootFakerpluscheckBox.UseVisualStyleBackColor = true;
            // 
            // installMissingMessengercheckBox
            // 
            this.installMissingMessengercheckBox.AutoSize = true;
            this.installMissingMessengercheckBox.Location = new System.Drawing.Point(1781, 218);
            this.installMissingMessengercheckBox.Name = "installMissingMessengercheckBox";
            this.installMissingMessengercheckBox.Size = new System.Drawing.Size(191, 29);
            this.installMissingMessengercheckBox.TabIndex = 261;
            this.installMissingMessengercheckBox.Text = "Install Messenger";
            this.installMissingMessengercheckBox.UseVisualStyleBackColor = true;
            // 
            // moiBusinesscheckBox
            // 
            this.moiBusinesscheckBox.AutoSize = true;
            this.moiBusinesscheckBox.Location = new System.Drawing.Point(1411, 113);
            this.moiBusinesscheckBox.Name = "moiBusinesscheckBox";
            this.moiBusinesscheckBox.Size = new System.Drawing.Size(155, 29);
            this.moiBusinesscheckBox.TabIndex = 262;
            this.moiBusinesscheckBox.Text = "Mồi Business";
            this.moiBusinesscheckBox.UseVisualStyleBackColor = true;
            this.moiBusinesscheckBox.CheckedChanged += new System.EventHandler(this.moiBusinesscheckBox_CheckedChanged);
            // 
            // moiKhong2facheckBox
            // 
            this.moiKhong2facheckBox.AutoSize = true;
            this.moiKhong2facheckBox.Location = new System.Drawing.Point(1567, 275);
            this.moiKhong2facheckBox.Name = "moiKhong2facheckBox";
            this.moiKhong2facheckBox.Size = new System.Drawing.Size(161, 29);
            this.moiKhong2facheckBox.TabIndex = 263;
            this.moiKhong2facheckBox.Text = "Moi không 2fa";
            this.moiKhong2facheckBox.UseVisualStyleBackColor = true;
            // 
            // getProyx20timecheckBox
            // 
            this.getProyx20timecheckBox.AutoSize = true;
            this.getProyx20timecheckBox.Location = new System.Drawing.Point(237, 293);
            this.getProyx20timecheckBox.Name = "getProyx20timecheckBox";
            this.getProyx20timecheckBox.Size = new System.Drawing.Size(181, 29);
            this.getProyx20timecheckBox.TabIndex = 264;
            this.getProyx20timecheckBox.Text = "Lấy proxy 20 lần";
            this.getProyx20timecheckBox.UseVisualStyleBackColor = true;
            // 
            // randomOnOffSimcheckBox
            // 
            this.randomOnOffSimcheckBox.AutoSize = true;
            this.randomOnOffSimcheckBox.Location = new System.Drawing.Point(1781, 258);
            this.randomOnOffSimcheckBox.Name = "randomOnOffSimcheckBox";
            this.randomOnOffSimcheckBox.Size = new System.Drawing.Size(198, 29);
            this.randomOnOffSimcheckBox.TabIndex = 265;
            this.randomOnOffSimcheckBox.Text = "RandomOnOffSim";
            this.randomOnOffSimcheckBox.UseVisualStyleBackColor = true;
            // 
            // forceMoiThanhCongcheckBox
            // 
            this.forceMoiThanhCongcheckBox.AutoSize = true;
            this.forceMoiThanhCongcheckBox.Location = new System.Drawing.Point(1567, 293);
            this.forceMoiThanhCongcheckBox.Name = "forceMoiThanhCongcheckBox";
            this.forceMoiThanhCongcheckBox.Size = new System.Drawing.Size(103, 29);
            this.forceMoiThanhCongcheckBox.TabIndex = 266;
            this.forceMoiThanhCongcheckBox.Text = "Ép mồi ";
            this.forceMoiThanhCongcheckBox.UseVisualStyleBackColor = true;
            // 
            // startWithtextBox
            // 
            this.startWithtextBox.Location = new System.Drawing.Point(1733, 148);
            this.startWithtextBox.Name = "startWithtextBox";
            this.startWithtextBox.Size = new System.Drawing.Size(100, 29);
            this.startWithtextBox.TabIndex = 267;
            this.startWithtextBox.Text = "27";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(1621, 140);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(34, 25);
            this.label25.TabIndex = 268;
            this.label25.Text = "Ip:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(1784, 178);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(74, 25);
            this.label26.TabIndex = 269;
            this.label26.Text = "Số lần:";
            // 
            // soLanChangeIptextBox
            // 
            this.soLanChangeIptextBox.Location = new System.Drawing.Point(1823, 176);
            this.soLanChangeIptextBox.Name = "soLanChangeIptextBox";
            this.soLanChangeIptextBox.Size = new System.Drawing.Size(100, 29);
            this.soLanChangeIptextBox.TabIndex = 270;
            this.soLanChangeIptextBox.Text = "1";
            // 
            // pushFileChargerbutton
            // 
            this.pushFileChargerbutton.Location = new System.Drawing.Point(1662, 12);
            this.pushFileChargerbutton.Name = "pushFileChargerbutton";
            this.pushFileChargerbutton.Size = new System.Drawing.Size(108, 21);
            this.pushFileChargerbutton.TabIndex = 271;
            this.pushFileChargerbutton.Text = "Push file Charger";
            this.pushFileChargerbutton.UseVisualStyleBackColor = true;
            this.pushFileChargerbutton.Click += new System.EventHandler(this.pushFileChargerbutton_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(1663, 293);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(131, 23);
            this.button8.TabIndex = 272;
            this.button8.Text = "Bật Changer60phone";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(1805, 293);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(115, 21);
            this.button9.TabIndex = 274;
            this.button9.Text = "Bật emu";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // tatcaiconlaicheckBox
            // 
            this.tatcaiconlaicheckBox.AutoSize = true;
            this.tatcaiconlaicheckBox.Location = new System.Drawing.Point(1667, 271);
            this.tatcaiconlaicheckBox.Name = "tatcaiconlaicheckBox";
            this.tatcaiconlaicheckBox.Size = new System.Drawing.Size(158, 29);
            this.tatcaiconlaicheckBox.TabIndex = 275;
            this.tatcaiconlaicheckBox.Text = "Tắt cái còn lại";
            this.tatcaiconlaicheckBox.UseVisualStyleBackColor = true;
            // 
            // restartAfterchangecheckBox
            // 
            this.restartAfterchangecheckBox.AutoSize = true;
            this.restartAfterchangecheckBox.Location = new System.Drawing.Point(1651, 260);
            this.restartAfterchangecheckBox.Name = "restartAfterchangecheckBox";
            this.restartAfterchangecheckBox.Size = new System.Drawing.Size(278, 29);
            this.restartAfterchangecheckBox.TabIndex = 276;
            this.restartAfterchangecheckBox.Text = "Restart máy sau khi change";
            this.restartAfterchangecheckBox.UseVisualStyleBackColor = true;
            // 
            // serverCacheMailTextbox
            // 
            this.serverCacheMailTextbox.Location = new System.Drawing.Point(1697, 38);
            this.serverCacheMailTextbox.Name = "serverCacheMailTextbox";
            this.serverCacheMailTextbox.Size = new System.Drawing.Size(228, 29);
            this.serverCacheMailTextbox.TabIndex = 277;
            this.serverCacheMailTextbox.Text = "http://hes09ez92az.sn.mynetname.net:8081";
            this.serverCacheMailTextbox.TextChanged += new System.EventHandler(this.serverCacheMailTextbox_TextChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(1792, 18);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(173, 25);
            this.label27.TabIndex = 278;
            this.label27.Text = "Server Cache mail";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1770, 113);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(151, 36);
            this.button10.TabIndex = 279;
            this.button10.Text = "Update Server";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // fixPasswordtextBox
            // 
            this.fixPasswordtextBox.Location = new System.Drawing.Point(1814, 532);
            this.fixPasswordtextBox.Name = "fixPasswordtextBox";
            this.fixPasswordtextBox.Size = new System.Drawing.Size(100, 29);
            this.fixPasswordtextBox.TabIndex = 280;
            this.fixPasswordtextBox.Text = "Matkhau12345";
            this.fixPasswordtextBox.Visible = false;
            this.fixPasswordtextBox.TextChanged += new System.EventHandler(this.fixPasswordtextBox_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(1539, 209);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(98, 25);
            this.label28.TabIndex = 281;
            this.label28.Text = "Password";
            this.label28.Visible = false;
            // 
            // fixPasswordCheckbox
            // 
            this.fixPasswordCheckbox.AutoSize = true;
            this.fixPasswordCheckbox.ForeColor = System.Drawing.Color.Red;
            this.fixPasswordCheckbox.Location = new System.Drawing.Point(1584, 204);
            this.fixPasswordCheckbox.Name = "fixPasswordCheckbox";
            this.fixPasswordCheckbox.Size = new System.Drawing.Size(155, 29);
            this.fixPasswordCheckbox.TabIndex = 282;
            this.fixPasswordCheckbox.Text = "Fix Password";
            this.fixPasswordCheckbox.UseVisualStyleBackColor = true;
            this.fixPasswordCheckbox.Visible = false;
            this.fixPasswordCheckbox.CheckedChanged += new System.EventHandler(this.fixPasswordCheckbox_CheckedChanged);
            // 
            // moiMessengercheckBox
            // 
            this.moiMessengercheckBox.AutoSize = true;
            this.moiMessengercheckBox.Location = new System.Drawing.Point(1411, 129);
            this.moiMessengercheckBox.Name = "moiMessengercheckBox";
            this.moiMessengercheckBox.Size = new System.Drawing.Size(173, 29);
            this.moiMessengercheckBox.TabIndex = 283;
            this.moiMessengercheckBox.Text = "Mồi Messenger";
            this.moiMessengercheckBox.UseVisualStyleBackColor = true;
            this.moiMessengercheckBox.CheckedChanged += new System.EventHandler(this.moiMessengercheckBox_CheckedChanged);
            // 
            // logProxycheckBox
            // 
            this.logProxycheckBox.AutoSize = true;
            this.logProxycheckBox.Location = new System.Drawing.Point(1080, 299);
            this.logProxycheckBox.Name = "logProxycheckBox";
            this.logProxycheckBox.Size = new System.Drawing.Size(126, 29);
            this.logProxycheckBox.TabIndex = 285;
            this.logProxycheckBox.Text = "Log Proxy";
            this.logProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // moiTruocProxycheckBox
            // 
            this.moiTruocProxycheckBox.AutoSize = true;
            this.moiTruocProxycheckBox.Checked = true;
            this.moiTruocProxycheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moiTruocProxycheckBox.Location = new System.Drawing.Point(1411, 161);
            this.moiTruocProxycheckBox.Name = "moiTruocProxycheckBox";
            this.moiTruocProxycheckBox.Size = new System.Drawing.Size(171, 29);
            this.moiTruocProxycheckBox.TabIndex = 286;
            this.moiTruocProxycheckBox.Text = "Mồi trước proxy";
            this.moiTruocProxycheckBox.UseVisualStyleBackColor = true;
            // 
            // chuyenQuaMoiKatanacheckBox
            // 
            this.chuyenQuaMoiKatanacheckBox.AutoSize = true;
            this.chuyenQuaMoiKatanacheckBox.Location = new System.Drawing.Point(1411, 239);
            this.chuyenQuaMoiKatanacheckBox.Name = "chuyenQuaMoiKatanacheckBox";
            this.chuyenQuaMoiKatanacheckBox.Size = new System.Drawing.Size(245, 29);
            this.chuyenQuaMoiKatanacheckBox.TabIndex = 287;
            this.chuyenQuaMoiKatanacheckBox.Text = "Chuyển qua mồi katana";
            this.chuyenQuaMoiKatanacheckBox.UseVisualStyleBackColor = true;
            // 
            // moiBusinessNhanhcheckBox
            // 
            this.moiBusinessNhanhcheckBox.AutoSize = true;
            this.moiBusinessNhanhcheckBox.Checked = true;
            this.moiBusinessNhanhcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moiBusinessNhanhcheckBox.Location = new System.Drawing.Point(1411, 145);
            this.moiBusinessNhanhcheckBox.Name = "moiBusinessNhanhcheckBox";
            this.moiBusinessNhanhcheckBox.Size = new System.Drawing.Size(215, 29);
            this.moiBusinessNhanhcheckBox.TabIndex = 288;
            this.moiBusinessNhanhcheckBox.Text = "Mồi Business nhanh";
            this.moiBusinessNhanhcheckBox.UseVisualStyleBackColor = true;
            // 
            // randomMoicheckBox
            // 
            this.randomMoicheckBox.AutoSize = true;
            this.randomMoicheckBox.Location = new System.Drawing.Point(1799, 738);
            this.randomMoicheckBox.Name = "randomMoicheckBox";
            this.randomMoicheckBox.Size = new System.Drawing.Size(148, 29);
            this.randomMoicheckBox.TabIndex = 289;
            this.randomMoicheckBox.Text = "Random Mồi";
            this.randomMoicheckBox.UseVisualStyleBackColor = true;
            // 
            // chuyenQuaveriGmailcheckBox
            // 
            this.chuyenQuaveriGmailcheckBox.AutoSize = true;
            this.chuyenQuaveriGmailcheckBox.Location = new System.Drawing.Point(1411, 254);
            this.chuyenQuaveriGmailcheckBox.Name = "chuyenQuaveriGmailcheckBox";
            this.chuyenQuaveriGmailcheckBox.Size = new System.Drawing.Size(232, 29);
            this.chuyenQuaveriGmailcheckBox.TabIndex = 290;
            this.chuyenQuaveriGmailcheckBox.Text = "Chuyển qua veri gmail";
            this.chuyenQuaveriGmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // randomVersioncheckBox
            // 
            this.randomVersioncheckBox.AutoSize = true;
            this.randomVersioncheckBox.Location = new System.Drawing.Point(1411, 209);
            this.randomVersioncheckBox.Name = "randomVersioncheckBox";
            this.randomVersioncheckBox.Size = new System.Drawing.Size(183, 29);
            this.randomVersioncheckBox.TabIndex = 291;
            this.randomVersioncheckBox.Text = "Random Version";
            this.randomVersioncheckBox.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(1542, 236);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(100, 23);
            this.button13.TabIndex = 292;
            this.button13.Text = "Install Latest FB";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // randomVersionSaudiecheckBox
            // 
            this.randomVersionSaudiecheckBox.AutoSize = true;
            this.randomVersionSaudiecheckBox.Checked = true;
            this.randomVersionSaudiecheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomVersionSaudiecheckBox.Location = new System.Drawing.Point(1411, 194);
            this.randomVersionSaudiecheckBox.Name = "randomVersionSaudiecheckBox";
            this.randomVersionSaudiecheckBox.Size = new System.Drawing.Size(255, 29);
            this.randomVersionSaudiecheckBox.TabIndex = 293;
            this.randomVersionSaudiecheckBox.Text = "Random Version Sau die";
            this.randomVersionSaudiecheckBox.UseVisualStyleBackColor = true;
            // 
            // randomVersionAfterverifailcheckBox
            // 
            this.randomVersionAfterverifailcheckBox.AutoSize = true;
            this.randomVersionAfterverifailcheckBox.Checked = true;
            this.randomVersionAfterverifailcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomVersionAfterverifailcheckBox.Location = new System.Drawing.Point(1411, 176);
            this.randomVersionAfterverifailcheckBox.Name = "randomVersionAfterverifailcheckBox";
            this.randomVersionAfterverifailcheckBox.Size = new System.Drawing.Size(232, 29);
            this.randomVersionAfterverifailcheckBox.TabIndex = 298;
            this.randomVersionAfterverifailcheckBox.Text = "Rand Ver after Veri fail";
            this.randomVersionAfterverifailcheckBox.UseVisualStyleBackColor = true;
            // 
            // ResetStatustimer
            // 
            this.ResetStatustimer.Interval = 3600000;
            this.ResetStatustimer.Tick += new System.EventHandler(this.ResetStatustimer_Tick);
            // 
            // chuyenquanvrcheckBox
            // 
            this.chuyenquanvrcheckBox.AutoSize = true;
            this.chuyenquanvrcheckBox.Location = new System.Drawing.Point(1411, 270);
            this.chuyenquanvrcheckBox.Name = "chuyenquanvrcheckBox";
            this.chuyenquanvrcheckBox.Size = new System.Drawing.Size(177, 29);
            this.chuyenquanvrcheckBox.TabIndex = 307;
            this.chuyenquanvrcheckBox.Text = "Chuyen qua nvr";
            this.chuyenquanvrcheckBox.UseVisualStyleBackColor = true;
            // 
            // changeProxy2P1checkBox
            // 
            this.changeProxy2P1checkBox.AutoSize = true;
            this.changeProxy2P1checkBox.Location = new System.Drawing.Point(1411, 288);
            this.changeProxy2P1checkBox.Name = "changeProxy2P1checkBox";
            this.changeProxy2P1checkBox.Size = new System.Drawing.Size(208, 29);
            this.changeProxy2P1checkBox.TabIndex = 308;
            this.changeProxy2P1checkBox.Text = "Change Proxy 2 P1";
            this.changeProxy2P1checkBox.UseVisualStyleBackColor = true;
            // 
            // chuyenQuaHotmailcheckBox
            // 
            this.chuyenQuaHotmailcheckBox.AutoSize = true;
            this.chuyenQuaHotmailcheckBox.Location = new System.Drawing.Point(1411, 224);
            this.chuyenQuaHotmailcheckBox.Name = "chuyenQuaHotmailcheckBox";
            this.chuyenQuaHotmailcheckBox.Size = new System.Drawing.Size(212, 29);
            this.chuyenQuaHotmailcheckBox.TabIndex = 309;
            this.chuyenQuaHotmailcheckBox.Text = "Chuyển qua hotmail";
            this.chuyenQuaHotmailcheckBox.UseVisualStyleBackColor = true;
            // 
            // UpdateStatusSheettimer
            // 
            this.UpdateStatusSheettimer.Interval = 60000;
            this.UpdateStatusSheettimer.Tick += new System.EventHandler(this.UpdateStatusSheettimer_Tick);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(1810, 826);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 54);
            this.button16.TabIndex = 333;
            this.button16.Text = "Delete all Wifi";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click_1);
            // 
            // InforMailtimer
            // 
            this.InforMailtimer.Interval = 5000;
            this.InforMailtimer.Tick += new System.EventHandler(this.InforMailtimer_Tick);
            // 
            // forceRebootAfterClearcheckBox
            // 
            this.forceRebootAfterClearcheckBox.AutoSize = true;
            this.forceRebootAfterClearcheckBox.Location = new System.Drawing.Point(1801, 324);
            this.forceRebootAfterClearcheckBox.Name = "forceRebootAfterClearcheckBox";
            this.forceRebootAfterClearcheckBox.Size = new System.Drawing.Size(233, 29);
            this.forceRebootAfterClearcheckBox.TabIndex = 334;
            this.forceRebootAfterClearcheckBox.Text = "ForceRebootAfterclear";
            this.forceRebootAfterClearcheckBox.UseVisualStyleBackColor = true;
            // 
            // HideRootbutton
            // 
            this.HideRootbutton.Location = new System.Drawing.Point(1797, 908);
            this.HideRootbutton.Name = "HideRootbutton";
            this.HideRootbutton.Size = new System.Drawing.Size(87, 35);
            this.HideRootbutton.TabIndex = 335;
            this.HideRootbutton.Text = "Ản root";
            this.HideRootbutton.UseVisualStyleBackColor = true;
            this.HideRootbutton.Click += new System.EventHandler(this.HideRootbutton_Click);
            // 
            // ScanDevicetimer
            // 
            this.ScanDevicetimer.Interval = 30000;
            this.ScanDevicetimer.Tick += new System.EventHandler(this.ScanDevicetimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(2384, 1111);
            this.Controls.Add(this.HideRootbutton);
            this.Controls.Add(this.forceRebootAfterClearcheckBox);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.statusSpeedlabel);
            this.Controls.Add(this.chuyenQuaHotmailcheckBox);
            this.Controls.Add(this.changeProxy2P1checkBox);
            this.Controls.Add(this.chuyenquanvrcheckBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.chayepdanbacheckBox);
            this.Controls.Add(this.randomVersionAfterverifailcheckBox);
            this.Controls.Add(this.choPutOtpcheckBox);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.mathetextBox);
            this.Controls.Add(this.randomVersionSaudiecheckBox);
            this.Controls.Add(this.gmail48hradioButton);
            this.Controls.Add(this.chuyenHotmailNhanhcheckBox);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.gmailOtpRadioButton);
            this.Controls.Add(this.randomVersioncheckBox);
            this.Controls.Add(this.chuyenQuaveriGmailcheckBox);
            this.Controls.Add(this.sellGmailServerradioButton);
            this.Controls.Add(this.randomMoicheckBox);
            this.Controls.Add(this.luuDuoiMailcheckBox);
            this.Controls.Add(this.moiBusinessNhanhcheckBox);
            this.Controls.Add(this.getDuoiMailFromServercheckBox);
            this.Controls.Add(this.chuyenQuaMoiKatanacheckBox);
            this.Controls.Add(this.randomDuoiMailcheckBox);
            this.Controls.Add(this.moiTruocProxycheckBox);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.runBoxLancheckBox);
            this.Controls.Add(this.logProxycheckBox);
            this.Controls.Add(this.moiMessengercheckBox);
            this.Controls.Add(this.fixPasswordCheckbox);
            this.Controls.Add(this.openFbByDeepLinkcheckBox1);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.fixPasswordtextBox);
            this.Controls.Add(this.InputEnglishNameCheckbox);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.otpVandongcheckBox);
            this.Controls.Add(this.chayuploadContactcheckBox);
            this.Controls.Add(this.setFastProxybutton);
            this.Controls.Add(this.getTrustMailcheckBox);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.fastcheckBox);
            this.Controls.Add(this.serverCacheMailTextbox);
            this.Controls.Add(this.restartAfterchangecheckBox);
            this.Controls.Add(this.tatcaiconlaicheckBox);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.pushFileChargerbutton);
            this.Controls.Add(this.soLanChangeIptextBox);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.activeDuoiMailtextBox);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.startWithtextBox);
            this.Controls.Add(this.forceMoiThanhCongcheckBox);
            this.Controls.Add(this.randomRegVericheckBox);
            this.Controls.Add(this.randomOnOffSimcheckBox);
            this.Controls.Add(this.getProyx20timecheckBox);
            this.Controls.Add(this.moiKhong2facheckBox);
            this.Controls.Add(this.phoneTypeLabel);
            this.Controls.Add(this.fixDuoiMailCheckBox);
            this.Controls.Add(this.moiBusinesscheckBox);
            this.Controls.Add(this.randPhone2TypecheckBox);
            this.Controls.Add(this.randomOldContactCheckBox);
            this.Controls.Add(this.installMissingMessengercheckBox);
            this.Controls.Add(this.rebootFakerpluscheckBox);
            this.Controls.Add(this.fixDuoiMailTextBox);
            this.Controls.Add(this.moiKatanacheckBox);
            this.Controls.Add(this.removeProxy2checkBox);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.percentVeriFailTextBox);
            this.Controls.Add(this.set2faWebCheckBox);
            this.Controls.Add(this.sockDroidCheckBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.onOffSimCountTextBox);
            this.Controls.Add(this.openMessengerCheckBox);
            this.Controls.Add(this.forceReupContactCheckBox);
            this.Controls.Add(this.addAccSettingCheckBox);
            this.Controls.Add(this.clearAccSettingcheckBox);
            this.Controls.Add(this.accMoilabel);
            this.Controls.Add(this.removeAccFblitecheckBox);
            this.Controls.Add(this.moiFbLitecheckBox);
            this.Controls.Add(this.openfblitecheckBox);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.veriNvrBenNgoaiCheckBox);
            this.Controls.Add(this.serverPathTextBox);
            this.Controls.Add(this.addDeviceButton);
            this.Controls.Add(this.change2Ip6Button);
            this.Controls.Add(this.veriBackupCheckBox);
            this.Controls.Add(this.change2Ip46Button);
            this.Controls.Add(this.alwaysChangeAirplaneCheckBox);
            this.Controls.Add(this.change2Ip4Button);
            this.Controls.Add(this.serverOnlineCheckBox);
            this.Controls.Add(this.changeProxyByCollegeCheckBox);
            this.Controls.Add(this.deviceFakerPlusCheckBox);
            this.Controls.Add(this.changeProxyDroidCheckBox);
            this.Controls.Add(this.resendCheckBox);
            this.Controls.Add(this.resendTextBox);
            this.Controls.Add(this.randomProxySimCheckBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.delayTimeTextBox);
            this.Controls.Add(this.installApkFbButton);
            this.Controls.Add(this.sleep1MinuteCheckBox);
            this.Controls.Add(this.forceAvatarUsCheckBox);
            this.Controls.Add(this.installFacebookButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.startButtonGroupBox);
            this.Controls.Add(this.fbInfoLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Fb Reg - LONGFB.NET-222";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.startButtonGroupBox.ResumeLayout(false);
            this.startButtonGroupBox.PerformLayout();
            this.romgroupBox.ResumeLayout(false);
            this.romgroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.orderGroupBox.ResumeLayout(false);
            this.orderGroupBox.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.devicesTabPage.ResumeLayout(false);
            this.devicesTabPage.PerformLayout();
            this.settingTabPage.ResumeLayout(false);
            this.settingTabPage.PerformLayout();
            this.emailTypeGroupBox.ResumeLayout(false);
            this.emailTypeGroupBox.PerformLayout();
            this.proxyGroupBox.ResumeLayout(false);
            this.proxyGroupBox.PerformLayout();
            this.controlGroupBox.ResumeLayout(false);
            this.controlGroupBox.PerformLayout();
            this.changeSimGroupBox.ResumeLayout(false);
            this.networkSimGroupBox.ResumeLayout(false);
            this.networkSimGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void femaleCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (femaleCheckbox.Checked)
            {
                maleCheckbox.Checked = false;
            }
        }

        
        private void mailCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (maleCheckbox.Checked)
            {
                femaleCheckbox.Checked = false;
            }
        }

        #endregion
        private System.Windows.Forms.Button runAllBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label reportLabel;
        private System.Windows.Forms.Label label1;
        
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.CheckBox InputEnglishNameCheckbox;
        private System.Windows.Forms.CheckBox femaleCheckbox;
        private System.Windows.Forms.CheckBox maleCheckbox;
        private System.Windows.Forms.CheckBox set2faCheckbox;
        private System.Windows.Forms.CheckBox runAvatarCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dausotextbox;
        private System.Windows.Forms.Button resetStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox yearOldFrom;
        private System.Windows.Forms.TextBox yearOldTo;
        private System.Windows.Forms.CheckBox TempMailcheckBox;
        private System.Windows.Forms.Button downloadAvatarBtn;
        private System.Windows.Forms.CheckBox randomPrePhoneCheckbox;
        private System.Windows.Forms.CheckBox noSuggestCheckbox;
        private System.Windows.Forms.TextBox delayTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox fbLiteCheckbox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox inputStringCheckbox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button addDeviceButton;
        private System.Windows.Forms.CheckBox regByMailCheckBox;
        private System.Windows.Forms.CheckBox verifiedCheckbox;
        private System.Windows.Forms.TextBox maxAccContinueTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox timeBreakTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox mailTextbox;
        private System.Windows.Forms.Button getCodeButton;
        private System.Windows.Forms.Label codeLabel;
        private System.Windows.Forms.CheckBox airplaneCheckBox;
        private System.Windows.Forms.CheckBox proxyCheckBox;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.CheckBox usePhoneLocalCheckBox;
        private System.Windows.Forms.Button installApk;
        private System.Windows.Forms.CheckBox changeDeviceEmuCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ssidTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox wifiPassTextBox;
        private System.Windows.Forms.Button setWifiButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button rebootAllbutton;
        private System.Windows.Forms.ComboBox timeZoneComboBox;
        private System.Windows.Forms.Button setTimeZoneButton;
        private System.Windows.Forms.CheckBox usPhoneCheckBox;
        private System.Windows.Forms.TextBox maxAccBlockRuntextBox;
        private System.Windows.Forms.TextBox timebreakDeadLocktextBox;
        private System.Windows.Forms.Button uninstallFbBtn;
        private System.Windows.Forms.Label Block;
        private System.Windows.Forms.Button viettelButton;
        private System.Windows.Forms.Button vinaButton;
        private System.Windows.Forms.Button vietnamButton;
        private System.Windows.Forms.CheckBox holdingCheckBox;
        private System.Windows.Forms.Button mobiButton;
        private System.Windows.Forms.Button beelineButton;
        private System.Windows.Forms.CheckBox vietCheckbox;
        private System.Windows.Forms.Button rootAdbButton;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckBox androidIDCheckBox;
        private System.Windows.Forms.CheckBox brightCheckBox;
        private System.Windows.Forms.CheckBox addFriendCheckBox;
        private System.Windows.Forms.CheckBox profileCheckBox;
        private System.Windows.Forms.CheckBox airplaneEnableCheckBox;
        private System.Windows.Forms.TextBox mailSuffixtextBox;
        private System.Windows.Forms.CheckBox miniProfileCheckBox;
        private System.Windows.Forms.Button turnOnSimButton;
        private System.Windows.Forms.CheckBox clearCacheCheckBox;
        private System.Windows.Forms.Button change2Ip4Button;
        private System.Windows.Forms.CheckBox runningCheckBox;
        private System.Windows.Forms.CheckBox changeSimCheckBox;
        private System.Windows.Forms.Button viettelTeleButton;
        private System.Windows.Forms.Button viettelMobileButton;
        private System.Windows.Forms.Button vnMobiButton;
        private System.Windows.Forms.CheckBox clearFbLiteCheckBox;
        private System.Windows.Forms.CheckBox forgotCheckBox;
        private System.Windows.Forms.Button change2Ip6Button;
        private System.Windows.Forms.Button change2Ip46Button;
        private System.Windows.Forms.CheckBox unsignCheckBox;
        private System.Windows.Forms.Button vnVinaphoneButton;
        private System.Windows.Forms.CheckBox randomMailPhoneSimCheckBox;
        private System.Windows.Forms.CheckBox verifyAccNvrCheckBox;
        private System.Windows.Forms.CheckBox autoRunVeriCheckBox;
        private System.Windows.Forms.CheckBox homeCheckBox;
        private System.Windows.Forms.Button checkFBInstalledBtn;
        private System.Windows.Forms.CheckBox randomMailPhoneCheckBox;
        private System.Windows.Forms.Button installMissingFBbutton;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox minSpeedTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox autoSpeedCheckBox;
        private System.Windows.Forms.Label speedlabel;
        private System.Windows.Forms.CheckBox autoVeriMailCheckBox;
        private System.Windows.Forms.Button turnoffSimButton;
        private System.Windows.Forms.Button rmFbliteButton;
        private System.Windows.Forms.CheckBox randomPhoneCheckBox;
        private System.Windows.Forms.CheckBox changeSimType2CheckBox;
        private System.Windows.Forms.CheckBox randomAllSimCheckBox;
        private System.Windows.Forms.CheckBox randomVeriCheckBox;
        private System.Windows.Forms.CheckBox vietUsCheckBox;
        private System.Windows.Forms.CheckBox veriHotmailCheckBox;
        private System.Windows.Forms.CheckBox vinaphoneCheckbox;
        private System.Windows.Forms.CheckBox viettelCheckBox;
        private System.Windows.Forms.CheckBox mobiphoneCheckBox;
        private System.Windows.Forms.CheckBox vietnamMobileCheckBox;
        private System.Windows.Forms.TextBox shoplikeTextBox1;
        private System.Windows.Forms.Button loadTinsoftButton;
        private System.Windows.Forms.CheckBox adbKeyCheckBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button turnOffEmuButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.CheckBox veriContactCheckBox;
        private System.Windows.Forms.CheckBox veriPhoneCheckBox;
        private System.Windows.Forms.CheckBox nvrUpAvatarCheckBox;
        private System.Windows.Forms.CheckBox textnowCheckbox;
        private System.Windows.Forms.CheckBox checkLoginCheckBox;
        private System.Windows.Forms.CheckBox coverCheckBox;
        private System.Windows.Forms.CheckBox noveriCoverCheckBox;
        private System.Windows.Forms.CheckBox loginByUserPassCheckBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.CheckBox addStatusCheckBox;
        private System.Windows.Forms.CheckBox veriDirectByPhoneCheckBox;
        private System.Windows.Forms.CheckBox americaPhoneCheckBox;
        private System.Windows.Forms.Button changeSimUsButton;
        private System.Windows.Forms.Button vietSimButton;
        private System.Windows.Forms.CheckBox micerCheckBox;
        private System.Windows.Forms.Button executeAdbButton;
        private System.Windows.Forms.TextBox codeKeyTextNowTextBox;
        private System.Windows.Forms.CheckBox clearCacheFBcheckBox;
        private System.Windows.Forms.CheckBox accMoiCheckBox;

        private System.Windows.Forms.TextBox cookieCodeTextNowtextBox;
        private System.Windows.Forms.Button turnOnEmubutton;
        private System.Windows.Forms.TextBox otpKeyTextBox;
        private System.Windows.Forms.Label reportPhoneLabel;
        private System.Windows.Forms.Button getPhoneCodeTextNowbutton;
        private System.Windows.Forms.CheckBox prefixTextNowCheckBox;
        private System.Windows.Forms.Label phoneInQueuelabel;
        private System.Windows.Forms.CheckBox veriMailAfterPhonecheckBox;
        private System.Windows.Forms.CheckBox forceIp4CheckBox;
        private System.Windows.Forms.CheckBox forceIp6checkBox;
        private System.Windows.Forms.CheckBox reupFullCheckBox;
        private System.Windows.Forms.CheckBox descriptionCheckBox;
        private System.Windows.Forms.TextBox drkKeyTextBox;
        private System.Windows.Forms.CheckBox drkCheckBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox numberOfFriendRequestTextBox;
        private System.Windows.Forms.CheckBox showFbVersionCheckBox;
        private System.Windows.Forms.TextBox drkDomainTextbox;
        private System.Windows.Forms.CheckBox nvrByDeviceCheckBox;
        private System.Windows.Forms.GroupBox startButtonGroupBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage devicesTabPage;
        private System.Windows.Forms.TabPage settingTabPage;
        private System.Windows.Forms.GroupBox changeSimGroupBox;
        private System.Windows.Forms.GroupBox orderGroupBox;
        private System.Windows.Forms.GroupBox controlGroupBox;
        private System.Windows.Forms.GroupBox networkSimGroupBox;
        private System.Windows.Forms.Label fbInfoLabel;
        private System.Windows.Forms.CheckBox avatarByCameraCheckBox;
        private System.Windows.Forms.CheckBox reinstallFbLiteCheckBox;
        private System.Windows.Forms.TextBox reinstallFbliteTextbox;
        private System.Windows.Forms.TextBox reinstallFbCountTextBox;
        private System.Windows.Forms.CheckBox reinstallFbCheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox veriBackupCheckBox;
        private System.Windows.Forms.CheckBox forceDungMayCheckBox;
        private System.Windows.Forms.Label releaseNoteLabel;
        private System.Windows.Forms.GroupBox proxyGroupBox;
        private System.Windows.Forms.RadioButton tinsoftRadioButton;
        private System.Windows.Forms.RadioButton shopLike1RadioButton;
        private System.Windows.Forms.TextBox tinsoftTextBox;
        private System.Windows.Forms.Button removeProxyButton;
        private System.Windows.Forms.CheckBox changeAllSim2checkBox;
        private System.Windows.Forms.Timer changeSim2Timer;
        private System.Windows.Forms.Button installFacebookButton;
        private System.Windows.Forms.CheckBox forceAvatarUsCheckBox;
        private System.Windows.Forms.CheckBox sleep1MinuteCheckBox;
        private System.Windows.Forms.Button installApkFbButton;
        private System.Windows.Forms.CheckBox dausoCheckBox;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.TextBox delayTimeTextBox;
        private System.Windows.Forms.TextBox dauso12TextBox;
        private System.Windows.Forms.CheckBox dauso12CheckBox;
        private System.Windows.Forms.Button clearContactButton;
        private System.Windows.Forms.CheckBox veriByProxyCheckBox;
        private System.Windows.Forms.TextBox maxFailClearTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox delayAfterDieTextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox delayAfterRegTextBox;
        private System.Windows.Forms.TextBox tinProxyTextBox;
        private System.Windows.Forms.RadioButton tinProxyRadioButton;
        private System.Windows.Forms.TextBox allowIpTextBox;
        private System.Windows.Forms.CheckBox alwaysChangeAirplaneCheckBox;
        private System.Windows.Forms.CheckBox randomProxySimCheckBox;
        private System.Windows.Forms.TextBox maxLiveClearTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox reupRegCheckBox;
        private System.Windows.Forms.CheckBox deviceFakerPlusCheckBox;
        private System.Windows.Forms.TextBox resendTextBox;
        private System.Windows.Forms.CheckBox resendCheckBox;
        private System.Windows.Forms.CheckBox changeProxyDroidCheckBox;
        private System.Windows.Forms.CheckBox changeProxyByCollegeCheckBox;
        private System.Windows.Forms.CheckBox serverOnlineCheckBox;
        private System.Windows.Forms.TextBox serverPathTextBox;
        private System.Windows.Forms.CheckBox forcePortraitCheckBox;
        private System.Windows.Forms.ToolStripMenuItem getInfoToolStripMenuItem;
        private System.Windows.Forms.CheckBox veriNvrBenNgoaiCheckBox;
        private System.Windows.Forms.RadioButton tmProxyRadioButton;
        private System.Windows.Forms.TextBox tmProxyTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox locationProxyTextBox;
        private System.Windows.Forms.ToolStripMenuItem change2Ip4;
        private System.Windows.Forms.ToolStripMenuItem change2Ip6;
        private System.Windows.Forms.Button button4G;
        private System.Windows.Forms.Button button3G;
        private System.Windows.Forms.CheckBox inputStringMailCheckBox;
        private System.Windows.Forms.Button offAllbutton;
        private System.Windows.Forms.Button onAllbutton;
        private System.Windows.Forms.Timer startStoptimer;
        private System.Windows.Forms.CheckBox randPhone2TypecheckBox;
        private System.Windows.Forms.Timer randPhone2Typetimer;
        private System.Windows.Forms.CheckBox openfblitecheckBox;
        private System.Windows.Forms.CheckBox moiFbLitecheckBox;
        private System.Windows.Forms.CheckBox removeAccFblitecheckBox;
        private System.Windows.Forms.Label accMoilabel;
        private System.Windows.Forms.CheckBox removeProxyCheckBox;
        private System.Windows.Forms.CheckBox clearAccSettingcheckBox;
        private System.Windows.Forms.CheckBox addAccSettingCheckBox;
        private System.Windows.Forms.CheckBox forceReupContactCheckBox;
        private System.Windows.Forms.CheckBox randomNewContactCheckBox;
        private System.Windows.Forms.CheckBox randomOldContactCheckBox;
        private System.Windows.Forms.CheckBox carryCodecheckBox;
        private System.Windows.Forms.CheckBox openMessengerCheckBox;
        private System.Windows.Forms.TextBox onOffSimCountTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Timer countAccMoiTimer;
        private System.Windows.Forms.GroupBox emailTypeGroupBox;
        private System.Windows.Forms.RadioButton outlookDomainRadioButton;
        private System.Windows.Forms.RadioButton outlookRadioButton;
        private System.Windows.Forms.RadioButton hotmailRadioButton;
        private System.Windows.Forms.RadioButton dtProxyRadioButton;
        private System.Windows.Forms.TextBox dtProxyTextBox;
        private System.Windows.Forms.CheckBox sockDroidCheckBox;
        private System.Windows.Forms.CheckBox set2faWebCheckBox;
        private System.Windows.Forms.Label phoneTypeLabel;
        private System.Windows.Forms.ToolStripMenuItem viewScreenToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox numberClearAccSettingTextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox percentVeriFailTextBox;
        private System.Windows.Forms.Timer checkVeriTimer;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.CheckBox checkDieStopCheckBox;
        private System.Windows.Forms.TextBox fixDuoiMailTextBox;
        private System.Windows.Forms.CheckBox fixDuoiMailCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton tempmailLolradioButton;
        private System.Windows.Forms.RadioButton generatorEmailradioButton;
        private System.Windows.Forms.RadioButton dichvuGmailradioButton;
        private System.Windows.Forms.RadioButton sellGmailradioButton;
        private System.Windows.Forms.RadioButton fakemailgeneratorradioButton;
        private System.Windows.Forms.RadioButton fakeEmailradioButton;
        private System.Windows.Forms.Timer resetDuoiMailtimer;
        private System.Windows.Forms.TextBox activeDuoiMailtextBox;
        private System.Windows.Forms.CheckBox randomDuoiMailcheckBox;
        private System.Windows.Forms.CheckBox getDuoiMailFromServercheckBox;
        private System.Windows.Forms.CheckBox luuDuoiMailcheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox randomRegVericheckBox;
        private System.Windows.Forms.RadioButton MailOtpRadioButton;
        private System.Windows.Forms.RadioButton dichvugmail2radioButton;
        private System.Windows.Forms.CheckBox regByGmailcheckBox;
        private System.Windows.Forms.CheckBox laymailkhaccheckBox;
        private System.Windows.Forms.Timer timerAvailableSellGmail;
        private System.Windows.Forms.RadioButton gmail30minradioButton;
        private System.Windows.Forms.CheckBox veriAccRegMailcheckBox;
        private System.Windows.Forms.RadioButton sellGmailServerradioButton;
        private System.Windows.Forms.CheckBox randomIp46CheckBox;
        private System.Windows.Forms.CheckBox veriaccgmailCheckBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox showInfoDevicecheckBox;
        private System.Windows.Forms.CheckBox removeProxy2checkBox;
        private System.Windows.Forms.CheckBox forceSellgmailcheckBox;
        private System.Windows.Forms.ToolStripMenuItem getxmltoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureScreentoolStripMenuItem;
        private System.Windows.Forms.CheckBox set2faLoai2checkBox;
        private System.Windows.Forms.CheckBox moiKatanacheckBox;
        private System.Windows.Forms.CheckBox storeAccMoicheckBox;
        private System.Windows.Forms.CheckBox thoatGmailcheckBox;
        private System.Windows.Forms.CheckBox thoatOtpcheckBox;
        private System.Windows.Forms.CheckBox clearAllAccSettingcheckBox;
        private System.Windows.Forms.CheckBox reinstallSaudiecheckBox;
        private System.Windows.Forms.CheckBox rebootFakerpluscheckBox;
        private System.Windows.Forms.CheckBox installfblitecheckBox;
        private System.Windows.Forms.CheckBox installMissingMessengercheckBox;
        private System.Windows.Forms.Button UninstallMessenger;
        private System.Windows.Forms.Button uninstallbusinessbutton;
        private System.Windows.Forms.CheckBox moiBusinesscheckBox;
        private System.Windows.Forms.CheckBox moiKhong2facheckBox;
        private System.Windows.Forms.CheckBox getProyx20timecheckBox;
        private System.Windows.Forms.CheckBox randomOnOffSimcheckBox;
        private System.Windows.Forms.CheckBox forceMoiThanhCongcheckBox;
        private System.Windows.Forms.TextBox startWithtextBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox soLanChangeIptextBox;
        private System.Windows.Forms.Button pushFileChargerbutton;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox changer60checkBox;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.CheckBox tatcaiconlaicheckBox;
        private System.Windows.Forms.CheckBox restartAfterchangecheckBox;
        private System.Windows.Forms.CheckBox openFbByDeepLinkcheckBox1;
        private System.Windows.Forms.TextBox serverCacheMailTextbox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.CheckBox forceVeriAccRegBMailcheckBox;
        private System.Windows.Forms.TextBox fixPasswordtextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox fixPasswordCheckbox;
        private System.Windows.Forms.RadioButton gmailOtpRadioButton;
        private System.Windows.Forms.TextBox errortextBox;
        private System.Windows.Forms.TextBox FastProxyTextBox;
        private System.Windows.Forms.RadioButton fastProxyRadioButton;
        private System.Windows.Forms.ToolStripMenuItem rebootCmdtoolStripMenuItem;
        private System.Windows.Forms.Label accDieCapchalabel;
        private System.Windows.Forms.Label otplabel;
        private System.Windows.Forms.Button setFastProxybutton;
        private System.Windows.Forms.RadioButton superTeamRadioButton;
        private System.Windows.Forms.CheckBox showIpcheckBox;
        private System.Windows.Forms.RadioButton zuesProxyradioButton;
        private System.Windows.Forms.RadioButton impulseradioButton;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.RadioButton gmail48hradioButton;
        private System.Windows.Forms.CheckBox forceGmailcheckBox;
        private System.Windows.Forms.TextBox zuesProxyKeytextBox;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox nameVnUscheckBox;
        private System.Windows.Forms.CheckBox nameUsVncheckBox;
        private System.Windows.Forms.CheckBox doitenVncheckBox;
        private System.Windows.Forms.Button loadFBbutton;
        private System.Windows.Forms.ComboBox fbVersioncomboBox;
        private System.Windows.Forms.Button InstallFbbutton;
        private System.Windows.Forms.CheckBox moiMessengercheckBox;
        private System.Windows.Forms.CheckBox gichuTrenAvatarcheckBox;
        private System.Windows.Forms.Button randomContrybutton;
        private System.Windows.Forms.RadioButton zuesProxy4G;
        private System.Windows.Forms.Label Country;
        private System.Windows.Forms.TextBox countrytextBox;
        private System.Windows.Forms.CheckBox name3wordcheckBox;
        private System.Windows.Forms.RadioButton s5ProxyradioButton;
        private System.Windows.Forms.CheckBox proxy4GcheckBox;
        private System.Windows.Forms.Button waitAndTapbutton;
        private System.Windows.Forms.TextBox testTaptextBox;
        private System.Windows.Forms.TextBox xmltextBox;
        private System.Windows.Forms.CheckBox catProxySauVericheckBox;
        private System.Windows.Forms.CheckBox superProxycheckBox;
        private System.Windows.Forms.CheckBox logProxycheckBox;
        private System.Windows.Forms.CheckBox proxyFromServercheckBox;
        private System.Windows.Forms.CheckBox moiTruocProxycheckBox;
        private System.Windows.Forms.CheckBox randomProxySim2checkBox;
        private System.Windows.Forms.CheckBox moiAccRegThanhCongcheckBox;
        private System.Windows.Forms.Button GetRealPhonebutton;
        private System.Windows.Forms.CheckBox epMoiThanhCongcheckBox;
        private System.Windows.Forms.CheckBox moiKatanaNhanhcheckBox;
        private System.Windows.Forms.ToolStripMenuItem Call101toolStripMenuItem;
        private System.Windows.Forms.CheckBox chuyenQuaMoiKatanacheckBox;
        private System.Windows.Forms.CheckBox moiBusinessNhanhcheckBox;
        private System.Windows.Forms.CheckBox randomMoicheckBox;
        private System.Windows.Forms.CheckBox reinstallBusinesscheckBox;
        private System.Windows.Forms.CheckBox getSellMailCheckbox;
        private System.Windows.Forms.CheckBox getDvgmcheckBox;
        private System.Windows.Forms.CheckBox getSuperMailcheckBox;
        private System.Windows.Forms.CheckBox chuyenHotmailNhanhcheckBox;
        private System.Windows.Forms.CheckBox getHotmailKieumoicheckBox;
        private System.Windows.Forms.CheckBox chuyenQuaveriGmailcheckBox;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.CheckBox randomVersioncheckBox;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.CheckBox randomVersionSaudiecheckBox;
        private System.Windows.Forms.CheckBox chạyDoiTenDemcheckBox;
        private System.Windows.Forms.ToolStripMenuItem napThetoolStripMenuItem;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox mathetextBox;
        private System.Windows.Forms.CheckBox fastcheckBox;
        private System.Windows.Forms.CheckBox EpAccMoicheckBox;
        private System.Windows.Forms.CheckBox choPutOtpcheckBox;
        private System.Windows.Forms.CheckBox boAccNhapMailSaicheckBox;
        private System.Windows.Forms.RadioButton oneSecradioButton;
        private System.Windows.Forms.CheckBox getTrustMailcheckBox;
        private System.Windows.Forms.CheckBox otpVandongcheckBox;
        private System.Windows.Forms.RadioButton tunProxyradioButton;
        private System.Windows.Forms.CheckBox changePhoneNumbercheckBox;
        private System.Windows.Forms.CheckBox findPhonecheckBox;
        private System.Windows.Forms.CheckBox randomVersionAfterverifailcheckBox;
        private System.Windows.Forms.CheckBox proxyCMDcheckBox;
        private System.Windows.Forms.CheckBox chayuploadContactcheckBox;
        private System.Windows.Forms.CheckBox chayepdanbacheckBox;
        private System.Windows.Forms.CheckBox proxyWificheckBox;
        private System.Windows.Forms.CheckBox p1ProxycheckBox;
        private System.Windows.Forms.CheckBox p3ProxycheckBox;
        private System.Windows.Forms.Button updateFbVersionbutton;
        private System.Windows.Forms.CheckBox UsLanguagecheckBox;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.TextBox ipRangeLantextBox;
        private System.Windows.Forms.CheckBox runBoxLancheckBox;
        private System.Windows.Forms.CheckBox coverNewcheckBox;
        private System.Windows.Forms.CheckBox uploadContactNewCheckbox;
        private System.Windows.Forms.RadioButton wwProxyradioButton;
        private System.Windows.Forms.CheckBox proxyFromLocalcheckBox;
        private System.Windows.Forms.CheckBox proxyKeycheckBox;
        private System.Windows.Forms.CheckBox InitialPhonecheckBox;
        private System.Windows.Forms.CheckBox namServercheckBox;
        private System.Windows.Forms.CheckBox clearAccsettingsauregcheckBox;
        private System.Windows.Forms.CheckBox uuTienChay4GcheckBox;
        private System.Windows.Forms.CheckBox checkAllcheckBox;
        private System.Windows.Forms.CheckBox chuyenKeyVnicheckBox;
        private System.Windows.Forms.CheckBox rootRomcheckBox;
        private System.Windows.Forms.CheckBox checkTopProxycheckBox;
        private System.Windows.Forms.CheckBox checkVericheckBox;
        private System.Windows.Forms.CheckBox chaydocheckBox;
        private System.Windows.Forms.CheckBox boquaProxyVncheckBox;
        private System.Windows.Forms.CheckBox tamdungKiemTraAvatarcheckBox;
        private System.Windows.Forms.TextBox soLanLayMailtextBox;
        private System.Windows.Forms.CheckBox thuesimcheckBox;
        private System.Windows.Forms.CheckBox otpCheapcheckBox;
        private System.Windows.Forms.CheckBox p2ProxycheckBox;
        private System.Windows.Forms.Timer ResetStatustimer;
        private System.Windows.Forms.CheckBox dvgmcheckVipBox;
        private System.Windows.Forms.CheckBox docMailEducheckBox;
        private System.Windows.Forms.CheckBox nghi1phutsaudiecheckBox;
        private System.Windows.Forms.CheckBox nghi5phutsaudiecheckBox;
        private System.Windows.Forms.CheckBox chuyenquanvrcheckBox;
        private System.Windows.Forms.CheckBox rootRom11checkBox;
        private System.Windows.Forms.CheckBox thuesimVipcheckBox;
        private System.Windows.Forms.CheckBox dvgmNormalcheckBox;
        private System.Windows.Forms.CheckBox changeProxy2P1checkBox;
        private System.Windows.Forms.CheckBox allcheckBox;
        public System.Windows.Forms.CheckBox sptLocalcheckBox;
        private System.Windows.Forms.CheckBox sptVipcheckBox;
        private System.Windows.Forms.CheckBox shopgmailLocalcheckBox;
        private System.Windows.Forms.TextBox virtualDevicetextBox;
        private System.Windows.Forms.CheckBox getMailcheckBox;
        private System.Windows.Forms.TextBox maxMailtextBox;
        private System.Windows.Forms.Label maxMaillabel;
        private System.Windows.Forms.Button setMaxMailbutton;
        private System.Windows.Forms.CheckBox hvlgmailcheckBox;
        private System.Windows.Forms.CheckBox proxySharecheckBox;
        private System.Windows.Forms.CheckBox giulaiportcheckBox;
        private System.Windows.Forms.CheckBox chuyenQuaHotmailcheckBox;
        private System.Windows.Forms.Label statusSpeedlabel;
        private System.Windows.Forms.CheckBox chuyenVeri4gcheckBox;
        private System.Windows.Forms.CheckBox android11checkBox;
        private System.Windows.Forms.CheckBox forcestopDiecheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn device;
        private System.Windows.Forms.DataGridViewTextBoxColumn accInHalfHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalSuccess;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RunningCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sim;
        private System.Windows.Forms.DataGridViewTextBoxColumn publicIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealSim;
        private System.Windows.Forms.DataGridViewTextBoxColumn fbVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn turnSim;
        private System.Windows.Forms.DataGridViewTextBoxColumn VeriStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn simstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Proxy;
        private System.Windows.Forms.DataGridViewCheckBoxColumn verifyCol;
        private System.Windows.Forms.TextBox wifiListtextBox;
        private System.Windows.Forms.Button loadWifiListbutton;
        private System.Windows.Forms.CheckBox randomWificheckBox;
        private System.Windows.Forms.CheckBox forceChangeWificheckBox;
        private System.Windows.Forms.CheckBox randomProxyDatacheckBox;
        private System.Windows.Forms.Timer UpdateStatusSheettimer;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Timer InforMailtimer;
        private System.Windows.Forms.CheckBox forceRebootAfterClearcheckBox;
        private System.Windows.Forms.Button HideRootbutton;
        private System.Windows.Forms.CheckBox tuongtacnhecheckBox;
        private System.Windows.Forms.RadioButton A13radioButton;
        private System.Windows.Forms.RadioButton A11radioButton;
        private System.Windows.Forms.RadioButton A10radioButton;
        private System.Windows.Forms.RadioButton A9radioButton;
        private System.Windows.Forms.GroupBox romgroupBox;
        private System.Windows.Forms.Button Rombutton;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.ToolStripMenuItem viewDevicetoolStripMenuItem;
        private System.Windows.Forms.CheckBox getDecisioncheckBox;
        private System.Windows.Forms.CheckBox checkChangeIpcheckBox;
        private System.Windows.Forms.Timer ScanDevicetimer;
        private System.Windows.Forms.CheckBox veriDirectGmailcheckBox;
    }
}

