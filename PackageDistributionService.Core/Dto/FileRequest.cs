
namespace PackageDistributionService.Core.Dto
{
    /// <summary>
    /// File request dto
    /// </summary>
    public class FileRequest : BaseDto
    {
        #region Constructors
        #endregion

        #region Members

        private int _coopStoreId;
        private string _posNumber; 
        private string _filename;

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
        public int CoopStoreId
        {
            get { return _coopStoreId; }
            set { _coopStoreId  = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PosNumber
        {
            get { return _posNumber; }
            set { _posNumber = value; }
        }

        #endregion

        #region Properties
        #endregion
    }
}
