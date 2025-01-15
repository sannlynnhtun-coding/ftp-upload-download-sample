using FluentFTP;

namespace FtpConsoleAppSample
{
    public interface IFluentFtpClient
    {
        void Connect();
        bool CreateDirectory(string remoteDirectory);
        void Dispose();
        FtpStatus DownloadFile(string localFilePath, string remoteFilePath);
        IEnumerable<FtpListItem> GetListing(string remoteDirectory);
        FtpStatus UploadFile(string localFilePath, string remoteFilePath);
    }
}