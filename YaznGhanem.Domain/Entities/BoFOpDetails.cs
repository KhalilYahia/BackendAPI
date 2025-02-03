using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class BoFOpDetails
    {
        public int Id { set; get; }
        /// <summary>
        /// داخل ممتلئ
        /// داخل فارغ
        /// خارج
        /// </summary>
        public string Direction { set; get; }

        public int Count { set; get; }

        public string ColorType { set; get; }

        public int BoFOperationId { get; set; }
        public virtual BoFOperations BoFOperation { set; get; }


    }
}
