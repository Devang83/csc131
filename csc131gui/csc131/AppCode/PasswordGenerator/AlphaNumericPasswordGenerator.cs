using System;
using System.Text;
using System.Security.Cryptography;

namespace Sloppycode
{
	/// <summary>
	/// A very simple alphanumerical password generator. This class is free to use without restrictions.
	/// </summary>
	/// <example>
	/// // Alphanumeric password example
	/// int passwordLength = 20;
	/// AlphaNumericPasswordGenerator alphaNumeric = new AlphaNumericPasswordGenerator();
	/// string password = alphaNumeric.Generate(passwordLength);
	/// Console.Console.WriteLine(password);
	/// </example>
	public class AlphaNumericPasswordGenerator
	{
		/// <summary>
		/// Generates a password with the given character length.
		/// </summary>
		/// <param name="Length"></param>
		/// <returns></returns>
		public string Generate(int Length)
		{
			/* Alternative method
			Random random = new Random();
			return MD5Hash(random.Next(0,1000).ToString());
			*/

			if ( Length < 5 )
			{
				Length = 5;
			}
			
			Random random = new Random();
			string password = MD5Hash( random.Next().ToString() ).Substring(0,10);
			string newPass = "";

			// Uppercase at random
			random = new Random();
			for (int i=0;i < password.Length;i++)
			{
				if ( random.Next(0,2) == 1 )
					newPass += password.Substring(i,1).ToUpper();
				else
					newPass += password.Substring(i,1);
			}

			return newPass;
		}

		private string MD5Hash(string Data)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] hash = md5.ComputeHash( Encoding.ASCII.GetBytes(Data) );

			StringBuilder stringBuilder = new StringBuilder();
			foreach( byte b in hash ) 
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}
	}
}
