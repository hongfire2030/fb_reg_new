using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static fb_reg.Utility;
using static fb_reg.Form1Util;
using fb_reg.RequestApi;
using System.Drawing;

namespace fb_reg
{
    public static class AccMoi
    {
        public static OrderObject MoiFbLite(DeviceObject device, OrderObject order, bool epAccMoi)
        {
            string deviceID = device.deviceId;
            if (order.loginAccMoiLite)
            {
                LogRegStatus(device, "Mồi Fbliteeeeee");
                Utility.LogStatus(device, "Login acc moi fblite");
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
                                if (!Form1Util.LoginFbLite(deviceID, device.accMoi))
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
                                        if (epAccMoi)
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
                    else
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
                                        if (epAccMoi)
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
                    PublicData.dataGridView.Rows[device.index].DefaultCellStyle.BackColor = Color.Goldenrod;
                    if (epAccMoi)
                    {

                        order.error_code = Constant.CAN_NOT_GET_ACC_CODE;

                        Thread.Sleep(3000);
                        return order;
                    }
                }
            }
            return order;
        }
    }
}
