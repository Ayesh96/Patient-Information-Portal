
using System.Data.OleDb;


namespace PIMS
{
    class connection
    {
        public OleDbConnection conn;

        public void con(string user,string pass)
        {
            string oradb = "Provider=MSDAORA;Data Source=pims_oltp;User ID="+user+";password="+pass+";unicode=true";
             conn = new OleDbConnection(oradb);  // C#
            conn.Open();
            
        }
    }
}
