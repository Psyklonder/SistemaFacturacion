using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FacturacionElectronica.BL
{
    public class Utilidades
    {
        private static String _encryptionKey = String.Format("SegEyETools|{0}*$?#({1})", 97, 8974);
        private static byte[] _Key = { };
        private static byte[] _iV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef,
                                      0x15, 0x10, 0x40, 0xaa, 0x25, 0xba, 0xdd, 0xef,
                                      0x34, 0x25, 0x33, 0xbb, 0xef, 0x12, 0x09, 0xef};
        public static string Cifrar(string valor)
        {
            using (TripleDESCryptoServiceProvider _encriptor = new TripleDESCryptoServiceProvider())
            {
                using (MemoryStream _oMemoryStream = new MemoryStream())
                {
                    byte[] _inputByteArray = Encoding.UTF8.GetBytes(valor);
                    _Key = Encoding.UTF8.GetBytes(_encryptionKey);

                    CryptoStream _cryptoStream = new CryptoStream(_oMemoryStream, _encriptor.CreateEncryptor(_Key, _iV), CryptoStreamMode.Write);

                    _cryptoStream.Write(_inputByteArray, 0, _inputByteArray.Length);
                    _cryptoStream.FlushFinalBlock();

                    return Convert.ToBase64String(_oMemoryStream.ToArray());
                }
            }
        }

        public static string DesCifrar(string valor)
        {
            using (TripleDESCryptoServiceProvider _encriptor = new TripleDESCryptoServiceProvider())
            {
                using (MemoryStream _memoryStream = new MemoryStream())
                {
                    byte[] _inputByteArray = Convert.FromBase64String(valor);
                    _Key = Encoding.UTF8.GetBytes(_encryptionKey);

                    CryptoStream _cryptoStream = new CryptoStream(_memoryStream, _encriptor.CreateDecryptor(_Key, _iV), CryptoStreamMode.Write);

                    _cryptoStream.Write(_inputByteArray, 0, _inputByteArray.Length);
                    _cryptoStream.FlushFinalBlock();

                    return Encoding.UTF8.GetString(_memoryStream.ToArray());
                }
            }
        }

        public static string GenerarHash(string valor)
        {
            using (SHA512 _cifrarSHA = new SHA512CryptoServiceProvider())
            {
                UnicodeEncoding _codificador = new UnicodeEncoding();

                byte[] _datos = _codificador.GetBytes(Cifrar(valor));
                byte[] _resultado = _cifrarSHA.ComputeHash(_datos);

                StringBuilder _resultadoString = new StringBuilder();

                for (int i = 0; i < _resultado.Length; i++)
                { _resultadoString.Append(_resultado[i].ToString("x2")); }

                return _resultadoString.ToString();
            }
        }

        public static string GenerarHash128(string valor)
        {
            string _resultado = GenerarHash(valor).Substring(0, 128);
            string _resultadoSeparado = Regex.Replace(_resultado, ".{8}", "$0-");
            return _resultadoSeparado.Remove(_resultadoSeparado.Length - 1, 1);
        }

        public static string GenerarHash64(string valor)
        {
            string _resultado = GenerarHash(valor).Substring(0, 64);
            string _resultadoSeparado = Regex.Replace(_resultado, ".{8}", "$0-");
            return _resultadoSeparado.Remove(_resultadoSeparado.Length - 1, 1);
        }

        public static string GenerarHash20(string valor)
        {
            string _resultado = GenerarHash(valor).Substring(0, 20);
            string _resultadoSeparado = Regex.Replace(_resultado, ".{5}", "$0-");
            return _resultadoSeparado.Remove(_resultadoSeparado.Length - 1, 1);
        }
    }
}
