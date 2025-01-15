using System.Collections.Generic;
using FluentFTP;

namespace FtpConsoleAppSample
{
    public class FluentFtpClient : IFluentFtpClient
    {
        private readonly IFtpClientWrapper _ftpClientWrapper;

        public FluentFtpClient(IFtpClientWrapper ftpClientWrapper)
        {
            _ftpClientWrapper = ftpClientWrapper;
        }

        public FluentFtpClient(string host, string username, string password)
        {
            _ftpClientWrapper = new FtpClientWrapper(host, username, password);
        }

        public void Connect()
        {
            _ftpClientWrapper.Connect();
        }

        public FtpStatus UploadFile(string localFilePath, string remoteFilePath)
        {
            return _ftpClientWrapper.UploadFile(localFilePath, remoteFilePath);
        }

        public FtpStatus DownloadFile(string localFilePath, string remoteFilePath)
        {
            return _ftpClientWrapper.DownloadFile(localFilePath, remoteFilePath);
        }

        public IEnumerable<FtpListItem> GetListing(string remoteDirectory)
        {
            return _ftpClientWrapper.GetListing(remoteDirectory);
        }

        public bool CreateDirectory(string remoteDirectory)
        {
            return _ftpClientWrapper.CreateDirectory(remoteDirectory);
        }

        public void Dispose()
        {
            _ftpClientWrapper.Dispose();
        }
    }
}
