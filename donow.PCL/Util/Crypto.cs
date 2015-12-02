using System.Security.Cryptography;
using System.IO;
using System;
using System.Text;

namespace donow.Util
{

	public class Crypto
	{

		public static string Encrypt(string plainText)
		{

			string encrypted;
			// Create an Aes object
			// with the specified key and IV.
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes ("SecretPassphrase");
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = keyArray;
				aesAlg.IV = aesAlg.Key;
				// Create a decrytor to perform the stream transform.
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key
					, aesAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt
						, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(
							csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						//encrypted = msEncrypt.ToArray();
						encrypted = Base64Encode(EncodeToString(msEncrypt.ToArray()));

					}
				}
			}

			// Return the encrypted bytes from the memory stream.
			return encrypted;

		}

		public static string Decrypt(string cipherText)
		{
			

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			// Create an Aes object
			// with the specified key and IV.
			using (Aes aesAlg = Aes.Create())
			{
				byte[] keyArray = UTF8Encoding.UTF8.GetBytes ("SecretPassphrase");
				aesAlg.Key = keyArray;
				aesAlg.IV = aesAlg.Key;
				// Create a decrytor to perform the stream transform.
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key
					, aesAlg.IV);
				//var cipherTextdecoded = Encoding.ASCII.GetString(cipherText);
				byte[] cipherByteArray = DecodeToBytes(Base64Decode(cipherText));
				// Create the streams used for decryption.
				using (MemoryStream msDecrypt = new MemoryStream(cipherByteArray))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt
						, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(
							csDecrypt))
						{

							// Read the decrypted bytes from the decrypting 
							//stream
							// and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}

			}

			return plaintext;

		}

		public static string EncodeToString(byte[] bytes)
		{
			bool even = (bytes.Length % 2 == 0);
			char[] chars = new char[1 + bytes.Length / sizeof(char) + (even ? 0 : 1)];
			chars[0] = (even ? '0' : '1');
			System.Buffer.BlockCopy(bytes, 0, chars, 2, bytes.Length);
			return new string(chars);
		}
		public static byte[] DecodeToBytes(string str)
		{
			bool even = str[0] == '0';
			byte[] bytes = new byte[(str.Length - 1) * sizeof(char) + (even ? 0 : -1)];
			char[] chars = str.ToCharArray();
			System.Buffer.BlockCopy(chars, 2, bytes, 0, bytes.Length);
			return bytes;
		}

		public static string Base64Encode(string plainText) {
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}

		public static string Base64Decode(string base64EncodedData) {
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}

}