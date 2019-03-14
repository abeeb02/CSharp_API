using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

namespace MECTimesheetPortal
{
	[ScriptService]
	public partial class Object : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}
		public string cmdxn { get; set; }
		public List<KeyValuePair<string, object>> p { get; set; }
		public SQLAccess a { get; set; }
		public void InitializeSQL_Standard(string conxn = "MyODBCConnectionString", CommandType type = CommandType.StoredProcedure)
		{
			a = new SQLAccess();
			a.SetConnection(conxn);
			a.SetCommand(cmdxn, a.GetConnection(), type);
			a.SetParameters(p);
			a.AddParametersToCommand();
		}
		public static Object DataAccessor(List<List<string>> paramArray, string command)
		{
			Object a = new Object()
			{
				cmdxn = command,
				p = Serializer.DeserializeData(paramArray)
			};
			a.InitializeSQL_Standard();
			return a;
		}
		[WebMethod]
		public static string JavascriptDataAccessor(List<List<string>> paramArray, string command)
		{
			Object a = new Object()
			{
				cmdxn = command,
				p = Serializer.DeserializeData(paramArray)
			};
			a.InitializeSQL_Standard();
			return Serializer.SerializeData(a.GetData());
		}
		public void SetData()
		{
			a.ExecuteSQL_NoOutput();
		}
		public List<Dictionary<string, object>> GetData()
		{
			return ConvertData(a.ExecuteSQL_Output());
		}
		public List<Dictionary<string, object>> ConvertData(DataTable output)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (DataRow r in output.Rows)
			{
				Dictionary<string, object> inner = new Dictionary<string, object>();
				foreach (DataColumn c in output.Columns)
				{
					inner.Add(c.ColumnName, r[c.ColumnName]);
				}
				list.Add(inner);
			}
			return list;
		}
	}
}