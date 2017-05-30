namespace NotificationJob
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vaccinations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vaccinations()
        {
            vaccination_check = new HashSet<vaccination_check>();
        }

        [Key]
        public int vaccine_id { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        public int injection_month { get; set; }

        public int program_id { get; set; }

        public int vaccineinfo_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vaccination_check> vaccination_check { get; set; }
    }
}
