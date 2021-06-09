using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace foodfun.Models
{
    [MetadataType(typeof(ProgramListMetaData))]

    public partial class ProgramList
    {

        private class ProgramListMetaData
        {
            [Key]
            public int rowid { get; set; }
            public Nullable<int> parentid { get; set; }
            public string pro_no { get; set; }
            public string pro_name { get; set; }

            public string controller_name { get; set; }
            public string action_name { get; set; }
        }
    }
}