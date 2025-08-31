
using CsvHelper;
using System.Data;
using System.Globalization;
using WMT.Emailer;
string path = "C:\\Users\\Justi\\Downloads\\TEST.CSV";
using (var reader = new StreamReader("C:\\Users\\Justi\\Downloads\\TEST.CSV"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
     
    using (var dr = new CsvDataReader(csv)) {
        var dt = new DataTable();
        dt.Columns.Add("Area", typeof(string));
        dt.Columns.Add("Percent", typeof(float));
        dt.Columns.Add("All Entered", typeof(int));
        dt.Columns.Add("All Exited", typeof(int));
        dt.Load(dr);

        Dictionary<string, int[]> values = new Dictionary<string, int[]>();
        List<string> messages = new List<string>();
        
        for (int i = 0; i < dt.Rows.Count; i++) {
            int percent = Convert.ToInt32(dt.Rows[i]["Percent"]);
            string area = dt.Rows[i]["area"].ToString();
            int entered = Convert.ToInt32(dt.Rows[i]["All Entered"]);
            int exited = Convert.ToInt32(dt.Rows[i]["All Exited"]);
            // Only care about the sites that have 8% or greater disparity
            // Only take a few of the details from the query can add any of the other columns to the email
            if (percent >= 8.00) {
                values.Add(area, [percent, entered, exited]);
            }
        }
        foreach (var value in values){
            if (value.Value[1] > value.Value[2]){
                //messages.Add($"Area: {pair.Key}, Entry-Exit % difference: {pair.Value[0]}% , Total vehicles entered: {pair.Value[1]} , Total vehicles Exited: {pair.Value[2]}  - Please check the Exit cameras for this site");
                messages.Add($@"
                    <b>Area: {value.Key}</b>
                    <ul>
                        <li>Entry-Exit % difference: {value.Value[0]}%</li>
                        <li>Total vehicles entered: {value.Value[1]}</li>
                        <li>Total vehicles exited: {value.Value[2]}</li>
                        <li>Please check the Entry cameras for this site</li>
                    </ul>");
            }
            else {
                //messages.Add($"Area: {pair.Key}, Entry-Exit % difference: {pair.Value[0]}% , Total vehicles entered: {pair.Value[1]} , Total vehicles Exited: {pair.Value[2]}  - Please check the Entry cameras for this site");
                messages.Add($@"
                    <b>Area: {value.Key}</b>
                    <ul>
                        <li>Entry-Exit % difference: {value.Value[0]}%</li>
                        <li>Total vehicles entered: {value.Value[1]}</li>
                        <li>Total vehicles exited: {value.Value[2]}</li>
                        <li>Please check the Exit cameras for this site</li>
                    </ul>");
            }
               
        }
        messages.Add($@"
                    <p>This email only flags the sites that have a % Entry-Exit difference of 8% or higher</p>
                        ");
        string emailBody = string.Join(" <br/>", messages);
        if (messages.Count == 0)
        {
            emailBody = "No issues found for today.";
        }
            
            Emailer.SendEmail(emailBody,path);
    }

    
}

