using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace The_ATM_Project
{
    internal class Encryption
    {
        public byte[] Encrypt(string info) // encrypts the string
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(info);
                byte[] ciphertextBytes = rsa.Encrypt(plaintextBytes, true);
                return ciphertextBytes;
            }
        }

        public string Decrypt(byte[] data) //decrypts the byte[]
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                byte[] decryptedBytes = rsa.Decrypt(data, true);
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                return decryptedText;
            }
                
        }

        public bool Verify(string A,string B) // verifies the two strings to see if they are equal
        {

            if (A == B) 
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        
    }
    
}
