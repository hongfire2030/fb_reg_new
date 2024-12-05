using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace Regkerdong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool isStop = false;
        int dem;
        int ktthanhcong = 0;
        int j = 0;
        string pass;
        int ktthoatvong = 0;

       

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t11 = new Thread(() =>
            {
                while (true)
                 {
                        resetDcom(textBox3.Text);
                        List<Thread> list = new List<Thread>();
                    List<string> devices = new List<string>();
                    var listdevices = KAutoHelper.ADBHelper.GetDevices();
                    if (listdevices != null && listdevices.Count > 0)
                    {
                        devices = listdevices;
                        dem = listdevices.Count;
                    }
                    else { }



                    foreach (var deviceID in devices)
                    {
                        Thread t1 = new Thread(() =>
                        {
                            Auto(deviceID, dem);
                        });
                        t1.IsBackground = true;
                        t1.Start();
                        Thread.Sleep(1000);

                        list.Add(t1);

                    }
                    foreach (Thread thread2 in list)
                    {
                        thread2.Join();
                    }

    

                }


                });
                t11.IsBackground = true;
                t11.Start();
            }

        void Autoclone(string deviceID, int dem,string sdt)
        {

           // adb - s emulator - 5554 su
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " su");
          //  adb - s emulator - 5554 root
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " root");
           // adb - s emulator - 5554 remount
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " remount");
          //  adb - s emulator - 5554 shell uiautomator dump / data / local / tmp / uidump.xml
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s " + deviceID + " shell uiautomator dump /data/local/tmp/uidump.xml");


            // Tắt LD
            KAutoHelper.ADBHelper.ExecuteCMD(string.Format("E://ChangZhi//LDPlayer//dnconsole.exe quitall"));
            delay(1);
            for (int i = 0; i < 10; i++)
            {
                KAutoHelper.ADBHelper.ExecuteCMD(string.Format("E://ChangZhi//LDPlayer//dnconsole.exe remove --index " + i));
            }
            // xoa Fb Lite
            KAutoHelper.ADBHelper.ExecuteCMD("adb shell uiautomator dump /data/local/tmp/uidump.xml");
            delay(1);



            // xoa Fb Lite
            KAutoHelper.ADBHelper.ExecuteCMD("adb -s" + deviceID + " uninstall com.facebook.lite");
                delay(1);
                // cai Fb Lite
                KAutoHelper.ADBHelper.ExecuteCMD("adb -s" + deviceID + " install fb.apk");
                delay(3);

                //click fb
                if (isStop)
                    return;
                taotk(deviceID);
             

                // tieng anh
                if (isStop)
                    return;
                tienganh(deviceID);
                delay(1);

                //tao tai khoan moi
                if (isStop)
                    return;
            //creat(deviceID);
            KAutoHelper.ADBHelper.Tap(deviceID, 194, 286);
            delay(1);

                // next
                if (isStop)
                    return;
                next(deviceID);
       

                // ho
                if (isStop)
                    return;
              //  KAutoHelper.ADBHelper.TapByPercent(deviceID, 6, 35);

                string[] mang_ten = { "Bui", "Cao", "Chu", "Chung", "Chuong", "Cu", "Diep", "Doan", "Duong", "Dao", "Dang", "Dau", "Dinh", "Dien", "Doan", "Do", "Duong", "Giang", "Giao", "Giap", "Gia", "Hoang", "Huynh", "Ha", "Ha", "Ho", "Hong", "Hưa", "Khuong", "Khong", "Kim", "Kieu", "La", "La", "Le", "Lo", "Ly", "Luu", "Luong", "Mai", "Mac", "Nguyen", "Nong", "Ong", "Phan", "Phung", "Phuong", "Pham", "Quach", "Thai", "Tieu", "Trieu", "Truong", "Tran", "Trinh", "To", "Tang", "Ta", "Tong", "Tu", "Vo", "Vu", "Vuong", "An", "Anh", "Binh", "Bich", "Bang", "Bach", "Bao", "Ca", "Chi", "Chinh", "Chieu", "Chau", "Cat", "Cuc", "Cuong", "Cam", "Dao", "Di", "Diem", "Dieu", "Du", "Dung", "Duyen", "Dan", "Hieu", "Hien", "Hiep", "Hoa", "Hoan", "Hoai", "Hoan", "Huyen", "Hue", "Han", "Hoa", "Huong", "Huong", "Hanh", "Hai", "Hao", "Hau", "Hang", "Hop", "Khai", "Khanh", "Khuyen", "Khue", "Khanh", "Khe", "Khoi", "Lam", "Lan", "Linh", "Lien", "Lieu", "Loan", "Ly", "Lam", "Le", "Le", "Loc", "Loi", "Mi", "Minh", "Mien", "My", "Man", "My", "Nga", "Nghi", "Nguyen", "Nguyet", "Nga", "Ngan", "Ngon", "Ngoc", "Nhi", "Nhien", "Nhung", "Nhan", "Nhan", "Nha", "Nhu", "Nuong", "Nu", "Oanh", "Phi", "Phong" };
                Random r = new Random();
                int select123 = r.Next(1, 150);
                int select12 = r.Next(1, 150);
                int selectpass = r.Next(1, 150);
                int selectpass2 = r.Next(1, 150);
                string ho = mang_ten[select123];
                string ten = mang_ten[select12];
                string selectpass1 = mang_ten[selectpass];
                string selectpass3 = mang_ten[selectpass2];


                char[] charArr = ho.ToCharArray();
                foreach (char ch in charArr)
                {
                    KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                }
            Thread.Sleep(500);

            // ten
            KAutoHelper.ADBHelper.Tap(deviceID, 223, 174);

            char[] charArr1 = ten.ToCharArray();
                foreach (char ch in charArr1)
                {

                    KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                }
             

                // next
                next(deviceID);
        

                chophep(deviceID);
            

                // cho phep
                chophep(deviceID);
                delay(1);


                /// sdt
                if (isStop)
                    return;
        

                //////////////////////////// dien so dien thoai vao///////////////
                char[] charArr2 = sdt.ToCharArray();
                foreach (char ch in charArr2)
                {

                    KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                }


                // next

                next(deviceID);
                delay(3);


                // chon ngay
                KAutoHelper.ADBHelper.Tap(deviceID, 266, 442);
            Thread.Sleep(500);

                // click thang
                KAutoHelper.ADBHelper.Tap(deviceID, 54, 191);
            Thread.Sleep(500);

            // nhap thang
            KAutoHelper.ADBHelper.Tap(deviceID, 160, 444);
            Thread.Sleep(500);

            // click nam
            KAutoHelper.ADBHelper.Tap(deviceID, 88, 192);
                delay(1);

                // nhap nam
                KAutoHelper.ADBHelper.Tap(deviceID, 53, 389);
             
                KAutoHelper.ADBHelper.Tap(deviceID, 266, 442);
           
                KAutoHelper.ADBHelper.Tap(deviceID, 266, 442);
           
                KAutoHelper.ADBHelper.Tap(deviceID, 53, 389);
                delay(1);


            KAutoHelper.ADBHelper.Tap(deviceID, 166, 233);
            delay(2);
            for (int i = 0; i < 2; i++)
            {
                KAutoHelper.ADBHelper.Swipe(deviceID, 146, 417, 146, 85);
                delay(2);
            }
            KAutoHelper.ADBHelper.Tap(deviceID, 301, 200);
            delay(1);



                // next
                if (isStop)
                    return;
                next(deviceID);
                delay(3);

            KAutoHelper.ADBHelper.Tap(deviceID, 301, 200);
            delay(2);

                // mat khau

                 pass = textBox2.Text;
                char[] charArr3 = pass.ToCharArray();
                foreach (char ch in charArr3)
                {

                    KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                }

                // dang ky
                if (isStop)
                    return;
                singup(deviceID);
    

                // kiem tra hinh anh co thanh cong hay khong
                thanhcong(deviceID);
  
                if (ktthanhcong == 1)
                {
                    ktthanhcong = 0;

                    luudulieu(sdt, deviceID);

                KAutoHelper.ADBHelper.Tap(deviceID, 55, 176);
                Thread.Sleep(1000);

                string code = codesdt();
                char[] charArr4 = code.ToCharArray();
                foreach (char ch in charArr4)
                {

                    KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                }

                KAutoHelper.ADBHelper.Tap(deviceID, 1501, 213);
                Thread.Sleep(5000);
                /////////////welcaom
                KAutoHelper.ADBHelper.Tap(deviceID, 156, 334);
                Thread.Sleep(1000);
                ////////ship avatar
                KAutoHelper.ADBHelper.Tap(deviceID, 293, 32);
                Thread.Sleep(1000);
                /////////ton on
                KAutoHelper.ADBHelper.Tap(deviceID, 157, 260);
                Thread.Sleep(5000);

                /////////ban be goi y
                KAutoHelper.ADBHelper.Tap(deviceID, 154, 458);
                Thread.Sleep(1000);
                /////////////// vuot len
                KAutoHelper.ADBHelper.Swipe(deviceID, 167, 338,167,108);
                Thread.Sleep(1000);
                //////////// click vao ban be
                KAutoHelper.ADBHelper.Tap(deviceID, 80, 75);
                Thread.Sleep(1000);
                //////////// click add ban be
                KAutoHelper.ADBHelper.Tap(deviceID, 141, 258);

                KAutoHelper.ADBHelper.Tap(deviceID, 136, 349);
                /////////////// vuot len
                KAutoHelper.ADBHelper.Swipe(deviceID, 167, 338, 167, 108);

            }

        }

        void Auto(string deviceID, int dem)
        {


        
            // xoa Fb Lite
                    KAutoHelper.ADBHelper.ExecuteCMD("adb -s" + deviceID + " uninstall com.facebook.lite");
                    delay(1);
                    // cai Fb Lite
                    KAutoHelper.ADBHelper.ExecuteCMD("adb -s" + deviceID + " install fb.apk");
                    delay(2);

                    //click fb
                    if (isStop)
                        return;
                    taotk(deviceID);
                    delay(1);

                    // tieng anh
                    if (isStop)
                        return;
                    tienganh(deviceID);
                    delay(2);

                    //tao tai khoan moi
                    if (isStop)
                        return;
                //creat(deviceID);
                KAutoHelper.ADBHelper.Tap(deviceID, 194, 286);
                delay(1);

                    // next
                    if (isStop)
                        return;
                    next(deviceID);
                    delay(2);

                    // ho
                    if (isStop)
                        return;
                    //KAutoHelper.ADBHelper.TapByPercent(deviceID, 6, 35);

                    string[] mang_ten = { "Bui", "Cao", "Chu", "Chung", "Chuong", "Cu", "Diep", "Doan", "Duong", "Dao", "Dang", "Dau", "Dinh", "Dien", "Doan", "Do", "Duong", "Giang", "Giao", "Giap", "Gia", "Hoang", "Huynh", "Ha", "Ha", "Ho", "Hong", "Hưa", "Khuong", "Khong", "Kim", "Kieu", "La", "La", "Le", "Lo", "Ly", "Luu", "Luong", "Mai", "Mac", "Nguyen", "Nong", "Ong", "Phan", "Phung", "Phuong", "Pham", "Quach", "Thai", "Tieu", "Trieu", "Truong", "Tran", "Trinh", "To", "Tang", "Ta", "Tong", "Tu", "Vo", "Vu", "Vuong", "An", "Anh", "Binh", "Bich", "Bang", "Bach", "Bao", "Ca", "Chi", "Chinh", "Chieu", "Chau", "Cat", "Cuc", "Cuong", "Cam", "Dao", "Di", "Diem", "Dieu", "Du", "Dung", "Duyen", "Dan", "Hieu", "Hien", "Hiep", "Hoa", "Hoan", "Hoai", "Hoan", "Huyen", "Hue", "Han", "Hoa", "Huong", "Huong", "Hanh", "Hai", "Hao", "Hau", "Hang", "Hop", "Khai", "Khanh", "Khuyen", "Khue", "Khanh", "Khe", "Khoi", "Lam", "Lan", "Linh", "Lien", "Lieu", "Loan", "Ly", "Lam", "Le", "Le", "Loc", "Loi", "Mi", "Minh", "Mien", "My", "Man", "My", "Nga", "Nghi", "Nguyen", "Nguyet", "Nga", "Ngan", "Ngon", "Ngoc", "Nhi", "Nhien", "Nhung", "Nhan", "Nhan", "Nha", "Nhu", "Nuong", "Nu", "Oanh", "Phi", "Phong" };
                    Random r = new Random();
                    int select123 = r.Next(1, 150);
                    int select12 = r.Next(1, 150);
                    int selectpass = r.Next(1, 150);
               
                   string ho = mang_ten[select123];
                    string ten = mang_ten[select12];
                    string selectpass1 = mang_ten[selectpass];
               
                    char[] charArr = ho.ToCharArray();
                    foreach (char ch in charArr)
                    {
                        KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                    }
                    delay(1);

            // ten
                  KAutoHelper.ADBHelper.Tap(deviceID, 206, 175);

                  char[] charArr1 = ten.ToCharArray();
                    foreach (char ch in charArr1)
                    {

                        KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                    }
                    delay(2);

                    // next
                    next(deviceID);
                    delay(3);


                    // cho phep
                    if (isStop)
                        return;
                   chophep(deviceID);
                    delay(1);

                    // cho phep
                    chophep(deviceID);
                    delay(1);
                    /// sdt
                    if (isStop)
                        return;
                   // KAutoHelper.ADBHelper.TapByPercent(deviceID, 10.7, 38.9);
                    delay(1);

                    string[] sodienthoai = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" };
                    Random r6 = new Random();

                    int sdt4 = r6.Next(1, 99);
                    int sdt2 = r6.Next(1, 99);
                    int sdt3 = r6.Next(1, 99);
                    string sdtv1 = sodienthoai[sdt4];
                    string sdtv2 = sodienthoai[sdt2];
                    string sdtv3 = sodienthoai[sdt3];

                    string sdt = textBox1.Text + sdtv1 + textBox5.Text; 

                    char[] charArr2 = sdt.ToCharArray();
                    foreach (char ch in charArr2)
                    {

                        KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                    }


                    // next

                    next(deviceID);
                    delay(3);

                    // chon ngay
                    KAutoHelper.ADBHelper.Tap(deviceID, 266, 442);
                    delay(1);

                    // click thang
                    KAutoHelper.ADBHelper.Tap(deviceID, 54, 191);
                    delay(1);

                    // nhap thang
                    KAutoHelper.ADBHelper.Tap(deviceID, 160, 444);
                    delay(1);

                    // click nam
                    KAutoHelper.ADBHelper.Tap(deviceID, 88, 192);
                    delay(1);

                    // nhap nam
                    KAutoHelper.ADBHelper.Tap(deviceID, 53, 389);
                    delay(1);
                    KAutoHelper.ADBHelper.Tap(deviceID, 266, 442);
                    delay(1);
                    KAutoHelper.ADBHelper.Tap(deviceID, 266, 442);
                    delay(1);
                    KAutoHelper.ADBHelper.Tap(deviceID, 53, 389);
                    delay(1);

                    KAutoHelper.ADBHelper.Tap(deviceID, 166, 233);
                    delay(2);
                    for (int i = 0; i < 2; i ++)
                    {
                        KAutoHelper.ADBHelper.Swipe(deviceID, 146, 417, 146, 85);
                        delay(2);
                    }
                    KAutoHelper.ADBHelper.Tap(deviceID, 301, 200);
                    delay(1);

                    // next
                    if (isStop)
                        return;
                    next(deviceID);
                   delay(3);

                    // gioi tinh
                    if (isStop)
                        return;
                    KAutoHelper.ADBHelper.Tap(deviceID, 301, 200);
                    delay(2);

                    // mat khau

                    //KAutoHelper.ADBHelper.TapByPercent(deviceID, 8, 36);
                    delay(2);
                    pass = textBox2.Text;
                   
                    char[] charArr3 = pass.ToCharArray();
                    foreach (char ch in charArr3)
                    {

                        KAutoHelper.ADBHelper.InputText(deviceID, ch.ToString());
                    }


                    // dang ky
                    if (isStop)
                        return;
                    singup(deviceID);
                    delay(2);

                    // kiem tra hinh anh co thanh cong hay khong
                   thanhcong(deviceID);
                    delay(2);
                    if (ktthanhcong == 1)
                    {
                    ktthanhcong = 0;


                    luudulieu(sdt, deviceID);


                    }
                    ///////////// click vo cho code
          


        }


        string lines;
        string token;
        string sualines;
        string c_user1;
        string c_user;
        string xs1;
        string xs;
        string fr1;
        string fr;
        string datr1;
        string datr;
        string cookie;
        void luudulieu(string sdt,string deviceID)
        {
            chaylenlai:
            try
            {
               
                    KAutoHelper.ADBHelper.ExecuteCMD("adb -s" + deviceID + " pull data/data/com.facebook.lite/files/PropertiesStore_v02 " + sdt + ".txt");
                    delay(1);
                    lines = File.ReadAllText(sdt + ".txt");
                    token = Regex.Match(lines, "EAA[a-z,A-Z,0-9]{0,}").ToString();
                    sualines = lines.Replace("\"", " ");
                    c_user1 = Regex.Match(sualines, " c_user , is_secure :true, expires_timestamp :[0-9]{0,}, (.*?) }").Groups[1].ToString();
                    c_user = c_user1.Replace("value :", "c_user =");
                    xs1 = Regex.Match(sualines, " xs , is_secure :true, expires_timestamp :[0-9]{0,}, (.*?) }").Groups[1].ToString();
                    xs = xs1.Replace("value :", "xs =");
                    fr1 = Regex.Match(sualines, "fr , is_secure :true, expires_timestamp :[0-9]{0,}, (.*?) }").Groups[1].ToString();
                    fr = fr1.Replace("value :", "fr =");
                    datr1 = Regex.Match(sualines, "datr , is_secure :true, expires_timestamp :[0-9]{0,}, (.*?) }").Groups[1].ToString();
                    datr = datr1.Replace("value :", "datr =");

                    cookie = c_user + ";" + xs + ";" + fr + ";" + datr;
                    delay(2);
                File.AppendAllText("Acc.txt", sdt + "|" + pass + "|" + cookie);
                File.Delete(sdt + ".txt");
                /*
                using (StreamWriter HDD = new StreamWriter("Acc.txt", true))
                {
                    HDD.WriteLine(sdt + "|" + pass + "|" + cookie);
                    File.Delete(sdt + ".txt");
                    HDD.Close();

                }
                */
            }
            catch(Exception)
            {
                goto chaylenlai;
            }
        }


     
        void delay(int delay)
        {
            while (delay > 0)
            {

                Thread.Sleep(TimeSpan.FromSeconds(1));

                delay--;
                if (isStop)
                    break;

            }

        }
        int thoatvong = 0;
        private void thanhcong(string deviceID)
        {

            while (true)
            {
                var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID);// chup man hinh
                var thanhcong = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, TOP_UP_BMP);// so sanh hinh anh
                if (TOP_UP_BMP != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, thanhcong.Value.X, thanhcong.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                        thoatvong++;
                        if (thoatvong > 30)
                        {

                            thoatvong = 0;
                            return;
                        }
                    }

                }
                else { }
            }

        }


        private void taotk(string deviceID)
        {

            while (true)
            {
                var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
                var taotk = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, TOP_UP_BMP1);// so sanh hinh anh
                if (TOP_UP_BMP1 != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, taotk.Value.X, taotk.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception) {

                        Thread.Sleep(1000);
                        thoatvong++;
                        if (thoatvong > 30)
                        {

                            thoatvong = 0;
                            return;
                        }

                    }

                }
                else { }
            }

        }

        private void tienganh(string deviceID)
        {

            while (true)
            {
                var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
                var tienganh = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, TOP_UP_BMP2);// so sanh hinh anh
                if (TOP_UP_BMP2 != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, tienganh.Value.X, tienganh.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception) {

                        Thread.Sleep(1000);
                        thoatvong++;
                        if (thoatvong > 30)
                        {

                            thoatvong = 0;
                            return;
                        }
                    }

                }
                else { }
            }

        }
        private void next(string deviceID)
        {

            while (true)
            {
                var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
                var next = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, TOP_UP_BMP3);// so sanh hinh anh
                if (TOP_UP_BMP3 != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, next.Value.X, next.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception) {
                        Thread.Sleep(1000);
                        thoatvong++;
                        if (thoatvong > 10)
                        {

                            thoatvong = 0;
                            return;
                        }
                    }

                }
                else { }
            }

        }
     
        private void chophep(string deviceID)
        {

            while (true)
            {
                var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
                var chophep = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, TOP_UP_BMP4);// so sanh hinh anh
                if (TOP_UP_BMP4 != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, chophep.Value.X, chophep.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception) {
                        Thread.Sleep(1000);
                        thoatvong++;
                        if (thoatvong > 4)
                        {

                            thoatvong = 0;
                            return;
                        }

                    }

                }
                else { }
             // cho phep lan 2
                ///
                var screm1 = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
                var chophepeng = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm1, TOP_UP_BMP6);// so sanh hinh anh
                if (TOP_UP_BMP6 != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, chophepeng.Value.X, chophepeng.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception) {

                        Thread.Sleep(1000);
                        thoatvong++;
                        if (thoatvong > 10)
                        {

                            thoatvong = 0;
                            return;
                        }

                    }

                }
                else { }

            }

        }

      
        private void singup(string deviceID)
        {

            while (true)
            {
                var screm = KAutoHelper.ADBHelper.ScreenShoot(deviceID); // chup man hinh
                var singup = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, TOP_UP_BMP5);// so sanh hinh anh
                if (TOP_UP_BMP5 != null)// kiem tra
                {
                    try
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, singup.Value.X, singup.Value.Y);// click vào hinh anh
                        Thread.Sleep(1000);
                        ktthanhcong = 1;
                        thoatvong = 0;
                        return;
                    }
                    catch (Exception) {

                        Thread.Sleep(1000);
                        ktthoatvong++;
                        if (ktthoatvong > 30)
                        {
                            ktthoatvong = 0;
                            return;
                        }
                    
                    }

                }
                else { }
            }

        }

        string api;
        string input1;
        string idcode;
        int bienso;
        string sdt;
        public string laysdt()
        {
            string result;
             api = textBox4.Text;

            do
            {

            chaylen2:
                HttpRequest httpRequest = new HttpRequest();
                string input = httpRequest.Get("https://api.rentcode.net/api/v2/order/request?apiKey=" + api + "&ServiceProviderId=3", null).ToString();
                idcode = Regex.Match(input, "\"id\":(.*?),").Groups[1].ToString();
                if (idcode != "")
                {
                    goto chaylen;
                }
                else
                {
                    goto chaylen2;
                }


            chaylen:
                input1 = httpRequest.Get("https://api.rentcode.net/api/v2/order/" + idcode + "/check?apiKey=" + api, null).ToString();

                sdt = Regex.Match(input1, "\"phoneNumber\":\"(.*?)\"").Groups[1].ToString();
                result = sdt;
                if (sdt != "")
                {
                    bienso = 5;
                }
                else
                {
                    Thread.Sleep(2000);
                    goto chaylen;
                }


            }
            while (bienso <= 2);

            return result;

        }

        string codegmail;
        int ktcode;
        public string codesdt()
        {
            string result;
            do
            {

            chaylen2:
                HttpRequest httpRequest = new HttpRequest();
            chaylen3:
                string input3 = httpRequest.Get("https://api.rentcode.net/api/v2/order/" + idcode + "/check?apiKey=" + api, null).ToString();
                codegmail = Regex.Match(input3, "<#> (.*?) is").Groups[1].ToString();
                result = codegmail;
                if (codegmail != "")
                {
                    bienso = 5;

                }
                else
                {
                    Thread.Sleep(5000);
                    ktcode++;
                    if (ktcode == 30)
                    {
                        ktcode = 0;

                        return result;

                    }
                    goto chaylen3;
                }

            }
            while (bienso <= 2);

            return result;

        }



        public static void resetDcom(string profileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "rasdial.exe";
            process.StartInfo.Arguments = "\"" + profileName + "\" /disconnect";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.WaitForExit();
            Thread.Sleep(2000);

            process.StartInfo.FileName = "rasdial.exe";
            process.StartInfo.Arguments = "\"" + profileName + "\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.WaitForExit();
            Thread.Sleep(3000);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            isStop = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetDcom(textBox3.Text);
        }
        Bitmap TOP_UP_BMP;
        Bitmap TOP_UP_BMP1;
        Bitmap TOP_UP_BMP2;
        Bitmap TOP_UP_BMP3;
        Bitmap TOP_UP_BMP4;
        Bitmap TOP_UP_BMP5;
        Bitmap TOP_UP_BMP6;


        private void Form1_Load(object sender, EventArgs e)
        {
            TOP_UP_BMP = (Bitmap)Bitmap.FromFile("img/thanhcong.png");
            TOP_UP_BMP1 = (Bitmap)Bitmap.FromFile("img/taotk.png");
            TOP_UP_BMP2 = (Bitmap)Bitmap.FromFile("img/tienganh.png");
            TOP_UP_BMP3 = (Bitmap)Bitmap.FromFile("img/next.png");
            TOP_UP_BMP4 = (Bitmap)Bitmap.FromFile("img/chophep.png");
            TOP_UP_BMP5 = (Bitmap)Bitmap.FromFile("img/singup.png");
            TOP_UP_BMP6 = (Bitmap)Bitmap.FromFile("img/chopheptienganh.png");
            //TOP_UP_BMP7 = (Bitmap)Bitmap.FromFile("img/creat.png");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
     
  
        private void button5_Click(object sender, EventArgs e)
        {
            Thread t11 = new Thread(() =>
            {
                while (true)
                {
                    resetDcom(textBox3.Text);
                    List<Thread> list = new List<Thread>();
                    List<string> devices = new List<string>();
                    var listdevices = KAutoHelper.ADBHelper.GetDevices();
                    if (listdevices != null && listdevices.Count > 0)
                    {
                        devices = listdevices;
                        dem = listdevices.Count;
                    }
                    else { }
                    foreach (var deviceID in devices)
                    {
                        Thread t1 = new Thread(() =>
                        {

                           string sdt = laysdt();
                            Autoclone(deviceID, dem, sdt);
                        });
                        t1.IsBackground = true;
                        t1.Start();
                        Thread.Sleep(1000);

                        list.Add(t1);

                    }
                    foreach (Thread thread2 in list)
                    {
                        thread2.Join();
                    }
                }


            });
            t11.IsBackground = true;
            t11.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Thread t11 = new Thread(() =>
            {
                while (true)
                {
                  
                    List<Thread> list = new List<Thread>();
                    List<string> devices = new List<string>();
                    var listdevices = KAutoHelper.ADBHelper.GetDevices();
                    if (listdevices != null && listdevices.Count > 0)
                    {
                        devices = listdevices;
                        dem = listdevices.Count;
                    }
                    else { }
                    foreach (var deviceID in devices)
                    {
                        Thread t1 = new Thread(() =>
                        {

                    
                            Autoclone(deviceID, dem, sdt);
                        });
                        t1.IsBackground = true;
                        t1.Start();
                        Thread.Sleep(1000);

                        list.Add(t1);

                    }

                }


            });
            t11.IsBackground = true;
            t11.Start();


        }
    }
}
