using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class BoFOperations
    {
        public int Id { set; get; }

        public int Count { set; get; }

        /////// <summary>
        /////// داخل ممتلئ
        /////// داخل فارغ
        /////// خارج
        /////// </summary>
        ////public string Direction { set; get; }

        public int BoFUserId { get; set; }
        public string BoFUserName { get; set; }
        public virtual BoFUser BoFUser { set; get; }




        public DateTime Date { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<BoFOpDetails> BoFOpDetails { set; get; }

    }
}
