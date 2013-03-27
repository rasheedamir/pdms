using System;
using System.Runtime.Serialization;

namespace PackageDistributionService.Core.Dto
{
    public class PosPackage : BaseDto //, IPosPackage
    {
        #region Constructors
        #endregion

        #region Members

        private int _packageVersionId;
        private string _filename;
        private DateTime? _activationTime;
        private string _md5CheckSum;
        private byte[] _packageContents;
        private int _status;
        private string _message;

        #endregion

        #region Properties

        public int PackageVersionId
        {
            get { return _packageVersionId; }
            set { _packageVersionId = value; }
        }

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public DateTime? ActivationTime
        {
            get { return _activationTime; }
            set { _activationTime = value; }
        }

        public string Md5CheckSum
        {
            get { return _md5CheckSum; }
            set { _md5CheckSum = value; }
        }

        public byte[] PackageContents
        {
            get { return _packageContents; }
            set { _packageContents = value; }
        }

        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        #endregion

        #region Methods

        public string ToStringMini()
        {
            return String.Format(" Status: {0} | Message: {1} | PackageVersionId: {2} | ActivationTime: {3}", _status, _message, _packageVersionId, _activationTime);
        }

        #endregion
    }
}
