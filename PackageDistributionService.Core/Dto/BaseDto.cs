using System;
using System.Runtime.Serialization;
using System.Text;

namespace PackageDistributionService.Core.Dto
{
    //[Serializable]
    [DataContract]
    public abstract class BaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var property in GetType().GetProperties())
            {
                sb.Append(property.Name);
                sb.Append(": ");
                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    sb.Append(property.GetValue(this, null));
                }

                sb.Append(" | ");
            }

            return sb.ToString();
        }
    }
}
