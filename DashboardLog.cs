using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void AddTextToLog(System.Data.SqlTypes.SqlString strFileName, System.Data.SqlTypes.SqlString strMessage)
    {
        try
        {
            if (strFileName.Value == "") { return; }
            System.String tmpFolder = Environment.GetEnvironmentVariable("TMP");
            //System.String tmpFolder = "C:\\Temp\\";

            System.String strFileFullName = tmpFolder + @"\" + strFileName.Value;
            System.IO.StreamWriter sw = null;
            if (System.IO.File.Exists(strFileFullName) == true)
            {
                sw = System.IO.File.AppendText(strFileFullName);
            }
            else
            {
                sw = System.IO.File.CreateText(strFileFullName);
            }

            sw.WriteLine("");
            sw.WriteLine("{0} {1}", System.DateTime.Now.ToLongTimeString(),  System.DateTime.Now.ToLongDateString());
            sw.WriteLine(strMessage.Value);
            sw.Flush();
            sw.Close();

        }//try

        catch( System.Exception f )
        {
            //System.Windows.Forms.MessageBox.Show( null,
            //    "Ошибка выполнения операции 'пересчет куба'\n" + f.Message, "Ошибка",
            //    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
        }

        return ;
    }

};
