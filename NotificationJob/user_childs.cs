namespace NotificationJob
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_childs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user_childs()
        {
            vaccination_check = new HashSet<vaccination_check>();
        }

        [Key]
        public int child_id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Required]
        [StringLength(5)]
        public string gender { get; set; }

        public DateTime birthday { get; set; }

        [Required]
        [StringLength(50)]
        public string api_key { get; set; }

        public int user_id { get; set; }

        public int program_id { get; set; }

        public virtual users users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vaccination_check> vaccination_check { get; set; }
    }
}
