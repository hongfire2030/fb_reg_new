using System.Collections.Generic;
using System.Drawing;

namespace fb_reg
{
    class Constant
    {
        public static string ADB_DEVICE_OFFLINE = "\toffline";
        public static string ADB_DEVICE_RECOVERY = "\trecovery";
        public static string ADB_DEVICE_DISCONNECT = "disconnect";
        public static string ADB_DEVICE_RESTART = "restart";
        public static string ADB_DEVICE_NORMAL = "normal";

        public static string WRONG_OTP = "wrong_otp";
        public static string CHECKPOINT = "checkpoint";
        public static string VERI_SUCCESS = "veri_success";
        public static string ANDROID10 = "android10";
        public static string ANDROID11 = "android11";
        public static string ANDROID13 = "android13";
        public static string ANDROID9 = "android9";

        public static string ACC_TYPE_2FA_SECURITY_SETTING = "2fa_security_settings";
        public static string ACC_TYPE_2FA_SETTING = "2fa_settings";
        public static string ACC_TYPE_REG_BY_MAIL = "reg_by_mail";
        public static string ACC_TYPE_SET_2FA_WEB = "2fa_web";
        public static string ACC_TYPE_REUP = "acc_reup";
        public static string ACC_TYPE_REUP_DOI_TEN = "acc_reup_doi_ten";
        public static string ACC_TYPE_REUP_DOI_TEN_ERR = "acc_reup_doi_ten_error";
        public static string ACC_TYPE_REUP_DOI_TEN_DA_DOI_TEN = "acc_reup_doi_ten_da_doi_ten";
        public static string ACC_TYPE_DOI_TEN_KHong_login = "acc_doi_ten_khong_login_duoc";

        public static string BROWSER_PACKAGE = "org.lineageos.jelly";
        public static string ACC_TYPE_UP_AVATAR_NORMAL = "avatar_normal";

        public static string ACC_TYPE_VERI_BACKUP = "acc_veri_backup";
        public static string ACC_TYPE_NORMAL = "normal";
        public static string ACC_TYPE_FIX_PASSWORD = "fix_password";
        public static int CAN_NOT_GET_ACC_CODE = 1;
        public static int CAN_NOT_OPEN_FB_LITE_CODE = 2;
        public static int CAN_NOT_LOGIN_ACC_2FA = 3;
        public static string CAN_NOT_GET_ACC_MOI = "khong the lay acc moi";
        public static string MAXCLONE = "MAXCLONE";
        public static string HOTMAIL_TYPE = "HOTMAIL";
        public static string HOTMAIL = "hotmail";
        public static string HOTMAIL_TRUSTED = "HOTMAIL.TRUSTED";
        public static string OUTLOOK = "OUTLOOK";
        public static string OUTLOOK_DOMAIN = "OUTLOOK.DOMAIN";

        public static string VERI_BY_PHONE = "phone";
        public static string GMAIL_SELL_GMAIL = "gmailSellGmail";

        public static string GMAIL_SELL_GMAIL_SERVER = "gmailSellGmailServer";

        public static string GMAIL_OTP_GMAIL = "gmailOtpGmail";

        public static string GMAIL_DICH_VU_GMAIL = "gmailDichVuGmail";
        public static string GMAIL_DICH_VU_GMAIL2 = "gmailDichVuGmail2";
        public static string FAKE_MAIL = "fakemailgenerator";
        public static string TEMP_GENERATOR_EMAIL = "tempGeneratorEmail";
        public static string TEMP_GENERATOR_1_SEC_EMAIL = "tempGenerator1SecEmail";
        public static string TEMP_TEMPMAIL_LOL = "tempTempmailLol";
        public static string TEMP_FAKE_EMAIL = "fakeEmail";
        public static string MAIL_OTP = "mailOtp";
        public static string GMAIL_30_MIN = "gmail30min";

        public static string GMAIL_SUPERTEAM = "gmailsuperteam";
        public static string GMAIL_48h = "gmail48h";

        public static string REG = "reg";
        public static string REUP = "reup";
        public static string DIE = "die";
        public static string LIVE = "live";
        public static string UNKNOWN = "unknown";
        public static string DRK_TEXTNOW = "DRK_TEXTNOW";
        public static string CODE_TEXTNOW = "CODE_TEXTNOW";
        public static string OTP_MMO_TEXTNOW = "OTP_MMO";
        public static string PHONE_RAND_TEXTNOW = "PHONE_RAND";
        public static Color green1 = ColorTranslator.FromHtml("#e6ffe6");
        public static Color green2 = ColorTranslator.FromHtml("#99ff99");
        public static Color green3 = ColorTranslator.FromHtml("#4dff4d");
        public static Color green4 = ColorTranslator.FromHtml("#00ff00");
        public static Color green5 = ColorTranslator.FromHtml("#00b300");

        public static Color green6 = ColorTranslator.FromHtml("#008000");

        public static string FACEBOOK_PACKAGE = "com.facebook.katana";
        public static string FACEBOOK_BUSINESS_PACKAGE = "com.facebook.pages.app";
        public static string EXPOSE_PACKAGE = "org.meowcat.edxposed.manager";
        public static string FACEBOOK_LITE_PACKAGE = "com.facebook.lite";
        public static string MESSENGER_PACKAGE = "com.facebook.orca";
        public static string DIALER_PACKAGE = "com.android.dialer";

        public static string LANGUAGE_VN = "vn";
        public static string LANGUAGE_US = "en";
        public static string IP4 = "IP4";
        public static string IP6 = "IP6";
        public static string NO_INTERNET = "Khong co mang";
        public static string ACTION_CHANGE2IP4 = "ChangeApn2Ip4";
        public static string ACTION_CHANGE2IP6 = "ChangeApn2Ip6";
        public static string ACTION_REINSTALL_ROM = "ReinstallRom";
        public static string ACTION_CHANGE2IP4_6 = "ChangeApn2Ip4-6";

        public static string ACTION_CHANGE2IP4G = "ChangeApn2Ip4G";
        public static string ACTION_CHANGE2IP3G = "ChangeApn2Ip3G";

        public static string ACTION_CHANGE_SIM = "Change sim";
        public static string ACTION_VERIFY_ACC = "Action Verify Acc";
        public static string BEELINE = "Beeline";
        public static string MOBI = "MobiFone";
        public static string VIETTEL = "Viettel";
        public static string VIETTEL_TELECOM = "ViettelTelecom";
        public static string VIETTEL_MOBILE = "ViettelMobile";
        public static string VN_MOBIPHONE = "VN mobiphone";
        public static string VN_VINAPHONE = "VN Vinaphone";
        public static string US_PHONE = "us_phone";
        public static string VIET_PHONE = "viet_phone";
        public static string TURN_ON_SIM = "TurnOnSim";
        public static string RANDOM_COUNTRY = "RandomContry";
        public static string TURN_ON_SIM_SUBCRIBE = "TurnOnSimSubcribe";
        public static string TURN_OFF_SIM = "TurnOffSim";
        public static string TURN_OFF_EMU = "TurnOffEMU";
        public static string TURN_ON_EMU = "TurnOnEMU";
        public static string TURN_ON_ALL = "TurnOnAll";
        public static string TURN_OFF_ALL = "TurnOffAll";

        public static string ENABLE_SIM = "EnableSim";
        public static string VINAPHONE = "VinaPhone";
        public static string VIETNAM_MOBILE = "Vietnamobile";
        public static int RUNNING = 1;
        public static int PENDING = 0;
        public static string SCREEN_ON_LOCK_STATUS = "ON_LOCKED";
        public static string SCREEN_LOCK_STATUS = "OFF_LOCKED";
        public static string SCREEN_OPEN_STATUS = "ON_UNLOCKED";
        public static string EMULATOR = "emulator";
        public static string DEEMED = "deemed abusive";
        public static string CONFIRM_CODE_FAIL = "confirm_code_fail";
        public static string ACCOUNT_BLOCK = "Account block";
        public static string DEVICE_HOLDING = "Device Holding";
   
        public static string TEMPMAIL = "tempmail";
        public static string TRUE = "true";
        public static string FALSE = "false";

        public static string MANAGEMENT_SHEET = "Management";
        public static bool isEnglishName = false;
        public static string TEMP_MAIL = "tempmail";
        public static string FAIL = "fail";
        public static string MALE = "male";
        public static string FEMALE = "female";
        public const string LANGUAGE = "s7";
        public const string OTPSIM = "a031efd688aa484741e5c7e1eface58e";
        
        public const string CODE_TEXT_NOW_KEY = "3a6ef99e17b19d3356537e22e86bb79b";
        public const string MAXCLONE_KEY = "99286fe7bcd14317919aba8238d7d9a4a2010e4159ce45c4b790609e7b8f2b86";
        public const string DONGVAN_KEY = "ea3c1db011225df2641bdb7e1e68f7bb";

        public static string[] HEX = { "1","2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"};
        public static string[] PhonePrefix = { "081", "082", "083", "084", "085", "088", "091", "094", 
            "096", "097", "098", "086", "032", "033", "034", "035", "036", "037", "038", "039",
            "070", "076", "077", "078", "079", "090", "093", "089",
            "092", "056", "058", "099", "059", "087"
        };
        public static string[] PhonePrefixVINA = { "081", "082", "083", "084", "085", "088", "091", "094" };
        public static string[] PhonePrefixVETTEL = { "096", "097", "098", "086", "032", "033", "034", "035", "036", "037", "038", "039" };
        public static string[] PhonePrefixMOBI = { "070", "076", "077", "078", "079", "090", "093", "089" };
        public static string[] PhonePrefixVIETNAM = { "092", "056", "058", "099", "059", "087"};
        public static string[] PhonePrefixCANADA = { "403", "587", "780", "825", "236", "778", 
                                                    "250", "604","204", "431", "506", "709", "782", "902", "226", "249", "343", "519", "548",
                                                     "613", "705", "807", "289", "365", "416", "437", "647", "905", "418", "581", "819",
                                                    "873","438", "450", "514", "579", "306", "639", "867"};

        public static string[] PhonePrefixUS = { "011", "200", "201", "202", "203", "205", "206", "207", "208", "209", "210", 
            "211", "212", "213", "214", "215", "216", "217", "218", "219", "224", "225", "227", "228", "229", "231", "234", "239", 
            "240", "248", "251", "252", "253", "254", "256", "260", "262", "267", "269", "270", "276", "278", "281", "283", "300", 
            "301", "302", "303", "304", "305", "306", "307", "308", "309", "310", "311", "312", "313", "314", "315", "316", "317", 
            "318", "319", "320", "321", "323", "325", "330", "331", "334", "336", "337", "339", "340", "341", "347", "351", "352", 
            "360", "361", "369", "380", "385", "386", "400", "401", "402", "404", "405", "406", "407", "408", "409", "410", "411", 
            "412", "413", "414", "415", "417", "419", "423", "424", "425", "430", "432", "434", "435", "440", "442", "443", "445", 
            "456", "464", "469", "470", "475", "478", "479", "480", "484", "500", "501", "502", "503", "504", "505", "506", "507", 
            "508", "509", "510", "512", "513", "515", "516", "517", "518", "520", "530", "540", "541", "551", "557", "559", "561", 
            "562", "563", "564", "567", "570", "571", "573", "575", "580", "585", "586", "601", "602", "603", "605", "606", "607", 
            "608", "609", "610", "611", "612", "614", "615", "616", "617", "618", "619", "620", "623", "626", "627", "628", "630", 
            "631", "636", "641", "646", "650", "651", "657", "659", "660", "661", "664", "667", "669", "671", "678", "679", "682", 
            "689", "700", "701", "702", "703", "704", "706", "707", "708", "710", "711", "712", "713", "714", "715", "716", "717", 
            "718", "719", "720", "724", "727", "731", "732", "734", "737", "740", "747", "752", "754", "757", "760", "763", "764", 
            "765", "770", "773", "774", "775", "781", "785", "786", "787", "800", "801", "802", "803", "804", "805", "806", "808", 
            "810", "812", "813", "814", "815", "816", "817", "818", "828", "830", "831", "832", "835", "843", "845", "847", 
            "848", "850", "856", "857", "858", "859", "860", "862", "863", "864", "865", "866", "867", "870", "872", "876", "877", 
            "878", "880", "881", "882", "888", "900", "901", "903", "904", "906", "907", "908", "909", "910", "911", "912", "913", 
            "914", "915", "916", "917", "918", "919", "920", "925", "928", "931", "935", "936", "937", "939", "940", "941", "947", 
            "949", "951", "952", "954", "956", "959", "970", "971", "972", "973", "975", "978", "979", "980", "984", "985", "989", };

        public static string[] provices = { "An Giang", "Bà Rịa-Vũng Tàu", "Bạc Liêu", "Bắc Kạn", "Bắc Giang", "Bắc Ninh", "Bến Tre", "Bình Dương", "Bình Định", "Bình Phước", "Bình Thuận", "Cà Mau", "Cao Bằng", "Cần Thơ", "Đà Nẵng", "Đắk Lắk", "Đắk Nông", "Điện Biên", "Đồng Nai", "Đồng Tháp"
            ,"Gia Lai", "Hà Giang", "Hà Nam", "Hà Nội", "Hà Tây", "Hà Tĩnh", "Hải Dương", "Hải Phòng", "Hòa Bình", "Hồ Chí Minh", "Hậu Giang", "Hưng Yên", "Khánh Hòa", "Kiên Giang", "Kon Tum", "Lai Châu", "Lào Cai", "Lạng Sơn", "Lâm Đồng", "Long An", "Nam Định", "Nghệ An", "Ninh Bình", "Ninh Thuận"
            ,"Phú Thọ", "Phú Yên", "Quảng Bình", "Quảng Nam", "Quảng Ngãi", "Quảng Ninh", "Quảng Trị", "Sóc Trăng", "Sơn La", "Tây Ninh", "Thái Bình", "Thái Nguyên", "Thanh Hóa", "Thừa Thiên Huế", "Tiền Giang", "Trà Vinh", "Tuyên Quang", "Vĩnh Long", "Vĩnh Phúc", "Yên Bái"};
        public static string[] statusOfFb = { "chào mọi người", "hello mọi người", "hi everybody", "best wish", "Chào ngày mới!",
        "Cuộc sống rất thú vị, và thú vị nhất khi nó được sống vì người khác.",
        "Hãy cười lên và cả thế giới sẽ cười cùng bạn, nếu khóc, bạn sẽ phải chỉ khóc một mình.",
        "Hãy yêu một người có thể vì bạn mà làm tất cả chứ đừng yêu một người chỉ biết diễn tả tương lai!",
        "Con trai trưởng thành khi mất đi người mình yêu. Con gái trưởng thành khi yêu một người.",
        "Bạn không bao giờ nhận ra bạn yêu một người nhiều như thế nào cho đến khi thấy họ yêu một người khác.",
        "Những gì bạn cho đi một cách chân thành thì sẽ mãi là của bạn.",
        "Nếu bạn không thể xây dựng một thành phố thì hãy xây lấy một trái tim hồng.",
        "Yêu thương cho đi là yêu thương có thể giữ được mãi mãi.",
        "Nếu bạn không thể là Mặt Trời thì cũng đừng làm một đám mây.",
        "Sự chia sẻ và tình yêu thương là điều quý giá nhất trên đời.",
        "Với thế giới, bạn chỉ là một hạt cát nhỏ – nhưng với một người nào đó, bạn là cả thế giới của họ.",
        "Người ta có thể quên đi điều bạn nói, nhưng những gì bạn để lại trong lòng họ thì không bao giờ nhạt phai.",
        "Con người trở nên cô đơn vì trong cuộc đời, thay vì xây những chiếc cầu người ta lại xây những bức tường",
        "Niềm tin vào chính mình có sức mạnh xua tan bất kì sự hoài nghi nào của người khác.",
        "Hãy nhớ rằng, đôi khi sự im lặng là câu trả lời hay nhất.",
        "Đừng nói mà hãy làm. Đừng huyên thuyên mà hãy hành động. Đừng hứa mà hãy chứng minh.",
        "Đừng bao giờ quyết định những vấn đề lâu dài trong lúc cảm xúc đang ngắn hạn.",
        "Hạnh phúc không có sẵn. Hạnh phúc xuất phát từ chính hành động của bạn.",
        "Hãy nhớ rằng mối quan hệ đẹp nhất là khi tình yêu thương bạn dành cho nhau vượt trên những nhu cầu đòi hỏi từ nhau.",
        "Cảm ơn ai đó đã vô tâm hời hợt để rồi tôi biết mình nên dừng lại ở đâu.",
        "Lần cuối cùng em khóc vì anh. Em sẽ ngừng khóc và ngừng cả sự yêu thương.",
        "Hạnh phúc thì chẳng được bao lâu mà nỗi đau thì in sâu không thể xóa.",
        "Cái lạnh nhất không phải là khi mùa đông sang, mà là sự vô tâm hời hợt từ người mà bạn đã từng xem là tất cả.",
        "Có những người mình yêu mà không thể gần được. Và cũng có những người yêu mình nhưng không thể ừ được.",
        "Quá khó để bắt buộc ai đó phải yêu mình. Và càng khó hơn khi ép bản thân mình phải ngừng yêu ai đó.",
        "Yêu một người không yêu mình giống như ôm một cây xương rồng, càng ôm chặt càng làm bản thân mình đau hơn.",
        "Chỉ cần khoảng cách đủ xa, thời gian đủ lâu thì dù có quen thuộc đến thế nào cũng sẽ trở lên xa lạ",
        "Đêm nay, đêm mai, đêm sau nữa. Tôi nhớ một người chưa từng nhớ tôi.",
        "Khi yêu sợ nhất là người ta vẫn nhận lời yêu nhưng trong trái tim của họ chưa bao giờ xuất hiện hình bóng của mình.",
        "Thật đáng sợ nếu bỗng dưng một ngày mình thức dậy và nhận ra rằng mình không có ai và không còn điều gì để chờ đợi.",
        "Tình yêu của anh nhẹ nhàng như gió, mong manh như nắng và để lại trong tim em “cay đắng ngút ngàn”.",
        "Cố gắng để quên người mình yêu cũng giống như cách mà mình cố gắng nhớ một người chưa từng gặp.",
        "Đôi khi, chỉ là khoảng trống của một người để lại mà cho dù có cả thế giới vẫn không thể lấp đầy.",
        "Có những người mình yêu nhưng lại không gần được. Có những người nói yêu nhưng hồn lại cách xa nhau.",
        "Yêu là chết ở trong lòng một ít.",
        "Đau khổ nhất là khi yêu ai đó, thương ai đó mà không thể ở bên, không thể nói ra nỗi lòng của mình với người ấy.",
        "Hạnh phúc không phải là thứ xảy ra ngẫu nhiên, cũng chẳng phải là điều mong ước. Nó chính là thứ do chúng ta tạo ra.",
        "Cuộc sống vốn ngắn ngủi, vì vậy hãy dành sự yêu thương cho những người xứng đáng.",
        "Người dễ cười cũng là người dễ khóc, nhưng khi đau khổ tan nát nhất thì họ sẽ im lặng.",
        "Nụ cười vẫn luôn ở trên môi nhưng cuộc sống của tôi từ trước đến nay chưa bao giờ là ổn.",
        "Bỏ mặt anh lại cùng với buồn rầu chồng chất. Đường tình anh và em, mãi sẽ vẫn không đồng nhất.",
        "Có những vết thương nhưng vẫn để lại sẹo. Có những ký ức tuy đã xóa mờ nhưng mãi là nỗi đau.",
        "Thích cảm giác có ai đó gọi mình là “bé yêu”.",
        "A leader is one who knows the way, goes the way and shows the way.",
        "Everything has beauty, but not everyone sees it.",
        "Sadness flies away on the wings of time.",
        "Life is like riding a bicycle. To keep your balance, you must keep moving.",
        "To live is to fight.",
        "Live each day as if it’s your last.",
        "Work hard, dream big.",
        "Defeat is simply a signal to press onward.",
        "Where there is a will, there is a way.",
        "A winner never stops trying.",
        "Keep your face to the sunshine and you can’t see a shadow.",
        "Do not pray for an easy life, pray for the strength to endure a difficult one.",
        "Every new day is another chance to change your life.",
        "Stars can’t shine without darkness.",
        "Life is really simple, but we insist on making it complicated.",
        "Giá như có người yêu để cùng nhau khám phá thế giới.",
        "Ai đó có thể cho mình mượn avatar cho đỡ cô đơn đi?",
        "Nắng đã có mũ, mưa đã có ô, còn tôi sẽ là của ai?",
        "Giờ mà có ai đó nguyện bên mình thì mình sẽ làm cho người ấy hạnh phúc mãi về sau.",
        "Đẹp như thế này đã đủ tiêu chuẩn làm bạn trai/bạn gái của anh/em chưa?",
        "Ngủ là biện pháp quên đi nỗi buồn, Xóa tan muộn phiền và ổn định túi tiền.",
        "Cách để bắt đầu đó là ngừng nói suông và bắt tay vào công việc",
        "Đừng đặt ra giới hạn cho chính mình"
        };
        public static List<string> lastNameVietArr = new List<string> {"Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Đặng", "Bùi",
            "Đỗ", "Hồ", "Ngô", "Dương", "Lý", "Chu", "La", "Lê", "Lô", "Lưu", "Lương", "Mai", "Mạc",
            "Nguyên", "Nông", "Ông",  "Quách", "Thái", "Tiêu", "Triều", "Trương",
            "Trịnh", "Tô", "Tăng", "Tạ", "Tống", "Tu", "Võ", "Vương"};

        public static List<string> firstNameMaleVietArr = new List<string> { "Chung", "Chương", 
            "Diệp", "Đậu", "Đinh", "Điền", "Đoàn", "Giang", "Giao", "Giáp", "Gia", "Hà","Hồng", "Hứa", 
             "Kim", "Kiều", "Bách", "Bảo", "Ca","Cường","Hiệp",  "Hậu","Khải", "Khánh", 
            "Khuyên", "Khuê", "Khanh", "Khê", "Khôi", "Lâm", "Lộc", "Lợi",  "Minh", "Miên", "Nhân", "Nhẫn","Phi",
            "Phong" };
        public static List<string> firstNameFemaleVietArr = new List<string> { "Phụng", "Phương","An", "Anh",
            "Bình", "Bích", "Băng",   "Chi", "Chinh", "Chiêu", "Châu",
            "Cát", "Cúc", "Cẩm", "Đào", "Di", "Diễm", "Diệu", "Du", "Dung",
            "Duyên", "Dần", "Hiếu", "Hiền","Hoa", "Hoan", "Hoài", "Hoàn", "Huyền",
            "Huệ", "Hân", "Hoa", "Hương", "Hường", "Hạnh", "Hải", "Hào","Hằng",
            "Hợp",  "Lam", "Lan",
            "Linh", "Liên", "Liễu", "Loan","Mi","Mỹ", "Mẫn", "Nga", "Nghi", "Nguyệt", "Nga", "Ngân", "Ngọc", "Nhi",
            "Nhiên", "Nhung","Nhã", "Như", "Nương", "Nữ", "Oanh" };
        
        public static List<string> lastNameUSArr = new List<string> {"Johnson", "William", "Brown", "John", "Garcia", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez",
            "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",  "Perez", "Thomson",
            "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Allen", "Wright", "Torres",
                "Adams", "nelson", "rivera",
            "Campbell", "michell",  "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker",
            "Cruz", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez",
            "Peterson", "Bailey", "Kelly", "Ramos", "Cox", "Watson", "Brooks", "Chavez"};

        public static List<string> middleNameMaleUSArr = new List<string> {"Byron","Connor", "Cornelius", "Monsel", "Paton", "Hawke", "Hale", "Gray",
            "Grose", "Averill",  "Mayhew", "Blake", "Peregrine", "Humphrey", "Guy", "Murray", "Garrett", "Tobias", "Claude",
            "Alexander", "Cecil", "Julian", "Gage", "Ackerley", "Action"
        };
        public static List<string> middleNameFemaleUSArr = new List<string> { "Maeve","Isla", "Genevieve",
             "Iris",  "Elena", "Willow"
        };

        public static List<string> firstNameMaleUSArr = new List<string>{"Adonis","Alger","Alva","Alvar","Amory","Archibald","Athelstan","Aubrey","Augustus","Aylmer","Baldric","Barrett","Bernard","Cadell","Cyrus",
            "Derek","Devlin","Dieter","Duncan","Egbert","Emery","Fergal","Fergus","Garrick","Geoffrey","Gideon","Griffith","Harding","Jocelyn","Joyce","Kane","Kelsey","Kenelm","Maynard","Meredith","Mervyn","Mortimer",
            "Ralph","Randolph","Reginald","Roderick","Roger","Waldo","Anselm","Azaria","Basil","Benedict","Clitus","Cuthbert","Carwyn","Dai","Dominic","Darius","Edsel","Elmer","Ethelbert","Eugene","Galvin","Gwyn","Jethro",
            "Magnus","Maximilian","Nolan","Orborne","Otis","Patrick","Clement","Curtis","Dermot","Enoch","Finn","Gregory","Hubert","Phelim","Bellamy","Bevis","Boniface","Caradoc","Duane","Flynn","Kieran","Lloyd","Rowan","Venn",
            "Aidan","Anatole","Conal","Dalziel","Egan","Enda","Farley","Farrer","Lagan","Leighton","Lionel","Lovell","Phelan","Radley","Silas","Uri","Wolfgang","Alden","Alvin","Amyas","Aneurin","Baldwin","Darryl","Elwyn","Engelbert",
            "Erasmus","Erastus","Goldwin","Oscar","Sherwin","Ambrose","Christopher","Isidore","Jesse","Jonathan","Osmund","Oswald","Theophilus","Abner","Baron","Bertram","Damian","Dante","Dempsey","Diego","Diggory","Godfrey","Ivor","Jason",
            "Jasper","Jerome","Lancelot","Leander","Manfred","Merlin","Neil","Orson","Samson","Seward","Shanley","Siegfried","Sigmund","Stephen","Tadhg","Vincent","Wilfred","Andrew","Alexander","Walter","Leon","Leonard","Marcus","Ryder","Drake",
            "Harvey","Harold","Charles","Abraham","Jonathan","Matthew","Michael","Samuel","Theodore","Timothy","Gabriel","Issac", "Mason", "Cato",
            "Gray", "Ace", "Nolan", "Justin", "Alexander", "Levi", "Jonathan", "Otis", "Maverick",
            "Jesse", "Matthew", "Leo", "Liam", "Jayden", "Casper", "Remington", "Bear", "Basil", "Duke",
            "Tarek", "Griffin", "Ethan", "Wyatt", "Carlos", "Alan", "Eric", "Anselm", "Azaria",
            "Basil", "Benedict", "Darius", "Edsel", "Elmer", "Ethelbert", "Maximilian", "Nolan", "Orborne", "Otis",
            "Patrick", "Eugene", "Galvin", "Clitus", "Cuthbert", "Carwyn", "Dai", "Dominic", "Gwyn", "Jethro",
            "Aidan", "Anatole", "Conal", "Dalziel", "Lagan", "Leighton", "Lionel", "Samson", "Uri", "Wolfgang",
            "Lovell", "Neil", "Phelan", "Radley", "Radley", "Silas", "Douglas", "Dylan", "Egan", "Enda",
            "Farley",  "Alan", "David", "Edgar", "Asher", "Benedict", "Felix", "Kenneth", "Paul",
            "Paul", "Max", "Jake", "Buddy", "Jack", "Cody", "Charue", "Bailey", "Rocky", "Sam", "Buster", "Rusty", "Barney",
            "Toby", "Winston", "Murphy", "Harley", "Oscar", "Ben", "Jess", "Zack", "Monty", "Duke",
            "Cooper", "Lucky", "Riley", "Tyson", "Tucker", "Harry", "Oliver", "Jasper", "Teddy", "Sammy", "Beau",
            "Alfie", "Simba", "Milo", "Gizmo", "Zeus", "Bentley", "Rex", "Sandy", "Baxter", "Jackson", "Baandit",
            "Samson", "Gus", "Hunter", "Rudy", "Adney", "Adolf", "Aethelred", "Aiken", "Ainsley", "Al", "Albany"
                , "Alcott", "Alden", "Aldway", "Aldwin", "Alexavier", "Alger", "Almond", "Aloysius"
                , "Alston", "Alton", "Alvin", "Amory", "Anson", "Arathorn", "Arch", "Archer", "Archie", "   "
                , "Arlen", "Arlington", "Arlo", "Armstead", "Armstrong", "Arran", "Asbjorn", "Asbury", "Ashby"
                ,  "Ashley", "Ashton", "Aston", "Atherton", "Auden", "Audrey", "Axton"
        };

        public static List<string> firstNameFemaleUSArr = new List<string> { "Adela","Adelaide","Agatha","Agnes","Alethea","Alida","Aliyah","Alma","Almira","Alula","Alva","Amanda",
            "Amelinda","Amity","Angela","Annabella","Anthea","Aretha","Arianne","Artemis","Aubrey","Audrey","Aurelia","Aurora","Azura","Bernice","Bertha","Blanche","Brenna","Bridget",
            "Calantha","Calliope","Celina","Ceridwen","Charmaine","Christabel","Ciara","Cleopatra","Cosima","Daria","Delwyn","Dilys","Donna","Doris","Drusilla","Dulcie","Edana","Edna","Eira",
            "Eirian","Eirlys","Elain","Elfleda","Elfreda","Elysia","Erica","Ermintrude","Ernesta","Esperanza","Eudora","Eulalia","Eunice","Euphemia","Fallon","Farah","Felicity","Fidelia",
            "Fidelma","Fiona","Florence","Genevieve","Gerda","Giselle","Gladys","Glenda","Godiva","Grainne","Griselda","Guinevere","Gwyneth","Halcyon","Hebe","Helga","Heulwen","Hypatia","Imelda",
            "Iolanthe","Iphigenia","Isadora","Isolde","Jena","Jezebel","Jocasta","Jocelyn","Joyce","Kaylin","Keelin","Keisha","Kelsey","Kerenza","Keva","Kiera","Ladonna","Laelia","Lani","Latifah",
            "Letitia","Louisa","Lucasta","Lysandra","Mabel","Maris","Martha","Meliora","Meredith","Milcah","Mildred","Mirabel","Miranda","Muriel","Myrna","Neala","Odette/Odile","Olwen","Oralie","Oriana",
            "Orla","Pandora","Phedra","Philomena","Phoebe","Rowan","Rowena","Selina","Sigourney","Sigrid","Sophronia","Stella","Thekla","Theodora","Tryphena","Ula","Vera","Verity","Veronica","Vivian","Winifred","Xavia","Xenia","Mila", 
            "Cara", "Allison", "Mia", "Rose", "Milan", "Hannah", "Ellie", "Cora", "Sadie",
            "Rihanna", "Alexandra", "Beatrice", "Constance", "Paige", "Madeline", "Olivia", "Lily", "Sophia", "Natalia",
            "Desi", "Lita", "Quinn", "Alethea", "Verity", "Viva", "Zelda", "Giselle",
            "Grainne", "Kerenza", "Zelda", "Amity", "Edna", "Ermintrude", "Esperanza", "Letitia",
             "Philomena", "Vera", "Adela", "Elysia", "Genevieve", "Gladys", "Gwyneth",
            "Almira", "Alva", "Ariadne", "Cleopatra", "Donna", "Helga", "Adelaide", "Hypatia", "Milcah",
            "Mirabel", "Ladonna", "Orla", "Pandora", "Phoebe", "Rowena", "Xavia", "Martha", "Meliora",
            "Olwen", "Diamond", "Jade", "Scarlet", "Sienna", "Gemma", "Melanie", "Kiera", "Margaret", "Pearl",
            "Ruby", "Emma", "Susan", "Linda", "Karen", "Michelle", "Carol", "Cynthia", "Dora", "Nancy", "Betty",
            "Amalia", "Margaret", "Barbara", "Laura", "Nancie", "Donna", "Helen", "Vickie", "Sharon", "Suzanne", "Molly",
            "Bella", "Daisy", "Maggie", "Ginger", "Bonnie", "Sade", "Zoe", "Sasha", "Missy", "Jess", "Meg",
            "Princess", "Cassie", "Lola", "Lily", "Sally", "Tess", "Holly", "Rosie", "Abbey", "Chloe", "Roxy", "Gracie", "Coco",
            "Poppy", "Angel", "Mille", "Bear", "Lucky", "Chelsea", "Honney", "Sheba", "Misty", "Kate", "Gemma",
            "Mia", "Madison", "Sandy", "Zoey", "Cindy"
                , "Adamaris", "Addison", "Ainsley", "Alma", "Alvina", "Amanda", "Anastasia", "Angela"
                , "Angelica", "Angelina", "Ansley", "Antonia", "Araminta", "Ash", "Ashley", "Ashlyn", "Ashton"
                , "Asia", "Aspen", "Audie", "Audrey", "Audrina", "Bailey", "Barbara", "Beatrice", "Berenice"
                , "Bevery", "Beyonce", "Blythe", "Braelyn", "Brooke", "Burgundy", "Cadence", "Callie","Camelia"
                , "Camellia", "Camilla", "Cassandra", "Cecilia", "Celeste", "Celia", "Channing", "Chelsea", "Cherish", "China"
        };

        

        public static string FINISH = "finish";
        public static string LD_START = "ld_start";
    }
        
    }
