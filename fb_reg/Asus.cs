using AE.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fb_reg
{
    class Asus
    {
        // Asus
        //int totalRun = 0;
        //int fail = 0;
        //int regOk = 0;
        //int noVerified = 0;
        //public static string FAIL = "fail";
        //Bitmap FACEBOOK_OPEN;
        //Bitmap NEXT_BTN_IMAGE;
        //Bitmap NOT_NOW;
        //Bitmap DEVICE_FAKER_APP;


        //Bitmap SIGN_UP_WITH_EMAIL;
        //Bitmap LOCK_NICK;
        //Bitmap NO_CONNECTION;
        //Bitmap AIR_PLANE_MODE;
        //Bitmap MISSING_NAME;
        //Bitmap UPLOAS_CONTACT;
        //Bitmap UPLOAS_CONTACT2;
        //Bitmap SELECT_YOUR_NAME;
        //Bitmap ENTER_CODE;
        //Bitmap SELECT_CONFIRM_BY_EMAIL;

        //Bitmap FEB;
        //List<string> devices = new List<string>();
        //string selectedDevice;
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    string imageFolder = "img/" + Constant.LANGUAGE + "/";

        //    FACEBOOK_OPEN = (Bitmap)Image.FromFile(imageFolder + "check_facebook_open_ok.png");
        //    NEXT_BTN_IMAGE = (Bitmap)Image.FromFile(imageFolder + "next_btn_image.png");
        //    NOT_NOW = (Bitmap)Image.FromFile(imageFolder + "NotNowStoreInfo.png");
        //    DEVICE_FAKER_APP = (Bitmap)Bitmap.FromFile(imageFolder + "DeviceFaker.png");


        //    SIGN_UP_WITH_EMAIL = (Bitmap)Bitmap.FromFile(imageFolder + "sign_up_with_email.png");
        //    LOCK_NICK = (Bitmap)Bitmap.FromFile(imageFolder + "lock_nick.png");
        //    NO_CONNECTION = (Bitmap)Bitmap.FromFile(imageFolder + "no_internet.png");
        //    AIR_PLANE_MODE = (Bitmap)Bitmap.FromFile(imageFolder + "airplane_mode.png");
        //    MISSING_NAME = (Bitmap)Bitmap.FromFile(imageFolder + "missing_name.png");
        //    UPLOAS_CONTACT = (Bitmap)Bitmap.FromFile(imageFolder + "upload_contact.png");
        //    UPLOAS_CONTACT2 = (Bitmap)Bitmap.FromFile(imageFolder + "upload_contact2.png");
        //    SELECT_YOUR_NAME = (Bitmap)Bitmap.FromFile(imageFolder + "select_your_name.png");
        //    ENTER_CODE = (Bitmap)Bitmap.FromFile(imageFolder + "enter_code.png");
        //    SELECT_CONFIRM_BY_EMAIL = (Bitmap)Bitmap.FromFile(imageFolder + "confirm_by_email.png");
        //    FEB = (Bitmap)Bitmap.FromFile(imageFolder + "feb.png");

        //    GoogleSheet.Initial();
        //    var listDevices = KAutoHelper.ADBHelper.GetDevices();
        //    //if (listDevices != null && listDevices.Count > 0)
        //    //{
        //    //    selectedDevice = listDevices[listDevices.Count - 1];
        //    //    for (int i = 0; i < listDevices.Count; i++)
        //    //    {
        //    //        deviceIdBox.Items.Add(listDevices[i]);
        //    //    }
        //    //    deviceIdBox.SelectedIndex = listDevices.Count - 1;
        //    //}
        //}

        //private void CloneBtn_Click(object sender, EventArgs e)
        //{
        //    //selectedDevice = (string)deviceIdBox.SelectedItem;
        //    Task t = new Task(() =>
        //    {

        //        while (true)
        //        {
        //            totalRun++;
        //            var watch = System.Diagnostics.Stopwatch.StartNew();

        //            //int size = devices.Count;

        //            Autoclone(selectedDevice);
        //            Thread.Sleep(5000);
        //            watch.Stop();
        //            long second = watch.ElapsedMilliseconds / 1000;
        //            Console.WriteLine($"Execution Time: {second} ms");
        //            //label3.Invoke(new MethodInvoker(() =>
        //            //{
        //            //    string status = "totalReg:" + totalRun + " --regOk:" + regOk + " --regFail:" + fail + "  --NoVerified:" + noVerified;
        //            //    label3.Text = status;
        //            //}));
        //        }


        //    });
        //    t.Start();
        //}

        //void Autoclone(string deviceID)
        //{
        //    try
        //    {
        //        if (!ChangeDeviceInfo(deviceID))
        //        {
        //            fail++;
        //            return;
        //        }

        //        ChangeIp(deviceID);
        //        OpenFacebookApp(deviceID);

        //        //tao tai khoan moi
        //        Utility.Log("Tap to new account button");
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.0, 89.0);
        //        Delay(1);

        //        // next
        //        Next(deviceID);
        //        Thread.Sleep(300);
        //        ///////////// cho phép
        //        Utility.Log("Allow all permission");
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.3, 57.2);
        //        Thread.Sleep(300);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 78.3, 57.2);
        //        Thread.Sleep(1000);

        //        //string prePass = InputName(deviceID);
        //        // Random gender
        //        Random r = new Random();
        //        int rand = r.Next(100);
        //        string gender = Constant.MALE;
        //        if (rand > 50)
        //        {
        //            gender = Constant.FEMALE;
        //        }
        //        else
        //        {
        //            gender = Constant.MALE;
        //        }

        //        string prePass = InputNameEnglish(deviceID, gender);
        //        ////////////////// ngay tham nam sinh
        //        Utility.Log("Input Birthday");
        //        InputBirthday(deviceID);
        //        /////////////
        //        Next(deviceID);
        //        Thread.Sleep(1000);

        //        Utility.Log("Input gender");
        //        InputGender(deviceID, gender);
        //        Next(deviceID);
        //        Thread.Sleep(300);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 35.2);

        //        string phone = Utility.GeneratePhoneNumber();
        //        KAutoHelper.ADBHelper.InputText(deviceID, ConvertToUnsign(phone));

        //        Next(deviceID);
        //        Thread.Sleep(2000);
        //        string password = prePass + "2A01474";

        //        string pass2 = Regex.Match(password, "[\\w]+").ToString();
        //        Utility.Log("Input password:" + password);
        //        KAutoHelper.ADBHelper.InputText(deviceID, pass2);

        //        Next(deviceID);

        //        Thread.Sleep(2000);
        //        // Dang ky
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.0, 53.5);
        //        Utility.Log("Process register information account: waiting 18s");

        //        Thread.Sleep(18000);

        //        // Check lock nick
        //        if (checkLock(deviceID))
        //        {
        //            fail++;
        //            return;
        //        }
        //        if (CheckImage(deviceID, SELECT_YOUR_NAME))
        //        {
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.7, 47.8);
        //            Thread.Sleep(1000);
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.4, 64.4);
        //            Thread.Sleep(8000);
        //        }

        //        // popup khong luu thong tin dang nhap
        //        Utility.Log("Check Notnow");
        //        if (CheckImage(deviceID, NOT_NOW))
        //        {
        //            Utility.Log("Have popup");

        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.9, 64.2);
        //            Thread.Sleep(1000);
        //            // tiep tuc
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 75.5);
        //            Thread.Sleep(1000);
        //        }
        //        Utility.Log("Check upload contact 2");
        //        if (CheckImage(deviceID, UPLOAS_CONTACT2))
        //        {
        //            Utility.Log("Upload contact 2 - Tap continue");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.2, 85.3);
        //            Thread.Sleep(2000);
        //            Utility.Log("Tap allow");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 83.1, 71.9);
        //            Thread.Sleep(2000);
        //        }
        //        string Hotmail = "";
        //        string[] TKMK;
        //        if (CheckImage(deviceID, SELECT_CONFIRM_BY_EMAIL))
        //        {
        //            Utility.Log("Tap confirm by email");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 27.3, 70.6);
        //            Thread.Sleep(2000);
        //            Utility.Log("Tap text box email address");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.5, 24.9);
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.5, 24.9);

        //            //// Lay dia chi email put vao
        //            Hotmail = CheckLive();
        //            TKMK = Hotmail.Split('|');

        //            Thread.Sleep(300);
        //            Utility.Log("Put email:" + TKMK[0]);
        //            KAutoHelper.ADBHelper.InputText(deviceID, TKMK[0]);

        //            Thread.Sleep(3000);
        //            Utility.Log("Tap button continue");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.6, 32.9);
        //        }
        //        else
        //        {
        //            Utility.Log("Check Notnow");
        //            if (CheckImage(deviceID, NOT_NOW))
        //            {
        //                Utility.Log("Have popup");

        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 46.9, 64.2);
        //                Thread.Sleep(1000);
        //                // tiep tuc
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.9, 75.5);
        //                Thread.Sleep(1000);
        //            }
        //            Utility.Log("Check upload contact 2");
        //            if (CheckImage(deviceID, UPLOAS_CONTACT2))
        //            {
        //                Utility.Log("Upload contact 2 - Tap continue");
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.2, 85.3);
        //                Thread.Sleep(2000);
        //                Utility.Log("Tap allow");
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 83.1, 71.9);
        //                Thread.Sleep(2000);
        //            }

        //            if (CheckImage(deviceID, SELECT_CONFIRM_BY_EMAIL))
        //            {
        //                Utility.Log("Tap confirm by email");
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 27.3, 70.6);
        //                Thread.Sleep(2000);
        //                Utility.Log("Tap text box email address");
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.5, 24.9);
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.5, 24.9);

        //                //// Lay dia chi email put vao
        //                Hotmail = CheckLive();
        //                TKMK = Hotmail.Split('|');
        //                //KAutoHelper.ADBHelper.Tap(deviceID, 162, 454);
        //                Thread.Sleep(300);
        //                Utility.Log("Put email:" + TKMK[0]);
        //                KAutoHelper.ADBHelper.InputText(deviceID, TKMK[0]);

        //                Thread.Sleep(3000);
        //                Utility.Log("Tap button continue");
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.6, 32.9);
        //            }
        //            else
        //            {
        //                Utility.Log("Can not go to screen confirm");
        //                fail++;
        //                return;
        //            }
        //        }


        //        Utility.Log("Check enter screen enter code - if not - return");
        //        if (!CheckImage(deviceID, ENTER_CODE))
        //        {
        //            Utility.Log("Tap text box email address");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 17.5, 24.9);
        //            Thread.Sleep(1000);
        //            Utility.Log("Put email:" + TKMK[0]);
        //            KAutoHelper.ADBHelper.InputText(deviceID, TKMK[0]);

        //            Thread.Sleep(3000);
        //            Utility.Log("Tap button continue");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 47.6, 32.9);
        //            Thread.Sleep(6000);
        //            if (!CheckImage(deviceID, ENTER_CODE))
        //            {
        //                Utility.Log("Can not enter screen enter code");
        //                fail++;

        //                return;
        //            }
        //        }

        //        string code = GetHotmailCode(Hotmail);

        //        if (code == FAIL)
        //        {
        //            // Resend mail
        //            Utility.Log("Get verify code fail");
        //            Utility.StoreInfoNoVerify(deviceID, password, Hotmail);

        //            noVerified++;
        //            return;
        //        }
        //        Utility.Log("tap to confirm code");
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 32.2, 31.0);
        //        Thread.Sleep(1000);
        //        Utility.Log("Input code:" + code);
        //        KAutoHelper.ADBHelper.InputText(deviceID, code);

        //        Thread.Sleep(1000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 49.5, 40.3);
        //        Thread.Sleep(4000);
        //        Utility.Log("Check upload contact 3");
        //        if (CheckImage(deviceID, UPLOAS_CONTACT))
        //        {
        //            Utility.Log("Upload contact 3");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.2, 85.3);
        //            Thread.Sleep(4000);
        //        }
        //        // Skip upload photo
        //        Utility.Log("Skip upload photo ");
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.9, 7.7);
        //        Thread.Sleep(6000);
        //        // Add your friend
        //        Utility.Log("Check upload contact 3");
        //        if (CheckImage(deviceID, UPLOAS_CONTACT))
        //        {
        //            Utility.Log("Upload contact 3");
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.2, 85.3);
        //            Thread.Sleep(4000);
        //        }

        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 54.8, 95.5); //3s
        //        Thread.Sleep(6000);
        //        // Invite your friend
        //        Utility.Log("Invite friend tap");
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.8, 95.9);
        //        Thread.Sleep(4000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 82.3, 55.1);
        //        Thread.Sleep(4000);
        //        // Save login info
        //        Utility.Log("Save login info");
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 73.5, 96.8);
        //        // Check lock nick
        //        if (checkLock(deviceID))
        //        {
        //            fail++;
        //            return;
        //        }

        //        // Done
        //        Utility.Log("Store information account facebook");
        //        regOk++;
        //        Utility.StoreInfo(deviceID, password, Hotmail);
        //        Thread.Sleep(1000);
        //    }
        //    catch (Exception e)
        //    {
        //        Utility.Log("exception:" + e.Message);
        //    }
        //}

        //public bool checkLock(string deviceID)
        //{
        //    Utility.Log("Check account lock");
        //    return CheckImage(deviceID, LOCK_NICK);
        //}

        //public bool CheckConnection(string deviceID)
        //{
        //    var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //    var hasLock = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, NO_CONNECTION);// so sanh hinh anh
        //    if (hasLock != null)// kiem tra
        //    {
        //        Utility.Log("Lock nick - continue");
        //        return true;
        //    }
        //    return false;
        //}
        //// Change device info
        //public bool ChangeDeviceInfo(string deviceID)
        //{
        //    try
        //    {
        //        Utility.Log("Change device Information");
        //        QLong.Phone.Về_Màn_Hình_Chính(deviceID);
        //        Thread.Sleep(500);

        //        Utility.Log("Open fake setting app");
        //        QLong.Phone.Mở_Ứng_Dụng(deviceID, "com.devicefaker.plus");

        //        bool check = Wait(deviceID, 30, DEVICE_FAKER_APP);
        //        if (!check)
        //        {
        //            return false;
        //        }
        //        // TODO
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 52.2, 30.3);

        //        Thread.Sleep(1000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 57.0, 42.1);
        //        Thread.Sleep(2000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 32.4, 92.1);
        //        Thread.Sleep(1000);
        //        Utility.Log("Finish change device info");
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        //public void ChangeIp(string deviceID)
        //{
        //    try
        //    {
        //        QLong.Phone.Về_Màn_Hình_Chính(deviceID);
        //        Thread.Sleep(2200);
        //        Utility.Log("Turn on/off airplan mode");
        //        KAutoHelper.ADBHelper.Swipe(deviceID, 500, 1, 500, 700, 500);
        //        Thread.Sleep(1500);
        //        //KAutoHelper.ADBHelper.Swipe(deviceID, 500, 1, 500, 700, 500);
        //        //Thread.Sleep(2000);
        //        //KAutoHelper.ADBHelper.Tap(deviceID, 966, 177);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.1, 17.9);
        //        Thread.Sleep(3000);
        //        //KAutoHelper.ADBHelper.Tap(deviceID, 966, 177);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.1, 17.9);
        //        Thread.Sleep(2000);
        //        QLong.Phone.Về_Màn_Hình_Chính(deviceID);
        //        Thread.Sleep(2000);
        //        // Check airplan mode
        //        if (CheckAirPlanmode(deviceID))
        //        {
        //            KAutoHelper.ADBHelper.Swipe(deviceID, 500, 1, 500, 700, 500);
        //            Thread.Sleep(500);
        //            //KAutoHelper.ADBHelper.Swipe(deviceID, 500, 1, 500, 700, 500);

        //            //Thread.Sleep(1000);
        //            //KAutoHelper.ADBHelper.Tap(deviceID, 966, 177);
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.1, 17.9);
        //            Thread.Sleep(2000);
        //            QLong.Phone.Về_Màn_Hình_Chính(deviceID);
        //        }
        //    }
        //    catch (Exception) { }
        //}

        //public void OpenFacebookApp(string deviceID)
        //{
        //    Utility.Log("Delete cache data on facebook");
        //    QLong.Phone.XóaDữLiệu(deviceID, "com.facebook.katana");
        //    Thread.Sleep(500);

        //    QLong.Phone.Mở_Ứng_Dụng(deviceID, "com.facebook.katana");

        //    CheckFacebookOpenOk(deviceID);
        //    Thread.Sleep(1000);
        //}

        //public static string ConvertToUnsign(string text)
        //{
        //    string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
        //                                "đ",
        //                                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
        //                                "í","ì","ỉ","ĩ","ị",
        //                                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
        //                                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
        //                                "ý","ỳ","ỷ","ỹ","ỵ",};
        //    string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
        //                                "d",
        //                                "e","e","e","e","e","e","e","e","e","e","e",
        //                                "i","i","i","i","i",
        //                                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
        //                                "u","u","u","u","u","u","u","u","u","u","u",
        //                                "y","y","y","y","y",};
        //    for (int i = 0; i < arr1.Length; i++)
        //    {
        //        text = text.Replace(arr1[i], arr2[i]);
        //        text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
        //    }
        //    return text;
        //}
        //public string InputName(string deviceID)
        //{
        //    string[] lastNameArr = {"Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Đặng", "Bùi",
        //    "Đỗ", "Hồ", "Ngô", "Dương", "Lý", "Cao", "Chu", "Chung", "Chương", "Cụ",
        //    "Diệp", "Đậu", "Đinh", "Điền", "Đoàn", "Giang", "Giao", "Giáp", "Gia", "Hà",
        //        "Hồng", "Hứa", "Khương",
        //    "Khổng", "Kim", "Kiều", "La", "Lê", "Lô", "Lưu", "Lương", "Mai", "Mạc",
        //    "Nguyên", "Nông", "Ông", "Phụng", "Phương", "Quách", "Thái", "Tiêu", "Triều", "Trương",
        //    "Trịnh", "Tô", "Tăng", "Tạ", "Tống", "Tu", "Võ", "Vương"};

        //    string[] firstNameArr = { "An", "Anh",
        //    "Bình", "Bích", "Băng", "Bách", "Bảo", "Ca", "Chi", "Chinh", "Chiêu", "Châu",
        //    "Cát", "Cúc", "Cường", "Cẩm", "Đào", "Di", "Diễm", "Diệu", "Du", "Dung",
        //    "Duyên", "Dần", "Hiếu", "Hiền", "Hiệp", "Hoa", "Hoan", "Hoài", "Hoàn", "Huyền",
        //    "Huệ", "Hân", "Hoa", "Hương", "Hường", "Hạnh", "Hải", "Hào", "Hậu", "Hằng",
        //    "Hợp", "Khải", "Khánh", "Khuyên", "Khuê", "Khanh", "Khê", "Khôi", "Lam", "Lan",
        //    "Linh", "Liên", "Liễu", "Loan", "Lâm", "Lộc", "Lợi", "Mi", "Minh", "Miên",
        //    "Mỹ", "Mẫn", "Nga", "Nghi", "Nguyệt", "Nga", "Ngân", "Ngon", "Ngọc", "Nhi",
        //    "Nhiên", "Nhung", "Nhân", "Nhẫn", "Nhã", "Như", "Nương", "Nữ", "Oanh", "Phi",
        //    "Phong" };
        //    Random r = new Random();
        //    int selectLastNameIndex = r.Next(1, 60);
        //    int select12 = r.Next(1, 82);
        //    int selectName = r.Next(1, 82);
        //    int selectpass = r.Next(1, 60);
        //    int selectpass2 = r.Next(1, 82);
        //    string ho = lastNameArr[selectLastNameIndex];
        //    string ten = firstNameArr[select12] + " " + firstNameArr[selectName];
        //    string selectpass1 = firstNameArr[selectpass];


        //    Utility.Log("Text: Name:" + ten);
        //    //string name = Regex.Match(ten, "[\\w]+").ToString();
        //    Thread.Sleep(3000);
        //    Utility.Log("Input text: ten :" + ten);
        //    QLong.Phone.Truyền_Chuỗi(deviceID, ten);
        //    //KAutoHelper.ADBHelper.InputText(deviceID, ConvertToUnsign(ten));

        //    Thread.Sleep(3000);

        //    // Ho
        //    KAutoHelper.ADBHelper.TapByPercent(deviceID, 67.2, 32.4);
        //    Thread.Sleep(100);
        //    KAutoHelper.ADBHelper.TapByPercent(deviceID, 67.2, 32.4);
        //    Utility.Log("Text: Ho:" + ho);
        //    string ho1 = Regex.Match(ho, "[\\w]+").ToString();
        //    Utility.Log("Input text: ten:" + ho1);
        //    QLong.Phone.Truyền_Chuỗi(deviceID, ho1);
        //    //KAutoHelper.ADBHelper.InputText(deviceID, ConvertToUnsign(ho1));
        //    // next
        //    Next(deviceID);
        //    Thread.Sleep(300);
        //    // Check error
        //    if (CheckImage(deviceID, MISSING_NAME))
        //    {
        //        Utility.Log("Input text fail: ten:" + ho1);
        //        QLong.Phone.Truyền_Chuỗi(deviceID, ho1);
        //        // next
        //        Next(deviceID);
        //        Thread.Sleep(300);
        //    }
        //    //  KAutoHelper.ADBHelper.InputText(deviceID, ten1);

        //    if (CheckImage(deviceID, SELECT_YOUR_NAME))
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.7, 47.8);
        //        Thread.Sleep(1000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.4, 64.4);
        //        Thread.Sleep(8000);
        //    }
        //    return ConvertToUnsign(selectpass1 + selectpass2);
        //}

        //public string InputNameEnglish(string deviceID, string gender)
        //{

        //    Random r = new Random();

        //    int selectLastNameIndex = r.Next(1, Constant.lastNameArr.Length - 1);

        //    int selectpass = r.Next(1, Constant.firstNameFemaleArr.Length - 1);
        //    int selectpass2 = r.Next(1, 82);
        //    string ho = Constant.lastNameArr[selectLastNameIndex];
        //    string ten = "";
        //    if (gender == Constant.MALE)
        //    {
        //        int select1Male = r.Next(1, Constant.firstNameMaleArr.Length - 1);
        //        int select2Male = r.Next(1, Constant.firstNameMaleArr.Length - 1);
        //        ten = Constant.firstNameMaleArr[select1Male] + " " + Constant.firstNameMaleArr[select2Male];
        //    }
        //    else
        //    {
        //        int select1Female = r.Next(1, Constant.firstNameFemaleArr.Length - 1);
        //        int select2Female = r.Next(1, Constant.firstNameFemaleArr.Length - 1);
        //        ten = Constant.firstNameFemaleArr[select1Female] + " " + Constant.firstNameFemaleArr[select2Female];
        //    }

        //    string selectpass1 = Constant.firstNameFemaleArr[selectpass];


        //    Utility.Log("Text: Name:" + ten);
        //    //string name = Regex.Match(ten, "[\\w]+").ToString();
        //    Thread.Sleep(3000);
        //    Utility.Log("Input text: ten :" + ten);
        //    QLong.Phone.Truyền_Chuỗi(deviceID, ten);
        //    //KAutoHelper.ADBHelper.InputText(deviceID, ConvertToUnsign(ten));

        //    Thread.Sleep(3000);

        //    // Ho
        //    KAutoHelper.ADBHelper.TapByPercent(deviceID, 66.8, 34.2);
        //    Thread.Sleep(500);
        //    KAutoHelper.ADBHelper.TapByPercent(deviceID, 66.8, 34.2);
        //    Utility.Log("Text: Ho:" + ho);
        //    string ho1 = Regex.Match(ho, "[\\w]+").ToString();
        //    Utility.Log("Input text: ten:" + ho1);
        //    QLong.Phone.Truyền_Chuỗi(deviceID, ho1);
        //    //KAutoHelper.ADBHelper.InputText(deviceID, ConvertToUnsign(ho1));
        //    // next
        //    Next(deviceID);
        //    Thread.Sleep(300);
        //    // Check error
        //    if (CheckImage(deviceID, MISSING_NAME))
        //    {
        //        Utility.Log("Input text fail: ten:" + ho1);
        //        QLong.Phone.Truyền_Chuỗi(deviceID, ho1);
        //        // next
        //        Next(deviceID);
        //        Thread.Sleep(300);
        //    }
        //    //  KAutoHelper.ADBHelper.InputText(deviceID, ten1);

        //    if (CheckImage(deviceID, SELECT_YOUR_NAME))
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.7, 47.8);
        //        Thread.Sleep(1000);
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 51.4, 64.4);
        //        Thread.Sleep(8000);
        //    }
        //    return ConvertToUnsign(selectpass1 + selectpass2);
        //}

        //public class Data
        //{

        //    public string Username { get; set; }
        //    [JsonProperty("Password")]
        //    public string Password { get; set; }
        //    [JsonProperty("Status")]
        //    public string Status { get; set; }
        //}
        //public class ResponseObject
        //{
        //    [JsonProperty("Code")]
        //    public int Code { get; set; }
        //    [JsonProperty("Message")]
        //    public string Message { get; set; }

        //    [JsonProperty("Data")]
        //    public Data[] Data { get; set; }

        //}
        //public ResponseObject GetHotMailApi()
        //{
        //    string apiGetHotMail = String.Format("http://api.maxclone.vn/api/portal/buyaccount?key={0}&type=HOTMAIL&quantity=1", Constant.MAXCLONE_KEY);
        //    var request = (HttpWebRequest)WebRequest.Create(apiGetHotMail);
        //    request.Method = "GET";
        //    request.Accept = "application/json";
        //    request.ContentType = "application/json; charset=utf-8";

        //    var response = (HttpWebResponse)request.GetResponse();
        //    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //    ResponseObject data = JsonConvert.DeserializeObject<ResponseObject>(responseString);
        //    //dynamic data = JOBject.Parse(response);

        //    Console.WriteLine(data.Message);
        //    return data;
        //}

        //public bool CheckImage(string deviceID, Bitmap imageCheck)
        //{
        //    var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //    var hasLock = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, imageCheck);// so sanh hinh anh
        //    if (hasLock != null)// kiem tra
        //    {
        //        Utility.Log("Check image - ok");
        //        return true;
        //    }
        //    return false;
        //}

        //public void InputBirthday(string deviceID)
        //{
        //    int baseY = 451;
        //    var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //    var hasLock = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, FEB);// so sanh hinh anh
        //    if (hasLock != null)// kiem tra
        //    {
        //        Utility.Log("Check image - ok");
        //        if (hasLock.Value.Y > 849)
        //        {
        //            baseY = 1140;
        //        }
        //    }
        //    Random random = new Random();
        //    int randomMonth = random.Next(400);
        //    int randomYear = random.Next(400);
        //    KAutoHelper.ADBHelper.Swipe(deviceID, 228, baseY, 228, baseY + 300 + randomMonth);
        //    Thread.Sleep(100);
        //    KAutoHelper.ADBHelper.Swipe(deviceID, 357, baseY, 357, baseY + 300 + randomMonth);
        //    Thread.Sleep(100);
        //    KAutoHelper.ADBHelper.Swipe(deviceID, 485, baseY, 485, baseY + 300);
        //    Thread.Sleep(1000);

        //    KAutoHelper.ADBHelper.Swipe(deviceID, 485, baseY, 485, baseY + 300);
        //    Thread.Sleep(100);

        //    KAutoHelper.ADBHelper.Swipe(deviceID, 485, baseY, 485, baseY + 300 + randomYear);
        //    Thread.Sleep(100);
        //    KAutoHelper.ADBHelper.Swipe(deviceID, 485, baseY, 485, baseY + 300 + randomYear);
        //    Thread.Sleep(100);
        //    KAutoHelper.ADBHelper.Swipe(deviceID, 485, baseY, 485, baseY + 300);
        //    Thread.Sleep(100);
        //}

        //public void InputGender(string deviceID, string gender)
        //{

        //    if (gender == Constant.FEMALE)
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.5, 36.6);
        //    }
        //    else
        //    {
        //        KAutoHelper.ADBHelper.TapByPercent(deviceID, 92.3, 45.4);
        //    }
        //}

        //public bool SignUpWithEmail(string deviceID)
        //{
        //    // Find sign up with email button

        //    for (int i = 0; i < 30; i++)
        //    {
        //        var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //        var next = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, SIGN_UP_WITH_EMAIL);// so sanh hinh anh
        //        if (next != null)// kiem tra
        //        {
        //            try
        //            {
        //                KAutoHelper.ADBHelper.Tap(deviceID, next.Value.X + 50, next.Value.Y + 20);// click vào hinh anh
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                Thread.Sleep(250);
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public string GetHotmailCode(string Hotmail)
        //{

        //    string[] List = Hotmail.Split('|');
        //    int KT = 0;
        //    string Code = FAIL;

        //    while (KT < 30)
        //    {
        //        try
        //        {
        //            Delay(1);
        //            using (ImapClient Imap = new ImapClient())
        //            {
        //                Imap.Connect("pop-mail.outlook.com", 993, true, false);
        //                Imap.Login(List[0], List[1]);
        //                Imap.SelectMailbox("Inbox");
        //                int SốMail = Imap.GetMessageCount();
        //                MailMessage[] mm = Imap.GetMessages(SốMail - 1, SốMail - 1, false, false);
        //                MailMessage i = mm[0];
        //                string NộiDung = i.Body;
        //                string TiêuĐề = i.Subject;
        //                if (TiêuĐề.Contains("Facebook"))
        //                {
        //                    var Item = TiêuĐề.Split(' ');
        //                    Code = Item[0];
        //                    return Code;
        //                }
        //                Imap.Dispose();
        //                if (Code != FAIL)
        //                {
        //                    break;
        //                }
        //                else
        //                {
        //                    Thread.Sleep(5000);
        //                }
        //            }
        //            KT++;
        //        }
        //        catch (Exception)
        //        {
        //            Utility.Log("Get verify code fail");
        //            return Code;
        //        }
        //    }
        //    return Code;
        //}

        //public bool Wait(string deviceID, int time, Bitmap keyImage)
        //{
        //    bool check = false;
        //    for (int i = 0; i < time; i++)
        //    {
        //        Thread.Sleep(250);
        //        var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //        var kiemtravao = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, keyImage);// so sanh hinh anh
        //        if (kiemtravao != null)// kiem tra
        //        {
        //            return true;
        //        }
        //    }
        //    return check;
        //}


        //Random n = new Random();
        //public List<string> DomainList = new List<string>() { "hopthu.top", "pastebin.win", "linkbin.win", "nuochoa.win", "tailieu.win" };
        //const string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVVBNM0123456789";

        //void Delay(int delay)
        //{
        //    while (delay > 0)
        //    {
        //        Thread.Sleep(TimeSpan.FromSeconds(1));
        //        delay--;
        //    }
        //}

        //private void CheckFacebookOpenOk(string deviceID)
        //{
        //    for (int i = 0; i < 30; i++)
        //    {
        //        Thread.Sleep(1000);
        //        var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //        var kiemtravao = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, FACEBOOK_OPEN);// so sanh hinh anh
        //        if (kiemtravao != null)// kiem tra
        //        {
        //            return;
        //        }
        //        else { }
        //    }
        //}

        //private bool CheckAirPlanmode(string deviceID)
        //{
        //    var screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //    var kiemtravao = KAutoHelper.ImageScanOpenCV.FindOutPoint(screen, AIR_PLANE_MODE);// so sanh hinh anh
        //    if (kiemtravao != null)// kiem tra
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //private void Next(string deviceID)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
        //        var next = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, NEXT_BTN_IMAGE);// so sanh hinh anh
        //        if (next != null)// kiem tra
        //        {
        //            try
        //            {
        //                KAutoHelper.ADBHelper.Tap(deviceID, next.Value.X, next.Value.Y);// click vào hinh anh
        //                return;
        //            }
        //            catch (Exception)
        //            {
        //                Thread.Sleep(250);
        //            }
        //        }
        //    }
        //}

        //public string CheckLive()
        //{
        //    while (true)
        //    {
        //        try
        //        {

        //            string userName = "";
        //            string password = "";
        //            ResponseObject res = GetHotMailApi();
        //            if (res.Code != 0)
        //            {
        //                string[] List = QLong.Cơ_Bản.Dòng_TXT("FileHotmail.txt").Split('|');
        //                userName = List[0];
        //                password = List[1];
        //            }
        //            else
        //            {
        //                userName = res.Data[0].Username;
        //                password = res.Data[0].Password;
        //            }
        //            using (ImapClient ic = new ImapClient())
        //            {
        //                ic.Connect("pop-mail.outlook.com", 993, true, false);
        //                try
        //                {
        //                    ic.Login(userName, password);
        //                    ic.Dispose();
        //                    return userName + "|" + password;
        //                }
        //                catch
        //                {
        //                    using (StreamWriter HDD = new StreamWriter("FileCLone/HotmailChết.txt", true))
        //                    {
        //                        HDD.WriteLine(userName + "|" + password);
        //                        HDD.Close();
        //                    }
        //                    ic.Dispose();
        //                }
        //            }
        //        }
        //        catch (Exception) { }

        //    }
        //}

        //private void refreshBtn_Click(object sender, EventArgs e)
        //{
        //    var listDevices = KAutoHelper.ADBHelper.GetDevices();
        //    deviceIdBox.Items.Clear();
        //    if (listDevices != null && listDevices.Count > 0)
        //    {
        //        selectedDevice = listDevices[listDevices.Count - 1];
        //        for (int i = 0; i < listDevices.Count; i++)
        //        {
        //            deviceIdBox.Items.Add(listDevices[i]);
        //        }
        //        deviceIdBox.SelectedIndex = listDevices.Count - 1;
        //    }
        //}
    }
}
