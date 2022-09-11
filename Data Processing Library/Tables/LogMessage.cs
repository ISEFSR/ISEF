namespace cvti.data.Tables 
{
    using cvti.data.Core;
    using System;
    using System.Data;

    public class LogMessage : TableRow
    {
        public static string TableName = "[dbo].[Log]";

        public static readonly string[] Columns = new string[]
        {
            "Id",
            "CreatedBy",
            "CreatedDate",
            "LogTitle",
            "LogInfo",
            "LogType"
        };

        public LogMessage(IDataRecord data)
            : base(data)
        {
        }

        [TableColumnAttribute]
        public int Id { get; set; }
        [TableColumnAttribute]
        public string CreatedBy { get; set; }
        [TableColumnAttribute]
        public DateTime CreatedDate { get; set; }
        [TableColumnAttribute]
        public string LogTitle { get; set; }
        [TableColumnAttribute]
        public string LogInfo { get; set; }
        [TableColumnAttribute]
        public byte LogType { get; set; }
    }
}
