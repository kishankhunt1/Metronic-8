using System.Data.SqlTypes;
using System.Text;

namespace Metronic_8.BAL
{
    public class CommonFunctions
    {
        #region For Encryption
        public static string EncryptBase64(string password)
        {
            if (CV.IsURLEncryption)
            {
                string strmsg = string.Empty;
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                strmsg = Convert.ToBase64String(encode);
                return strmsg;
            }
            else
                return password;

        }
        #endregion

        #region For Converting String into ByteCode
        public static bool IsBase64Encoded(String str)
        {
            try
            {
                // If no exception is caught, then it is possibly a base64 encoded string
                byte[] data = Convert.FromBase64String(str);
                // The part that checks if the string was properly padded to the
                // correct length was borrowed from d@anish's solution
                return (str.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                // If exception is caught, then it is not a base64 encoded string
                return false;
            }
        }
        #endregion

        #region For Decryption
        public static SqlInt32 DecryptBase64Int32(SqlString encryptpwd)
        {
            int n;

            if (CV.IsURLEncryption)
            {

                if (!encryptpwd.IsNull && encryptpwd.Value != String.Empty)
                {
                    if (IsBase64Encoded(encryptpwd.Value))
                    {
                        string decryptpwd = string.Empty;
                        UTF8Encoding encodepwd = new UTF8Encoding();
                        Decoder Decode = encodepwd.GetDecoder();
                        byte[] todecode_byte = Convert.FromBase64String(encryptpwd.Value);
                        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                        char[] decoded_char = new char[charCount];
                        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                        decryptpwd = new String(decoded_char);

                        if (int.TryParse(decryptpwd, out n))
                            return Convert.ToInt32(decryptpwd);
                        else return SqlInt32.Null;
                    }
                    else return SqlInt32.Null;
                }
                else
                    return SqlInt32.Null;
            }
            else
            {
                if (encryptpwd.IsNull)
                    return SqlInt32.Null;
                else if (int.TryParse(encryptpwd.Value, out n))
                    return Convert.ToInt32(encryptpwd.Value);
                else return SqlInt32.Null;
            }

        }
        #endregion'
    }
}
