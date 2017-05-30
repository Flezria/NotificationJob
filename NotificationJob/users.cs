namespace NotificationJob
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public users()
        {
            user_childs = new HashSet<user_childs>();
        }

        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(30)]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string surname { get; set; }

        public int mobile { get; set; }

        [StringLength(50)]
        public string api_key { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_childs> user_childs { get; set; }
    }
}
