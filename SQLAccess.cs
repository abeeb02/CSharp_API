using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace MECTimesheetPortal
{
	public class SQLAccess
	{
		protected SqlConnection con { get; set; }
		protected SqlCommand cmd { get; set; }
		protected List<SqlParameter> param { get; set; }

		public void SetConnection(string name)
		{
			this.con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString.ToString());
		}

		public void SetCommand(string query, SqlConnection con, CommandType type)
		{
			this.cmd = new SqlCommand()
			{
				CommandText = query,
				CommandType = type,
				Connection = con,
			};
		}

		public void SetParameters(List<KeyValuePair<string, object>> parameters)
		{
			this.param = new List<SqlParameter>();
			foreach(KeyValuePair<string, object> p in parameters)
			{
				SqlDbType type = new SqlDbType();

				if (p.Value.GetType() == typeof(int)) { type = SqlDbType.Int; }
				else if (p.Value.GetType() == typeof(string)) { type = SqlDbType.VarChar; }
				else if (p.Value.GetType() == typeof(bool)) { type = SqlDbType.Bit; }

				this.param.Add(new SqlParameter(p.Key, type) { Value = p.Value });
			}
		}

		public SqlConnection GetConnection()
		{
			return this.con;
		}

		public SqlCommand GetCommand()
		{
			return this.cmd;
		}

		public List<SqlParameter> GetParameters()
		{
			return this.param;
		}

		public void AddParametersToCommand()
		{
			this.cmd.Parameters.AddRange(param.ToArray());
		}

		public void ExecuteSQL_NoOutput()
		{
			using (this.con)
			{
				using (this.cmd)
				{
					con.Open();
					try
					{
						cmd.ExecuteScalar();
					}
					catch (SqlException i)
					{
						Debug.WriteLine(i.Message);
					}
				}
			}
		}

		public DataTable ExecuteSQL_Output()
		{
			DataTable output = new DataTable();
			using (this.con)
			{
				using (this.cmd)
				{
					con.Open();
					try
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
						{
							da.Fill(output);
						}
					}
					catch (SqlException i)
					{
						Debug.WriteLine(i.Message);
					}
				}
			}
			return output;
		}
	}
}