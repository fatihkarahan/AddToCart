using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCustomer.Model
{
    /// <summary>The Api result model.</summary>
    /// <typeparam name="T">the Generic Data</typeparam>
    public class ApiResultModel<T>
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ApiResultModel{T}"/> class.</summary>
        public ApiResultModel()
        {
            this.Status = ResultStatusEnum.UnSuccess;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the data.</summary>
        public T Data { get; set; }

        /// <summary>Gets or sets the data count.</summary>
        public int DataCount { get; set; }

        /// <summary>Gets or sets the exception.</summary>
        public Exception Exception { get; set; }

        /// <summary>Gets or sets the message.</summary>
        public string Message { get; set; }

        /// <summary>Gets or sets the status.</summary>
        public ResultStatusEnum Status { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The set data.</summary>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        public void SetData(T data, string message)
        {
            this.Data = data;
            this.Status = ResultStatusEnum.Success;
            this.Message = message;
        }

        /// <summary>The set data.</summary>
        /// <param name="data">The data.</param>
        public void SetData(T data)
        {
            this.SetData(data, string.Empty);
        }

        /// <summary>The set no data.</summary>
        /// <param name="message">The message.</param>
        public void SetNoData(string message)
        {
            this.Data = default(T);
            this.Status = ResultStatusEnum.NoData;
            this.Message = message;
        }

        /// <summary>The set no data.</summary>
        public void SetNoData()
        {
            this.SetNoData(string.Empty);
        }

        /// <summary>The set locked.</summary>
        /// <param name="message">The message.</param>
        public void SetLocked(string message)
        {
            this.Status = ResultStatusEnum.Locked;
            this.Message = message;
        }

        #endregion
    }
}
