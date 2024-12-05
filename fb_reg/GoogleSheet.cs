using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace fb_reg
{
	class GoogleSheet
	{
		private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
		private const string ApplicationName = "Reg Facebook";
		private const string SpreadsheetId = "1EvjpJckIN08ewXb5vZcPIePwxdkaSVRgeMjT23BLbDI";
		private const string AdminSheet = "1D3sNGJadl2Nf-JbSsK7GGU7irdBPXvO2SX87scqPAb4";
		private const string ClientSheet = "11NTCM6oPOIle7n_sUlpPDZozN9nSfsG6nyF4hQg600s";
		private const string Sheet = "A7";
		public static SheetsService _service;

		public static void Initial()
		{
			string key = "";
			GoogleCredential credential;
			using (var stream = new FileStream("releasemimiolab-4ed9f389d391.json", FileMode.Open, FileAccess.Read))
			{
				credential = GoogleCredential.FromStream(stream)
					.CreateScoped(Scopes);
			}

			// Create Google Sheets API service.
			_service = new SheetsService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName,
			});

			//CreateMultipleEntries(5);
		}

		public static ArrayList getListAdmin(string sheet, string range)
		{
			return getList(sheet, range, AdminSheet);
		}
		public static ArrayList getList(string sheet, string range, string SpreadsheetId)
		{
			ArrayList list = new ArrayList();
			var RANGE = $"{sheet}!{range}";
			SpreadsheetsResource.ValuesResource.GetRequest request =
					_service.Spreadsheets.Values.Get(SpreadsheetId, RANGE);

			var response = request.Execute();
			IList<IList<object>> values = response.Values;

			if (values != null && values.Count > 0)
			{
				foreach (var row in values)
				{
					string value = (string)row[0];
					list.Add(value);
				}
			}
			else
			{
				Console.WriteLine("No data found.");
				return list;
			}
			return list;
		}
		private static void ReadEntries()
		{
			var range = $"{Sheet}!A:F";
			SpreadsheetsResource.ValuesResource.GetRequest request =
					_service.Spreadsheets.Values.Get(SpreadsheetId, range);

			var response = request.Execute();
			IList<IList<object>> values = response.Values;
			if (values != null && values.Count > 0)
			{
				foreach (var row in values)
				{
					// Print columns A to F, which correspond to indices 0 and 4.
					Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5}", row[0], row[1], row[2], row[3], row[4], row[5]);
				}
			}
			else
			{
				Console.WriteLine("No data found.");
			}
		}

		public static int getLastLineIndex()
		{
			var range = $"{Sheet}!E2:E2";
			SpreadsheetsResource.ValuesResource.GetRequest request =
					_service.Spreadsheets.Values.Get(SpreadsheetId, range);

			var response = request.Execute();
			IList<IList<object>> values = response.Values;
			if (values != null && values.Count > 0)
			{
				foreach (var row in values)
				{
					return Int32.Parse((string)row[0]) + 2;

				}
			}
			else
			{
				Console.WriteLine("No data found.");
				return 2;
			}
			return 2;
		}

		public static string GetValue(string sheetId, string cell)
		{
			
			var range = $"{sheetId}!{cell}";
			SpreadsheetsResource.ValuesResource.GetRequest 
				request =_service.Spreadsheets.Values.Get(SpreadsheetId, range);

			var response = request.Execute();
			IList<IList<object>> values = response.Values;
			if (values != null && values.Count > 0)
			{
				foreach (var row in values)
				{
					return (string)row[0];
				}
			}
			else
			{
				Console.WriteLine("No data found.");
				return "";
			}
			
			return "";
		}

		public static IList<IList<object>> GetValues(string sheetId, string range)
		{

			var rangeSheet = $"{sheetId}!{range}";
			SpreadsheetsResource.ValuesResource.GetRequest
				request = _service.Spreadsheets.Values.Get(SpreadsheetId, rangeSheet);

			var response = request.Execute();
			return response.Values;
		}

		private static void CreateEntry()
		{
			var range = $"{Sheet}!A:F";
			var valueRange = new ValueRange();

			var oblist = new List<object>() { "Hello!", "This", "was", "insertd", "via", "C#" };
			valueRange.Values = new List<IList<object>> { oblist };

			var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
			appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
			appendRequest.Execute();
		}

		public static void WriteAccount(string acc)
		{
			int lastIndex = getLastLineIndex();
			var range = $"{Sheet}!B{lastIndex}:B{lastIndex}";
			
				var valueRange = new ValueRange();

				var oblist = new List<object>() {  };
				oblist.Add(acc);
				valueRange.Values = new List<IList<object>> { oblist };

				var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
				appendRequest.ValueInputOption =
					SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
				appendRequest.Execute();
			
		}

		
		public static bool WriteAccount(string data, string sheet)
		{
			try
            {
				var range = $"{sheet}!A2:B";

				var valueRange = new ValueRange();

				var oblist = new List<object>() { };
				oblist.Add(data);
				valueRange.Values = new List<IList<object>> { oblist };

				var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
				appendRequest.ValueInputOption =
					SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
			   AppendValuesResponse check =	appendRequest.Execute();
				return true;
			} catch(Exception e)
            {
				Console.WriteLine(e.Message);
				return false;
            }
		}
		
		private static void CreateMultipleEntries(int quantity = 1)
		{
			var range = $"{Sheet}!A:G";
			for (var i = 0; i < quantity; i++)
			{
				var valueRange = new ValueRange();

				var oblist = new List<object>() { "Hello!", "This", "was", "insertd", "via", "C#", i };
				valueRange.Values = new List<IList<object>> { oblist };

				var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
				appendRequest.ValueInputOption =
					SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
				appendRequest.Execute();
			}
		}


		private static void UpdateEntry()
		{
			var range = $"{Sheet}!D543";
			var valueRange = new ValueRange();

			var oblist = new List<object>() { "updated" };
			valueRange.Values = new List<IList<object>> { oblist };

			var updateRequest = _service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
			updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
			updateRequest.Execute();
		}

		public static void UpdateEntry(string sheetId, string range, string value)
		{
			var rangeSheet = $"{sheetId}!{range}";
			var valueRange = new ValueRange();

			var oblist = new List<object>() { value };
			valueRange.Values = new List<IList<object>> { oblist };

			var updateRequest = _service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, rangeSheet);
			updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
			updateRequest.Execute();
		}

		private static void DeleteEntry()
		{
			var range = $"{Sheet}!A543:F";
			var requestBody = new ClearValuesRequest();

			var deleteRequest = _service.Spreadsheets.Values.Clear(requestBody, SpreadsheetId, range);
			deleteRequest.Execute();
		}
		public static void AddSheet(string sheetName)
        {

			// Add new Sheet
			try
            {
				var addSheetRequest = new AddSheetRequest();
				addSheetRequest.Properties = new SheetProperties();
				addSheetRequest.Properties.Title = sheetName;
				BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
				batchUpdateSpreadsheetRequest.Requests = new List<Request>();
				batchUpdateSpreadsheetRequest.Requests.Add(new Request
				{
					AddSheet = addSheetRequest
				});

				var batchUpdateRequest =
					_service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, SpreadsheetId);

				batchUpdateRequest.Execute();
			} catch(Exception e)
            {
				Console.WriteLine(e.Message);
            }
			
		}
	}
}
