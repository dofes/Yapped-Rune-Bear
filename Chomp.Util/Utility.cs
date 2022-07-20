using System;
using System.IO;
using System.Windows.Forms;
using SoulsFormats;

namespace Chomp.Util;

internal class Utility
{
	private static byte[] ds2RegulationKey = new byte[16]
	{
		64, 23, 129, 48, 223, 10, 148, 84, 51, 9,
		225, 113, 236, 191, 37, 76
	};

	public static BND4 DecryptDS2Regulation(string path)
	{
		byte[] bytes = File.ReadAllBytes(path);
		byte[] iv = new byte[16]
		{
			128, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0
		};
		Array.Copy(bytes, 0, iv, 1, 11);
		iv[15] = 1;
		byte[] input = new byte[bytes.Length - 32];
		Array.Copy(bytes, 32, input, 0, bytes.Length - 32);
		using MemoryStream ms = new MemoryStream(input);
		byte[] decrypted = CryptographyUtility.DecryptAesCtr(ms, ds2RegulationKey, iv);
		File.WriteAllBytes("ffff.bnd", decrypted);
		return SoulsFile<BND4>.Read(decrypted);
	}

	public static void EncryptDS2Regulation(string path, BND4 bnd)
	{
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		bnd.Write(path);
	}

	public static void ShowError(string message)
	{
		MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	public static void DebugPrint(string message)
	{
		bool flag = true;
		Console.WriteLine(message);
	}
}
