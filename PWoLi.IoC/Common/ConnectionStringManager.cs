using System.Data.SqlClient;

namespace PWoLi.IoC
{
    public class ConnectionStringManager
    {
        private readonly string _encryptedConnectionString;
        private readonly string _passPhrase;

        public ConnectionStringManager(string encryptedConnectionString, string passPhrase)
        {
            _encryptedConnectionString = encryptedConnectionString ?? throw new System.ArgumentNullException(nameof(encryptedConnectionString));
            _passPhrase = passPhrase ?? throw new System.ArgumentNullException(nameof(passPhrase));
        }

        public string GetConnectionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(_encryptedConnectionString);
            string eUserName = connectionStringBuilder.UserID;
            string ePassWord = connectionStringBuilder.Password;

            string userName = DESProvider.Decrypt(eUserName, _passPhrase);
            string passWord = DESProvider.Decrypt(ePassWord, _passPhrase);

            connectionStringBuilder.UserID = userName;
            connectionStringBuilder.Password = passWord;

            return connectionStringBuilder.ConnectionString;
        }
    }
}