using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace MECTimesheetPortal
{
	public class Serializer
	{
		public static string SerializeData(List<Dictionary<string, object>> list)
		{
			var js = new JavaScriptSerializer();
			return js.Serialize(list);
		}
		public static List<KeyValuePair<string, object>> DeserializeData(List<List<string>> data)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (List<string> p in data)
			{
				list.Add(new KeyValuePair<string, object>(p[0], p[1]));
			}
			return list;
		}
	}
}