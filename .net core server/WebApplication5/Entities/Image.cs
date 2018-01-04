using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Entities
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string type { get; set; }

        [Column(TypeName = "image")]
        public byte[] bytes { get; set; }

        public int? IdUser { get; set; }

        public virtual User User { get; set; }
    }
}
