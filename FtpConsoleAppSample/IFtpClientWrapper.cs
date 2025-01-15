using System.Collections.Generic;
using FluentFTP;

namespace FtpConsoleAppSample
{
    public interface IFtpClientWrapper
    {
        void Connect();
        FtpStatus UploadFile(string localFilePath, string remoteFilePath);
        FtpStatus DownloadFile(string localFilePath, string remoteFilePath);
        IEnumerable<FtpListItem> GetListing(string remoteDirectory);
        bool CreateDirectory(string remoteDirectory);
        void Dispose();
    }
}

namespace FtpConsoleAppSample
{
    public class FtpClientWrapper : IFtpClientWrapper
    {
        private readonly FtpClient _ftpClient;

        public FtpClientWrapper(string host, string username, string password)
        {
            _ftpClient = new FtpClient(host, username, password);
        }

        public void Connect()
        {
            _ftpClient.Connect();
        }

        public FtpStatus UploadFile(string localFilePath, string remoteFilePath)
        {
            return _ftpClient.UploadFile(localFilePath, remoteFilePath);
        }

        public FtpStatus DownloadFile(string localFilePath, string remoteFilePath)
        {
            return _ftpClient.DownloadFile(localFilePath, remoteFilePath);
        }

        public IEnumerable<FtpListItem> GetListing(string remoteDirectory)
        {
            return _ftpClient.GetListing(remoteDirectory);
        }

        public bool CreateDirectory(string remoteDirectory)
        {
            return _ftpClient.CreateDirectory(remoteDirectory);
        }

        public void Dispose()
        {
            _ftpClient.Dispose();
        }
    }
}
