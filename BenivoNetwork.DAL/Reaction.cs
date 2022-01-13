//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BenivoNetwork.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reaction()
        {
            this.Notifications = new HashSet<Notification>();
        }
    
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> PostID { get; set; }
        public Nullable<int> CommentID { get; set; }
        public Nullable<int> MessageID { get; set; }
    
        public virtual Comment Comment { get; set; }
        public virtual Message Message { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}