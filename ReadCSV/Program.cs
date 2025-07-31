
using CsvHelper;
using System.Data;
using System.Globalization;

using (var reader = new StreamReader("C:\\Users\\JustinSummers\\source\\repos\\ReadCSV\\ReadCSV\\Data\\TEST.csv"))
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

        for (int i = 0; i < dt.Rows.Count; i++) {
            int percent = Convert.ToInt32(dt.Rows[i]["Percent"]);
            string area = dt.Rows[i]["area"].ToString();
            int entered = Convert.ToInt32(dt.Rows[i]["All Entered"]);
            int exited = Convert.ToInt32(dt.Rows[i]["All Exited"]);

            if (percent >= 8.00) {
                values.Add(area, [percent, entered, exited]);
            }
        }
        foreach (var pair in values){
            if (pair.Value[1] > pair.Value[2]){
                Console.WriteLine($"Area: {pair.Key}, Entry-Exit % difference: {pair.Value[0]}% , Total vehicles entered: {pair.Value[1]} , Total vehicles Exited: {pair.Value[2]}  - Please check the Exit cameras for this site");
            }else {
                Console.WriteLine($"Area: {pair.Key}, Entry-Exit % difference: {pair.Value[0]}% , Total vehicles entered: {pair.Value[1]} , Total vehicles Exited: {pair.Value[2]}  - Please check the Entry cameras for this site");
            }
               
        }
        
    }
    
}

