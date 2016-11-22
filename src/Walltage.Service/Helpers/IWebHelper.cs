
namespace Walltage.Service.Helpers
{
    public interface IWebHelper
    {
        /// <summary>
        /// Get public IP address from http://checkip.dyndns.org
        /// </summary>
        /// <returns>URL referrer</returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// Get the encrypted hash string with MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Encrypted hash String</returns>
        string EncryptToMd5(string text);
    }
}
