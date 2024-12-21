using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    class FBItems
    {
		// Token: 0x0600013E RID: 318 RVA: 0x000021AB File Offset: 0x000003AB
		public void Dispose()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000B91C File Offset: 0x00009B1C
		public FBItems(bool initName = false)
		{
			FBItems fbitems = new FBItems();
			this.FirstName = (initName ? fbitems.FirstName : "");
			this.LastName = (initName ? fbitems.LastName : "");
			this.Pass = fbitems.Pass;
			this.TwoFA = "";
			this.Phone = "";
			this.Cookie = "";
			this.Uid = "";
			this.UserAgent = "";
			
			this.MyTempId = "Temp_" + Guid.NewGuid().ToString("N");
			this.FolderId = 1;
			this.DateOfBirth = "";
			this.Email = fbitems.Email;
			this.LastIp = "";
			this.CreateDate = "";
			this.TrangThai = "Live";
			this.Token = "";
			this.MyGroup = new List<string>();
			this.PassEmail = "";
			this.UidLayBai = "";
			this.Avatar = false;
			this.Brand = "";
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000BA5C File Offset: 0x00009C5C
		

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000261C File Offset: 0x0000081C
		public List<string> MyGroup { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000C104 File Offset: 0x0000A304
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00002625 File Offset: 0x00000825
		
		// Token: 0x06000145 RID: 325 RVA: 0x0000C11C File Offset: 0x0000A31C
		public string NinjaString()
		{
			return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", new object[]
			{
				this.Uid,
				this.Pass,
				"",
				this.Cookie,
				this.TwoFA,
				""
			});
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000C178 File Offset: 0x0000A378
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000262E File Offset: 0x0000082E
		public string LastIp { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000C190 File Offset: 0x0000A390
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00002637 File Offset: 0x00000837
		public string UserAgent { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00002640 File Offset: 0x00000840
		public string FirstName { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002649 File Offset: 0x00000849
		public string LastName { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00002652 File Offset: 0x00000852
		public string Email { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000265B File Offset: 0x0000085B
		public string PassEmail { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000C208 File Offset: 0x0000A408
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00002664 File Offset: 0x00000864
		public string Phone { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000C220 File Offset: 0x0000A420
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000266D File Offset: 0x0000086D
		public string TwoFA { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000C238 File Offset: 0x0000A438
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00002676 File Offset: 0x00000876
		public string Pass { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000C250 File Offset: 0x0000A450
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000267F File Offset: 0x0000087F
		public string Uid { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000C268 File Offset: 0x0000A468
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00002688 File Offset: 0x00000888
		public string Cookie { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000C280 File Offset: 0x0000A480
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00002691 File Offset: 0x00000891
		public string TrangThai { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000C298 File Offset: 0x0000A498
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000269A File Offset: 0x0000089A
		public string DateOfBirth { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000026A3 File Offset: 0x000008A3
		public string CreateDate { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000026AC File Offset: 0x000008AC
		public string MyTempId { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000026B5 File Offset: 0x000008B5
		public int FolderId { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000026BE File Offset: 0x000008BE
		public string Token { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000026C7 File Offset: 0x000008C7
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000026CF File Offset: 0x000008CF
		public bool Avatar { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000C310 File Offset: 0x0000A510
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000026D8 File Offset: 0x000008D8
		public string UidLayBai { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000C328 File Offset: 0x0000A528
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000026E1 File Offset: 0x000008E1
		public string Brand { get; set; }

	}

}
