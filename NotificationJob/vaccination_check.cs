namespace NotificationJob
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vaccination_check
    {
        [Key]
        public int vac_check_id { get; set; }

        public bool is_vac_done { get; set; }

        public bool is_notification_sent { get; set; }

        public int child_id { get; set; }

        public int vaccine_id { get; set; }

        public virtual user_childs user_childs { get; set; }

        public virtual vaccinations vaccinations { get; set; }
    }
}
