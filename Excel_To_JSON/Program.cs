using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelDataReader;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        System.Text.Encoding.RegisterProvider(
            System.Text.CodePagesEncodingProvider.Instance);

        var subscribers = new List<object>();

        using (var stream = File.Open("Userdata.xlsx", FileMode.Open, FileAccess.Read))
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
            var dataSet = reader.AsDataSet();
            var table = dataSet.Tables[0];

            int id = 1;

            for (int i = 1; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];

                string rawName = row[0]?.ToString() ?? "";

                // ✅ Extract 10-digit phone numbers
                var phoneNumbers = Regex.Matches(rawName, @"\b\d{10}\b")
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .Distinct()
                    .Take(2)
                    .ToList();

                string phone1 = phoneNumbers.Count > 0 ? phoneNumbers[0] : "";
                string phone2 = phoneNumbers.Count > 1 ? phoneNumbers[1] : "";

                // ✂ Clean name (remove phones & junk)
                string cleanName = rawName;

                foreach (var phone in phoneNumbers)
                {
                    cleanName = cleanName.Replace(phone, "");
                }

                cleanName = Regex.Replace(cleanName, @"[\/\-,]+", " ");
                cleanName = Regex.Replace(cleanName, @"\s+", " ").Trim();

                // ✅ SET-TOP BOX HANDLING (ONLY NEW LOGIC)
                string rawSetTopBox = row[6]?.ToString() ?? "";
                string digitsOnly = Regex.Replace(rawSetTopBox, @"\D", "");
                string setTopBoxNumber = digitsOnly.Length >= 10
                    ? digitsOnly.Substring(0, 10)
                    : "";

                subscribers.Add(new
                {
                    Id = id++,
                    SubscriberName = cleanName,
                    PhoneNumber1 = phone1,
                    PhoneNumber2 = phone2,
                    NickName = row[1]?.ToString()?.Trim(),
                    RentAmount = int.TryParse(row[2]?.ToString(), out var r) ? r : 0,
                    Status = row[3]?.ToString()?.Trim(),
                    AreaName = row[4]?.ToString()?.Trim(),
                    CompanyName = row[5]?.ToString()?.Trim(),

                    // ✅ NEW FIELD
                    SetTopBoxNumber = setTopBoxNumber,

                    Address = ""
                });
            }
        }

        File.WriteAllText(
            "subscribers.json",
            JsonConvert.SerializeObject(subscribers, Formatting.Indented)
        );

        Console.WriteLine("✅ JSON created successfully (set-top box handled)");
        Console.ReadLine();
    }
}
