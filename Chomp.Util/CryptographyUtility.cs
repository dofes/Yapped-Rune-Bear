using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Chomp.Util;

public static class CryptographyUtility
{
	public static byte[] DecryptAesEcb(Stream inputStream, byte[] key)
	{
		BufferedBlockCipher cipher = CreateAesEcbCipher(key);
		return DecryptAes(inputStream, cipher, inputStream.Length);
	}

	public static byte[] DecryptAesCbc(Stream inputStream, byte[] key, byte[] iv)
	{
		AesEngine cipher2 = new AesEngine();
		ICipherParameters parameters = new ParametersWithIV(new KeyParameter(key), iv);
		BufferedBlockCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(cipher2));
		cipher.Init(forEncryption: false, parameters);
		return DecryptAes(inputStream, cipher, inputStream.Length);
	}

	public static byte[] DecryptAesCtr(Stream inputStream, byte[] key, byte[] iv)
	{
		AesEngine cipher2 = new AesEngine();
		ICipherParameters parameters = new ParametersWithIV(new KeyParameter(key), iv);
		BufferedBlockCipher cipher = new BufferedBlockCipher(new SicBlockCipher(cipher2));
		cipher.Init(forEncryption: false, parameters);
		return DecryptAes(inputStream, cipher, inputStream.Length);
	}

	public static byte[] EncryptAesCtr(byte[] input, byte[] key, byte[] iv)
	{
		IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CTR/NoPadding");
		cipher.Init(forEncryption: true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", key), iv));
		return cipher.DoFinal(input);
	}

	private static BufferedBlockCipher CreateAesEcbCipher(byte[] key)
	{
		AesEngine cipher = new AesEngine();
		KeyParameter parameter = new KeyParameter(key);
		BufferedBlockCipher bufferedBlockCipher = new BufferedBlockCipher(cipher);
		bufferedBlockCipher.Init(forEncryption: false, parameter);
		return bufferedBlockCipher;
	}

	private static byte[] DecryptAes(Stream inputStream, BufferedBlockCipher cipher, long length)
	{
		int blockSize = cipher.GetBlockSize();
		int inputLength = (int)length;
		int paddedLength = inputLength;
		if (paddedLength % blockSize > 0)
		{
			paddedLength += blockSize - paddedLength % blockSize;
		}
		byte[] input = new byte[paddedLength];
		byte[] output = new byte[cipher.GetOutputSize(paddedLength)];
		inputStream.Read(input, 0, inputLength);
		int len = cipher.ProcessBytes(input, 0, input.Length, output, 0);
		cipher.DoFinal(output, len);
		return output;
	}

	public static MemoryStream DecryptRsa(string filePath, string key)
	{
		if (filePath == null)
		{
			throw new ArgumentNullException("filePath");
		}
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		AsymmetricKeyParameter keyParameter = GetKeyOrDefault(key);
		RsaEngine engine = new RsaEngine();
		engine.Init(forEncryption: false, keyParameter);
		MemoryStream outputStream = new MemoryStream();
		using (FileStream inputStream = File.OpenRead(filePath))
		{
			int inputBlockSize = engine.GetInputBlockSize();
			int outputBlockSize = engine.GetOutputBlockSize();
			byte[] inputBlock = new byte[inputBlockSize];
			while (inputStream.Read(inputBlock, 0, inputBlock.Length) > 0)
			{
				byte[] outputBlock = engine.ProcessBlock(inputBlock, 0, inputBlockSize);
				int requiredPadding = outputBlockSize - outputBlock.Length;
				if (requiredPadding > 0)
				{
					byte[] paddedOutputBlock = new byte[outputBlockSize];
					outputBlock.CopyTo(paddedOutputBlock, requiredPadding);
					outputBlock = paddedOutputBlock;
				}
				outputStream.Write(outputBlock, 0, outputBlock.Length);
			}
		}
		outputStream.Seek(0L, SeekOrigin.Begin);
		return outputStream;
	}

	public static AsymmetricKeyParameter GetKeyOrDefault(string key)
	{
		try
		{
			return (AsymmetricKeyParameter)new PemReader(new StringReader(key)).ReadObject();
		}
		catch
		{
			return null;
		}
	}
}
