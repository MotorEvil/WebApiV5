//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiV5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Treniruotes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Treniruotes()
        {
            this.SignUps = new HashSet<SignUps>();
        }
    
        public int Id { get; set; }
        public Nullable<int> UsersId { get; set; }
        public string Time { get; set; }
        public Nullable<int> FreeSpaces { get; set; }
        public Nullable<int> Joins { get; set; }
        public string TName { get; set; }
        public string UsersString { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SignUps> SignUps { get; set; }
        public virtual Users Users { get; set; }
    }
}
