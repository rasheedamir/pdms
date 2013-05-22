
namespace PackageDistributionService.Core.Dto
{
    /// <summary>
    /// A file package dto
    /// </summary>
    public class FilePackage : BaseDto
    {
        #region Constructors
        #endregion

        #region Members

        private string _filename;
        private string _md5CheckSum;
        private byte[] _fileContents;
        private int _status;
        private string _message;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Md5CheckSum
        {
            get { return _md5CheckSum; }
            set { _md5CheckSum = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] FileContents
        {
            get { return _fileContents; }
            set { _fileContents = value; }
        }

        /// <summary>
        /// 0 - No file
        /// 1 - Some issue. See Message for details.
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        #endregion

        #region Properties
        #endregion
    }
}
