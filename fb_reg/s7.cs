using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    class s7
    {
        //public void Autoclone(string deviceID)
        //{
        //    try
        //    {
        //        //GoogleSheet.WriteAccount2All("aaaaa", "All");
        //        //GoogleSheet.WriteAccount("sss", deviceID);
        //        //string aaa = Utility.CheckLive();
        //        if (!ChangeDeviceInfo(deviceID))
        //        {
        //            fail++;
        //            return;
        //        }
        //        Thread.Sleep(1000);
        //        ChangeIp(deviceID);
        //        Thread.Sleep(1000);
        //        OpenFacebookApp(deviceID);
        //        Thread.Sleep(1000);
        //        Utility.Log("Tap to new account button", status);
        //        if (selectedDeviceName == "a30")
        //        {
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 50.7, 80.6);
        //        }
        //        else if (selectedDeviceName == "s7")
        //        {
        //            Device.TapByPercentDelay(deviceID, 51.2, 87.4, 1000);
        //        }
        //        if (selectedDeviceName == "a7Cook")
        //        {
        //            KAutoHelper.ADBHelper.Tap(deviceID, 572, 1708);
        //        }

        //        // next
        //        Next(deviceID);
        //        Thread.Sleep(1000);
        //        // Allow all
        //        Utility.Log("Allow all permission", status);
        //        WaitAndTapXML(deviceID, 10, "Allowresource");
        //        Thread.Sleep(1000);
        //        WaitAndTapXML(deviceID, 10, "Allowresource");

        //        // Random gender
        //        string gender = GetGender();
        //        Thread.Sleep(2000);
        //        if (!InputName(deviceID, gender))
        //        {
        //            fail++;
        //            return;
        //        }

        //        Utility.Log("Input Birthday", status);
        //        //Thread.Sleep(2000);
        //        if (!InputBirthday(deviceID))
        //        {
        //            fail++;
        //            return;
        //        }

        //        Next(deviceID);
        //        Thread.Sleep(1000);

        //        Utility.Log("Input gender", status);
        //        InputGender(deviceID, gender);

        //        Next(deviceID);
        //        Thread.Sleep(2000);
        //        string[] ll = Utility.GetCordText(deviceID, "edittext");
        //        if (ll == null)
        //        {
        //            fail++;
        //            return;
        //        }
        //        KAutoHelper.ADBHelper.Tap(deviceID, Convert.ToInt32(ll[2]) - 10, Convert.ToInt32(ll[3]) - 10);
        //        Thread.Sleep(1000);
        //        string phone = Utility.GeneratePhoneNumber(dausotextbox.Text);
        //        KAutoHelper.ADBHelper.InputText(deviceID, Utility.ConvertToUnsign(phone));
        //        Thread.Sleep(2000);
        //        Next(deviceID);
        //        Thread.Sleep(1300);
        //        string password = Utility.GeneratePassword();

        //        KAutoHelper.ADBHelper.InputText(deviceID, password);

        //        Next(deviceID);
        //        Thread.Sleep(1300);


        //        Utility.Log("Tap sign up button", status);
        //        WaitAndTapXML(deviceID, 20, "SignUpresourceid");

        //        // Wait about 18s and check action

        //        Thread.Sleep(25000);



        //        if (CheckLock(deviceID))
        //        {
        //            fail++;
        //            return;
        //        }

        //        if (Utility.CheckTextExist(deviceID, "what their friends call them"))
        //        {
        //            string[] aa = Utility.GetCordText(deviceID, "edittext");
        //            if (aa == null)
        //            {
        //                fail++;
        //                return;
        //            }
        //            KAutoHelper.ADBHelper.Tap(deviceID, Convert.ToInt32(aa[2]) - 10, Convert.ToInt32(aa[3]) - 10);
        //            string name = Utility.GetFirtName(InputEnglishNameCheckbox.Checked, gender);
        //            QLong.Phone.Truyền_Chuỗi(deviceID, name);
        //            Thread.Sleep(3000);
        //            Next(deviceID);
        //        }
        //        if (Utility.CheckTextExist(deviceID, "SelectYourName"))
        //        {
        //            Utility.Log("select your name after create", status);
        //            if (selectedDeviceName == "s7")
        //            {
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 91.5, 37.7);
        //            }
        //            else if (selectedDeviceName == "a30")
        //            {
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.8, 38.4);
        //            }
        //            else if (selectedDeviceName == "a7Cook")
        //            {
        //                KAutoHelper.ADBHelper.TapByPercent(deviceID, 90.7, 47.8);
        //            }

        //            Next(deviceID);
        //        }

        //        if (Utility.CheckTextExist(deviceID, "Phonenumberalreadyinuse"))
        //        {
        //            fail++;
        //            return;
        //        }

        //        if (Utility.CheckTextExist(deviceID, "ConfirmbyEmail"))
        //        {
        //            goto ENTER_CODE_CONFIRM_EMAIL;
        //        }

        //        if (WaitAndTapXML(deviceID, 3, "NOTNOW"))
        //        {
        //            Utility.Log("Have popup Not Now", status);

        //            Thread.Sleep(1000);

        //            Device.EnterPress(deviceID);
        //            Device.EnterPress(deviceID);
        //            Thread.Sleep(1000);
        //        }

        //    ENTER_CODE_CONFIRM_EMAIL:

        //        if (!WaitAndTapXML(deviceID, 20, "ConfirmbyEmail"))
        //        {
        //            fail++;
        //            return;
        //        }
        //        string Hotmail = "";
        //        string[] TKMK;
        //        Utility.Log("Tap text box email address", status);
        //        WaitAndTapXML(deviceID, 3, "editText");

        //        Hotmail = Utility.CheckLive();
        //        if (Hotmail == Constant.FAIL)
        //        {
        //            fail++;
        //            return;
        //        }
        //        TKMK = Hotmail.Split('|');
        //        Thread.Sleep(300);
        //        Utility.Log("Put email:" + TKMK[0], status);
        //        KAutoHelper.ADBHelper.InputText(deviceID, TKMK[0]);

        //        Thread.Sleep(3000);
        //        Utility.Log("Tap button continue", status);
        //        WaitAndTapXML(deviceID, 20, "Continue");

        //        Utility.Log("Check enter screen enter code - if not - return", status);

        //        if (!WaitXML(deviceID, 20, "Enterthecode"))
        //        {
        //            fail++;
        //            return;
        //        }

        //        string code = Utility.GetHotmailCode(Hotmail);
        //        if (code == Constant.FAIL)
        //        {
        //            // Resend mail
        //            Utility.Log("Get verify code fail", status);
        //            WaitAndTapXML(deviceID, 3, "sendemailagain");
        //            Thread.Sleep(2000);
        //            code = Utility.GetHotmailCode(Hotmail);
        //            if (code == Constant.FAIL)
        //            {
        //                Utility.StoreInfoNoVerify(selectedDeviceName, deviceID, password, Hotmail, gender);

        //                noVerified++;
        //                return;
        //            }
        //        }
        //        Utility.Log("tap to confirm code", status);
        //        WaitAndTapXML(deviceID, 3, "editText");

        //        Utility.Log("Input code:" + code, status);
        //        KAutoHelper.ADBHelper.InputText(deviceID, code);

        //        Thread.Sleep(4000);
        //        WaitAndTapXML(deviceID, 10, "Confirmresource");

        //        Thread.Sleep(4000);
        //        Utility.Log("Skip upload photo ", status);
        //        string qrCode = "";
        //        if (!Utility.CheckTextExist(deviceID, "Add5"))
        //        {

        //            WaitAndTapXML(deviceID, 20, "Skipresource");
        //            Thread.Sleep(2000);

        //            if (Utility.CheckTextExist(deviceID, "SaveYourLoginInfo"))
        //            {
        //                WaitAndTapXML(deviceID, 5, "okresource");
        //                goto DENY_LOCATION_AND_STORE_DATA;
        //            }
        //        }
        //        // Add your friend
        //        if (!WaitAndTapXML(deviceID, 20, "Add5"))
        //        {
        //            WaitAndTapXML(deviceID, 5, "skip");
        //        }

        //        // Invite your friend
        //        Utility.Log("Invite friend tap", status);
        //        WaitAndTapXML(deviceID, 5, "InviteAll");

        //        WaitAndTapXML(deviceID, 5, "YESresource");
        //        WaitAndTapXML(deviceID, 5, "OKresource");

        //        Utility.Log("Save login info", status);

        //    // Done
        //    DENY_LOCATION_AND_STORE_DATA:
        //        regOk++;
        //        Thread.Sleep(3000);
        //        if (Utility.CheckTextExist(deviceID, "allowfacebooktoaccessyourlocation"))
        //        {
        //            // Deny access location
        //            KAutoHelper.ADBHelper.TapByPercent(deviceID, 58.4, 63.3);
        //        }
        //        else
        //        {
        //            WaitAndTapXML(deviceID, 3, "deny");
        //        }

        //        if (runAvatarCheckbox.Checked)
        //        {
        //            UploadAvatar(deviceID);
        //        }

        //        if (set2faCheckbox.Checked)
        //        {
        //            qrCode = Set2fa(deviceID);
        //        }
        //        Utility.Log("Store information account facebook", status);
        //        Utility.StoreInfo(selectedDeviceName, deviceID, password, Hotmail, qrCode, gender);
        //    }
        //    catch (Exception e)
        //    {
        //        Utility.Log(e.Message, status);
        //    }
        //}

    }
}
