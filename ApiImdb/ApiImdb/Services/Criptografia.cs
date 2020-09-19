using System.Security.Cryptography;
using System.Text;

namespace ApiImdb.Services
{
    public class Criptografia
    {
        #region CriptografiaMd5
        public string GerarHashMd5(string senha)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion
    }
}
