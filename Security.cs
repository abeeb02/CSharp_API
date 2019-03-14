using System;
using System.Security.Cryptography;
using System.Text;

namespace MECTimesheetPortal
{
	public class Security
	{
		public static byte[] AgentSmith(string pwd, Guid salt)
		{
			string p = pwd + salt.ToString();
			byte[] bs = Encoding.Unicode.GetBytes(p);
			return bs;
		}
		public static string Neo(byte[] bs)
		{
			MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
			bs = x.ComputeHash(bs);
			StringBuilder s = new StringBuilder();
			foreach (byte b in bs)
			{
				s.Append(b.ToString("x2").ToLower());
			}
			string hashedPass = s.ToString();
			return hashedPass;
		}
	}
}